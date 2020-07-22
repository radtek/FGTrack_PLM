using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using HTN.BITS.BEL.PLASESS;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HTN.BITS.BLL.PLASESS
{
    public class LoadingOrderBLL : IDisposable
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

        ~LoadingOrderBLL()
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

        public LoadingOrderBLL()
        {
        }

        #region "Method Member"

        public List<LoadingOrder> GetLoadingOrderList(string findValue, string whid, DateTime? formDate, DateTime? toDate)
        {
            List<LoadingOrder> lstLoadingOrd = null;
            LoadingOrder LoadingOrd;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "LOADING_PACK.GET_LOADING_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strLOADING_NO", DBNull.Value);
                if (formDate.HasValue)
                {
                    param.AddParamInput(3, "strDel_DT_From", formDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDel_DT_From", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(4, "strDel_DT_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDel_DT_to", DBNull.Value);
                }
                param.AddParamInput(5, "strWH_ID", whid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstLoadingOrd = new List<LoadingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        LoadingOrd = new LoadingOrder();

                        LoadingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        LoadingOrd.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        LoadingOrd.LOADING_DATE = OraDataReader.Instance.GetDateTime("LOADING_DATE");
                        LoadingOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        LoadingOrd.TRUCK_NO = OraDataReader.Instance.GetString("TRUCK_NO");
                        LoadingOrd.CONTAINER_NO = OraDataReader.Instance.GetString("CONTAINER_NO");
                        LoadingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        LoadingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstLoadingOrd.Add(LoadingOrd);
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

            return lstLoadingOrd;
        }

        public List<LoadingOrder> AdvLoadingOrder(string loadingNo, string whid, DateTime? formDate, DateTime? toDate)
        {
            List<LoadingOrder> lstLoadingOrd = null;
            LoadingOrder LoadingOrd;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "LOADING_PACK.GET_LOADING_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strLOADING_NO", loadingNo);
                if (formDate.HasValue)
                {
                    param.AddParamInput(3, "strDel_DT_From", formDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDel_DT_From", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(4, "strDel_DT_to", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDel_DT_to", DBNull.Value);
                }
                param.AddParamInput(5, "strWH_ID", whid);


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstLoadingOrd = new List<LoadingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        LoadingOrd = new LoadingOrder();

                        LoadingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        LoadingOrd.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        LoadingOrd.LOADING_DATE = OraDataReader.Instance.GetDateTime("LOADING_DATE");
                        LoadingOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        LoadingOrd.TRUCK_NO = OraDataReader.Instance.GetString("TRUCK_NO");
                        LoadingOrd.CONTAINER_NO = OraDataReader.Instance.GetString("CONTAINER_NO");
                        LoadingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        LoadingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstLoadingOrd.Add(LoadingOrd);
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

            return lstLoadingOrd;
        }

        public LoadingOrder GetLoadingOrder(string loadingNo)
        {
            LoadingOrder LoadingOrd = null;

            try
            {
                ProcParam param = new ProcParam(6) { ProcedureName = "LOADING_PACK.GET_LOADING_HDR" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strLOADING_NO", loadingNo);
                param.AddParamInput(3, "strDel_DT_From", DBNull.Value);
                param.AddParamInput(4, "strDel_DT_to", DBNull.Value);
                param.AddParamInput(5, "strWH_ID", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        LoadingOrd = new LoadingOrder();

                        LoadingOrd.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        LoadingOrd.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        LoadingOrd.LOADING_DATE = OraDataReader.Instance.GetDateTime("LOADING_DATE");
                        LoadingOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        LoadingOrd.TRUCK_NO = OraDataReader.Instance.GetString("TRUCK_NO");
                        LoadingOrd.CONTAINER_NO = OraDataReader.Instance.GetString("CONTAINER_NO");
                        LoadingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        LoadingOrd.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                LoadingOrd = null;
                throw ex;
            }

            return LoadingOrd;
        }

        public DataTable GetLoadingOrderDetail(string loadingNo)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "LOADING_PACK.GET_LOADING_DTL" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strLOADING_NO", loadingNo);

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

        public string InsertLoadingOrder(ref LoadingOrder loadingOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10) { ProcedureName = "LOADING_PACK.LOADING_HDR_INS" };

                paramHDR.AddParamInOutput(0, "strLOADING_NO", OracleDbType.Varchar2, 255, loadingOrd.LOADING_NO);
                paramHDR.AddParamInput(1, "strLOADING_DATE ", loadingOrd.LOADING_DATE);
                paramHDR.AddParamInput(2, "strDELIVERY_DATE", loadingOrd.DELIVERY_DATE);
                paramHDR.AddParamInput(3, "strTRUCK_NO", loadingOrd.TRUCK_NO);
                paramHDR.AddParamInput(4, "strCONTAINER_NO", loadingOrd.CONTAINER_NO);
                paramHDR.AddParamInput(5, "strWH_ID", loadingOrd.WH_ID);
                paramHDR.AddParamInput(6, "strREMARK", loadingOrd.REMARK);
                paramHDR.AddParamInput(7, "strREC_STAT", (loadingOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(8, "strUSER_ID", userid);

                paramHDR.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)paramHDR.ReturnValue(0);
                OracleString result = (OracleString)paramHDR.ReturnValue(9);

                if (!result.IsNull)
                {
                    loadingOrd.LOADING_NO = resultDB.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateLoadingOrder(LoadingOrder loadingOrd, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10) { ProcedureName = "LOADING_PACK.LOADING_HDR_UPD" };

                paramHDR.AddParamInput(0, "strLOADING_NO", loadingOrd.LOADING_NO);
                paramHDR.AddParamInput(1, "strLOADING_DATE ", loadingOrd.LOADING_DATE);
                paramHDR.AddParamInput(2, "strDELIVERY_DATE", loadingOrd.DELIVERY_DATE);
                paramHDR.AddParamInput(3, "strTRUCK_NO", loadingOrd.TRUCK_NO);
                paramHDR.AddParamInput(4, "strCONTAINER_NO", loadingOrd.CONTAINER_NO);
                paramHDR.AddParamInput(5, "strWH_ID", loadingOrd.WH_ID);
                paramHDR.AddParamInput(6, "strREMARK", loadingOrd.REMARK);
                paramHDR.AddParamInput(7, "strREC_STAT", (loadingOrd.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(8, "strUSER_ID", userid);

                paramHDR.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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

        public DataSet PrintLoadingOrderReport(List<LoadingOrder> lstLoadingOrd)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_LOADING_ORDER");

            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstLoadingOrd.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrLOADING_NO = (from job in lstLoadingOrd
                                 select job.LOADING_NO).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrLOADING_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstLoadingOrd.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstLoadingOrd.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstLoadingOrd.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (LoadingOrder loadingOrd in lstLoadingOrd)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", loadingOrd.LOADING_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                //get print value header
                DataTable dtHeader = PrintingBuilder.Instance.PrintTableResult("LOADING_PACK.RPT_DELIVERY_HDR",
                                                                               seqPrint,
                                                                               "T_LOADING");

                dtsResult.Tables.Add(dtHeader);


                DataTable dtDetail = this.GetPrintLoadingOrder(seqPrint, "T_LOADING_HDR");

                dtsResult.Tables.Add(dtDetail);

                //maping datatable to dataset
                dtsResult.Relations.Add("T_LOADING_T_LOADING_HDR",
                                dtsResult.Tables["T_LOADING"].Columns["LOADING_NO"],
                                dtsResult.Tables["T_LOADING_HDR"].Columns["LOADING_NO"]);

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

        private DataTable GetPrintLoadingOrder(int seqNo, string tableName)
        {
            List<LoadingOrder> lstLoadingOrd = null;
            LoadingOrder loadingOrd;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "LOADING_PACK.RPT_DELIVERY_HDR" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstLoadingOrd = new List<LoadingOrder>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        loadingOrd = new LoadingOrder();

                        loadingOrd.WH_ID = OraDataReader.Instance.GetString("WH");
                        loadingOrd.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        loadingOrd.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        loadingOrd.TRUCK_NO = OraDataReader.Instance.GetString("TRUCK_NO");
                        loadingOrd.REMARK = OraDataReader.Instance.GetString("REMARK");
                        //loadingOrd.BARCODE = UtilityBLL.QRCode_Encode(loadingOrd.LOADING_NO);

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

        public DataSet PrintDeliverySlipReport(List<LoadingOrder> lstLoadingOrd)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_DELIVERY_SLIP");
            int seqPrint = 0;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstLoadingOrd.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrLOADING_NO = (from job in lstLoadingOrd
                                     select job.LOADING_NO).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrLOADING_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstLoadingOrd.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstLoadingOrd.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstLoadingOrd.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (LoadingOrder loadingOrd in lstLoadingOrd)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", loadingOrd.LOADING_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

                //get print value header
                DataTable dtHeader = PrintingBuilder.Instance.PrintTableResult("LOADING_PACK.RPT_DELIVERY_HDR",
                                                                               seqPrint,
                                                                               "T_LOADING_HDR");
                dtsResult.Tables.Add(dtHeader);
                //------------------------------------------------------------------------------------------
                //get print value detail
                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("LOADING_PACK.RPT_DELIVERY_DTL",
                                                                               seqPrint,
                                                                               "T_LOADING_DTL");
                dtsResult.Tables.Add(dtDetail);
                //------------------------------------------------------------------------------------------
                dtsResult.Relations.Add("T_LOADING_HDR_T_LOADING_DTL",
                                dtsResult.Tables["T_LOADING_HDR"].Columns["LOADING_NO"],
                                dtsResult.Tables["T_LOADING_DTL"].Columns["LOADING_NO"]);

                PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

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

        public DataSet PrintPartListInPalletReport(string loadingNo)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_PARTLIST_IN_PALLET");
            try
            {
                //get print value header
                DataTable dtHeader = PrintingBuilder.Instance.PrintTableResult("LOADING_PACK.RPT_PART_LIST_R1",
                                                                               loadingNo,
                                                                               "PART_LIST_HDR");
                dtsResult.Tables.Add(dtHeader);
                //------------------------------------------------------------------------------------------
                //get print value detail
                DataTable dtDetail = PrintingBuilder.Instance.PrintTableResult("LOADING_PACK.RPT_PART_LIST_R2",
                                                                               loadingNo,
                                                                               "PART_LIST_DTL");
                dtsResult.Tables.Add(dtDetail);
                //------------------------------------------------------------------------------------------
                //dtsResult.Relations.Add("PART_LIST_HDR_PART_LIST_DTL",
                //                dtsResult.Tables["PART_LIST_HDR"].Columns["LOADING_NO"],
                //                dtsResult.Tables["PART_LIST_DTL"].Columns["LOADING_NO"]);

                //change mutiples column
                dtsResult.Relations.Add("PART_LIST_HDR_PART_LIST_DTL",
                                new DataColumn[] {dtsResult.Tables["PART_LIST_HDR"].Columns["LOADING_NO"],
                                                  dtsResult.Tables["PART_LIST_HDR"].Columns["CUSTOMER"]
                                                 },
                                new DataColumn[] {dtsResult.Tables["PART_LIST_DTL"].Columns["LOADING_NO"],
                                                  dtsResult.Tables["PART_LIST_DTL"].Columns["CUSTOMER"]
                                                 }
                                       );

                dtsResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtsResult;

        }

        #endregion
         
    }
}
