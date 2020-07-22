using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace HTN.BITS.BLL.PLASESS
{
    public class InterfaceDataBLL : IDisposable
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

        ~InterfaceDataBLL()
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

        public InterfaceDataBLL()
        {
        }

        public List<PostType> PostTypeList()
        {
            List<PostType> lstPostType = null;
            PostType postType;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "INTERFACE_PACK.LOV_POST_TYPE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstPostType = new List<PostType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        postType = new PostType();

                        postType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        postType.NAME = OraDataReader.Instance.GetString("NAME");

                        lstPostType.Add(postType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstPostType = null;
                throw ex;
            }

            return lstPostType;
        }

        public DataTable IfInAssemblyList(string postType, string wh_id, string productNo, string postRef,
            DateTime? fromDate, DateTime? toDate, DateTime? stkfDate, DateTime? stktDate, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(10) { ProcedureName = "INTERFACE_PACK.QUERY_FG_ASM" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strDATA_TYPE", postType);
                param.AddParamInput(2, "strWH_ID", wh_id);
                param.AddParamInput(3, "strPROD_NO", productNo);
                param.AddParamInput(4, "strPOST_REF ", postRef);
                //Post Date
                if (fromDate.HasValue)
                {
                    param.AddParamInput(5, "strDATE_FROM", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strDATE_FROM", DBNull.Value);
                }

                if (toDate.HasValue)
                {
                    param.AddParamInput(6, "strDATE_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(6, "strDATE_TO", DBNull.Value);
                }

                //Stock In Date
                if (stkfDate.HasValue)
                {
                    param.AddParamInput(7, "strSTK_DATE_FROM", stkfDate.Value);
                }
                else
                {
                    param.AddParamInput(7, "strSTK_DATE_FROM", DBNull.Value);
                }

                if (stktDate.HasValue)
                {
                    param.AddParamInput(8, "strSTK_DATE_TO", stktDate.Value);
                }
                else
                {
                    param.AddParamInput(8, "strSTK_DATE_TO", DBNull.Value);
                }

                param.AddParamInput(9, "strUser_id ", userid);

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

        public DataTable IfInPurchaseList(string postType, string productNo, string postRef, DateTime? fromDate, DateTime? toDate, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "INTERFACE_PACK.QUERY_FG_PURCHASE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strDATA_TYPE", postType);
                param.AddParamInput(2, "strPROD_NO", productNo);
                param.AddParamInput(3, "strPOST_REF ", postRef);
                if (fromDate != null)
                {
                    param.AddParamInput(4, "strDATE_FROM", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDATE_FROM", DBNull.Value);
                }

                if (toDate != null)
                {
                    param.AddParamInput(5, "strDATE_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strDATE_TO", DBNull.Value);
                }
                param.AddParamInput(6, "strUser_id ", userid);

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

        public DataTable IfInAdjustInList(string postType, string productNo, string postRef, DateTime? fromDate, DateTime? toDate, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(7) { ProcedureName = "INTERFACE_PACK.QUERY_FG_ADJ" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strDATA_TYPE", postType);
                param.AddParamInput(2, "strPROD_NO", productNo);
                param.AddParamInput(3, "strPOST_REF ", postRef);
                if (fromDate != null)
                {
                    param.AddParamInput(4, "strDATE_FROM", fromDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDATE_FROM", DBNull.Value);
                }

                if (toDate != null)
                {
                    param.AddParamInput(5, "strDATE_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(5, "strDATE_TO", DBNull.Value);
                }
                param.AddParamInput(6, "strUser_id ", userid);

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



        public string GetPostData(string procName, string wh, DataTable tableSelect, string pathSelect, string userid, out DataTable dtbPosted ) //out ICollection<string> files
        {
            //declare dataset and name.
            //files = new Collection<string>();
            dtbPosted = new DataTable();

            try
            {
                int seq = this.InsertPrintTransaction(tableSelect);
                if (seq < 0) throw new Exception("Can't Insert Sequance!");

                dtbPosted = this.GetDataToPost2(procName, wh, seq, userid);
                dtbPosted.AcceptChanges();

                return "Post Data Complete";

                /*
                List<IF_ControlOrder> lstIF_ShipOrd = this.GetDataToPost(procName, wh, seq, userid);

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

                return "Post Data Complete";
                 * */
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int InsertPrintTransaction(DataTable dtbSelect)
        {
            int seqPrint = -1;
            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (ProductCard prdCard in lstPrdCard)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", prdCard.SERIAL_NO);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(dtbSelect.Rows.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrPRODUCT_NO = (from DataRow row in dtbSelect.Rows
                                     select row["PRODUCT_NO"]).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrPRODUCT_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                var arrSTOCK_IN_DATE = (from DataRow row in dtbSelect.Rows
                                select string.Format("{0:yyyyMMdd}", row["STOCK_IN_DATE"])).ToArray();
                paramPrint.AddParamInput(2, "strTR2", arrSTOCK_IN_DATE, OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(dtbSelect.Rows.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, dtbSelect.Rows.Count);

                return seqPrint;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private List<IF_ControlOrder> GetDataToPost(string procName, string wh, int seq, string userid)
        {
            //List<IF_ShippingOrder> lstIfShippingOrd = new List<IF_ShippingOrder>();
            List<IF_ControlOrder> lstIfShippingOrd = null;
            IF_ControlOrder ifShipOrd;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = procName };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH", wh);
                param.AddParamInput(2, "strSEQ", seq);
                param.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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

        private DataTable GetDataToPost2(string procName, string wh, int seq, string userid)
        {
            DataTable dtb = new DataTable();

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = procName };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strWH", wh);
                param.AddParamInput(2, "strSEQ", seq);
                param.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);
                OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                dtb.Load(OraDataReader.Instance.OraReader);
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtb;
        }

        public string UploadRevision(string fileid, string userid, ICollection<string> alldata)
        {
            string seq = string.Empty;
            try
            {
                //Generate seq no with insert temporary data to table
                seq = this.GetBomRevisionSeq(fileid, userid);
                if (!string.IsNullOrEmpty(seq))
                {
                    List<ProcParam> lstParam = new List<ProcParam>(alldata.Count);
                    ProcParam param = null;
                    int i = 0;
                    foreach (string text in alldata)
                    {
                        i++;
                        param = new ProcParam(3);
                        param.ProcedureName = "INTERFACE_PACK.UP_BOM_REVISION_INS";

                        param.AddParamInput(0, "strNo", seq);
                        param.AddParamInput(1, "strLine", i);
                        param.AddParamInput(2, "strData", text);

                        lstParam.Add(param);
                    }

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(lstParam);
                    //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                }
                else
                {
                    throw new Exception("THE SYSTEM RETURN NULL VALUE");
                }

                return seq;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetBomRevisionSeq(string fileid, string userid)
        {
            string result = string.Empty;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "INTERFACE_PACK.UP_BOM_REVISION" };
                param.AddParamReturn(0, "ReturnValue", Oracle.DataAccess.Client.OracleDbType.Varchar2, 255);
                param.AddParamInput(1, "strFILE_ID", fileid);
                param.AddParamInput(2, "strUser_id", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)param.ReturnValue(0);
                if (!resultDB.IsNull)
                {
                    result = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public DataTable GetResultUploadRevision(string seq, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "INTERFACE_PACK.UP_BOM_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ", seq);
                param.AddParamInput(2, "strUser_id", userid);

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

        public string GetRevisionLastUpdate()
        {
            string result = string.Empty;
            try
            {
                ProcParam param = new ProcParam(1) { ProcedureName = "INTERFACE_PACK.RET_LAST_REV" };
                param.AddParamReturn(0, "ReturnValue", Oracle.DataAccess.Client.OracleDbType.Date, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);
                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                object oResult = param.ReturnValue(0);
                if (!oResult.Equals(DBNull.Value))
                {
                    OracleDate resultDB = (OracleDate)oResult;
                    if(!resultDB.IsNull)
                        result = resultDB.Value.ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
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

        public DataTable PostPurchaseReceiveInterface(string arrivalNo, string userid, out string seqno)
        {
            seqno = string.Empty;
            DataTable dtResult = null;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "INTERFACE_PACK.POST_PURCHASE_RECEIVE_SAGE50" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamOutput(1, "strSEQ_NO", OracleDbType.Varchar2, 30);
                param.AddParamInput(2, "strARRIVAL_NO", arrivalNo);
                param.AddParamInput(3, "strUSER_ID", userid);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

                OracleString resultDB = (OracleString)param.ReturnValue(1);

                if (!resultDB.IsNull)
                {
                    seqno = resultDB.Value;
                }
                else
                {
                    seqno = string.Empty;
                }

            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public DataTable PostWorkTicketInterface(int seq, string userid, out string seqno)
        {
            DataTable dtResult = null;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "INTERFACE_PACK.POST_JOB_WORKTICKET_SAGE50" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamOutput(1, "strSEQ_NO", OracleDbType.Varchar2, 30);
                param.AddParamInput(2, "strSEQ", seq);
                param.AddParamInput(3, "strUSER_ID", userid);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

                OracleString resultDB = (OracleString)param.ReturnValue(1);

                if (!resultDB.IsNull)
                {
                    seqno = resultDB.Value;
                }
                else
                {
                    seqno = string.Empty;
                }

            }
            catch (Exception ex)
            {
                dtResult = null;
                throw ex;
            }

            return dtResult;
        }

        public DataTable PostSalesInvoiceInterface(string soNo, string userid, out string seqno)
        {
            seqno = string.Empty;
            DataTable dtResult = null;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "INTERFACE_PACK.POST_SALES_INVOICE_SAGE50" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamOutput(1, "strSEQ_NO", OracleDbType.Varchar2, 30);
                param.AddParamInput(2, "strSO_NO", soNo);
                param.AddParamInput(3, "strUSER_ID", userid);

                dtResult = GlobalDB.Instance.DataAc.GetDataTable(param, 1000);

                OracleString resultDB = (OracleString)param.ReturnValue(1);

                if (!resultDB.IsNull)
                {
                    seqno = resultDB.Value;
                }
                else
                {
                    seqno = string.Empty;
                }

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
