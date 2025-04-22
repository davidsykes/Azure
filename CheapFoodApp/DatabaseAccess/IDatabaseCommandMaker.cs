using DatabaseAccessInterfaces.Commands;
using DatabaseAccessInterfaces.DatabaseTableValues;

namespace DatabaseAccess
{
    internal interface IDatabaseCommandMaker
    {
        DatabaseCommand MakeInsertCommand(string tableName, IList<DatabaseTableValue> values);
    }
}
