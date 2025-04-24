using SQLiteLibrary;
using SQLLibrary.SQLite;
using SQLLibrary.TableAnalysis;

namespace SQLLibrary.CRUD
{
    internal class DatabaseTransactionFactory : IDatabaseTransactionFactory
    {
        readonly ITableAnalyser _tableAnalyser;

        public DatabaseTransactionFactory(ITableAnalyser tableAnalyser)
        {
            _tableAnalyser = tableAnalyser;
        }

        public IDatabaseTransaction CreateTransaction(ISQLiteWrapper sqLiteWrapper)
        {
            return new DatabaseTransaction(sqLiteWrapper, _tableAnalyser);
        }
    }
}
