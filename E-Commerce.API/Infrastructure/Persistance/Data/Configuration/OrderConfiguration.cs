using Domain.Entities.OrderModule;

namespace Persistance.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.Address, a => a.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.PaymentStatus).HasConversion(
                p => p.ToString(), p => Enum.Parse<OrderPaymentStatus>(p));
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,4)");
        }
    }
}
