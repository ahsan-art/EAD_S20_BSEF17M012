using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using MySql.Data.MySqlClient;

namespace DAL
{
    internal class DBConnection:IDisposable
    {
        String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        MySqlConnection conn = null;

         
        public DBConnection()
        {
            conn= new MySqlConnection(connectionString);
            conn.Open();
        }


        public int ExcueteQuery(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            int res = command.ExecuteNonQuery();
            return res;
        }


        public Object ExcueteScalar(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            return command.ExecuteScalar();
        }

        public MySqlDataReader ExcueteReader(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

}
