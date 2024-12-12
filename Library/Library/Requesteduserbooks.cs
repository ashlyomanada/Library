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
    public partial class RequestedUserBooks : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ashly Omanada\source\repos\Library\Library\library.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public RequestedUserBooks()
        {
            InitializeComponent();
            LoadRequestedBooks();
        }
        public void LoadRequestedBooks()
        {
            // SQL query to retrieve requested books for the logged-in user, joining with books table
            string query = @"
                SELECT 
                    books.Title AS BookTitle, 
                    requests.RequestDate, 
                    requests.Status,
          requests.ReturnDate
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
                        // Assume you have a method to get the logged-in user ID
                        int userId = Session.UserId;
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        // Execute the query and load the results into a DataTable
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the DataTable to the DataGridView
                        dataGridViewRequestedUserBooks.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RequestedUserBooks_Load(object sender, EventArgs e)
        {

        }
    }
}
