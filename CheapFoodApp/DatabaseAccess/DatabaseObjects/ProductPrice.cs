using SQLiteLibrary.Attributes;

namespace DatabaseAccess.DatabaseObjects
{
    [TableName("Prices")]
    public class ProductPrice
    {
        [PrimaryKey]
        public Int64 ShopId { get; set; }
        [PrimaryKey]
        public Int64 FoodId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
