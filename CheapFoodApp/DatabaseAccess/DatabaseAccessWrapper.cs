using DatabaseAccessInterfaces;
using SQLDatabaseAccess;
using SQLiteDatabaseAccess;

namespace DatabaseAccess
{
    public class DatabaseAccessWrapper : IDatabaseAccess
    {
        readonly IDatabaseAccess _databaseAccess;

        public DatabaseAccessWrapper(bool IsRunningOnAzure)
        {
            _databaseAccess = IsRunningOnAzure ? new AzureDatabaseAccess() : new TestDatabaseAccess();

            if (!_databaseAccess.TableExists("Foods"))
            {
                _databaseAccess.CreateTable("Foods");
            }

        }

        public void AddNewFood(string inputText)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTestData()
        {
            throw new NotImplementedException();
        }

        public bool TableExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}
