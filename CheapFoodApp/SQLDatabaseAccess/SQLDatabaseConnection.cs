using Microsoft.Data.SqlClient;
using SQLiteLibrary;
using SQLLibraryInterface;

namespace SQLDatabaseAccess
{
    public class SQLDatabaseConnection : IActualDatabaseConnection
    {
        SqlConnection _connection;
        readonly string _connectionString = "Server=tcp:cheapfooddbserver.database.windows.net,1433;Initial Catalog=CheapFoodDb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

        public SQLDatabaseConnection()
        {
            _connection = new SqlConnection(_connectionString);
            TryOpenConnection(_connection);

        }

        private static void TryOpenConnection(SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("needs re-authentication"))
                {
                    throw new SQLiteLibraryException("Application needs re-authentication");
                }
                if (ex.Message.Contains("Connection Timeout Expired"))
                {
                    throw new SQLiteLibraryException("The Connection Timeout has expired. TRY AGAIN");
                }
                throw;
            }
        }

        public IDatabaseTransactionWrapper CreateTransaction()
        {
            throw new NotImplementedException();
            //return new SQLiteTransactionWrapper(_connection.BeginTransaction());
        }

        public int ExecuteNonQueryCommand(IDatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public object? ExecuteScalarCommand(IDatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public IDatabaseCommand CreateDatabaseCommand(
            string commandText, IDatabaseTransactionWrapper? transactionWrapper)
        {
            var transaction = transactionWrapper as SQLTransactionWrapper;

            return new SQLCommandWrapper(_connection, commandText, transaction?.SqlTransaction);
        }
    }
}
