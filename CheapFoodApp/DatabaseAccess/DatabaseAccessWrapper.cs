using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
using SQLDatabaseAccess;
using SQLiteDatabaseAccess;
using SQLiteLibrary;
using SQLLibrary;
using SQLLibraryInterface;

namespace DatabaseAccess
{
    public class DatabaseAccessWrapper : IDatabaseAccess
    {
        readonly IOldDatabaseAccessImplementation _oldDatabaseAccessImplementation;
        readonly IActualDatabaseConnection _newDatabaseAccess;
        readonly IDatabaseConnection _databaseConnection;

        public DatabaseAccessWrapper(bool IsRunningOnAzure)
        {
            _oldDatabaseAccessImplementation = IsRunningOnAzure ? new AzureDatabaseAccess() : new TestDatabaseAccess();
            _newDatabaseAccess = IsRunningOnAzure ? new SQLDatabaseConnection() : new SQLLiteDatabaseConnection();
            var dbServices = new DBServices();
            _databaseConnection = dbServices.OpenConnection(_newDatabaseAccess);

            if (!TableExists("Foods"))
            {
                _oldDatabaseAccessImplementation.CreateFoodsTable();
            }

            if (!TableExists("Supermarkets"))
            {
                _oldDatabaseAccessImplementation.CreateSupermarketsTable();
            }

        }

        public bool TableExists(string name)
        {
            var tables = _oldDatabaseAccessImplementation.GetTableNames();
            return tables.Contains(name);
        }
        public void AddNewFood(string name)
        {
            _oldDatabaseAccessImplementation.AddNewFood(new DatabaseString(name));
        }

        public List<FoodItem> GetFoodItems()
        {
            var query = "SELECT Id, Name FROM FOODS";
            return _oldDatabaseAccessImplementation.Query<FoodItem>(query);
        }

        public FoodItem GetFoodItem(int id)
        {
            var query = $"SELECT Id, Name FROM FOODS WHERE Id={id}";
            return _oldDatabaseAccessImplementation.Query<FoodItem>(query).First();
        }

        public void AddNewSupermarket(string name)
        {
            _oldDatabaseAccessImplementation.AddNewSupermarket(new DatabaseString(name));
        }

        public List<Supermarket> GetSupermarkets()
        {
            var query = "SELECT Id, Name FROM SUPERMARKETS";
            return _oldDatabaseAccessImplementation.Query<Supermarket>(query);
        }

        public void AddPrice(int foodId, int supermarketId, double quantity, double price)
        {
            var pricing = new ProductPrice();

            _databaseConnection.RunInTransaction(
                (IDatabaseTransaction transaction) =>
                transaction.InsertRow(pricing));
        }
    }
}