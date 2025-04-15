namespace DatabaseAccess.Library.Placeholders
{
    internal class SQLiteTransactionWrapper
    {
        private object _v;

        public SQLiteTransactionWrapper(object v)
        {
            _v = v;
        }

        public SqliteTransaction SqliteTransaction => throw new NotImplementedException();
    }
}
