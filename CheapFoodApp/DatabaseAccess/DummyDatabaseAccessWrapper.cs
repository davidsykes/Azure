using DatabaseAccess.DatabaseObjects;
using DatabaseAccessInterfaces;

namespace DatabaseAccess
{
    public class DummyDatabaseAccessWrapper : IDatabaseAccess
    {
        public void AddNewFood(string inputText)
        {
        }

        public void AddNewSupermarket(string name)
        {
        }

        public IList<FoodItem> GetFoodItems()
        {
            return [.. GetTestData().Select(m => new FoodItem { Id = 1, Name = m })];
        }

        public FoodItem GetFoodItem(int id)
        {
            return new FoodItem { Id = 1, Name = "Food" };
        }

        public IList<Supermarket> GetSupermarkets()
        {
            return [.. GetTestData().Select(m => new Supermarket { Id = 1, Name = m })];
        }

        public static IList<string> GetTestData()
        {
            return ["Dummy", "data"];
        }

        public static bool TableExists(string _) => true;

        public void AddPrice(int foodId, int supermarketId, double quantity, double price)
        {
        }

        public IList<ProductPrice> GetFoodPrices(int id)
        {
            return [.. GetTestData().Select(m => new ProductPrice
            {
                ShopId = 1,
                FoodId = 1,
                Price = 1,
                Quantity = 1
            })];
        }
    }
}
