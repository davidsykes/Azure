using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AzureDatabaseAccess AzureDatabaseAccess;

        [BindProperty]
        public string SelectedFruit { get; set; } = "";
        public List<SelectListItem> FruitOptions { get; set; } = [];
        public bool CreateNewFood { get; set; }
        [BindProperty]
        public string InputText { get; set; }

        public string Result { get; set; }
        public bool isRunningOnAzure { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            AzureDatabaseAccess = new AzureDatabaseAccess();
        }

        public void OnGet()
        {
            Console.WriteLine("Here");
            isRunningOnAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));


            {
                FruitOptions =
                [
                    new SelectListItem { Value = "apple", Text = "Apple" },
                    new SelectListItem { Value = "banana", Text = "Banana" },
                    new SelectListItem { Value = "cherry", Text = "Cherry" }
                ];

                var dbEntries = AzureDatabaseAccess.GetTestData();

                dbEntries.ForEach(m => 
                FruitOptions.Add(
    new SelectListItem
    {
        Value = m,
        Text = m
    }));


            }
        }
        public void OnPost()
        {
            CreateNewFood = true;
            Result = InputText;
        }
    }
}
