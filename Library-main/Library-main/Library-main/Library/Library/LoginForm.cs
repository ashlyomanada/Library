using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;

namespace Library
{
    public partial class LoginForm : Form
    {
        private SqlConnection con;
        public LoginForm()
        {
            InitializeComponent();
            con = Dbcon.GetConnection();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            RegisterForm rForm = new RegisterForm();
            rForm.Show();
            this.Hide();
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            loginpassword.PasswordChar = showPassword.Checked ? '\0' : '*';
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (loginusername.Text == "" || loginpassword.Text == "")
            {
                MessageBox.Show("Please fill all the fields to login", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (con.State != ConnectionState.Open)
                {
                    try
                    {
                        con.Open();
                        string loginData = "SELECT Id, role FROM users WHERE username = @username AND password = @password";
                        using (SqlCommand CMD = new SqlCommand(loginData, con))
                        {
                            CMD.Parameters.AddWithValue("@username", loginusername.Text.Trim());
                            CMD.Parameters.AddWithValue("@password", loginpassword.Text.Trim());

                            SqlDataReader reader = CMD.ExecuteReader();

                            if (reader.HasRows)
                            {
                                reader.Read();
                                int userId = Convert.ToInt32(reader["Id"]); // Fetch the user ID
                                string role = reader["role"].ToString();

                                // Store the user ID in a global or session-like variable
                                Session.UserId = userId;

                                // Redirect based on role
                                if (role == "user")
                                {
                                    MessageBox.Show("Login successful! Redirecting to User Dashboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    userdashboard userdashboard = new userdashboard();
                                    userdashboard.Show();
                                    this.Hide();
                                }
                                else if (role == "admin")
                                {
                                    MessageBox.Show("Login successful! Redirecting to Admin Dashboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    adminForm adminForm = new adminForm();
                                    adminForm.Show();
                                    this.Hide();
                                }
                                else if (role == "librarian")
                                {
                                    MessageBox.Show("Login successful! Redirecting to Librarian Dashboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    librarianform librarianform = new librarianform();
                                    librarianform.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Role not recognized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting to database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
