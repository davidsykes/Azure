namespace SQLiteLibrary.SQLite
{
    internal class SQLiteLoggedCommand
    {
        public string Command { get; internal set; } = "";
        public object? Parameters { get; internal set; }
    }
}
