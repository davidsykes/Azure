using SQLLibraryInterface;

namespace SQLDatabaseAccess
{
    public class SQLDatabaseConnection : IActualDatabaseConnection
    {
        public void Close()
        {
            throw new NotImplementedException();
        }

        public IDatabaseCommand CreateDatabaseCommand(string query, IDatabaseTransactionWrapper? transactionWrapper)
        {
            throw new NotImplementedException();
        }

        public IDatabaseTransactionWrapper CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQueryCommand(IDatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public object? ExecuteScalarCommand(IDatabaseCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
