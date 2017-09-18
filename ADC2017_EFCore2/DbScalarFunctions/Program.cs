using DbScalarFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbScalarFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupDatabase();

            using (var context = new BooksDbContext())
            {
                var authors = context.Authors
                    .Where(a => BooksDbContext.CountBooksFromAuthor(a.Id) > 1)
                    .ToList();

                authors.ForEach(a => Console.WriteLine(a.Name));
            }

            Console.ReadKey();
        }

        private static void SetupDatabase()
        {
            using (var context = new BooksDbContext())
            {
                if (context.Database.EnsureCreated())
                {
                    context.Database.ExecuteSqlCommand(@"
CREATE FUNCTION dbo.CountBooksFromAuthor 
(
	@authorId as int 
)
RETURNS int AS BEGIN 
	DECLARE @result as int
	SELECT @result = Count(*) from Books WHERE AuthorId = @authorId
	RETURN @result
END");
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
            optionsBuilder.UseSqlServer("Server=localhost;Database=Demo.DbScalarFunctions;Integrated Security=True");
        }

        [DbFunction(FunctionName = "CountBooksFromAuthor", Schema = "dbo")]
        public static int CountBooksFromAuthor(int authorId) => throw new Exception();
    }
}
