using SQLiteLibrary.Attributes;
using SQLLibraryInterface;
using System.Reflection;

namespace SQLLibrary.TableAnalysis
{
    internal class TableAnalyser : ITableAnalyser
    {
        public IAnalysedTable AnalyseTable<TDBType>()
        {
            var tableType = typeof(TDBType);
            var results = new AnalysedTable(tableType.Name);
            ProcessClassAttributes(tableType, results);

            var properties = typeof(TDBType).GetProperties();

            foreach (var property in properties)
            {
                var propertyDetail = AnalyseProperty(property, results, tableType);
                if (propertyDetail != null)
                {
                    results.Properties.Add(propertyDetail);
                }
            }

            CheckAtLeastOneMappablePropertyWasFound(results);

            return results;
        }

        private static void ProcessClassAttributes(Type tableType, AnalysedTable details)
        {
            var classHasTableOrSelectQueryDefined = false;
            var attributes = tableType.GetCustomAttributes(true);
            foreach(object attribute in attributes)
            {
                if (attribute is TableNameAttribute tableName)
                {
                    details.TableName = tableName.TableName;
                    classHasTableOrSelectQueryDefined = true;
                }
                else if (attribute is SelectQueryAttribute select)
                {
                    details.SelectQuery = select.SelectQuery;
                    classHasTableOrSelectQueryDefined = true;
                }
            }

            if (!classHasTableOrSelectQueryDefined)
            {
                throw new SQLiteLibraryException($"{details.ClassName} requires a table name or select query.");
            }
        }

        private static TableAnalyseProperty? AnalyseProperty(
            PropertyInfo property, AnalysedTable tableDetails, Type tableType)
        {
            var propertyDetail = new TableAnalyseProperty
            {
                Name = property.Name
            };

            if (Attribute.GetCustomAttribute(property, typeof(NonPersistentAttribute)) is not null)
            {
                return null;
            }

            SetPropertyType(property.PropertyType, propertyDetail);

            CheckForPrimaryKeys(property, tableDetails, tableType);

            return propertyDetail;
        }

        private static void CheckForPrimaryKeys(PropertyInfo property, AnalysedTable tableDetails, Type tableType)
        {
            var pk = Attribute.GetCustomAttribute(property, typeof(AutoIncrementPrimaryKeyAttribute));
            if (pk != null)
            {
                if (property.PropertyType != typeof(long))
                {
                    throw new SQLiteLibraryException($"{tableType.Name}: The auto increment primary key must be Int64.");
                }
                SetPrimaryKey(property, tableDetails, tableType, true);
            }
            else
            {
                pk = Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (pk != null)
                {
                    SetPrimaryKey(property, tableDetails, tableType, false);
                }
            }
        }

        private static void SetPrimaryKey(PropertyInfo property, AnalysedTable tableDetails, Type tableType, bool isAutoIncrement)
        {
            if (isAutoIncrement)
            {
                if (tableDetails.PrimaryKeys.Any())
                {
                    throw new SQLiteLibraryException($"{tableType.Name} can not have auto increment and non auto increment primary keys.");
                }
                tableDetails.PrimaryKeys.Add(property.Name);
                tableDetails.PrimaryKeyIsAutoIncrement = true;
            }
            else
            {
                if (tableDetails.PrimaryKeyIsAutoIncrement)
                {
                    throw new SQLiteLibraryException($"{tableType.Name} can not have auto increment and non auto increment primary keys.");
                }
                tableDetails.PrimaryKeys.Add(property.Name);
            }
        }

        private static void SetPropertyType(Type propertyType, TableAnalyseProperty detail)
        {
            if (propertyType == typeof(string))
            {
                detail.Type = "TEXT";
                detail.Nullable = false;
            }
            else if (propertyType == typeof(int))
            {
                detail.Type = "INTEGER";
                detail.Nullable = false;
            }
            else if (propertyType == typeof(long))
            {
                detail.Type = "INTEGER";
                detail.Nullable = false;
            }
            else if (propertyType == typeof(int?))
            {
                detail.Type = "INTEGER";
                detail.Nullable = true;
            }
            else if (propertyType == typeof(int?))
            {
                detail.Type = "TEXT";
                detail.Nullable = true;
            }
            else if (propertyType == typeof(DateTime))
            {
                detail.Type = "TEXT";
                detail.Nullable = false;
            }
            else if (propertyType == typeof(double))
            {
                detail.Type = "REAL";
                detail.Nullable = false;
            }
            else
            {
                throw new SQLiteLibraryException($"Property type {propertyType.Name} is not supported.");
            }
        }

        private static void CheckAtLeastOneMappablePropertyWasFound(AnalysedTable table)
        {
            if (table.Properties.Count < 1)
            {
                throw new SQLiteLibraryException($"{table.ClassName} has no mappable items.");
            }
        }
    }
}