namespace SQLLibraryInterface
{
    public interface IDatabaseDataReader : IDisposable
    {
        int FieldCount { get; }

        DateTime GetDateTime(int ordinal);
        object? GetDouble(int ordinal);
        object? GetInt32(int ordinal);
        object? GetInt64(int ordinal);
        string GetName(int ordinal);
        string? GetString(int ordinal);
        bool IsDBNull(int ordinal);
        bool Read();
    }
}
