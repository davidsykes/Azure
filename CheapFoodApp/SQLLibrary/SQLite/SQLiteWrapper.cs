using SQLiteLibrary;
using SQLiteLibrary.SQLite;
using SQLLibraryInterface;
using System.Reflection;

namespace SQLLibrary.SQLite
{
    internal class SQLiteWrapper(IActualDatabaseConnection connection) : ISQLiteWrapper, IDisposable
    {
        readonly IActualDatabaseConnection _connection = connection;

        public event LogSQLiteCommandDelegate? LogSQLiteCommandEvent;

        #region Public Methods

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

            var command = _connection.CreateDatabaseCommand(query, transaction);
            AddParametersToCommand(command, parameters);

            var results = new List<T>();
            var objectProperties = typeof(T).GetProperties();

            using (var reader = command.ExecuteReader())
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
            var sCommand = _connection.CreateDatabaseCommand(command, transaction);
            AddParametersToCommand(sCommand, parameters);
            return sCommand.ExecuteNonQuery();
        }

        public long ExecuteScalar(
            IDatabaseTransactionWrapper transaction, string command, object? parameters = null)
        {
            ExecuteNonQuery(transaction, command, parameters);

            command = _connection.GetSelectIdentityCommand();
            var sCommand = _connection.CreateDatabaseCommand(command, transaction);
            var result = sCommand.ExecuteScalar() ?? throw new SQLiteLibraryException("Null result from ExecuteScalar");
            var resultAsInt = (long)(decimal)result;
            return resultAsInt;
        }

        public void Close()
        {
            _connection.Close();
        }

        #endregion

        #region Support Code

        private static void AddParametersToCommand(IDatabaseCommand command, object? parameters)
        {
            if (parameters != null)
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var value = prop.GetValue(parameters, null);

                    command.AddParameter("@" + name, value ?? DBNull.Value);
                }
            }
        }

        private static string? GetString(IDatabaseDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            return reader.GetString(ordinal);
        }

        private static DateTime GetDateTime(IDatabaseDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return DateTime.MinValue;
            return reader.GetDateTime(ordinal);
        }

        public IDatabaseTransactionWrapper CreateTransaction()
        {
           return _connection.CreateTransaction();
        }

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
