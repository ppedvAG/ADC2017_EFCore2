using FieldMapping.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FieldMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();

            using (var context = new BooksDbContext())
            {
                var author = new Author();
                author.Name = "Robert C Martin";
                author.SetUrl("http://blog.cleancoder.com/");

                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        private static void SetupDatabase()
        {
            using (var db = new BooksDbContext())
            {
                Console.WriteLine("Recreating database from current model");
                Console.WriteLine(" Dropping database...");
                db.Database.EnsureDeleted();

                Console.WriteLine(" Creating database...");
                db.Database.EnsureCreated();
            }
        }
    }

    internal class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property<string>("Url").HasField("_validatedUrl");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.FieldMapping;Integrated Security=True");
        }
    }
}
