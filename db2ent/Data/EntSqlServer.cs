using db2ent.Misc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace db2ent.Data
{
    /// <summary>
    /// EntConnector inherited class to specifically address SQL Server tables
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
            this.connection?.Dispose();
            return true;
        }

        public bool OpenConnection()
        {
            this.connection = new SqlConnection(this.connectionString);
            this.connection.Open();
            return true;
        }

        public string TableToEntity(string tableName, int numRecords)
        {
            var sb = new StringBuilder($"var {tableName.ToLower()}List = new List<{tableName}>()");
            sb.AppendLine("\n{");

            using var cmd = new SqlCommand($"SELECT TOP({numRecords})* FROM {tableName}", connection);

            using var da = new SqlDataAdapter(cmd);
            using var dt = new DataTable(tableName);

            da.Fill(dt);
            da.FillSchema(dt, SchemaType.Mapped);

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                sb.AppendLine($"\tnew {tableName}()");
                sb.AppendLine("\t{");

                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    sb.Append("\t\t")
                      .Append(dt.Columns[colIndex].ColumnName)
                      .Append(" = ")
                      .Append(dt.Rows[rowIndex].ItemArray[colIndex].ToTypedString(dt.Columns[colIndex].DataType))
                      .AppendLine(",");
                }

                sb.AppendLine("\t},");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
