using db2ent.Misc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace db2ent.Data
{
    /// <summary>
    /// EntConnector inherited class to specifically address SQL Server tables
    /// Referer to EntConnector interface for informations
    /// </summary>
    /// </summary>
    public class EntSqlServer: EntConnector
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public EntSqlServer(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CloseConnection()
        {
            try
            {
                this.connection?.Dispose();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool OpenConnection()
        {
            try
            {
                this.connection = new SqlConnection(this.connectionString);
                this.connection.Open();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string TableToEntity(string tableName, int numRecords, string whereClause = "")
        {
            try
            {
                var sb = new StringBuilder($"var {tableName.ToLower()}List = new List<{tableName}>()");
                sb.AppendLine("\n{");

                var completeWhere = string.IsNullOrEmpty(whereClause) ? "" : "WHERE " + whereClause;
                using var cmd = new SqlCommand($"SELECT TOP({numRecords})* FROM {tableName} {completeWhere}", connection);

                using var da = new SqlDataAdapter(cmd);
                using var dt = new DataTable(tableName);

                da.Fill(dt);
                da.FillSchema(dt, SchemaType.Mapped);

                // Extension method to process datatable to POCO objects
                sb.AppendLine(dt.DataTableToString());

                sb.AppendLine("}");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
