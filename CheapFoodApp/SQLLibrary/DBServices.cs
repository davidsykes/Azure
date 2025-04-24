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
    }
}
