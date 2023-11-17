using Lakeshore.SpecialOrderPickupStatus.Domain.Models;

namespace Lakeshore.SpecialOrderPickupStatus.Domain.SpecialOrderPickupStatus;

public interface IOrderShippingCommandRepository
{
    Task Update(decimal storeTransactionNumber,CancellationToken cancellationToken);
}
