namespace SQLiteLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SelectQueryAttribute : Attribute
    {
        public string SelectQuery { get; init; }

        public SelectQueryAttribute(string selectQuery)
        {
            SelectQuery = selectQuery;
        }
    }
}
