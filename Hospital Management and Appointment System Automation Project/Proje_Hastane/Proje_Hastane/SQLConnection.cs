using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class SQLConnection
    {
        public SqlConnection connect()
        {
            SqlConnection connect = new SqlConnection("Data Source=ALICAN\\SQLEXPRESS;Initial Catalog=HospitalProject;Integrated Security=True");
            connect.Open();
            return connect;
        }
    }
}
