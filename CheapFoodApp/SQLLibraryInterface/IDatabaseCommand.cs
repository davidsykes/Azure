

namespace SQLLibraryInterface
{
    public interface IDatabaseCommand
    {
        void AddParameter(string name, object value);
        IDatabaseDataReader ExecuteReader();
        int ExecuteNonQuery();
        object? ExecuteScalar();
    }
}
