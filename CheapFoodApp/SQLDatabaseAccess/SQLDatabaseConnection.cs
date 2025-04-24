using SQLLibraryInterface;
using SQLLibraryInterface.ToBeImplemented;

namespace SQLDatabaseAccess
{
    public class SQLDatabaseConnection : IActualDatabaseConnection
    {
        public void Close()
        {
            throw new NotImplementedException();
        }

        public ISQLiteTransactionWrapper CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQueryCommand(SqliteCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReaderCommand(SqliteCommand command)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarCommand(SqliteCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
