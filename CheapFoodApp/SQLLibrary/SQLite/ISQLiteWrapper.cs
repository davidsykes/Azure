using SQLiteLibrary;
using SQLLibraryInterface;

namespace SQLLibrary.SQLite
{
    internal interface ISQLiteWrapper
    {
        IDatabaseTransactionWrapper CreateTransaction();
        List<T> Select<T>(
            IDatabaseTransactionWrapper? transaction,
            string query,
            string? where = null,
            object? parameters = null) where T : new();
        int ExecuteNonQuery(IDatabaseTransactionWrapper transaction, string command, object? parameters = null);
        long ExecuteScalar(IDatabaseTransactionWrapper transaction, string command, object? parameters = null);
        //void Commit(IDatabaseTransactionWrapper transaction);
        //void Rollback(IDatabaseTransactionWrapper transaction);
        public void Close();
        event LogSQLiteCommandDelegate LogSQLiteCommandEvent;
    }
}
