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
            if (!TableExists("Foods"))
            {
                CreateAzureFoodsTable();
            }
            if (!TableExists("Supermarkets"))
            {
                CreateAzureSupermarketsTable();
            }
            if (!TableExists("Prices"))
            {
                CreateAzurePricesTable();
            }
        }

        private bool TableExists(string name)
        {
            var results = _databaseConnection.Select<FoodItem>(
                query: $"SELECT Name FROM sysobjects  WHERE xtype='u' AND name='{name}'");
            return results.Any();
        }

        public void CreateAzureFoodsTable()
        {
            string tableCommand =
@"CREATE TABLE Foods (Id BIGINT PRIMARY KEY IDENTITY(1,1),NAME CHAR(200) NOT NULL);";

            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                =>
            {
                tr.ExecuteNonQuery(tableCommand);
            });
        }

        public void CreateAzureSupermarketsTable()
        {
            string tableCommand =
@"CREATE TABLE Supermarkets (Id BIGINT PRIMARY KEY IDENTITY(1,1),NAME VARCHAR(MAX) NOT NULL);";

            _databaseConnection.RunInTransaction((IDatabaseTransaction tr)
                =>
            {
                tr.ExecuteNonQuery(tableCommand);
            });
        }

        public void CreateAzurePricesTable()
        {
            string tableCommand =
@"CREATE TABLE Prices (ShopId BIGINT,FoodId BIGINT, Quantity float, Price float, PRIMARY KEY(ShopId,FoodId));";

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
                {
                    transaction.DeleteRow(pricing);
                    transaction.UpdateRow(pricing);
                });
        }

        public IList<ProductPrice> GetFoodPrices(int foodId)
        {
            return _databaseConnection.Select<ProductPrice>(where: $"FoodId={foodId}");
        }
    }
}