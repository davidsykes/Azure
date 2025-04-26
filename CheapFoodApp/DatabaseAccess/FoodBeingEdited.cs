using DatabaseAccess.DatabaseObjects;
using DatabaseAccessInterfaces;

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

        public Int64 Id => _foodItem.Id;
    }
}
