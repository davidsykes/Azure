using Microsoft.Data.Sqlite;

namespace SQLiteDatabaseAccess.Library
{
    internal class SQLiteTransactionWrapper
    {
        public SqliteTransaction SqliteTransaction { get; private set; }

        public SQLiteTransactionWrapper(SqliteTransaction sqliteTransaction)
        {
            SqliteTransaction = sqliteTransaction;
        }
    }
}
