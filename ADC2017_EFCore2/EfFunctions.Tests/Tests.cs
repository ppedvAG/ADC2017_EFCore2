using EfFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EfFunctions
{
    public class Tests
    {
        [Fact]
        public void SearchBooks_2_of_5()
        {
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchBooks_2_of_5")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                context.Authors.Add(new Author
                {
                    Name = "SomeName",
                    Books = new List<Book>
                    {
                        new Book { Name = "Abcd"},
                        new Book { Name = "Bcde"},
                        new Book { Name = "Cdef"},
                        new Book { Name = "Defg"},
                        new Book { Name = "Efgh"},
                    }
                });
                context.SaveChanges();
            }

            using (var context = new BooksDbContext(options))
            {
                var service = new BookService(context);
                var blogs = service.SearchBooks("bcd");
                Assert.Equal(2, blogs.Count());
            }
        }
    }
}
