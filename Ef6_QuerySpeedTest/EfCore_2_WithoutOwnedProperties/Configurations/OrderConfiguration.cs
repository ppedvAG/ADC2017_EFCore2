using EfCore_2_WithoutOwnedProperties.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_2_WithoutOwnedProperties.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Bestellungen");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("BNR");

            builder.Property(o => o.CustomerId)
                   .HasColumnName("KDNR");

            builder.Property(o => o.OrderDate)
                   .HasColumnName("BestellDatum")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(o => o.Freight)
                   .HasColumnName("Lieferkosten")
                   .IsRequired();

            builder.HasMany(o => o.OrderDetails)
                   .WithOne(od => od.Order)
                   .HasForeignKey(od => od.OrderId);
        }
    }
}