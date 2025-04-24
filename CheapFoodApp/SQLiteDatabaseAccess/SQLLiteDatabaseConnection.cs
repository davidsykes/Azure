using Microsoft.Data.Sqlite;
using SQLiteDatabaseAccess.Library;
using SQLLibraryInterface;

namespace SQLiteDatabaseAccess
{
    public class SQLLiteDatabaseConnection : IActualDatabaseConnection
    {
        SqliteConnection _connection;

        public SQLLiteDatabaseConnection(string connectionString)
        {
            _connection = new SqliteConnection(CreateFileConnectionString(connectionString));
            _connection.Open();

        }

        private static string CreateFileConnectionString(string databasePath)
        {
            return $"Data Source={databasePath}";
        }

        public IDatabaseTransactionWrapper CreateTransaction()
        {
            return new SQLiteTransactionWrapper(_connection.BeginTransaction());
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
            var transaction = transactionWrapper as SQLiteTransactionWrapper;

            return new SQLiteCommandWrapper(_connection, commandText, transaction?.SqliteTransaction);
        }
    }
}
