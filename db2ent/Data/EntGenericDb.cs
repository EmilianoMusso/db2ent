namespace db2ent.Data
{
    /// <summary>
    /// Generic class to access data operations. Referer to EntConnector interface for informations8
    /// </summary>
    public class EntGenericDb : EntConnector
    {
        EntConnector _connector;

        public EntGenericDb(EntConnector connector)
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

        public string TableToEntity(string tableName, int numRecords)
        {
            return this._connector.TableToEntity(tableName, numRecords);
        }
    }
}
