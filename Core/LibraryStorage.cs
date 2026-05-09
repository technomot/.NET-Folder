using System;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class LibraryStorage : IEnumerable<Book>
    {
        private List<Book> _books = new List<Book>();

        public void Add(Book book)
        {
            _books.Add(book);
        }

        public int Count => _books.Count;

        
        public IEnumerator<Book> GetEnumerator()
        {
            return _books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        private Dictionary<int, Book> _bookDict = new Dictionary<int, Book>();

        public void AddToDictionary(int id, Book book)
        {
            _bookDict[id] = book;
        }

        public Book FindById(int id)
        {
            _bookDict.TryGetValue(id, out Book book);
            return book;
        }

        public IEnumerable<Book> FilterDictionary(Func<Book, bool> condition)
        {
            var result = new List<Book>();
            foreach (var kvp in _bookDict)
            {
                if (condition(kvp.Value))
                    result.Add(kvp.Value);
            }
            return result;
        }

        
        public HashSet<string> GetCategories()
        {
            return new HashSet<string> { "Programming", "Algorithms", "Design", "Architecture" };
        }
    }
}