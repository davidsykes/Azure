namespace SQLLibrary.SQLite
{
    internal class SqliteParameter
    {
        private string _name;
        private object _value;

        public SqliteParameter(string name, object value)
        {
            _name = name;
            _value = value;
        }
    }
}
