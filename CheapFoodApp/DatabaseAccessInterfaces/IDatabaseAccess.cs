namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        bool TableExists(string name);
        void CreateFoodsTable();
        void AddNewFood(string inputText);
        public List<string> GetTestData();
    }
}
