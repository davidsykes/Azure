namespace DatabaseAccess.Library.Placeholders
{
    internal class SqliteDataReader : IDisposable
    {
        public int FieldCount { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal DateTime GetDateTime(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal object? GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal object? GetInt32(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal object? GetInt64(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal string GetName(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal string? GetString(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal bool IsDBNull(int ordinal)
        {
            throw new NotImplementedException();
        }

        internal bool Read()
        {
            throw new NotImplementedException();
        }
    }
}
