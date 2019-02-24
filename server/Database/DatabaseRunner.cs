using System.Data.SqlClient;
using System.Dynamic;

namespace Eze.AdminConsole.Database
{
    public class DatabaseRunner
    {
        static private string GetConnectionString()
        {
            return "Data Source=marston9020b.ezesoft.net;Initial Catalog=TC;"
                + "User Id=sa;Password=ezetc;";
        }
        public string RunDiagnostics()
        {
            string queryString = "exec sp_Blitz";
            string sqlResult = "";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader[3].ToString() == "TC")
                        {
                            sqlResult += (string)reader[5] + '\n';
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return sqlResult;
        }
        public dynamic WhatsRunning()
        {
            string queryString = "exec sp_BlitzWho";
            dynamic sqlResult = new ExpandoObject();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader[3].ToString() == "TC")
                        {
                            sqlResult.loginName = reader[16].ToString();
                            sqlResult.programName = reader[18].ToString();
                            sqlResult.queryText = reader[4].ToString();
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return sqlResult;
        }
    }
}