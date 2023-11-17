using Lakeshore.SpecialOrderPickupStatus.Domain.Models;

namespace Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus;

public interface IOrderShippingQueryRepository
{
    Task<List<OrderShipping>> GetPickedOrderShipping(CancellationToken cancellationToken);
}
