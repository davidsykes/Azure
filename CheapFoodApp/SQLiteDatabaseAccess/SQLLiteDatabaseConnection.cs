using Microsoft.Data.Sqlite;
using SQLiteDatabaseAccess.Library;
using SQLLibraryInterface;
using SQLLibraryInterface.ToBeImplemented;

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

        public int ExecuteNonQueryCommand(DatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReaderCommand(DatabaseCommand command)
        {
            throw new NotImplementedException();
        }

        public object? ExecuteScalarCommand(DatabaseCommand sCommand)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
