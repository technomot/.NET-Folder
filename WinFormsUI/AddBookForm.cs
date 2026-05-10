using System;
using System.Windows.Forms;
using Core;

namespace WinFormsUI
{
    public class AddBookForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private NumericUpDown numPages;
        private NumericUpDown numPrice;
        private DateTimePicker dtpPublished;
        private CheckBox chkAvailable;
        private Button btnOK;
        private Button btnCancel;

        public Book NewBook { get; private set; }

        public AddBookForm()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Add New Book";
            this.Size = new System.Drawing.Size(350, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;

            int labelX = 20;
            int inputX = 120;
            int y = 20;
            int gap = 40;

           
            this.Controls.Add(new Label { Text = "Title:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            txtTitle = new TextBox { Location = new System.Drawing.Point(inputX, y), Width = 180 };
            this.Controls.Add(txtTitle);
            y += gap;

            
            this.Controls.Add(new Label { Text = "Author:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            txtAuthor = new TextBox { Location = new System.Drawing.Point(inputX, y), Width = 180 };
            this.Controls.Add(txtAuthor);
            y += gap;

            
            this.Controls.Add(new Label { Text = "Pages:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            numPages = new NumericUpDown { Location = new System.Drawing.Point(inputX, y), Width = 100, Minimum = 1, Maximum = 9999 };
            this.Controls.Add(numPages);
            y += gap;

            
            this.Controls.Add(new Label { Text = "Price:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            numPrice = new NumericUpDown { Location = new System.Drawing.Point(inputX, y), Width = 100, Minimum = 0, Maximum = 9999, DecimalPlaces = 2 };
            this.Controls.Add(numPrice);
            y += gap;

            this.Controls.Add(new Label { Text = "Published:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            dtpPublished = new DateTimePicker { Location = new System.Drawing.Point(inputX, y), Width = 180, Format = DateTimePickerFormat.Short };
            this.Controls.Add(dtpPublished);
            y += gap;

            
            this.Controls.Add(new Label { Text = "Available:", Location = new System.Drawing.Point(labelX, y), AutoSize = true });
            chkAvailable = new CheckBox { Location = new System.Drawing.Point(inputX, y), Checked = true };
            this.Controls.Add(chkAvailable);
            y += gap;

            
            btnOK = new Button
            {
                Text = "Add",
                Location = new System.Drawing.Point(inputX, y),
                Width = 80,
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnOK.Click += BtnOK_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(inputX + 90, y),
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Title and Author cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            NewBook = new Book(
                txtTitle.Text,
                txtAuthor.Text,
                (int)numPages.Value,
                (double)numPrice.Value,
                dtpPublished.Value,
                chkAvailable.Checked
            );
        }
    }
}