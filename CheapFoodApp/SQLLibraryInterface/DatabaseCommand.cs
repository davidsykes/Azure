namespace SQLLibraryInterface
{
    public class DatabaseCommand
    {
        public string CommandText { get; private set; }
        public List<DatabaseCommandParameter> Parameters { get; private set; }

        public DatabaseCommand(string commandText)
        {
            CommandText = commandText;
            Parameters = [];
        }
    }
}
