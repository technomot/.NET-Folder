using System;
using System.Windows;
using Core;

namespace WpfMVVM
{
    public partial class BookDialog : Window
    {
        public Book Result { get; private set; }

        public BookDialog()
        {
            InitializeComponent();
            DtpPublished.SelectedDate = DateTime.Today;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text) || string.IsNullOrWhiteSpace(TxtAuthor.Text))
            {
                MessageBox.Show("Title and Author are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtPages.Text, out int pages))
            {
                MessageBox.Show("Pages must be a number.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(TxtPrice.Text, out double price))
            {
                MessageBox.Show("Price must be a number.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Result = new Book(TxtTitle.Text, TxtAuthor.Text, pages, price,
                DtpPublished.SelectedDate ?? DateTime.Today, ChkAvailable.IsChecked == true);
            DialogResult = true;
        }
    }
}