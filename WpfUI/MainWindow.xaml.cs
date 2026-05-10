using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Core;
using Microsoft.Win32;

namespace WpfUI
{
    public partial class MainWindow : Window
    {
        private List<Book> _allBooks = new List<Book>();
        private bool _isLoaded = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadSampleData();
            _isLoaded = true;
            ApplyFilter();
        }

        private void LoadSampleData()
        {
            _allBooks.Add(new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true));
            _allBooks.Add(new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true));
            _allBooks.Add(new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false));
            _allBooks.Add(new Book("Refactoring", "Martin Fowler", 448, 39.99, new DateTime(1999, 7, 1), true));
            _allBooks.Add(new Book("Code Complete", "Steve McConnell", 960, 49.99, new DateTime(2004, 6, 1), false));
        }

        private void ApplyFilter()
        {
            if (!_isLoaded) return;

            string search = TxtSearch?.Text?.ToLower() ?? "";

            IEnumerable<Book> filtered = _allBooks;

            if (RbAvailable?.IsChecked == true)
                filtered = filtered.Where(b => b.IsAvailable);
            else if (RbUnavailable?.IsChecked == true)
                filtered = filtered.Where(b => !b.IsAvailable);

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(b => b.Title.ToLower().Contains(search));

            var result = filtered.ToList();
            BooksGrid.ItemsSource = result;
            TxtCount.Text = $"Showing: {result.Count} books";
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilter();
        private void Filter_Changed(object sender, RoutedEventArgs e) => ApplyFilter();

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BookDialog();
            if (dialog.ShowDialog() == true)
            {
                _allBooks.Add(dialog.Result);
                ApplyFilter();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is not Book selected)
            {
                MessageBox.Show("Please select a book to edit.", "No selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new BookDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                int index = _allBooks.IndexOf(selected);
                _allBooks[index] = dialog.Result;
                ApplyFilter();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is not Book selected)
            {
                MessageBox.Show("Please select a book to delete.", "No selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete \"{selected.Title}\"?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                _allBooks.Remove(selected);
                ApplyFilter();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "JSON files|*.json", FileName = "books.json" };
            if (dlg.ShowDialog() == true)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(dlg.FileName, JsonSerializer.Serialize(_allBooks, options));
                MessageBox.Show("Saved successfully.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "JSON files|*.json" };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    _allBooks = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(dlg.FileName));
                    ApplyFilter();
                    MessageBox.Show($"Loaded {_allBooks.Count} books.", "Loaded", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}