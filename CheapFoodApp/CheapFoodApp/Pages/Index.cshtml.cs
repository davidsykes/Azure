using DatabaseAccess;
using DatabaseAccessInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseAccess _databaseAccess;
        public string ErrorMessage;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            try
            {
                _databaseAccess = new DatabaseAccessWrapper(IsRunningOnAzure);
            }
            catch (SQLiteLibraryException ex)
            {
                ErrorMessage = ex.Message;
                _databaseAccess = new DummyDatabaseAccessWrapper();
            }
        }

        private static bool IsRunningOnAzure => true; // !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));





        // TOREMOVE


        [BindProperty]
        public string SelectedFruit { get; set; } = "";
        public List<SelectListItem> FruitOptions { get; set; } = [];
        public bool CreateNewFood { get; set; }
        [BindProperty]
        public string InputText { get; set; }

        public string Result { get; set; }
        public bool isRunningOnAzure { get; set; }

        public void OnGet()
        {
            Console.WriteLine("Here");
            isRunningOnAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));

                FruitOptions =
                [
                    new SelectListItem { Value = "apple", Text = "Apple" },
                    new SelectListItem { Value = "banana", Text = "Banana" },
                    new SelectListItem { Value = "cherry", Text = "Cherry" }
                ];

                var dbEntries = _databaseAccess.GetTestData();

                dbEntries.ForEach(m =>
                            FruitOptions.Add(
                            new SelectListItem
                            {
                                Value = m,
                                Text = m
                            }));
        }
        public void OnPost()
        {
            var qq = Request.Form["new food"];
                if (qq == "new_food")
                {
                    _databaseAccess.AddNewFood(InputText);
            }

                CreateNewFood = true;
            Result = InputText;
        }
    }
}
