using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Diagnostics;


namespace HTN.BITS.DAL
{
    public class OracleDB : ODPDataAccess
    {
        #region // Member Variables //
        //OracleConnection con = null;
        private DateTime executionStartTime; //Var will hold Execution Starting Time
        private DateTime executionStopTime;//Var will hold Execution Stopped Time
        //private TimeSpan executionTime;//Var will count Total Execution Time-Our Main Hero

        #endregion / Member Variables /

        #region // Constructor //
        public OracleDB()
            : base()
        {
        }
        #endregion / Constructor /

        #region // Properties //
        public override OracleConnection Connection
        {
            get { return base.Connection; }
        }
        #endregion / Properties /

        #region // Functions - Protected //
        protected override void createConnection()
        {
            base.Connection = new OracleConnection();

            if (OracleConnection.IsAvailable)
            {
                base.Connection.ConnectionString = "context connection=true";
            }
            else
            {
                base.Connection.ConnectionString = this.ConnectionString;
            }
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
                OracleConnection.ClearPool(base.Connection);
            }
        }

        #endregion / Functions - Protected /

        #region // Functions - Public //
        public override DataTable GetDataTable(string query)
        {
            Stopwatch _stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                dtb.Load(reader);

                reader.Dispose();
                _stopwatch.Stop();
                dtb.EndLoadData();

                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(string query, string tableName)
        {
            Stopwatch _stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                dtb.Load(reader);

                reader.Dispose();
                _stopwatch.Stop();
                dtb.EndLoadData();

                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam)
        {
            Stopwatch _stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                dtb.Load(reader);

                reader.Dispose();
                _stopwatch.Stop();
                dtb.EndLoadData();

                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam, string tableName)
        {
            Stopwatch _stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                dtb.BeginLoadData();
                _stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                dtb.Load(reader);

                reader.Dispose();
                _stopwatch.Stop();
                dtb.EndLoadData();

                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return dtb;
        }

        //add new for check connection 04-10-2013 17:53 by Jack
        public override void CheckIsConnection(string query)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;

            try
            {
                _stopwatch = new Stopwatch();

                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    _stopwatch.Start();

                    cmd.ExecuteNonQuery();

                    _stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }
            }
            catch (MSDataLayerException tEx)
            {
                throw tEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public override int ExecuteNonQuery(string query)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                this.executionStartTime = DateTime.Now;
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

                this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx; 
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex; 
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                this.executionStartTime = DateTime.Now;
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

                this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList, int keyHeader, int keyDetail)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;
            object objKeyHeader;

            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    _stopwatch.Start();
                    ret = cmd.ExecuteNonQuery();
                    _stopwatch.Stop();

