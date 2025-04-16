
namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccessImplementation
    {
        List<string> GetTableNames();
        void CreateFoodsTable();
        void AddNewFood(DatabaseString name);
        List<string> GetFoodItems();
        List<string> GetTestData();
    }
}
