using SQLLibraryInterface.ToBeImplemented;

namespace SQLLibraryInterface
{
    public interface IActualDatabaseConnection
    {
        void Close();
        IDatabaseTransactionWrapper CreateTransaction();
        int ExecuteNonQueryCommand(DatabaseCommand sCommand);
        IDataReader ExecuteReaderCommand(DatabaseCommand command);
        object? ExecuteScalarCommand(DatabaseCommand sCommand);
    }
}
