using OrderEntity = Domain.Models.Order.Order;

namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, a => a.WithOwner());

            builder.HasMany(o => o.OrderItems).WithOne();

            builder.Property(o => o.Status)
                .HasConversion(s => s.ToString(), s => Enum.Parse<OrderPaymentStatus>(s));

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany().HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");

        }
    }
}
