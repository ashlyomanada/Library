using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Library
{
    internal class Dbcon
    {
        public static SqlConnection GetConnection()
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Downloads\\Library-main\\Library-main\\Library-main\\Library\\Library\\Database.mdf;Integrated Security=True";
            return new SqlConnection(con);
        }
    }
}
