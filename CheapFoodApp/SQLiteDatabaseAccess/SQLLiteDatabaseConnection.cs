using Microsoft.Data.Sqlite;
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

        public void Close()
        {
            _connection.Close();
        }

        public IDatabaseCommand CreateDatabaseCommand(
            string commandText, IDatabaseTransactionWrapper? transactionWrapper)
        {
            var transaction = transactionWrapper as SQLiteTransactionWrapper;

            return new SQLiteCommandWrapper(_connection, commandText, transaction?.SqliteTransaction);
        }
    }
}
