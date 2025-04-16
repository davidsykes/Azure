
namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccessImplementation
    {
        List<string> GetTableNames();
        void CreateFoodsTable();
        void AddNewFood(string name);
        List<string> GetTestData();
    }
}
