using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace HTN.BITS.BLL.PLASESS
{
    public class MachineBLL : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    //this.CloseConnection();
                }
            }

            IsDisposed = true;
        }

        ~MachineBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        //private TimeSpan executionTime;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        #endregion

        public MachineBLL()
        {
            //constructer
        }

        /// <summary>
        /// Get Machine List
        /// </summary>
        /// <returns>Machine List</returns>
        public List<Machine> GetMachineList(string findValue)
        {
            List<Machine> lstMachine = null;
            Machine mc;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_MACHINE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strMC_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMachine = new List<Machine>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mc = new Machine();

                        mc.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        mc.MACHINE_NAME = OraDataReader.Instance.GetString("MACHINE_NAME");
                        mc.MACHINE_TYPE = OraDataReader.Instance.GetString("MACHINE_TYPE");
                        mc.MACHINE_SIZE = OraDataReader.Instance.GetString("MACHINE_SIZE");
                        mc.REMARK = OraDataReader.Instance.GetString("REMARK");
                        mc.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstMachine.Add(mc);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMachine = null;
                throw ex;
            }

            return lstMachine;
        }

        public Machine GetMachine(string mcNo)
        {
            Machine mc = null;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_MACHINE_LIST";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strMC_NO", mcNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mc = new Machine();

                        mc.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        mc.MACHINE_NAME = OraDataReader.Instance.GetString("MACHINE_NAME");
                        mc.MACHINE_TYPE = OraDataReader.Instance.GetString("MACHINE_TYPE");
                        mc.MACHINE_SIZE = OraDataReader.Instance.GetString("MACHINE_SIZE");
                        mc.REMARK = OraDataReader.Instance.GetString("REMARK");
                        mc.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                mc = null;
                throw ex;
            }

            return mc;
        }

        /// <summary>
        /// Insert Machine
        /// </summary>
        /// <param name="mc">Machine Entity</param>
        /// <returns>Result Message</returns>
        public string InsertMachine(Machine mc, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(8);

                param.ProcedureName = "MASTER_PACK.M_MACHINE_INS";

                param.AddParamInput(0, "strMC_NO", mc.MC_NO);
                param.AddParamInput(1, "strMACHINE_NAME", mc.MACHINE_NAME);
                param.AddParamInput(2, "strMACHINE_TYPE", mc.MACHINE_TYPE);
                param.AddParamInput(3, "strMACHINE_SIZE", mc.MACHINE_SIZE);
                param.AddParamInput(4, "strREMARK", mc.REMARK);
                param.AddParamInput(5, "strREC_STAT", (mc.REC_STAT ? "Y" : "N"));
                param.AddParamInput(6, "strUSER_ID", userid);

                param.AddParamOutput(7, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(7);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        /// <summary>
        /// Update Machine
        /// </summary>
        /// <param name="mc">Machine Entity</param>
        /// <returns>Result Message</returns>
        public string UpdateMachine(Machine mc, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(8);

                param.ProcedureName = "MASTER_PACK.M_MACHINE_UPD";

                param.AddParamInput(0, "strMC_NO", mc.MC_NO);
                param.AddParamInput(1, "strMACHINE_NAME", mc.MACHINE_NAME);
                param.AddParamInput(2, "strMACHINE_TYPE", mc.MACHINE_TYPE);
                param.AddParamInput(3, "strMACHINE_SIZE", mc.MACHINE_SIZE);
                param.AddParamInput(4, "strREMARK", mc.REMARK);
                param.AddParamInput(5, "strREC_STAT", (mc.REC_STAT ? "Y" : "N"));
                param.AddParamInput(6, "strUSER_ID", userid);

                param.AddParamOutput(7, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(7);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        /// <summary>
        /// Delete Machine
        /// </summary>
        /// <param name="mcID">Machine ID</param>
        /// <returns>Result Message</returns>
        public string DeleteMachine(string mcNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "MASTER_PACK.M_MACHINE_DEL";

                param.AddParamInput(0, "strMC_NO", mcNo);
                param.AddParamInput(1, "strUSER_ID", userid);

                param.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(2);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultMsg;
        }

        public DataSet PrintMachineReport(List<Machine> lstMachine)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_MACHINE");

            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstMachine.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrMC_NO = (from job in lstMachine
                                     select job.MC_NO).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrMC_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstMachine.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstMachine.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstMachine.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (Machine mc in lstMachine)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", mc.MC_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                DataTable dtDetail = this.GetPrintMachineOrder(seqPrint, "M_MACHINE");

                dtsResult.Tables.Add(dtDetail);

                PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                dtsResult = null;
                throw ex;
            }

            return dtsResult;
        }

        private DataTable GetPrintMachineOrder(int seqNo, string tableName)
        {
            List<Machine> lstMachine = null;
            Machine mc;

            try
            {
                ProcParam param = new ProcParam(2);

                param.ProcedureName = "MASTER_PACK.RPT_M_MACHINE_LIST";

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMachine = new List<Machine>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mc = new Machine();

                        mc.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        mc.MACHINE_NAME = OraDataReader.Instance.GetString("MACHINE_NAME");
                        //mc.BARCODE = UtilityBLL.QRCode_Encode(mc.MC_NO);
                        lstMachine.Add(mc);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMachine = null;
                throw ex;
            }

            return UtilityBLL.ListToDataTable(lstMachine, tableName);
        }
    }
}
