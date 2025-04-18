using DatabaseAccess.Library.Placeholders;
using DatabaseAccessInterfaces;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace SQLDatabaseAccess.Library
{
    internal class SQLiteWrapper : ISQLiteWrapper, IDisposable
    {
        readonly IDBConnection _connection;

        public event LogSQLiteCommandDelegate? LogSQLiteCommandEvent;

        #region Public Methods

        public SQLiteWrapper(IDBConnection connection)
        {
            _connection = connection;
        }

        public List<T> Select<T>(
            IDBTransaction? transaction,
            string query,
            string? where,
            object? parameters) where T : new()
        {
            if (where is not null)
            {
                query = query + " WHERE " + where;
            }

            var command = CreateCommand(query, transaction);
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
            IDBTransaction? transaction, string command, object? parameters)
        {
            RaiseLogSQLiteCommandEvent(command, parameters);
            var sCommand = CreateCommand(command, transaction);
            AddParametersToCommand(sCommand, parameters);
            return sCommand.ExecuteNonQuery();
        }

        public long ExecuteScalar(
            IDBTransaction transaction, string command, object? parameters = null)
        {
            ExecuteNonQuery(transaction, command, parameters);

            command = "; SELECT last_insert_rowid()";
            var sCommand = CreateCommand(command, transaction);
            var result = sCommand.ExecuteScalar() ?? throw new SQLiteLibraryException("Null result from ExecuteScalar");
            var resultAsInt = (long)result;
            return resultAsInt;
        }

        public void Close()
        {
            _connection.Close();
        }

        #endregion

        #region Support Code

        private SqlCommand CreateCommand(string commandText, IDBTransaction? transaction)
        {
            return _connection.CreateCommand(commandText, transaction);
        }

        private static void AddParametersToCommand(SqlCommand command, object? parameters)
        {
            if (parameters != null)
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var value = prop.GetValue(parameters, null);

                    var parameter = new SqliteParameter("@" + name, value ?? DBNull.Value);
                    command.Parameters.Add(parameter);
                }
            }
        }

        private static string? GetString(SqlDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            return reader.GetString(ordinal);
        }

        private static DateTime GetDateTime(SqlDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return DateTime.MinValue;
            return reader.GetDateTime(ordinal);
        }

        public IDBTransaction CreateTransaction()
        {
            return _connection.BeginTransaction();
        }

        public void Commit(IDBTransaction transaction)
        {
            transaction.Commit();
        }

        public void Rollback(IDBTransaction transaction)
        {
            transaction.Rollback();
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
