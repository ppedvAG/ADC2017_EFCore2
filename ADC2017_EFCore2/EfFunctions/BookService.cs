﻿using EfFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EfFunctions
{
    internal class BookService
    {
        private readonly BooksDbContext _context;

        public BookService(BooksDbContext context) => _context = context;

        public IEnumerable<Book> SearchBooks(string name)
        {
            var likeExpression = $"%{name}%";
            
            return _context.Books 
                .FromSql($"SELECT * FROM dbo.Books WHERE Name LIKE {likeExpression}");
        }
    }
}