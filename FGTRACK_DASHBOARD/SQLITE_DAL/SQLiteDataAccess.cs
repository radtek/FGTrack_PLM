using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace HTN.BITS.SQLITE.DAL
{
    public abstract class SQLiteDataAccess : ISQLiteBackend
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
        private SQLiteConnection currentConnection = null;
        private SQLiteTransaction currentTransaction = null;

        protected readonly char[] TrimChars = new char[] { ':', '@', '?' };
        protected bool isConnected = false;

        protected Exception lastException;
        //protected TimeSpan executeTime;

        #endregion / Member Variables /

        #region // Constructor //
        public SQLiteDataAccess()
        {
        }
        #endregion / Constructor /

        #region ISQLiteBackendSource Members

        public virtual string ConnectionString
        {
            get { return this.conString; }
            set { this.conString = value; }
        }

        public virtual SQLiteConnection Connection
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

        //public virtual TimeSpan ExecuteTime
        //{
        //    get
        //    {
        //        return this.executeTime;
        //    }
        //    set
        //    {
        //        this.executeTime = value;
        //    }
        //}

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
        public SQLiteTransaction BeginTransaction()
        {
            return this._beginTransaction(IsolationLevel.Unspecified, false);
        }
        public SQLiteTransaction BeginTransaction(IsolationLevel isolationLevel)
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

        public abstract DataTable GetDataTable(string query);
        public abstract DataTable GetDataTable(SQLiteParam procParam);
        public abstract DataTable GetDataTable(string query, string tableName);
        public abstract DataTable GetDataTable(SQLiteParam procParam, string tableName);

        public abstract int ExecuteNonQuery(string query);
        public abstract int ExecuteNonQuery(SQLiteParam procParam);
        public abstract int ExecuteNonQuery(List<SQLiteParam> procList);

        public abstract object ExecuteScalar(string query);
        public abstract object ExecuteScalar(SQLiteParam procParam);

        public abstract SQLiteDataReader ExecuteDataReader(string query);
        public abstract SQLiteDataReader ExecuteDataReader(SQLiteParam procParam);


        // *** Public ***
        // IBackend Members

        #endregion / Abstract Functions /

        #region // Helper Functions - Protected //
        protected SQLiteCommand _getCommand(string sqlCommand)
        {
            if (this.Connection == null) return null;

            SQLiteCommand ret = new SQLiteCommand();

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

        protected SQLiteCommand _getCommand(SQLiteParam param)
        {
            if (this.Connection == null) return null;

            SQLiteCommand ret = new SQLiteCommand();

            ret.CommandText = param.CommandText;
            ret.CommandType = CommandType.Text;
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

        protected SQLiteDataReader _getReader(string sqlCommand)
        {
            if (this.Connection == null) return null;

            SQLiteCommand ret = new SQLiteCommand();

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

        protected SQLiteDataReader _getReader(SQLiteParam param)
        {
            if (this.Connection == null) return null;

            SQLiteCommand ret = new SQLiteCommand();

            ret.CommandType = CommandType.Text;
            ret.CommandText = param.CommandText;
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
        private void SetParaMeterToCommand(ref SQLiteCommand command, SQLiteParameter[] parameters)
        {
            command.Parameters.Clear();

            foreach (SQLiteParameter objPara in parameters)
            {
                command.Parameters.Add(objPara);
            }
        }

        object transLock = new object();
        private SQLiteTransaction _beginTransaction(IsolationLevel il, bool useIL)
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
