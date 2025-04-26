using SQLLibraryInterface;

namespace SQLLibrary.TableAnalysis
{
    internal class AnalysedTable : IAnalysedTable
    {
        public string ClassName { get; internal set; }
        public string TableName { get; internal set; } = string.Empty;
        public IList<TableAnalyseProperty> Properties { get; internal set; } = new List<TableAnalyseProperty>();
        public bool PrimaryKeyIsAutoIncrement { get; internal set; }
        public IList<string> PrimaryKeys { get; internal set; } = new List<string>();
        public string CreateTableQuery => GetCreateTableQuery();
        public string InsertQuery => GetInsertQuery();
        public string UpsertQuery1 => GetUpsertQuery1();
        public string UpsertQuery2 => GetUpsertQuery2();
        public string SelectQuery { get { return GetSelectQuery(); } set { SetSelectQuery(value); } }
        public string UpdateQuery => GetUpdateQuery();
        public string DeleteQueryBase => GetDeleteQueryBase();

        public AnalysedTable(string className)
        {
            ClassName = className;
        }

        private string? _createTableQuery = null;
        private string GetCreateTableQuery()
        {
            if (_createTableQuery == null)
            {
                _createTableQuery = ClassHasAutoIncrementOrNoPrimaryKeys
                    ? CreateCreateQueryForAutoIncrementPrimaryKey()
                    : CreateCreateQueryForNonAutoIncrementPrimaryKey();
            }
            return _createTableQuery;
        }

        private bool ClassHasAutoIncrementOrNoPrimaryKeys =>
            !PrimaryKeys.Any() || PrimaryKeyIsAutoIncrement;

        private string CreateCreateQueryForAutoIncrementPrimaryKey()
        {
            StringWriter sw = new();
            sw.Write($"CREATE TABLE IF NOT EXISTS {TableName}(");

            var props = Properties.Select(p =>
            {
                var s = p.Name + " " + p.Type;
                if (PropertyIsAutoIncrementPrimaryKey(p))
                {
                    s += " PRIMARY KEY AUTOINCREMENT";
                }
                else
                {
                    s += p.Nullable ? " NULL" : " NOT NULL";
                }
                return s;
            }).ToArray();

            sw.Write(string.Join(',', props));
            sw.Write(");");
            return sw.ToString();
        }

        private string CreateCreateQueryForNonAutoIncrementPrimaryKey()
        {
            StringWriter sw = new();
            sw.Write($"CREATE TABLE IF NOT EXISTS {TableName}(");

            var props = Properties.Select(p =>
            {
                var s = p.Name + " " + p.Type;
                s += p.Nullable ? " NULL" : " NOT NULL";
                return s;
            }).ToArray();

            sw.Write(string.Join(',', props));
            sw.Write(",PRIMARY KEY (");
            sw.Write(string.Join(',', PrimaryKeys));
            sw.Write("));");
            return sw.ToString();
        }

        private bool PropertyIsAutoIncrementPrimaryKey(TableAnalyseProperty p)
        {
            if (PrimaryKeys.Contains(p.Name))
            {
                if (PrimaryKeyIsAutoIncrement)
                {
                    return true;
                }
                else
                {
                    throw new SQLiteLibraryException(
                        $"{ClassName}: Non auto increment primary keys are not supported yet.");
                }
            }
            return false;
        }

        private string? _insertQuery = null;

        private string GetInsertQuery()
        {
            if (_insertQuery == null)
            {
                StringWriter sw = new();
                sw.Write($"INSERT INTO {TableName}(");
                var nonPrimaryProperties = GetPropertiesApartFromAnAutoIncrementingPrimaryKey();
                var names = nonPrimaryProperties.Select(p => p.Name).ToArray();
                var namesAsParameters = nonPrimaryProperties.Select(p => "@" + p.Name).ToArray();
                sw.Write(string.Join(',', names));
                sw.Write(") VALUES(");
                sw.Write(string.Join(',', namesAsParameters));
                sw.Write(");");
                _insertQuery = sw.ToString();
            }
            return _insertQuery;
        }

        private string? _upsertQuery1 = null;

        private string GetUpsertQuery1()
        {
            if (_upsertQuery1 == null)
            {
                StringWriter sw = new();
                sw.Write($"INSERT OR IGNORE INTO {TableName} (");
                sw.Write(string.Join(',', Properties.Select(p => p.Name)));
                sw.Write(") VALUES (");
                sw.Write(string.Join(',', Properties.Select(p => "@" + p.Name)));
                sw.Write(");");
                _upsertQuery1 = sw.ToString();
            }
            return _upsertQuery1;
        }

        private string? _upsertQuery2 = null;

        private string GetUpsertQuery2()
        {
            if (_upsertQuery2 == null)
            {
                var nonPrimaryKeys = GetPropertiesApartFromAnyPrimaryKeys();
                if (nonPrimaryKeys.Count > 0)
                {
                    StringWriter sw = new();
                    sw.Write($"UPDATE {TableName} SET ");
                    sw.Write(string.Join(',', nonPrimaryKeys.Select(p => $"{p.Name}=@{p.Name}")));
                    sw.Write(" WHERE ");
                    sw.Write(string.Join(" AND ", PrimaryKeys.Select(p => $"{p}=@{p}")));
                    sw.Write(";");
                    _upsertQuery2 = sw.ToString();
                }
                else
                {
                    _upsertQuery2 = string.Empty;
                }
            }
            return _upsertQuery2;
        }

        private string? _updateQuery = null;

        private string GetUpdateQuery()
        {
            if (_updateQuery == null)
            {
                if (PrimaryKeys.Count == 0)
                {
                    throw new SQLiteLibraryException($"{TableName} can not be updated without a primary key.");
                }
                StringWriter sw = new();
                sw.Write($"UPDATE {TableName} SET ");
                var nonPrimaryProperties = GetPropertiesApartFromAnyPrimaryKeys();
                var props = nonPrimaryProperties.Select(p => $"{p.Name}=@{p.Name}").ToArray();
                sw.Write(string.Join(',', props));
                var primaryKey = PrimaryKeys.Single();
                sw.Write($" WHERE {primaryKey}=@{primaryKey};");
                _updateQuery = sw.ToString();
            }
            return _updateQuery;
        }

        private string? _selectQuery = null;

        private string GetSelectQuery()
        {
            if (_selectQuery == null)
            {
                StringWriter sw = new();
                sw.Write("SELECT ");

                var props = Properties.Select(p => p.Name).ToArray();
                sw.Write(string.Join(',', props));
                sw.Write($" FROM {TableName}");
                _selectQuery = sw.ToString();
            }
            return _selectQuery;
        }

        private void SetSelectQuery(string selectQuery)
        {
            _selectQuery = selectQuery;
        }

        private string? _deleteQueryBase = null;

        private string GetDeleteQueryBase()
        {
            if (_deleteQueryBase == null)
            {
                _deleteQueryBase = $"DELETE FROM {TableName} WHERE";
            }
            return _deleteQueryBase;
        }

        private IList<TableAnalyseProperty> GetPropertiesApartFromAnAutoIncrementingPrimaryKey()
        {
            return Properties.Where(p => !PrimaryKeyIsAutoIncrement || !PrimaryKeys.Contains(p.Name))
                .ToList();
        }

        private IList<TableAnalyseProperty> GetPropertiesApartFromAnyPrimaryKeys()
        {
            return Properties.Where(p => !PrimaryKeys.Contains(p.Name))
                .ToList();
        }
    }
}
