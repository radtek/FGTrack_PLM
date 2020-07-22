using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace HTN.BITS.SQLITE.DAL
{
    #region // Delegates //

    public delegate void ConnectionStateChangeEventHandler(ConnectionState state);
    public delegate void CommandExecutionStateEventHandler(CommandExecutionStatus status);

    #endregion / Delegates /

    #region // Interfaces //

    public interface ISQLiteBackendSource : IDisposable
    {
        string ConnectionString
        { get; set; }

        SQLiteConnection Connection
        { get; }

        bool IsConnected
        { get; }

        Exception LastException
        { get; }

        bool Connect(bool reconnect);
        bool Connect(string conString, bool reconnect);
        bool Disconnect();

        event ConnectionStateChangeEventHandler ConnectionStateChanged;
        event CommandExecutionStateEventHandler CommandExecutionState;
    }

    public interface ISQLiteBackend : ISQLiteBackendSource
    {
        DataTable GetDataTable(string query);
        DataTable GetDataTable(SQLiteParam procParam);
        DataTable GetDataTable(string query, string tableName);
        DataTable GetDataTable(SQLiteParam procParam, string tableName);

        int ExecuteNonQuery(string query);
        int ExecuteNonQuery(SQLiteParam procParam);
        int ExecuteNonQuery(List<SQLiteParam> procList);

        object ExecuteScalar(string query);
        object ExecuteScalar(SQLiteParam procParam);

        SQLiteDataReader ExecuteDataReader(string query);
        SQLiteDataReader ExecuteDataReader(SQLiteParam procParam);

        void CommitTransaction();
        void RollbackTransaction();
    }

    #endregion / Interfaces /

    #region // Enumerations //
    public enum AppendOptions
    {
        None = 0,
        First,
        Last,
        Both
    }
    // This enum might be going to be help full in a multi threaded data access situations
    // and analysis/profiling purposes
    public enum CommandExecutionStatus
    {
        None = 0,
        Started,
        Finished,
        Aborted
    }
    #endregion / Enumerations /

    #region // Exception Classes //
    public class MSDataLayerException : ApplicationException
    {
        private string msg;
        public MSDataLayerException(string message)
            : base(message)
        {
            this.msg = message;
        }
    }
    #endregion / Exception Classes /
}
