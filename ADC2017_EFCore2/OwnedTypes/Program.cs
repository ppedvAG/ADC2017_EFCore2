using Microsoft.EntityFrameworkCore;
using OwnedTypes.Models;

namespace OwnedTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();
        }

        private static void SetupDatabase()
        {
            using (var context = new CustomersDbContext())
                context.Database.EnsureCreated();
        }
    }


    internal class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.OwnedTypes;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.Firstname).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Lastname).IsRequired();
        }
    }
}
