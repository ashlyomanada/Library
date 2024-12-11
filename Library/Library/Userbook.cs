using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace Library
{
    public partial class userbook : UserControl
    {
        private DataTable bookData; // Store the books data for filtering
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\renzj\source\repos\Library\Library\library.mdf;Integrated Security=True;Connect Timeout=30");

        public userbook()
        {
            InitializeComponent();
            LoadBooks();
        }
        public void LoadBooks()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = @"
    SELECT 
        books.Id,
        books.Title,
        books.Author,
        books.Genre,
        books.Status,
        books.image,
        CONCAT(locations.Section, ' - ', locations.Shelf, ' - Floor ', locations.Floor) AS Location,
        books.date_Insert
    FROM 
        books
    LEFT JOIN 
        locations
    ON 
        books.LocationId = locations.Id
    WHERE 
        books.Status = 'Available'
    ORDER BY 
        books.date_Insert DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                bookData = new DataTable(); // Initialize the DataTable
                adapter.Fill(bookData);

                dataGridViewBooks.DataSource = bookData;

                if (dataGridViewBooks.Columns["image"] != null)
                {
                    dataGridViewBooks.Columns["image"].Visible = false;
                }
                if (dataGridViewBooks.Columns["date_Insert"] != null)
                {
                    dataGridViewBooks.Columns["date_Insert"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void dataGridViewBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewBooks_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewBooks.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridViewBooks.Rows[e.RowIndex];

                // Display the data in the textboxes
                bookTxt.Text = selectedRow.Cells["Title"].Value?.ToString();
                authorTxt.Text = selectedRow.Cells["Author"].Value?.ToString();
                genreTxt.Text = selectedRow.Cells["Genre"].Value?.ToString();
                statusTxt.Text = selectedRow.Cells["Status"].Value?.ToString();
                locationTxt.Text = selectedRow.Cells["Location"].Value?.ToString();

                // Load the image if available
                string imagePath = selectedRow.Cells["image"].Value?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    addbook_picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    addbook_picture.Image = null;
                }
            }
        }

        private void requestBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected book's ID
            int selectedBookId = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["Id"].Value);

            // Assuming you have a way to get the current user's ID
            int currentUserId = Session.UserId; // Replace this with your logic to retrieve the logged-in user's ID.

            string query = "INSERT INTO requests (UserId, BookId) VALUES (@UserId, @BookId)";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", currentUserId);
                    cmd.Parameters.AddWithValue("@BookId", selectedBookId);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Request submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (bookData == null || bookData.Rows.Count == 0) return;

            string searchQuery = SearchTextBox.Text.Trim().ToLower();

            var filteredRows = bookData.AsEnumerable()
                .Where(row =>
                    row.Field<string>("Title").ToLower().Contains(searchQuery) ||
                    row.Field<string>("Author").ToLower().Contains(searchQuery) ||
                    row.Field<string>("Genre").ToLower().Contains(searchQuery));

            // Update DataGridView with filtered rows or clear it if no results
            if (filteredRows.Any())
            {
                dataGridViewBooks.DataSource = filteredRows.CopyToDataTable();
            }
            else
            {
                dataGridViewBooks.DataSource = null; // Clear DataGridView when no match
            }
        }
    }
}
