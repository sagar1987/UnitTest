using Lakeshore.SpecialOrderPickupStatus.Application.SendSpecialOrder;
using Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus.Command.UpdateOrder;
using Lakeshore.SpecialOrderPickupStatus.Domain;
using Lakeshore.SpecialOrderPickupStatus.Domain.Har.Events;
using Lakeshore.SpecialOrderPickupStatus.Domain.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.DomainEventsDispatching;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.SendSpecialOrder;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.SpecialOrderPickupStatus;
using MediatR;
using SpecialOrderStatusPickUpStatus.Tests.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptosOpenPo.Tests
{
    [TestFixture]
    public class ExtractSpecialOrderCommandHandlerTest : BaseTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IKafkaProducerClient> _kafkaProducerClientMock;
        private Mock<IProducer<Null, string>> _producerMock;
        private Mock<ICommandUnitOfWork> _commandUnitOfWorkMock;
        private Mock<Serilog.ILogger> _loggerMock;

        private IOrderShippingQueryRepository _orderShippingRepository;
        private IOrderShippingCommandRepository _orderShippingCommandRepository;
        private IOrderLineQueryRepository _orderLineQueryRepository;
        private IDomainEventsAccessor _domainEventsAccessor;

        private ExtractSpecialOrderCommandHandler _handler;

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange Database related objects
            SeedDatabase();

            _orderShippingRepository = new OrderShippingQueryRepository(SpecialOrderDbContext);
            _orderShippingCommandRepository = new OrderShippingCommandRepository(SpecialOrderDbContext);
            _orderLineQueryRepository = new OrderLineQueryRepository(SpecialOrderDbContext);
            _domainEventsAccessor = new DomainEventsAccessor(SpecialOrderDbContext);

            _loggerMock = new Mock<Serilog.ILogger> ();

            // Arrange Unit Of Work
            _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Publish(It.IsAny<SpecialOrderUpdatedDomainEvent>(), It.IsAny<CancellationToken>()));

            _producerMock = new Mock<IProducer<Null, string>>();
            _producerMock.Setup(m => m.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeliveryResult<Null, string>());

            _kafkaProducerClientMock = new Mock<IKafkaProducerClient>();
            _kafkaProducerClientMock.Setup(p => p.Producer)
                .Returns(_producerMock.Object);

            var commandUnitOfWork = new CommandUnitOfWork(
                SpecialOrderDbContext,
                _domainEventsAccessor,
                _mediatorMock.Object,
                _kafkaProducerClientMock.Object);

            _commandUnitOfWorkMock = new Mock<ICommandUnitOfWork>();
            _commandUnitOfWorkMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                // Allow us to defer calling the actual method, until its actually called within our test.
                .Returns(() => commandUnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()));

            // Arrange Command Handler
            _handler = new ExtractSpecialOrderCommandHandler(
                _orderShippingCommandRepository,
                _orderShippingRepository,
                _orderLineQueryRepository,
                _commandUnitOfWorkMock.Object,
                _loggerMock.Object);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            SpecialOrderDbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task WhenRunAptosOpenPoCommandRequestHandlerCalled_ThenPublishAndUpdate()
        {
            var command = new ExtractSpecialOrderCommand();

            await _handler.Handle(command, It.IsAny<CancellationToken>());

            var updatedOrderShipping = SpecialOrderDbContext.OrderShipping.ToList();

            _commandUnitOfWorkMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

            _mediatorMock.Verify(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

            Assert.IsTrue(updatedOrderShipping.Any(q => q.Bart_Status == "C"));
        }
    }
}
