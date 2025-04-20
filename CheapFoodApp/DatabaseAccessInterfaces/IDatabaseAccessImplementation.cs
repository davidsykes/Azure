using DatabaseAccessInterfaces.DatabaseObjects;

namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccessImplementation
    {
        List<string> GetTableNames();
        void CreateFoodsTable();
        void AddNewFood(DatabaseString name);
        void CreateSupermarketsTable();
        void AddNewSupermarket(DatabaseString databaseString);
        List<T> Query<T>(string query) where T : new();

        List<string> GetTestData();
    }
}
