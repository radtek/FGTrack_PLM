using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using System.IO;
using System.Drawing;
using Oracle.DataAccess.Types;
using System.Drawing.Imaging;
using System.Data;
using System.Globalization;

namespace HTN.BITS.BLL.PLASESS
{
    public class MaterialBLL : IDisposable
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

        ~MaterialBLL()
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

        public MaterialBLL()
        {
            //constructer
        }


        public List<Material> GetMaterialList(string findAll)
        {
            List<Material> lstMaterail = null;
            Material material;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_M_MATERIAL" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findAll);
                param.AddParamInput(2, "strMTL_SEQ_NO", DBNull.Value);

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
                        material.UNIT = OraDataReader.Instance.GetString("UNIT");
                        material.STD_QTY = OraDataReader.Instance.GetDecimal("STD_QTY");
                        material.MIN_QTY = OraDataReader.Instance.GetDecimal("MIN_QTY");
                        material.MAX_QTY = OraDataReader.Instance.GetDecimal("MAX_QTY");
                        material.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        material.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        //material.MTL_IMAGE = OraDataReader.Instance.GetBitmap("MTL_IMAGE");
                        material.LOCATION_ID = OraDataReader.Instance.GetString("LOCATION_ID");
                        material.REMARK = OraDataReader.Instance.GetString("REMARK");
                        material.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

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

        public Material GetMaterial(string mtlSeq)
        {
            Material material = null;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_M_MATERIAL" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strMTL_SEQ_NO", mtlSeq);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        material = new Material();

                        material.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        material.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        material.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        material.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        material.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        material.UNIT = OraDataReader.Instance.GetString("UNIT");
                        material.STD_QTY = OraDataReader.Instance.GetDecimal("STD_QTY");
                        material.MIN_QTY = OraDataReader.Instance.GetDecimal("MIN_QTY");
                        material.MAX_QTY = OraDataReader.Instance.GetDecimal("MAX_QTY");
                        material.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        material.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        material.MTL_IMAGE = OraDataReader.Instance.GetBitmap("MTL_IMAGE");
                        material.LOCATION_ID = OraDataReader.Instance.GetString("LOCATION_ID");
                        material.REMARK = OraDataReader.Instance.GetString("REMARK");
                        material.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

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

        public string InsertMaterial(ref Material material, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(16) { ProcedureName = "MASTER_PACK.M_MATERIAL_INS" };

                param.AddParamInOutput(0, "strMTL_SEQ_NO", OracleDbType.Varchar2, 255, material.MTL_SEQ_NO); //OracleDbType.Varchar2, prod.PROD_SEQ_NO
                param.AddParamInput(1, "strMTL_CODE", material.MTL_CODE);
                param.AddParamInput(2, "strMTL_NAME", material.MTL_NAME);
                param.AddParamInput(3, "strMTL_GRADE", material.MTL_GRADE);
                param.AddParamInput(4, "strMTL_COLOR", material.MTL_COLOR);
                param.AddParamInput(5, "strUNIT", material.UNIT);
                param.AddParamInput(6, "strSTD_QTY", material.STD_QTY);
                param.AddParamInput(7, "strMIN_QTY", material.MIN_QTY);
                param.AddParamInput(8, "strMAX_QTY", material.MAX_QTY);
                param.AddParamInput(9, "strPARTY_ID", material.PARTY_ID);

                if (material.MTL_IMAGE != null)
                {
                    param.AddParamBLOBInput(10, "strMTL_IMAGE", OracleDbType.Blob, this.BitmapToByteArray(material.MTL_IMAGE)); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                else
                {
                    param.AddParamInput(10, "strMTL_IMAGE", DBNull.Value); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                
                param.AddParamInput(11, "strLOCATION_ID", material.LOCATION_ID);
                param.AddParamInput(12, "strREMARK", material.REMARK);
                param.AddParamInput(13, "strUSER_ID", userid);
                param.AddParamInput(14, "strREC_STAT", (material.REC_STAT ? "Y" : "N"));
                param.AddParamOutput(15, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString mtlSeq = (OracleString)param.ReturnValue(0);

                OracleString result = (OracleString)param.ReturnValue(15);

                if (!result.IsNull)
                {
                    material.MTL_SEQ_NO = mtlSeq.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateMaterial(Material material, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(16) { ProcedureName = "MASTER_PACK.M_MATERIAL_UPD" };

                param.AddParamInput(0, "strMTL_SEQ_NO", material.MTL_SEQ_NO);
                param.AddParamInput(1, "strMTL_CODE", material.MTL_CODE);
                param.AddParamInput(2, "strMTL_NAME", material.MTL_NAME);
                param.AddParamInput(3, "strMTL_GRADE", material.MTL_GRADE);
                param.AddParamInput(4, "strMTL_COLOR", material.MTL_COLOR);
                param.AddParamInput(5, "strUNIT", material.UNIT);
                param.AddParamInput(6, "strSTD_QTY", material.STD_QTY);
                param.AddParamInput(7, "strMIN_QTY", material.MIN_QTY);
                param.AddParamInput(8, "strMAX_QTY", material.MAX_QTY);
                param.AddParamInput(9, "strPARTY_ID", material.PARTY_ID);

                if (material.MTL_IMAGE != null)
                {
                    param.AddParamBLOBInput(10, "strMTL_IMAGE", OracleDbType.Blob, this.BitmapToByteArray(material.MTL_IMAGE));
                }
                else
                {
                    param.AddParamInput(10, "strMTL_IMAGE", DBNull.Value);
                }

                param.AddParamInput(11, "strLOCATION_ID", material.LOCATION_ID);
                param.AddParamInput(12, "strREMARK", material.REMARK);
                param.AddParamInput(13, "strUSER_ID", userid);
                param.AddParamInput(14, "strREC_STAT", (material.REC_STAT ? "Y" : "N"));
                param.AddParamOutput(15, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(15);

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

        private byte[] BitmapToByteArray(System.Drawing.Bitmap bitmapIn)
        {
            MemoryStream ms = new MemoryStream();
            bitmapIn.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }

        public string DeleteMaterial(string mtlSeq, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.M_MATERIAL_DEL" };

                param.AddParamInput(0, "strMTL_SEQ_NO", mtlSeq);
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

        public List<Unit> GetUnitList()
        {
            List<Unit> lstUnit = null;
            Unit unit;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_UNIT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstUnit = new List<Unit>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        unit = new Unit();

                        unit.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        unit.NAME = OraDataReader.Instance.GetString("NAME");
                        unit.REMARK = OraDataReader.Instance.GetString("REMARK");
                        unit.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstUnit.Add(unit);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstUnit = null;
                throw ex;
            }

            return lstUnit;
        }

        public List<Location> GetLocationList()
        {
            List<Location> lstLocation = null;
            Location location;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_LOCATION_LIST" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstLocation = new List<Location>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        location = new Location();

                        location.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        location.NAME = OraDataReader.Instance.GetString("NAME");
                        location.REMARK = OraDataReader.Instance.GetString("REMARK");
                        location.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstLocation.Add(location);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstLocation = null;
                throw ex;
            }

            return lstLocation;
        }


        #region MATERIAL SUPPLY SHEET

        /*

        public void ClearTempUploadSupplySheet()
        {
            try
            {
                GlobalDB.Instance.DataAc.ExecuteNonQuery(@"TRUNCATE TABLE TEMP_UPLOAD_SUPPLY_SHEET PRESERVE MATERIALIZED VIEW LOG");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadSupplySheetToTemp(DataTable dtbUploadOrder)
        {
            string resultMsg = string.Empty;

            try
            {
                if (dtbUploadOrder != null && dtbUploadOrder.Rows.Count > 0)
                {
                    
                    ProcParam paramIns = new ProcParam(16) { ProcedureName = "MTL_OUT_PACK.UPLOAD_MTL_SUPPLY_SHEET" };

                    //CUSTOMER
                    var arrCUSTOMER = (from DataRow row in dtbUploadOrder.Rows
                                    select row["F1"]).ToArray();
                    paramIns.AddParamInput(0, "strCUSTOMER", arrCUSTOMER, OracleDbType.Varchar2);

                    //PRODUCT_NO
                    var arrPRODUCT_NO = (from DataRow row in dtbUploadOrder.Rows
                                       select row["F2"]).ToArray();
                    paramIns.AddParamInput(1, "strPRODUCT_NO", arrPRODUCT_NO, OracleDbType.Varchar2);

                    //PRODUCT_NAME
                    var arrPRODUCT_NAME = (from DataRow row in dtbUploadOrder.Rows
                                     select row["F3"]).ToArray();
                    paramIns.AddParamInput(2, "strPRODUCT_NAME", arrPRODUCT_NAME, OracleDbType.Varchar2);

                    //MTL_CODE
                    var arrMTL_CODE = (from DataRow row in dtbUploadOrder.Rows
                                       select row["F4"]).ToArray();
                    paramIns.AddParamInput(3, "strMTL_CODE", arrMTL_CODE, OracleDbType.Varchar2);

                    //MTL_TYPE
                    var arrMTL_TYPE = (from DataRow row in dtbUploadOrder.Rows
                                  select row["F5"]).ToArray();
                    paramIns.AddParamInput(4, "strMTL_TYPE", arrMTL_TYPE, OracleDbType.Varchar2);

                    //MTL_GRADE
                    var arrMTL_GRADE = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F6"]).ToArray();
                    paramIns.AddParamInput(5, "strMTL_GRADE", arrMTL_GRADE, OracleDbType.Varchar2);

                    //MTL_COLOR
                    var arrMTL_COLOR = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F7"]).ToArray();
                    paramIns.AddParamInput(6, "strMTL_COLOR", arrMTL_COLOR, OracleDbType.Varchar2);

                    //QTY1
                    var arrQTY1 = (from DataRow row in dtbUploadOrder.Rows
                                      select row["F8"]).ToArray();
                    paramIns.AddParamInput(7, "strQTY1", arrQTY1, OracleDbType.Decimal);

                    //QTY2 
                    var arrQTY2 = (from DataRow row in dtbUploadOrder.Rows
                                  select row["F9"]).ToArray();
                    paramIns.AddParamInput(8, "strQTY2", arrQTY2, OracleDbType.Decimal);

                    //CAP_D
                    var arrCAP_D = (from DataRow row in dtbUploadOrder.Rows
                                      select row["F14"]).ToArray();
                    paramIns.AddParamInput(9, "strCAP_D", arrCAP_D, OracleDbType.Int32);

                    //DAYS
                    var arrDAYS = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F12"]).ToArray();
                    paramIns.AddParamInput(10, "strDAYS", arrDAYS, OracleDbType.Int32);

                    //ORDER_DATE
                    var arrORDER_DATE = (from DataRow row in dtbUploadOrder.Rows
                                         select (object)Convert.ToDateTime(row["F13"], DateTimeFormatInfo.CurrentInfo)).ToArray();
                    paramIns.AddParamInput(11, "strORDER_DATE", arrORDER_DATE, OracleDbType.Date);

                    //ORDER_CARD_NO
                    var arrORDER_CARD_NO = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F15"]).ToArray();
                    paramIns.AddParamInput(12, "strORDER_CARD_NO", arrORDER_CARD_NO, OracleDbType.Varchar2);

                    //MC_NO
                    var arrMC_NO = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F0"]).ToArray();
                    paramIns.AddParamInput(13, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    //START_TIME
                    var arrSTART_TIME = (from DataRow row in dtbUploadOrder.Rows
                                         select row["F16"]).ToArray();
                    paramIns.AddParamInput(14, "strSTART_TIME", arrSTART_TIME, OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(15, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dtbUploadOrder.Rows.Count);


                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramIns, dtbUploadOrder.Rows.Count);

                    if (GlobalDB.Instance.LastException != null)
                        throw GlobalDB.Instance.LastException;

                    resultMsg = "OK";

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultMsg;
        }

        public DataTable GetUploadSupplySheet()
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "MTL_OUT_PACK.GET_UPLOAD_MTL_SHPPLY_SHEET" };
                param.AddParamRefCursor(0, "io_cursor");

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public string InsertMtlSupplySheet(DataTable dtbInsertOrder, string userid)
        {
            string resultMsg = string.Empty;

            try
            {

                #region New Material Supply Sheet Order

                DataRow[] insertRows = dtbInsertOrder.Select("[ACTION] = 2");

                ProcParam paramIns = null;

                if (insertRows.Length > 0)
                {
                    paramIns = new ProcParam(17) { ProcedureName = "MTL_OUT_PACK.INSERT_ORDER_CARD" };

                    //ORDER_CARD_NO
                    var arrORDER_CARD_NO = (from DataRow row in insertRows
                                            select row["ORDER_CARD_NO"]).ToArray();
                    paramIns.AddParamInput(0, "strORDER_CARD_NO", arrORDER_CARD_NO, OracleDbType.Varchar2);

                    //ORDER_DATE
                    var arrORDER_DATE = (from DataRow row in insertRows
                                         select row["ORDER_DATE"]).ToArray();
                    paramIns.AddParamInput(1, "strORDER_DATE", arrORDER_DATE, OracleDbType.Date);

                    //CUSTOMER
                    var arrCUSTOMER = (from DataRow row in insertRows
                                       select row["CUSTOMER"]).ToArray();
                    paramIns.AddParamInput(2, "strCUSTOMER", arrCUSTOMER, OracleDbType.Varchar2);

                    //MC_NO
                    var arrMC_NO = (from DataRow row in insertRows
                                    select row["MC_NO"]).ToArray();
                    paramIns.AddParamInput(3, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    //PROD_SEQ_NO
                    var arrPROD_SEQ_NO = (from DataRow row in insertRows
                                          select row["PROD_SEQ_NO"]).ToArray();
                    paramIns.AddParamInput(4, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    //MTL_SEQ_NO
                    var arrMTL_SEQ_NO = (from DataRow row in insertRows
                                         select row["MTL_SEQ_NO"]).ToArray();
                    paramIns.AddParamInput(5, "strMTL_SEQ_NO", arrMTL_SEQ_NO, OracleDbType.Varchar2);

                    //MTL_TYPE
                    var arrMTL_TYPE = (from DataRow row in insertRows
                                       select row["MTL_TYPE"]).ToArray();
                    paramIns.AddParamInput(6, "strMTL_TYPE", arrMTL_TYPE, OracleDbType.Varchar2);

                    //MTL_GRADE
                    var arrMTL_GRADE = (from DataRow row in insertRows
                                        select row["MTL_GRADE"]).ToArray();
                    paramIns.AddParamInput(7, "strMTL_GRADE", arrMTL_GRADE, OracleDbType.Varchar2);

                    //MTL_COLOR
                    var arrMTL_COLOR = (from DataRow row in insertRows
                                        select row["MTL_COLOR"]).ToArray();
                    paramIns.AddParamInput(8, "strMTL_COLOR", arrMTL_COLOR, OracleDbType.Varchar2);

                    //UNIT_ID
                    var arrUNIT_ID = (from DataRow row in insertRows
                                      select row["MTL_UNIT"]).ToArray();
                    paramIns.AddParamInput(9, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    //QTY1
                    var arrQTY1 = (from DataRow row in insertRows
                                   select row["QTY1"]).ToArray();
                    paramIns.AddParamInput(10, "strQTY1", arrQTY1, OracleDbType.Decimal);

                    //QTY2
                    var arrQTY2 = (from DataRow row in insertRows
                                   select row["QTY2"]).ToArray();
                    paramIns.AddParamInput(11, "strQTY2", arrQTY2, OracleDbType.Decimal);

                    //DAYS
                    var arrDAYS = (from DataRow row in insertRows
                                   select row["DAYS"]).ToArray();
                    paramIns.AddParamInput(12, "strDAYS", arrDAYS, OracleDbType.Int32);

                    //CAP_D
                    var arrCAP_D = (from DataRow row in insertRows
                                    select row["CAP_D"]).ToArray();
                    paramIns.AddParamInput(13, "strCAP_D", arrCAP_D, OracleDbType.Int32);

                    //START_TIME
                    var arrSTART_TIME = (from DataRow row in insertRows
                                         select row["START_TIME"]).ToArray();
                    paramIns.AddParamInput(14, "strSTART_TIME", arrSTART_TIME, OracleDbType.Varchar2);

                    //USER_ID
                    paramIns.AddParamInput(15, "strUSER_ID", ArrayOf<object>.Create(insertRows.Count(), userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(16, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", insertRows.Count());

                }

                #endregion

                #region Replace Material Supply Sheet Order

                DataRow[] updateRows = dtbInsertOrder.Select("[ACTION] = 3");

                ProcParam paramUpd = null;

                if (updateRows.Length > 0)
                {
                    paramUpd = new ProcParam(17) { ProcedureName = "MTL_OUT_PACK.UPDATE_ORDER_CARD" };

                    //ORDER_CARD_NO
                    var arrORDER_CARD_NO = (from DataRow row in updateRows
                                            select row["ORDER_CARD_NO"]).ToArray();
                    paramUpd.AddParamInput(0, "strORDER_CARD_NO", arrORDER_CARD_NO, OracleDbType.Varchar2);

                    //ORDER_DATE
                    var arrORDER_DATE = (from DataRow row in updateRows
                                         select row["ORDER_DATE"]).ToArray();
                    paramUpd.AddParamInput(1, "strORDER_DATE", arrORDER_DATE, OracleDbType.Date);

                    //CUSTOMER
                    var arrCUSTOMER = (from DataRow row in updateRows
                                       select row["CUSTOMER"]).ToArray();
                    paramUpd.AddParamInput(2, "strCUSTOMER", arrCUSTOMER, OracleDbType.Varchar2);

                    //MC_NO
                    var arrMC_NO = (from DataRow row in updateRows
                                    select row["MC_NO"]).ToArray();
                    paramUpd.AddParamInput(3, "strMC_NO", arrMC_NO, OracleDbType.Varchar2);

                    //PROD_SEQ_NO
                    var arrPROD_SEQ_NO = (from DataRow row in updateRows
                                          select row["PROD_SEQ_NO"]).ToArray();
                    paramUpd.AddParamInput(4, "strPROD_SEQ_NO", arrPROD_SEQ_NO, OracleDbType.Varchar2);

                    //MTL_SEQ_NO
                    var arrMTL_SEQ_NO = (from DataRow row in updateRows
                                         select row["MTL_SEQ_NO"]).ToArray();
                    paramUpd.AddParamInput(5, "strMTL_SEQ_NO", arrMTL_SEQ_NO, OracleDbType.Varchar2);

                    //MTL_TYPE
                    var arrMTL_TYPE = (from DataRow row in updateRows
                                       select row["MTL_TYPE"]).ToArray();
                    paramUpd.AddParamInput(6, "strMTL_TYPE", arrMTL_TYPE, OracleDbType.Varchar2);

                    //MTL_GRADE
                    var arrMTL_GRADE = (from DataRow row in updateRows
                                        select row["MTL_GRADE"]).ToArray();
                    paramUpd.AddParamInput(7, "strMTL_GRADE", arrMTL_GRADE, OracleDbType.Varchar2);

                    //MTL_COLOR
                    var arrMTL_COLOR = (from DataRow row in updateRows
                                        select row["MTL_COLOR"]).ToArray();
                    paramUpd.AddParamInput(8, "strMTL_COLOR", arrMTL_COLOR, OracleDbType.Varchar2);

                    //UNIT_ID
                    var arrUNIT_ID = (from DataRow row in updateRows
                                      select row["MTL_UNIT"]).ToArray();
                    paramUpd.AddParamInput(9, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    //QTY1
                    var arrQTY1 = (from DataRow row in updateRows
                                   select row["QTY1"]).ToArray();
                    paramUpd.AddParamInput(10, "strQTY1", arrQTY1, OracleDbType.Decimal);

                    //QTY2
                    var arrQTY2 = (from DataRow row in updateRows
                                   select row["QTY2"]).ToArray();
                    paramUpd.AddParamInput(11, "strQTY2", arrQTY2, OracleDbType.Decimal);

                    //DAYS
                    var arrDAYS = (from DataRow row in updateRows
                                   select row["DAYS"]).ToArray();
                    paramUpd.AddParamInput(12, "strDAYS", arrDAYS, OracleDbType.Int32);

                    //CAP_D
                    var arrCAP_D = (from DataRow row in updateRows
                                    select row["CAP_D"]).ToArray();
                    paramUpd.AddParamInput(13, "strCAP_D", arrCAP_D, OracleDbType.Int32);

                    //START_TIME
                    var arrSTART_TIME = (from DataRow row in updateRows
                                         select row["START_TIME"]).ToArray();
                    paramUpd.AddParamInput(14, "strSTART_TIME", arrSTART_TIME, OracleDbType.Varchar2);

                    //USER_ID
                    paramUpd.AddParamInput(15, "strUSER_ID", ArrayOf<object>.Create(updateRows.Count(), userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramUpd.AddParamOutput(16, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", updateRows.Count());

                }

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramIns, insertRows.Count(), paramUpd, updateRows.Count());

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

        public List<MtlSupplySheet> GetMtlSupplySheet(string mc, DateTime? fromDate, DateTime? toDate, string findAll)
        {
            List<MtlSupplySheet> lstMtlSheet = null;
            MtlSupplySheet mtlSheet;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "MTL_OUT_PACK.GET_ORDER_CARD" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strORDER_CARD_NO", DBNull.Value);
                param.AddParamInput(2, "strMC_NO", mc);
                if (fromDate.HasValue)
                {
                    param.AddParamInput(3, "strORDER_DATE_F", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strORDER_DATE_F", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(4, "strORDER_DATE_T", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strORDER_DATE_T", DBNull.Value);
                }
                param.AddParamInput(5, "strFindAll", findAll);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMtlSheet = new List<MtlSupplySheet>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtlSheet = new MtlSupplySheet();

                        mtlSheet.ORDER_CARD_NO = OraDataReader.Instance.GetString("ORDER_CARD_NO");
                        mtlSheet.ORDER_DATE = OraDataReader.Instance.GetDateTime("ORDER_DATE");
                        mtlSheet.CUSTOMER = OraDataReader.Instance.GetString("CUSTOMER");
                        mtlSheet.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        mtlSheet.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        mtlSheet.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        mtlSheet.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        mtlSheet.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        mtlSheet.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtlSheet.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtlSheet.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        mtlSheet.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtlSheet.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtlSheet.STD_QTY = OraDataReader.Instance.GetInteger("STD_QTY");
                        mtlSheet.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtlSheet.QTY = OraDataReader.Instance.GetInteger("QTY");
                        mtlSheet.ADD_QTY = OraDataReader.Instance.GetInteger("ADD_QTY");
                        mtlSheet.OUT_QTY = OraDataReader.Instance.GetInteger("OUT_QTY");
                        mtlSheet.DAYS = OraDataReader.Instance.GetInteger("DAYS");
                        if (!OraDataReader.Instance.IsDBNull("START_DATE"))
                        {
                            mtlSheet.START_DATE = OraDataReader.Instance.GetDateTime("START_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("END_DATE"))
                        {
                            mtlSheet.END_DATE = OraDataReader.Instance.GetDateTime("END_DATE");
                        }
                        mtlSheet.REMARK = OraDataReader.Instance.GetString("REMARK");
                        mtlSheet.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        mtlSheet.CAP_D = OraDataReader.Instance.GetInteger("CAP_D");
                        mtlSheet.START_TIME = OraDataReader.Instance.GetString("START_TIME");

                        //add new property
                        mtlSheet.N_USER_ID = OraDataReader.Instance.GetString("N_USER_ID");
                        if (!OraDataReader.Instance.IsDBNull("N_USER_DATE"))
                        {
                            mtlSheet.N_USER_DATE = OraDataReader.Instance.GetDateTime("N_USER_DATE");
                        }
                        mtlSheet.U_USER_ID = OraDataReader.Instance.GetString("U_USER_ID");
                        if (!OraDataReader.Instance.IsDBNull("U_USER_DATE"))
                        {
                            mtlSheet.U_USER_DATE = OraDataReader.Instance.GetDateTime("U_USER_DATE");
                        }

                        lstMtlSheet.Add(mtlSheet);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMtlSheet = null;
                throw ex;
            }

            return lstMtlSheet;
        }

        public List<MaterialCard> GetOrderMaterialCard(string orderno)
        {
            List<MaterialCard> lstMtlCard = null;
            MaterialCard mtlCard;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MTL_OUT_PACK.GET_ORDER_MTL_CARD" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strORDER_CARD_NO", orderno);
                
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
                        mtlCard.LOT_DATE = OraDataReader.Instance.GetDateTime("LOT_DATE");
                        mtlCard.MTL_SEQ_NO = OraDataReader.Instance.GetString("MTL_SEQ_NO");
                        mtlCard.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtlCard.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtlCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        mtlCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");

                        if (!OraDataReader.Instance.IsDBNull("STOCK_IN_DATE"))
                        {
                            mtlCard.STOCK_IN_DATE = OraDataReader.Instance.GetDateTime("STOCK_IN_DATE");
                        }

                        mtlCard.PIC_STOCK_IN = OraDataReader.Instance.GetString("PIC_STOCK_IN");

                        if (!OraDataReader.Instance.IsDBNull("STOCK_OUT_DATE"))
                        {
                            mtlCard.STOCK_OUT_DATE = OraDataReader.Instance.GetDateTime("STOCK_OUT_DATE");
                        }

                        mtlCard.PIC_STOCK_OUT = OraDataReader.Instance.GetString("PIC_STOCK_OUT");


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

        public string UpdateMtlSupplySheetQty(string orderno, DateTime orderDate, int days, int qty, int addQty, string starttime, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(8) { ProcedureName = "MTL_OUT_PACK.UPDATE_ORDER_CARD_QTY" };

                param.AddParamInput(0, "strORDER_CARD_NO", orderno);
                param.AddParamInput(1, "strORDER_DATE", orderDate);
                param.AddParamInput(2, "strDAYS", days);
                param.AddParamInput(3, "strQTY", qty);
                param.AddParamInput(4, "strADD_QTY", addQty);
                param.AddParamInput(5, "strSTART_TIME", starttime);
                param.AddParamInput(6, "strUSER_ID", userid);
                param.AddParamOutput(7, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

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
         * */



        #endregion

    }
}
