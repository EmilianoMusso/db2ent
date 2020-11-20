using db2ent.Misc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace db2ent.Data
{
    /// <summary>
    /// Generic class to access data operations. Referer to EntConnector interface for informations
    /// </summary>
    public class EntGenericDb : IEntConnector
    {
        IEntConnector _connector;

        public EntGenericDb(IEntConnector connector)
        {
            this._connector = connector;
        }
        public bool CloseConnection()
        {
            return this._connector.CloseConnection();
        }

        public bool OpenConnection()
        {
            return this._connector.OpenConnection();
        }

        public DataTable TableToEntity(string tableName, int numRecords, string whereClause = "")
        {
            var completeWhere = string.IsNullOrEmpty(whereClause) ? "" : "WHERE " + whereClause;
            
            using var dt = this._connector.TableToEntity(tableName, numRecords, completeWhere);
            using var da = new SqlDataAdapter();

            try
            {
                da.Fill(dt);
                da.FillSchema(dt, SchemaType.Mapped);
            }
            catch { }

            return dt;
        }

        /// <summary>
        /// Returns a textual representation of class and strongly typed entities
        /// </summary>
        /// <param name="tableName">The name of table to work on</param>
        /// <param name="numRecords">The number of record to extract</param>
        /// <param name="whereClause">Optional parameters for WHERE clause (beware: prone to injection)</param>
        /// <returns></returns>
        public string ToString(string tableName, int numRecords, string whereClause = "")
        {
            using var dt = this.TableToEntity(tableName, numRecords, whereClause);
            if (dt == null) return "// Cannot retrieve DataTable";

            var sb = new StringBuilder($"var {tableName.ToLower()}List = new List<{tableName}>()")
                .AppendLine("\n{")
                .AppendLine(dt.DataTableToString())
                .AppendLine("};")
                .AppendLine(dt.SchemaToClass());

            return sb.ToString();
        }
    }
}
