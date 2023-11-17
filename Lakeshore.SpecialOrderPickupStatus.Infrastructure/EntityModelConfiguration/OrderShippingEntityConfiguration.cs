using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lakeshore.SpecialOrderPickupStatus.Domain.Models;


namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;

public class OrderShippingEntityConfiguration : IEntityTypeConfiguration<OrderShipping>
{
    public void Configure(EntityTypeBuilder<OrderShipping> entity)
    {
        entity.HasKey(e => new { e.StoreTransactionNumber, e.StoreNumber, e.EntryDateTime, e.SequenceNumber });

        entity.ToTable("order_shipping");

        entity.Property(e => e.StoreTransactionNumber).HasColumnName("store_transaction_no");
        entity.Property(e => e.StoreNumber).HasColumnName("store_no");
        entity.Property(e => e.EntryDateTime).HasColumnName("entry_datetime");
        entity.Property(e => e.SequenceNumber).HasColumnName("sequence_no");
        entity.Property(e => e.OrderType).HasColumnName("order_type");
        entity.Property(e => e.Es_Order_Id).HasColumnName("es_order_id");
        entity.Property(e => e.Bart_Status).HasColumnName("bart_status");
        entity.Property(e => e.BartProcessedDatetime).HasColumnName("bart_processed_datetime");
    }
}
