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

namespace Library
{
    public partial class requestedBooks : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\renzj\source\repos\Library\Library\library.mdf;Integrated Security=True;Connect Timeout=30";
        public requestedBooks()
        {
            InitializeComponent();
            LoadRequestedBooks();
        }

        private void requestedBooks_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void LoadRequestedBookDetails(int requestId)
        {
            string query = @"
        SELECT 
            u.username AS UserName, 
            b.Title AS BookTitle, 
            b.Author, 
            r.RequestDate, 
            r.ReturnDate
        FROM 
            requests r
        INNER JOIN 
            Users u ON r.UserId = u.Id
        INNER JOIN 
            Books b ON r.BookId = b.Id
        WHERE 
            r.Id = @RequestId";

            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\renzj\source\repos\Library\Library\library.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RequestId", requestId);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Populate the TextBoxes with the fetched details
                            txtUserName.Text = reader["UserName"].ToString();
                            txtBookTitle.Text = reader["BookTitle"].ToString();
                            txtAuthor.Text = reader["Author"].ToString();

                            // Set the return date from the database
                            if (reader["ReturnDate"] != DBNull.Value)
                            {
                                returnDatePicker.Value = Convert.ToDateTime(reader["ReturnDate"]);
                            }
                            else
                            {
                                // Set the default date (optional)
                                returnDatePicker.Value = DateTime.Now;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Request details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                int requestId = Convert.ToInt32(selectedRow.Cells["Id"].Value);  // Get request ID from the row

                // Fetch the user and book details from the database using requestId
                LoadRequestedBookDetails(requestId);
            }
        }
        public void LoadRequestedBooks()
        {
            string query = @"
    SELECT 
        requests.Id,
        books.Id as BookId,
        books.Title AS BookTitle, 
        requests.RequestDate, 
        requests.Status,
        requests.ReturnDate,
        users.Username AS RequestedBy
    FROM 
        requests requests
    INNER JOIN 
        Books books ON requests.BookId = books.Id
    INNER JOIN
        Users users ON requests.UserId = users.Id"; // Assuming there is a Users table for usernames

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind data to DataGridView
                        dataGridView1.DataSource = dt;

                        // Auto size columns to fill the available space
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the Request ID and Book ID from the selected row
                int requestId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                int bookId = Convert.ToInt32(selectedRow.Cells["BookId"].Value); // Ensure this column exists in your DataGridView

                // Check if the request is already accepted
                string currentStatus = selectedRow.Cells["Status"].Value.ToString();
                if (currentStatus == "Accepted")
                {
                    MessageBox.Show("This request is already accepted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Get the return date selected by the user
                DateTime returnDate = returnDatePicker.Value;

                // Ensure the selected return date is in the future
                if (returnDate <= DateTime.Now)
                {
                    MessageBox.Show("The return date must be in the future.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the request status, return date, and book status
                AcceptRequestAndUpdateBook(requestId, bookId, "Accepted", returnDate, "Borrowed");
            }
            else
            {
                MessageBox.Show("Please select a request to accept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AcceptRequestAndUpdateBook(int requestId, int bookId, string requestStatus, DateTime returnDate, string bookStatus)
        {
            string updateRequestQuery = "UPDATE requests SET Status = @RequestStatus, ReturnDate = @ReturnDate WHERE Id = @RequestId";
            string updateBookQuery = "UPDATE Books SET Status = @BookStatus WHERE Id = @BookId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Begin a transaction to ensure data consistency
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(updateRequestQuery, con, transaction))
                        {
                            // Update request details
                            cmd.Parameters.AddWithValue("@RequestStatus", requestStatus);
                            cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                            cmd.Parameters.AddWithValue("@RequestId", requestId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(updateBookQuery, con, transaction))
                        {
                            // Update book status
                            cmd.Parameters.AddWithValue("@BookStatus", bookStatus);
                            cmd.Parameters.AddWithValue("@BookId", bookId);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Request accepted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Refresh the DataGridView to show updated data
                    LoadRequestedBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the Request ID from the selected row
                int requestId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Check if the request is already declined
                string currentStatus = selectedRow.Cells["Status"].Value.ToString();
                if (currentStatus == "Declined")
                {
                    MessageBox.Show("This request is already declined.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirm the action with the user
                DialogResult confirmResult = MessageBox.Show("Are you sure you want to decline this request?", "Confirm Decline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    // Update the request status to "Declined"
                    DeclineRequest(requestId, "Declined");
                }
            }
            else
            {
                MessageBox.Show("Please select a request to decline.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeclineRequest(int requestId, string requestStatus)
        {
            string updateRequestQuery = "UPDATE requests SET Status = @RequestStatus WHERE Id = @RequestId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(updateRequestQuery, con))
                    {
                        // Update the request status
                        cmd.Parameters.AddWithValue("@RequestStatus", requestStatus);
                        cmd.Parameters.AddWithValue("@RequestId", requestId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Request has been declined.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView to reflect changes
                    LoadRequestedBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = comboBox1.SelectedItem.ToString();
            LoadRequestedBooks(selectedStatus);
        }
    

    private void LoadRequestedBooks(string statusFilter)
        {
            string query = @"
        SELECT 
            requests.Id,
            books.Id AS BookId,
            books.Title AS BookTitle,
            requests.RequestDate,
            requests.Status,
            requests.ReturnDate,
            users.Username AS RequestedBy
        FROM 
            requests requests
        INNER JOIN 
            Books books ON requests.BookId = books.Id
        INNER JOIN 
            Users users ON requests.UserId = users.Id
        WHERE 
            (@Status = 'All' OR requests.Status = @Status)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", statusFilter);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt;
            }
        }
    }
    }
