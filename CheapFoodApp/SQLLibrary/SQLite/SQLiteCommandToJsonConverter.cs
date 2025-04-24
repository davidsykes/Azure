using System.Text.Json;

namespace SQLiteLibrary.SQLite
{
    internal class SQLiteCommandToJsonConverter
    {
        internal static string Convert(string command, object? parameters)
        {
            return JsonSerializer.Serialize(
                new SQLiteLoggedCommand
                {
                    Command = command,
                    Parameters = parameters
                });
        }
    }
}
