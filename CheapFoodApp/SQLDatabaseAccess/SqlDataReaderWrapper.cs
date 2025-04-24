using Microsoft.Data.SqlClient;
using SQLLibraryInterface;

namespace SQLDatabaseAccess
{
    internal class SqlDataReaderWrapper(SqlDataReader sqlDataReader) : IDatabaseDataReader
    {
        readonly SqlDataReader _sqlDataReader = sqlDataReader;

        public int FieldCount => _sqlDataReader.FieldCount;

        public bool Read()
        {
            return _sqlDataReader.Read();
        }

        public DateTime GetDateTime(int ordinal)
        {
            return _sqlDataReader.GetDateTime(ordinal);
        }

        public object? GetDouble(int ordinal)
        {
            return _sqlDataReader.GetDouble(ordinal);
        }

        public object? GetInt32(int ordinal)
        {
            return _sqlDataReader.GetInt32(ordinal);
        }

        public object? GetInt64(int ordinal)
        {
            return _sqlDataReader.GetInt64(ordinal);
        }

        public string GetName(int ordinal)
        {
            return _sqlDataReader.GetName(ordinal);
        }

        public string? GetString(int ordinal)
        {
            return _sqlDataReader.GetString(ordinal);
        }

        public bool IsDBNull(int ordinal)
        {
            return _sqlDataReader.IsDBNull(ordinal);
        }

        public void Dispose()
        {
            _ = _sqlDataReader.DisposeAsync();
        }
    }
}
