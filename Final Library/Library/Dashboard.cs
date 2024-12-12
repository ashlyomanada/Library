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
    public partial class dashboard : UserControl
    {
        private SqlConnection con;

        public dashboard()
        {
            con = Dbcon.GetConnection();
            InitializeComponent();
            LoadCounts();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void LoadCounts()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Query to count available books
                string availableBooksQuery = "SELECT COUNT(*) FROM books WHERE Status = 'Available'";
                SqlCommand cmdAvailableBooks = new SqlCommand(availableBooksQuery, con);
                int availableBooksCount = Convert.ToInt32(cmdAvailableBooks.ExecuteScalar());
                lblAvailableBooks.Text = availableBooksCount.ToString();

                // Query to count users
                string usersQuery = "SELECT COUNT(*) FROM users WHERE role = 'user'"; // Adjust table name to match your database
                SqlCommand cmdUsers = new SqlCommand(usersQuery, con);
                int usersCount = Convert.ToInt32(cmdUsers.ExecuteScalar());
                lblUsers.Text = usersCount.ToString();

                // Query to count borrowed books
                string borrowedBooksQuery = "SELECT COUNT(*) FROM books WHERE Status = 'Borrowed'";
                SqlCommand cmdBorrowedBooks = new SqlCommand(borrowedBooksQuery, con);
                int borrowedBooksCount = Convert.ToInt32(cmdBorrowedBooks.ExecuteScalar());
                lblBorrowedBooks.Text = borrowedBooksCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading counts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}

