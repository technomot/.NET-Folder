using System;
using System.Collections.Generic;
using Core;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" TASK 2: Abstract Class ");

            Book book1 = new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true);
            Magazine mag1 = new Magazine("Tech Monthly", "John Smith", 42, "Technology", new DateTime(2023, 1, 1), true);

            Console.WriteLine(book1.GetItemType() + ": " + book1.GetInfo());
            Console.WriteLine(mag1.GetItemType() + ": " + mag1.GetInfo());
            Console.WriteLine();

            Console.WriteLine("TASK 3: Interface ISearchable");

            ISearchable searchable = book1;
            Console.WriteLine("ContainsKeyword('Clean'): " + searchable.ContainsKeyword("Clean"));
            Console.WriteLine("ContainsKeyword('Java'):  " + searchable.ContainsKeyword("Java"));
            Console.WriteLine("Summary: " + searchable.GetSummary());
            Console.WriteLine();

            Console.WriteLine("TASK 4: Composition");

            LibraryController controller = new LibraryController("Central Library", 5, true);
            controller.ShowConfig();
            Console.WriteLine();

            Console.WriteLine("TASK 5: Aggregation");

            Book book2 = new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true);
            Book book3 = new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false);
            Magazine mag2 = new Magazine("Science Weekly", "Jane Doe", 10, "Science", new DateTime(2023, 5, 1), true);

            LibraryStorage storage = new LibraryStorage();
            storage.Add(book1);
            storage.Add(book2);
            storage.Add(book3);
            storage.Add(mag1);
            storage.Add(mag2);

            Console.WriteLine($"Total items in storage: {storage.Count}");
            foreach (var item in storage)
            {
                Console.WriteLine("  " + item.GetInfo());
            }
            Console.WriteLine();
            Console.WriteLine("TASK 6: Polymorphism");

            ISearchable[] searchables = new ISearchable[] { book1, book2, book3, mag1, mag2 };
            foreach (var item in searchables)
            {
                Console.WriteLine(item.GetSummary());
            }
            Console.WriteLine();

            Console.WriteLine("============================================");
        }
    }
}