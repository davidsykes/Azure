using SQLLibraryInterface.ToBeImplemented;

namespace SQLLibraryInterface
{
    public interface IActualDatabaseConnection
    {
        void Close();
        ISQLiteTransactionWrapper CreateTransaction();
        int ExecuteNonQueryCommand(SqliteCommand sCommand);
        IDataReader ExecuteReaderCommand(SqliteCommand command);
        object? ExecuteScalarCommand(SqliteCommand sCommand);
    }
}
