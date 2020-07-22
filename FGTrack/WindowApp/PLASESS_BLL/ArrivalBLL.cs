using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Drawing;
using System.Data;
using System.IO;

namespace HTN.BITS.BLL.PLASESS
{
    public class ArrivalBLL : IDisposable
    {
        
        public ArrivalBLL()
        {
            //Constructor
        }


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

        ~ArrivalBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;

        #endregion

        #region Method member

        public List<T_ARRIVAL_HDR> GetArrivalHeaderList(string FindAll, string arrival_no, DateTime? formDate, DateTime? todate, string whId)
        {
            List<T_ARRIVAL_HDR> arrivalList = null;
            T_ARRIVAL_HDR objArrival;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "MTL_IN_PACK.GET_ARRIVAL_HDR" };

                param.AddParamRefCursor(0, "io_cursor");

                param.AddParamInput(1, "strFindAll", FindAll);

                param.AddParamInput(2, "strARRIVAL_NO", arrival_no);

                if (formDate.HasValue)
                {
                    param.AddParamInput(3, "strREF_DT_FROM", formDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strREF_DT_FROM", DBNull.Value);
                }

                if (todate.HasValue)
                {
                    param.AddParamInput(4, "strREF_DT_TO", todate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strREF_DT_TO", DBNull.Value);
                }

                param.AddParamInput(5, "strWH_ID", whId);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                if (OraDataReader.Instance.HasRows) //reader.HasRows
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    arrivalList = new List<T_ARRIVAL_HDR>();

                    while (OraDataReader.Instance.OraReader.Read()) //reader.Read()
                    {

                        objArrival = new T_ARRIVAL_HDR(); //reader.GetOrdinal()

                        objArrival.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        objArrival.ARR_TYPE = OraDataReader.Instance.GetString("ARR_TYPE");
                        //objArrival.ARRIVAL_TYPE_DESC = OraDataReader.Instance.GetString("ARRIVAL_TYPE_DESC");

                        if (!OraDataReader.Instance.IsDBNull("ARRIVAL_DATE"))
                        {
                            objArrival.ARRIVAL_DATE = OraDataReader.Instance.GetDateTime("ARRIVAL_DATE");
                        }

                        objArrival.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        objArrival.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");

                        if (!OraDataReader.Instance.IsDBNull("REF_DATE"))
                        {
                            objArrival.REF_DATE = OraDataReader.Instance.GetDateTime("REF_DATE");
                        }

                        objArrival.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        objArrival.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        objArrival.REMARK = OraDataReader.Instance.GetString("REMARK");
                        objArrival.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").Equals("Y");
                        objArrival.STATUS = OraDataReader.Instance.GetString("STATUS");
                        objArrival.ARR_TYPE = OraDataReader.Instance.GetString("ARR_TYPE");

                        arrivalList.Add(objArrival);
                    }
                }

                // always call Close when done reading.
                //reader.Close();
                OraDataReader.Instance.Close();

            }
            catch (Exception ex)
            {
                arrivalList = null;
                throw ex;
            }

            return arrivalList;
        }

