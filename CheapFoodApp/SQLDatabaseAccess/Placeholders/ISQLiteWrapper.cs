namespace DatabaseAccess.Library.Placeholders
{
    internal interface ISQLiteWrapper
    {
        SQLiteTransactionWrapper CreateTransaction();
        List<T> Select<T>(
            SQLiteTransactionWrapper? transaction,
            string query,
            string? where = null,
            object? parameters = null) where T : new();
        int ExecuteNonQuery(SQLiteTransactionWrapper transaction, string command, object? parameters = null);
        long ExecuteScalar(SQLiteTransactionWrapper transaction, string command, object? parameters = null);
        void Commit(SQLiteTransactionWrapper transaction);
        void Rollback(SQLiteTransactionWrapper transaction);
        public void Close();
        event LogSQLiteCommandDelegate LogSQLiteCommandEvent;
    }
}
