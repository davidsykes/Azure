using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string SelectedFruit { get; set; } = "";
        public List<SelectListItem> FruitOptions { get; set; } = [];
        public bool CreateNewFood { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Here");

            {
                FruitOptions =
                [
                    new SelectListItem { Value = "apple", Text = "Apple" },
                    new SelectListItem { Value = "banana", Text = "Banana" },
                    new SelectListItem { Value = "cherry", Text = "Cherry" }
                ];


                try
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
                }
                catch(Exception ex)
                {
                    FruitOptions.Add(new SelectListItem { Value = "error", Text = ex.Message });
                }
            }
        }
        public void OnPost()
        {
            CreateNewFood = true;
        }
    }
}
