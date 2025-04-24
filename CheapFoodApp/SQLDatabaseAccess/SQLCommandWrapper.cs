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
            throw new NotImplementedException();
        }

        public IDatabaseDataReader ExecuteReader()
        {
            return new SqlDataReaderWrapper(_command.ExecuteReader());
        }
    }
}
