using SQLiteLibrary;
using SQLLibraryInterface.ToBeImplemented;

namespace SQLLibrary.SQLite
{
    internal interface ISQLiteWrapper
    {
        ISQLiteTransactionWrapper CreateTransaction();
        List<T> Select<T>(
            ISQLiteTransactionWrapper? transaction,
            string query,
            string? where = null,
            object? parameters = null) where T : new();
        int ExecuteNonQuery(ISQLiteTransactionWrapper transaction, string command, object? parameters = null);
        long ExecuteScalar(ISQLiteTransactionWrapper transaction, string command, object? parameters = null);
        void Commit(ISQLiteTransactionWrapper transaction);
        void Rollback(ISQLiteTransactionWrapper transaction);
        public void Close();
        event LogSQLiteCommandDelegate LogSQLiteCommandEvent;
    }
}
