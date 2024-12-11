using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    public partial class addBooks : UserControl
    {
        private DataTable bookData;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\renzj\source\repos\Library\Library\library.mdf;Integrated Security=True;Connect Timeout=30");

        public addBooks()
        {
            InitializeComponent();
            LoadLocationsIntoComboBox();
            LoadBooks();
        }



        private void importBtn_Click(object sender, EventArgs e)
        {
            string imagePath = "";

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png )|*.jpg; *.png ";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addbook_picture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void addBooks_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLocation.SelectedItem != null)
            {
                LocationItem selectedLocation = (LocationItem)comboBoxLocation.SelectedItem;

                int locationId = selectedLocation.Value;
                string locationDescription = selectedLocation.Text;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (addbook_picture.Image == null || string.IsNullOrEmpty(bookTxt.Text) || string.IsNullOrEmpty(genreTxt.Text))
            {
                MessageBox.Show("Please fill all of the fields to add a book.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxLocation.SelectedItem == null)
            {
                MessageBox.Show("Please select a location for the book.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Ensure the database connection is open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                LocationItem selectedLocation = (LocationItem)comboBoxLocation.SelectedItem;
                int locationId = selectedLocation.Value;

                string insert = "INSERT INTO books (Title, Author, Genre, Status, LocationId, date_Insert, image) " +
                                "VALUES (@Title, @Author, @Genre, @Status, @LocationId, @Date_Insert, @image)";

                string directoryPath = Path.Combine(@"C:\Users\renzj\source\repos\Library\Library\BookDirectory\",
                                                     bookTxt.Text.Trim() + "_" + authorTxt.Text.Trim() + "_" + DateTime.Today.ToString("yyyyMMdd"));

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Define the full path for the image
                string imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));

                // Copy the image to the directory
                File.Copy(addbook_picture.ImageLocation, imagePath, true);

                using (SqlCommand insertCMD = new SqlCommand(insert, con))
                {
                    insertCMD.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Status", "Available");
                    insertCMD.Parameters.AddWithValue("@LocationId", locationId);
                    insertCMD.Parameters.AddWithValue("@Date_Insert", DateTime.Today);
                    insertCMD.Parameters.AddWithValue("@image", imagePath);

                    insertCMD.ExecuteNonQuery();

                    MessageBox.Show("Book added successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the fields after success
                    bookTxt.Clear();
                    authorTxt.Clear();
                    genreTxt.Clear();
                    addbook_picture.Image = null;
                    comboBoxLocation.SelectedIndex = -1; // Reset the ComboBox
                }
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the database connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }










        public void LoadLocationsIntoComboBox()
        {
            try
            {

                // Clear existing items in the ComboBox
                comboBoxLocation.Items.Clear();

                // Corrected query with explicit CAST
                string query = @"
            SELECT 
                Id, 
                CAST(Section AS NVARCHAR(50)) + ' - ' + 
                CAST(Shelf AS NVARCHAR(50)) + ' - ' + 
                CAST(Floor AS NVARCHAR(50)) AS LocationDescription 
            FROM locations";

                // Open the database connection
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create a new LocationItem and add it to the ComboBox
                        LocationItem locationItem = new LocationItem
                        {
                            Value = Convert.ToInt32(reader["Id"]),
                            Text = reader["LocationDescription"].ToString()
                        };
                        comboBoxLocation.Items.Add(locationItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading locations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
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
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewBooks.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridViewBooks.Rows[e.RowIndex];

                // Set the BookId
                BookId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Display the data in the textboxes
                bookTxt.Text = selectedRow.Cells["Title"].Value?.ToString();
                authorTxt.Text = selectedRow.Cells["Author"].Value?.ToString();
                genreTxt.Text = selectedRow.Cells["Genre"].Value?.ToString();

                // Find the corresponding LocationItem in the ComboBox based on the location text
                if (selectedRow.Cells["Location"].Value != null)
                {
                    string locationDescription = selectedRow.Cells["Location"].Value.ToString();

                    // Match the location description with the ComboBox items
                    foreach (LocationItem item in comboBoxLocation.Items)
                    {
                        if (item.Text.Equals(locationDescription, StringComparison.OrdinalIgnoreCase))
                        {
                            comboBoxLocation.SelectedItem = item;
                            return; // Stop searching once found
                        }
                    }

                    // If no match is found, reset the ComboBox selection
                    comboBoxLocation.SelectedIndex = -1;
                }
                else
                {
                    comboBoxLocation.SelectedIndex = -1; // Reset if no location
                }

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



        private int BookId = 0;
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (addbook_picture.Image == null || string.IsNullOrEmpty(bookTxt.Text) ||
                string.IsNullOrEmpty(authorTxt.Text) || string.IsNullOrEmpty(genreTxt.Text) ||
                comboBoxLocation.SelectedItem == null)
            {
                MessageBox.Show("Please fill all of the fields to update.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show("Are you sure you want to UPDATE?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    LocationItem selectedLocation = comboBoxLocation.SelectedItem as LocationItem;
                    if (selectedLocation == null)
                    {
                        MessageBox.Show("Please select a valid location.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int locationId = selectedLocation.Value;

                    // Check if the directory exists
                    string directoryPath = Path.Combine(@"C:\Users\renzj\source\repos\Library\Library\BookDirectory\",
                                                        bookTxt.Text.Trim() + "_" + authorTxt.Text.Trim() + "_" + DateTime.Today.ToString("yyyyMMdd"));
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Define the image path
                    string imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));

                    // Copy the image to the directory
                    File.Copy(addbook_picture.ImageLocation, imagePath, true);

                    // Update query with the image
                    string updateData = "UPDATE books SET Title=@Title, Author=@Author, Genre=@Genre, LocationId=@LocationId, image=@image WHERE Id=@Id";
                    using (SqlCommand updateCmd = new SqlCommand(updateData, con))
                    {
                        updateCmd.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
                        updateCmd.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
                        updateCmd.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
                        updateCmd.Parameters.AddWithValue("@LocationId", locationId);
                        updateCmd.Parameters.AddWithValue("@image", imagePath);
                        updateCmd.Parameters.AddWithValue("@Id", BookId);

                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh DataGridView
                    LoadBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating book: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Update cancelled", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (BookId == 0)
            {
                MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmDelete = MessageBox.Show("Are you sure you want to DELETE this book?",
                                                         "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmDelete == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Retrieve the image path of the book to delete
                    string imagePath = string.Empty;
                    string getImagePathQuery = "SELECT image FROM books WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(getImagePathQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", BookId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            imagePath = reader["image"].ToString();
                        }
                        reader.Close();
                    }

                    // Clear the PictureBox image to release the file lock
                    if (addbook_picture.Image != null)
                    {
                        addbook_picture.Image.Dispose();
                        addbook_picture.Image = null;
                    }

                    // Delete the book record from the database
                    string deleteQuery = "DELETE FROM books WHERE Id=@Id";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                    {
                        deleteCmd.Parameters.AddWithValue("@Id", BookId);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Delete the associated image file if it exists
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView
                    LoadBooks();

                    // Reset the form fields
                    bookTxt.Clear();
                    authorTxt.Clear();
                    genreTxt.Clear();
                    addbook_picture.Image = null;
                    comboBoxLocation.SelectedIndex = -1;
                    BookId = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Delete operation cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            MessageBox.Show("Form cleared successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFormFields()
        {
            bookTxt.Clear();
            authorTxt.Clear();
            genreTxt.Clear();

            if (addbook_picture.Image != null)
            {
                addbook_picture.Image.Dispose();
                addbook_picture.Image = null;
            }

            comboBoxLocation.SelectedIndex = -1;
            BookId = 0;
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

        private void bookTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
