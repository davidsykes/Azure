using DatabaseAccessInterfaces;

namespace DatabaseAccess
{
    public class DummyDatabaseAccessWrapper : IDatabaseAccess
    {
        public void AddNewFood(string inputText)
        {
        }

        public void CreateFoodsTable()
        {
        }

        public List<string> GetTestData()
        {
            return ["Dummy", "data"];
        }

        public bool TableExists(string name)
        {
            return true;
        }
    }
}
