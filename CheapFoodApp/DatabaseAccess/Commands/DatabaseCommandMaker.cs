using DatabaseAccessInterfaces.Commands;
using DatabaseAccessInterfaces.DatabaseTableValues;
using System.Text;

namespace DatabaseAccess.Commands
{
    internal class DatabaseCommandMaker : IDatabaseCommandMaker
    {
        public DatabaseCommand MakeInsertCommand(string tableName, IList<DatabaseTableValue> values)
        {
            var query = new StringBuilder($"INSERT INTO {tableName}(");

            var needComma = false;
            foreach(var value in values)
            {
                if (needComma)
                {
                    query.Append(",");
                }
                query.Append(value.Name);
                needComma = true;
            }
            query.Append(") VALUES(");
            needComma = false;
            foreach (var value in values)
            {
                if (needComma)
                {
                    query.Append(",");
                }
                query.Append("@");
                query.Append(value.Name);
                needComma = true;
            }
            query.Append(")");

            var databaseCommand = new DatabaseCommand(query.ToString());

            return databaseCommand;
        }
    }
}
