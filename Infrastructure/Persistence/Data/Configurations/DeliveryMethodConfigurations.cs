﻿namespace Persistence.Data.Configurations
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Cost)
                .HasColumnType("decimal(18,3)");
        }
    }
}
