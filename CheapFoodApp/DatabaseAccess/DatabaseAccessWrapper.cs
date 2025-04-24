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
        readonly IActualDatabaseConnection _actualDatabaseConnection;
        readonly IDatabaseConnection _databaseConnection;

        public DatabaseAccessWrapper(bool IsRunningOnAzure)
        {
            string sqlDatabasePath = "D:\\TestData\\CheapFood.sql";
            _actualDatabaseConnection = IsRunningOnAzure ? new SQLDatabaseConnection() : new SQLLiteDatabaseConnection(sqlDatabasePath);
            var dbServices = new DBServices();
            _databaseConnection = dbServices.OpenConnection(_actualDatabaseConnection);
        }

        public void AddNewFood(string name)
        {
            var food = new FoodItem { Name = name };
            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                => tr.InsertRow(food));
        }

        public IList<FoodItem> GetFoodItems()
        {
            //var query = "SELECT Id, Name FROM FOODS";
            //return _oldDatabaseAccessImplementation.Query<FoodItem>(query);
            return _databaseConnection.Select<FoodItem>();
        }

        public FoodItem GetFoodItem(int id)
        {
            return _databaseConnection.Select<FoodItem>().Single(m => m.Id == id);
        }

        public void AddNewSupermarket(string name)
        {
            throw new NotImplementedException();
            //_oldDatabaseAccessImplementation.AddNewSupermarket(new DatabaseString(name));
        }

        public IList<Supermarket> GetSupermarkets()
        {
            //var query = "SELECT Id, Name FROM SUPERMARKETS";
            return _databaseConnection.Select<Supermarket>();
            //return _oldDatabaseAccessImplementation.Query<Supermarket>(query);
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