using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using Core;

namespace WpfMVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set { _books = value; OnPropertyChanged(); }
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set { _selectedBook = value; OnPropertyChanged(); }
        }

        
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); ApplyFilter(); }
        }

        private bool _showAll = true;
        public bool ShowAll
        {
            get => _showAll;
            set { _showAll = value; OnPropertyChanged(); ApplyFilter(); }
        }

        private bool _showAvailable;
        public bool ShowAvailable
        {
            get => _showAvailable;
            set { _showAvailable = value; OnPropertyChanged(); ApplyFilter(); }
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        
        private System.Collections.Generic.List<Book> _allBooks = new();

        
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();

            AddCommand = new RelayCommand(_ => AddBook());
            DeleteCommand = new RelayCommand(_ => DeleteBook(), _ => SelectedBook != null);
            SaveCommand = new RelayCommand(_ => SaveToJson());
            LoadCommand = new RelayCommand(_ => LoadFromJson());

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _allBooks.Add(new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true));
            _allBooks.Add(new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true));
            _allBooks.Add(new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false));
            _allBooks.Add(new Book("Refactoring", "Martin Fowler", 448, 39.99, new DateTime(1999, 7, 1), true));
            _allBooks.Add(new Book("Code Complete", "Steve McConnell", 960, 49.99, new DateTime(2004, 6, 1), false));
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = _allBooks.AsEnumerable();

            if (ShowAvailable)
                filtered = filtered.Where(b => b.IsAvailable);

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(b => b.Title.ToLower().Contains(SearchText.ToLower()));

            Books = new ObservableCollection<Book>(filtered);
            StatusText = $"Showing: {Books.Count} books";
        }

        private void AddBook()
        {
            var dialog = new BookDialog();
            if (dialog.ShowDialog() == true)
            {
                _allBooks.Add(dialog.Result);
                ApplyFilter();
            }
        }

        private void DeleteBook()
        {
            if (SelectedBook == null) return;
            _allBooks.Remove(SelectedBook);
            ApplyFilter();
        }

        private void SaveToJson()
        {
            var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "JSON|*.json", FileName = "books.json" };
            if (dlg.ShowDialog() == true)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(dlg.FileName, JsonSerializer.Serialize(_allBooks, options));
                StatusText = "Saved successfully.";
            }
        }

        private void LoadFromJson()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { Filter = "JSON|*.json" };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    _allBooks = JsonSerializer.Deserialize<System.Collections.Generic.List<Book>>(File.ReadAllText(dlg.FileName));
                    ApplyFilter();
                    StatusText = $"Loaded {_allBooks.Count} books.";
                }
                catch (Exception ex)
                {
                    StatusText = $"Error: {ex.Message}";
                }
            }
        }
    }
}