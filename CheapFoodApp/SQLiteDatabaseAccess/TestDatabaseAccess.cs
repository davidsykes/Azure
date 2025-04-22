using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.Commands;

namespace SQLiteDatabaseAccess
{
    class Table
    {
        public string name { get; set; } = "";
    }

    public class TestDatabaseAccess : IDatabaseAccessImplementation
    {
        private readonly ISQLiteWrapper _wrapper;

        public TestDatabaseAccess()
        {
            string databasePath = "D:\\TestData\\CheapFood.sql";

            _wrapper = new SQLiteWrapper(CreateFileConnectionString(databasePath));
        }

        private static string CreateFileConnectionString(string databasePath)
        {
            return $"Data Source={databasePath}";
        }

        public List<string> GetTableNames()
        {
            var tables = _wrapper.Select<Table>(null, "SELECT name FROM sqlite_master WHERE type='table';");
            var names = tables.Select(m => m.name);
            return [.. names];
        }

        public void CreateFoodsTable()
        {
            var t = _wrapper.CreateTransaction();
            _wrapper.ExecuteNonQuery(
                t,
                "CREATE TABLE IF NOT EXISTS Foods(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);");
            _wrapper.Commit(t);
        }

        public void CreateSupermarketsTable()
        {
            var t = _wrapper.CreateTransaction();
            _wrapper.ExecuteNonQuery(
                t,
                "CREATE TABLE IF NOT EXISTS Supermarkets(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);");
            _wrapper.Commit(t);
        }

        public void AddNewFood(DatabaseString name)
        {
            var t = _wrapper.CreateTransaction();

            var query = $"INSERT INTO Foods(Name) VALUES(@Name)";
            _wrapper.ExecuteNonQuery(
                t,
                query,
                new { Name = name.ToString()});
            _wrapper.Commit(t);
        }

        public List<T> Query<T>(string query) where T : new()
        {
            var foodItems = _wrapper.Select<T>(null, query);

            return foodItems;
        }

        public void AddNewSupermarket(DatabaseString name)
        {
            var t = _wrapper.CreateTransaction();

            var query = $"INSERT INTO Supermarkets(Name) VALUES(@Name)";
            _wrapper.ExecuteNonQuery(
                t,
                query,
                new { Name = name.ToString() });
            _wrapper.Commit(t);
        }

        public void ExecuteCommand(DatabaseCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
