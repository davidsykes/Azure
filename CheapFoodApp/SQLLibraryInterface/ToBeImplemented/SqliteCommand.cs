namespace SQLLibraryInterface.ToBeImplemented
{
    public class SqliteCommand
    {
        public SqliteCommand(
            IActualDatabaseConnection connection,
            string commandText,
            ISQLiteTransactionWrapper? transaction)
        {
            Connection = connection;
            CommandText = commandText;
            Transaction = transaction;
            Parameters = [];
        }

        public IActualDatabaseConnection Connection { get; set; }
        public string CommandText { get; set; }
        public ISQLiteTransactionWrapper? Transaction { get; set; }
        public List<object> Parameters { get; internal set; }
    }
}
