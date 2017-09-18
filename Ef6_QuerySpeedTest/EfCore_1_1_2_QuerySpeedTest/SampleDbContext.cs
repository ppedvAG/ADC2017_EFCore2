using EfCore_1_1_2_QuerySpeedTest.Configurations;
using EfCore_1_1_2_QuerySpeedTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCore_1_1_2_QuerySpeedTest
{
    public class SampleDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        public SampleDbContext(string connectionString) => _connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // In Ef Core 1.1 gibt es noch nicht das Konzept der eigenen Konfigurationsklassen.
            // Siehe ModelBuilderExtensions
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }

    /// <summary>
    /// In Ef Core 1.1 gibt es noch nicht das Konzept der eigenen Konfigurationsklassen.
    /// Darum wurde hier die Erweiterung mit inkludiert.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfiguration<T>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<T> configruation)
            where T : class
            => modelBuilder.Entity<T>(configruation.Configure);
    }
}