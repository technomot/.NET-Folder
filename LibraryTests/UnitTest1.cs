using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Xunit;

namespace LibraryTests
{
    public class BookTests
    {
        
        [Fact]
        public void Book_CreatedWithCorrectProperties()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true);

            Assert.Equal("Clean Code", book.Title);
            Assert.Equal("Robert C. Martin", book.Author);
            Assert.Equal(431, book.Pages);
            Assert.Equal(29.99, book.Price);
            Assert.True(book.IsAvailable);
        }

        
        [Fact]
        public void Book_ContainsKeyword_ReturnsTrue_ForMatchingTitle()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true);

            Assert.True(book.ContainsKeyword("Clean"));
        }

        
        [Fact]
        public void Book_ContainsKeyword_ReturnsFalse_ForNonMatchingKeyword()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true);

            Assert.False(book.ContainsKeyword("Java"));
        }

        
        [Fact]
        public void Linq_Filter_ReturnsOnlyAvailableBooks()
        {
            var books = new List<Book>
            {
                new Book("Clean Code",      "Robert C. Martin", 431, 29.99, new DateTime(2008, 8,  1), true),
                new Book("Design Patterns", "Gang of Four",     395, 44.99, new DateTime(1994, 11, 1), false),
                new Book("Refactoring",     "Martin Fowler",    448, 39.99, new DateTime(1999, 7,  1), true),
            };

            var available = books.Where(b => b.IsAvailable).ToList();

            Assert.Equal(2, available.Count);
            Assert.All(available, b => Assert.True(b.IsAvailable));
        }

        
        [Fact]
        public void Linq_Filter_ReturnsBooksWithPriceOver40()
        {
            var books = new List<Book>
            {
                new Book("Clean Code",      "Robert C. Martin", 431,  29.99, new DateTime(2008, 8,  1), true),
                new Book("Design Patterns", "Gang of Four",     395,  44.99, new DateTime(1994, 11, 1), false),
                new Book("Code Complete",   "Steve McConnell",  960,  49.99, new DateTime(2004, 6,  1), false),
            };

            var expensive = books.Where(b => b.Price > 40).ToList();

            Assert.Equal(2, expensive.Count);
            Assert.All(expensive, b => Assert.True(b.Price > 40));
        }
    }
}