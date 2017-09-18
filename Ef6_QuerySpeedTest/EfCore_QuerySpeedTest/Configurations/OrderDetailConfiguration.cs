using EfCore_QuerySpeedTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_QuerySpeedTest.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("Bestelldetails");

            builder.Property(od => od.Id)
                   .HasColumnName("BDNR");

            builder.Property(od => od.OrderId)
                   .HasColumnName("BNR");

            builder.Property(od => od.ProductId)
                   .HasColumnName("PNR");
        }
    }
}