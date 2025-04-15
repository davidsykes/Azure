namespace DatabaseAccess.Library.Placeholders
{
    internal class SqliteCommand
    {
        public List<object> Parameters => throw new NotImplementedException();

        internal int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        internal SqliteDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        internal object? ExecuteScalar()
        {
            throw new NotImplementedException();
        }
    }
}
