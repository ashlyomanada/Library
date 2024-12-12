using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Library
{
    internal class bookAdd
    {
        private SqlConnection con;
        
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }


        public List<bookAdd> addBooks()
        {
            con = Dbcon.GetConnection();
            List<bookAdd> listData = new List<bookAdd>();
            if (con.State != ConnectionState.Open)
            {
                try
                {

                    con.Open();
                    
                    string select = "SELECT * FROM books WHERE date_delete IS NULL";
                    using (SqlCommand CMD = new SqlCommand(select, con))
                    {
                        SqlDataReader reader =CMD.ExecuteReader();
                        bookAdd bookAdd = new bookAdd();

                        while (reader.Read()) {
                            bookAdd.ID=(int)reader["Id"];
                            bookAdd.Title = reader["Title"].ToString();
                            bookAdd.Author = reader["Author"].ToString();
                            bookAdd.Genre = reader["Genre"].ToString();
                            bookAdd.Status = reader["Status"].ToString();
                            listData.Add(bookAdd);
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
            return listData;
        }
    }
}
