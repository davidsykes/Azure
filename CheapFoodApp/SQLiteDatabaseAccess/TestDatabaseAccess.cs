using DatabaseAccessInterfaces;

namespace SQLiteDatabaseAccess
{
    public class TestDatabaseAccess : IDatabaseAccess
    {
        public void AddNewFood(string inputText)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(string v)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTestData()
        {
            return ["Testing", "The", "Database"];
        }

        public bool TableExists(string v)
        {
            throw new NotImplementedException();
        }
    }
}
