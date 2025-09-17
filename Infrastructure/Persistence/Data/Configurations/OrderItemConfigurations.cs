namespace Persistence.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(i => i.Price)
                .HasColumnType("decimal(18,3)");

            builder.OwnsOne(i => i.Product, p => p.WithOwner());
        }
    }
}
