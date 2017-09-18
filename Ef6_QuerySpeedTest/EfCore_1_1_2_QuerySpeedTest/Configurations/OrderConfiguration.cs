using EfCore_1_1_2_QuerySpeedTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_1_1_2_QuerySpeedTest.Configurations
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