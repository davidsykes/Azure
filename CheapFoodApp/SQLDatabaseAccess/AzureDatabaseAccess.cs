using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.Commands;
using Microsoft.Data.SqlClient;
using SQLDatabaseAccess.AzureImplementations;
using SQLDatabaseAccess.Library;

namespace SQLDatabaseAccess
{
    public class AzureDatabaseAccess : IDatabaseAccessImplementation
    {
        readonly string _connectionString = "Server=tcp:cheapfooddbserver.database.windows.net,1433;Initial Catalog=CheapFoodDb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
        //private ISQLiteWrapper? _wrapper;

        public List<string> GetTableNames()
        {
            var rows = new List<string>();

            using var conn = new SqlConnection(_connectionString);
            TryOpenConnection(conn);

            var command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", conn);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rows.Add(reader.GetString(0));
                }
            }

            return rows;
        }

        private static void TryOpenConnection(SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch(Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("needs re-authentication"))
                {
                    throw new SQLiteLibraryException("Application needs re-authentication");
                }
                if (ex.Message.Contains("Connection Timeout Expired"))
                {
                    throw new SQLiteLibraryException("Connection Timeout Expired");
                }
                throw;
            }
        }

        public void CreateFoodsTable()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string tableCommand = @"CREATE TABLE Foods (Id INT IDENTITY(1,1) PRIMARY KEY, Name TEXT NOT NULL);";

            using var command = new SqlCommand(tableCommand, conn);
            command.ExecuteNonQuery();
        }

        public void CreateSupermarketsTable()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string tableCommand = @"CREATE TABLE Supermarkets (Id INT IDENTITY(1,1) PRIMARY KEY, Name TEXT NOT NULL);";

            using var command = new SqlCommand(tableCommand, conn);
            command.ExecuteNonQuery();
        }

        public void AddNewFood(DatabaseString name)
        {
            var query = $"INSERT INTO Foods(Name) VALUES(@Name)";

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Name", name.ToString());
            command.ExecuteNonQuery();
        }

        public List<T> Query<T>(string query) where T : new()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            ISQLiteWrapper _wrapper = new SQLiteWrapper(new AzureDBConnection(conn));

            var foodItems = _wrapper.Select<T>(null, query);

            return foodItems;
        }

        public void AddNewSupermarket(DatabaseString name)
        {
            var query = $"INSERT INTO Supermarkets(Name) VALUES(@Name)";

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Name", name.ToString());
            command.ExecuteNonQuery();
        }

        public void ExecuteCommand(DatabaseCommand command)
        {
            throw new NotImplementedException();
        }

        //public List<string> GetTestData()
        //{
        //    var rows = new List<string>();
        //    try
        //    {
        //        using var conn = new SqlConnection(_connectionString);
        //        conn.Open();

        //        var command = new SqlCommand("SELECT * FROM Persons", conn);
        //        using SqlDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rows.Add("error " + ex.Message );
        //    }

        //    return rows;
        //}
    }
}
