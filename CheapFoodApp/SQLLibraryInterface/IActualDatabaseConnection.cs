namespace SQLLibraryInterface
{
    public interface IActualDatabaseConnection
    {
        void Close();
        IDatabaseCommand CreateDatabaseCommand(string query, IDatabaseTransactionWrapper? transactionWrapper);
        IDatabaseTransactionWrapper CreateTransaction();
        int ExecuteNonQueryCommand(IDatabaseCommand sCommand);
        object? ExecuteScalarCommand(IDatabaseCommand sCommand);
    }
}
