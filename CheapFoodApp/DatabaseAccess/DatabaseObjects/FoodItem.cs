#nullable disable
using SQLiteLibrary.Attributes;

namespace DatabaseAccess.DatabaseObjects
{
    [TableName("Foods")]
    public class FoodItem
    {
        [AutoIncrementPrimaryKey]
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }
}
