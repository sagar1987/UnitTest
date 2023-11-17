using Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.SpecialOrderPickupStatus;

namespace SpecialOrderStatusPickUpStatus.Tests.Repository
{
    [TestFixture]
    public class OrderShippingQueryRepositoryTests : BaseTest
    {
        private IOrderShippingQueryRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDatabase();

            _repository = new OrderShippingQueryRepository(SpecialOrderDbContext);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            SpecialOrderDbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task GetAll_ReturnsCorrectNumberOfRecords()
        {
            var records = await _repository.GetPickedOrderShipping(CancellationToken.None);

            Assert.That(records.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task GetAll_ReturnsStoreTransactionNo()
        {
            var records = await _repository.GetPickedOrderShipping(CancellationToken.None);

            Assert.That(records.Count, Is.GreaterThan(0));

            Assert.IsTrue(records.Any(q => q.StoreTransactionNumber == 124));
        }
    }
}
