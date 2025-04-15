namespace DatabaseAccess.Library.Placeholders
{
    internal class SqliteConnection
    {
        private string _connectionString;

        public SqliteConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal object BeginTransaction()
        {
            throw new NotImplementedException();
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}
