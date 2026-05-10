using System;

namespace Core
{
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAvailable { get; set; }

        public LibraryItem() { }

        public LibraryItem(string title, string author, DateTime publishedDate, bool isAvailable)
        {
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            IsAvailable = isAvailable;
        }

        public virtual string GetInfo()
        {
            return $"Title: {Title}, Author: {Author}, Available: {IsAvailable}";
        }

        public abstract string GetItemType();

        public override string ToString()
        {
            return GetInfo();
        }
    }
}