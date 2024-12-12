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
using Microsoft.Reporting.WinForms;

namespace Library
{
    public partial class Reports : UserControl
    {
        private SqlConnection connection;

        public Reports()
        {
            InitializeComponent();
            // Initialize the connection string
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gutie\source\repos\Library\Library\Library\Database.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                connection.Open();

                SqlCommand command = new SqlCommand("Select * from books", connection);
                SqlDataAdapter d = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                d.Fill(dt);

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource source = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.ReportPath = @"C:\\Users\\gutie\\source\\repos\\Library\\Library\\Library\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}
