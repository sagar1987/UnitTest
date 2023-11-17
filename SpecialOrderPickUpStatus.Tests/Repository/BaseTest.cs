using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SpecialOrderStatusPickUpStatus.Tests.Repository
{
    public abstract class BaseTest
    {
        protected readonly DbContextOptions<SpecialOrderDbContext> DbContextOptions;
        protected SpecialOrderDbContext SpecialOrderDbContext;
        protected Mock<ILogger> LoggerMock;

        protected BaseTest()
        {
            DbContextOptions = new DbContextOptionsBuilder<SpecialOrderDbContext>()
                .UseInMemoryDatabase(databaseName: "SpecialOrderData")
                .Options;

            SpecialOrderDbContext = new SpecialOrderDbContext(DbContextOptions);

            LoggerMock = new Mock<ILogger>();
        }
        protected void SeedDatabase()
        {
            SpecialOrderDbContext.Database.EnsureCreated();

            SeedOrderShippingData();
            SeedOrderLineData();

            SpecialOrderDbContext.SaveChanges();

            foreach (var entity in SpecialOrderDbContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        private void SeedOrderShippingData()
        {
            var orderShippings = new List<OrderShipping>
            {
                new OrderShipping(124,1,new DateTime(),23,"STORE PICKUP",23,"R",new DateTime()),
                new OrderShipping(125,2,new DateTime(),25,"testOrderType2",236,"testBartStatus2",new DateTime())
            };

            SpecialOrderDbContext.OrderShipping.AddRange(orderShippings);
        }

        private void SeedOrderLineData()
        {
            var orderLines = new List<OrderLine>
            {
                new OrderLine(124,1,new DateTime(),134,"testItemNumber",2),
                new OrderLine(125,2,new DateTime(),137,"testItemNumber",2)
            };

            SpecialOrderDbContext.OrderLine.AddRange(orderLines);
        }

    }
}
