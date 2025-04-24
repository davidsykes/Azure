namespace SQLLibraryInterface
{
    public class DatabaseCommandParameter(string name, object value)
    {
        private string _name = name;
        private object _value = value;
    }
}
