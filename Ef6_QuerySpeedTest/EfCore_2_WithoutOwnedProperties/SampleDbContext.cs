using EfCore_2_WithoutOwnedProperties.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCore_2_WithoutOwnedProperties
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
            modelBuilder.ApplyConfiguration(new Configurations.CustomerConfiguration());            
            modelBuilder.ApplyConfiguration(new Configurations.OrderConfiguration());            
            modelBuilder.ApplyConfiguration(new Configurations.OrderDetailConfiguration());            
            modelBuilder.ApplyConfiguration(new Configurations.ProductConfiguration());
        }
    }
}