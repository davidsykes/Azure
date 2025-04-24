using DatabaseAccess;
using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLiteLibrary;

namespace CheapFoodApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseAccess _databaseAccess;
        public string? ErrorMessage;
        public IList<FoodItem> FoodItems;
        public IList<Supermarket> Supermarkets;
        public FoodBeingEdited? FoodBeingEdited;

        [BindProperty]
        public string InputText { get; set; } = "";

        [BindProperty]
        public string SelectedAddPriceSupermarket { get; set; } = "";
        [BindProperty]
        public string AddPriceQuantity { get; set; } = "";
        [BindProperty]
        public string AddPricePrice { get; set; } = "";
        [BindProperty]
        public string AddPriceId { get; set; } = "";

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

        public IActionResult OnPostAddFoodPrice()
        {
            _databaseAccess.AddPrice(
                StringToInt(AddPriceId),
                StringToInt(SelectedAddPriceSupermarket),
                StringToDouble(AddPriceQuantity),
                StringToDouble(AddPricePrice));
            return RedirectToPage("Index"); // Example
        }

        static int StringToInt(string s)
        {
            return int.Parse(s);
        }

        static double StringToDouble(string s)
        {
            return double.Parse(s);
        }




        public void OnGet()
        {

        }
    }
}
