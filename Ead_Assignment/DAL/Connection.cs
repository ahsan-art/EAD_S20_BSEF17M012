using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Connection:IDisposable
    {
        String connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Assignment2;User ID=sa;Password=12345";
        SqlConnection conn = null;


        public Connection()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        public Object ExcueteScalar(String Query)
        {
            SqlCommand command = new SqlCommand(Query, conn);
            return command.ExecuteScalar();
        }

        public int ExcueteQuery(String Query)
        {
            SqlCommand command = new SqlCommand(Query, conn);
            int res = command.ExecuteNonQuery();
            return res;
        }


        public SqlDataReader ExcueteReader(String Query)
        {
            SqlCommand command = new SqlCommand(Query, conn);
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            if (conn!= null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }
}
