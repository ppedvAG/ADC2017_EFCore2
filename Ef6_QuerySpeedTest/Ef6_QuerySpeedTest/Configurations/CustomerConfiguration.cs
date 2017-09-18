using Ef6_QuerySpeedTest.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ef6_QuerySpeedTest.Configurations
{
    internal class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Kunden");

            HasKey(c => c.Id);
            Property(c => c.Id)
                   .HasColumnName("KDNR");

            Property(c => c.LastUpdated)
                   .HasColumnName("LetztesUpdate")
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(c => c.IsDeleted)
                   .HasColumnName("Gelöscht");

            Property(c => c.Firstname)
                   .HasColumnName("Vorname")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false);

            Property(c => c.Lastname)
                   .HasColumnName("Nachname")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false);

            Property(c => c.Birthdate)
                   .HasColumnName("Geburtsdatum")
                   .HasColumnType("date")
                   .IsRequired();

            Property(c => c.Adress.City)
               .HasColumnName("Stadt")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            Property(c => c.Adress.Country)
               .HasColumnName("Land")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            Property(c => c.Adress.ZipCode)
               .HasColumnName("Postleitzahl")
               .IsUnicode()
               .HasMaxLength(6)
               .IsRequired();

            Property(c => c.Adress.Street)
               .HasColumnName("Straße")
               .IsUnicode()
               .HasMaxLength(50)
               .IsRequired();

            HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}