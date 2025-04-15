namespace DatabaseAccess.Library.Placeholders
{
    internal class SqliteParameter
    {
        private string _v1;
        private object _v2;

        public SqliteParameter(string v1, object v2)
        {
            _v1 = v1;
            _v2 = v2;
        }
    }
}
