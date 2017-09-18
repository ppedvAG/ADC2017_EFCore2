using EfCore_2_WithoutOwnedProperties.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_2_WithoutOwnedProperties.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Kunden");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("KDNR");

            builder.Property(c => c.LastUpdated)
                   .HasColumnName("LetztesUpdate");

            builder.Property(c => c.IsDeleted)
                   .HasColumnName("Gelöscht");

            builder.Property(c => c.Firstname)
                   .HasColumnName("Vorname")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false);

            builder.Property(c => c.Lastname)
                   .HasColumnName("Nachname")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false);

            builder.Property(c => c.Birthdate)
                   .HasColumnName("Geburtsdatum")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(a => a.City)
               .HasColumnName("Stadt")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(a => a.Country)
               .HasColumnName("Land")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(a => a.ZipCode)
               .HasColumnName("Postleitzahl")
               .IsUnicode()
               .HasMaxLength(6)
               .IsRequired();

            builder.Property(a => a.Street)
               .HasColumnName("Straße")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}