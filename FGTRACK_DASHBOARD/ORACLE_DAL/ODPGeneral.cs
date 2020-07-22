using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace HTN.BITS.ORACLE.DAL
{
    #region // Delegates //
    public delegate void ConnectionStateChangeEventHandler(ConnectionState state);
    public delegate void CommandExecutionStateEventHandler(CommandExecutionStatus status);
    #endregion / Delegates /

    #region // Interfaces //
    public interface IODPBackendSource : IDisposable
    {
        string ConnectionString
        { get; set; }

        OracleConnection Connection
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

    public interface IODPBackend : IODPBackendSource
    {
        DataTable GetDataTable(string query);
        DataTable GetDataTable(string query, string tableName);
        DataTable GetDataTable(ProcParam procParam);
        DataTable GetDataTable(ProcParam procParam, string tableName);

        int ExecuteNonQuery(string query);
        int ExecuteNonQuery(ProcParam procParam);
        int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList, int keyHeader, int keyDetail);
        int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList);
        int ExecuteNonQuery(List<ProcParam> procList);

        object ExecuteScalar(string query);
        object ExecuteScalar(ProcParam procParam);

        OracleDataReader ExecuteDataReader(string query);
        OracleDataReader ExecuteDataReader(ProcParam procParam);
        OracleDataReader ExecuteDataReaderRefCur(string procname, ProcParam procParam, int indexRefCur);

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
