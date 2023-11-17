using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.SpecialOrderPickupStatus;

public class OrderShippingQueryRepository : IOrderShippingQueryRepository
{
    private readonly SpecialOrderDbContext _context;

    public OrderShippingQueryRepository(SpecialOrderDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

   
    public async Task<List<OrderShipping>> GetPickedOrderShipping(CancellationToken cancellationToken)
    {
        var orderShipping = await (from p1 in _context.OrderShipping
                                   from p2 in _context.OrderShipping
                                   where p1.Es_Order_Id == p2.Es_Order_Id
                                   && p1.StoreTransactionNumber != p2.StoreTransactionNumber
                                   && p1.StoreNumber == p2.StoreNumber
                                   && p1.OrderType == "STORE PICKUP"
                                   && p1.Bart_Status == "R"
                                   select p1).ToListAsync();
                    
        return orderShipping;
    }
}
