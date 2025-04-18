namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccessImplementation
    {
        List<string> GetTableNames();
        void CreateFoodsTable();
        void AddNewFood(DatabaseString name);
        List<FoodItem> GetFoodItems();
        List<string> GetTestData();
    }
}
