using FromSql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FromSql
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();

            using (var context = new BooksDbContext())
            {
                var count = 2;

                var authors = context.Authors
                    .FromSql($@"
SELECT a.Id, a.Name
FROM dbo.Authors a 
INNER JOIN dbo.Books b ON a.Id = b.AuthorId
GROUP BY a.Id, a.Name
HAVING COUNT(*) > {count}")
                    .ToList();

                Console.WriteLine();
                foreach (var a in authors)
                    Console.WriteLine($"{a.Id} - {a.Name}");
            }

            Console.ReadKey();
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.FromSql;Integrated Security=True");

            var lf = new LoggerFactory();
            lf.AddProvider(new LoggerProvider());
            optionsBuilder.UseLoggerFactory(lf);
        }
    }
}
