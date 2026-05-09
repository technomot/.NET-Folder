using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class LibraryStorage : IEnumerable<LibraryItem>
    {
        private List<LibraryItem> _items = new List<LibraryItem>();

        public void Add(LibraryItem item)
        {
            _items.Add(item);
        }

        public int Count => _items.Count;

        public IEnumerator<LibraryItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}