using EfFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EfFunctions.Tests")]

namespace EfFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();

            using (var context = new BooksDbContext())
            {
                var service = new BookService(context);

                var books = service.SearchBooks("bartimäus");

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Name} - {book.ISBN}");
                }
            }
        }

        private static void SetupDatabase()
        {
            using (var context = new BooksDbContext())
            {
                if (context.Database.EnsureCreated())
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
        }
    }

    internal class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BooksDbContext()
        { }
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.EfFunctions;Integrated Security=True");
        }
    }
}
