#nullable disable
using SQLiteLibrary.Attributes;

namespace DatabaseAccessInterfaces.DatabaseObjects
{

    [TableName("Supermarkets")]
    public class Supermarket
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
