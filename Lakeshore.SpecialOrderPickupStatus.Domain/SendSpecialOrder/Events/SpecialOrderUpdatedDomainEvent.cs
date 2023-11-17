using Lakeshore.SpecialOrderPickupStatus.Dto.SpecialOrderPickupStatus;
using MediatR;
using System.Text.Json;

namespace Lakeshore.SpecialOrderPickupStatus.Domain.Har.Events;

public class SpecialOrderUpdatedDomainEvent : IDomainEvent
{
    public SpecialOrderDto orderExtract { get;}

    public Guid Id => new Guid();

    public DateTime OccurredOn => DateTime.Now;

    public string NotificationJson => JsonSerializer.Serialize(orderExtract);

    public SpecialOrderUpdatedDomainEvent(SpecialOrderDto orderExtract)
    {
        this.orderExtract = orderExtract;        
    }
}
