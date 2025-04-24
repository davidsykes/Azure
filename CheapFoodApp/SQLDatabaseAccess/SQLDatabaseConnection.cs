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

        public IDatabaseTransactionWrapper CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQueryCommand(DatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReaderCommand(DatabaseCommand command)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarCommand(DatabaseCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
