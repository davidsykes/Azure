using SQLiteLibrary;

namespace SQLLibrary
{
    public interface IDatabaseConnection
    {
        IList<T> Select<T>(string? query = null, string? where = null, string? endWith = null, object? parameters = null) where T : new();

        void RunInTransaction(Action<IDatabaseTransaction> action);
        void RunInTransaction<TParam>(Action<IDatabaseTransaction, TParam> action, TParam parameters);

        void AddLogger(LogSQLiteCommandDelegate logger);
    }
}
