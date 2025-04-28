namespace SQLLibraryInterface
{
    public interface IActualDatabaseConnection
    {
        void Close();
        IDatabaseCommand CreateDatabaseCommand(string query, IDatabaseTransactionWrapper? transactionWrapper);
        IDatabaseTransactionWrapper CreateTransaction();
        string GetSelectIdentityCommand();
    }
}
