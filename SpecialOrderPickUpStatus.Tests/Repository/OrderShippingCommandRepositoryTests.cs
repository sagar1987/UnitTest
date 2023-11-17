using Lakeshore.SpecialOrderPickupStatus.Domain.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.SpecialOrderPickupStatus;


namespace SpecialOrderStatusPickUpStatus.Tests.Repository
{
    [TestFixture]
    public class OrderShippingCommandRepositoryTests : BaseTest
    {
        private IOrderShippingCommandRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDatabase();

            _repository = new OrderShippingCommandRepository(SpecialOrderDbContext);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            SpecialOrderDbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task UpdatetOrderShipping_Success()
        {
            decimal storeTransactionNumber = 124;

            await _repository.Update(storeTransactionNumber, default);

            Assert.IsTrue(SpecialOrderDbContext.OrderShipping.Any(q => q.Bart_Status == "C"));
        }
       
    }
}
