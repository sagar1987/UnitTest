

using Lakeshore.SpecialOrderPickupStatus.Domain;

namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.DomainEventsDispatching;

public interface IDomainEventsAccessor
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}