using Lakeshore.SpecialOrderPickupStatus.Application.SpecialOrderPickupStatus.Command.UpdateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lakeshore.SpecialOrderPickupStatus.Controllers
{
    [ApiController]
    [Route("itint-msv-order-pickup-notification-special-ib")]
    public class SpecialOrderPickupStatusController : ControllerBase
    {       
        private readonly Serilog.ILogger _logger;

        private readonly IMediator _mediator;
       
        public SpecialOrderPickupStatusController(  IMediator mediator, Serilog.ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("ProcessSpecialOrderStatusForSAP")]
        public async Task<ActionResult<bool>> ExtractAllSpecialOrder(CancellationToken cancellationToken)
        {
            try
            {
                var extracted = await _mediator.Send(new ExtractSpecialOrderCommand(), cancellationToken);
                
                return Ok(extracted);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }

           
        }

    }
}