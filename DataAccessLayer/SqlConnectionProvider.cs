using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class SqlConnectionProvider {
        private static string connectionString = @"Data Source=localhost;Initial Catalog=dumb_scrum_db;Integrated Security=True";

        public static SqlConnection GetConnection() {
            return new SqlConnection(connectionString);
        }
    }
}
