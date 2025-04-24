using SQLiteLibrary;
using SQLiteLibrary.SQLite;
using SQLLibrary.SQLite;
using SQLLibrary.TableAnalysis;

namespace SQLLibrary.CRUD
{
    internal class DatabaseConnection : IDatabaseConnection
    {
        private readonly ISQLiteWrapper _sqLiteWrapper;
        private readonly ITableAnalyser _tableAnalyser;
        private readonly IDatabaseTransactionFactory _databaseTransactionFactory;

        public DatabaseConnection(
            ISQLiteWrapper sqLiteWrapper,
            ITableAnalyser tableAnalyser,
            IDatabaseTransactionFactory databaseTransactionFactory)
        {
            _sqLiteWrapper = sqLiteWrapper;
            _tableAnalyser = tableAnalyser;
            _databaseTransactionFactory = databaseTransactionFactory;
        }

        public IList<T> Select<T>(string? query = null, string? filter = null, string? endWith = null, object? parameters = null) where T : new()
        {
            if (query is null)
            {
                query = GetSelectQueryFromTheClass<T>();
            }

            if (endWith is not null)
            {
                query = query + " " + endWith;
            }

            return _sqLiteWrapper.Select<T>(null, query, filter, parameters);
        }

        private string GetSelectQueryFromTheClass<T>()
        {
            return _tableAnalyser.AnalyseTable<T>().SelectQuery;
        }

        public void RunInTransaction(Action<IDatabaseTransaction> action)
        {
            var transaction = _databaseTransactionFactory.CreateTransaction(_sqLiteWrapper);

            try
            {
                action(transaction);
                if (transaction.RollbackTransaction)
                {
                    (transaction as ICommittable)?.Rollback();
                }
                else
                {
                    (transaction as ICommittable)?.Commit();
                }
            }
            catch (Exception)
            {
                (transaction as ICommittable)?.Rollback();
                throw;
            }
        }

        public void RunInTransaction<TParam>(
            Action<IDatabaseTransaction, TParam> action,
            TParam parameters)
        {
            RunInTransaction((transaction)
                => action(transaction, parameters));
        }

        public void AddLogger(LogSQLiteCommandDelegate logger)
        {
            _sqLiteWrapper.LogSQLiteCommandEvent += logger;
        }
    }
}
