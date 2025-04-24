namespace SQLiteLibrary
{
    public class SQLQueryString
    {
        private readonly string _query;

        public SQLQueryString(string query)
        {
            var lines = query.Replace("\r\n","\n").Replace("\r","\n").Split('\n');
            var strippedLines = lines.Select(l => l.Trim())
                .Where(l => l.Length > 0);
            _query = string.Join(" ", strippedLines);
        }

        public override string ToString() => _query;
    }
}
