using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HTN.BITS.DAL;
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.BLL.PLASESS
{
    public class QueryBLL : IDisposable
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

        ~QueryBLL()
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

        public QueryBLL()
        {
        }

        public List<Warehouse> GetWarehouse()
        {
            List<Warehouse> lstWH = null;
            Warehouse wareHouse;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "LOV_PACK.GET_WH_LIST";
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

        public DataTable JobTrackingList(string jobNo, string partyID, DateTime? fromDate, DateTime? toDate,
            string productionType, string mcNo, string product)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(8);

                param.ProcedureName = "QUERY_PACK.JOB_TRACKING";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strJOB_NO", jobNo);
                param.AddParamInput(2, "strPARTY_ID", partyID);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strPLAN_DATE_FROM", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strPLAN_DATE_FROM", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strPLAN_DATE_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strPLAN_DATE_TO", DBNull.Value);
                }
                param.AddParamInput(5, "strPROD_TYPE", productionType);
                param.AddParamInput(6, "strMC_NO", mcNo);
                param.AddParamInput(7, "strPRODUCT", product);

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

        public DataTable JobTrackingDetail(string jobNo, string jobLot)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "QUERY_PACK.JOB_TRACKING_DTL";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strJOB_NO", jobNo);
                param.AddParamInput(2, "strJOB_LOT", jobLot);
                
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

        public DataTable StockAsOn(string whid, string product)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "QUERY_PACK.STOCK_AS_ON";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);

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

        public DataTable StockAsOn(string whid, string partyid, string product)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.STOCK_AS_ON_REVISE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                param.AddParamInput(3, "strCustomer", partyid);

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

        public DataTable StockAsOnDate(string whid, string partyid, string product, DateTime? dateSelect)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "QUERY_PACK.STOCK_AS_ON_DATE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                param.AddParamInput(3, "strCustomer", partyid);
                if (dateSelect != null)
                {
                    param.AddParamInput(4, "strAsOnDate", dateSelect.Value);
                }
                else
                {
                    param.AddParamInput(4, "strAsOnDate", DBNull.Value);
                }

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

        public DataTable StockAsOnDateDetail(string whid, string product, DateTime? dateSelect)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.STOCK_AS_ON_DATE_DTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                if (dateSelect.HasValue)
                    param.AddParamInput(3, "strAsOnDate", dateSelect.Value);
                else
                    param.AddParamInput(3, "strAsOnDate", DBNull.Value);

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

        public DataTable ProductionSummaryDaily(string productionType, string product, DateTime? fromDate, DateTime? toDate, string shift)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "QUERY_PACK.PROD_SUM_DAILY2" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strProdType", productionType);
                param.AddParamInput(2, "strPRODUCT", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strFGDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strFGDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strFGDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strFGDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strShift", shift);

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

        public DataTable StockInSummary(string whid, string partyID, string product, DateTime? fromDate, DateTime? toDate, string shift)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(7);

                param.ProcedureName = "QUERY_PACK.STOCK_IN_SUM2";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strCustomer", partyID);
                param.AddParamInput(6, "strShift", shift);

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

        public DataTable StockOutSummary(string whid, string partyID, string product, DateTime? fromDate, DateTime? toDate, string shift)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "QUERY_PACK.STOCK_OUT_SUM2" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strCustomer", partyID);
                param.AddParamInput(6, "strShift", shift);

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

        public DataTable StockOutSummaryDetail(string refNo, string product, DateTime outDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4);

                param.ProcedureName = "QUERY_PACK.STOCK_OUT_DTL";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strREF_NO", refNo);
                param.AddParamInput(2, "strPRODUCT", product);
                param.AddParamInput(3, "strOutDate", outDate);

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

        public DataTable StockOutSummary_Mtl(string whid, string partyID, string product, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(6);

                param.ProcedureName = "QUERY_PACK.STOCK_OUT_MTL_SUM";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strMTL", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strCustomer", partyID);
                //param.AddParamInput(6, "strShift", shift);

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

        public DataTable StockInSummary_Mtl(string whid, string partyID, string product, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(6);

                param.ProcedureName = "QUERY_PACK.STOCK_IN_MTL_SUM";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strMTL", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strCustomer", partyID);
                //param.AddParamInput(6, "strShift", shift);

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

        public DataTable StockInHistory(string whid, string partyID, string product, DateTime? fromDate, DateTime? toDate, string shift)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(7);

                param.ProcedureName = "QUERY_PACK.STOCK_IN_HISTORY";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH_ID", whid);
                param.AddParamInput(2, "strPRODUCT", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDate_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDate_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDate_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDate_to", DBNull.Value);
                }
                param.AddParamInput(5, "strCustomer", partyID);
                param.AddParamInput(6, "strShift", shift);

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

        public DataTable DeliveryLotSummary(string productionType, string product, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "QUERY_PACK.DELIVERY_LOT_SUMMARY" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strProdType", productionType);
                param.AddParamInput(2, "strPRODUCT", product);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDelivery_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDelivery_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(4, "strDelivery_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDelivery_to", DBNull.Value);
                }

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

        public DataTable StockAsOnDate_Mtl(string location, string partyid, string material)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.STOCK_AS_ON_DATE_MTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strLOCATION_ID", location);
                param.AddParamInput(2, "strPARTY_ID", partyid);
                param.AddParamInput(3, "strCustomer", material);

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

        public DataTable StockAsOnDateDetail_Mtl(string mtl_code)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "QUERY_PACK.STOCK_AS_ON_DATE_MTL_DTL" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strMTL", mtl_code);

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


        public DataTable StockOutSummaryDetail_Mtl(string refNo, string mtlCode, DateTime outDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.STOCK_OUT_MTL_DTL" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strREF_NO", refNo);
                param.AddParamInput(2, "strMTL", mtlCode);
                param.AddParamInput(3, "strOutDate", outDate);


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

        public DataTable StockInSummaryDetail_Mtl(string whId, string arrType,string partyName,string mtlCode, DateTime inDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "QUERY_PACK.STOCK_IN_MTL_DTL" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strWH_ID", whId);
                param.AddParamInput(2, "strARR_TYPE", arrType);
                param.AddParamInput(3, "strPARTY_NAME", partyName);
                param.AddParamInput(4, "strMTL_CODE", mtlCode);
                param.AddParamInput(5, "strInDate", inDate);


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

        public DataTable MaterialMixedDaily(string mcno, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.MIXED_DAILY_RECORD" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strMC_NO", mcno);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(2, "strDelivery_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(2, "strDelivery_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDelivery_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDelivery_to", DBNull.Value);
                }

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

        public DataTable MaterialRepDaily(string mcno, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "QUERY_PACK.REP_DAILY_RECORD" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strMC_NO", mcno);
                if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(2, "strDelivery_from", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(2, "strDelivery_from", DBNull.Value);
                }
                if (toDate.HasValue && toDate.Value != DateTime.MinValue)
                {
                    param.AddParamInput(3, "strDelivery_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDelivery_to", DBNull.Value);
                }

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


        public DataTable Doc_PurchaseOrder_List()
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "QUERY_PACK.DOC_PO_INFO" };
                param.AddParamRefCursor(0, "IO_CURSOR");

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

        public DataTable Doc_SalesOrder_List()
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "QUERY_PACK.DOC_SO_INFO" };
                param.AddParamRefCursor(0, "IO_CURSOR");

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



       
    }
}
