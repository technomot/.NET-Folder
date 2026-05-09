using System;

namespace Core
{
    public class Book : LibraryItem, ISearchable
    {
        public int Pages { get; set; }
        public double Price { get; set; }

        public Book(string title, string author, int pages, double price, DateTime publishedDate, bool isAvailable)
            : base(title, author, publishedDate, isAvailable)
        {
            Pages = pages;
            Price = price;
        }

        public override string GetItemType()
        {
            return "Book";
        }

        public override string GetInfo()
        {
            return $"[Book] {Title} by {Author} | Pages: {Pages} | Price: {Price:F2} | Available: {IsAvailable}";
        }

        public bool ContainsKeyword(string keyword)
        {
            return Title.Contains(keyword) || Author.Contains(keyword);
        }

        public string GetSummary()
        {
            return $"{Title} by {Author} — {Pages} pages, {Price:F2} USD";
        }
    }
}