using Microsoft.Data.SqlClient;
using SQLLibraryInterface;

namespace SQLDatabaseAccess
{
    internal class SQLTransactionWrapper(SqlTransaction sqlTransaction) : IDatabaseTransactionWrapper
    {
        public SqlTransaction SqlTransaction { get; private set; } = sqlTransaction;

        public void Commit()
        {
            SqlTransaction.Commit();
        }

        public void Rollback()
        {
            SqlTransaction.Rollback();
        }
    }
}
