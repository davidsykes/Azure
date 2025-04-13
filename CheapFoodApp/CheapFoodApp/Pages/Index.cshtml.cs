using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Here");

            {
                string connectionString = "Server=tcp:cheapfooddbserver.database.windows.net,1433;Initial Catalog=CheapFoodDb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
                var rows = new List<string>();

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

                Console.WriteLine(rows.Count);
            }
        }
    }
}
