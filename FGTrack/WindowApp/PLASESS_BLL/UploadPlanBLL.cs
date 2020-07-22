using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using HTN.BITS.BEL.PLASESS;
using System.ComponentModel;

namespace HTN.BITS.BLL.PLASESS
{
    public class UploadPlanBLL : IDisposable
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

        ~UploadPlanBLL()
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


        public List<Machine> GetActiveMachineList(string mType)
        {
            List<Machine> lstMc = null;
            Machine Mc;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "UPLOAD_PLAN.GET_ACTIVE_MC" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strTYPE", mType);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);
                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMc = new List<Machine>();
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;
                    while (OraDataReader.Instance.OraReader.Read())
                    {

                        Mc = new Machine();

                        Mc.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        Mc.MACHINE_NAME = OraDataReader.Instance.GetString("MACHINE_NAME");


                        lstMc.Add(Mc);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMc = null;
                throw ex;
            }

            return lstMc;
        }

        public List<ULPlanDetail> GetPlanDetails(string planNo)
        {
            List<ULPlanDetail> lstulp_dtl = null;
            ULPlanDetail ulp_dtl;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "UPLOAD_PLAN.GET_PLAN_DTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPLAN_NO", planNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);
                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstulp_dtl = new List<ULPlanDetail>();
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;
                    while (OraDataReader.Instance.OraReader.Read())
                    {      

                        ulp_dtl = new ULPlanDetail();

                        ulp_dtl.PLAN_DTL_ID = OraDataReader.Instance.GetInteger("PLAN_DTL_ID");
                        ulp_dtl.PLAN_NO = OraDataReader.Instance.GetString("PLAN_NO");
                        ulp_dtl.MC_SIZE_TON = OraDataReader.Instance.GetString("MC_SIZE_TON");
                        ulp_dtl.PDTL_BLOCK = OraDataReader.Instance.GetString("PDTL_BLOCK");
                        ulp_dtl.MC_NO = OraDataReader.Instance.GetString("MC_NO");

                        ulp_dtl.PDTL_SEQUENCE = OraDataReader.Instance.GetString("PDTL_SEQUENCE");
                        ulp_dtl.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        ulp_dtl.PARTNAME = OraDataReader.Instance.GetString("PARTNAME");
                        ulp_dtl.MAT_TYPE = OraDataReader.Instance.GetString("MAT_TYPE");
                        ulp_dtl.INSERT_1 = OraDataReader.Instance.GetString("INSERT_1");

                        ulp_dtl.INSERT_2 = OraDataReader.Instance.GetString("INSERT_2");
                        ulp_dtl.INSERT_3 = OraDataReader.Instance.GetString("INSERT_3");
                        ulp_dtl.CAV_ACT = OraDataReader.Instance.GetInteger("CAV_ACT");
                        ulp_dtl.CAV_FULL = OraDataReader.Instance.GetInteger("CAV_FULL");
                        ulp_dtl.MP_START = OraDataReader.Instance.GetDateTime("MP_START");

                        ulp_dtl.MP_FINISH = OraDataReader.Instance.GetDateTime("MP_FINISH");
                        ulp_dtl.PLAN_MP_DAY = OraDataReader.Instance.GetDecimal("PLAN_MP_DAY");
                        ulp_dtl.PRO_SHOT_WEIGHT = OraDataReader.Instance.GetDecimal("PRO_SHOT_WEIGHT");
                        ulp_dtl.CYCLE_TIME = OraDataReader.Instance.GetDecimal("CYCLE_TIME");
                        ulp_dtl.QTY_DAY = OraDataReader.Instance.GetInteger("QTY_DAY");

                        ulp_dtl.TOTAL_MAT_USE_KG = OraDataReader.Instance.GetDecimal("TOTAL_MAT_USE_KG");
                        ulp_dtl.TPCT_LOSS = OraDataReader.Instance.GetDecimal("TPCT_LOSS");
                        ulp_dtl.QTY_PLAN = OraDataReader.Instance.GetInteger("QTY_PLAN");
                        ulp_dtl.PLAN_MAT_AVG_DAY_KG = OraDataReader.Instance.GetDecimal("PLAN_MAT_AVG_DAY_KG");
                        ulp_dtl.MAT_DRY = OraDataReader.Instance.GetDecimal("MAT_DRY");

                        ulp_dtl.TARGET_DAY = OraDataReader.Instance.GetInteger("TARGET_DAY");
                        ulp_dtl.PDTL_REMARK = OraDataReader.Instance.GetString("PDTL_REMARK");
                        ulp_dtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        ulp_dtl.PROD_LOT = OraDataReader.Instance.GetString("PROD_LOT");
                        ulp_dtl.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        ulp_dtl.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");

                        ulp_dtl.CHANGE_MOLD = (OraDataReader.Instance.GetString("CHANGE_MOLD").ToUpper() == "Y");
                        ulp_dtl.CONTINUE_ORDER = (OraDataReader.Instance.GetString("CONTINUE_ORDER").ToUpper() == "Y");
                        ulp_dtl.REVISED_PLAN = (OraDataReader.Instance.GetString("REVISED_PLAN").ToUpper() == "Y");

                        ulp_dtl.PLAN_STAT = OraDataReader.Instance.GetString("PLAN_STAT");
                        ulp_dtl.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        ulp_dtl.FLAG = 1;
                        ulp_dtl.MC_NAME = OraDataReader.Instance.GetString("INFO.MC_NAME(MC_NO)");

                        lstulp_dtl.Add(ulp_dtl);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstulp_dtl = null;
                throw ex;
            }

