using Microsoft.Data.Sqlite;
using SQLLibraryInterface;

namespace SQLiteDatabaseAccess
{
    internal class SqliteDataReaderWrapper(SqliteDataReader sqLiteDataReader) : IDatabaseDataReader
    {
        readonly SqliteDataReader _sqLiteDataReader = sqLiteDataReader;

        public int FieldCount => _sqLiteDataReader.FieldCount;

        public bool Read()
        {
            return _sqLiteDataReader.Read();
        }

        public DateTime GetDateTime(int ordinal)
        {
            return _sqLiteDataReader.GetDateTime(ordinal);
        }

        public object? GetDouble(int ordinal)
        {
            return _sqLiteDataReader.GetDouble(ordinal);
        }

        public object? GetInt32(int ordinal)
        {
            return _sqLiteDataReader.GetInt32(ordinal);
        }

        public object? GetInt64(int ordinal)
        {
            return _sqLiteDataReader.GetInt64(ordinal);
        }

        public string GetName(int ordinal)
        {
            return _sqLiteDataReader.GetName(ordinal);
        }

        public string? GetString(int ordinal)
        {
            return _sqLiteDataReader.GetString(ordinal);
        }

        public bool IsDBNull(int ordinal)
        {
            return _sqLiteDataReader.IsDBNull(ordinal);
        }

        public void Dispose()
        {
            _ = _sqLiteDataReader.DisposeAsync();
        }
    }
}
