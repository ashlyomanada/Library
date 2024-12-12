using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class userRequested : UserControl
    {
        private string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\JUSTINE MADERAZO\source\repos\Library\Library\library.mdf"";Integrated Security=True;Connect Timeout=30";
        public userRequested()
        {
            InitializeComponent();
            LoadRequestedBooks();
        }
        private void LoadRequestedBooks()
        {
            // Query to fetch requested books for the current user (assuming a user ID)
            // Adjust query based on your database structure
            string query = @"
                SELECT 
                    books.Title AS BookTitle, 
                    requests.RequestDate, 
                    requests.Status
                FROM 
                    requests requests
                INNER JOIN 
                    Books books
                ON 
                    requests.BookId = books.Id
                WHERE 
                    requests.UserId = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int userId = Session.UserId;
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the DataTable to the DataGridView
                        dataGridViewRequestedBooks.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void dataGridViewRequestedBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
