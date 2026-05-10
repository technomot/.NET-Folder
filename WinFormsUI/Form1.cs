using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Core;

namespace WinFormsUI
{
    public partial class Form1 : Form
    {
        private List<Book> _books = new List<Book>();
        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnSave;
        private Button btnLoad;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
            LoadSampleData();
        }

        private void SetupUI()
        {
            this.Text = "Library Management System";
            this.Size = new System.Drawing.Size(800, 500);

            
            Panel toolbar = new Panel();
            toolbar.Dock = DockStyle.Top;
            toolbar.Height = 45;
            toolbar.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);

            btnAdd = new Button();
            btnAdd.Text = "Add";
            btnAdd.Location = new System.Drawing.Point(10, 8);
            btnAdd.Size = new System.Drawing.Size(80, 28);
            btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Click += BtnAdd_Click;

            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Location = new System.Drawing.Point(100, 8);
            btnDelete.Size = new System.Drawing.Size(80, 28);
            btnDelete.BackColor = System.Drawing.Color.FromArgb(204, 50, 50);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Click += BtnDelete_Click;

            btnSave = new Button();
            btnSave.Text = "Save JSON";
            btnSave.Location = new System.Drawing.Point(200, 8);
            btnSave.Size = new System.Drawing.Size(90, 28);
            btnSave.BackColor = System.Drawing.Color.FromArgb(50, 150, 50);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            btnLoad = new Button();
            btnLoad.Text = "Load JSON";
            btnLoad.Location = new System.Drawing.Point(300, 8);
            btnLoad.Size = new System.Drawing.Size(90, 28);
            btnLoad.BackColor = System.Drawing.Color.FromArgb(50, 150, 50);
            btnLoad.ForeColor = System.Drawing.Color.White;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Click += BtnLoad_Click;

            toolbar.Controls.Add(btnAdd);
            toolbar.Controls.Add(btnDelete);
            toolbar.Controls.Add(btnSave);
            toolbar.Controls.Add(btnLoad);

            
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Title" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Author", HeaderText = "Author" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Pages", HeaderText = "Pages" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Price" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PublishedDate", HeaderText = "Published" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IsAvailable", HeaderText = "Available" });

            this.Controls.Add(dataGridView);
            this.Controls.Add(toolbar);
        }

        private void LoadSampleData()
        {
            _books.Add(new Book("Clean Code", "Robert C. Martin", 431, 29.99, new DateTime(2008, 8, 1), true));
            _books.Add(new Book("The Pragmatic Programmer", "David Thomas", 352, 34.99, new DateTime(1999, 10, 1), true));
            _books.Add(new Book("Design Patterns", "Gang of Four", 395, 44.99, new DateTime(1994, 11, 1), false));
            _books.Add(new Book("Refactoring", "Martin Fowler", 448, 39.99, new DateTime(1999, 7, 1), true));
            _books.Add(new Book("Code Complete", "Steve McConnell", 960, 49.99, new DateTime(2004, 6, 1), false));
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _books;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (AddBookForm form = new AddBookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _books.Add(form.NewBook);
                    RefreshGrid();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this book?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                int index = dataGridView.SelectedRows[0].Index;
                _books.RemoveAt(index);
                RefreshGrid();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "JSON files (*.json)|*.json";
                dlg.FileName = "books.json";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(_books, options);
                    File.WriteAllText(dlg.FileName, json);
                    MessageBox.Show("Books saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "JSON files (*.json)|*.json";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string json = File.ReadAllText(dlg.FileName);
                        List<Book> loaded = JsonSerializer.Deserialize<List<Book>>(json);
                        _books = loaded;
                        RefreshGrid();
                        MessageBox.Show($"Loaded {_books.Count} books.", "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}