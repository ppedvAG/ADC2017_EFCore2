using Ef6_QuerySpeedTest.Models;
using System.Data.Entity.ModelConfiguration;

namespace Ef6_QuerySpeedTest.Configurations
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("Bestelldetails");

            Property(od => od.Id)
                   .HasColumnName("BDNR");

            Property(od => od.OrderId)
                   .HasColumnName("BNR");

            Property(od => od.ProductId)
                   .HasColumnName("PNR");
        }
    }
}