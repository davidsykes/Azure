using SQLLibrary.CRUD;
using SQLLibrary.SQLite;
using SQLLibrary.TableAnalysis;
using SQLLibraryInterface;

namespace SQLLibrary
{
    public class DBServices
    {
        private readonly IDatabaseTransactionFactory _databaseTransactionFactory;
        private readonly ITableAnalyser _tableAnalyser;

        public DBServices()
        {
            _tableAnalyser = new TableAnalyserCache(new TableAnalyser());
            _databaseTransactionFactory = new DatabaseTransactionFactory(_tableAnalyser);
        }

        public IDatabaseConnection OpenConnection(IActualDatabaseConnection sqliteConnection)
        {
            return new DatabaseConnection(
                new SQLiteWrapper(sqliteConnection),
                _tableAnalyser,
                _databaseTransactionFactory);
        }

        //public IDatabaseConnection OpenFileConnection(string databasePath)
        //{
        //    return new DatabaseConnection(
        //        new SQLiteWrapper(CreateFileConnectionString(databasePath)),
        //        _tableAnalyser,
        //        _databaseTransactionFactory);
        //}

        //public IDatabaseConnection OpenMemoryDatabase()
        //{
        //    return new DatabaseConnection(new SQLiteWrapper(CreateMemoryConnectionString()),
        //        _tableAnalyser,
        //        _databaseTransactionFactory);
        //}

        //private static string CreateFileConnectionString(string databasePath)
        //{
        //    return $"Data Source={databasePath}";
        //}

        //private static string CreateMemoryConnectionString()
        //{
        //    return "Data Source=:memory:;";
        //}
    }
}
