using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;

namespace HTN.BITS.BLL.PLASESS
{
    public class ShippingOrderBLL : IDisposable
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

        ~ShippingOrderBLL()
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

        public ShippingOrderBLL()
        {
        }

        public List<ShippingOrder> GetShippingOrderList(string findValue,string whid, DateTime? formDate, DateTime? toDate)
        {
            List<ShippingOrder> lstShippingOrd = null;
            ShippingOrder shippingOrd;

            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "SO_PACK.GET_SO_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strSO_NO", DBNull.Value);
                param.AddParamInput(3, "strREF_NO", DBNull.Value);
                if (formDate.HasValue)
                {
                    param.AddParamInput(4, "strREF_DT_F", formDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strREF_DT_F", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(5, "strREF_DT_T", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strREF_DT_T", DBNull.Value);
                }
                param.AddParamInput(6, "strWH_ID", whid);


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstShippingOrd = new List<ShippingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 10000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        shippingOrd = new ShippingOrder();

                        shippingOrd.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        shippingOrd.SO_DATE = OraDataReader.Instance.GetDateTime("SO_DATE");
                        shippingOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        shippingOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        shippingOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        if (!OraDataReader.Instance.IsDBNull("REF_DATE"))
                        {
                            shippingOrd.REF_DATE = OraDataReader.Instance.GetDateTime("REF_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("ETA"))
                        {
                            shippingOrd.ETA = OraDataReader.Instance.GetDateTime("ETA");
                        }
                        shippingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        shippingOrd.STATUS = OraDataReader.Instance.GetString("STATUS");
                        shippingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        shippingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        //add new get value on 07-Jun-2011
                        shippingOrd.QTY_PCS = OraDataReader.Instance.GetInteger("QTY_PCS");
                        shippingOrd.QTY_BOX = OraDataReader.Instance.GetInteger("QTY_BOX");
                        shippingOrd.POST_REF = OraDataReader.Instance.GetString("POST_REF");
                        shippingOrd.SALES_ORDER_NO = OraDataReader.Instance.GetString("SALES_ORDER_NO");

                        lstShippingOrd.Add(shippingOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstShippingOrd = null;
                throw ex;
            }

            return lstShippingOrd;
        }

        public List<ShippingOrder> AdvShippingOrder(string soNo, string refNo, string whid, DateTime? formDate, DateTime? toDate)
        {
            List<ShippingOrder> lstShippingOrd = null;
            ShippingOrder shippingOrd;

            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "SO_PACK.GET_SO_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSO_NO", soNo);
                param.AddParamInput(3, "strREF_NO", refNo);
                if (formDate.HasValue)
                {
                    param.AddParamInput(4, "strREF_DT_F", formDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strREF_DT_F", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(5, "strREF_DT_T", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strREF_DT_T", DBNull.Value);
                }
                param.AddParamInput(6, "strWH_ID", whid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstShippingOrd = new List<ShippingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        shippingOrd = new ShippingOrder();

                        shippingOrd.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        shippingOrd.SO_DATE = OraDataReader.Instance.GetDateTime("SO_DATE");
                        shippingOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        shippingOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        shippingOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        if (!OraDataReader.Instance.IsDBNull("REF_DATE"))
                        {
                            shippingOrd.REF_DATE = OraDataReader.Instance.GetDateTime("REF_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("ETA"))
                        {
                            shippingOrd.ETA = OraDataReader.Instance.GetDateTime("ETA");
                        }
                        shippingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        shippingOrd.STATUS = OraDataReader.Instance.GetString("STATUS");
                        shippingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        shippingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        //add new get value on 07-Jun-2011
                        shippingOrd.QTY_PCS = OraDataReader.Instance.GetInteger("QTY_PCS");
                        shippingOrd.QTY_BOX = OraDataReader.Instance.GetInteger("QTY_BOX");
                        shippingOrd.POST_REF = OraDataReader.Instance.GetString("POST_REF");
                        shippingOrd.SALES_ORDER_NO = OraDataReader.Instance.GetString("SALES_ORDER_NO");

                        lstShippingOrd.Add(shippingOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstShippingOrd = null;
                throw ex;
            }

            return lstShippingOrd;
        }

        public ShippingOrder GetShippingOrder(string soNo)
        {
            ShippingOrder shippingOrd = null;

            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "SO_PACK.GET_SO_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSO_NO", soNo);
                param.AddParamInput(3, "strREF_NO", DBNull.Value);
                param.AddParamInput(4, "strREF_DT_F", DBNull.Value);
                param.AddParamInput(5, "strREF_DT_T", DBNull.Value);
                param.AddParamInput(6, "strWH_ID", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        shippingOrd = new ShippingOrder();

                        shippingOrd.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        shippingOrd.SO_DATE = OraDataReader.Instance.GetDateTime("SO_DATE");
                        shippingOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        shippingOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        shippingOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        if (!OraDataReader.Instance.IsDBNull("REF_DATE"))
                        {
                            shippingOrd.REF_DATE = OraDataReader.Instance.GetDateTime("REF_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("ETA"))
                        {
                            shippingOrd.ETA = OraDataReader.Instance.GetDateTime("ETA");
                        }
                        shippingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        shippingOrd.STATUS = OraDataReader.Instance.GetString("STATUS");
                        shippingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        shippingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        //add new get value on 07-Jun-2011
                        shippingOrd.QTY_PCS = OraDataReader.Instance.GetInteger("QTY_PCS");
                        shippingOrd.QTY_BOX = OraDataReader.Instance.GetInteger("QTY_BOX");
                        shippingOrd.POST_REF = OraDataReader.Instance.GetString("POST_REF");
                        shippingOrd.SALES_ORDER_NO = OraDataReader.Instance.GetString("SALES_ORDER_NO");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                shippingOrd = null;
                throw ex;
            }

            return shippingOrd;
        }

        public DataTable GetShippingOrderDetail(string soNo, string whID)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "SO_PACK.GET_SO_DTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSO_NO", soNo);
                param.AddParamInput(2, "strWH", whID);

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

        public string InsertShippingOrder(ref ShippingOrder shipOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(12) { ProcedureName = "SO_PACK.SO_HDR_INS" };

                paramHDR.AddParamInOutput(0, "strSO_NO", OracleDbType.Varchar2, 255, shipOrd.SO_NO);
                paramHDR.AddParamInput(1, "strSO_DATE ", shipOrd.SO_DATE);
                paramHDR.AddParamInput(2, "strPARTY_ID", shipOrd.PARTY_ID);
                paramHDR.AddParamInput(3, "strREF_NO", shipOrd.REF_NO);
                if (shipOrd.REF_DATE.HasValue)
                {
                    paramHDR.AddParamInput(4, "strREF_DATE", shipOrd.REF_DATE.Value);
                }
                else
                {
                    paramHDR.AddParamInput(4, "strREF_DATE", DBNull.Value);
                }
                if (shipOrd.ETA.HasValue)
                {
                    paramHDR.AddParamInput(5, "strETA", shipOrd.ETA.Value);
                }
                else
                {
                    paramHDR.AddParamInput(5, "strETA", DBNull.Value);
                }
                paramHDR.AddParamInput(6, "strWH_ID", shipOrd.WH_ID);
                paramHDR.AddParamInput(7, "strREMARK", shipOrd.REMARK);
                paramHDR.AddParamInput(8, "strREC_STAT", (shipOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(9, "strSALES_ORDER_NO", shipOrd.SALES_ORDER_NO);
                paramHDR.AddParamInput(10, "strUSER_ID", userid);

                paramHDR.AddParamOutput(11, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                #region "Transcation Detail"

                List<ProcParam> lstParam = new List<ProcParam>();
                ProcParam paramDTL = null;

                foreach (ShippingOrderDtl soDtl in shipOrd.SHIPPING_ORD_DTL)
                {
                    paramDTL = new ProcParam(11);
                    paramDTL.ProcedureName = "SO_PACK.SO_DTL_INS";

                    paramDTL.AddParamInput(0, "strSO_NO", DBNull.Value);
                    paramDTL.AddParamInput(1, "strPROD_SEQ_NO", soDtl.PROD_SEQ_NO);
                    paramDTL.AddParamInput(2, "strUNIT_ID", soDtl.UNIT_ID);
                    paramDTL.AddParamInput(3, "strPACKAGING", soDtl.PACKAGING);
                    paramDTL.AddParamInput(4, "strQTY", soDtl.QTY);
                    paramDTL.AddParamInput(5, "strUnit_Price", soDtl.UNIT_PRICE);
                    paramDTL.AddParamInput(6, "strRemark", soDtl.REMARK);
                    paramDTL.AddParamInput(7, "strPO_NO", soDtl.PO_NO);
                    paramDTL.AddParamInput(8, "strREC_STAT ", (soDtl.REC_STAT ? "Y" : "N"));
                    paramDTL.AddParamInput(9, "strUSER_ID", userid);
                    paramDTL.AddParamOutput(10, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    lstParam.Add(paramDTL);
                }
                #endregion


                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, lstParam, 0, 0);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)paramHDR.ReturnValue(0);
                OracleString result = (OracleString)paramHDR.ReturnValue(11);

                if (!result.IsNull)
                {
                    shipOrd.SO_NO = resultDB.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateShippingOrder(ShippingOrder shipOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(12) { ProcedureName = "SO_PACK.SO_HDR_UPD" };

                paramHDR.AddParamInput(0, "strSO_NO", shipOrd.SO_NO);
                paramHDR.AddParamInput(1, "strSO_DATE ", shipOrd.SO_DATE);
                paramHDR.AddParamInput(2, "strPARTY_ID", shipOrd.PARTY_ID);
                paramHDR.AddParamInput(3, "strREF_NO", shipOrd.REF_NO);
                if (shipOrd.REF_DATE.HasValue)
                {
                    paramHDR.AddParamInput(4, "strREF_DATE", shipOrd.REF_DATE.Value);
                }
                else
                {
                    paramHDR.AddParamInput(4, "strREF_DATE", DBNull.Value);
                }
                if (shipOrd.ETA.HasValue)
                {
                    paramHDR.AddParamInput(5, "strETA", shipOrd.ETA.Value);
                }
                else
                {
                    paramHDR.AddParamInput(5, "strETA", DBNull.Value);
                }

                paramHDR.AddParamInput(6, "strWH_ID", shipOrd.WH_ID);
                paramHDR.AddParamInput(7, "strREMARK", shipOrd.REMARK);
                paramHDR.AddParamInput(8, "strREC_STAT", (shipOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(9, "strSALES_ORDER_NO", shipOrd.SALES_ORDER_NO);
                paramHDR.AddParamInput(10, "strUSER_ID", userid);

                paramHDR.AddParamOutput(11, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                #region "Transcation Detail"

                List<ProcParam> lstParam = new List<ProcParam>();
                ProcParam paramDTL = null;

                foreach (ShippingOrderDtl soDtl in shipOrd.SHIPPING_ORD_DTL)
                {
                    switch(soDtl.FLAG)
                    {
                        case 0:
                            paramDTL = new ProcParam(5);
                            paramDTL.ProcedureName = "SO_PACK.SO_DTL_DEL";

                            paramDTL.AddParamInput(0, "strSO_NO", shipOrd.SO_NO);
                            paramDTL.AddParamInput(1, "strPROD_SEQ_NO", soDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(2, "strLINE_NO", soDtl.LINE_NO);
                            paramDTL.AddParamInput(3, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);
                            break;
                        case 1:
                            break;
                        case 2:

                            paramDTL = new ProcParam(11);
                            paramDTL.ProcedureName = "SO_PACK.SO_DTL_INS";

                            paramDTL.AddParamInput(0, "strSO_NO", shipOrd.SO_NO);
                            paramDTL.AddParamInput(1, "strPROD_SEQ_NO", soDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(2, "strUNIT_ID", soDtl.UNIT_ID);
                            paramDTL.AddParamInput(3, "strPACKAGING", soDtl.PACKAGING);
                            paramDTL.AddParamInput(4, "strQTY", soDtl.QTY);
                            paramDTL.AddParamInput(5, "strUnit_Price", soDtl.UNIT_PRICE);
                            paramDTL.AddParamInput(6, "strRemark", soDtl.REMARK);
                            paramDTL.AddParamInput(7, "strPO_NO", soDtl.PO_NO);
                            paramDTL.AddParamInput(8, "strREC_STAT ", (soDtl.REC_STAT ? "Y" : "N"));
                            paramDTL.AddParamInput(9, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(10, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);

                            break;
                        case 3:

                            paramDTL = new ProcParam(12);
                            paramDTL.ProcedureName = "SO_PACK.SO_DTL_UPD";

                            paramDTL.AddParamInput(0, "strSO_NO", shipOrd.SO_NO);
                            paramDTL.AddParamInput(1, "strPROD_SEQ_NO", soDtl.PROD_SEQ_NO);
                            paramDTL.AddParamInput(2, "strLINE_NO", soDtl.LINE_NO);
                            paramDTL.AddParamInput(3, "strUNIT_ID", soDtl.UNIT_ID);
                            paramDTL.AddParamInput(4, "strPACKAGING", soDtl.PACKAGING);
                            paramDTL.AddParamInput(5, "strQTY", soDtl.QTY);
                            paramDTL.AddParamInput(6, "strUnit_Price", soDtl.UNIT_PRICE);
                            paramDTL.AddParamInput(7, "strRemark", soDtl.REMARK);
                            paramDTL.AddParamInput(8, "strPO_NO", soDtl.PO_NO);
                            paramDTL.AddParamInput(9, "strREC_STAT ", (soDtl.REC_STAT ? "Y" : "N"));
                            paramDTL.AddParamInput(10, "strUSER_ID", userid);
                            paramDTL.AddParamOutput(11, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(paramDTL);
                            break;
                    }
                }
                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR, lstParam);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)paramHDR.ReturnValue(11);

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

        public List<PickingDtl> GetPickingList(string soNo, string whID)
        {
            List<PickingDtl> lstPickingDtl = null;
            PickingDtl picking;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "SO_PACK.GET_PICK_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSO_NO", soNo);
                param.AddParamInput(2, "strWH_ID", whID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPickingDtl = new List<PickingDtl>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        picking = new PickingDtl();

                        picking.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        picking.PICK_NO = OraDataReader.Instance.GetString("PICK_NO");
                        picking.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        picking.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        picking.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        picking.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        picking.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        picking.INI_QTY = OraDataReader.Instance.GetInteger("QTY");
                        picking.QTY = OraDataReader.Instance.GetInteger("QTY");
                        picking.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        picking.PICKED_QTY = OraDataReader.Instance.GetInteger("PICKED_QTY");
                        picking.LOADED_QTY = OraDataReader.Instance.GetInteger("LOADED_QTY");
                        picking.FLAG = "0"; //initial value

                        lstPickingDtl.Add(picking);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPickingDtl = null;
                throw ex;
            }

            return lstPickingDtl;
        }

        public List<Packaging> GetPackaging()
        {
            List<Packaging> lstPackaging = null;
            Packaging packaging;

            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "LOV_PACK.GET_PACKAGE_LIST" };
                param.AddParamRefCursor(0, "io_cursor");

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPackaging = new List<Packaging>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        packaging = new Packaging();

                        packaging.PACKAGE_ID = OraDataReader.Instance.GetString("PACKAGE_ID");
                        packaging.PACKAGE_NAME = OraDataReader.Instance.GetString("PACKAGE_NAME");

                        lstPackaging.Add(packaging);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPackaging = null;
                throw ex;
            }

            return lstPackaging;
        }

        public List<Warehouse> GetWarehouse()
        {
            List<Warehouse> lstWH = null;
            Warehouse wareHouse;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_WH_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstWH = new List<Warehouse>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        wareHouse = new Warehouse();

                        wareHouse.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        wareHouse.NAME = OraDataReader.Instance.GetString("NAME");
                        wareHouse.REMARK = OraDataReader.Instance.GetString("REMARK");
                        wareHouse.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

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

        public DataSet PrintPickingListReport(string soNo, List<PickingDtl> lstPicking, string userid)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_PICKING_LIST_UPD");
            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstPicking.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrPICK_NO = (from job in lstPicking
                                 select job.PICK_NO).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrPICK_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstPicking.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstPicking.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstPicking.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (PickingDtl picking in lstPicking)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", picking.PICK_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                //get print value header
                DataTable dtHeader = this.GetPrintPickingHDR(seqPrint, "T_PICKING_HDR");

                dtsResult.Tables.Add(dtHeader);

                //get print value detail
                //DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("JOB_ORDER_PACK.RPT_PRODUCT_CARD_LABEL",
                //                                                               seqPrint,
                //                                                               "T_PRODUCT_CARD");

                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("SO_PACK.RPT_PICKING_DTL", seqPrint, "T_PICKING_DTL");

                dtsResult.Tables.Add(dtDetail);

                ////maping datatable to dataset
                //T_PICKING_HDR.T_PICKING_HDR_T_PICKING_DTL
                dtsResult.Relations.Add("T_PICKING_HDR_T_PICKING_DTL",
                    dtsResult.Tables["T_PICKING_HDR"].Columns["PICK_NO"],
                    dtsResult.Tables["T_PICKING_DTL"].Columns["PICK_NO"]
                    );
                

                PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                dtsResult.AcceptChanges();
                //string test = dtsResult.Tables["T_PICKING_DTL"].Rows[2]["PRODUCT_NO"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        private DataTable GetPrintPickingHDR(int seqNo, string tableName)
        {
            List<PickingHdr> lstPickHdr = null;
            PickingHdr pickHdr;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "SO_PACK.RPT_PICKING_HDR" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPickHdr = new List<PickingHdr>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pickHdr = new PickingHdr();

                        pickHdr.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        pickHdr.CUSTOMER_NAME = OraDataReader.Instance.GetString("CUSTOMER_NAME");
                        if (!OraDataReader.Instance.IsDBNull("ETA"))
                        {
                            pickHdr.ETA = string.Format("{0:dd-MM-yyyy HH:mm}", OraDataReader.Instance.GetDateTime("ETA"));
                        }
                        pickHdr.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        pickHdr.PICK_NO = OraDataReader.Instance.GetString("PICK_NO");
                        //pickHdr.BARCODE = UtilityBLL.QRCode_Encode(pickHdr.PICK_NO);

                        lstPickHdr.Add(pickHdr);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPickHdr = null;
                throw ex;
            }

            return UtilityBLL.ListToDataTable(lstPickHdr, tableName);
        }

        public List<Product> LovProductList(string wareID, string partyid, string pono, string findValue)
        {
            List<Product> lstProduct = null;
            Product product;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "SO_PACK.LOV_SO_PRODUCT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPRODUCT_NO", DBNull.Value);
                param.AddParamInput(3, "strWH_ID", wareID);

                param.AddParamInput(4, "strPARTY_ID", partyid);
                param.AddParamInput(5, "strPO_NO", pono);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //test
                // Step 1 - get the data reader type
                //Type type = OraDataReader.Instance.OraReader.GetType();

                // Step 2 - get the "m_rowSize" field info
                //FieldInfo fi = type.GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);

                // Step 3 - get the value of m_rowSize for the dr object
                //long rowSize = Convert.ToInt64(fi.GetValue(OraDataReader.Instance.OraReader));

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstProduct = new List<Product>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 10000;//special config

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        product = new Product();

                        product.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        product.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        product.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        product.MATERIAL_NAME = OraDataReader.Instance.GetString("MATERIAL_NAME");
                        product.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        product.UNIT = OraDataReader.Instance.GetString("UNIT");
                        product.FREE_STOCK = OraDataReader.Instance.GetInteger("FREE_STOCK");

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

        public Product LovProductByNo(string wareID, string partyid, string pono, string prodNo)
        {
            Product product = null;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "SO_PACK.LOV_SO_PRODUCT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPRODUCT_NO", prodNo);
                param.AddParamInput(3, "strWH_ID", wareID);

                param.AddParamInput(4, "strPARTY_ID", partyid);
                param.AddParamInput(5, "strPO_NO", pono);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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
                        product.FREE_STOCK = OraDataReader.Instance.GetInteger("FREE_STOCK");
                        //use for unit price
                        product.COST_PRICE = OraDataReader.Instance.GetDecimal("UNIT_PRICE");
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

        public string DeletePickingHDR(string pickingNo, int lineNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "SO_PACK.PICK_HDR_DEL" };

                param.AddParamInput(0, "strPICK_NO", pickingNo);
                param.AddParamInput(1, "strLINE_NO ", lineNo);
                param.AddParamInput(2, "strUSER_ID", userid);
                param.AddParamOutput(3, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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

        public string UpdatePickingHDR(List<PickingDtl> lstPicking, string userid)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstPicking.Count);
            ProcParam param = null;

            try
            {
                foreach (PickingDtl picking in lstPicking)
                {
                    param = new ProcParam(6) { ProcedureName = "SO_PACK.PICK_HDR_UPD" };

                    param.AddParamInput(0, "strPICK_NO", picking.PICK_NO);
                    param.AddParamInput(1, "strLINE_NO", picking.LINE_NO);
                    param.AddParamInput(2, "strRemark", "Change Qty of Shipping Order");
                    param.AddParamInput(3, "strQTY", picking.QTY);
                    param.AddParamInput(4, "strUSER_ID", userid);
                    param.AddParamOutput(5, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    lstParam.Add(param);
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

        public DataTable ShowPickingDetail(string sono, string prodseq, string type)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "SO_PACK.GET_SO_DTL_PICK_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSO_NO", sono);
                param.AddParamInput(2, "strPROD_SEQ_NO", prodseq);
                param.AddParamInput(3, "strTYPE", type);

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

        public int GetBoxQty(string prodSeq, int qty)
        {
            int boxQty = 0;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.PRODUCT_BOX_QTY" };
                param.AddParamReturn(0, "ReturnValue", OracleDbType.Decimal, 100);
                param.AddParamInput(1, "strNo", prodSeq);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                OracleDecimal result = (OracleDecimal)param.ReturnValue(0);

                double dResult = qty / result.ToDouble();

                boxQty = (int)Math.Ceiling(dResult);

                return boxQty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPostData(List<ShippingOrder> lstShippingOrd, string pathSelect, string userid, out ICollection<string> files)
        {
            //declare dataset and name.
            files = new Collection<string>();

            int seqPost = 0;
            try
            {
                seqPost = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstShippingOrd.Count, seqPost), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrSO_NO = (from so in lstShippingOrd
                                select so.SO_NO).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrSO_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstShippingOrd.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstShippingOrd.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstShippingOrd.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (ShippingOrder shipOrd in lstShippingOrd)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPost);
                //    procInsTPrint.AddParamInput(1, "strTR1", shipOrd.SO_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                List<IF_ControlOrder> lstIF_ShipOrd = this.GetDataToPost(seqPost, userid);

                try
                {
                    if (lstIF_ShipOrd != null)
                    {
                        if (lstIF_ShipOrd.Count > 0)
                        {
                            string fileid = "NONE";
                            string fileName = string.Empty;
                            FileStream file_data = null;
                            StreamWriter sw_data = null;



                            foreach (IF_ControlOrder ifShipOrd in lstIF_ShipOrd)
                            {
                                if (!fileid.Equals(ifShipOrd.FILE_ID))
                                {
                                    if (file_data != null)
                                    {
                                        sw_data.Close();
                                        sw_data.Dispose();

                                        file_data.Close();
                                        file_data.Dispose();
                                    }
                                    //create new file
                                    fileName = string.Format("{0}\\{1}", pathSelect, ifShipOrd.FILE_ID);

                                    //check exist file
                                    if (File.Exists(fileName))
                                        File.Delete(fileName);

                                    files.Add(fileName);

                                    file_data = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                                    sw_data = new StreamWriter(file_data);

                                    sw_data.WriteLine(ifShipOrd.TEXT);

                                    fileid = ifShipOrd.FILE_ID;
                                }
                                else
                                {
                                    //write to same text file
                                    sw_data.WriteLine(ifShipOrd.TEXT);
                                }
                            }



                            if (file_data != null)
                            {
                                sw_data.Close();
                                sw_data.Dispose();

                                file_data.Close();
                                file_data.Dispose();
                            }
                        }
                        else
                        {
                            throw new Exception("The System not return Data");
                        }
                        
                    }
                    else
                    {
                        throw new Exception("The System not return Data");
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                PrintingBuilder.Instance.RemovePrintSEQ(seqPost);

                return "Post Data Complete";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<IF_ControlOrder> GetDataToPost(int seq, string userid)
        {
            //List<IF_ShippingOrder> lstIfShippingOrd = new List<IF_ShippingOrder>();
            List<IF_ControlOrder> lstIfShippingOrd = null;
            IF_ControlOrder ifShipOrd;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "INTERFACE_PACK.IF_SO_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ", seq);
                param.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstIfShippingOrd = new List<IF_ControlOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        ifShipOrd = new IF_ControlOrder();

                        ifShipOrd.SEQ = OraDataReader.Instance.GetString("SEQ");
                        ifShipOrd.SEQ_DATE = OraDataReader.Instance.GetDateTime("SEQ_DATE");
                        ifShipOrd.FILE_ID = OraDataReader.Instance.GetString("FILE_ID");
                        ifShipOrd.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        ifShipOrd.TEXT = OraDataReader.Instance.GetString("TEXT");

                        lstIfShippingOrd.Add(ifShipOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstIfShippingOrd = null;
                throw ex;
            }

            return lstIfShippingOrd;
        }

        //create on 29-08-2011
        //for Rollback Picking and Loading
        public string RollBackPicking(List<RollBackDocument> lstPicking, string userid)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstPicking.Count);
            ProcParam param = null;

            try
            {
                foreach (RollBackDocument picking in lstPicking)
                {
                    param = new ProcParam(4);
                    param.ProcedureName = "SO_PACK.ROLLBACK_PICKING";

                    param.AddParamInput(0, "strPICK_NO", picking.DOC_NO);
                    param.AddParamInput(1, "strSERIAL_NO", picking.SERIAL_NO);
                    param.AddParamInput(2, "strUSER_ID", userid);
                    param.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    lstParam.Add(param);

                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RollBackLoading(List<RollBackDocument> lstLoading, string userid)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstLoading.Count);
            ProcParam param = null;

            try
            {
                foreach (RollBackDocument picking in lstLoading)
                {
                    param = new ProcParam(4);
                    param.ProcedureName = "SO_PACK.ROLLBACK_LOADING";

                    param.AddParamInput(0, "strPICK_NO", picking.DOC_NO);
                    param.AddParamInput(1, "strSERIAL_NO", picking.SERIAL_NO);
                    param.AddParamInput(2, "strUSER_ID", userid);
                    param.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                    lstParam.Add(param);

                }

                GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DETAILS ON THE PALLET

        public DataTable GetPalletHdr(string sono)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "SO_PACK.GET_PALLET_HDR_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSO_NO", sono);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public DataTable GetPalletDtl(string palletno)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "SO_PACK.GET_PALLET_DTL_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPALLET_NO", palletno);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public string InsertPalletHdr(DataTable dtbPalletHdr, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                DataRow[] insertRows = dtbPalletHdr.Select("[FLAG] = 2");

                ProcParam paramDTL = new ProcParam(8) { ProcedureName = "SO_PACK.PALLET_HDR_INS" };

                

                //PALLET_NO
                var arrPALLET_NO = (from DataRow row in insertRows
                                    select row["PALLET_NO"]).ToArray();
                paramDTL.AddParamInput(0, "strPALLET_NO", arrPALLET_NO, OracleDbType.Varchar2);

                //SO_NO
                var arrSO_NO = (from DataRow row in insertRows
                                select row["SO_NO"]).ToArray();
                paramDTL.AddParamInput(1, "strSO_NO", arrSO_NO, OracleDbType.Varchar2);

                //PALLET_SEQ
                var arrPALLET_SEQ = (from DataRow row in insertRows
                                     select row["PALLET_SEQ"]).ToArray();
                paramDTL.AddParamInput(2, "strPALLET_SEQ", arrPALLET_SEQ, OracleDbType.Decimal);

                //PALLET_TOTAL
                var arrPALLET_TOTAL = (from DataRow row in insertRows
                                       select row["PALLET_TOTAL"]).ToArray();
                paramDTL.AddParamInput(3, "strPALLET_TOTAL", arrPALLET_TOTAL, OracleDbType.Decimal);

                //PALLET_STATUS
                var arrPALLET_STATUS = (from DataRow row in insertRows
                                        select row["PALLET_STATUS"]).ToArray();
                paramDTL.AddParamInput(4, "strPALLET_STATUS", arrPALLET_STATUS, OracleDbType.Varchar2);

                //WH_ID
                var arrWH_ID = (from DataRow row in insertRows
                                        select row["WH_ID"]).ToArray();
                paramDTL.AddParamInput(5, "strWH_ID", arrWH_ID, OracleDbType.Varchar2);

                //USER_ID
                paramDTL.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(insertRows.Length, userid), OracleDbType.Varchar2);

                //RESULTMSG
                paramDTL.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", insertRows.Length);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDTL, insertRows.Length);

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

        public string UpdatePalletHdr(DataTable dtbPalletHdr, string userid)
        {
            string resultMsg = string.Empty;

            try
            {

                #region Transaction Detail Delete

                DataRow[] deleteRows = dtbPalletHdr.Select("[FLAG] = 0");
                ProcParam paramDel = null;

                if (deleteRows.Length > 0)
                {
                    paramDel = new ProcParam(3) { ProcedureName = "SO_PACK.PALLET_HDR_DEL" };

                    //PALLET_NO
                    var arrPALLET_NO = (from DataRow row in deleteRows
                                        select row["PALLET_NO"]).ToArray();
                    paramDel.AddParamInput(0, "strPALLET_NO", arrPALLET_NO, OracleDbType.Varchar2);

                    //USER_ID
                    paramDel.AddParamInput(1, "strUSER_ID", ArrayOf<object>.Create(deleteRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramDel.AddParamOutput(2, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", deleteRows.Length);
                }

                #endregion

                #region Transaction Detail Insert

                DataRow[] insertRows = dtbPalletHdr.Select("[FLAG] = 2");
                ProcParam paramIns = null;

                if (insertRows.Length > 0)
                {
                    paramIns = new ProcParam(8) { ProcedureName = "SO_PACK.PALLET_HDR_INS" };

                    //PALLET_NO
                    var arrPALLET_NO = (from DataRow row in insertRows
                                        select row["PALLET_NO"]).ToArray();
                    paramIns.AddParamInput(0, "strPALLET_NO", arrPALLET_NO, OracleDbType.Varchar2);

                    //SO_NO
                    var arrSO_NO = (from DataRow row in insertRows
                                    select row["SO_NO"]).ToArray();
                    paramIns.AddParamInput(1, "strSO_NO", arrSO_NO, OracleDbType.Varchar2);

                    //PALLET_SEQ
                    var arrPALLET_SEQ = (from DataRow row in insertRows
                                         select row["PALLET_SEQ"]).ToArray();
                    paramIns.AddParamInput(2, "strPALLET_SEQ", arrPALLET_SEQ, OracleDbType.Decimal);

                    //PALLET_TOTAL
                    var arrPALLET_TOTAL = (from DataRow row in insertRows
                                           select row["PALLET_TOTAL"]).ToArray();
                    paramIns.AddParamInput(3, "strPALLET_TOTAL", arrPALLET_TOTAL, OracleDbType.Decimal);

                    //PALLET_STATUS
                    var arrPALLET_STATUS = (from DataRow row in insertRows
                                            select row["PALLET_STATUS"]).ToArray();
                    paramIns.AddParamInput(4, "strPALLET_STATUS", arrPALLET_STATUS, OracleDbType.Varchar2);

                    //WH_ID
                    var arrWH_ID = (from DataRow row in insertRows
                                    select row["WH_ID"]).ToArray();
                    paramIns.AddParamInput(5, "strWH_ID", arrWH_ID, OracleDbType.Varchar2);

                    //USER_ID
                    paramIns.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(insertRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", insertRows.Length);
                }

                #endregion

                #region Transaction Detail Update

                DataRow[] updateRows = dtbPalletHdr.Select("[FLAG] = 3");
                ProcParam paramUpd = null;

                if (updateRows.Length > 0)
                {
                    paramUpd = new ProcParam(5) { ProcedureName = "SO_PACK.PALLET_HDR_UPD" };



                    //PALLET_NO
                    var arrPALLET_NO = (from DataRow row in updateRows
                                        select row["PALLET_NO"]).ToArray();
                    paramUpd.AddParamInput(0, "strPALLET_NO", arrPALLET_NO, OracleDbType.Varchar2);

                    //PALLET_SEQ
                    var arrPALLET_SEQ = (from DataRow row in updateRows
                                         select row["PALLET_SEQ"]).ToArray();
                    paramUpd.AddParamInput(1, "strPALLET_SEQ", arrPALLET_SEQ, OracleDbType.Decimal);

                    //PALLET_TOTAL
                    var arrPALLET_TOTAL = (from DataRow row in updateRows
                                           select row["PALLET_TOTAL"]).ToArray();
                    paramUpd.AddParamInput(2, "strPALLET_TOTAL", arrPALLET_TOTAL, OracleDbType.Decimal);

                    //USER_ID
                    paramUpd.AddParamInput(3, "strUSER_ID", ArrayOf<object>.Create(updateRows.Length, userid), OracleDbType.Varchar2);

                    //RESULTMSG
                    paramUpd.AddParamOutput(4, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", updateRows.Length);
                }

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel, deleteRows.Length, paramIns, insertRows.Length, paramUpd, updateRows.Length);

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

        public DataSet PrintDetailsOnPalletReport(List<string> lstPallet, string whid, string userid)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_PALLET_DETAIL");
            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstPallet.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrPALLET_NO = (from pallet in lstPallet
                                    select pallet).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrPALLET_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstPallet.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstPallet.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstPallet.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;

                //foreach (string palletno in lstPallet)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", palletno);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                if (userid.ToLower().Equals("system")) // is auto print
                {
                    this.UpdatePalletPrint(lstPallet, whid);
                }

                //get print value header
                DataTable dtHeader = this.GetPrintPalletDetail(seqPrint, "T_PALLET_HDR"); 

                dtsResult.Tables.Add(dtHeader);

                //get print value detail
                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("SO_PACK.RPT_PALLET_DTL",
                                                                               seqPrint,
                                                                               "T_PALLET_DTL");

                dtsResult.Tables.Add(dtDetail);

                ////maping datatable to dataset
                //T_PICKING_HDR.T_PICKING_HDR_T_PICKING_DTL
                dtsResult.Relations.Add("T_PALLET_HDR_T_PALLET_DTL",
                    dtsResult.Tables["T_PALLET_HDR"].Columns["PALLET_NO"],
                    dtsResult.Tables["T_PALLET_DTL"].Columns["PALLET_NO"]
                    );


                PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        private DataTable GetPrintPalletDetail(int seqNo, string tableName)
        {
            List<Pallet> lstPallet = null;
            Pallet pallet;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "SO_PACK.RPT_PALLET_HDR" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPallet = new List<Pallet>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pallet = new Pallet();

                        pallet.PALLET_NO = OraDataReader.Instance.GetString("PALLET_NO");
                        pallet.PALLET_SEQ = OraDataReader.Instance.GetString("PALLET_SEQ");
                        pallet.ETA = OraDataReader.Instance.GetDateTime("ETA");
                        pallet.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        //pallet.BARCODE = UtilityBLL.QRCode_Encode(pallet.PALLET_NO);

                        lstPallet.Add(pallet);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPallet = null;
                throw ex;
            }

            //return this.ListToDataTable(lstPrdCard, tableName);
            //DataTable dt  = UtilityBLL.ListToDataTable(lstPrdCard, tableName);
            return UtilityBLL.ListToDataTable(lstPallet, tableName);
        }

        public List<string> GetAutoPrintPallet(string whid)
        {
            List<string> lstPallet = null;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "SO_PACK.GET_AUTOPRINT_PALLET" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPallet = new List<string>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        lstPallet.Add(OraDataReader.Instance.GetString("PALLET_NO"));
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPallet = null;
                throw ex;
            }

            return lstPallet;
        }

        public string UpdatePalletPrint(List<string> lstPallet, string whid)
        {
            string resultMsg = string.Empty;

            try
            {

                ProcParam paramDTL = new ProcParam(3) { ProcedureName = "SO_PACK.PALLET_PRINT_UPD" };

                //PALLET_NO
                var arrPALLET_NO = (from palletno in lstPallet
                                    select palletno).ToArray();
                paramDTL.AddParamInput(0, "strPALLET_NO", arrPALLET_NO, OracleDbType.Varchar2);

                //WH_ID
                paramDTL.AddParamInput(1, "strWH_ID", ArrayOf<object>.Create(lstPallet.Count, whid), OracleDbType.Varchar2);

                //RESULTMSG
                paramDTL.AddParamOutput(2, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK", lstPallet.Count);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDTL, lstPallet.Count);

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

        public string RollBackRepack(string palletno, string serialno, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "SO_PACK.ROLLBACK_REPACK" };

                param.AddParamInput(0, "strPALLET_NO", palletno);
                param.AddParamInput(1, "strSERIAL_NO", serialno);
                param.AddParamInput(2, "strUSER_ID", userid);
                param.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                OracleString result = (OracleString)param.ReturnValue(3);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                }



                return resultMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //UPLOAD SO FROM EXCEL
        public void ClearTempUploadSo()
        {
            try
            {
                GlobalDB.Instance.DataAc.ExecuteNonQuery(@"TRUNCATE TABLE TEMP_UPLOAD_SO PRESERVE MATERIALIZED VIEW LOG");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadSOToTemp(DataTable dtbUploadSo)
        {
            string resultMsg = string.Empty;

            try
            {
                if (dtbUploadSo != null && dtbUploadSo.Rows.Count > 0)
                {
                    ProcParam paramIns = new ProcParam(14) { ProcedureName = "SO_PACK.UPLOAD_SO_INS" };

                    //WH_ID
                    var arrWH_ID = (from DataRow row in dtbUploadSo.Rows
                                    select row["WH_ID"]).ToArray();
                    paramIns.AddParamInput(0, "strWH_ID", arrWH_ID, OracleDbType.Varchar2);

                    //PARTY_ID
                    var arrPARTY_ID = (from DataRow row in dtbUploadSo.Rows
                                       select row["PARTY_ID"]).ToArray();
                    paramIns.AddParamInput(1, "strPARTY_ID", arrPARTY_ID, OracleDbType.Varchar2);

                    //REF_NO
                    var arrREF_NO = (from DataRow row in dtbUploadSo.Rows
                                     select row["REF_NO"]).ToArray();
                    paramIns.AddParamInput(2, "strREF_NO", arrREF_NO, OracleDbType.Varchar2);

                    //REF_DATE
                    var arrREF_DATE = (from DataRow row in dtbUploadSo.Rows
                                       select row["REF_DATE"]).ToArray();
                    paramIns.AddParamInput(3, "strREF_DATE", arrREF_DATE, OracleDbType.Date);

                    //ETA
                    var arrETA = (from DataRow row in dtbUploadSo.Rows
                                  select row["ETA"]).ToArray();
                    paramIns.AddParamInput(4, "strETA", arrETA, OracleDbType.Date);

                    //HDR_REMARK
                    var arrHDR_REMARK = (from DataRow row in dtbUploadSo.Rows
                                         select row["HDR_REMARK"]).ToArray();
                    paramIns.AddParamInput(5, "strHDR_REMARK", arrHDR_REMARK, OracleDbType.Varchar2);

                    //PRODUCT_NO
                    var arrPRODUCT_NO = (from DataRow row in dtbUploadSo.Rows
                                         select row["PRODUCT_NO"]).ToArray();
                    paramIns.AddParamInput(6, "strPRODUCT_NO", arrPRODUCT_NO, OracleDbType.Varchar2);

                    //LINE_NO
                    var arrLINE_NO = (from DataRow row in dtbUploadSo.Rows
                                      select row["LINE_NO"]).ToArray();
                    paramIns.AddParamInput(7, "strLINE_NO", arrLINE_NO, OracleDbType.Int32);

                    //PACKAGING 
                    var arrPACKAGING = (from DataRow row in dtbUploadSo.Rows
                                        select row["PACKAGING"]).ToArray();
                    paramIns.AddParamInput(8, "strPACKAGING", arrPACKAGING, OracleDbType.Varchar2);

                    //QTY 
                    var arrQTY = (from DataRow row in dtbUploadSo.Rows
                                  select row["QTY"]).ToArray();
                    paramIns.AddParamInput(9, "strQTY", arrQTY, OracleDbType.Int32);

                    //UNIT_ID
                    var arrUNIT_ID = (from DataRow row in dtbUploadSo.Rows
                                      select row["UNIT_ID"]).ToArray();
                    paramIns.AddParamInput(10, "strUNIT_ID", arrUNIT_ID, OracleDbType.Varchar2);

                    //DTL_REMARK
                    var arrDTL_REMARK = (from DataRow row in dtbUploadSo.Rows
                                         select row["DTL_REMARK"]).ToArray();
                    paramIns.AddParamInput(11, "strDTL_REMARK", arrDTL_REMARK, OracleDbType.Varchar2);

                    //PO_NO
                    var arrPO_NO = (from DataRow row in dtbUploadSo.Rows
                                         select row["PO_NO"]).ToArray();
                    paramIns.AddParamInput(12, "strPO_NO", arrPO_NO, OracleDbType.Varchar2);

                    //RESULTMSG
                    paramIns.AddParamOutput(13, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dtbUploadSo.Rows.Count);


                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramIns, dtbUploadSo.Rows.Count);

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

        public List<ShippingOrder> GetUploadSO_HDR()
        {
            List<ShippingOrder> lstShippingOrd = null;
            ShippingOrder shippingOrd;

            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "SO_PACK.GET_UPLOAD_SO_HDR" };
                param.AddParamRefCursor(0, "io_cursor");

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstShippingOrd = new List<ShippingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        shippingOrd = new ShippingOrder();

                        shippingOrd.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        shippingOrd.SO_DATE = OraDataReader.Instance.GetDateTime("SO_DATE");
                        shippingOrd.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        shippingOrd.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        shippingOrd.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        if (!OraDataReader.Instance.IsDBNull("REF_DATE"))
                        {
                            shippingOrd.REF_DATE = OraDataReader.Instance.GetDateTime("REF_DATE");
                        }
                        if (!OraDataReader.Instance.IsDBNull("ETA"))
                        {
                            shippingOrd.ETA = OraDataReader.Instance.GetDateTime("ETA");
                        }
                        shippingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        shippingOrd.STATUS = OraDataReader.Instance.GetString("STATUS");
                        shippingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        shippingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstShippingOrd.Add(shippingOrd);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstShippingOrd = null;
                throw ex;
            }

            return lstShippingOrd;
        }

        public DataTable GetUploadSO_DTL()
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "SO_PACK.GET_UPLOAD_SO_DTL" };
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

        public DataTable GetCompletedSO(string sono, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "SO_PACK.GET_SO_CSV2" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strSO_NO", sono);
                param.AddParamInput(2, "strUSER_ID", userid);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public string UpdatePostSO(string sono, string postRef, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam param = new ProcParam(4) { ProcedureName = "SO_PACK.SO_POSTREF_UPD" };

                param.AddParamInput(0, "strSO_NO", sono);
                param.AddParamInput(1, "strPOST_REF ", postRef);
                param.AddParamInput(2, "strUSER_ID", userid);

                param.AddParamOutput(3, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion


                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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

        public int GetPickedQty(string pickno, int lineno)
        {
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "INFO.PICKING_PICKED_QTY_2" };

                param.AddParamReturn(0, "ReturnValue", OracleDbType.Decimal, 100);
                param.AddParamInput(1, "strPICK_NO", pickno);
                param.AddParamInput(2, "strLINE", lineno);

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

        public string UnlockAlreadyPostSO(string sono, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam param = new ProcParam(3) { ProcedureName = "SO_PACK.UNLOCK_ALREADYPOST_SO" };

                param.AddParamInput(0, "strSO_NO", sono);
                param.AddParamInput(1, "strUSER_ID", userid);

                param.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion


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


        public string UploadSalesOrder_SAGE50(DataTable dt, string userid)
        {
            try
            {
                ProcParam param = new ProcParam(21) { ProcedureName = "SO_PACK.UPLOAD_SALES_ORDER" };

                var arrCUSTOMER_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Customer ID")).ToArray();
                param.AddParamInput(0, "strCUSTOMER_ID", arrCUSTOMER_ID, OracleDbType.Varchar2);

                var arrSALES_ORDER_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Order/Proposal #")).ToArray();
                param.AddParamInput(1, "strSALES_ORDER_NO", arrSALES_ORDER_NO, OracleDbType.Varchar2);

                var arrSALES_ORDER_DATE = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Date")).ToArray();
                param.AddParamInput(2, "strSALES_ORDER_DATE", arrSALES_ORDER_DATE, OracleDbType.Varchar2);

                var arrSHIP_BY = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Ship By")).ToArray();
                param.AddParamInput(3, "strSHIP_BY", arrSHIP_BY, OracleDbType.Varchar2);

                var arrSHIP_TO_NAME = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Ship to Name")).ToArray();
                param.AddParamInput(4, "strSHIP_TO_NAME", arrSHIP_TO_NAME, OracleDbType.Varchar2);

                var arrSHIP_TO_ADDL1 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Ship to Address-Line One")).ToArray();
                param.AddParamInput(5, "strSHIP_TO_ADDL1", arrSHIP_TO_ADDL1, OracleDbType.Varchar2);

                var arrSHIP_TO_ADDL2 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Ship to Address-Line Two")).ToArray();
                param.AddParamInput(6, "strSHIP_TO_ADDL2", arrSHIP_TO_ADDL2, OracleDbType.Varchar2);

                var arrSHIP_TO_COUNTRY = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Ship to Country")).ToArray();
                param.AddParamInput(7, "strSHIP_TO_COUNTRY", arrSHIP_TO_COUNTRY, OracleDbType.Varchar2);

                var arrCUSTOMER_PO_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Customer PO")).ToArray();
                param.AddParamInput(8, "strCUSTOMER_PO_NO", arrCUSTOMER_PO_NO, OracleDbType.Varchar2);

                var arrDISCOUNT_AMOUNT = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Discount Amount")).ToArray();
                param.AddParamInput(9, "strDISCOUNT_AMOUNT", arrDISCOUNT_AMOUNT, OracleDbType.Decimal);

                var arrSALES_TAX_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Tax ID")).ToArray();
                param.AddParamInput(10, "strSALES_TAX_ID", arrSALES_TAX_ID, OracleDbType.Varchar2);

                var arrNUMBER_OF_DISTRIB = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Number of Distributions")).ToArray();
                param.AddParamInput(11, "strNUMBER_OF_DISTRIB", arrNUMBER_OF_DISTRIB, OracleDbType.Decimal);

                var arrSO_DISTRIB = dt.AsEnumerable().ToList().Select(r => r.Field<string>("SO/Proposal Distribution")).ToArray();
                param.AddParamInput(12, "strSO_DISTRIB", arrSO_DISTRIB, OracleDbType.Decimal);

                var arrQUANTITY = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Quantity")).ToArray();
                param.AddParamInput(13, "strQUANTITY ", arrQUANTITY, OracleDbType.Decimal);

                var arrITEM_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Item ID")).ToArray();
                param.AddParamInput(14, "strITEM_ID", arrITEM_ID, OracleDbType.Varchar2);

                var arrDESCRIPTION = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Description")).ToArray();
                param.AddParamInput(15, "strDESCRIPTION", arrDESCRIPTION, OracleDbType.Varchar2);

                var arrGL_ACCOUNT = dt.AsEnumerable().ToList().Select(r => r.Field<string>("G/L Account")).ToArray();
                param.AddParamInput(16, "strGL_ACCOUNT", arrGL_ACCOUNT, OracleDbType.Varchar2);

                var arrUNIT_PRICE = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Unit Price")).ToArray();
                param.AddParamInput(17, "strUNIT_PRICE", arrUNIT_PRICE, OracleDbType.Decimal);

                var arrUM_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("U/M ID")).ToArray();
                param.AddParamInput(18, "strUM_ID", arrUM_ID, OracleDbType.Varchar2);

                param.AddParamInput(19, "strUSER_ID", ArrayOf<object>.Create(dt.Rows.Count, userid), OracleDbType.Varchar2);

                param.AddParamOutput(20, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dt.Rows.Count);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param, dt.Rows.Count);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;
                else
                {
                    if (((Oracle.DataAccess.Types.OracleString[])(param.ReturnValue(20)))[0].Value == "OK")
                    {
                        return "OK";
                    }
                    else
                        return "Error";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Document> LovDocumentSalesList(string partyId, string findValue)
        {
            List<Document> lstDocument = null;
            Document document;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_SALES_ORDER_LIST" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strPARTY_ID", partyId);
                param.AddParamInput(2, "strFindAll", findValue);



                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstDocument = new List<Document>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        document = new Document();

                        document.DOC_NO = OraDataReader.Instance.GetString("DOC_NO");
                        document.DOC_DATE = OraDataReader.Instance.GetString("DOC_DATE");

                        lstDocument.Add(document);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstDocument = null;
                throw ex;
            }

            return lstDocument;
        }

        public DataTable GenerateSalesOrderDetail(string whID, string partyId, string salesNo)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "SO_PACK.GENERATE_SALES_ORDER_DTL" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strWH", whID);
                param.AddParamInput(2, "strCUSTOMER_ID", partyId);
                param.AddParamInput(3, "strSALES_ORDER_NO", salesNo);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);
            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public string GetSoProductionType(string sono)
        {
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "INFO.SO_PROD_TYPE" };

                param.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 100);
                param.AddParamInput(1, "strNo", sono);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(0);
                if (!resultDB.IsNull)
                {
                    return resultDB.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
