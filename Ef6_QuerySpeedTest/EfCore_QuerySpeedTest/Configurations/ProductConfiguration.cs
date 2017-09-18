using EfCore_QuerySpeedTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_QuerySpeedTest.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produkte");

            builder.Property(p => p.Id)
                   .HasColumnName("PNR");

            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode();

            builder.HasMany(p => p.OrderDetails)
                   .WithOne(od => od.Product)
                   .HasForeignKey(od => od.ProductId);
        }
    }
}