using Lakeshore.SpecialOrderPickupStatus.Application.SendSpecialOrder;
using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.SendSpecialOrder
{
    public class OrderLineQueryRepository : IOrderLineQueryRepository
    {
        private readonly SpecialOrderDbContext _context;

        public OrderLineQueryRepository(SpecialOrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<OrderLine>> GetOrderLine(CancellationToken cancellationToken)
        {
            var orderLine = await _context.OrderLine.ToListAsync();

            return orderLine;
        }
    }
}
