using DatabaseAccessInterfaces;

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

        class FoodStuff
        {
            public string? Name { get; set; }
        }

        public List<string> GetFoodItems()
        {
            var foodItems = _wrapper.Select<FoodStuff>(null, "SELECT NAME FROM FOODS");

            return [.. foodItems.Select(f => f.Name!)];
        }

        public List<string> GetTestData()
        {
            return ["Testing", "The", "Database"];
        }
    }
}
