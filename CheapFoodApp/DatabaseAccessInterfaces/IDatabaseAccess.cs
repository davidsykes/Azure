using DatabaseAccessInterfaces.DatabaseObjects;

namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        bool TableExists(string name);
        void AddNewFood(string name);
        List<FoodItem> GetFoodItems();
        FoodItem GetFoodItem(int id);
        void AddNewSupermarket(string name);
        List<Supermarket> GetSupermarkets();


        public List<string> GetTestData();
    }
}
