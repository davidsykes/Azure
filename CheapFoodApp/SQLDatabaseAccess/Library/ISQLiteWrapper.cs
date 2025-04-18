namespace SQLDatabaseAccess.Library
{
    internal interface ISQLiteWrapper
    {
        IDBTransaction CreateTransaction();
        List<T> Select<T>(
            IDBTransaction? transaction,
            string query,
            string? where = null,
            object? parameters = null) where T : new();
        int ExecuteNonQuery(IDBTransaction transaction, string command, object? parameters = null);
        long ExecuteScalar(IDBTransaction transaction, string command, object? parameters = null);
        void Commit(IDBTransaction transaction);
        void Rollback(IDBTransaction transaction);
        public void Close();
        event LogSQLiteCommandDelegate LogSQLiteCommandEvent;
    }
}
