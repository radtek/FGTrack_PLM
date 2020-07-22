using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Data.SQLite;


namespace HTN.BITS.SQLITE.DAL
{
    public class SQLiteDB : SQLiteDataAccess
    {
        #region // Member Variables //
        //OracleConnection con = null;
        //private DateTime executionStartTime; //Var will hold Execution Starting Time
        //private DateTime executionStopTime;//Var will hold Execution Stopped Time
        ////private TimeSpan executionTime;//Var will count Total Execution Time-Our Main Hero

        #endregion / Member Variables /

        #region // Constructor //
        public SQLiteDB()
            : base()
        {
        }
        #endregion / Constructor /

        #region // Properties //
        public override SQLiteConnection Connection
        {
            get { return base.Connection; }
        }
        #endregion / Properties /

        #region // Functions - Protected //
        protected override void createConnection()
        {
            base.Connection = new SQLiteConnection();
            base.Connection.ConnectionString = this.ConnectionString;
            base.Connection.ParseViaFramework = true;
        }

        protected override void disconnection()
        {
            try
            {
                if (base.Connection.State == ConnectionState.Open)
                {
                    base.Connection.Close();
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                SQLiteConnection.ClearPool(base.Connection);
            }
        }

        #endregion / Functions - Protected /

        #region // Functions - Public //
        public override DataTable GetDataTable(string query)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter dap = null;
            DataTable dtb = new DataTable();
            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                cmd = base._getCommand(query);
                dap = new SQLiteDataAdapter(cmd);
                dap.Fill(dtb);

                _stopwatch.Stop();
                dtb.EndLoadData();

                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;

                dap.Dispose();
                dap = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(SQLiteParam procParam)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter dap = null;
            DataTable dtb = new DataTable();
            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                cmd = base._getCommand(procParam);
                dap = new SQLiteDataAdapter(cmd);
                dap.Fill(dtb);

                _stopwatch.Stop();
                dtb.EndLoadData();

                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;

                dap.Dispose();
                dap = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(string query, string tableName)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter dap = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                cmd = base._getCommand(query);
                dap = new SQLiteDataAdapter(cmd);
                dap.Fill(dtb);

                _stopwatch.Stop();
                dtb.EndLoadData();

                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;

                dap.Dispose();
                dap = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(SQLiteParam procParam, string tableName)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter dap = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                cmd = base._getCommand(procParam);
                dap = new SQLiteDataAdapter(cmd);
                dap.Fill(dtb);

                _stopwatch.Stop();
                dtb.EndLoadData();

                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;

                dap.Dispose();
                dap = null;
            }

            return dtb;
        }


        public override int ExecuteNonQuery(string query)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    _stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    _stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                //this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = SQLiteDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(SQLiteParam procParam)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    _stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    _stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                //this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = SQLiteDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(List<SQLiteParam> procList)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand commandDetail = null;
            int ret = 0;
            int checkResult = 0;


            try
            {
                _stopwatch = new Stopwatch();

                string resultMessage = string.Empty;

                base.BeginTransaction();

                foreach (SQLiteParam paramDetail in procList)
                {
                    commandDetail = base._getCommand(paramDetail);

                    _stopwatch.Reset();

                    _stopwatch.Start();

                    checkResult = commandDetail.ExecuteNonQuery();

                    if (checkResult <= 0) throw new Exception("ExecuteNonQuery Error!!");

                    ret += checkResult;
                    _stopwatch.Stop();
                }

                this.lastException = null;
                base.CommitTransaction();
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = SQLiteDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                _stopwatch = null;

                commandDetail.Dispose();
                commandDetail = null;
            }

            return ret;
        }

        public override object ExecuteScalar(string query)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            object objScalar = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();
                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    _stopwatch.Start();
                    objScalar = cmd.ExecuteScalar();
                    _stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                //this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                objScalar = null;
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                objScalar = null;
                this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;
        }

        public override object ExecuteScalar(SQLiteParam procParam)
        {
            Stopwatch _stopwatch = null;
            SQLiteCommand cmd = null;
            object objScalar = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();
                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    _stopwatch.Start();
                    objScalar = cmd.ExecuteScalar();
                    _stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                //this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                objScalar = null;
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                objScalar = null;
                this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;

            throw new NotImplementedException();
        }

        public override SQLiteDataReader ExecuteDataReader(string query)
        {
            Stopwatch _stopwatch = null;
            SQLiteDataReader reader = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                _stopwatch.Start();

                reader = base._getReader(query);

                _stopwatch.Stop();
                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return reader;
        }

        public override SQLiteDataReader ExecuteDataReader(SQLiteParam procParam)
        {
            Stopwatch _stopwatch = null;
            SQLiteDataReader reader = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                _stopwatch.Start();

                reader = base._getReader(procParam);

                _stopwatch.Stop();
                //this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {

                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return reader;
        }

        #endregion / Functions - Public /

    }
}
