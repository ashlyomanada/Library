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
    public partial class userdashboard : Form
    {
        public userdashboard()
        {
            InitializeComponent();
            userbook1.Visible=false;
            requestedUserBooks1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            userbook1.Visible = true;
            requestedUserBooks1.Visible = false;
            userbook1.LoadBooks();
        }

        private void userbook1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            userbook1.Visible = false;
            requestedUserBooks1.Visible = true;
            requestedUserBooks1.LoadRequestedBooks();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void userdashboard_Load(object sender, EventArgs e)
        {

        }

        private void requestedUserBooks1_Load(object sender, EventArgs e)
        {

        }
    }
}
