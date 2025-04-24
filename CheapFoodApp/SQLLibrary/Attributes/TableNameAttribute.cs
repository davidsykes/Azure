namespace SQLiteLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; init; }

        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
