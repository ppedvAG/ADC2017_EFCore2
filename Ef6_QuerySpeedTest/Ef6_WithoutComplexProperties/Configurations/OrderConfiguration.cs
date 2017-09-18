using Ef6_WithoutComplexProperties.Models;
using System.Data.Entity.ModelConfiguration;

namespace Ef6_WithoutComplexProperties.Configurations
{
    internal class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Bestellungen");

            HasKey(o => o.Id);
            Property(o => o.Id).HasColumnName("BNR");

            Property(o => o.CustomerId)
                   .HasColumnName("KDNR");

            Property(o => o.OrderDate)
                   .HasColumnName("BestellDatum")
                   .HasColumnType("date")
                   .IsRequired();

            Property(o => o.Freight)
                   .HasColumnName("Lieferkosten")
                   .IsRequired();

            HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId);
        }
    }
}