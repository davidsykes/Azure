using Microsoft.Data.SqlClient;
using SQLDatabaseAccess.Library;
using System.Data;

namespace SQLDatabaseAccess.AzureImplementations
{
    internal class AzureDBConnection : IDBConnection
    {
        private SqlConnection _connection;

        public AzureDBConnection(SqlConnection conn)
        {
            _connection = conn;
        }

        public AzureTransaction BeginTransaction()
        {
            return new AzureTransaction(_connection.BeginTransaction());
        }

        public void Close()
        {
            _connection.Close();
        }

        public SqlCommand CreateCommand(string commandText, IDBTransaction? transaction)
        {
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            if (transaction is not null)
            {
                command.Transaction = (transaction as AzureTransaction)!.Transaction;
            }
            return command;
        }

        IDBTransaction IDBConnection.BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
