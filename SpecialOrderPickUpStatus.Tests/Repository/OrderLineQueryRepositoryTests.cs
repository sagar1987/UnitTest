using Lakeshore.SpecialOrderPickupStatus.Application.SendSpecialOrder;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.SendSpecialOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialOrderStatusPickUpStatus.Tests.Repository
{
    [TestFixture]
    public class OrderLineQueryRepositoryTests : BaseTest
    {
        private IOrderLineQueryRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDatabase();

            _repository = new OrderLineQueryRepository(SpecialOrderDbContext);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            SpecialOrderDbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task GetAll_ReturnsCorrectNumberOfRecords()
        {
            var records = await _repository.GetOrderLine(CancellationToken.None);

            Assert.That(records.Count, Is.EqualTo(2));
        }
        [Test]
        public async Task GetAll_ReturnsStoreTransactionNo()
        {
            var records = await _repository.GetOrderLine(CancellationToken.None);

            Assert.IsTrue(records.Any(q => q.StoreTransactionNumber == 125));
        }
        [Test]
        public async Task GetAllAccounts_ReturnsCorrectStoreNo()
        {
            var records = await _repository.GetOrderLine(CancellationToken.None);

            Assert.IsTrue(records.Any(q => q.StoreNumber == 2));
        }
    }
}
