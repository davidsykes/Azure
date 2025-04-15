namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        bool TableExists(string name);
        void CreateTable(string name);
        void AddNewFood(string inputText);
        public List<string> GetTestData();
    }
}
