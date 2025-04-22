namespace DatabaseAccessInterfaces.DatabaseTableValues
{
    public class DatabaseTableIntValue(string name, int value) : DatabaseTableValue(name)
    {
        private readonly int _value = value;
    }
}
