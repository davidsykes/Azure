namespace DatabaseAccessInterfaces.DatabaseTableValues
{
    public class DatabaseTableValue
    {
        public string Name { get; set; }

        public DatabaseTableValue(string name)
        {
            Name = name;
        }
    }
}
