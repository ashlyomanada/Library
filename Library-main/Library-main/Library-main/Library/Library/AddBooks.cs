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
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
namespace Library
{

    public partial class addBooks : UserControl
    {
        private DataTable bookData;
        private SqlConnection con;
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ashly Omanada\source\repos\Library\Library\library.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");

        public addBooks()
        {
            InitializeComponent();
            con = Dbcon.GetConnection();
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
            try
            {
                // Ensure the selected item is not null
                if (comboBoxSection.SelectedItem == null)
                {
                    return; // Exit if nothing is selected
                }

                // Clear Shelf and Floor ComboBoxes
                comboBoxShelf.Items.Clear();
                comboBoxFloor.Items.Clear();

                // Get the selected Section
                string selectedSection = comboBoxSection.SelectedItem.ToString();

                // Query to get Shelves for the selected Section
                string query = "SELECT DISTINCT Shelf FROM locations WHERE Section = @Section ORDER BY Shelf";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Section", selectedSection);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxShelf.Items.Add(reader["Shelf"]?.ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering shelves: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    // Check if any required field is empty
        //    if (addbook_picture.Image == null ||
        //        string.IsNullOrEmpty(bookTxt.Text) ||
        //        string.IsNullOrEmpty(authorTxt.Text) || // Ensure the author field is checked
        //        string.IsNullOrEmpty(genreTxt.Text) ||
        //        string.IsNullOrEmpty(quantityTxt.Text) ||
        //        comboBoxSection.SelectedItem == null ||
        //        comboBoxShelf.SelectedItem == null ||
        //        comboBoxFloor.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please fill in all the fields to add a book.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    try
        //    {
        //        // Ensure the database connection is open
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }

        //        // Parse quantity input
        //        if (!int.TryParse(quantityTxt.Text.Trim(), out int quantity) || quantity <= 0)
        //        {
        //            MessageBox.Show("Please enter a valid quantity (positive integer).", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // Get selected values for Section, Shelf, and Floor
        //        string section = comboBoxSection.SelectedItem.ToString();
        //        string shelf = comboBoxShelf.SelectedItem.ToString();
        //        string floor = comboBoxFloor.SelectedItem.ToString();

        //        // Ensure the book directory exists
        //        string directoryPath = Path.Combine(
        //            @"C:\Users\Ashly Omanada\source\repos\Library\Library\BookDirectory",
        //            $"{bookTxt.Text.Trim()}_{authorTxt.Text.Trim()}_{DateTime.Today:yyyyMMdd}");

        //        if (!Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);
        //        }

        //        // Define the full path for the image
        //        string imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));

        //        // Copy the image to the directory if it doesn't already exist
        //        if (File.Exists(addbook_picture.ImageLocation))
        //        {
        //            File.Copy(addbook_picture.ImageLocation, imagePath, true);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Image file not found. Please select a valid image.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // Insert query with the Section, Shelf, and Floor fields
        //        string insertQuery = @"INSERT INTO books (Title, Author, Genre, Status, LocationId, date_Insert, image, quantity, Section, Shelf, Floor) 
        //                       VALUES (@Title, @Author, @Genre, @Status, @LocationId, @Date_Insert, @image, @Quantity, @Section, @Shelf, @Floor)";

        //        using (SqlCommand insertCMD = new SqlCommand(insertQuery, con))
        //        {
        //            insertCMD.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
        //            insertCMD.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
        //            insertCMD.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
        //            insertCMD.Parameters.AddWithValue("@Status", "Available");
        //            insertCMD.Parameters.AddWithValue("@LocationId", DBNull.Value); // Set this to an appropriate value if applicable
        //            insertCMD.Parameters.AddWithValue("@Date_Insert", DateTime.Today);
        //            insertCMD.Parameters.AddWithValue("@image", imagePath);
        //            insertCMD.Parameters.AddWithValue("@Quantity", quantity);
        //            insertCMD.Parameters.AddWithValue("@Section", section);
        //            insertCMD.Parameters.AddWithValue("@Shelf", shelf);
        //            insertCMD.Parameters.AddWithValue("@Floor", floor);

        //            insertCMD.ExecuteNonQuery();

        //            MessageBox.Show("Book added successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            // Clear the fields after success
        //            bookTxt.Clear();
        //            authorTxt.Clear();
        //            genreTxt.Clear();
        //            quantityTxt.Clear();
        //            addbook_picture.Image = null;
        //            comboBoxSection.SelectedIndex = -1;
        //            comboBoxShelf.SelectedIndex = -1;
        //            comboBoxFloor.SelectedIndex = -1;
        //        }

        //        // Reload the books in the DataGridView
        //        LoadBooks();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        // Ensure the database connection is closed
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any required field is empty
            if (addbook_picture.Image == null ||
                string.IsNullOrEmpty(bookTxt.Text) ||
                string.IsNullOrEmpty(authorTxt.Text) || // Ensure the author field is checked
                string.IsNullOrEmpty(genreTxt.Text) ||
                string.IsNullOrEmpty(quantityTxt.Text) ||
                comboBoxSection.SelectedItem == null ||
                comboBoxShelf.SelectedItem == null ||
                comboBoxFloor.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all the fields to add a book.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Ensure the database connection is open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Parse quantity input
                if (!int.TryParse(quantityTxt.Text.Trim(), out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity (positive integer).", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get selected values for Section, Shelf, and Floor
                string section = comboBoxSection.SelectedItem.ToString();
                string shelf = comboBoxShelf.SelectedItem.ToString();
                string floor = comboBoxFloor.SelectedItem.ToString();

                // Ensure the book directory exists
                string directoryPath = Path.Combine(
                    @"C:\Users\Ashly Omanada\source\repos\Library\Library\BookDirectory",
                    $"{bookTxt.Text.Trim()}_{authorTxt.Text.Trim()}_{DateTime.Today:yyyyMMdd}");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Define the full path for the image
                string imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));

                // Copy the image to the directory if it doesn't already exist
                if (File.Exists(addbook_picture.ImageLocation))
                {
                    File.Copy(addbook_picture.ImageLocation, imagePath, true);
                }
                else
                {
                    MessageBox.Show("Image file not found. Please select a valid image.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert query with the Section, Shelf, and Floor fields
                string insertQuery = @"INSERT INTO books (Title, Author, Genre, Status, LocationId, date_Insert, image, quantity, Section, Shelf, Floor) 
                               VALUES (@Title, @Author, @Genre, @Status, @LocationId, @Date_Insert, @image, @Quantity, @Section, @Shelf, @Floor)";

                using (SqlCommand insertCMD = new SqlCommand(insertQuery, con))
                {
                    insertCMD.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
                    insertCMD.Parameters.AddWithValue("@Status", "Available");
                    insertCMD.Parameters.AddWithValue("@LocationId", DBNull.Value); // Set this to an appropriate value if applicable
                    insertCMD.Parameters.AddWithValue("@Date_Insert", DateTime.Today);
                    insertCMD.Parameters.AddWithValue("@image", imagePath);
                    insertCMD.Parameters.AddWithValue("@Quantity", quantity);
                    insertCMD.Parameters.AddWithValue("@Section", section);
                    insertCMD.Parameters.AddWithValue("@Shelf", shelf);
                    insertCMD.Parameters.AddWithValue("@Floor", floor);

                    insertCMD.ExecuteNonQuery();

                    MessageBox.Show("Book added successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the fields after success
                    bookTxt.Clear();
                    authorTxt.Clear();
                    genreTxt.Clear();
                    quantityTxt.Clear();
                    addbook_picture.Image = null;
                    comboBoxSection.SelectedIndex = -1;
                    comboBoxShelf.SelectedIndex = -1;
                    comboBoxFloor.SelectedIndex = -1;
                }

                // Reload the books in the DataGridView
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
                // Clear existing items in all ComboBoxes
                comboBoxSection.Items.Clear();
                comboBoxShelf.Items.Clear();
                comboBoxFloor.Items.Clear();

                // Open the database connection if not already open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Load Section data
                string sectionQuery = "SELECT DISTINCT Section FROM locations ORDER BY Section";
                using (SqlCommand cmd = new SqlCommand(sectionQuery, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxSection.Items.Add(reader["Section"].ToString());
                    }
                    reader.Close();
                }

                // Load Shelf data
                string shelfQuery = "SELECT DISTINCT Shelf FROM locations ORDER BY Shelf";
                using (SqlCommand cmd = new SqlCommand(shelfQuery, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxShelf.Items.Add(reader["Shelf"].ToString());
                    }
                    reader.Close();
                }

                // Load Floor data
                string floorQuery = "SELECT DISTINCT Floor FROM locations ORDER BY Floor";
                using (SqlCommand cmd = new SqlCommand(floorQuery, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxFloor.Items.Add(reader["Floor"].ToString());
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

                // Updated query to include separate columns for Section, Shelf, and Floor
                string query = @"
SELECT 
    books.Id,
    books.Title,
    books.Author,
    books.Genre,
    books.Status,
    books.image,
    books.quantity,
    books.Section,
    books.Shelf,
    books.Floor,
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

                // Load data into DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                bookData = new DataTable(); // Initialize the DataTable
                adapter.Fill(bookData);

                // Set the DataSource of the DataGridView
                dataGridViewBooks.DataSource = bookData;

                // Hide unnecessary columns
                if (dataGridViewBooks.Columns["Id"] != null)
                {
                    dataGridViewBooks.Columns["Id"].Visible = false; // Hide the Id column
                }
                if (dataGridViewBooks.Columns["image"] != null)
                {
                    dataGridViewBooks.Columns["image"].Visible = false; // Hide the image column
                }
                if (dataGridViewBooks.Columns["date_Insert"] != null)
                {
                    dataGridViewBooks.Columns["date_Insert"].Visible = false; // Hide the date_Insert column
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

                // Display data in the textboxes
                bookTxt.Text = selectedRow.Cells["Title"].Value?.ToString();
                authorTxt.Text = selectedRow.Cells["Author"].Value?.ToString();
                genreTxt.Text = selectedRow.Cells["Genre"].Value?.ToString();
                quantityTxt.Text = selectedRow.Cells["quantity"].Value?.ToString();

                // Set the Section, Shelf, and Floor ComboBoxes
                string section = selectedRow.Cells["Section"].Value?.ToString();
                string shelf = selectedRow.Cells["Shelf"].Value?.ToString();
                string floor = selectedRow.Cells["Floor"].Value?.ToString();

                // Set the Section ComboBox
                if (!string.IsNullOrEmpty(section))
                {
                    if (comboBoxSection.Items.Contains(section))
                    {
                        comboBoxSection.SelectedItem = section;
                    }
                    else
                    {
                        comboBoxSection.SelectedIndex = -1; // Reset selection if no match
                    }
                }

                // Set the Shelf ComboBox
                if (!string.IsNullOrEmpty(shelf))
                {
                    if (comboBoxShelf.Items.Contains(shelf))
                    {
                        comboBoxShelf.SelectedItem = shelf;
                    }
                    else
                    {
                        comboBoxShelf.SelectedIndex = -1; // Reset selection if no match
                    }
                }

                // Set the Floor ComboBox
                if (!string.IsNullOrEmpty(floor))
                {
                    if (comboBoxFloor.Items.Contains(floor))
                    {
                        comboBoxFloor.SelectedItem = floor;
                    }
                    else
                    {
                        comboBoxFloor.SelectedIndex = -1; // Reset selection if no match
                    }
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


        //private void buttonUpdate_Click(object sender, EventArgs e)
        //{
        //    // Validate each field individually
        //    if (string.IsNullOrEmpty(bookTxt.Text))
        //    {
        //        MessageBox.Show("Please enter the book title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        bookTxt.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(authorTxt.Text))
        //    {
        //        MessageBox.Show("Please enter the author name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        authorTxt.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(genreTxt.Text))
        //    {
        //        MessageBox.Show("Please enter the genre.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        genreTxt.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(quantityTxt.Text))
        //    {
        //        MessageBox.Show("Please enter the quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        quantityTxt.Focus();
        //        return;
        //    }

        //    // Validate quantity is a positive integer
        //    if (!int.TryParse(quantityTxt.Text.Trim(), out int quantity) || quantity <= 0)
        //    {
        //        MessageBox.Show("Please enter a valid quantity (positive integer).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        quantityTxt.Focus();
        //        return;
        //    }

        //    if (comboBoxSection.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please select a section.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        comboBoxSection.Focus();
        //        return;
        //    }

        //    if (comboBoxShelf.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please select a shelf.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        comboBoxShelf.Focus();
        //        return;
        //    }

        //    if (comboBoxFloor.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please select a floor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        comboBoxFloor.Focus();
        //        return;
        //    }

        //    // Confirm update action
        //    DialogResult check = MessageBox.Show("Are you sure you want to UPDATE?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (check != DialogResult.Yes)
        //    {
        //        MessageBox.Show("Update cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        // Ensure database connection is open
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }

        //        // Get selected values from ComboBoxes
        //        string section = comboBoxSection.SelectedItem.ToString();
        //        string shelf = comboBoxShelf.SelectedItem.ToString();
        //        string floor = comboBoxFloor.SelectedItem.ToString();

        //        // Handle image saving
        //        string imagePath = null;
        //        if (addbook_picture.ImageLocation != null)
        //        {
        //            // If the image was loaded from a file
        //            string directoryPath = Path.Combine(@"C:\Users\Ashly Omanada\source\repos\Library\Library\BookDirectory\",
        //                                                bookTxt.Text.Trim() + "_" + authorTxt.Text.Trim() + "_" + DateTime.Today.ToString("yyyyMMdd"));
        //            if (!Directory.Exists(directoryPath))
        //            {
        //                Directory.CreateDirectory(directoryPath);
        //            }

        //            imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));
        //            File.Copy(addbook_picture.ImageLocation, imagePath, true);
        //        }

        //        // Update query with conditional image update
        //        string updateData = imagePath != null
        //            ? @"
        //        UPDATE books 
        //        SET Title = @Title,
        //            Author = @Author,
        //            Genre = @Genre,
        //            Section = @Section,
        //            Shelf = @Shelf,
        //            Floor = @Floor,
        //            image = @image,
        //            Quantity = @Quantity,
        //            date_update = @date_update
        //        WHERE Id = @Id"
        //            : @"
        //        UPDATE books 
        //        SET Title = @Title,
        //            Author = @Author,
        //            Genre = @Genre,
        //            Section = @Section,
        //            Shelf = @Shelf,
        //            Floor = @Floor,
        //            Quantity = @Quantity,
        //            date_update = @date_update
        //        WHERE Id = @Id";

        //        using (SqlCommand updateCmd = new SqlCommand(updateData, con))
        //        {
        //            updateCmd.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
        //            updateCmd.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
        //            updateCmd.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
        //            updateCmd.Parameters.AddWithValue("@Section", section);
        //            updateCmd.Parameters.AddWithValue("@Shelf", shelf);
        //            updateCmd.Parameters.AddWithValue("@Floor", floor);
        //            updateCmd.Parameters.AddWithValue("@Quantity", quantity);
        //            updateCmd.Parameters.AddWithValue("@date_update", DateTime.Now);
        //            updateCmd.Parameters.AddWithValue("@Id", BookId);

        //            // Handle the image parameter
        //            if (imagePath != null)
        //            {
        //                updateCmd.Parameters.AddWithValue("@image", imagePath);
        //            }

        //            updateCmd.ExecuteNonQuery();
        //        }

        //        MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // Refresh DataGridView
        //        LoadBooks();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error updating book: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        // Ensure database connection is closed
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //}

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // Validate each field individually
            if (string.IsNullOrEmpty(bookTxt.Text))
            {
                MessageBox.Show("Please enter the book title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookTxt.Focus();
                return;
            }

            if (string.IsNullOrEmpty(authorTxt.Text))
            {
                MessageBox.Show("Please enter the author name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                authorTxt.Focus();
                return;
            }

            if (string.IsNullOrEmpty(genreTxt.Text))
            {
                MessageBox.Show("Please enter the genre.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                genreTxt.Focus();
                return;
            }

            if (!int.TryParse(quantityTxt.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (positive integer).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                quantityTxt.Focus();
                return;
            }

            // Confirm update action
            DialogResult check = MessageBox.Show("Are you sure you want to UPDATE?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check != DialogResult.Yes)
            {
                MessageBox.Show("Update cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string section = comboBoxSection.SelectedItem.ToString();
                string shelf = comboBoxShelf.SelectedItem.ToString();
                string floor = comboBoxFloor.SelectedItem.ToString();

                string imagePath = null;

                if (addbook_picture.ImageLocation != null)
                {
                    if (addbook_picture.Image != null)
                    {
                        addbook_picture.Image.Dispose();
                        addbook_picture.Image = null;
                    }

                    string safeBookTitle = string.Join("_", bookTxt.Text.Trim().Split(Path.GetInvalidFileNameChars()));
                    string safeAuthorName = string.Join("_", authorTxt.Text.Trim().Split(Path.GetInvalidFileNameChars()));

                    string directoryPath = Path.Combine(@"C:\Users\Ashly Omanada\source\repos\Library\Library\BookDirectory\",
                                                         safeBookTitle + "_" + safeAuthorName + "_" + DateTime.Today.ToString("yyyyMMdd"));
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    imagePath = Path.Combine(directoryPath, Path.GetFileName(addbook_picture.ImageLocation));
                    File.Copy(addbook_picture.ImageLocation, imagePath, true);
                }

                string updateData = @"
        UPDATE books 
        SET Title = @Title,
            Author = @Author,
            Genre = @Genre,
            Section = @Section,
            Shelf = @Shelf,
            Floor = @Floor,
            image = @image,
            Quantity = @Quantity,
            date_update = @date_update
        WHERE Id = @Id";

                using (SqlCommand updateCmd = new SqlCommand(updateData, con))
                {
                    updateCmd.Parameters.AddWithValue("@Title", bookTxt.Text.Trim());
                    updateCmd.Parameters.AddWithValue("@Author", authorTxt.Text.Trim());
                    updateCmd.Parameters.AddWithValue("@Genre", genreTxt.Text.Trim());
                    updateCmd.Parameters.AddWithValue("@Section", section);
                    updateCmd.Parameters.AddWithValue("@Shelf", shelf);
                    updateCmd.Parameters.AddWithValue("@Floor", floor);
                    updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                    updateCmd.Parameters.AddWithValue("@date_update", DateTime.Now);
                    updateCmd.Parameters.AddWithValue("@Id", BookId);

                    if (imagePath != null)
                    {
                        updateCmd.Parameters.AddWithValue("@image", imagePath);
                    }
                    else
                    {
                        updateCmd.Parameters.AddWithValue("@image", DBNull.Value);
                    }

                    updateCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {

        }

        //private void button3_Click_1(object sender, EventArgs e)
        //{
        //    if (BookId == 0)
        //    {
        //        MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    DialogResult confirmDelete = MessageBox.Show("Are you sure you want to DELETE this book?",
        //                                                 "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    if (confirmDelete == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }

        //            // Retrieve the image path of the book to delete
        //            string imagePath = string.Empty;
        //            string getImagePathQuery = "SELECT image FROM books WHERE Id=@Id";
        //            using (SqlCommand cmd = new SqlCommand(getImagePathQuery, con))
        //            {
        //                cmd.Parameters.AddWithValue("@Id", BookId);
        //                SqlDataReader reader = cmd.ExecuteReader();

        //                if (reader.Read())
        //                {
        //                    imagePath = reader["image"]?.ToString();
        //                }
        //                reader.Close();
        //            }

        //            // Clear the PictureBox image to release the file lock
        //            if (addbook_picture.Image != null)
        //            {
        //                addbook_picture.Image.Dispose();
        //                addbook_picture.Image = null;
        //            }

        //            // Delete the book record from the database
        //            string deleteQuery = "DELETE FROM books WHERE Id=@Id";
        //            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
        //            {
        //                deleteCmd.Parameters.AddWithValue("@Id", BookId);
        //                deleteCmd.ExecuteNonQuery();
        //            }

        //            // Delete the associated image file if it exists
        //            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
        //            {
        //                File.Delete(imagePath);
        //            }

        //            MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            // Refresh the DataGridView
        //            LoadBooks();

        //            // Reset the form fields
        //            ResetFormFields();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error deleting book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            if (con.State == ConnectionState.Open)
        //            {
        //                con.Close();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Delete operation cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

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
                    // Ensure the database connection is open
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string imagePath = string.Empty;

                    // Retrieve the image path of the book to delete
                    string getImagePathQuery = "SELECT image FROM books WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(getImagePathQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", BookId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            imagePath = reader["image"]?.ToString();
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
                        try
                        {
                            File.Delete(imagePath);
                        }
                        catch (Exception fileEx)
                        {
                            MessageBox.Show($"Error deleting image file: {fileEx.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView
                    LoadBooks();

                    // Reset the form fields
                    ResetFormFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                MessageBox.Show("Delete operation cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Helper method to reset form fields
        private void ResetFormFields()
        {
            bookTxt.Clear();
            authorTxt.Clear();
            genreTxt.Clear();
            quantityTxt.Clear();
            addbook_picture.Image = null;
            comboBoxSection.SelectedIndex = -1;
            comboBoxShelf.SelectedIndex = -1;
            comboBoxFloor.SelectedIndex = -1;
            BookId = 0;
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            ResetFormFields();
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

            comboBoxSection.SelectedIndex = -1;
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Clear Floor ComboBox
        //        comboBoxFloor.Items.Clear();

        //        // Get the selected Section and Shelf
        //        string selectedSection = comboBoxSection.SelectedItem.ToString();
        //        string selectedShelf = comboBoxShelf.SelectedItem.ToString();

        //        // Query to get Floors for the selected Section and Shelf
        //        string query = "SELECT DISTINCT Floor FROM locations WHERE Section = @Section AND Shelf = @Shelf ORDER BY Floor";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@Section", selectedSection);
        //            cmd.Parameters.AddWithValue("@Shelf", selectedShelf);

        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                comboBoxFloor.Items.Add(reader["Floor"].ToString());
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error filtering floors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Ensure a valid selection is made in Section and Shelf ComboBoxes
                if (comboBoxSection.SelectedItem == null || comboBoxShelf.SelectedItem == null)
                {
                    comboBoxFloor.Items.Clear(); // Clear floors if prerequisites are not met
                    return;
                }

                // Clear Floor ComboBox
                comboBoxFloor.Items.Clear();

                // Get the selected Section and Shelf
                string selectedSection = comboBoxSection.SelectedItem.ToString();
                string selectedShelf = comboBoxShelf.SelectedItem.ToString();

                // Query to get Floors for the selected Section and Shelf
                string query = "SELECT DISTINCT Floor FROM locations WHERE Section = @Section AND Shelf = @Shelf ORDER BY Floor";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Section", selectedSection);
                    cmd.Parameters.AddWithValue("@Shelf", selectedShelf);

                    // Open the connection if it's closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Execute the query and populate the Floor ComboBox
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No floors found for the selected section and shelf.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        while (reader.Read())
                        {
                            comboBoxFloor.Items.Add(reader["Floor"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering floors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void comboBoxFloor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
