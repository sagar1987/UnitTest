using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration
{
    public class OrderLineEntityConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> entity)
        {
            entity.HasKey(e => new { e.StoreTransactionNumber, e.StoreNumber, e.EntryDateTime});

            entity.ToTable("order_line");

            entity.Property(e => e.StoreTransactionNumber).HasColumnName("store_transaction_no");
            entity.Property(e => e.StoreNumber).HasColumnName("store_no");
            entity.Property(e => e.EntryDateTime).HasColumnName("entry_datetime");
            entity.Property(e => e.StoreLineId).HasColumnName("store_line_id");
            entity.Property(e => e.ItemNumber).HasColumnName("item_no");
            entity.Property(e => e.Quantity).HasColumnName("qty");
        }
    }
}
