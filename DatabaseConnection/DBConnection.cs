using DatabaseConnection.Models;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class DBConnection
    {
        private readonly string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sebastian.ziolkowski\LibrarySQL.mdf;Integrated Security=True;Connect Timeout=30";
        protected SqlConnection conn;
        protected SqlDataAdapter adapter;


        public DBConnection()
        {
            conn = new SqlConnection(constr);

            try
            {
                conn.Open();
                adapter = new SqlDataAdapter();
            }
            finally
            {
                conn.Close();
            }
        }

        protected void openDBConnection()
        {
            conn.Open();
        }

        protected void openDBConnectionIfNotOpen()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        protected void closeDBConnection()
        {
            conn.Close();
        }


    }
}