            return lstulp_dtl;
        }

        public ULPlan GetULPlanByPlanNo(string ulplanNo)
        {
            ULPlan ulPlan = null;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "UPLOAD_PLAN.GET_UL_PLAN" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPLAN_NO", ulplanNo);
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
                        ulPlan = new ULPlan();

                        ulPlan.PLAN_NO = OraDataReader.Instance.GetString("PLAN_NO");
                        ulPlan.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        ulPlan.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                 
                        ulPlan.PLAN_DATE = OraDataReader.Instance.GetDateTime("PLAN_DATE");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                ulPlan = null;
                throw ex;
            }

            return ulPlan;
        }

        public List<ULPlan> GetULPlanList(string type, string findValue, DateTime? formDate, DateTime? toDate)
        {
            List<ULPlan> lstULPlan = null;
            ULPlan ULPlan;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "UPLOAD_PLAN.GET_UL_PLAN" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPLAN_NO", DBNull.Value);
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
                    lstULPlan = new List<ULPlan>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        ULPlan = new ULPlan();

                        ULPlan.PLAN_NO = OraDataReader.Instance.GetString("PLAN_NO");
                        ULPlan.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        ULPlan.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                       
                        ULPlan.PLAN_DATE = OraDataReader.Instance.GetDateTime("PLAN_DATE");

                        lstULPlan.Add(ULPlan);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstULPlan = null;
                throw ex;
            }

            return lstULPlan;
        }

        public List<Product> Getprod_all_ULP(string prodType)
        {
            List<Product> lstprod = null;
            Product prod;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "UPLOAD_PLAN.SELECT_ALL_PROD" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strProdType", prodType);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);


                if (OraDataReader.Instance.HasRows)
                {
                    lstprod = new List<Product>();
                    //OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prod = new Product();
                        prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prod.MATERIAL_NAME = OraDataReader.Instance.GetString("INFO.MTL_TYPE_NAME(MATERIAL_TYPE)");

                        lstprod.Add(prod);
                    }
                }
                OraDataReader.Instance.Close();
               
            }
            catch (Exception ex)
            {
                string re = ex.Message;

                lstprod = null;
            }

            return lstprod;
        }

        public string GenerateJob(string planNo, string userId)
        {
            string resultMsg = string.Empty;
            try
            {

                ProcParam param = new ProcParam(3) { ProcedureName = "UPLOAD_PLAN.GENERATE_JOB" };
                param.AddParamInput(0, "strPLAN_NO", planNo);
                param.AddParamInput(1, "strUSER_ID", userId);
                param.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

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

        public string UpdateStatusPlanHDR(string planNo,string status,string userId) {
            string resultMsg = string.Empty;
            try
            {

                ProcParam param = new ProcParam(4) { ProcedureName = "UPLOAD_PLAN.UPDATE_ULPLAN_STATUS" };
                param.AddParamInput(0, "strPLAN_NO", planNo);
                param.AddParamInput(1, "strREC_STAT", status);
                param.AddParamInput(2, "strU_USER_ID", userId);
                param.AddParamOutput(3, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                OracleString result = (OracleString)param.ReturnValue(3);



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
        
       
     public string InsUpdDelPlanListFLAG(List<ULPlanDetail> lstArrDtl, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Detail"

                #region Arrival Delete #Flag=0

                var delArr = from arr in lstArrDtl
                             where arr.FLAG == 0
                             select arr;

                ProcParam paramDel = null;

                if (delArr.Any() && delArr.Count() > 0)
                {
                    paramDel = new ProcParam(4) { ProcedureName = "UPLOAD_PLAN.UPLOAD_PLAN_DTL_DELETE" };

                    var arrPLAN_DTL_ID = (from arr in delArr
                                         select (object)arr.PLAN_DTL_ID).ToArray();
                    paramDel.AddParamInput(0, "strPLAN_DTL_ID", arrPLAN_DTL_ID,OracleDbType.Int32);

                    var arrPLAN_NO = (from arr in delArr
                                          select arr.PLAN_NO).ToArray();
                    paramDel.AddParamInput(1, "strPLAN_NO", arrPLAN_NO, OracleDbType.Varchar2);

                    paramDel.AddParamInput(2, "strUSER_ID", ArrayOf<object>.Create(delArr.Count(), userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramDel.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", delArr.Count());

                }

                #endregion

                #region Arrival Insert #Flag=2

                var insArr = from arr in lstArrDtl
                             where arr.FLAG == 2
                             select arr;

                ProcParam paramIns = null;

                if (insArr.Any() && insArr.Count() > 0)
                {
                    paramIns = new ProcParam(34) { ProcedureName = "UPLOAD_PLAN.UPLOAD_PLAN_DTL_INSERT" };

                    var arrPLAN_NO = (from arr in insArr
                                          select arr.PLAN_NO).ToArray();
                    paramIns.AddParamInput(0, "strPLAN_NO",arrPLAN_NO,OracleDbType.Varchar2);

                    var arrMC_SIZE_TON = (from arr in insArr
                                          select arr.MC_SIZE_TON).ToArray();
                    paramIns.AddParamInput(1, "strMC_SIZE_TON", arrMC_SIZE_TON, OracleDbType.Varchar2);

                    var arrPDTL_BLOCK = (from arr in insArr
                                         select arr.PDTL_BLOCK).ToArray();
                    paramIns.AddParamInput(2, "strPDTL_BLOCK", arrPDTL_BLOCK, OracleDbType.Varchar2);

                    var arrMC_NO = (from arr in insArr
                                    select arr.MC_NO).ToArray();
                    paramIns.AddParamInput(3, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    var arrPDTL_SEQUENCE = (from arr in insArr
                                            select arr.PDTL_SEQUENCE).ToArray();
                    paramIns.AddParamInput(4, "strPDTL_SEQUENCE", arrPDTL_SEQUENCE, OracleDbType.Varchar2);

                    var arrPRODUCT_NO = (from arr in insArr
                                         select arr.PRODUCT_NO).ToArray();
                    paramIns.AddParamInput(5, "strPRODUCT_NO", arrPRODUCT_NO, OracleDbType.Varchar2);
                    
                    var arrPARTNAME = (from arr in insArr
                                           select arr.PARTNAME).ToArray();
                    paramIns.AddParamInput(6, "strPARTNAME", arrPARTNAME, OracleDbType.Varchar2);

                    var arrMAT_TYPE = (from arr in insArr
                                       select arr.MAT_TYPE).ToArray();
                    paramIns.AddParamInput(7, "strMAT_TYPE", arrMAT_TYPE, OracleDbType.Varchar2);

                    var arrINSERT_1 = (from arr in insArr
                                       select arr.INSERT_1).ToArray();
                    paramIns.AddParamInput(8, "strINSERT_1", arrINSERT_1, OracleDbType.Varchar2);

                    var arrINSERT_2 = (from arr in insArr
                                       select arr.INSERT_2).ToArray();
                    paramIns.AddParamInput(9, "strINSERT_2", arrINSERT_2, OracleDbType.Varchar2);

                    var arrINSERT_3 =(from arr in insArr
                                      select arr.INSERT_3).ToArray();
                    paramIns.AddParamInput(10, "strINSERT_3", arrINSERT_3, OracleDbType.Varchar2);

                    var arrCAV_ACT = (from arr in insArr
                                          select (object)arr.CAV_ACT).ToArray();
                    paramIns.AddParamInput(11, "strCAV_ACT", arrCAV_ACT, OracleDbType.Int32);

                    var arrCAV_FULL = (from arr in insArr
                                       select (object)arr.CAV_FULL).ToArray();
                    paramIns.AddParamInput(12, "strCAV_FULL", arrCAV_FULL, OracleDbType.Int32);

                    var arrMP_START = (from arr in insArr
                                       select (object)arr.MP_START).ToArray();
                    paramIns.AddParamInput(13, "strMP_START", arrMP_START, OracleDbType.Date);

                    var arrMP_FINISH = (from arr in insArr
                                        select (object)arr.MP_FINISH).ToArray();
                    paramIns.AddParamInput(14, "strMP_FINISH", arrMP_FINISH, OracleDbType.Date);

                    var arrPLAN_MP_DAY = (from arr in insArr
                                          select (object)arr.PLAN_MP_DAY).ToArray();
                    paramIns.AddParamInput(15, "strPLAN_MP_DAY", arrPLAN_MP_DAY, OracleDbType.Decimal);

                    var arrPRO_SHOT_WEIGHT = (from arr in insArr
                                              select (object)arr.PRO_SHOT_WEIGHT).ToArray();
                    paramIns.AddParamInput(16, "strPRO_SHOT_WEIGHT", arrPRO_SHOT_WEIGHT, OracleDbType.Decimal);

                    var arrCYCLE_TIME = (from arr in insArr
                                         select (object)arr.CYCLE_TIME).ToArray();
                    paramIns.AddParamInput(17, "strCYCLE_TIME", arrCYCLE_TIME, OracleDbType.Decimal);

                    var arrQTY_DAY = (from arr in insArr
                                      select (object)arr.QTY_DAY).ToArray();
                    paramIns.AddParamInput(18, "strQTY_DAY", arrQTY_DAY, OracleDbType.Int32);

                    var arrTOTAL_MAT_USE_KG = (from arr in insArr
                                               select (object)arr.TOTAL_MAT_USE_KG).ToArray();
                    paramIns.AddParamInput(19, "strTOTAL_MAT_USE_KG", arrTOTAL_MAT_USE_KG, OracleDbType.Decimal);

                    var arrTPCT_LOSS = (from arr in insArr
                                           select (object)arr.TPCT_LOSS).ToArray();
                    paramIns.AddParamInput(20, "strTPCT_LOSS", arrTPCT_LOSS, OracleDbType.Decimal);

                    var arrQTY_PLAN = (from arr in insArr
                                       select (object)arr.QTY_PLAN).ToArray();
                    paramIns.AddParamInput(21, "strQTY_PLAN", arrQTY_PLAN, OracleDbType.Int32);

                    var arrPLAN_MAT_AVG_DAY_KG = (from arr in insArr
                                                  select (object)arr.PLAN_MAT_AVG_DAY_KG).ToArray();
                    paramIns.AddParamInput(22, "strPLAN_MAT_AVG_DAY_KG", arrPLAN_MAT_AVG_DAY_KG, OracleDbType.Decimal);

                    var arrMAT_DRY = (from arr in insArr
                                      select (object)arr.MAT_DRY).ToArray();
                    paramIns.AddParamInput(23, "strMAT_DRY", arrMAT_DRY, OracleDbType.Decimal);

                    var arrTARGET_DAY = (from arr in insArr
                                         select (object)arr.TARGET_DAY).ToArray();
                    paramIns.AddParamInput(24, "strTARGET_DAY", arrTARGET_DAY, OracleDbType.Int32);

                    var arrPDTL_REMARK = (from arr in insArr
                                          select arr.PDTL_REMARK).ToArray();
                    paramIns.AddParamInput(25, "strPDTL_REMARK", arrPDTL_REMARK, OracleDbType.Varchar2);

                    var arrPARTY_ID = (from arr in insArr
                                       select arr.PARTY_ID).ToArray();
                    paramIns.AddParamInput(26, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                    var arrPROD_LOT = (from arr in insArr
                                       select arr.PROD_LOT).ToArray();
                    paramIns.AddParamInput(27, "strPROD_LOT", arrPROD_LOT, OracleDbType.Varchar2);

                    paramIns.AddParamInput(28, "strN_USER_ID", ArrayOf<object>.Create(insArr.Count(), userid), OracleDbType.Varchar2);

                    var arrPROD_SEQ_NO = (from arr in insArr
                                          select arr.PROD_SEQ_NO).ToArray();
                    paramIns.AddParamInput(29, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    var arrCHANGE_MOLD = (from arr in insArr
                                          select (arr.CHANGE_MOLD ? "Y" : "N")).ToArray();
                    paramIns.AddParamInput(30, "strCHANGE_MOLD", arrCHANGE_MOLD, OracleDbType.Varchar2);

                    var arrCONTINUE_ORDER = (from arr in insArr
                                             select (arr.CONTINUE_ORDER ? "Y" : "N")).ToArray();
                    paramIns.AddParamInput(31, "strCONTINUE_ORDER", arrCONTINUE_ORDER, OracleDbType.Varchar2);

                    var arrREVISED_PLAN = (from arr in insArr
                                           select (arr.REVISED_PLAN ? "Y" : "N")).ToArray();
                    paramIns.AddParamInput(32, "strREVISED_PLAN", arrREVISED_PLAN, OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(33, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", insArr.Count());


                }

                #endregion

                #region Arrival Update #Flag=3

                var updArr = from arr in lstArrDtl
                             where arr.FLAG == 3
                             select arr;

                ProcParam paramUpd = null;

                if (updArr.Any() && updArr.Count() > 0)
                {

                    paramUpd = new ProcParam(34) { ProcedureName = "UPLOAD_PLAN.UPLOAD_PLAN_DTL_UPDATE" };

                    var arrPLAN_DTL_ID = (from arr in updArr
                                      select (object)arr.PLAN_DTL_ID).ToArray();
                    paramUpd.AddParamInput(0, "strPLAN_DTL_ID", arrPLAN_DTL_ID, OracleDbType.Int32);

                    var arrMC_SIZE_TON = (from arr in updArr
                                          select arr.MC_SIZE_TON).ToArray();
                    paramUpd.AddParamInput(1, "strMC_SIZE_TON", arrMC_SIZE_TON, OracleDbType.Varchar2);

                    var arrPDTL_BLOCK = (from arr in updArr
                                         select arr.PDTL_BLOCK).ToArray();
                    paramUpd.AddParamInput(2, "strPDTL_BLOCK", arrPDTL_BLOCK, OracleDbType.Varchar2);

                    var arrMC_NO = (from arr in updArr
                                    select arr.MC_NO).ToArray();
                    paramUpd.AddParamInput(3, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    var arrPDTL_SEQUENCE = (from arr in updArr
                                            select arr.PDTL_SEQUENCE).ToArray();
                    paramUpd.AddParamInput(4, "strPDTL_SEQUENCE", arrPDTL_SEQUENCE, OracleDbType.Varchar2);

                    var arrPRODUCT_NO = (from arr in updArr
                                         select arr.PRODUCT_NO).ToArray();
                    paramUpd.AddParamInput(5, "strPRODUCT_NO", arrPRODUCT_NO, OracleDbType.Varchar2);

                    var arrPARTNAME = (from arr in updArr
                                       select arr.PARTNAME).ToArray();
                    paramUpd.AddParamInput(6, "strPARTNAME", arrPARTNAME, OracleDbType.Varchar2);

                    var arrMAT_TYPE = (from arr in updArr
                                       select arr.MAT_TYPE).ToArray();
                    paramUpd.AddParamInput(7, "strMAT_TYPE", arrMAT_TYPE, OracleDbType.Varchar2);

                    var arrINSERT_1 = (from arr in updArr
                                       select arr.INSERT_1).ToArray();
                    paramUpd.AddParamInput(8, "strINSERT_1", arrINSERT_1, OracleDbType.Varchar2);

                    var arrINSERT_2 = (from arr in updArr
                                       select arr.INSERT_2).ToArray();
                    paramUpd.AddParamInput(9, "strINSERT_2", arrINSERT_2, OracleDbType.Varchar2);

                    var arrINSERT_3 = (from arr in updArr
                                       select arr.INSERT_3).ToArray();
                    paramUpd.AddParamInput(10, "strINSERT_3", arrINSERT_3, OracleDbType.Varchar2);

                    var arrCAV_ACT = (from arr in updArr
                                      select (object)arr.CAV_ACT).ToArray();
                    paramUpd.AddParamInput(11, "strCAV_ACT", arrCAV_ACT, OracleDbType.Int32);

                    var arrCAV_FULL = (from arr in updArr
                                       select (object)arr.CAV_FULL).ToArray();
                    paramUpd.AddParamInput(12, "strCAV_FULL", arrCAV_FULL, OracleDbType.Int32);

                    var arrMP_START = (from arr in updArr
                                       select (object)arr.MP_START).ToArray();
                    paramUpd.AddParamInput(13, "strMP_START", arrMP_START, OracleDbType.Date);

                    var arrMP_FINISH = (from arr in updArr
                                        select (object)arr.MP_FINISH).ToArray();
                    paramUpd.AddParamInput(14, "strMP_FINISH", arrMP_FINISH, OracleDbType.Date);

                    var arrPLAN_MP_DAY = (from arr in updArr
                                          select (object)arr.PLAN_MP_DAY).ToArray();
                    paramUpd.AddParamInput(15, "strPLAN_MP_DAY", arrPLAN_MP_DAY, OracleDbType.Decimal);

                    var arrPRO_SHOT_WEIGHT = (from arr in updArr
                                              select (object)arr.PRO_SHOT_WEIGHT).ToArray();
                    paramUpd.AddParamInput(16, "strPRO_SHOT_WEIGHT", arrPRO_SHOT_WEIGHT, OracleDbType.Decimal);

                    var arrCYCLE_TIME = (from arr in updArr
                                         select (object)arr.CYCLE_TIME).ToArray();
                    paramUpd.AddParamInput(17, "strCYCLE_TIME", arrCYCLE_TIME, OracleDbType.Decimal);

                    var arrQTY_DAY = (from arr in updArr
                                      select (object)arr.QTY_DAY).ToArray();
                    paramUpd.AddParamInput(18, "strQTY_DAY", arrQTY_DAY, OracleDbType.Int32);

                    var arrTOTAL_MAT_USE_KG = (from arr in updArr
                                               select (object)arr.TOTAL_MAT_USE_KG).ToArray();
                    paramUpd.AddParamInput(19, "strTOTAL_MAT_USE_KG", arrTOTAL_MAT_USE_KG, OracleDbType.Decimal);

                    var arrTPCT_LOSS = (from arr in updArr
                                        select (object)arr.TPCT_LOSS).ToArray();
                    paramUpd.AddParamInput(20, "strTPCT_LOSS", arrTPCT_LOSS, OracleDbType.Decimal);

                    var arrQTY_PLAN = (from arr in updArr
                                       select (object)arr.QTY_PLAN).ToArray();
                    paramUpd.AddParamInput(21, "strQTY_PLAN", arrQTY_PLAN, OracleDbType.Int32);

                    var arrPLAN_MAT_AVG_DAY_KG = (from arr in updArr
                                                  select (object)arr.PLAN_MAT_AVG_DAY_KG).ToArray();
                    paramUpd.AddParamInput(22, "strPLAN_MAT_AVG_DAY_KG", arrPLAN_MAT_AVG_DAY_KG, OracleDbType.Decimal);

                    var arrMAT_DRY = (from arr in updArr
                                      select (object)arr.MAT_DRY).ToArray();
                    paramUpd.AddParamInput(23, "strMAT_DRY", arrMAT_DRY, OracleDbType.Decimal);

                    var arrTARGET_DAY = (from arr in updArr
                                         select (object)arr.TARGET_DAY).ToArray();
                    paramUpd.AddParamInput(24, "strTARGET_DAY", arrTARGET_DAY, OracleDbType.Int32);

                    var arrPDTL_REMARK = (from arr in updArr
                                          select arr.PDTL_REMARK).ToArray();
                    paramUpd.AddParamInput(25, "strPDTL_REMARK", arrPDTL_REMARK, OracleDbType.Varchar2);

                    var arrPARTY_ID = (from arr in updArr
                                       select arr.PARTY_ID).ToArray();
                    paramUpd.AddParamInput(26, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                    var arrPROD_LOT = (from arr in updArr
                                       select arr.PROD_LOT).ToArray();
                    paramUpd.AddParamInput(27, "strPROD_LOT", arrPROD_LOT, OracleDbType.Varchar2);

                    paramUpd.AddParamInput(28, "strU_USER_ID", ArrayOf<object>.Create(updArr.Count(), userid), OracleDbType.Varchar2);

                    var arrPROD_SEQ_NO = (from arr in updArr
                                          select arr.PROD_SEQ_NO).ToArray();
                    paramUpd.AddParamInput(29, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    var arrCHANGE_MOLD = (from arr in updArr
                                          select (arr.CHANGE_MOLD ? "Y" : "N")).ToArray();
                    paramUpd.AddParamInput(30, "strCHANGE_MOLD", arrCHANGE_MOLD, OracleDbType.Varchar2);

                    var arrCONTINUE_ORDER = (from arr in updArr
                                             select (arr.CONTINUE_ORDER ? "Y" : "N")).ToArray();
                    paramUpd.AddParamInput(31, "strCONTINUE_ORDER", arrCONTINUE_ORDER, OracleDbType.Varchar2);

                    var arrREVISED_PLAN = (from arr in updArr
                                           select (arr.REVISED_PLAN ? "Y" : "N")).ToArray();
                    paramUpd.AddParamInput(32, "strREVISED_PLAN", arrREVISED_PLAN, OracleDbType.Varchar2);

                    //RESULTMSG
                    paramUpd.AddParamOutput(33, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", updArr.Count());

                }

                #endregion

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel, delArr.Count(), paramIns, insArr.Count(), paramUpd, updArr.Count());

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;


                resultMsg = "OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;

        }
    
        public string InsertPlan(string PlanType, DataTable DtExcelPlan_DTL ,DateTime planDate, string userid)
        {
            string resultMsg = string.Empty;

            try
            { 
                #region Transaction Header

                ProcParam paramHDR = new ProcParam(5) { ProcedureName = "UPLOAD_PLAN.UPLOAD_PLAN_HDR_INSERT" };

                paramHDR.AddParamInOutput(0, "strPLAN_NO", OracleDbType.Varchar2,30,"");
                paramHDR.AddParamInput(1, "strUSER_ID", userid);
                paramHDR.AddParamInput(2, "strTYPE", PlanType);
                paramHDR.AddParamInput(3, "strPLAN_DATE", planDate);
                paramHDR.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255);

                #endregion
                #region Transaction Detail

                // DataRow[] insertRows = dtCOMPDtl.Rows;
                ProcParam paramDTL = null;

                if (DtExcelPlan_DTL.Rows.Count > 0)
                {

                    paramDTL = new ProcParam(34) { ProcedureName = "UPLOAD_PLAN.UPLOAD_PLAN_DTL_INSERT" };

                    paramDTL.AddParamInput(0, "strPLAN_NO", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);

                    var arrMC_SIZE_TON = (from DataRow row in DtExcelPlan_DTL.Rows
                                      select row["MC_SIZE_TON"]).ToArray();
                    paramDTL.AddParamInput(1, "strMC_SIZE_TON", arrMC_SIZE_TON, OracleDbType.Varchar2);

                    var arrPDTL_BLOCK = (from DataRow row in DtExcelPlan_DTL.Rows
                                          select row["PDTL_BLOCK"]).ToArray();
                    paramDTL.AddParamInput(2, "strPDTL_BLOCK", arrPDTL_BLOCK, OracleDbType.Varchar2);

                    var arrMC_NO = (from DataRow row in DtExcelPlan_DTL.Rows
                                    select row["MC_NO"]).ToArray();
                    paramDTL.AddParamInput(3, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    var arrPDTL_SEQUENCE = (from DataRow row in DtExcelPlan_DTL.Rows
                                            select row["PDTL_SEQUENCE"]).ToArray();
                    paramDTL.AddParamInput(4, "strPDTL_SEQUENCE", arrPDTL_SEQUENCE, OracleDbType.Varchar2);

                    var arrPRODUCT_NO = (from DataRow row in DtExcelPlan_DTL.Rows
                                         select row["PRODUCT_NO"]).ToArray();
                    paramDTL.AddParamInput(5, "strPRODUCT_NO", arrPRODUCT_NO, OracleDbType.Varchar2);
                    if (PlanType == "H")
                    {
                        var arrPARTNAME = (from DataRow row in DtExcelPlan_DTL.Rows
                                           select row["PARTNAME"]).ToArray();
                        paramDTL.AddParamInput(6, "strPARTNAME", arrPARTNAME, OracleDbType.Varchar2);
                    }
                    else {
                        paramDTL.AddParamInput(6, "strPARTNAME", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);
                    
                    }
                    var arrMAT_TYPE = (from DataRow row in DtExcelPlan_DTL.Rows
                                       select row["MAT_TYPE"]).ToArray();
                    paramDTL.AddParamInput(7, "strMAT_TYPE", arrMAT_TYPE, OracleDbType.Varchar2);

                    if (PlanType == "V")
                    {
                        var arrINSERT_1 = (from DataRow row in DtExcelPlan_DTL.Rows
                                           select row["INSERT_1"]).ToArray();
                        paramDTL.AddParamInput(8, "strINSERT_1", arrINSERT_1, OracleDbType.Varchar2);

                        var arrINSERT_2 = (from DataRow row in DtExcelPlan_DTL.Rows
                                           select row["INSERT_2"]).ToArray();
                        paramDTL.AddParamInput(9, "strINSERT_2", arrINSERT_2, OracleDbType.Varchar2);

                        var arrINSERT_3 = (from DataRow row in DtExcelPlan_DTL.Rows
                                           select row["INSERT_3"]).ToArray();
                        paramDTL.AddParamInput(10, "strINSERT_3", arrINSERT_3, OracleDbType.Varchar2);

                        var arrTARGET_DAY = (from DataRow row in DtExcelPlan_DTL.Rows
                                             select row["TARGET_DAY"]).ToArray();
                        paramDTL.AddParamInput(24, "strTARGET_DAY", arrTARGET_DAY, OracleDbType.Int32);

                        var arrPROD_LOT = (from DataRow row in DtExcelPlan_DTL.Rows
                                           select row["PROD_LOT"]).ToArray();
                        paramDTL.AddParamInput(27, "strPROD_LOT", arrPROD_LOT, OracleDbType.Varchar2);
                    }
                    else {

                        paramDTL.AddParamInput(8, "strINSERT_1", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);
                        paramDTL.AddParamInput(9, "strINSERT_2", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);
                        paramDTL.AddParamInput(10, "strINSERT_3", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);
                        paramDTL.AddParamInput(24, "strTARGET_DAY", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, null), OracleDbType.Int32);
                        paramDTL.AddParamInput(27, "strPROD_LOT", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, ""), OracleDbType.Varchar2);
                    }
                    var arrCAV_ACT = (from DataRow row in DtExcelPlan_DTL.Rows
                                      select row["CAV_ACT"]).ToArray();
                    paramDTL.AddParamInput(11, "strCAV_ACT", arrCAV_ACT, OracleDbType.Int32);

                    var arrCAV_FULL = (from DataRow row in DtExcelPlan_DTL.Rows
                                       select row["CAV_FULL"]).ToArray();
                    paramDTL.AddParamInput(12, "strCAV_FULL", arrCAV_FULL, OracleDbType.Int32);

                    var arrMP_START = (from DataRow row in DtExcelPlan_DTL.Rows
                                       select row["MP_START"]).ToArray();
                    paramDTL.AddParamInput(13, "strMP_START", arrMP_START, OracleDbType.Date);

                    var arrMP_FINISH = (from DataRow row in DtExcelPlan_DTL.Rows
                                        select row["MP_FINISH"]).ToArray();
                    paramDTL.AddParamInput(14, "strMP_FINISH", arrMP_FINISH, OracleDbType.Date);

                    var arrPLAN_MP_DAY = (from DataRow row in DtExcelPlan_DTL.Rows
                                          select row["PLAN_MP_DAY"]).ToArray();
                    paramDTL.AddParamInput(15, "strPLAN_MP_DAY", arrPLAN_MP_DAY, OracleDbType.Decimal);

                    var arrPRO_SHOT_WEIGHT = (from DataRow row in DtExcelPlan_DTL.Rows
                                              select row["PRO_SHOT_WEIGHT"]).ToArray();
                    paramDTL.AddParamInput(16, "strPRO_SHOT_WEIGHT", arrPRO_SHOT_WEIGHT, OracleDbType.Decimal);

                    var arrCYCLE_TIME = (from DataRow row in DtExcelPlan_DTL.Rows
                                         select row["CYCLE_TIME"]).ToArray();
                    paramDTL.AddParamInput(17, "strCYCLE_TIME", arrCYCLE_TIME, OracleDbType.Decimal);

                    var arrQTY_DAY = (from DataRow row in DtExcelPlan_DTL.Rows
                                      select row["QTY_DAY"]).ToArray();
                    paramDTL.AddParamInput(18, "strQTY_DAY", arrQTY_DAY, OracleDbType.Int32);

                    var arrTOTAL_MAT_USE_KG = (from DataRow row in DtExcelPlan_DTL.Rows
                                               select row["TOTAL_MAT_USE_KG"]).ToArray();
                    paramDTL.AddParamInput(19, "strTOTAL_MAT_USE_KG", arrTOTAL_MAT_USE_KG, OracleDbType.Decimal);

                    var arrTPCT_LOSS = (from DataRow row in DtExcelPlan_DTL.Rows
                                        select row["TPCT_LOSS"]).ToArray();
                    paramDTL.AddParamInput(20, "strTPCT_LOSS", arrTPCT_LOSS, OracleDbType.Decimal);

                    var arrQTY_PLAN = (from DataRow row in DtExcelPlan_DTL.Rows
                                       select row["QTY_PLAN"]).ToArray();
                    paramDTL.AddParamInput(21, "strQTY_PLAN", arrQTY_PLAN, OracleDbType.Int32);

                    var arrPLAN_MAT_AVG_DAY_KG = (from DataRow row in DtExcelPlan_DTL.Rows
                                                  select row["PLAN_MAT_AVG_DAY_KG"]).ToArray();
                    paramDTL.AddParamInput(22, "strPLAN_MAT_AVG_DAY_KG", arrPLAN_MAT_AVG_DAY_KG, OracleDbType.Decimal);

                    var arrMAT_DRY = (from DataRow row in DtExcelPlan_DTL.Rows
                                      select row["MAT_DRY"]).ToArray();
                    paramDTL.AddParamInput(23, "strMAT_DRY", arrMAT_DRY, OracleDbType.Decimal);

                    var arrPDTL_REMARK = (from DataRow row in DtExcelPlan_DTL.Rows
                                          select row["PDTL_REMARK"]).ToArray();
                    paramDTL.AddParamInput(25, "strPDTL_REMARK", arrPDTL_REMARK, OracleDbType.Varchar2);

                    var arrPARTY_ID = (from DataRow row in DtExcelPlan_DTL.Rows
                                       select row["PARTY_ID"]).ToArray();
                    paramDTL.AddParamInput(26, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                   


                    paramDTL.AddParamInput(28, "strN_USER_ID", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, userid), OracleDbType.Varchar2);

                    var arrPROD_SEQ_NO = (from DataRow row in DtExcelPlan_DTL.Rows
                                          select row["PROD_SEQ_NO"]).ToArray();
                    paramDTL.AddParamInput(29, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    paramDTL.AddParamInput(30, "strCHANGE_MOLD", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, 'N'), OracleDbType.Varchar2);
                    paramDTL.AddParamInput(31, "strCONTINUE_ORDER", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, 'N'), OracleDbType.Varchar2);
                    paramDTL.AddParamInput(32, "strREVISED_PLAN", ArrayOf<object>.Create(DtExcelPlan_DTL.Rows.Count, 'N'), OracleDbType.Varchar2);
                    //RESULTMSG
                    paramDTL.AddParamOutput(33, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", DtExcelPlan_DTL.Rows.Count);
                }

                #endregion
                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, paramDTL, DtExcelPlan_DTL.Rows.Count, 0, 0);
               // GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, paramDTL, lstCOMPDtl.Count,0,0);
                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                OracleString result = (OracleString)paramHDR.ReturnValue(4);


                if (!result.IsNull)
                {
                    resultMsg = result.Value;

                }


            }
            catch(Exception ex){
                throw ex;
            }
            return resultMsg;
        }
  
    }
}
