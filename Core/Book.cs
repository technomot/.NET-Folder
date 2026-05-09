using System;

namespace Core
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public double Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, int pages, double price, DateTime publishedDate, bool isAvailable)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Price = price;
            PublishedDate = publishedDate;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"[Book]\n" +
                   $"  Title       : {Title}\n" +
                   $"  Author      : {Author}\n" +
                   $"  Pages       : {Pages}\n" +
                   $"  Price       : {Price:F2} USD\n" +
                   $"  Published   : {PublishedDate:dd.MM.yyyy}\n" +
                   $"  Available   : {IsAvailable}";
        }
    }
}