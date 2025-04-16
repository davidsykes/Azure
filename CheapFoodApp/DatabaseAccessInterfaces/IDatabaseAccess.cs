namespace DatabaseAccessInterfaces
{
    public interface IDatabaseAccess
    {
        bool TableExists(string name);
        void CreateFoodsTable();
        void AddNewFood(string name);
        public List<string> GetTestData();
    }
}
