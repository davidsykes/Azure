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
                CreateFoodsTable();
            }

        }

        public void CreateFoodsTable()
        {
            _databaseAccess.CreateFoodsTable();
        }

        public void AddNewFood(string inputText)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTestData()
        {
            return _databaseAccess.GetTestData();
        }

        public bool TableExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}
