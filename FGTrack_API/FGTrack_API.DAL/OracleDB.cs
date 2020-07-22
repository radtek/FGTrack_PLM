using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN.BITS.FGTRACK.DAL
{
    public class OracleDB : ODPDataAccess
    {
        #region // Member Variables //
        //OracleConnection con = null;
        //private DateTime executionStartTime; //Var will hold Execution Starting Time
        //private DateTime executionStopTime;//Var will hold Execution Stopped Time
        ////private TimeSpan executionTime;//Var will count Total Execution Time-Our Main Hero

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
        public override object CheckConnectDB(string query)
        {
            ////Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            object objScalar = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                ////_stopwatch = new //Stopwatch();

                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    ////_stopwatch.Start();
                    objScalar = cmd.ExecuteScalar();
                    ////_stopwatch.Stop();
                }
                else
                {
                    throw new MSDataLayerException("Command cannot be created since connection is not initialized.");
                }
                //this.executionStopTime = DateTime.Now;
            }
            catch (MSDataLayerException tEx)
            {
                objScalar = null;
                throw tEx;
            }
            catch (Exception ex)
            {
                objScalar = null;
                throw ex;
            }
            finally
            {
                ////base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                ////_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;
        }


        public override DataTable GetDataTable(string query)
        {
            ////Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                ////this.executionStartTime = DateTime.Now;
                ////_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                ////_stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                dtb.Load(reader);

                reader.Dispose();
                ////_stopwatch.Stop();
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
                ////_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(string query, long fetchsize)
        {
            ////Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                //this.executionStartTime = DateTime.Now;
                ////_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                ////_stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                reader.FetchSize = reader.RowSize * fetchsize;
                dtb.Load(reader);

                reader.Dispose();
                ////_stopwatch.Stop();
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
                ////_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(string query, string tableName)
        {
            ////Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(string query, string tableName, long fetchsize)
        {
            //Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(query);
                reader.FetchSize = reader.RowSize * fetchsize;
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam)
        {
            //Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam, long fetchsize)
        {
            //Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable();
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                reader.FetchSize = reader.RowSize * fetchsize;
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam, string tableName)
        {
            //Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }

        public override DataTable GetDataTable(ProcParam procParam, string tableName, long fetchsize)
        {
            //Stopwatch //_stopwatch = null;
            DataTable dtb = new DataTable(tableName);
            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                dtb.BeginLoadData();
                //_stopwatch.Start();

                OracleDataReader reader = base._getReader(procParam);
                reader.FetchSize = reader.RowSize * fetchsize;
                dtb.Load(reader);

                reader.Dispose();
                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return dtb;
        }


        public override int ExecuteNonQuery(string query)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList, int keyHeader, int keyDetail)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;
            object objKeyHeader;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    //_stopwatch.Start();
                    ret = cmd.ExecuteNonQuery();
                    //_stopwatch.Stop();

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
                        string resultMessage = String.Empty;
                        OracleCommand commandDetail;
                        foreach (ProcParam paramDetail in procList)
                        {
                            paramDetail.Parameters[keyDetail].Value = objKeyHeader;

                            commandDetail = base._getCommand(paramDetail);

                            //_stopwatch.Reset();

                            //_stopwatch.Start();
                            ret = commandDetail.ExecuteNonQuery();
                            //_stopwatch.Stop();

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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam procParam, List<ProcParam> procList)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    //_stopwatch.Start();
                    ret = cmd.ExecuteNonQuery();
                    //_stopwatch.Stop();

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
                        string resultMessage = String.Empty;
                        OracleCommand commandDetail;
                        foreach (ProcParam paramDetail in procList)
                        {
                            commandDetail = base._getCommand(paramDetail);

                            //_stopwatch.Reset();

                            //_stopwatch.Start();
                            ret = commandDetail.ExecuteNonQuery();
                            //_stopwatch.Stop();

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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(List<ProcParam> procList)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand commandDetail = null;
            int ret = 0;


            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                string resultMessage = String.Empty;

                base.BeginTransaction();

                foreach (ProcParam paramDetail in procList)
                {
                    commandDetail = base._getCommand(paramDetail);

                    //_stopwatch.Reset();

                    //_stopwatch.Start();
                    ret += commandDetail.ExecuteNonQuery();
                    //_stopwatch.Stop();

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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                commandDetail.Dispose();
                commandDetail = null;
            }

            return ret;
        }



        //Improve Performance
        public override int ExecuteNonQuery(ProcParam procParam, int arrBindCount)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            int ret = 0;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    //_stopwatch.Start();

                    cmd.ArrayBindCount = arrBindCount;

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (procParam.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])procParam.ReturnValue(procParam.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != procParam.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }



                        //arrResult.SequenceEqual(paramDtl.ArrMessageToCheck);
                        //if (!arrResult.Contains(paramDtl.MessageToCheck))
                        //{
                        //    throw new Exception(resultMessage);
                        //}

                        //if (!paramDtl.ArrMessageToCheck.Equals(arrResult))
                        //{
                        //    throw new Exception(resultMessage);
                        //}
                    }
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl, int arrBindCount, int keyHeader, int keyDetail)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            OracleCommand cmdDtl = null;
            int ret = 0;
            object objKeyHeader;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(paramHdr);

                if (cmd != null)
                {
                    #region Execute For Header

                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramHdr.IndexToCheck != -1)
                    {
                        resultMessage = paramHdr.ReturnValue(paramHdr.IndexToCheck).ToString();
                        if (!paramHdr.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    objKeyHeader = paramHdr.ReturnValue(keyHeader);

                    #endregion

                    #region Execute For Detail

                    if (paramDtl != null && arrBindCount > 0)
                    {
                        object[] arrKeyHeader = ArrayOf<object>.Create(arrBindCount, objKeyHeader);

                        paramDtl.Parameters[keyDetail].Value = arrKeyHeader;

                        cmdDtl = base._getCommand(paramDtl);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl.ArrayBindCount = arrBindCount;

                        ret = cmdDtl.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl.ReturnValue(paramDtl.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }



                            //arrResult.SequenceEqual(paramDtl.ArrMessageToCheck);
                            //if (!arrResult.Contains(paramDtl.MessageToCheck))
                            //{
                            //    throw new Exception(resultMessage);
                            //}

                            //if (!paramDtl.ArrMessageToCheck.Equals(arrResult))
                            //{
                            //    throw new Exception(resultMessage);
                            //}
                        }
                    }

                    #endregion
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;

                if (cmdDtl != null)
                {
                    cmdDtl.Dispose();
                    cmdDtl = null;
                }
            }

            return ret;
        }

        //Just one to one!
        public override int ExecuteNonQuery(ProcParam paramHdr, int arrBindCountHdr, ProcParam paramDtl, int arrBindCountDtl, int keyHeader, int keyDetail)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            OracleCommand cmdDtl = null;
            int ret = 0;
            //object objKeyHeader;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(paramHdr);

                if (cmd != null)
                {
                    #region Execute For Header

                    //_stopwatch.Start();

                    cmd.ArrayBindCount = arrBindCountHdr;

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramHdr.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramHdr.ReturnValue(paramHdr.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramHdr.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }

                        //resultMessage = paramHdr.ReturnValue(paramHdr.IndexToCheck).ToString();
                        //if (!paramHdr.MessageToCheck.Equals(resultMessage))
                        //{
                        //    throw new Exception(resultMessage);
                        //}
                    }

                    OracleString[] objKeyHeader = (OracleString[])paramHdr.ReturnValue(keyHeader);

                    #endregion

                    #region Execute For Detail

                    if (paramDtl != null && arrBindCountDtl > 0)
                    {
                        var arrKeyHeader = (from keyheader in objKeyHeader
                                            select keyheader.Value).ToArray();

                        paramDtl.Parameters[keyDetail].Value = arrKeyHeader;

                        cmdDtl = base._getCommand(paramDtl);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl.ArrayBindCount = arrBindCountDtl;

                        ret = cmdDtl.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl.ReturnValue(paramDtl.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }



                            //arrResult.SequenceEqual(paramDtl.ArrMessageToCheck);
                            //if (!arrResult.Contains(paramDtl.MessageToCheck))
                            //{
                            //    throw new Exception(resultMessage);
                            //}

                            //if (!paramDtl.ArrMessageToCheck.Equals(arrResult))
                            //{
                            //    throw new Exception(resultMessage);
                            //}
                        }
                    }

                    #endregion
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;

                if (cmdDtl != null)
                {
                    cmdDtl.Dispose();
                    cmdDtl = null;
                }
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl, int arrBindCount)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            OracleCommand cmdDtl = null;
            int ret = 0;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(paramHdr);

                if (cmd != null)
                {
                    #region Execute For Header

                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramHdr.IndexToCheck != -1)
                    {
                        resultMessage = paramHdr.ReturnValue(paramHdr.IndexToCheck).ToString();
                        if (!paramHdr.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    #endregion

                    #region Execute For Detail

                    if (paramDtl != null && arrBindCount > 0)
                    {
                        cmdDtl = base._getCommand(paramDtl);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl.ArrayBindCount = arrBindCount;

                        ret = cmdDtl.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl.ReturnValue(paramDtl.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }

                            //resultMessage = paramDtl.ReturnValue(paramDtl.IndexToCheck).ToString();
                            //if (!paramDtl.MessageToCheck.Equals(resultMessage))
                            //{
                            //    throw new Exception(resultMessage);
                            //}
                        }
                    }

                    #endregion
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;

                if (cmdDtl != null)
                {
                    cmdDtl.Dispose();
                    cmdDtl = null;
                }
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            OracleCommand cmdDtl1 = null;
            OracleCommand cmdDtl2 = null;
            int ret = 0;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(paramHdr);

                if (cmd != null)
                {
                    #region Execute For Header

                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramHdr.IndexToCheck != -1)
                    {
                        resultMessage = paramHdr.ReturnValue(paramHdr.IndexToCheck).ToString();
                        if (!paramHdr.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    #endregion

                    #region Execute For Detail 1

                    if (paramDtl1 != null && arrBindCount1 > 0)
                    {
                        cmdDtl1 = base._getCommand(paramDtl1);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl1.ArrayBindCount = arrBindCount1;

                        ret = cmdDtl1.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl1.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl1.ReturnValue(paramDtl1.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl1.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }
                        }
                    }

                    #endregion

                    #region Execute For Detail 2

                    if (paramDtl2 != null && arrBindCount2 > 0)
                    {
                        cmdDtl2 = base._getCommand(paramDtl2);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl2.ArrayBindCount = arrBindCount2;

                        ret = cmdDtl2.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl2.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl2.ReturnValue(paramDtl2.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl2.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }
                        }
                    }

                    #endregion
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;

                if (cmdDtl1 != null)
                {
                    cmdDtl1.Dispose();
                    cmdDtl1 = null;
                }

                if (cmdDtl2 != null)
                {
                    cmdDtl2.Dispose();
                    cmdDtl2 = null;
                }
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmdDtl1 = null;
            OracleCommand cmdDtl2 = null;
            int ret = 0;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                #region Execute For Detail 1

                if (paramDtl1 != null && arrBindCount1 > 0)
                {
                    cmdDtl1 = base._getCommand(paramDtl1);

                    //_stopwatch.Start();

                    cmdDtl1.ArrayBindCount = arrBindCount1;

                    ret = cmdDtl1.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramDtl1.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramDtl1.ReturnValue(paramDtl1.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramDtl1.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }
                    }
                }

                #endregion

                #region Execute For Detail 2

                if (paramDtl2 != null && arrBindCount2 > 0)
                {
                    cmdDtl2 = base._getCommand(paramDtl2);

                    //_stopwatch.Reset();

                    //_stopwatch.Start();

                    cmdDtl2.ArrayBindCount = arrBindCount2;

                    ret = cmdDtl2.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramDtl2.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramDtl2.ReturnValue(paramDtl2.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramDtl2.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }
                    }
                }

                #endregion


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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                if (cmdDtl1 != null)
                {
                    cmdDtl1.Dispose();
                    cmdDtl1 = null;
                }

                if (cmdDtl2 != null)
                {
                    cmdDtl2.Dispose();
                    cmdDtl2 = null;
                }
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2, ProcParam paramDtl3, int arrBindCount3)
        {
            //Stopwatch //_stopwatch = null;

            OracleCommand cmdDtl1 = null;
            OracleCommand cmdDtl2 = null;
            OracleCommand cmdDtl3 = null;

            int ret = 0;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                #region Execute For Detail 1

                if (paramDtl1 != null && arrBindCount1 > 0)
                {
                    cmdDtl1 = base._getCommand(paramDtl1);

                    //_stopwatch.Reset();

                    //_stopwatch.Start();

                    cmdDtl1.ArrayBindCount = arrBindCount1;

                    ret = cmdDtl1.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramDtl1.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramDtl1.ReturnValue(paramDtl1.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramDtl1.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }
                    }
                }

                #endregion

                #region Execute For Detail 2

                if (paramDtl2 != null && arrBindCount2 > 0)
                {
                    cmdDtl2 = base._getCommand(paramDtl2);

                    //_stopwatch.Reset();

                    //_stopwatch.Start();

                    cmdDtl2.ArrayBindCount = arrBindCount2;

                    ret = cmdDtl2.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramDtl2.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramDtl2.ReturnValue(paramDtl2.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramDtl2.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }
                    }
                }

                #endregion

                #region Execute For Detail 3

                if (paramDtl3 != null && arrBindCount3 > 0)
                {
                    cmdDtl3 = base._getCommand(paramDtl3);

                    //_stopwatch.Reset();

                    //_stopwatch.Start();

                    cmdDtl3.ArrayBindCount = arrBindCount3;

                    ret = cmdDtl3.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramDtl3.IndexToCheck != -1)
                    {
                        OracleString[] arrResult = ((OracleString[])paramDtl3.ReturnValue(paramDtl3.IndexToCheck));

                        var resultCheck = from ar in arrResult
                                          where ar.Value != paramDtl3.MessageToCheck
                                          select ar;

                        if (resultCheck.Any())
                        {
                            throw new Exception(resultCheck.First<OracleString>().Value);
                        }
                    }
                }

                #endregion


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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;


                if (cmdDtl1 != null)
                {
                    cmdDtl1.Dispose();
                    cmdDtl1 = null;
                }

                if (cmdDtl2 != null)
                {
                    cmdDtl2.Dispose();
                    cmdDtl2 = null;
                }

                if (cmdDtl3 != null)
                {
                    cmdDtl3.Dispose();
                    cmdDtl3 = null;
                }
            }

            return ret;
        }

        public override int ExecuteNonQuery(ProcParam paramHdr, ProcParam paramDtl1, int arrBindCount1, ProcParam paramDtl2, int arrBindCount2, ProcParam paramDtl3, int arrBindCount3)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;

            OracleCommand cmdDtl1 = null;
            OracleCommand cmdDtl2 = null;
            OracleCommand cmdDtl3 = null;
            int ret = 0;
            string resultMessage = String.Empty;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();

                cmd = base._getCommand(paramHdr);

                if (cmd != null)
                {
                    #region Execute For Header

                    //_stopwatch.Start();

                    ret = cmd.ExecuteNonQuery();

                    //_stopwatch.Stop();

                    if (paramHdr.IndexToCheck != -1)
                    {
                        resultMessage = paramHdr.ReturnValue(paramHdr.IndexToCheck).ToString();
                        if (!paramHdr.MessageToCheck.Equals(resultMessage))
                        {
                            throw new Exception(resultMessage);
                        }
                    }

                    #endregion

                    #region Execute For Detail 1

                    if (paramDtl1 != null && arrBindCount1 > 0)
                    {
                        cmdDtl1 = base._getCommand(paramDtl1);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl1.ArrayBindCount = arrBindCount1;

                        ret = cmdDtl1.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl1.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl1.ReturnValue(paramDtl1.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl1.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }
                        }
                    }

                    #endregion

                    #region Execute For Detail 2

                    if (paramDtl2 != null && arrBindCount2 > 0)
                    {
                        cmdDtl2 = base._getCommand(paramDtl2);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl2.ArrayBindCount = arrBindCount2;

                        ret = cmdDtl2.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl2.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl2.ReturnValue(paramDtl2.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl2.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }
                        }
                    }

                    #endregion

                    #region Execute For Detail 3

                    if (paramDtl3 != null && arrBindCount3 > 0)
                    {
                        cmdDtl3 = base._getCommand(paramDtl3);

                        //_stopwatch.Reset();

                        //_stopwatch.Start();

                        cmdDtl3.ArrayBindCount = arrBindCount3;

                        ret = cmdDtl3.ExecuteNonQuery();

                        //_stopwatch.Stop();

                        if (paramDtl3.IndexToCheck != -1)
                        {
                            OracleString[] arrResult = ((OracleString[])paramDtl3.ReturnValue(paramDtl3.IndexToCheck));

                            var resultCheck = from ar in arrResult
                                              where ar.Value != paramDtl3.MessageToCheck
                                              select ar;

                            if (resultCheck.Any())
                            {
                                throw new Exception(resultCheck.First<OracleString>().Value);
                            }
                        }
                    }

                    #endregion
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
                ret = ODPDataAccess.ERROR_RESULT; this.lastException = ex;
            }
            finally
            {
                //base.ExecuteTime = this.executionStopTime - this.executionStartTime;
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;

                if (cmdDtl1 != null)
                {
                    cmdDtl1.Dispose();
                    cmdDtl1 = null;
                }

                if (cmdDtl2 != null)
                {
                    cmdDtl2.Dispose();
                    cmdDtl2 = null;
                }

                if (cmdDtl3 != null)
                {
                    cmdDtl3.Dispose();
                    cmdDtl3 = null;
                }
            }

            return ret;
        }


        public override object ExecuteScalar(string query)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            object objScalar = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();
                cmd = base._getCommand(query);

                if (cmd != null)
                {
                    //_stopwatch.Start();
                    objScalar = cmd.ExecuteScalar();
                    //_stopwatch.Stop();
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
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;
        }

        public override object ExecuteScalar(ProcParam procParam)
        {
            //Stopwatch //_stopwatch = null;
            OracleCommand cmd = null;
            object objScalar = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                base.BeginTransaction();
                cmd = base._getCommand(procParam);

                if (cmd != null)
                {
                    //_stopwatch.Start();
                    objScalar = cmd.ExecuteScalar();
                    //_stopwatch.Stop();
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
                //_stopwatch = null;

                cmd.Dispose();
                cmd = null;
            }

            return objScalar;

            throw new NotImplementedException();
        }

        public override OracleDataReader ExecuteDataReader(string query)
        {
            //Stopwatch //_stopwatch = null;
            OracleDataReader reader = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                //_stopwatch.Start();

                reader = base._getReader(query);

                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return reader;
        }

        public override OracleDataReader ExecuteDataReader(ProcParam procParam)
        {
            //Stopwatch //_stopwatch = null;
            OracleDataReader reader = null;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                //_stopwatch.Start();

                reader = base._getReader(procParam);

                //_stopwatch.Stop();
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
                //_stopwatch = null;
            }

            return reader;
        }

        public override OracleDataReader ExecuteDataReaderRefCur(string procname, ProcParam procParam, int indexRefCur)
        {
            //Stopwatch //_stopwatch = null;
            OracleDataReader reader = null; ;

            try
            {
                //this.executionStartTime = DateTime.Now;
                //_stopwatch = new //Stopwatch();

                //_stopwatch.Start();
                OracleCommand cmd = base._getCommand(procParam);
                //_stopwatch.Stop();
                OracleRefCursor refCur = (OracleRefCursor)procParam.Parameters[indexRefCur].Value;
                reader = refCur.GetDataReader();

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
                //_stopwatch = null;
            }

            return reader;
        }

        #endregion / Functions - Public /

    }
}