        public List<T_ARRIVAL_DTL> GetArrivalDetailList(string arrNo)
        {

            string ResultMsg = string.Empty;
            List<T_ARRIVAL_DTL> lstArr = null;
            T_ARRIVAL_DTL objArr;

            try
            {

                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_IN_PACK.GET_ARRIVAL_DTL" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strARRIVAL_NO", arrNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstArr = new List<T_ARRIVAL_DTL>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        objArr = new T_ARRIVAL_DTL();

                        objArr.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        objArr.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        objArr.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        objArr.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        objArr.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        objArr.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        objArr.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        objArr.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        objArr.REC_QTY = OraDataReader.Instance.GetDecimal("REC_QTY");
                        objArr.REMARK = OraDataReader.Instance.GetString("REMARK");
                        //objArr.FLAG = OraDataReader.Instance.GetInteger("FLAG");
                        objArr.NO_OF_LABEL = OraDataReader.Instance.GetInteger("NO_OF_LABEL");
                        objArr.GEN_LABEL_STATUS = OraDataReader.Instance.GetString("GEN_LABEL_STATUS");
                        objArr.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        objArr.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").Equals("Y");
                        objArr.LOT_DATE = OraDataReader.Instance.GetDateTime("LOT_DATE");
                        objArr.FLAG = OraDataReader.Instance.GetInteger("FLAG");
                        objArr.STATUS = OraDataReader.Instance.GetString("STATUS");

                        lstArr.Add(objArr);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();


                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstArr;
        }

        public string InsertArrival(T_ARRIVAL_HDR ArrHdr, List<T_ARRIVAL_DTL> lstArrDtl, string userid, out string ARRIVAL_NO)
        {
            string resultMsg = string.Empty;

            ARRIVAL_NO = string.Empty;

            try
            {

                #region "Transaction Header"

                ProcParam procHeader = new ProcParam(11) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_HDR_INS" };

                procHeader.AddParamInOutput(0, "strARRIVAL_NO", OracleDbType.NVarchar2, 30, ArrHdr.ARRIVAL_NO);
                procHeader.AddParamInput(1, "strARRIVAL_DATE", ArrHdr.ARRIVAL_DATE);
                procHeader.AddParamInput(2, "strWH_ID", ArrHdr.WH_ID);
                procHeader.AddParamInput(3, "strPARTY_ID", ArrHdr.PARTY_ID);
                procHeader.AddParamInput(4, "strREF_NO ", ArrHdr.REF_NO);
                procHeader.AddParamInput(5, "strREF_DATE", ArrHdr.REF_DATE);
                procHeader.AddParamInput(6, "strREMARK", ArrHdr.REMARK);
                procHeader.AddParamInput(7, "strUSER_ID", ArrHdr.USER_ID);
                procHeader.AddParamInput(8, "strREC_STAT", ArrHdr.REC_STAT ? "Y" : "N");
                procHeader.AddParamInput(9, "strARR_TYPE", ArrHdr.ARR_TYPE);
                procHeader.AddParamOutput(10, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");


                #endregion "Transaction Header"

                #region "Transaction Detail"

                #region Arrival Insert #Flag=2

                var insArr = from arr in lstArrDtl
                             where arr.FLAG == 2
                             select arr;

                ProcParam paramIns = null;

                if (insArr.Any() && insArr.Count() > 0)
                {
                    paramIns = new ProcParam(10) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_DTL_INS" };

                    var arrARRIVAL_NO = (from arr in insArr
                                         select arr.ARRIVAL_NO).ToArray();
                    paramIns.AddParamInput(0, "strARRIVAL_NO", arrARRIVAL_NO, OracleDbType.Varchar2);

                    var arrLINE_NO = (from arr in insArr
                                      select (object)arr.LINE_NO).ToArray();
                    paramIns.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    var arrMTL_SEQ_NO = (from arr in insArr
                                      select arr.MTL_SEQ_NO).ToArray();
                    paramIns.AddParamInput(2, "strMTL_SEQ_NO", arrMTL_SEQ_NO, OracleDbType.Varchar2);

                    var arrUNIT_ID = (from arr in insArr
                                           select arr.UNIT_ID).ToArray();
                    paramIns.AddParamInput(3, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    var arrQTY = (from arr in insArr
                                          select (object)arr.QTY).ToArray();
                    paramIns.AddParamInput(4, "strQTY", arrQTY, OracleDbType.Decimal);

                    var arrREMARK = (from arr in insArr
                                     select arr.REMARK).ToArray();
                    paramIns.AddParamInput(5, "strREMARK", arrREMARK, OracleDbType.Varchar2);

                    //strCREATED_BY
                    paramIns.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(insArr.Count(), userid), OracleDbType.Varchar2);
                    
                    //REC_STATUS
                    paramIns.AddParamInput(7, "strREC_STAT", ArrayOf<object>.Create(insArr.Count(), "Y"), OracleDbType.Varchar2);


                    var arrLOT_DATE = (from arr in insArr
                                  select (object)arr.LOT_DATE).ToArray();
                    paramIns.AddParamInput(8, "strLOT_DATE", arrLOT_DATE, OracleDbType.Date);

                    //RESULTMSG
                    paramIns.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", insArr.Count());
                }

                #endregion

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procHeader, paramIns, insArr.Count(), 0, 0);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                OracleString resultKey = (OracleString)procHeader.ReturnValue(0);
                OracleString result = (OracleString)procHeader.ReturnValue(10);

                if (!resultKey.IsNull)
                {
                    ARRIVAL_NO = resultKey.Value;
                }

                if (!result.IsNull)
                    resultMsg = result.Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateArrival(T_ARRIVAL_HDR ArrHdr, List<T_ARRIVAL_DTL> lstArrDtl, string userid)
        {

            string resultMsg = string.Empty;

            try
            {

                #region "Transaction Header"

                ProcParam procHeader = new ProcParam(11) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_HDR_UPD" };

                procHeader.AddParamInput(0, "strARRIVAL_NO", ArrHdr.ARRIVAL_NO);
                procHeader.AddParamInput(1, "strARRIVAL_DATE", ArrHdr.ARRIVAL_DATE);
                procHeader.AddParamInput(2, "strWH_ID", ArrHdr.WH_ID);
                procHeader.AddParamInput(3, "strPARTY_ID", ArrHdr.PARTY_ID);
                procHeader.AddParamInput(4, "strREF_NO ", ArrHdr.REF_NO);
                procHeader.AddParamInput(5, "strREF_DATE", ArrHdr.REF_DATE);
                procHeader.AddParamInput(6, "strREMARK", ArrHdr.REMARK);
                procHeader.AddParamInput(7, "strUSER_ID", ArrHdr.USER_ID);
                procHeader.AddParamInput(8, "strREC_STAT", ArrHdr.REC_STAT ? "Y" : "N");
                procHeader.AddParamInput(9, "strARR_TYPE", ArrHdr.ARR_TYPE);
                procHeader.AddParamOutput(10, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");


                #endregion "Transaction Header"

                #region "Transaction Detail"

                #region Arrival Delete #Flag=0

                var delArr = from arr in lstArrDtl
                             where arr.FLAG == 0
                             select arr;

                ProcParam paramDel = null;

                if (delArr.Any() && delArr.Count() > 0)
                {
                    paramDel = new ProcParam(4) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_DTL_DEL" };

                    //strUNIT_CODE
                    var arrARRIVAL_NO = (from arr in delArr
                                         select arr.ARRIVAL_NO).ToArray();
                    paramDel.AddParamInput(0, "strARRIVAL_NO", arrARRIVAL_NO, OracleDbType.Varchar2);

                    var arrLINE_NO = (from arr in delArr
                                      select (object)arr.LINE_NO).ToArray();
                    paramDel.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    //User id
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
                    paramIns = new ProcParam(10) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_DTL_INS" };

                    var arrARRIVAL_NO = (from arr in insArr
                                         select arr.ARRIVAL_NO).ToArray();
                    paramIns.AddParamInput(0, "strARRIVAL_NO", arrARRIVAL_NO, OracleDbType.Varchar2);

                    var arrLINE_NO = (from arr in insArr
                                      select (object)arr.LINE_NO).ToArray();
                    paramIns.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    var arrMTL_SEQ_NO = (from arr in insArr
                                         select arr.MTL_SEQ_NO).ToArray();
                    paramIns.AddParamInput(2, "strMTL_SEQ_NO", arrMTL_SEQ_NO, OracleDbType.Varchar2);

                    var arrUNIT_ID = (from arr in insArr
                                      select arr.UNIT_ID).ToArray();
                    paramIns.AddParamInput(3, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    var arrQTY = (from arr in insArr
                                  select (object)arr.QTY).ToArray();
                    paramIns.AddParamInput(4, "strQTY", arrQTY, OracleDbType.Decimal);

                    var arrREMARK = (from arr in insArr
                                     select arr.REMARK).ToArray();
                    paramIns.AddParamInput(5, "strREMARK", arrREMARK, OracleDbType.Varchar2);

                    //strCREATED_BY
                    paramIns.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(insArr.Count(), userid), OracleDbType.Varchar2);

                    //REC_STATUS
                    paramIns.AddParamInput(7, "strREC_STAT", ArrayOf<object>.Create(insArr.Count(), "Y"), OracleDbType.Varchar2);


                    var arrLOT_DATE = (from arr in insArr
                                       select (object)arr.LOT_DATE).ToArray();
                    paramIns.AddParamInput(8, "strLOT_DATE", arrLOT_DATE, OracleDbType.Date);

                    //RESULTMSG
                    paramIns.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", insArr.Count());
                }

                #endregion

                #region Arrival Update #Flag=3

                var updArr = from arr in lstArrDtl
                             where arr.FLAG == 3
                             select arr;

                ProcParam paramUpd = null;

                if (updArr.Any() && updArr.Count() > 0)
                {

                    paramUpd = new ProcParam(9) { ProcedureName = "MTL_IN_PACK.T_ARRIVAL_DTL_UPD" };

                    var arrARRIVAL_NO = (from arr in updArr
                                         select arr.ARRIVAL_NO).ToArray();
                    paramUpd.AddParamInput(0, "strARRIVAL_NO", arrARRIVAL_NO, OracleDbType.Varchar2);

                    var arrLINE_NO = (from arr in updArr
                                      select (object)arr.LINE_NO).ToArray();
                    paramUpd.AddParamInput(1, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    var arrMTL_SEQ_NO = (from arr in updArr
                                         select arr.MTL_SEQ_NO).ToArray();
                    paramUpd.AddParamInput(2, "strMTL_SEQ_NO", arrMTL_SEQ_NO, OracleDbType.Varchar2);

                    var arrUNIT_ID = (from arr in updArr
                                      select arr.UNIT_ID).ToArray();
                    paramUpd.AddParamInput(3, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    var arrQTY = (from arr in updArr
                                  select (object)arr.QTY).ToArray();
                    paramUpd.AddParamInput(4, "strQTY", arrQTY, OracleDbType.Decimal);

                    var arrREMARK = (from arr in updArr
                                     select arr.REMARK).ToArray();
                    paramUpd.AddParamInput(5, "strREMARK", arrREMARK, OracleDbType.Varchar2);

                    //strCREATED_BY
                    paramUpd.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(updArr.Count(), userid), OracleDbType.Varchar2);

                    //REC_STATUS
                    paramUpd.AddParamInput(7, "strREC_STAT", ArrayOf<object>.Create(updArr.Count(), "Y"), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramUpd.AddParamOutput(8, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", updArr.Count());

                }

                #endregion

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procHeader, paramDel, delArr.Count(), paramIns, insArr.Count(), paramUpd, updArr.Count());

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

        public List<T_ARRIVAL_DTL_SUB> GetLabelList(string arrNo, int lineNo)
        {
            List<T_ARRIVAL_DTL_SUB> lstArr = null;
            T_ARRIVAL_DTL_SUB objArr;

            string ResultMsg = string.Empty;

            try
            {


                ProcParam param = new ProcParam(3) { ProcedureName = "MTL_IN_PACK.GET_LABEL" };

                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strARRIVAL_NO", arrNo);
                param.AddParamInput(2, "strLINE_NO", lineNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstArr = new List<T_ARRIVAL_DTL_SUB>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        objArr = new T_ARRIVAL_DTL_SUB();

                        objArr.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        objArr.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        objArr.LINE_NO_SUB = OraDataReader.Instance.GetInteger("LINE_NO_SUB");
                        objArr.UNIT = OraDataReader.Instance.GetString("UNIT");
                        objArr.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").Equals("Y");
                        objArr.STD_QTY = OraDataReader.Instance.GetDecimal("STD_QTY");
                        objArr.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        objArr.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        objArr.DOC_PKG_QTY = OraDataReader.Instance.GetDecimal("DOC_PKG_QTY");

                        lstArr.Add(objArr);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();


                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstArr = null;
                throw ex;
            }

            return lstArr;
        }

        public string InsertLabel(string arrNo, int lineNo, List<T_ARRIVAL_DTL_SUB> lstLabel, string userid)
        {
            string resultMsg = string.Empty;

            try
            {

                ProcParam procDel = new ProcParam(3) { ProcedureName = "MTL_IN_PACK.DELETE_LABEL" };

                procDel.AddParamInput(0, "strARRIVAL_NO", arrNo);
                procDel.AddParamInput(1, "strLINE_NO", lineNo);
                procDel.AddParamOutput(2, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procDel);



                List<ProcParam> paraList = new List<ProcParam>();
                ProcParam procDetail = null;
                foreach (T_ARRIVAL_DTL_SUB labelOpt in lstLabel)
                {
                    procDetail = new ProcParam(10) { ProcedureName = "MTL_IN_PACK.INSERT_LABEL" };

                    procDetail.AddParamInput(0, "strARRIVAL_NO", arrNo);
                    procDetail.AddParamInput(1, "strITEM_NO", labelOpt.MTL_SEQ_NO);
                    procDetail.AddParamInput(2, "strLINE_NO", lineNo);
                    procDetail.AddParamInput(3, "strLINE_NO_SUB", labelOpt.LINE_NO_SUB);
                    procDetail.AddParamInput(4, "strQTY_SKU", labelOpt.STD_QTY);
                    procDetail.AddParamInput(5, "strDOC_INNER_QTY", labelOpt.QTY);
                    procDetail.AddParamInput(6, "strDOC_PKG_QTY", labelOpt.DOC_PKG_QTY);
                    procDetail.AddParamInput(7, "strPKG_UNIT", labelOpt.UNIT);
                    procDetail.AddParamInput(8, "strUSER_ID", labelOpt.U_USER_ID);
                    procDetail.AddParamOutput(9, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    paraList.Add(procDetail);
                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paraList);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;


                OracleString result = (OracleString)procDetail.ReturnValue(9);

                if (!result.IsNull)
                    resultMsg = result.Value;
            }
            catch (Exception ex)
            {

                resultMsg = null;
                throw ex;

            }

            return resultMsg;
        }

        public string GenerateRecevingLabels(string arrNo, string WH_ID, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procGenerate = new ProcParam(4) { ProcedureName = "MTL_IN_PACK.GENERATE_LABEL" };

                procGenerate.AddParamInput(0, "strARRIVAL_NO", arrNo);

                procGenerate.AddParamInput(1, "strUSER_ID", userid);

                procGenerate.AddParamInput(2, "strWH_ID", WH_ID);

                procGenerate.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                //paraList.Add(procGenerate);
                GlobalDB.Instance.DataAc.ExecuteNonQuery(procGenerate);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                OracleString result = (OracleString)procGenerate.ReturnValue(3);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }
                //inform time
                //this.executionTime = oracleData.ExecutionTime;

            }
            catch (Exception ex)
            {
                resultMsg = null;
                throw ex;
            }

            return resultMsg;
        }

        public List<Material> GetMaterialList(string findAll,string mtl_seq_no,string supplier,string locId)
        {
            List<Material> lstMaterail = null;
            Material material;
            ProcParam param = null;

            try
            {


                //if (findAll != string.Empty)
                //{
                     param = new ProcParam(5) { ProcedureName = "LOV_PACK.GET_MATERIAL_LIST" };

                    param.AddParamRefCursor(0, "io_cursor");
                    param.AddParamInput(1, "strFindAll", findAll);
                    param.AddParamInput(2, "strMTL_SEQ_NO", mtl_seq_no);
                    param.AddParamInput(3, "strPARTY_ID", supplier);
                    param.AddParamInput(4, "strLOCATION_ID", locId);
                //}
                //else
                //{
                //     param = new ProcParam(1) { ProcedureName = "LOV_PACK.GET_MATERIAL_ALL_LIST" };

                //    param.AddParamRefCursor(0, "io_cursor");

                //}

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMaterail = new List<Material>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        material = new Material();

                        material.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        material.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        material.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        material.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        material.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        material.STD_QTY = OraDataReader.Instance.GetDecimal("STD_QTY");
                        material.UNIT = OraDataReader.Instance.GetString("UNIT");
                        //material.MTL_IMAGE = OraDataReader.Instance.GetBitmap("MTL_IMAGE");
                        //material.KANBAN = OraDataReader.Instance.GetLong("KANBAN");
                       // material.LOCATION_ID = OraDataReader.Instance.GetString("LOCATION_ID");
                        //material.BARCODE_SUPPLIER = OraDataReader.Instance.GetString("BARCODE_SUPPLIER");
                        //material.REMARK = OraDataReader.Instance.GetString("REMARK");
                        //material.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstMaterail.Add(material);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMaterail = null;
                throw ex;
            }

            return lstMaterail;
        }

        public Material GetMaterialCode(string strMTL_CODE, string strPARTY_ID)
        {
            //List<Material> lstMaterail = null;
            Material material = null;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_MATERIAL_CODE" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strMTL_CODE", strMTL_CODE);
                param.AddParamInput(2, "strPARTY_ID", strPARTY_ID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    material = new Material();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        material = new Material();

                        material.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        material.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        material.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        material.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        material.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        material.STD_QTY = OraDataReader.Instance.GetDecimal("STD_QTY");
                        material.UNIT = OraDataReader.Instance.GetString("UNIT");
                        //material.MTL_IMAGE = OraDataReader.Instance.GetBitmap("MTL_IMAGE");
                        //material.KANBAN = OraDataReader.Instance.GetLong("KANBAN");
                        //material.LOCATION_ID = OraDataReader.Instance.GetString("LOCATION_ID");
                        //material.BARCODE_SUPPLIER = OraDataReader.Instance.GetString("BARCODE_SUPPLIER");
                        //material.REMARK = OraDataReader.Instance.GetString("REMARK");
                        //material.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                material = null;
                throw ex;
            }

            return material;
        }

        public bool IsNumberOfPalateMaching(string arrNo, out string resultMessage)
        {
            resultMessage = string.Empty;
            string result = string.Empty;

            try
            {

                ProcParam procPara = new ProcParam(3) { ProcedureName = "ARRIVAL_PACK.ARR_NUMBER_PTL_MACHING" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.NVarchar2, 100);
                procPara.AddParamInput(1, "strARRIVAL_NO", arrNo);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.NVarchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                //inform time
                //this.executionTime = oracleData.ExecutionTime;

                result = procPara.Parameters[0].Value.ToString().Trim();
                resultMessage = procPara.Parameters[2].Value.ToString();


            }
            catch (Exception ex)
            {
                resultMessage = ex.Message;
                result = "";

            }

            if (result == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<MaterialCard> GetSelectMtlCard(int seqNo)
        {
            List<MaterialCard> lstMtlCard = null;
            MaterialCard mtlCard;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_IN_PACK.GET_MATERIAL_CARD" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMtlCard = new List<MaterialCard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtlCard = new MaterialCard();

                        mtlCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mtlCard.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mtlCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        mtlCard.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtlCard.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        mtlCard.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtlCard.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtlCard.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtlCard.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtlCard.CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS");
                        mtlCard.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        //prdCard.BARCODE = OraDataReader.Instance.GetBitmap("BARCODE");

                        mtlCard.NO_OF_PRINT = OraDataReader.Instance.GetInteger("NO_OF_PRINT");

                        lstMtlCard.Add(mtlCard);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMtlCard = null;
                throw ex;
            }

            return lstMtlCard;
        }

        public DataSet PrintProductCardReport(List<T_ARRIVAL_DTL> jobOrdNo, string prodSEQNo, List<MaterialCard> lstPrdCard, string userid, out int seqPrint)
        {
            //declare dataset and name.
            string processSEQ = string.Empty;

            //Bitmap imgProduct = null;
            seqPrint = -1;
            DataSet dtsResult = new DataSet("DTS_MATERIAL_CARD");
            //int seqPrint = 0;
            try
            {
               // processSEQ = this.ProductProcessSEQ(prodSEQNo);

                

                //imgProduct = this.ProductImage(prodSEQNo);

                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                List<ProcParam> procInsTPrintList = new List<ProcParam>();
                ProcParam procInsTPrint = null;
                foreach (MaterialCard prdCard in lstPrdCard)
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
                DataTable dtHeader = new DataTable("T_ARRIVAL");
                dtHeader.Columns.Add(new DataColumn("ARRIVAL_NO"));
                //dtHeader.Columns.Add(new DataColumn("PROD_IMAGE", typeof(System.Drawing.Bitmap)));
                //dtHeader.Columns.Add(new DataColumn("PROCESS_SEQ"));
                DataRow row = dtHeader.NewRow();

                foreach ( T_ARRIVAL_DTL labelOpt in jobOrdNo)
                {
                    row["ARRIVAL_NO"] = labelOpt.ARRIVAL_NO;
                    
                }

                //row["PROD_IMAGE"] = imgProduct;
                //row["PROCESS_SEQ"] = processSEQ;

                //dtHeader.Rows.Add(row);
                dtHeader.Rows.Add(row);
                dtHeader.AcceptChanges();
                dtsResult.Tables.Add(dtHeader);

                DataTable dtDetail = this.GetPrintMaterialCard(seqPrint, "T_MATERIAL_CARD");

                //dtDetail.Columns.Add(new DataColumn("PROD_IMAGE", typeof(System.Drawing.Bitmap)));

                //DataRow rowDT = dtDetail.NewRow();

                //foreach ( MaterialCard labelOpt in lstPrdCard)
                //{
                //    imgProduct = this.ProductImage(labelOpt.MTL_SEQ_NO);

                //    rowDT["PROD_IMAGE"] = imgProduct;

                //    //dtDetail.Rows.Add(rowDT);
                //}

                //foreach (DataRow dtRow in dtDetail.Rows)
                //{
                //    imgProduct = this.ProductImage((string)dtRow["MTL_SEQ_NO"]);
                //    dtRow["PROD_IMAGE"] = imgProduct;
                //}

                dtsResult.Tables.Add(dtDetail);

                //maping datatable to dataset
                dtsResult.Relations.Add("T_ARRIVAL_T_MATERIAL_CARD",
                                dtsResult.Tables["T_ARRIVAL"].Columns["ARRIVAL_NO"],
                                dtsResult.Tables["T_MATERIAL_CARD"].Columns["ARRIVAL_NO"],false);


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
            //string proSEQArr = string.Empty;
            //try
            //{
            //    ProcParam param = new ProcParam(2) { ProcedureName = "INFO.PROD_SEQ_PROCESS" };
            //    param.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 100);
            //    param.AddParamInput(1, "PROD_SEQ_NO", proSeqNo);

            //    GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
            //    //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

            //    OracleString dbResult = (OracleString)param.ReturnValue(0);

            //    if (!dbResult.IsNull)
            //    {
            //        return dbResult.Value;
            //    }
            //    else
            //    {
            //        return string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            return string.Empty;
            //}
        }

        private Bitmap ProductImage(string proSeqNo)
        {
            Bitmap result = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.MATERIAL_IMAGE" };

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

        private DataTable GetPrintMaterialCard(int seqNo, string tableName)
        {
            List<MaterialCard> lstMCard = null;
            MaterialCard mCard;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_IN_PACK.RPT_MATERIAL_CARD_LABEL" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMCard = new List<MaterialCard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mCard = new MaterialCard();

                        mCard.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mCard.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        mCard.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mCard.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mCard.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mCard.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mCard.LOCATION_NAME = OraDataReader.Instance.GetString("LOCATION_NAME");
                        mCard.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");

                        if (!OraDataReader.Instance.IsDBNull("LOT_DATE"))
                        {
                            mCard.LOT_DATE = OraDataReader.Instance.GetDateTime("LOT_DATE");
                        }

                        mCard.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mCard.NO_OF_LABEL = OraDataReader.Instance.GetString("NO_OF_LABEL");
                        mCard.MIN_QTY = OraDataReader.Instance.GetDecimal("MIN_QTY");
                        mCard.MAX_QTY = OraDataReader.Instance.GetDecimal("MAX_QTY");


                        //prdCard.BARCODE = UtilityBLL.QRCode_Encode(prdCard.SERIAL_NO);

                        //prdCard.BARCODE = OraDataReader.Instance.GetBitmap("BARCODE");
                        //prdCard.BARCODE.Save(string.Format("C:\\Temp\\PicTemp2D\\{0}.BMP", prdCard.SERIAL_NO));
                        mCard.MTL_IMAGE = OraDataReader.Instance.GetBitmap("MTL_IMAGE");

                        lstMCard.Add(mCard);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMCard = null;
                throw ex;
            }

            //return this.ListToDataTable(lstPrdCard, tableName);
            //DataTable dt  = UtilityBLL.ListToDataTable(lstPrdCard, tableName);
            return UtilityBLL.ListToDataTable(lstMCard, tableName);
        }

   

        public void InsertLotPlaningToPrint(int seqNo, List<T_ARRIVAL_DTL> lstJobLotPlan)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstJobLotPlan.Count);
            ProcParam param = null;
            try
            {
                foreach (T_ARRIVAL_DTL jLotPlan in lstJobLotPlan)
                {
                    param = new ProcParam(4);
                    param.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";
                    param.AddParamInput(0, "strSEQ_NO", seqNo);
                    param.AddParamInput(1, "strTR1", jLotPlan.ARRIVAL_NO);
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

        public DataTable GetPrintTime(int seq)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_IN_PACK.GET_PRINTED_CARD" };
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
                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_IN_PACK.UPD_PRT_MATERIAL_CARD" };

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

        public List<M_ARRIVAL_TYPE> GetArrivalTypeList()
        {
            List<M_ARRIVAL_TYPE> lstArrivalType = null;
            M_ARRIVAL_TYPE productType;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MTL_IN_PACK.GET_M_ARRIVAL_TYPE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstArrivalType = new List<M_ARRIVAL_TYPE>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        productType = new M_ARRIVAL_TYPE();

                        productType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        productType.NAME = OraDataReader.Instance.GetString("NAME");
                        productType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        productType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstArrivalType.Add(productType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstArrivalType = null;
                throw ex;
            }

            return lstArrivalType;
        }


        #endregion

    }
}
