using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Helpers
{
    public class SqlHelper
    {
        private IDbConnection Connection { get; set; }
        public ConnectionState State => this.Connection.State;

        //SQLHelper Constructor with Connection String
        public SqlHelper(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
        }

        public void Open() { this.Connection.Open(); }

        public void Close() { this.Connection.Close(); }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SchoolManagementSystem.Properties.Settings.dejongDBConnectionString"].ConnectionString;
        }
    }
}
