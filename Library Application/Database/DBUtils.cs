using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Database
{
    internal class DBUtils
    {
        // public
        public static SqlConnection Connection
        {
            get => new(connectionString);
        }

        // private
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString;
    }
}
