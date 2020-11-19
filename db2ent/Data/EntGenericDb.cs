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

        public string TableToEntity(string tableName, int numRecords, string whereClause = "")
        {
            return this._connector.TableToEntity(tableName, numRecords, whereClause);
        }
    }
}
