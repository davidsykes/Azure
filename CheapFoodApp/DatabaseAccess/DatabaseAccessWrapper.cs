using DatabaseAccess.DatabaseObjects;
using DatabaseAccessInterfaces;
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

            if (IsRunningOnAzure)
            {
                CreateAzureTables();
            }
            else
            {
                CreateSqlLiteTables();
            }
        }

        private void CreateSqlLiteTables()
        {
            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                =>
            {
                tr.CreateTable<FoodItem>();
                tr.CreateTable<Supermarket>();
                tr.CreateTable<ProductPrice>();
            });
        }

        private void CreateAzureTables()
        {
            //AUTO_INCREMENT
            CreateAzurePricesTable();
        }


        public void CreateAzurePricesTable()
        {
            string tableCommand = @"CREATE TABLE Prices (ShopId INT PRIMARY KEY,FoodId INT PRIMARY KEY, Quantity MONEY, Price MONEY);";


            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                =>
            {
                tr.ExecuteNonQuery(tableCommand);
            });
        }


        public void AddNewFood(string name)
        {
            var food = new FoodItem { Name = name };
            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                => tr.InsertRow(food));
        }

        public IList<FoodItem> GetFoodItems()
        {
            return _databaseConnection.Select<FoodItem>();
        }

        public FoodItem GetFoodItem(int id)
        {
            return _databaseConnection.Select<FoodItem>().Single(m => m.Id == id);
        }

        public void AddNewSupermarket(string name)
        {
            var supermarket = new Supermarket
            {
                Name = name
            };
            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                => tr.InsertRow(supermarket));
        }

        public IList<Supermarket> GetSupermarkets()
        {
            return _databaseConnection.Select<Supermarket>();
        }

        public void AddPrice(int foodId, int supermarketId, double quantity, double price)
        {
            var pricing = new ProductPrice
            {
                ShopId = supermarketId,
                FoodId = foodId,
                Quantity = quantity,
                Price = price
            };

            _databaseConnection.RunInTransaction(
                (IDatabaseTransaction transaction) =>
                transaction.UpdateRow(pricing));
        }

        public IList<ProductPrice> GetFoodPrices(int foodId)
        {
            return _databaseConnection.Select<ProductPrice>(where: $"FoodId={foodId}");
        }
    }
}