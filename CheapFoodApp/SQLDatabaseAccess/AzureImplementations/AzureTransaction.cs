using Microsoft.Data.SqlClient;

namespace SQLDatabaseAccess.AzureImplementations
{
    internal class AzureTransaction(SqlTransaction sqlTransaction)
    {
        private SqlTransaction _sqlTransaction = sqlTransaction;

        public SqlTransaction Transaction => _sqlTransaction;
    }
}
