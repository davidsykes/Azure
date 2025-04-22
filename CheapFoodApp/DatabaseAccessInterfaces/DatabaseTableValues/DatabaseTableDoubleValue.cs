namespace DatabaseAccessInterfaces.DatabaseTableValues
{
    public class DatabaseTableDoubleValue(string name, double value) : DatabaseTableValue(name)
    {
        private readonly double _value = value;
    }
}
