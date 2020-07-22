using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN.BITS.FGTRACK.DAL
{
    public abstract class ODPDataAccess : IODPBackend
    {
        #region // Events //

        public virtual event ConnectionStateChangeEventHandler ConnectionStateChanged;
        public virtual event CommandExecutionStateEventHandler CommandExecutionState;

        #endregion / Events /

        #region // Constants //

        public const int ERROR_RESULT = int.MaxValue;

        #endregion / Constants /

        #region // Member Variables //

        private string conString;
        private bool isTransactionActive = false;
        private OracleConnection currentConnection = null;
        private OracleTransaction currentTransaction = null;

        protected readonly char[] TrimChars = new char[] { ':', '@', '?' };
        protected bool isConnected = false;

        protected Exception lastException;
        protected TimeSpan executeTime;

        #endregion / Member Variables /

        #region // Constructor //

        public ODPDataAccess()
        {
        }

        #endregion / Constructor /

        #region IODPBackendSource Members

        public virtual string ConnectionString
        {
            get { return this.conString; }
            set { this.conString = value; }
        }

        public virtual OracleConnection Connection
        {
            get { return this.currentConnection; }
            set { this.currentConnection = value; }
        }

        public virtual bool IsConnected
        {
            get
            {
                bool ret = false;
                if (this.Connection != null) ret = (this.Connection.State == ConnectionState.Open);
                return ret;
            }
        }

        public virtual Exception LastException
        {
            get { return this.lastException; }
        }

        public virtual TimeSpan ExecuteTime
        {
            get
            {
                return this.executeTime;
            }
            set
            {
                this.executeTime = value;
            }
        }

        public virtual bool Connect(bool reconnect)
        {
            return this.Connect(this.ConnectionString, reconnect);
        }

        public virtual bool Connect(string conString, bool reconnect)
        {
            bool ret = true;
            bool recreated = false;
            try
            {
                this.ConnectionString = conString;

                if (reconnect)
                {
                    if (this.Connection != null)
                    {
                        this.Disconnect();
                        this.Connection.ConnectionString = this.ConnectionString;
                        this.Connection.Open();
                    }
                    else
                    {
                        this.createConnection();
                        this.Connection.Open();
                        recreated = true;
                    }
                }
                else
                {
                    this.createConnection();
                    this.Connection.Open();
                    recreated = true;
                }

                this.SetGlobalizationParams();

                if (recreated && this.Connection != null)
                    this.Connection.StateChange += new StateChangeEventHandler(connStateChanged);
            }
            catch (Exception ex)
            {
                this.lastException = ex;
                ret = false;
            }

            return ret;
        }

        private bool SetGlobalizationParams()
        {
            try
            {
                // Get the default thread setting 
                OracleGlobalization threadGlob = OracleGlobalization.GetThreadInfo();

                //modify the NLS_LANGUAGE parameter AMERICAN_AMERICA
                threadGlob.Language = "AMERICAN";  // "THAI";

                // modify the NLS_TERRITORY parameter
                threadGlob.Territory = "AMERICA"; // "THAILAND";

                // modify the NLS_DATE_FORMAT parameter
                threadGlob.DateFormat = "Day:Dd Month yyyy";//"dd/MM/yyyy";

                // set the modified NLS parameters for thread
                //Thread's NLS settings are used by any data retrieved
                //as .NET String type.
                OracleGlobalization.SetThreadInfo(threadGlob);

                //Get session's default NLS settings
                OracleGlobalization sessionGlob = this.Connection.GetSessionInfo();

                // modify the NLS_TERRITORY parameter
                sessionGlob.Territory = "AMERICA";//"THAI";

                // set the modified NLs parameters for session
                // Session's NLS settings are used by data retrieved using
                //TO_CHAR function used in SELECT statements.
                this.Connection.SetSessionInfo(sessionGlob);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public virtual bool Disconnect()
        {
            if (this.Connection == null) return false;

            try
            {
                if (this.isTransactionActive) this.currentTransaction.Dispose();
            }
            catch { }
            finally
            {
                this.currentTransaction = null;
                this.isTransactionActive = false;
            }

            if (this.Connection.State != ConnectionState.Closed)
            {
                try
                {
                    this.Connection.Close();
                    this.disconnection();
                }
                catch { }
                try
                {
                    this.Connection.Dispose();
                    OracleConnection.ClearAllPools();
                }
                catch { }
            }
            return true;
        }

        public virtual void Dispose()
        {
            this.Disconnect();
        }

        #endregion

        #region // Properties - (Non IBackendSource/IBackend Members) //

        protected bool IsTransactionActive
        {
            get { return this.isTransactionActive; }
            private set { this.isTransactionActive = value; }
        }

        #endregion / Properties - (Non IBackendSource/IBackend Members) /

        #region // Public Functions //

        // *** Transaction Handling ***
        public OracleTransaction BeginTransaction()
        {
            return this._beginTransaction(IsolationLevel.Unspecified, false);
        }
        public OracleTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this._beginTransaction(isolationLevel, false);
        }
        public void CommitTransaction()
        {
            this._transCommitRollback(true);
        }
        public void RollbackTransaction()
        {
            this._transCommitRollback(false);
        }
        #endregion / Public Functions /

        #region // Abstract Functions //

        // *** Protected ***
        protected abstract void createConnection();
        protected abstract void disconnection();

        public abstract object CheckConnectDB(string query);

        public abstract DataTable GetDataTable(string query);
        public abstract DataTable GetDataTable(string query, long fetchsize);
        public abstract DataTable GetDataTable(string query, string tableName);
        public abstract DataTable GetDataTable(string query, string tableName, long fetchsize);
        public abstract DataTable GetDataTable(ProcParam procParam);
        public abstract DataTable GetDataTable(ProcParam procParam, long fetchsize);
        public abstract DataTable GetDataTable(ProcParam procParam, string tableName);
        public abstract DataTable GetDataTable(ProcParam procParam, string tableName, long fetchsize);

        public abstract int ExecuteNonQuery(string query);
        public abstract int ExecuteNonQuery(ProcParam procParam);
        public abstract int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList, int keyHeader, int keyDetail);
        public abstract int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList);
        public abstract int ExecuteNonQuery(List<ProcParam> procList);

        //Improve Performance
        public abstract int ExecuteNonQuery(ProcParam procParam, int arrBindCount);
        public abstract int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl, int arrBindCount, int keyHeader, int keyDetail);
        //Just one to one!
        public abstract int ExecuteNonQuery(ProcParam paramHdr, int arrBindCountHdr, ProcParam paramDtl, int arrBindCountDtl, int keyHeader, int keyDetail);
        public abstract int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl, int arrBindCount);
        public abstract int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2);
        public abstract int ExecuteNonQuery(ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2);
        public abstract int ExecuteNonQuery(ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2, ProcParam paramDtl3, int arrBindCount3);
        public abstract int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2, ProcParam paramDtl3, int arrBindCount3);

        public abstract object ExecuteScalar(string query);
        public abstract object ExecuteScalar(ProcParam procParam);
        public abstract OracleDataReader ExecuteDataReader(string query);
        public abstract OracleDataReader ExecuteDataReader(ProcParam procParam);
        public abstract OracleDataReader ExecuteDataReaderRefCur(string procname, ProcParam procParam, int indexRefCur);

        // *** Public ***
        // IBackend Members

        #endregion / Abstract Functions /

        #region // Helper Functions - Protected //
        protected OracleCommand _getCommand(string sqlCommand)
        {
            if (this.Connection == null) return null;

            OracleCommand ret = new OracleCommand();

            ret.CommandType = CommandType.Text;
            ret.CommandText = sqlCommand;
            ret.Connection = this.Connection;
            ret.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                {
                    ret.Transaction = this.currentTransaction;
                }
                else
                {
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
                }
            }

            return ret;
        }

        protected OracleCommand _getCommand(ProcParam param)
        {
            if (this.Connection == null) return null;

            OracleCommand ret = new OracleCommand();

            ret.CommandType = CommandType.StoredProcedure;
            ret.CommandText = param.ProcedureName;
            ret.Connection = this.Connection;
            ret.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                {
                    ret.Transaction = this.currentTransaction;
                }
                else
                {
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
                }
            }

            if (param.Parameters != null)
            {
                this.SetParaMeterToCommand(ref ret, param.Parameters);
            }

            return ret;
        }


        protected OracleDataReader _getReader(ProcParam param)
        {
            if (this.Connection == null) return null;

            OracleCommand ret = new OracleCommand();

            ret.CommandType = CommandType.StoredProcedure;
            ret.CommandText = param.ProcedureName;
            ret.Connection = this.Connection;
            ret.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                {
                    ret.Transaction = this.currentTransaction;
                }
                else
                {
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
                }
            }

            if (param.Parameters != null)
            {
                this.SetParaMeterToCommand(ref ret, param.Parameters);
            }

            return ret.ExecuteReader();
        }

        protected OracleDataReader _getReader(string sqlCommand)
        {
            if (this.Connection == null) return null;

            OracleCommand ret = new OracleCommand();

            ret.CommandType = CommandType.Text;
            ret.CommandText = sqlCommand;
            ret.Connection = this.Connection;
            ret.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                {
                    ret.Transaction = this.currentTransaction;
                }
                else
                {
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
                }
            }

            return ret.ExecuteReader();
        }
        #endregion / Helper Functions - Protected /

        #region // Helper Functions - Private //
        private void connStateChanged(object sender, StateChangeEventArgs e)
        {
            if (this.ConnectionStateChanged != null) this.ConnectionStateChanged(e.CurrentState);
        }
        private void cmdStateChanged(object sender, CommandExecutionStatus e)
        {
            if (this.CommandExecutionState != null) this.CommandExecutionState(CommandExecutionStatus.None);
        }
        private void SetParaMeterToCommand(ref OracleCommand command, OracleParameter[] parameters)
        {
            foreach (OracleParameter objPara in parameters)
            {
                command.Parameters.Add(objPara);
            }
        }

        object transLock = new object();
        private OracleTransaction _beginTransaction(IsolationLevel il, bool useIL)
        {
            Exception tex = null;

            lock (transLock)
            {
                try
                {
                    if (this.IsConnected)
                    {
                        if (!this.isTransactionActive)
                        {
                            this.currentTransaction = (useIL ? this.Connection.BeginTransaction(il) : this.Connection.BeginTransaction());
                            this.IsTransactionActive = true;
                        }
                        else
                            tex = new MSDataLayerException("A transaction is already being active.");
                    }
                    else
                        tex = new MSDataLayerException("Transaction cannot begin when connection is not established.");
                }
                catch (Exception ex)
                {
                    tex = ex;
                }
            }

            if (tex != null) throw tex;

            return this.currentTransaction;
        }
        private void _transCommitRollback(bool isCommit)
        {
            Exception tex = null;

            lock (transLock)
            {
                try
                {
                    if (this.currentTransaction != null)
                    {
                        try
                        {
                            if (isCommit)
                                this.currentTransaction.Commit();
                            else
                                this.currentTransaction.Rollback();

                            this.currentTransaction.Dispose();
                        }
                        catch (Exception ex)
                        {
                            tex = ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tex = ex;
                }
            }

            this.isTransactionActive = false;
            this.currentTransaction = null;

            if (tex != null) throw tex;
        }

        #endregion / Helper Functions - Private /

    }
}
