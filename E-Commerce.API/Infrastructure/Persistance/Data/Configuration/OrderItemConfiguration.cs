using Domain.Entities.OrderModule;

namespace Persistance.Data.Configuration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,4)");
            builder.OwnsOne(o => o.Product, p => p.WithOwner());
        }
    }
}
