using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class adminForm : Form
    {
        public adminForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult check= MessageBox.Show("Login successful! Redirecting to Admin Dashboard.", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes) { 
                LoginForm lform = new LoginForm();
                lform.Show();
                this.Hide();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = true;
            locationForm1.Visible = false;
            addBooks1.Visible = false;
            dashboard1.LoadCounts();
        }

        private void booksBtn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            locationForm1.Visible = false;
            addBooks1.Visible = true;
            addBooks1.LoadBooks();
            addBooks1.LoadLocationsIntoComboBox();
        }

        private void locationsBtn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            locationForm1.Visible = true;
            addBooks1.Visible = false;
            locationForm1.LoadLocations();
        }

        private void adminForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NotificationForm notif = new NotificationForm();
            notif.TopLevel = false;
            notif.Dock = DockStyle.Fill;

            panel3.Controls.Clear();
            panel3.Controls.Add(notif);

            notif.Show();
        }

        private void dashboard1_Load(object sender, EventArgs e)
        {

        }
    }
}
