#nullable disable
using SQLiteLibrary.Attributes;

namespace DatabaseAccessInterfaces.DatabaseObjects
{
    [TableName("Foods")]
    public class FoodItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