                    if (procParam.IndexToCheck != -1)
                    {
                        string resultMessage = procParam.ReturnValue(procParam.IndexToCheck).ToString();
                        if (!procParam.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    objKeyHeader = procParam.ReturnValue(keyHeader);

                    if (procList != null)
                    {
                        string resultMessage = string.Empty;
                        OracleCommand commandDetail;
                        foreach (ProcParam paramDetail in procList)
                        {
                            paramDetail.Parameters[keyDetail].Value = objKeyHeader;

                            commandDetail = base._getCommand(paramDetail);

                            _stopwatch.Reset();

                            _stopwatch.Start();
                            ret = commandDetail.ExecuteNonQuery();
                            _stopwatch.Stop();

                            if (paramDetail.IndexToCheck != -1)
                            {
                                resultMessage = paramDetail.ReturnValue(paramDetail.IndexToCheck).ToString();
                                if (!paramDetail.MessageToCheck.Equals(resultMessage))
                                {
                                    //Problem case
                                    throw new Exception(resultMessage);
                                }
                            }

                        }
                    }

                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    _stopwatch.Start();
                    ret = cmd.ExecuteNonQuery();
                    _stopwatch.Stop();

                    if (procParam.IndexToCheck != -1)
                    {
                        string resultMessage = procParam.ReturnValue(procParam.IndexToCheck).ToString();
                        if (!procParam.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    if (procList != null)
                    {
                        string resultMessage = string.Empty;
                        OracleCommand commandDetail;
                        foreach (ProcParam paramDetail in procList)
                        {
                            commandDetail = base._getCommand(paramDetail);

                            _stopwatch.Reset();

                            _stopwatch.Start();
                            ret = commandDetail.ExecuteNonQuery();
                            _stopwatch.Stop();

                            if (paramDetail.IndexToCheck != -1)
                            {
                                resultMessage = paramDetail.ReturnValue(paramDetail.IndexToCheck).ToString();
                                if (!paramDetail.MessageToCheck.Equals(resultMessage))
                                {
                                    //Problem case
                                    throw new Exception(resultMessage);
                                }
                            }

                        }
                    }

                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }

                this.lastException = null;
                base.CommitTransaction();

                this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(List<ProcParam> procList)
        {
            Stopwatch _stopwatch = null;
            OracleCommand commandDetail = null;
            int ret = 0;


            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                string resultMessage = string.Empty;

                base.BeginTransaction();
                
                foreach (ProcParam paramDetail in procList)
                {
                    commandDetail = base._getCommand(paramDetail);

                    _stopwatch.Reset();

                    _stopwatch.Start();
                    ret += commandDetail.ExecuteNonQuery();
                    _stopwatch.Stop();

                    if (paramDetail.IndexToCheck != -1)
                    {
                        resultMessage = paramDetail.ReturnValue(paramDetail.IndexToCheck).ToString();
                        if (!paramDetail.MessageToCheck.Equals(resultMessage))
                        {
                            //Problem case
                            throw new Exception(resultMessage);
                        }
                    }

                }

                this.lastException = null;
                base.CommitTransaction();

                this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                base.RollbackTransaction();
                throw tEx;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                commandDetail.Dispose();
                commandDetail = null;
            }

            return ret;
        }

        public override object ExecuteScalar(string query)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            object objScalar = null;

            try
            {
                this.executionStartTime = DateTime.Now;
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

                this.executionStopTime = DateTime.Now;
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
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;
        }

        public override object ExecuteScalar(ProcParam procParam)
        {
            Stopwatch _stopwatch = null;
            OracleCommand cmd = null;
            object objScalar = null;

            try
            {
                this.executionStartTime = DateTime.Now;
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

                this.executionStopTime = DateTime.Now;
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
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;

            throw new NotImplementedException();
        }

        public override OracleDataReader ExecuteDataReader(string query)
        {
            Stopwatch _stopwatch = null;
            OracleDataReader reader = null;

            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                _stopwatch.Start();

                reader = base._getReader(query);

                _stopwatch.Stop();
                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return reader;
        }

        public override OracleDataReader ExecuteDataReader(ProcParam procParam)
        {
            Stopwatch _stopwatch = null;
            OracleDataReader reader = null;

            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                _stopwatch.Start();

                reader = base._getReader(procParam);

                _stopwatch.Stop();
                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return reader;
        }

        public override OracleDataReader ExecuteDataReaderRefCur(string procname, ProcParam procParam, int indexRefCur)
        {
            Stopwatch _stopwatch = null;
            OracleDataReader reader = null; ;

            try
            {
                this.executionStartTime = DateTime.Now;
                _stopwatch = new Stopwatch();

                _stopwatch.Start();
                OracleCommand cmd = base._getCommand(procParam);
                _stopwatch.Stop();
                OracleRefCursor refCur = (OracleRefCursor)procParam.Parameters[indexRefCur].Value;
                reader = refCur.GetDataReader();

                this.executionStopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            finally
            {
                base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                _stopwatch = null;
            }

            return reader;
        }

        #endregion / Functions - Public /

    }
}
