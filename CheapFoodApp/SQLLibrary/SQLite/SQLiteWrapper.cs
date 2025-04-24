using SQLiteLibrary;
using SQLiteLibrary.SQLite;
using SQLLibraryInterface;
using SQLLibraryInterface.ToBeImplemented;
using System.Reflection;

namespace SQLLibrary.SQLite
{
    internal class SQLiteWrapper : ISQLiteWrapper, IDisposable
    {
        readonly IActualDatabaseConnection _connection;

        public event LogSQLiteCommandDelegate? LogSQLiteCommandEvent;

        #region Public Methods

        public SQLiteWrapper(IActualDatabaseConnection connection)
        {
            _connection = connection;
        }

        public List<T> Select<T>(
            IDatabaseTransactionWrapper? transaction,
            string query,
            string? where,
            object? parameters) where T : new()
        {
            if (where is not null)
            {
                query = query + " WHERE " + where;
            }

            var command = new DatabaseCommand(query);
            AddParametersToCommand(command, parameters);

            var results = new List<T>();
            var objectProperties = typeof(T).GetProperties();

            using (var reader = _connection.ExecuteReaderCommand(command))
            {
                while (reader.Read())
                {
                    var targetObject = new T();

                    for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                    {
                        var name = reader.GetName(ordinal);

                        var property = FindProperty(objectProperties, name);

                        var type = property.PropertyType;

                        if (type == typeof(long))
                        {
                            if (reader.IsDBNull(ordinal))
                            {
                                property.SetValue(targetObject, 0);
                            }
                            else
                            {
                                property.SetValue(targetObject, reader.GetInt64(ordinal));
                            }
                        }
                        else if (type == typeof(int))
                        {
                            if (reader.IsDBNull(ordinal))
                            {
                                property.SetValue(targetObject, 0);
                            }
                            else
                            {
                                property.SetValue(targetObject, reader.GetInt32(ordinal));
                            }
                        }
                        else if (type == typeof(int?))
                        {
                            if (reader.IsDBNull(ordinal))
                            {
                                property.SetValue(targetObject, null);
                            }
                            else
                            {
                                property.SetValue(targetObject, reader.GetInt32(ordinal));
                            }
                        }
                        else if (type == typeof(string))
                        {
                            property.SetValue(targetObject, GetString(reader, ordinal));
                        }
                        else if (type == typeof(DateTime))
                        {
                            property.SetValue(targetObject, GetDateTime(reader, ordinal));
                        }
                        else if (type == typeof(double))
                        {
                            property.SetValue(targetObject, reader.GetDouble(ordinal));
                        }
                        else
                        {
                            throw new SQLiteLibraryException("Unsupported data type");
                        }
                    }
                    results.Add(targetObject);
                }
            }

            return results;
        }

        private static PropertyInfo FindProperty(PropertyInfo[] objectProperties, string name)
        {
            var property = objectProperties.SingleOrDefault(m => m.Name == name);
            if (property == null)
            {
                throw new SQLiteLibraryException($"Property '{name}' could not be found.");
            }
            return property;
        }

        public int ExecuteNonQuery(
            IDatabaseTransactionWrapper? transaction, string command, object? parameters)
        {
            RaiseLogSQLiteCommandEvent(command, parameters);
            var sCommand = new DatabaseCommand(command);
            AddParametersToCommand(sCommand, parameters);
            return _connection.ExecuteNonQueryCommand(sCommand);
        }

        public long ExecuteScalar(
            IDatabaseTransactionWrapper transaction, string command, object? parameters = null)
        {
            ExecuteNonQuery(transaction, command, parameters);

            command = "; SELECT last_insert_rowid()";
            var sCommand = new DatabaseCommand(command);
            var result = _connection.ExecuteScalarCommand(sCommand) ?? throw new SQLiteLibraryException("Null result from ExecuteScalar");
            var resultAsInt = (long)result;
            return resultAsInt;
        }

        public void Close()
        {
            _connection.Close();
        }

        #endregion

        #region Support Code

        private static void AddParametersToCommand(DatabaseCommand command, object? parameters)
        {
            if (parameters != null)
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var value = prop.GetValue(parameters, null);

                    var parameter = new DatabaseCommandParameter("@" + name, value ?? DBNull.Value);
                    command.Parameters.Add(parameter);
                }
            }
        }

        private static string? GetString(IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            return reader.GetString(ordinal);
        }

        private static DateTime GetDateTime(IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return DateTime.MinValue;
            return reader.GetDateTime(ordinal);
        }

        public IDatabaseTransactionWrapper CreateTransaction()
        {
           return _connection.CreateTransaction();
        }

        //public void Commit(IDatabaseTransactionWrapper transaction)
        //{
        //    transaction.Commit();
        //}

        //public void Rollback(IDatabaseTransactionWrapper transaction)
        //{
        //    transaction.Rollback();
        //}

        private void RaiseLogSQLiteCommandEvent(string command, object? parameters)
        {
            LogSQLiteCommandEvent?.Invoke(SQLiteCommandToJsonConverter.Convert(command, parameters));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _connection.Close();
        }

        #endregion
    }
}
