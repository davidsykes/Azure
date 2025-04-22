namespace DatabaseAccessInterfaces.DatabaseTableValues
{
    internal class DatabaseTableStringValue(string name, string value) : DatabaseTableValue(name)
    {
        private readonly string _value = value;
    }
}
