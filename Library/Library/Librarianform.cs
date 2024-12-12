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
            dashboard1.Visible = true;
            librarian_borrowed1.Visible = false;
            requestedBooks1.Visible = false;
            dashboard1.LoadCounts();


        }

        private void borrowedBtn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            librarian_borrowed1.Visible = true;
            requestedBooks1.Visible = false;
            librarian_borrowed1.LoadBorrowedBooks();

        }

        private void requestedBtn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            librarian_borrowed1.Visible = false;
            requestedBooks1.Visible = true;
            requestedBooks1.LoadRequestedBooks();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboard1_Load(object sender, EventArgs e)
        {

        }
    }
}
