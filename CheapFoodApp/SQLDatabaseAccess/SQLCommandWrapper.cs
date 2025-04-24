using Microsoft.Data.SqlClient;
using SQLLibraryInterface;

namespace SQLDatabaseAccess
{
    internal class SQLCommandWrapper : IDatabaseCommand
    {
        readonly SqlCommand _command;

        public SQLCommandWrapper(SqlConnection connection, string commandText, SqlTransaction? transaction)
        {
            _command = new SqlCommand
            {
                Connection = connection,
                CommandText = commandText,
                Transaction = transaction
            };
        }

        public void AddParameter(string name, object value)
        {
            var parameter = new SqlParameter(name, value);
            _command.Parameters.Add(parameter);
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public IDatabaseDataReader ExecuteReader()
        {
            return new SqlDataReaderWrapper(_command.ExecuteReader());
        }

        public object? ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }
    }
}
