using DatabaseAccess.Commands;
using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
using DatabaseAccessInterfaces.DatabaseTableValues;
using SQLDatabaseAccess;
using SQLiteDatabaseAccess;

namespace DatabaseAccess
{
    public class DatabaseAccessWrapper : IDatabaseAccess
    {
        readonly IDatabaseAccessImplementation _databaseAccess;
        readonly IDatabaseCommandMaker _databaseTableInserter;

        public DatabaseAccessWrapper(bool IsRunningOnAzure)
        {
            _databaseAccess = IsRunningOnAzure ? new AzureDatabaseAccess() : new TestDatabaseAccess();
            _databaseTableInserter = new DatabaseCommandMaker();

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

        public FoodItem GetFoodItem(int id)
        {
            var query = $"SELECT Id, Name FROM FOODS WHERE Id={id}";
            return _databaseAccess.Query<FoodItem>(query).First();
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

        public void AddPrice(int foodId, int supermarketId, double quantity, double price)
        {
            var insertCommand = _databaseTableInserter.MakeInsertCommand("FoodPrices", [
                new DatabaseTableIntValue("FoodId", foodId),
                new DatabaseTableIntValue("SupermarketId", supermarketId),
                new DatabaseTableDoubleValue("Quantity", quantity),
                new DatabaseTableDoubleValue("Price", price),
                ]);
            _databaseAccess.ExecuteCommand(insertCommand);
        }
    }
}
