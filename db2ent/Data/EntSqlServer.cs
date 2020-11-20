using System;
using System.Data;
using System.Data.SqlClient;

namespace db2ent.Data
{
    /// <summary>
    /// EntConnector inherited class to specifically address SQL Server tables
    /// To implement other providers, new classes must inherit from IEntConnector interface
    /// Referer to IEntConnector interface for informations
    /// </summary>
    public class EntSqlServer: IEntConnector
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public EntSqlServer(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CloseConnection()
        {
            this.connection?.Dispose();
            return true;
        }

        public bool OpenConnection()
        {
            this.connection = new SqlConnection(this.connectionString);
            
            try
            {
                this.connection.Open();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable TableToEntity(string tableName, int numRecords, string whereClause = "")
        {
            using var cmd = new SqlCommand($@"SET NOCOUNT ON
                                              SELECT TOP({numRecords})* FROM {tableName} {whereClause}", connection);

            using var da = new SqlDataAdapter(cmd);
            using var dt = new DataTable(tableName);
            
            try
            {
                da.Fill(dt);
                da.FillSchema(dt, SchemaType.Mapped);
            }
            catch { }

            return dt;
        }
    }
}
