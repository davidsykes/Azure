using SQLiteLibrary;
using SQLiteLibrary.SQLite;
using SQLLibrary.SQLite;
using SQLLibrary.TableAnalysis;
using SQLLibraryInterface.ToBeImplemented;
using System.Reflection;

namespace SQLLibrary.CRUD
{
    internal class DatabaseTransaction : IDatabaseTransaction, ICommittable
    {
        private readonly ISQLiteWrapper _sqLiteWrapper;
        private readonly ITableAnalyser _tableAnalyser;
        private readonly ISQLiteTransactionWrapper _sqliteTransaction;

        public bool RollbackTransaction { get; set; }

        public DatabaseTransaction(
            ISQLiteWrapper sqLiteWrapper,
            ITableAnalyser tableAnalyser)
        {
            _sqLiteWrapper = sqLiteWrapper;
            _tableAnalyser = tableAnalyser;
            _sqliteTransaction = sqLiteWrapper.CreateTransaction();
        }

        #region Create Table

        public void CreateTable<T>()
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();

            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, tableInfo.CreateTableQuery);
        }

        #endregion

        #region Select

        public IList<T> Select<T>(string? filter = null, object? filterValue = null) where T : new()
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();

            return _sqLiteWrapper.Select<T>(
                _sqliteTransaction,
                tableInfo.SelectQuery,
                filter,
                filterValue);
        }

        #endregion

        #region Insert

        public void InsertRows<T>(IEnumerable<T> rows)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            foreach (var row in rows)
            {
                InsertRow(row, tableInfo);
            }
        }

        public void InsertRow<T>(T row)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            InsertRow(row, tableInfo);
        }

        private void InsertRow<T>(T row, IAnalysedTable tableInfo)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            _sqLiteWrapper.ExecuteScalar(_sqliteTransaction, tableInfo.InsertQuery, row);
        }

        #endregion

        #region Update

        public void UpdateRows<T>(IList<T> rows)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            foreach (var row in rows)
            {
                UpdateRow(row, tableInfo);
            }
        }

        public void UpdateRow<T>(T row)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            UpdateRow(row, tableInfo);
        }

        private void UpdateRow<T>(T row, IAnalysedTable tableInfo)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (!tableInfo.PrimaryKeys.Any())
            {
                throw new SQLiteLibraryException($"Update is not valid for {tableInfo.ClassName} as it has no primary key.");
            }

            if (tableInfo.PrimaryKeyIsAutoIncrement)
            {
                UpdateRowForTableWithSingleAutoIncrementPrimaryKey(row, tableInfo);
            }
            else
            {
                UpdateRowForTableWithNonAutoIncrementPrimaryKeys(row, tableInfo);
            }
        }

        private void UpdateRowForTableWithSingleAutoIncrementPrimaryKey<T>(T row, IAnalysedTable tableInfo)
        {
            var primaryKeyProperty = GetPrimaryKeyProperty(tableInfo, row);
            var primaryKeyValue = primaryKeyProperty.GetValue(row, null);
            var primaryKeyValueInt64 = (long)primaryKeyValue!;

            if (primaryKeyValueInt64 == 0)
            {
                var newId = _sqLiteWrapper.ExecuteScalar(_sqliteTransaction, tableInfo.InsertQuery, row);
                primaryKeyProperty.SetValue(row, newId, null);
            }
            else
            {
                var rowsUpdated = _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, tableInfo.UpdateQuery, row);

                if (rowsUpdated != 1)
                {
                    throw new SQLiteLibraryException($"Update failed for auto increment primary key value {primaryKeyValue}");
                }
            }
        }

        private void UpdateRowForTableWithNonAutoIncrementPrimaryKeys<T>(T row, IAnalysedTable tableInfo)
        {
            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, tableInfo.UpsertQuery1, row);
            if (!string.IsNullOrEmpty(tableInfo.UpsertQuery2))
            {
                _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, tableInfo.UpsertQuery2, row);
            }
        }
        #endregion

        #region Delete

        public void DeleteRow<T>(T row)
        {
            DeleteRow(row, _tableAnalyser.AnalyseTable<T>());
        }

        public void DeleteRows<T>(IEnumerable<T> rows)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            foreach (var row in rows)
            {
                DeleteRow(row, tableInfo);
            }
        }

        private void DeleteRow<T>(T? row, IAnalysedTable tableInfo)
        {
            var deleteQuery = MakeDeleteByPrimaryKeyQuery(tableInfo);
            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, deleteQuery, row);
            if (tableInfo.PrimaryKeyIsAutoIncrement)
            {
                var primaryKeyProperty = GetPrimaryKeyProperty(tableInfo, row);
                primaryKeyProperty.SetValue(row, 0, null);
            }
        }

        private static string MakeDeleteByPrimaryKeyQuery(IAnalysedTable tableInfo)
        {
            if (tableInfo.PrimaryKeys.Count == 0)
            {
                throw new SQLiteLibraryException(
                    $"DeleteRow with no filter needs a primary key for {tableInfo.ClassName}.");
            }

            return tableInfo.DeleteQueryBase + " " +
                string.Join(" AND ", tableInfo.PrimaryKeys.Select(p => $"{p}=@{p}")) + ";";
        }

        public void DeleteRows<T>(string filter, object value)
        {
            var tableInfo = _tableAnalyser.AnalyseTable<T>();
            var deleteQuery = MakeDeleteByFilterQuery(tableInfo, filter);
            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, deleteQuery, value);
        }

        private static string MakeDeleteByFilterQuery(IAnalysedTable tableInfo, string filter)
        {
            return $"{tableInfo.DeleteQueryBase} {filter};";
        }

        #endregion

        public void ExecuteNonQuery(string command, object parameters)
        {
            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, command, parameters);
        }

        public void ExecuteNonQuery(string command)
        {
            _sqLiteWrapper.ExecuteNonQuery(_sqliteTransaction, command);
        }

        private static PropertyInfo GetPrimaryKeyProperty<T>(IAnalysedTable tableInfo, T row)
        {
            var primaryKeyName = tableInfo.PrimaryKeys.Single();
            var primaryKeyProperty = row!.GetType().GetProperty(primaryKeyName);
            return primaryKeyProperty!;
        }

        public void Commit()
        {
            _sqLiteWrapper.Commit(_sqliteTransaction);
        }

        public void Rollback()
        {
            _sqLiteWrapper.Rollback(_sqliteTransaction);
        }
    }
}
