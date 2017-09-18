using GlobalQueryFilters.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalQueryFilters
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();

            using (var context = new BooksDbContext())
            {
                var authors = context.Authors
                    .Include(a => a.Books)
                    .ToList();

                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Name,-20}" + $"[IsDeleted: ");
                    Console.ForegroundColor = author.IsDeleted ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.Write(author.IsDeleted);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("]");

                    foreach (var book in author.Books)
                        Console.WriteLine($"\t{book.Name}");

                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        private static void SetupDatabase()
        {
            using (var context = new BooksDbContext())
            {
                if(context.Database.EnsureCreated())
                {
                    context.Authors.Add(new Author
                    {
                        Name = "Christoph Scholder",
                        Books = new List<Book>
                        {
                            new Book { Name = "Oktoberfest", ISBN = "3426198886" }
                        }
                    });

                    context.Authors.Add(new Author
                    {
                        Name = "Jonathan Stroud",
                        Books = new List<Book>
                        {
                            new Book { Name = "Bartimäus - Das Amulett von Samarkand", ISBN = "3570127753" },
                            new Book { Name = "Bartimäus - Das Auge des Golem", ISBN = "3570127761" },
                            new Book { Name = "Bartimäus - Die Pforte des Magiers", ISBN = "3442368014" },
                            new Book { Name = "Bartimäus - Der Ring des Salomo", ISBN = "3570139670" }
                        }
                    });

                    context.Authors.Add(new Author
                    {
                        Name = "Robert Cecil Martin",
                        Books = new List<Book>
                        {
                            new Book { Name = "Clean Code", ISBN = "0132350882" },
                            new Book { Name = "The Clean Coder", ISBN = "0137081073" }
                        }
                    });

                    context.SaveChanges();
                }
            }

            using (var context = new BooksDbContext())
            {
                context.Authors
                    .Where(a => a.Name == "Christoph Scholder")
                    .ToList()
                    .ForEach(a => context.Remove(a));

                context.SaveChanges();
            }
        }
    }

    internal class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.GlobalQueryFilters;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<Author>().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChanges();
        }
    }
}
