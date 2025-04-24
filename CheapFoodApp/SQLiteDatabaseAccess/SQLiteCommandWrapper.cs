using Microsoft.Data.Sqlite;
using SQLLibraryInterface;

namespace SQLiteDatabaseAccess
{
    internal class SQLiteCommandWrapper : IDatabaseCommand
    {
        readonly SqliteCommand _command;

        public SQLiteCommandWrapper(SqliteConnection connection, string commandText, SqliteTransaction? transaction)
        {
            _command = new SqliteCommand
            {
                Connection = connection,
                CommandText = commandText,
                Transaction = transaction
            };
        }

        public void AddParameter(string name, object value)
        {
            var parameter = new SqliteParameter(name, value);
            _command.Parameters.Add(parameter);
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public IDatabaseDataReader ExecuteReader()
        {
            return new SqliteDataReaderWrapper(_command.ExecuteReader());
        }

        public object? ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }
    }
}
