#nullable disable
using SQLiteLibrary.Attributes;

namespace DatabaseAccess.DatabaseObjects
{

    [TableName("Supermarkets")]
    public class Supermarket
    {
        [AutoIncrementPrimaryKey]
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }
}
