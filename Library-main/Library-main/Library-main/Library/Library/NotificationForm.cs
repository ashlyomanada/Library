using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class NotificationForm : Form
    {
        private SqlConnection con;
        public NotificationForm()
        {
            InitializeComponent();
            con = Dbcon.GetConnection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadDueToday();
            button1.BackColor = Color.Orange;
            button2.BackColor = Color.White;        }

        public void loadDueToday()
        {
            try
            {
                string query = @"
            SELECT 
                u.username AS UserName,
                b.title AS BookTitle,
                r.RequestDate,
                r.ReturnDate
            FROM 
                requests r
            INNER JOIN 
                users u ON r.UserId = u.id
            INNER JOIN 
                books b ON r.BookId = b.id
            WHERE 
                CAST(r.ReturnDate AS DATE) = CAST(GETDATE() AS DATE)";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["UserName"].HeaderText = "User Name";
                dataGridView1.Columns["BookTitle"].HeaderText = "Book Title";
                dataGridView1.Columns["RequestDate"].HeaderText = "Request Date";
                dataGridView1.Columns["ReturnDate"].HeaderText = "Return Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying due today records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadPastDue()
        {
            try
            {
                string query = @"
            SELECT 
                u.username AS UserName,
                b.title AS BookTitle,
                r.RequestDate,
                r.ReturnDate
            FROM 
                requests r
            INNER JOIN 
                users u ON r.UserId = u.id
            INNER JOIN 
                books b ON r.BookId = b.id
            WHERE 
                CAST(r.ReturnDate AS DATE) < CAST(GETDATE() AS DATE) AND 
                r.Status = 'Accepted'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["UserName"].HeaderText = "User Name";
                dataGridView1.Columns["BookTitle"].HeaderText = "Book Title";
                dataGridView1.Columns["RequestDate"].HeaderText = "Request Date";
                dataGridView1.Columns["ReturnDate"].HeaderText = "Return Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying past due records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void NotificationForm_Load(object sender, EventArgs e)
        {
            loadDueToday();
            button1.BackColor = Color.Orange;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadPastDue();
            button2.BackColor = Color.Orange;
            button1.BackColor = Color.White;
        }
    }
}
