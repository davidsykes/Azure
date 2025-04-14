using Microsoft.Data.SqlClient;

namespace DatabaseAccess
{
    public class AzureDatabaseAccess
    {
        public List<string> GetTestData()
        {
            var rows = new List<string>();
            try
            {
                string connectionString = "Server=tcp:cheapfooddbserver.database.windows.net,1433;Initial Catalog=CheapFoodDb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

                using var conn = new SqlConnection(connectionString);
                conn.Open();

                var command = new SqlCommand("SELECT * FROM Persons", conn);
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
                    }
                }
            }
            catch (Exception ex)
            {
                rows.Add("error " + ex.Message );
            }

            return rows;
        }
    }
}
