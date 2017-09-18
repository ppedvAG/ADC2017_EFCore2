using Ef6_QuerySpeedTest.Configurations;
using Ef6_QuerySpeedTest.Models;
using System.Data.Entity;

namespace Ef6_QuerySpeedTest
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
