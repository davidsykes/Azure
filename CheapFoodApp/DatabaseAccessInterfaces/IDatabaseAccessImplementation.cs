using DatabaseAccessInterfaces.DatabaseObjects;

namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccessImplementation
    {
        List<string> GetTableNames();
        void CreateFoodsTable();
        void AddNewFood(DatabaseString name);
        List<T> Query<T>(string query) where T : new();
        void AddNewSupermarket(DatabaseString databaseString);

        List<string> GetTestData();
    }
}
