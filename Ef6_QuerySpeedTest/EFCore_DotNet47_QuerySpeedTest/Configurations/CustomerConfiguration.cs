using EFCore_DotNet47_QuerySpeedTest.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_DotNet47_QuerySpeedTest.Configurations
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
                   .HasColumnName("LetztesUpdate")
                   .HasComputedColumnSql("GETDATE()")
                   .ValueGeneratedOnAddOrUpdate();

            // builder.Property<DateTime>("LastUpdated")
            //        .HasColumnName("LetztesUpdate")
            //        .HasComputedColumnSql("GETDATE()")
            //        .ValueGeneratedOnAddOrUpdate();

            builder.Property(c => c.IsDeleted)
                   .HasColumnName("Gelöscht");

            // builder.Property<bool>("IsDeleted")
            //        .HasColumnName("Gelöscht");

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

            builder.OwnsOne(c => c.Adress, abuilder =>
            {
                abuilder.Property(a => a.City)
                   .HasColumnName("Stadt")
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired();
                   
                abuilder.Property(a => a.Country)
                   .HasColumnName("Land")
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired();
            
                abuilder.Property(a => a.ZipCode)
                   .HasColumnName("Postleitzahl")
                   .IsUnicode()
                   .HasMaxLength(6)
                   .IsRequired();
                   
                abuilder.Property(a => a.Street)
                   .HasColumnName("Straße")
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired();
            });

            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}