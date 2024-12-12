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
    public partial class librarianform : Form
    {
        public librarianform()
        {
            InitializeComponent();
        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            dashboard admin = new dashboard();
            admin.Dock = DockStyle.Fill;

            panel3.Controls.Clear();
            panel3.Controls.Add(admin);

            admin.Show();


        }

        private void borrowedBtn_Click(object sender, EventArgs e)
        {
            librarian_borrowed librarian = new librarian_borrowed();
            librarian.Dock = DockStyle.Fill;

            panel3.Controls.Clear();
            panel3.Controls.Add(librarian);

            librarian.Show();

        }

        private void requestedBtn_Click(object sender, EventArgs e)
        {

            requestedBooks requested = new requestedBooks();
            requested.Dock = DockStyle.Fill;

            panel3.Controls.Clear();
            panel3.Controls.Add(requested);

            requested.Show();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboard1_Load(object sender, EventArgs e)
        {

        }

        private void librarianform_Load(object sender, EventArgs e)
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
    }
}
