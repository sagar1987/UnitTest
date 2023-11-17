using Lakeshore.SpecialOrderPickupStatus.Domain.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Domain;
using MediatR;
using Lakeshore.SpecialOrderPickupStatus.Dto.SpecialOrderPickupStatus;
using Lakeshore.SpecialOrderPickupStatus.Application.SendSpecialOrder;
using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus.Command.UpdateOrder;

public class ExtractSpecialOrderCommandHandler : IRequestHandler<ExtractSpecialOrderCommand, bool>
{
    private readonly IOrderShippingCommandRepository _commandRepository;
    private readonly IOrderShippingQueryRepository _queryRepository;
    private readonly IOrderLineQueryRepository _orderLineQueryRepository;
    private readonly ICommandUnitOfWork _unitWork;
    private readonly Serilog.ILogger _logger;

    public ExtractSpecialOrderCommandHandler(IOrderShippingCommandRepository commandRepository,
        IOrderShippingQueryRepository queryRepository,
        IOrderLineQueryRepository orderLineQueryRepository,
        ICommandUnitOfWork unitWork,
        Serilog.ILogger logger)
    {
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
        _queryRepository = queryRepository;
        _orderLineQueryRepository = orderLineQueryRepository;
        _unitWork = unitWork ?? throw new ArgumentNullException(nameof(unitWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(ExtractSpecialOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderShipping = await _queryRepository.GetPickedOrderShipping(cancellationToken);
            var orderLine = await _orderLineQueryRepository.GetOrderLine(cancellationToken);

            var result = from ordShip in orderShipping
                         join ordLine in orderLine
                         on new { ordShip.StoreTransactionNumber, ordShip.StoreNumber }
                         equals new { ordLine.StoreTransactionNumber, ordLine.StoreNumber }
                         group ordLine by new { ordShip.StoreTransactionNumber } into g
                         select new
                         {
                             storeTransactionNumber = g.Key.StoreTransactionNumber,
                             orderLine = g.ToList()
                         };

          
            foreach (var transNo in result)
            {
                SpecialOrderDto specialOrderDto = new SpecialOrderDto();
                Header header = new Header();
                header.TypeOfOrder = "Special";
                header.OrderNumber = transNo.storeTransactionNumber.ToString();
                header.HeaderStatus = "Picked";
                List<Item> items = new List<Item>();
                foreach (var orderline in transNo.orderLine)
                {
                    items.Add(new Item
                    {
                        OrderLine = orderline.StoreLineId,
                        ItemStatus = "Picked",
                        ItemNumber = orderline.ItemNumber,
                        Quantity = orderline.Quantity
                    });
                }
                header.Items = items;

                var orderShipUpdate = orderShipping.FirstOrDefault(x => x.StoreTransactionNumber == transNo.storeTransactionNumber);
                orderShipUpdate?.StatusUpdate(specialOrderDto);

                await _commandRepository.Update(transNo.storeTransactionNumber, cancellationToken);
            }

            await _unitWork.SaveChangesAsync(cancellationToken);
            _logger.Information("Processing Aptos Open PO records...");

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }
        return true;
    }
}
