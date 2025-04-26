using DatabaseAccess.DatabaseObjects;

namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        void AddNewFood(string name);
        IList<FoodItem> GetFoodItems();
        FoodItem GetFoodItem(int id);
        void AddNewSupermarket(string name);
        IList<Supermarket> GetSupermarkets();
        void AddPrice(int foodId, int supermarketId, double quantity, double price);
        IList<ProductPrice> GetFoodPrices(int id);
    }
}
