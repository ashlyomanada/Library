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
    public partial class librarian_borrowed : UserControl
    {
        private SqlConnection con;
        
        public librarian_borrowed()
        {
            con = Dbcon.GetConnection();
            InitializeComponent();
            LoadBorrowedBooks();
        }

        private void ClearFields()
        {
            txtBookTitle.Clear();
            txtAuthor.Clear();
            txtRequestDate.Clear();
            txtReturnDate.Clear();
            txtUsername.Clear();
            txtStatus.Clear();
            quantityTxt.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Populate textboxes with selected row data
                txtBookTitle.Text = selectedRow.Cells["BookTitle"].Value.ToString();
                txtAuthor.Text = selectedRow.Cells["Author"].Value.ToString();
                txtRequestDate.Text = Convert.ToDateTime(selectedRow.Cells["RequestDate"].Value).ToString("yyyy-MM-dd");
                txtReturnDate.Text = Convert.ToDateTime(selectedRow.Cells["ReturnDate"].Value).ToString("yyyy-MM-dd");
                txtUsername.Text = selectedRow.Cells["BorrowedBy"].Value.ToString();
                txtStatus.Text = selectedRow.Cells["Status"].Value.ToString();
                quantityTxt.Text = selectedRow.Cells["RequestQuantity"].Value.ToString();
            }
        }



        //    public void LoadBorrowedBooks()
        //    {
        //        string query = @"
        //SELECT 
        //    books.Id AS BookId,
        //    requests.Id AS RequestId,
        //    books.Title AS BookTitle,
        //    books.Author AS Author,
        //    requests.RequestDate,
        //    requests.ReturnDate,
        //    books.Status,
        //    users.Username AS BorrowedBy
        //FROM 
        //    requests
        //INNER JOIN 
        //    Books ON requests.BookId = Books.Id
        //INNER JOIN 
        //    Users ON requests.UserId = Users.Id
        //WHERE 
        //    books.Status = 'Borrowed' -- Filter for currently borrowed books
        //    AND requests.Id = (
        //        SELECT MAX(r.Id)
        //        FROM requests r
        //        WHERE r.BookId = books.Id
        //        AND r.Status IN ('Accepted') -- Modify this based on how you track borrow status
        //    )
        //";

        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            try
        //            {
        //                con.Open();
        //                using (SqlCommand cmd = new SqlCommand(query, con))
        //                {
        //                    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                    DataTable dt = new DataTable();
        //                    da.Fill(dt);

        //                    // Bind data to DataGridView
        //                    dataGridView1.DataSource = dt;

        //                    // Hide the BookId and RequestId columns
        //                    if (dataGridView1.Columns["BookId"] != null)
        //                    {
        //                        dataGridView1.Columns["BookId"].Visible = false;
        //                    }

        //                    if (dataGridView1.Columns["RequestId"] != null)
        //                    {
        //                        dataGridView1.Columns["RequestId"].Visible = false;
        //                    }

        //                    // Auto-size columns
        //                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }


        public void LoadBorrowedBooks()
        {
            string query = @"
SELECT 
    books.Id AS BookId,
    requests.Id AS RequestId,
    books.Title AS BookTitle,
    books.Author AS Author,
    requests.RequestDate,
    requests.ReturnDate,
    requests.Quantity AS RequestQuantity,
    books.Status,
    users.Username AS BorrowedBy
FROM 
    requests
INNER JOIN 
    Books ON requests.BookId = Books.Id
INNER JOIN 
    Users ON requests.UserId = Users.Id
WHERE 
    books.Status = 'Borrowed'";

            using (SqlConnection con = Dbcon.GetConnection())
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

                        // Hide the BookId and RequestId columns
                        if (dataGridView1.Columns["BookId"] != null)
                        {
                            dataGridView1.Columns["BookId"].Visible = false;
                        }

                        if (dataGridView1.Columns["RequestId"] != null)
                        {
                            dataGridView1.Columns["RequestId"].Visible = false;
                        }

                        // Auto-size columns
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int bookId = Convert.ToInt32(selectedRow.Cells["BookId"].Value);
                int requestQuantity = Convert.ToInt32(selectedRow.Cells["RequestQuantity"].Value);

                // Call the ReturnBook method
                ReturnBook(bookId, requestQuantity);
                LoadBorrowedBooks();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a row to return the book.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //private void ReturnBook(int bookId)
        //{
        //    // Transaction to ensure both updates happen together
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlTransaction transaction = null;

        //        try
        //        {
        //            con.Open();
        //            transaction = con.BeginTransaction();

        //            // Update the Books table to set the status to Available
        //            string updateBookQuery = "UPDATE Books SET Status = 'Available' WHERE Id = @BookId";
        //            using (SqlCommand cmd = new SqlCommand(updateBookQuery, con, transaction))
        //            {
        //                cmd.Parameters.AddWithValue("@BookId", bookId);
        //                cmd.ExecuteNonQuery();
        //            }

        //            // Update the Requests table to set the status to Returned
        //            string updateRequestQuery = "UPDATE Requests SET Status = 'Returned' WHERE BookId = @BookId AND Status = 'Accepted'";
        //            using (SqlCommand cmd = new SqlCommand(updateRequestQuery, con, transaction))
        //            {
        //                cmd.Parameters.AddWithValue("@BookId", bookId);
        //                cmd.ExecuteNonQuery();
        //            }

        //            // Commit the transaction
        //            transaction.Commit();

        //            MessageBox.Show("The book has been returned.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Rollback the transaction in case of an error
        //            if (transaction != null)
        //            {
        //                transaction.Rollback();
        //            }

        //            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }


        private void ReturnBook(int bookId, int returnQuantity)
        {
            using (SqlConnection con = Dbcon.GetConnection())
            {
                SqlTransaction transaction = null;

                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();

                    // Update the Books table to increment the quantity and set the status to 'Available' if applicable
                    string updateBookQuery = @"
                UPDATE Books 
                SET 
                    Status = CASE WHEN quantity + @ReturnQuantity > 0 THEN 'Available' ELSE Status END,
                    quantity = quantity + @ReturnQuantity 
                WHERE Id = @BookId";

                    using (SqlCommand cmd = new SqlCommand(updateBookQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.Parameters.AddWithValue("@ReturnQuantity", returnQuantity); // Use the value from the DataGridView
                        cmd.ExecuteNonQuery();
                    }

                    // Update the Requests table to set the status to 'Returned'
                    string updateRequestQuery = @"
                UPDATE Requests 
                SET Status = 'Returned', ReturnDate = GETDATE()
                WHERE BookId = @BookId AND Status = 'Accepted'";

                    using (SqlCommand cmd = new SqlCommand(updateRequestQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();

                    MessageBox.Show("The book has been returned and quantity updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the book ID (Assuming the ID is in the "BookId" column)
                int bookId = Convert.ToInt32(selectedRow.Cells["BookId"].Value);

                // Call the function to update the book's status to "Not Returned" and request status to "Not Returned"
                NotReturned(bookId);

                // Reload the DataGridView to reflect the updated status
                LoadBorrowedBooks();
            }
            else
            {
                MessageBox.Show("Please select a book to mark as 'Not Returned'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void NotReturned(int bookId)
        {
            // Transaction to ensure both updates happen together
            using (SqlConnection con = Dbcon.GetConnection())
            {
                SqlTransaction transaction = null;

                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();

                    // Update the Requests table to set the status to Not Returned
                    string updateRequestQuery = "UPDATE Requests SET Status = 'Not Returned' WHERE BookId = @BookId";
                    using (SqlCommand cmd = new SqlCommand(updateRequestQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();

                    MessageBox.Show("The book has been marked as Not Returned", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
