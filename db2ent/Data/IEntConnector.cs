﻿using System.Data;

namespace db2ent.Data
{
    /// <summary>
    /// Generic interface for data access classes
    /// </summary>
    public interface IEntConnector
    {
        /// <summary>
        /// Establish and opens a connection to database
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection();
        
        /// <summary>
        /// Close an opened database connection
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection();
        
        /// <summary>
        /// Queries a table and manages to create a datatable of strongly typed entities
        /// </summary>
        /// <param name="tableName">The table name to be processed</param>
        /// <returns></returns>
        public DataTable TableToEntity(string tableName, int numRecords, string whereClause = "");
    }
}
