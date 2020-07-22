using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Collections;
using System.Drawing;
using HTN.BITS.QRCodeLib;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Globalization;

namespace HTN.BITS.BLL.PLASESS
{
    public class JobOrderBLL : IDisposable
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

        ~JobOrderBLL()
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

        public JobOrderBLL()
        {
            //constructer
        }

        public List<JobOrder> GetJobOrderList(string type, string findValue, DateTime? formDate, DateTime? toDate)
        {
            List<JobOrder> lstJobOrder = null;
            JobOrder jobOrd;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "JOB_ORDER_PACK.GET_JOB_ORDER" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strJOB_NO", DBNull.Value);
                param.AddParamInput(3, "strTYPE", type);
                if (formDate.HasValue)
                {
                    param.AddParamInput(4, "strPDT_FROM", formDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strPDT_FROM", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(5, "strPDT_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strPDT_TO", DBNull.Value);
                }
                

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobOrder = new List<JobOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobOrd = new JobOrder();

                        jobOrd.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        jobOrd.JOB_DATE = OraDataReader.Instance.GetDateTime("JOB_DATE");
                        jobOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        jobOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        jobOrd.PRODUCTION_DATE = OraDataReader.Instance.GetDateTime("PRODUCTION_DATE");
                        jobOrd.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        jobOrd.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        jobOrd.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        jobOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        jobOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        if (!OraDataReader.Instance.IsDBNull("MP_START_DATE"))
                        {
                            jobOrd.MP_START_DATE = OraDataReader.Instance.GetDateTime("MP_START_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("MP_STOP_DATE"))
                        {
                            jobOrd.MP_STOP_DATE = OraDataReader.Instance.GetDateTime("MP_STOP_DATE");
                        }
                        jobOrd.PLAN_QTY = OraDataReader.Instance.GetInteger("PLAN_QTY");
                        jobOrd.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        jobOrd.PLAN_DAY = OraDataReader.Instance.GetInteger("PLAN_DAY");
                        jobOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        jobOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstJobOrder.Add(jobOrd);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstJobOrder = null;
                throw ex;
            }

            return lstJobOrder;
        }

        public List<JobOrder> AdvJobOrder(string jobOrdNo, string type, DateTime? formDate, DateTime? toDate)
        {
            List<JobOrder> lstJobOrder = null;
            JobOrder jobOrd;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "JOB_ORDER_PACK.GET_JOB_ORDER" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strJOB_NO", jobOrdNo);
                param.AddParamInput(3, "strTYPE", type);
                if (formDate.HasValue)
                {
                    param.AddParamInput(4, "strPDT_FROM", formDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strPDT_FROM", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(5, "strPDT_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strPDT_TO", DBNull.Value);
                }


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobOrder = new List<JobOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobOrd = new JobOrder();

                        jobOrd.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        jobOrd.JOB_DATE = OraDataReader.Instance.GetDateTime("JOB_DATE");
                        jobOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        jobOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        jobOrd.PRODUCTION_DATE = OraDataReader.Instance.GetDateTime("PRODUCTION_DATE");
                        jobOrd.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        jobOrd.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        jobOrd.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        jobOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        jobOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        if (!OraDataReader.Instance.IsDBNull("MP_START_DATE"))
                        {
                            jobOrd.MP_START_DATE = OraDataReader.Instance.GetDateTime("MP_START_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("MP_STOP_DATE"))
                        {
                            jobOrd.MP_STOP_DATE = OraDataReader.Instance.GetDateTime("MP_STOP_DATE");
                        }
                        jobOrd.PLAN_QTY = OraDataReader.Instance.GetInteger("PLAN_QTY");
                        jobOrd.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        jobOrd.PLAN_DAY = OraDataReader.Instance.GetInteger("PLAN_DAY");
                        jobOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        jobOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstJobOrder.Add(jobOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstJobOrder = null;
                throw ex;
            }

            return lstJobOrder;
        }

        public JobOrder GetJobOrder(string jobNo)
        {
            JobOrder jobOrd = null;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "JOB_ORDER_PACK.GET_JOB_ORDER" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strJOB_NO", jobNo);
                param.AddParamInput(3, "strTYPE", DBNull.Value);
                param.AddParamInput(4, "strPDT_FROM", DBNull.Value);
                param.AddParamInput(5, "strPDT_TO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobOrd = new JobOrder();

                        jobOrd.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        jobOrd.JOB_DATE = OraDataReader.Instance.GetDateTime("JOB_DATE");
                        jobOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        jobOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        jobOrd.PRODUCTION_DATE = OraDataReader.Instance.GetDateTime("PRODUCTION_DATE");
                        jobOrd.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        jobOrd.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        jobOrd.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        jobOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        jobOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        if (!OraDataReader.Instance.IsDBNull("MP_START_DATE"))
                        {
                            jobOrd.MP_START_DATE = OraDataReader.Instance.GetDateTime("MP_START_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("MP_STOP_DATE"))
                        {
                            jobOrd.MP_STOP_DATE = OraDataReader.Instance.GetDateTime("MP_STOP_DATE");
                        }
                        jobOrd.PLAN_QTY = OraDataReader.Instance.GetInteger("PLAN_QTY");
                        jobOrd.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        jobOrd.PLAN_DAY = OraDataReader.Instance.GetInteger("PLAN_DAY");
                        jobOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        jobOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                jobOrd = null;
                throw ex;
            }

            return jobOrd;
        }

        public int ProductStandardBoxQty(string proID)
        {
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.PRODUCT_BOX_QTY" };

                param.AddParamReturn(0, "ReturnValue", OracleDbType.Decimal, 100);
                param.AddParamInput(1, "strNo", proID);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal resultDB = (OracleDecimal)param.ReturnValue(0);
                if (!resultDB.IsNull)
                {
                    return resultDB.ToInt32();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertJobOrder(ref JobOrder jobOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(17) { ProcedureName = "JOB_ORDER_PACK.JOB_ORDER_INS" };

                param.AddParamInOutput(0, "strJOB_NO", OracleDbType.Varchar2, 255, jobOrd.JOB_NO); //OracleDbType.Varchar2, prod.PROD_SEQ_NO
                param.AddParamInput(1, "strJOB_DATE ", jobOrd.JOB_DATE);
                param.AddParamInput(2, "strPROD_TYPE", jobOrd.PROD_TYPE);
                param.AddParamInput(3, "strREF_NO", jobOrd.REF_NO);
                param.AddParamInput(4, "strPRODUCTION_DATE", jobOrd.PRODUCTION_DATE);
                param.AddParamInput(5, "strMC_NO", jobOrd.MC_NO);
                param.AddParamInput(6, "strPROD_SEQ_NO", jobOrd.PROD_SEQ_NO);
                param.AddParamInput(7, "strPARTY_ID", jobOrd.PARTY_ID);

                if (jobOrd.MP_START_DATE.HasValue)
                {
                    param.AddParamInput(8, "strMP_START_DATE", jobOrd.MP_START_DATE.Value);
                }
                else
                {
                    param.AddParamInput(8, "strMP_START_DATE", DBNull.Value);
                }
                if (jobOrd.MP_STOP_DATE.HasValue)
                {
                    param.AddParamInput(9, "strMP_STOP_DATE", jobOrd.MP_STOP_DATE.Value);
                }
                else
                {
                    param.AddParamInput(9, "strMP_STOP_DATE", DBNull.Value);
                }
                
                param.AddParamInput(10, "strPLAN_QTY", jobOrd.PLAN_QTY);
                param.AddParamInput(11, "strUNIT_ID", jobOrd.UNIT_ID);
                param.AddParamInput(12, "strPLAN_DAY", jobOrd.PLAN_DAY);
                param.AddParamInput(13, "strREMARK", jobOrd.REMARK);
                param.AddParamInput(14, "strREC_STAT", (jobOrd.REC_STAT ? "Y" : "N"));
                param.AddParamInput(15, "strUSER_ID", userid);

                param.AddParamOutput(16, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(0);
                OracleString result = (OracleString)param.ReturnValue(16);

                if (!result.IsNull)
                {
                    jobOrd.JOB_NO = resultDB.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateJobOrder(JobOrder jobOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(17) { ProcedureName = "JOB_ORDER_PACK.JOB_ORDER_UPD" };

                param.AddParamInput(0, "strJOB_DATE ", jobOrd.JOB_NO);
                param.AddParamInput(1, "strJOB_DATE ", jobOrd.JOB_DATE);
                param.AddParamInput(2, "strPROD_TYPE", jobOrd.PROD_TYPE);
                param.AddParamInput(3, "strREF_NO", jobOrd.REF_NO);
                param.AddParamInput(4, "strPRODUCTION_DATE", jobOrd.PRODUCTION_DATE);
                param.AddParamInput(5, "strMC_NO", jobOrd.MC_NO);
                param.AddParamInput(6, "strPROD_SEQ_NO", jobOrd.PROD_SEQ_NO);
                param.AddParamInput(7, "strPARTY_ID", jobOrd.PARTY_ID);
                if (jobOrd.MP_START_DATE.HasValue)
                {
                    param.AddParamInput(8, "strMP_START_DATE", jobOrd.MP_START_DATE.Value);
                }
                else
                {
                    param.AddParamInput(8, "strMP_START_DATE", DBNull.Value);
                }
                if (jobOrd.MP_STOP_DATE.HasValue)
                {
                    param.AddParamInput(9, "strMP_STOP_DATE", jobOrd.MP_STOP_DATE.Value);
                }
                else
                {
                    param.AddParamInput(9, "strMP_STOP_DATE", DBNull.Value);
                }

                param.AddParamInput(10, "strPLAN_QTY", jobOrd.PLAN_QTY);
                param.AddParamInput(11, "strUNIT_ID", jobOrd.UNIT_ID);
                param.AddParamInput(12, "strPLAN_DAY", jobOrd.PLAN_DAY);
                param.AddParamInput(13, "strREMARK", jobOrd.REMARK);
                param.AddParamInput(14, "strREC_STAT", (jobOrd.REC_STAT ? "Y" : "N"));
                param.AddParamInput(15, "strUSER_ID", userid);

                param.AddParamOutput(16, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(16);

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

        public DataTable GetJobLotPlaning(string jobOrdNo)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "JOB_ORDER_PACK.GET_JOB_LOT" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strJOB_NO", jobOrdNo);
                param.AddParamInput(2, "strLINE_NO", DBNull.Value);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public string UpdateJobLotPlaning(List<JobLotPlan> lstJobLotPlan, string userid)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstJobLotPlan.Count);
            ProcParam param = null;

            try
            {
                foreach (JobLotPlan jLotPlan in lstJobLotPlan)
                {
                    switch (jLotPlan.FLAG)
                    {
                        case 0: //delete
                            param = new ProcParam(4);
                            param.ProcedureName = "JOB_ORDER_PACK.JOB_LOT_DEL";

                            param.AddParamInput(0, "strJOB_NO", jLotPlan.JOB_NO);
                            param.AddParamInput(1, "strLINE_NO", jLotPlan.LINE_NO);
                            param.AddParamInput(2, "strUSER_ID", userid);
                            param.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(param);

                            break;
                        case 1: //no change
                            break;
                        case 2: //add new 

                            param = new ProcParam(7);
                            param.ProcedureName = "JOB_ORDER_PACK.JOB_LOT_INS";

                            param.AddParamInput(0, "strJOB_NO", jLotPlan.JOB_NO);
                            param.AddParamInput(1, "strSHIFT_ID", jLotPlan.SHIFT_ID);
                            if (jLotPlan.SHIFT_DATE.HasValue)
                            {
                                param.AddParamInput(2, "strSHIFT_DATE", jLotPlan.SHIFT_DATE.Value);
                            }
                            else
                            {
                                param.AddParamInput(2, "strSHIFT_DATE", DBNull.Value);
                            }
                            param.AddParamInput(3, "strNO_OF_LABEL", jLotPlan.NO_OF_LABEL);
                            param.AddParamInput(4, "strQTY_PER_LABEL", jLotPlan.QTY_PER_LABEL);
                            param.AddParamInput(5, "strUSER_ID", userid);
                            param.AddParamOutput(6, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(param);

                            break;
                        default:
                            break;
                    }
                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public string UpdateJobLotPlaning(string prodType, string productSeq, DataTable dtbJobLotPlan, string userid)
        {
            string resultMsg = string.Empty;

            try
            {

                #region Transaction Detail Delete

                DataRow[] deleteRows = dtbJobLotPlan.Select("[FLAG] = 0");
                ProcParam paramDel = null;

                if (deleteRows.Length > 0)
                {
                    paramDel = new ProcParam(4) { ProcedureName = "JOB_ORDER_PACK.JOB_LOT_DEL" };

                    //JOB_NO
                    var arrJOB_NO = (from DataRow row in deleteRows
                                     select row["JOB_NO"]).ToArray();
                    paramDel.AddParamInput(0, "strJOB_NO", arrJOB_NO, OracleDbType.Varchar2);

                    //LINE_NO
                    var arrLINE_NO = (from DataRow row in deleteRows
                                      select row["LINE_NO"]).ToArray();
                    paramDel.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    //USER_ID
                    paramDel.AddParamInput(2, "strUSER_ID", ArrayOf<object>.Create(deleteRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramDel.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", deleteRows.Length);
                }

                #endregion

                #region Transaction Detail Update



                DataRow[] updateRows = dtbJobLotPlan.Select("[FLAG] = 3");
                ProcParam paramUpd = null;

                if (updateRows.Length > 0)
                {

                    paramUpd = new ProcParam(10) { ProcedureName = "JOB_ORDER_PACK.JOB_LOT_UPD" };

                    //JOB_NO
                    var arrJOB_NO = (from DataRow row in updateRows
                                     select row["JOB_NO"]).ToArray();
                    paramUpd.AddParamInput(0, "strJOB_NO", arrJOB_NO, OracleDbType.Varchar2);

                    //LINE_NO
                    var arrLINE_NO = (from DataRow row in updateRows
                                      select row["LINE_NO"]).ToArray();
                    paramUpd.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    //SHIFT_ID
                    var arrSHIFT_ID = (from DataRow row in updateRows
                                       select row["SHIFT_ID"]).ToArray();
                    paramUpd.AddParamInput(2, "strSHIFT_ID", arrSHIFT_ID, OracleDbType.Varchar2);

                    //SHIFT_DATE
                    var arrSHIFT_DATE = (from DataRow row in updateRows
                                         select row["SHIFT_DATE"]).ToArray();
                    paramUpd.AddParamInput(3, "strSHIFT_DATE", arrSHIFT_DATE, OracleDbType.Date);

                    //NO_OF_LABEL
                    var arrNO_OF_LABEL = (from DataRow row in updateRows
                                          select row["NO_OF_LABEL"]).ToArray();
                    paramUpd.AddParamInput(4, "strNO_OF_LABEL", arrNO_OF_LABEL, OracleDbType.Int32);

                    //QTY_PER_LABEL
                    var arrQTY_PER_LABEL = (from DataRow row in updateRows
                                            select row["QTY_PER_LABEL"]).ToArray();
                    paramUpd.AddParamInput(5, "strQTY_PER_LABEL", arrQTY_PER_LABEL, OracleDbType.Int32);

                    //REMARK
                    paramUpd.AddParamInput(6, "strREMARK", ArrayOf<object>.Create(updateRows.Length, ""), OracleDbType.Varchar2);
                    //REC_STAT
                    paramUpd.AddParamInput(7, "strREC_STAT", ArrayOf<object>.Create(updateRows.Length, "Y"), OracleDbType.Varchar2);


                    //USER_ID
                    paramUpd.AddParamInput(8, "strUSER_ID", ArrayOf<object>.Create(updateRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramUpd.AddParamOutput(9, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", updateRows.Length);
                }

                #endregion

                #region Transaction Detail Insert

                

                DataRow[] insertRows = dtbJobLotPlan.Select("[FLAG] = 2");
                ProcParam paramIns = null;

                if (insertRows.Length > 0)
                {

                    paramIns = new ProcParam(8) { ProcedureName = "JOB_ORDER_PACK.JOB_LOT_INS" };

                    //JOB_NO
                    var arrJOB_NO = (from DataRow row in insertRows
                                     select row["JOB_NO"]).ToArray();
                    paramIns.AddParamInput(0, "strJOB_NO", arrJOB_NO, OracleDbType.Varchar2);

                    //LINE_NO
                    var arrLINE_NO = (from DataRow row in insertRows
                                      select row["LINE_NO"]).ToArray();
                    paramIns.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    //SHIFT_ID
                    var arrSHIFT_ID = (from DataRow row in insertRows
                                       select row["SHIFT_ID"]).ToArray();
                    paramIns.AddParamInput(2, "strSHIFT_ID", arrSHIFT_ID, OracleDbType.Varchar2);

                    //SHIFT_DATE
                    var arrSHIFT_DATE = (from DataRow row in insertRows
                                         select row["SHIFT_DATE"]).ToArray();
                    paramIns.AddParamInput(3, "strSHIFT_DATE", arrSHIFT_DATE, OracleDbType.Date);

                    //NO_OF_LABEL
                    var arrNO_OF_LABEL = (from DataRow row in insertRows
                                          select row["NO_OF_LABEL"]).ToArray();
                    paramIns.AddParamInput(4, "strNO_OF_LABEL", arrNO_OF_LABEL, OracleDbType.Int32);

                    //QTY_PER_LABEL
                    var arrQTY_PER_LABEL = (from DataRow row in insertRows
                                    select row["QTY_PER_LABEL"]).ToArray();
                    paramIns.AddParamInput(5, "strQTY_PER_LABEL", arrQTY_PER_LABEL, OracleDbType.Int32);

                    //USER_ID
                    paramIns.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(insertRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", insertRows.Length);
                }

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel, deleteRows.Length, paramIns, insertRows.Length, paramUpd, updateRows.Length);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                //if(insertRows.Length > 0)
                //    this.InsertProductCard(prodType, productSeq, insertRows);

                resultMsg = "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public void InsertProductCard(string prodType, string productSeq, DataRow[] rows)
        {
            try
            {
                DataTable dtInsProdCard = new DataTable();

                dtInsProdCard.BeginInit();

                dtInsProdCard.Columns.Add(new DataColumn("PROD_TYPE", typeof(System.String)));
                dtInsProdCard.Columns.Add(new DataColumn("JOB_NO", typeof(System.String)));
                dtInsProdCard.Columns.Add(new DataColumn("LINE_NO", typeof(System.Int32)));
                dtInsProdCard.Columns.Add(new DataColumn("PLAN_DATE", typeof(System.DateTime)));
                dtInsProdCard.Columns.Add(new DataColumn("PROD_SEQ_NO", typeof(System.String)));
                dtInsProdCard.Columns.Add(new DataColumn("QTY", typeof(System.Int32)));
                dtInsProdCard.Columns.Add(new DataColumn("REC_NO", typeof(System.Int32)));

                dtInsProdCard.EndInit();

                int noOfLabel = 0;
                DataRow newRow;

                dtInsProdCard.BeginLoadData();

                foreach (DataRow row in rows)
                {
                    noOfLabel = Convert.ToInt32(row["NO_OF_LABEL"], NumberFormatInfo.CurrentInfo);

                    for (int i = 1; i <= noOfLabel; i++)
                    {
                        newRow = dtInsProdCard.NewRow();

                        newRow["PROD_TYPE"] = prodType;
                        newRow["JOB_NO"] = row["JOB_NO"];
                        newRow["LINE_NO"] = row["LINE_NO"];
                        newRow["PLAN_DATE"] = row["SHIFT_DATE"];
                        newRow["PROD_SEQ_NO"] = productSeq;
                        newRow["QTY"] = row["QTY_PER_LABEL"];
                        newRow["REC_NO"] = i;

                        dtInsProdCard.Rows.Add(newRow);
                    }
                }

                dtInsProdCard.EndLoadData();

                dtInsProdCard.AcceptChanges();

                //check already 
                string sql = string.Format("SELECT COUNT(1) FROM T_PRODUCT_CARD WHERE JOB_NO = '{0}' AND LINE_NO = {1}", rows[0]["JOB_NO"], rows[0]["LINE_NO"]);
                var obj = GlobalDB.Instance.DataAc.ExecuteScalar(sql);
                if (Convert.ToInt32(obj) == 0)
                {
                    #region Transaction Detail Insert

                    DataRow[] insertRows = dtInsProdCard.Select();
                    ProcParam paramIns = null;

                    if (insertRows.Length > 0)
                    {

                        paramIns = new ProcParam(7) { ProcedureName = "PLS_FUNCTION.PRODUCT_CARD_INS" };

                        //PROD_TYPE
                        var arrPROD_TYPE = (from DataRow row in insertRows
                                            select row["PROD_TYPE"]).ToArray();
                        paramIns.AddParamInput(0, "strPROD_TYPE", arrPROD_TYPE, OracleDbType.Varchar2);

                        //JOB_NO
                        var arrJOB_NO = (from DataRow row in insertRows
                                         select row["JOB_NO"]).ToArray();
                        paramIns.AddParamInput(1, "strJOB_NO", arrJOB_NO, OracleDbType.Varchar2);

                        //LINE_NO
                        var arrLINE_NO = (from DataRow row in insertRows
                                          select row["LINE_NO"]).ToArray();
                        paramIns.AddParamInput(2, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                        //PLAN_DATE
                        var arrPLAN_DATE = (from DataRow row in insertRows
                                            select row["PLAN_DATE"]).ToArray();
                        paramIns.AddParamInput(3, "strPLAN_DATE", arrPLAN_DATE, OracleDbType.Date);

                        //PROD_SEQ_NO
                        var arrPROD_SEQ_NO = (from DataRow row in insertRows
                                              select row["PROD_SEQ_NO"]).ToArray();
                        paramIns.AddParamInput(4, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                        //QTY
                        var arrQTY = (from DataRow row in insertRows
                                      select row["QTY"]).ToArray();
                        paramIns.AddParamInput(5, "strQTY", arrQTY, OracleDbType.Int32);

                        //REC_NO
                        var arrREC_NO = (from DataRow row in insertRows
                                         select row["REC_NO"]).ToArray();
                        paramIns.AddParamInput(6, "strREC_NO", arrREC_NO, OracleDbType.Int32);

                    }

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramIns, insertRows.Length);

                    if (GlobalDB.Instance.LastException != null)
                        throw GlobalDB.Instance.LastException;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        

        }

        public void GenerateSerialNo(string jobOrdNo)
        {
            //chekc null serial first
            List<ProductCard> lstPrdCard = null;
            ProductCard prdCard;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.GET_CHK_SR_GEN" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strJOB_NO", jobOrdNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPrdCard = new List<ProductCard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prdCard = new ProductCard();

                        prdCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");

                        lstPrdCard.Add(prdCard);
                    }

                    this.InsertBitmapSerial(ref lstPrdCard);

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private bool InsertBitmapSerial(ref List<ProductCard> lstPrdCard)
        {
            //int i = 0;
            //int intdex = 0;
            List<ProcParam> lstParam = new List<ProcParam>(lstPrdCard.Count);
            ProcParam param = null;
            Bitmap bitBar = null;
            //string fileName = string.Empty;
            foreach (ProductCard prdCard in lstPrdCard)
            {
                //intdex = i++;
                //lstPrdCard[intdex].BARCODE = this.QRCode_Encode(lstPrdCard[intdex].SERIAL_NO);
                bitBar = this.QRCode_Encode(prdCard.SERIAL_NO);
                //fileName = prdCard.SERIAL_NO + ".JPG";
                //bitBar.Save(string.Format("C:\\Temp\\PicTemp2D\\{0}", fileName));

                //byte[] byteValue = this.BitmapToByteArray(bitBar);

                param = new ProcParam(2);
                param.ProcedureName = "JOB_ORDER_PACK.PRODUCT_CARD_BARCODE_UPD";
                param.AddParamInput(0, "strSERIAL_NO", prdCard.SERIAL_NO);
                param.AddParamBLOBInput(1, "strBARCODE", OracleDbType.Blob, this.BitmapToByteArray(bitBar)); //this.BitmapToByteArray(prod.PROD_IMAGE)

                lstParam.Add(param);
            }

            GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);

            //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;


            return true;
        }

        private Bitmap QRCode_Encode(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
            {
                return null;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = 4;
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                return null;
            }
            try
            {
                int version = 4;
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                return null;
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            return qrCodeEncoder.Encode(serialNo);
        }

        private byte[] BitmapToByteArray(Bitmap bitmapIn)
        {
            MemoryStream ms = new MemoryStream();
            bitmapIn.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }

        public void InsertLotPlaningToPrint(int seqNo, List<JobLotPlan> lstJobLotPlan)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstJobLotPlan.Count);
            ProcParam param = null;
            try
            {
                foreach (JobLotPlan jLotPlan in lstJobLotPlan)
                {
                    param = new ProcParam(4);
                    param.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";
                    param.AddParamInput(0, "strSEQ_NO", seqNo);
                    param.AddParamInput(1, "strTR1", jLotPlan.JOB_NO);
                    param.AddParamInput(2, "strTR2", jLotPlan.LINE_NO);
                    param.AddParamInput(3, "strTR3", DBNull.Value);

                    lstParam.Add(param);
                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCard> GetSelectProductCard(int seqNo)
        {
            List<ProductCard> lstPrdCard = null;
            ProductCard prdCard;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.GET_PRODUCT_CARD" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPrdCard = new List<ProductCard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prdCard = new ProductCard();

                        prdCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        prdCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        prdCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        prdCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prdCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prdCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        prdCard.LABEL_STATUS = OraDataReader.Instance.GetString("LABEL_STATUS");
                        //prdCard.BARCODE = OraDataReader.Instance.GetBitmap("BARCODE");
                        prdCard.NO_OF_PRINT = OraDataReader.Instance.GetInteger("NO_OF_PRINT");

                        lstPrdCard.Add(prdCard);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPrdCard = null;
                throw ex;
            }

            return lstPrdCard;
        }

        public DataSet PrintProductCardReport(string jobOrdNo, string prodSEQNo, List<ProductCard> lstPrdCard, string userid, out int seqPrint)
        {
            //declare dataset and name.
            string processSEQ = string.Empty;

            Bitmap imgProduct = null;
            seqPrint = -1;
            DataSet dtsResult = new DataSet("DTS_PRODUCT_CARD");
            //int seqPrint = 0;
            try
            {
                processSEQ = this.ProductProcessSEQ(prodSEQNo);
                imgProduct = this.ProductImage(prodSEQNo);

                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                List<ProcParam> procInsTPrintList = new List<ProcParam>();
                ProcParam procInsTPrint = null;
                foreach (ProductCard prdCard in lstPrdCard)
                {
                    procInsTPrint = new ProcParam(4);

                    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                    procInsTPrint.AddParamInput(1, "strTR1", prdCard.SERIAL_NO);
                    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                    procInsTPrintList.Add(procInsTPrint);
                }

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                //get print value header
                DataTable dtHeader = new DataTable("T_JOB_ORDER");
                dtHeader.Columns.Add(new DataColumn("JOB_NO"));
                dtHeader.Columns.Add(new DataColumn("PROD_IMAGE", typeof(System.Drawing.Bitmap)));
                dtHeader.Columns.Add(new DataColumn("PROCESS_SEQ"));
                DataRow row = dtHeader.NewRow();

                
                row["JOB_NO"] = jobOrdNo;
                row["PROD_IMAGE"] = imgProduct;
                row["PROCESS_SEQ"] = processSEQ;

                dtHeader.Rows.Add(row);
                dtHeader.AcceptChanges();

                dtsResult.Tables.Add(dtHeader);

                //get print value detail
                //DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("JOB_ORDER_PACK.RPT_PRODUCT_CARD_LABEL",
                //                                                               seqPrint,
                //                                                               "T_PRODUCT_CARD");

                DataTable dtDetail = this.GetPrintProductCard(seqPrint, "T_PRODUCT_CARD");
                //Bitmap temp = null;
                //Image imgTemp = null;
                //foreach (DataRow rowTemp in dtDetail.Rows)
                //{
                //    imgTemp = (Bitmap)rowTemp["BARCODEIMG"];       
                //    temp = (Bitmap)rowTemp["BARCODE"];
                    
                //    temp.Save(string.Format("C:\\Temp\\PicTemp2D\\{0}.BMP", rowTemp["SERIAL_NO"].ToString()));
                //    imgTemp.Save(string.Format("C:\\Temp\\PicTemp2D\\{0}.JPG", rowTemp["SERIAL_NO"].ToString()));
                //}


                dtsResult.Tables.Add(dtDetail);

                //maping datatable to dataset
                dtsResult.Relations.Add("T_PRODUCT_CARD_T_JOB_ORDER",
                                dtsResult.Tables["T_JOB_ORDER"].Columns["JOB_NO"],
                                dtsResult.Tables["T_PRODUCT_CARD"].Columns["JOB_NO"]);

                //no need to remove seq now!!
                //PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);


                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        private string ProductProcessSEQ(string proSeqNo)
        {
            string proSEQArr = string.Empty;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.PROD_SEQ_PROCESS" };
                param.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 100);
                param.AddParamInput(1, "PROD_SEQ_NO", proSeqNo);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString dbResult = (OracleString)param.ReturnValue(0);

                if (!dbResult.IsNull)
                {
                    return dbResult.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return "Y,Y,Y,Y,Y";
            }
        }

        private Bitmap ProductImage(string proSeqNo)
        {
            Bitmap result = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.PRODUCT_IMAGE" };

                param.AddParamReturn(0, "ReturnValue", OracleDbType.Blob, 255);
                param.AddParamInput(1, "strNo", proSeqNo);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleBlob blobDB = (OracleBlob)param.ReturnValue(0);

                if (!blobDB.IsNull)
                {
                    using (MemoryStream ms = new MemoryStream(blobDB.Value))
                    {
                        result = (Bitmap)Bitmap.FromStream(ms);
                    }

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetPrintProductCard(int seqNo, string tableName)
        {
            List<ProductCard> lstPrdCard = null;
            ProductCard prdCard;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.RPT_PRODUCT_CARD_LABEL" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPrdCard = new List<ProductCard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prdCard = new ProductCard();

                        prdCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        prdCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        prdCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prdCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prdCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        prdCard.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        prdCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        prdCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        prdCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        prdCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        prdCard.NO_OF_LABEL = OraDataReader.Instance.GetString("NO_OF_LABEL");
                        //prdCard.BARCODE = UtilityBLL.QRCode_Encode(prdCard.SERIAL_NO);
                        //prdCard.BARCODE = OraDataReader.Instance.GetBitmap("BARCODE");
                        //prdCard.BARCODE.Save(string.Format("C:\\Temp\\PicTemp2D\\{0}.BMP", prdCard.SERIAL_NO));
                        //prdCard.PROD_IMAGE = OraDataReader.Instance.GetBitmap("PROD_IMAGE");

                        lstPrdCard.Add(prdCard);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPrdCard = null;
                throw ex;
            }

            //return this.ListToDataTable(lstPrdCard, tableName);
            //DataTable dt  = UtilityBLL.ListToDataTable(lstPrdCard, tableName);
            return UtilityBLL.ListToDataTable(lstPrdCard, tableName);
        }

        private DataTable ListToDataTable<T>(IList<T> data, string tableName)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(tableName);
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public DataSet PrintJobOrderReport(List<JobOrder> lstJobOrd, string userid)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_JOB_ORDER");
            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                List<ProcParam> procInsTPrintList = new List<ProcParam>();
                ProcParam procInsTPrint = null;
                foreach (JobOrder jobOrd in lstJobOrd)
                {
                    procInsTPrint = new ProcParam(4);

                    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                    procInsTPrint.AddParamInput(1, "strTR1", jobOrd.JOB_NO);
                    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                    procInsTPrintList.Add(procInsTPrint);
                }

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                //get print value header
                DataTable dtHeader = this.GetJobOrderHDR(seqPrint, "T_JOB_ORDER");

                dtsResult.Tables.Add(dtHeader);


                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("JOB_ORDER_PACK.RPT_JOB_ORDER_DTL", seqPrint, "T_JOB_LOT");


                dtsResult.Tables.Add(dtDetail);

                //maping datatable to dataset
                dtsResult.Relations.Add("T_JOB_ORDER_T_JOB_LOT",
                                dtsResult.Tables["T_JOB_ORDER"].Columns["JOB_NO"],
                                dtsResult.Tables["T_JOB_LOT"].Columns["JOB_NO"]);

                //dtsResult.Relations.Add("T_JOB_ORDER_T_JOB_LOT",
                //                new DataColumn[] {dtsResult.Tables["T_JOB_ORDER"].Columns["JOB_NO"],
                //                                  dtsResult.Tables["T_JOB_ORDER"].Columns["SHIFT_DATE"],
                //                                  dtsResult.Tables["T_JOB_ORDER"].Columns["SHIFT"],
                //                                 },
                //                new DataColumn[] {dtsResult.Tables["T_JOB_LOT"].Columns["JOB_NO"],
                //                                  dtsResult.Tables["T_JOB_LOT"].Columns["PLAN_DATE"],
                //                                  dtsResult.Tables["T_JOB_LOT"].Columns["SHIFT"],
                //                                 }
                //                       );

                PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        private DataTable GetJobOrderHDR(int seqNo, string tableName)
        {
            List<JobOrderHdr> lstJobOrd = null;
            JobOrderHdr jobOrd;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.RPT_JOB_ORDER_HDR" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobOrd = new List<JobOrderHdr>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobOrd = new JobOrderHdr();

                        jobOrd.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        jobOrd.PRODUCTION_TYPE = OraDataReader.Instance.GetString("PRODUCTION_TYPE");
                        jobOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        if (!OraDataReader.Instance.IsDBNull("PLAN_DATE"))
                        {
                            jobOrd.PLAN_DATE = OraDataReader.Instance.GetDateTime("PLAN_DATE");
                        }
                        jobOrd.CUSTOMER = OraDataReader.Instance.GetString("CUSTOMER");
                        jobOrd.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        jobOrd.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        jobOrd.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        jobOrd.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        jobOrd.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        if (!OraDataReader.Instance.IsDBNull("MP_START_DATE"))
                        {
                            jobOrd.MP_START_DATE = OraDataReader.Instance.GetDateTime("MP_START_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("MP_STOP_DATE"))
                        {
                            jobOrd.MP_STOP_DATE = OraDataReader.Instance.GetDateTime("MP_STOP_DATE");
                        }
                        jobOrd.PLAN_QTY = OraDataReader.Instance.GetInteger("PLAN_QTY");
                        jobOrd.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        //jobOrd.BARCODE = UtilityBLL.QRCode_Encode(jobOrd.JOB_NO);

                        lstJobOrd.Add(jobOrd);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstJobOrd = null;
                throw ex;
            }

            return UtilityBLL.ListToDataTable(lstJobOrd, tableName);
        }

        public DataTable GetPrintTime(int seq)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.GET_PRINTED_CARD" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seq);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public void UpdatePrintTime(int seq, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "JOB_ORDER_PACK.UPD_PRT_PRODUCT_CARD" };

                param.AddParamInput(0, "strSEQ_NO", seq);
                param.AddParamInput(1, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
