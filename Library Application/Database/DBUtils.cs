using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Database
{
    internal class DBUtils
    {
        // public
        public static SqlConnection Connection
        {
            get => new SqlConnection(connectionString);
        }

        public static bool doesAccountExists(string Email, string Phone)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesAccountExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                conn.Open();
                count = (int) cmd.ExecuteScalar();
                
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        // private
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString;
    }
}
