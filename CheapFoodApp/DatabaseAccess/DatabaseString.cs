namespace DatabaseAccessInterfaces
{
    public class DatabaseString(string value)
    {
        private readonly string _value = value.Trim();

        public override string ToString() => _value;
    }
}
