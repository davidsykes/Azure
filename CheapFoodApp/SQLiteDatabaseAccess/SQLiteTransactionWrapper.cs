using Microsoft.Data.Sqlite;
using SQLLibraryInterface;

namespace SQLiteDatabaseAccess
{
    internal class SQLiteTransactionWrapper(SqliteTransaction sqliteTransaction) : IDatabaseTransactionWrapper
    {
        public SqliteTransaction SqliteTransaction { get; private set; } = sqliteTransaction;

        public void Commit()
        {
            SqliteTransaction.Commit();
        }

        public void Rollback()
        {
            SqliteTransaction.Rollback();
        }
    }
}
