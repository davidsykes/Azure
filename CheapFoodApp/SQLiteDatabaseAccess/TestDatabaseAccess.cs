using DatabaseAccessInterfaces;

namespace SQLiteDatabaseAccess
{
    class Table
    {
        public string name { get; set; } = "";
    }

    public class TestDatabaseAccess : IDatabaseAccess
    {
        readonly ISQLiteWrapper _wrapper;

        public TestDatabaseAccess()
        {
            string databasePath = "D:\\TestData\\CheapFood.sql";

            _wrapper = new SQLiteWrapper(CreateFileConnectionString(databasePath));
        }

        private static string CreateFileConnectionString(string databasePath)
        {
            return $"Data Source={databasePath}";
        }

        public bool TableExists(string name)
        {
            var tables = _wrapper.Select<Table>(null, "SELECT name FROM sqlite_master WHERE type='table';");
            var names = tables.Select(m => m.name);
            return names.Contains(name);
        }

        public void CreateFoodsTable()
        {
            var t = _wrapper.CreateTransaction();
            _wrapper.ExecuteNonQuery(
                t,
                "CREATE TABLE IF NOT EXISTS Foods(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);");
            _wrapper.Commit(t);
        }

        public void AddNewFood(string inputText)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTestData()
        {
            return ["Testing", "The", "Database"];
        }
    }
}
