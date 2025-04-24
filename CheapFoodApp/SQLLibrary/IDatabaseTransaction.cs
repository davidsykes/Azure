namespace SQLiteLibrary
{
    public interface IDatabaseTransaction
    {
        void CreateTable<T>();
        IList<T> Select<T>(string? filter = null, object? filterValue = null) where T : new();
        void InsertRow<T>(T row);
        void InsertRows<T>(IEnumerable<T> rows);
        void UpdateRow<T>(T row);
        void UpdateRows<T>(IList<T> rows);
        void DeleteRow<T>(T row);
        void DeleteRows<T>(IEnumerable<T> rows);
        void DeleteRows<T>(string filter, object value);
        void ExecuteNonQuery(string command, object parameters);
        void ExecuteNonQuery(string command);

        bool RollbackTransaction { get; set; }
    }
}
