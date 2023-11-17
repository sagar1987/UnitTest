using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.SpecialOrderPickupStatus.Application.SendSpecialOrder
{
    public interface IOrderLineQueryRepository
    {
        Task<List<OrderLine>> GetOrderLine(CancellationToken cancellationToken);
    }
}
