using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;

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

        public IList<string> GetTestData()
        {
            return ["Dummy", "data"];
        }

        public bool TableExists(string name)
        {
            return true;
        }

        public void AddPrice(int foodId, int supermarketId, double quantity, double price)
        {
        }
    }
}
