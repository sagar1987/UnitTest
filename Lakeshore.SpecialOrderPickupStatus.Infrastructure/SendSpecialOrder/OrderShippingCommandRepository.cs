using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Lakeshore.SpecialOrderPickupStatus.Domain.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.SpecialOrderPickupStatus;

public class OrderShippingCommandRepository : IOrderShippingCommandRepository
{
    private readonly SpecialOrderDbContext _context;
    
    public OrderShippingCommandRepository(SpecialOrderDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));        
    }

    public Task Update(decimal storeTransactionNumber, CancellationToken cancellationToken)
    {
        var orderShipping = _context.OrderShipping.Where(item => item.StoreTransactionNumber == storeTransactionNumber);

        foreach (var item in orderShipping)
        {
            item.Bart_Status = "C";
            item.BartProcessedDatetime = DateTime.Now;
            var orderShippingNew = _context.OrderShipping.Attach(item);
            orderShippingNew.State = EntityState.Modified;
        }
        return Task.CompletedTask;
    }
}
