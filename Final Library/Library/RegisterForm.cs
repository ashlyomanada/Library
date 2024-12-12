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

namespace Library
{
    public partial class RegisterForm : Form
    {
        private SqlConnection con;
        public RegisterForm()
        {
            InitializeComponent();
            con = Dbcon.GetConnection();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm lform = new LoginForm();
            lform.Show();
            this.Hide();
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            password_register.PasswordChar = showPassword.Checked ? '\0' : '*';
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (email_register.Text == "" || username_register.Text == "" || password_register.Text == "")
            {
                MessageBox.Show("Please Fill all of the fields to register", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (con.State != ConnectionState.Open)
                {
                    try
                        
                    {
                        con.Open();
                        String checkUsername = "SELECT COUNT(*) FROM users WHERE username = @username";
                        using (SqlCommand checkCMD = new SqlCommand(checkUsername, con))
                        {
                            checkCMD.Parameters.AddWithValue("@username", username_register.Text.Trim());
                            int count = (int)checkCMD.ExecuteScalar();
                            if (count >= 1)
                            {
                                MessageBox.Show(username_register.Text.Trim() + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime day = DateTime.Today;
                                string insertData = "INSERT INTO users (email,username,password,date_register)" + "VALUES(@email,@username,@password,@date)";
                                using (SqlCommand insertCMD = new SqlCommand(insertData, con))
                                {
                                    insertCMD.Parameters.AddWithValue("@email",email_register.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@username", username_register.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@password", password_register.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@date", day);
                                    insertCMD.ExecuteNonQuery();
                                    MessageBox.Show("Registered Succesfully","Information Message",MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoginForm lform = new LoginForm();
                                    lform.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Connecting to Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
