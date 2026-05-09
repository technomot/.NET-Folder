using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("=== Extension Methods ===");

            string title = "The Clean Coder by Robert Martin";
            Console.WriteLine($"Text: \"{title}\"");
            Console.WriteLine($"WordCount()       : {title.WordCount()}");

            double price = 29.99;
            Console.WriteLine($"Price: {price.ToCurrencyString()}");
            Console.WriteLine();

            
            Console.WriteLine("=== LibraryStorage + foreach ===");

            LibraryStorage storage = new LibraryStorage();
            storage.Add(new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true));
            storage.Add(new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true));
            storage.Add(new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false));
            storage.Add(new Book("Refactoring", "Martin Fowler", 448, 39.99, new DateTime(1999, 7, 1), true));
            storage.Add(new Book("Code Complete", "Steve McConnell", 960, 49.99, new DateTime(2004, 6, 1), false));

            Console.WriteLine($"Total books: {storage.Count}");
            Console.WriteLine("Iterating with foreach:");
            foreach (var book in storage)
            {
                Console.WriteLine($"  - {book.Title} ({book.Author})");
            }
            Console.WriteLine();

            
            Console.WriteLine("=== Dictionary ===");

            storage.AddToDictionary(101, new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true));
            storage.AddToDictionary(102, new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true));
            storage.AddToDictionary(103, new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false));
            storage.AddToDictionary(104, new Book("Introduction to Algorithms", "Thomas Cormen", 1292, 79.99, new DateTime(2009, 7, 1), true));
            storage.AddToDictionary(105, new Book("Code Complete", "Steve McConnell", 960, 49.99, new DateTime(2004, 6, 1), false));

            Book found = storage.FindById(103);
            Console.WriteLine($"FindById(103): {found?.Title ?? "Not found"}");

            Console.WriteLine("Books with Price > 40 (LINQ on Dictionary):");
            var expensive = storage.FilterDictionary(b => b.Price > 40);
            foreach (var b in expensive)
                Console.WriteLine($"  - {b.Title} : {b.Price.ToCurrencyString()}");
            Console.WriteLine();

            Console.WriteLine("=== HashSet ===");

            HashSet<string> supplier1 = new HashSet<string> { "Programming", "Algorithms", "Design", "Networks" };
            HashSet<string> supplier2 = new HashSet<string> { "Design", "Architecture", "Programming", "Security" };

            Console.WriteLine("Supplier 1: " + string.Join(", ", supplier1));
            Console.WriteLine("Supplier 2: " + string.Join(", ", supplier2));

            HashSet<string> intersection = new HashSet<string>(supplier1);
            intersection.IntersectWith(supplier2);
            Console.WriteLine("Common categories (Intersection): " + string.Join(", ", intersection));

            HashSet<string> union = new HashSet<string>(supplier1);
            union.UnionWith(supplier2);
            Console.WriteLine("All categories (Union): " + string.Join(", ", union));
            Console.WriteLine();

            Console.WriteLine("============================================");
        }
    }
}