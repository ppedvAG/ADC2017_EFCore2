using Ef6_QuerySpeedTest.Models;
using System.Data.Entity.ModelConfiguration;

namespace Ef6_QuerySpeedTest.Configurations
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Produkte");

            Property(p => p.Id)
                   .HasColumnName("PNR");

            Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode();

            HasMany(p => p.OrderDetails)
                   .WithRequired(od => od.Product)
                   .HasForeignKey(od => od.ProductId);
        }
    }
}