using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;

namespace DatabaseAccess
{
    public class FoodBeingEdited
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly FoodItem _foodItem;

        public FoodBeingEdited(int id, IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;

            _foodItem = _databaseAccess.GetFoodItem(id);
        }

        public string Name => _foodItem.Name;

        public int Id => _foodItem.Id;
    }
}
