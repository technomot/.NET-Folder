using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using Core;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book("Clean Code",               "Robert C. Martin", 431,  29.99, new DateTime(2008, 8,  1), true),
                new Book("The Pragmatic Programmer", "David Thomas",     352,  34.99, new DateTime(1999, 10, 1), true),
                new Book("Design Patterns",          "Gang of Four",     395,  44.99, new DateTime(1994, 11, 1), false),
                new Book("Refactoring",              "Martin Fowler",    448,  39.99, new DateTime(1999, 7,  1), true),
                new Book("Code Complete",            "Steve McConnell",  960,  49.99, new DateTime(2004, 6,  1), false),
            };

            
            Console.WriteLine("TASK 2: JSON Serialization");

            string jsonPath = "books.json";
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(books, options);
            File.WriteAllText(jsonPath, jsonString);
            Console.WriteLine($"Saved {books.Count} books to {jsonPath}");

            string jsonLoaded = File.ReadAllText(jsonPath);
            List<Book> loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonLoaded);
            Console.WriteLine($"Loaded {loadedBooks.Count} books from {jsonPath}");
            foreach (var b in loadedBooks)
                Console.WriteLine("  " + b.GetInfo());
            Console.WriteLine();

            
            Console.WriteLine("TASK 3: XML Export");

            string xmlPath = "books.xml";
            XDocument xmlDoc = new XDocument(
                new XElement("Library",
                    from b in books
                    where b.IsAvailable == true
                    select new XElement("Book",
                        new XElement("Title", b.Title),
                        new XElement("Author", b.Author),
                        new XElement("Pages", b.Pages),
                        new XElement("Price", b.Price),
                        new XElement("PublishedDate", b.PublishedDate.ToString("yyyy-MM-dd")),
                        new XElement("IsAvailable", b.IsAvailable)
                    )
                )
            );

            xmlDoc.Save(xmlPath);
            Console.WriteLine($"Exported available books to {xmlPath}");

            XDocument loaded = XDocument.Load(xmlPath);
            var xmlBooks = loaded.Descendants("Book");
            Console.WriteLine("Books from XML:");
            foreach (var xb in xmlBooks)
                Console.WriteLine($"  {xb.Element("Title").Value} by {xb.Element("Author").Value}");
            Console.WriteLine();

            
            Console.WriteLine("TASK 4: IDisposable");

            ResourceManager manager = new ResourceManager("library_log.txt");
            manager.Log("Application started");
            manager.Log("Books loaded successfully");
            manager.Dispose();
            Console.WriteLine();

            
            Console.WriteLine("TASK 5: using block");

            using (ResourceManager rm = new ResourceManager("library_log2.txt"))
            {
                rm.Log("Saving books to JSON");
                rm.Log("Exporting books to XML");
                rm.Log("All operations completed");
            }
            Console.WriteLine();

            Console.WriteLine("TASK 6: Validation and Error Handling");

            string missingFile = "missing.json";
            if (!File.Exists(missingFile))
            {
                Console.WriteLine($"File '{missingFile}' does not exist.");
            }

            string corruptedJson = "corrupted.json";
            File.WriteAllText(corruptedJson, "{ this is not valid json }}}");

            try
            {
                string content = File.ReadAllText(corruptedJson);
                List<Book> result = JsonSerializer.Deserialize<List<Book>>(content);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Error handling completed.");
            }

            Console.WriteLine();
            Console.WriteLine("============================================");
        }
    }
}