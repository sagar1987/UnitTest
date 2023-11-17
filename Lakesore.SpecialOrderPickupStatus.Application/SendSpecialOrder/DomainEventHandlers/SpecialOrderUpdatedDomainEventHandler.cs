using Lakeshore.SpecialOrderPickupStatus.Domain.Har.Events;
using Lakeshore.Kafka.Client.Interfaces;
using MediatR;
using System.Text.Json;
using Confluent.Kafka;


namespace Lakeshore.SpecialOrderPickupStatus.Application.Har.DomainEventHandlers;

public class SpecialOrderUpdatedDomainEventHandler : INotificationHandler<SpecialOrderUpdatedDomainEvent>
{
    private readonly IKafkaProducerClient _kafkaProducerClient;

    public SpecialOrderUpdatedDomainEventHandler(IKafkaProducerClient kafkaProducerClient)
    {
        _kafkaProducerClient = kafkaProducerClient ?? throw new ArgumentNullException(nameof(kafkaProducerClient));
    }
    public async Task Handle(SpecialOrderUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string strJson = JsonSerializer.Serialize(notification.orderExtract);
        await _kafkaProducerClient.Producer.ProduceAsync(_kafkaProducerClient.Topic, new Message<Null, string> { Value = strJson }, cancellationToken);
    }
}
