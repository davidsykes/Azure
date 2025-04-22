namespace DatabaseAccessInterfaces.Commands
{
    public class DatabaseCommand(string query)
    {
        public string Query { get; internal set; } = query;
    }
}
