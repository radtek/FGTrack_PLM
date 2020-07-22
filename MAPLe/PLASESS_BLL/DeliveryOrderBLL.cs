using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HTN.BITS.BLL.PLASESS
{
    public class DeliveryOrderBLL : IDisposable
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

        ~DeliveryOrderBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        ////private TimeSpan executionTime;

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

        public DeliveryOrderBLL()
        {
            //constructer
        }

        public List<ProductionType> GetProductionTypeList()
        {
            List<ProductionType> lstProductionType = null;
            ProductionType productType;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "DO_PACK.LOV_PROD_TYPE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstProductionType = new List<ProductionType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        productType = new ProductionType();

                        productType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        productType.NAME = OraDataReader.Instance.GetString("NAME");
                        productType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        productType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstProductionType.Add(productType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProductionType = null;
                throw ex;
            }

            return lstProductionType;
        }

        public List<Warehouse> GetDestinationList()
        {
            List<Warehouse> lstWH = null;
            Warehouse wareHouse;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "DO_PACK.LOV_DESTINATION" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstWH = new List<Warehouse>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        wareHouse = new Warehouse();

                        wareHouse.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        wareHouse.NAME = OraDataReader.Instance.GetString("NAME");

                        lstWH.Add(wareHouse);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstWH = null;
                throw ex;
            }

            return lstWH;
        }

        public List<DeliveryOrder> GetDeliveryOrderList(string findValue, string doNo, string refNo, string prodType, string desTo, 
                                                        DateTime? doFormDate, DateTime? doToDate, DateTime? deFormDate, DateTime? deToDate)
        {
            List<DeliveryOrder> lstDeliOrder = null;
            DeliveryOrder delOrd;

            try
            {
                ProcParam param = new ProcParam(10) { ProcedureName = "DO_PACK.GET_DO_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strDO_NO", doNo);
                param.AddParamInput(3, "strREF_NO", refNo);
                param.AddParamInput(4, "strPROD_TYPE", prodType);
                param.AddParamInput(5, "strDEST_TO", desTo);
                //DO Date
                if (doFormDate.HasValue)
                {
                    param.AddParamInput(6, "strDO_DT_F", doFormDate.Value);
                }
                else
                {
                    param.AddParamInput(6, "strDO_DT_F", DBNull.Value);
                }
                if (doToDate.HasValue)
                {
                    param.AddParamInput(7, "strDO_DT_T", doToDate.Value);
                }
                else
                {
                    param.AddParamInput(7, "strDO_DT_T", DBNull.Value);
                }

                //Delivery Date
                if (deFormDate.HasValue)
                {
                    param.AddParamInput(8, "strDEL_DT_F", deFormDate.Value);
                }
                else
                {
                    param.AddParamInput(8, "strDEL_DT_F", DBNull.Value);
                }
                if (deToDate.HasValue)
                {
                    param.AddParamInput(9, "strDEL_DT_T", deToDate.Value);
                }
                else
                {
                    param.AddParamInput(9, "strDEL_DT_T", DBNull.Value);
                }


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstDeliOrder = new List<DeliveryOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        delOrd = new DeliveryOrder();

                        delOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        delOrd.DO_NO = OraDataReader.Instance.GetString("DO_NO");
                        if (!OraDataReader.Instance.IsDBNull("DO_DATE"))
                        {
                            delOrd.DO_DATE = OraDataReader.Instance.GetDateTime("DO_DATE");
                        }
                        delOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        delOrd.TO_DEST = OraDataReader.Instance.GetString("TO_DEST");
                        if (!OraDataReader.Instance.IsDBNull("DELIVERY_DATE"))
                        {
                            delOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        }
                        delOrd.QTY_PCS = OraDataReader.Instance.GetInteger("QTY_PCS");
                        delOrd.QTY_BOX = OraDataReader.Instance.GetInteger("QTY_BOX");
                        delOrd.DELIVERY_QTY = OraDataReader.Instance.GetInteger("DELIVERY_QTY");
                        delOrd.DELIVERY_BOX = OraDataReader.Instance.GetInteger("DELIVERY_BOX");
                        delOrd.REMARK = OraDataReader.Instance.GetString("REMARK");

                        lstDeliOrder.Add(delOrd);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstDeliOrder = null;
                throw ex;
            }

            return lstDeliOrder;
        }

        public DeliveryOrder GetDeliveryOrder(string doNo)
        {
            DeliveryOrder delOrd = null;

            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "DO_PACK.GET_DO_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strDO_NO", doNo);
                param.AddParamInput(3, "strREF_NO", DBNull.Value);
                param.AddParamInput(4, "strDO_DT_F", DBNull.Value);
                param.AddParamInput(5, "strDO_DT_T", DBNull.Value);
                param.AddParamInput(6, "strPROD_TYPE", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        delOrd = new DeliveryOrder();

                        delOrd.DO_NO = OraDataReader.Instance.GetString("DO_NO");
                        if (!OraDataReader.Instance.IsDBNull("DO_DATE"))
                        {
                            delOrd.DO_DATE = OraDataReader.Instance.GetDateTime("DO_DATE");
                        }
                        delOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        delOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        delOrd.TO_DEST = OraDataReader.Instance.GetString("TO_DEST");
                        if (!OraDataReader.Instance.IsDBNull("DELIVERY_DATE"))
                        {
                            delOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        }
                        delOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        delOrd.REC_STAT = OraDataReader.Instance.GetString("REC_STAT").Equals("Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                delOrd = null;
                throw ex;
            }

            return delOrd;
        }

        public DataTable GetDeliveryOrderDetail(string doNo)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "DO_PACK.GET_DO_DTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strDO_NO", doNo);
                param.AddParamInput(3, "strLINE_NO", DBNull.Value);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public List<Product> LovProductList(string findValue, string prodType)
        {
            List<Product> lstProduct = null;
            Product product;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "DO_PACK.LOV_PRODUCT" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPROD_SEQ_NO", DBNull.Value);
                param.AddParamInput(3, "strPROD_TYPE", prodType);
                param.AddParamInput(4, "strPROD_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstProduct = new List<Product>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        product = new Product();

                        product.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        product.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        product.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        product.MATERIAL_NAME = OraDataReader.Instance.GetString("MATERIAL_NAME");
                        product.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        product.UNIT = OraDataReader.Instance.GetString("UNIT");

                        lstProduct.Add(product);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProduct = null;
                throw ex;
            }

            return lstProduct;
        }

        public Product GetProductDetial(string prodNo)
        {
            Product product = null;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "DO_PACK.LOV_PRODUCT" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPROD_SEQ_NO", DBNull.Value);
                param.AddParamInput(3, "strPROD_TYPE", DBNull.Value);
                param.AddParamInput(4, "strPROD_NO", prodNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        product = new Product();

                        product.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        product.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        product.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        product.MATERIAL_NAME = OraDataReader.Instance.GetString("MATERIAL_NAME");
                        product.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        product.UNIT = OraDataReader.Instance.GetString("UNIT");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                product = null;
                throw ex;
            }

            return product;
        }

        public string InsertDeliveryOrder(ref DeliveryOrder delOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10) { ProcedureName = "DO_PACK.DO_HDR_INS" };

                paramHDR.AddParamInOutput(0, "strDO_NO", OracleDbType.Varchar2, 255, delOrd.DO_NO);
                paramHDR.AddParamInput(1, "strDO_DATE ", delOrd.DO_DATE);
                paramHDR.AddParamInput(2, "strREF_NO", delOrd.REF_NO);
                paramHDR.AddParamInput(3, "strPROD_TYPE", delOrd.PROD_TYPE);
                paramHDR.AddParamInput(4, "strTO_DEST", delOrd.TO_DEST);
                paramHDR.AddParamInput(5, "strDELIVERY_DATE", delOrd.DELIVERY_DATE);
                paramHDR.AddParamInput(6, "strREMARK", delOrd.REMARK);
                paramHDR.AddParamInput(7, "strREC_STAT", (delOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(8, "strUSER_ID", userid);
                paramHDR.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                #region "Transcation Detail"

                List<ProcParam> lstParam = new List<ProcParam>();
                ProcParam paramDTL = null;

                foreach (DeliveryOrderDtl delDtl in delOrd.DELIVERY_ORD_DTL)
                {
                    paramDTL = new ProcParam(8);
                    paramDTL.ProcedureName = "DO_PACK.DO_DTL_INS";

                    paramDTL.AddParamInput(0, "strDO_NO", DBNull.Value);
                    paramDTL.AddParamInput(1, "strPROD_SEQ_NO", delDtl.PROD_SEQ_NO);
                    paramDTL.AddParamInput(2, "strQTY", delDtl.QTY);
                    paramDTL.AddParamInput(3, "strUNIT_ID", delDtl.UNIT_ID);
                    paramDTL.AddParamInput(4, "strREMARK", delDtl.REMARK);
                    paramDTL.AddParamInput(5, "strREC_STAT ", "Y"); //default is 'Y'
                    paramDTL.AddParamInput(6, "strUSER_ID", userid);
                    paramDTL.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    lstParam.Add(paramDTL);
                }
                #endregion


                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, lstParam, 0, 0);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)paramHDR.ReturnValue(0);
                OracleString result = (OracleString)paramHDR.ReturnValue(9);

                if (!result.IsNull)
                {
                    delOrd.DO_NO = resultDB.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateDeliveryOrder(DeliveryOrder delOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10) { ProcedureName = "DO_PACK.DO_HDR_UPD" };

                paramHDR.AddParamInput(0, "strDO_NO", delOrd.DO_NO);
                paramHDR.AddParamInput(1, "strDO_DATE ", delOrd.DO_DATE);
                paramHDR.AddParamInput(2, "strREF_NO", delOrd.REF_NO);
                paramHDR.AddParamInput(3, "strPROD_TYPE", delOrd.PROD_TYPE);
                paramHDR.AddParamInput(4, "strTO_DEST", delOrd.TO_DEST);
                paramHDR.AddParamInput(5, "strDELIVERY_DATE", delOrd.DELIVERY_DATE);
                paramHDR.AddParamInput(6, "strREMARK", delOrd.REMARK);
                paramHDR.AddParamInput(7, "strREC_STAT", (delOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(8, "strUSER_ID", userid);
                paramHDR.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                #region "Transcation Detail"

                List<ProcParam> lstParam = new List<ProcParam>();
                ProcParam paramDTL = null;

                foreach (DeliveryOrderDtl delDtl in delOrd.DELIVERY_ORD_DTL)
                {
                    switch (delDtl.FLAG)
                    {
                        case 0:
                            paramDTL = new ProcParam(5);
                            paramDTL.ProcedureName = "DO_PACK.DO_DTL_DEL";

                            paramDTL.AddParamInput(0, "strDO_NO", delDtl.DO_NO);
                            paramDTL.AddParamInput(1, "strLINE_NO", delDtl.LINE_NO);
                            paramDTL.AddParamInput(2, "strPROD_SEQ_NO", delDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(3, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);
                            break;
                        case 1:
                            break;
                        case 2:

                            paramDTL = new ProcParam(8);
                            paramDTL.ProcedureName = "DO_PACK.DO_DTL_INS";

                            paramDTL.AddParamInput(0, "strDO_NO", delOrd.DO_NO);
                            paramDTL.AddParamInput(1, "strPROD_SEQ_NO", delDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(2, "strQTY", delDtl.QTY);
                            paramDTL.AddParamInput(3, "strUNIT_ID", delDtl.UNIT_ID);
                            paramDTL.AddParamInput(4, "strREMARK", delDtl.REMARK);
                            paramDTL.AddParamInput(5, "strREC_STAT ", "Y"); //default is 'Y'
                            paramDTL.AddParamInput(6, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);

                            break;
                        case 3:

                            paramDTL = new ProcParam(9);
                            paramDTL.ProcedureName = "DO_PACK.DO_DTL_UPD";

                            paramDTL.AddParamInput(0, "strDO_NO", delOrd.DO_NO);
                            paramDTL.AddParamInput(1, "strLINE_NO", delDtl.LINE_NO);
                            paramDTL.AddParamInput(2, "strPROD_SEQ_NO", delDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(3, "strQTY", delDtl.QTY);
                            paramDTL.AddParamInput(4, "strUNIT_ID", delDtl.UNIT_ID);
                            paramDTL.AddParamInput(5, "strREMARK", delDtl.REMARK);
                            paramDTL.AddParamInput(6, "strREC_STAT ", "Y"); //default is 'Y'
                            paramDTL.AddParamInput(7, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(8, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);
                            break;
                    }
                }
                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, lstParam);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)paramHDR.ReturnValue(9);

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

        public string DeleteDeliveryOrder(string doNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam paramHDR = new ProcParam(3) { ProcedureName = "DO_PACK.DO_HDR_DEL" };

                paramHDR.AddParamInput(0, "strDO_NO ", doNo);
                paramHDR.AddParamInput(1, "strUSER_ID", userid);
                paramHDR.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)paramHDR.ReturnValue(2);

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

        public DataSet PrintDeliveryOrder(string doNo, string userid)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_DELIVERY_ORDER");
            try
            {
                DataTable dtHeader = this.GetPrintLoadingOrder(doNo, userid, "T_DO_HDR");

                dtsResult.Tables.Add(dtHeader);

                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("DO_PACK.REP_DO_R2", doNo, "T_DO_DTL");

                dtsResult.Tables.Add(dtDetail);

                dtsResult.Relations.Add("T_DO_HDR_T_DO_DTL",
                    dtsResult.Tables["T_DO_HDR"].Columns["DO_NO"],
                    dtsResult.Tables["T_DO_DTL"].Columns["DO_NO"]
                    );

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        private DataTable GetPrintLoadingOrder(string doNo, string userid, string tableName)
        {
            List<DeliveryOrder> lstLoadingOrd = null;
            DeliveryOrder loadingOrd;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "DO_PACK.REP_DO_R1" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strDO_NO", doNo);
                param.AddParamInput(2, "strUSER_ID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstLoadingOrd = new List<DeliveryOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        loadingOrd = new DeliveryOrder();

                        loadingOrd.DO_NO = OraDataReader.Instance.GetString("DO_NO");
                        loadingOrd.DO_DATE = OraDataReader.Instance.GetDateTime("DO_DATE");
                        
                        loadingOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");

                        loadingOrd.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        loadingOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        loadingOrd.TO_DEST = OraDataReader.Instance.GetString("TO_DEST");
                        loadingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");

                        //loadingOrd.BARCODE = UtilityBLL.QRCode_Encode(loadingOrd.DO_NO);

                        lstLoadingOrd.Add(loadingOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstLoadingOrd = null;
                throw ex;
            }

            return UtilityBLL.ListToDataTable(lstLoadingOrd, tableName);
        }
    }
}
