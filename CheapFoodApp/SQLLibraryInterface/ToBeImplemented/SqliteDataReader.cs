namespace SQLLibraryInterface.ToBeImplemented
{
    public interface SqliteDataReader
    {
        DateTime GetDateTime(int ordinal);
        string? GetString(int ordinal);
        bool IsDBNull(int ordinal);
    }
}
