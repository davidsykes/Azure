using DatabaseAccess;
using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseAccess _databaseAccess;
        public string? ErrorMessage;
        public List<FoodItem> FoodItems;
        public List<Supermarket> Supermarkets;
        public FoodBeingEdited? FoodBeingEdited;

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

            FoodItems = _databaseAccess.GetFoodItems();
            Supermarkets = _databaseAccess.GetSupermarkets();
        }

        private static bool IsRunningOnAzure => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
        //private static bool IsRunningOnAzure => true;



        public IActionResult OnPost()
        {
            var edit_command = Request.Form["edit_command"];
            if (edit_command == "new_food")
            {
                _databaseAccess.AddNewFood(InputText);
                FoodItems = _databaseAccess.GetFoodItems();
                return Redirect("/");
            }
            else if (edit_command == "new_supermarket")
            {
                _databaseAccess.AddNewSupermarket(InputText);
                Supermarkets = _databaseAccess.GetSupermarkets();
                return Redirect("/");
            }

            CreateNewFood = true;
            Result = InputText;
            return Page();
        }

        public IActionResult OnGetDoSomething(int id)
        {
            SetFoodBeingEdited(id);
            return Page();
        }

        void SetFoodBeingEdited(int id)
        {
            FoodBeingEdited = new FoodBeingEdited(id, _databaseAccess);
        }

        // TOREMOVE




        [BindProperty]
        public List<string> SelectedItems { get; set; } = [];

        public List<SelectListItem> ItemList { get; set; } = [];








        public bool CreateNewFood { get; set; }
        [BindProperty]
        public string InputText { get; set; }

        public string Result { get; set; }

        public void OnGet()
        {

            var dbEntries = _databaseAccess.GetTestData();
        }
    }
}
