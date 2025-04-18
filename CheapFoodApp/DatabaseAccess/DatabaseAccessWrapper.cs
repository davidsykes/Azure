using DatabaseAccessInterfaces;
using SQLDatabaseAccess;
using SQLiteDatabaseAccess;

namespace DatabaseAccess
{
    public class DatabaseAccessWrapper : IDatabaseAccess
    {
        readonly IDatabaseAccessImplementation _databaseAccess;

        public DatabaseAccessWrapper(bool IsRunningOnAzure)
        {
            _databaseAccess = IsRunningOnAzure ? new AzureDatabaseAccess() : new TestDatabaseAccess();

            if (!TableExists("Foods"))
            {
                CreateFoodsTable();
            }

        }

        public bool TableExists(string name)
        {
            var tables = _databaseAccess.GetTableNames();
            return tables.Contains(name);
        }

        public void CreateFoodsTable()
        {
            _databaseAccess.CreateFoodsTable();
        }

        public void AddNewFood(string name)
        {
            _databaseAccess.AddNewFood(new DatabaseString(name));
        }

        public List<FoodItem> GetFoodItems()
        {
            return _databaseAccess.GetFoodItems();
        }

        public List<string> GetTestData()
        {
            return _databaseAccess.GetTestData();
        }
    }
}
