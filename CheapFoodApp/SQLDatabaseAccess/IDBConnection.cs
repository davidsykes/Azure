using Microsoft.Data.SqlClient;
using SQLDatabaseAccess.Library;

namespace SQLDatabaseAccess
{
    internal interface IDBConnection
    {
        IDBTransaction BeginTransaction();
        void Close();
        SqlCommand CreateCommand(string commandText, IDBTransaction? transaction);
    }
}
