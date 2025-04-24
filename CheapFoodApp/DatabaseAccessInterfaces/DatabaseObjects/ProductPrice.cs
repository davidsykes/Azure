using SQLiteLibrary.Attributes;

namespace DatabaseAccessInterfaces.DatabaseObjects
{
    [TableName("Prices")]
    public class ProductPrice
    {
        [PrimaryKey]
        public int FoodId { get; set; }
        [PrimaryKey]
        public int ShopId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
