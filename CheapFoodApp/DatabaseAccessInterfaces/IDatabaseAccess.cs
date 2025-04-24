using DatabaseAccessInterfaces.DatabaseObjects;

namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        //bool TableExists(string name);
        void AddNewFood(string name);
        IList<FoodItem> GetFoodItems();
        FoodItem GetFoodItem(int id);
        void AddNewSupermarket(string name);
        IList<Supermarket> GetSupermarkets();
        void AddPrice(int foodId, int supermarketId, double quantity, double price);
    }
}
