using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
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
                _databaseAccess.CreateFoodsTable();
            }

            if (!TableExists("Supermarkets"))
            {
                _databaseAccess.CreateSupermarketsTable();
            }

        }

        public bool TableExists(string name)
        {
            var tables = _databaseAccess.GetTableNames();
            return tables.Contains(name);
        }
        public void AddNewFood(string name)
        {
            _databaseAccess.AddNewFood(new DatabaseString(name));
        }

        public List<FoodItem> GetFoodItems()
        {
            var query = "SELECT Id, Name FROM FOODS";
            return _databaseAccess.Query<FoodItem>(query);
        }

        public void AddNewSupermarket(string name)
        {
            _databaseAccess.AddNewSupermarket(new DatabaseString(name));
        }

        public List<Supermarket> GetSupermarkets()
        {
            var query = "SELECT Id, Name FROM SUPERMARKETS";
            return _databaseAccess.Query<Supermarket>(query);
        }

        public List<string> GetTestData()
        {
            return _databaseAccess.GetTestData();
        }
    }
}
