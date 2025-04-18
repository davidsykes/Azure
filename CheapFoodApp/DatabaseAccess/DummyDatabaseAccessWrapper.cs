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

        public void CreateFoodsTable()
        {
        }

        public List<FoodItem> GetFoodItems()
        {
            return [.. GetTestData().Select(m => new FoodItem { Id = 1, Name = m })];
        }

        public List<Supermarket> GetSupermarkets()
        {
            return [.. GetTestData().Select(m => new Supermarket { Id = 1, Name = m })];
        }

        public List<string> GetTestData()
        {
            return ["Dummy", "data"];
        }

        public bool TableExists(string name)
        {
            return true;
        }
    }
}
