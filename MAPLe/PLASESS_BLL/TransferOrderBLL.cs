using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Drawing;
using System.ComponentModel;
using HTN.BITS.QRCodeLib;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;

namespace HTN.BITS.BLL.PLASESS
{
    public class TransferOrderBLL : IDisposable
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

        ~TransferOrderBLL()
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
        
        public TransferOrderBLL()
        {
            //constructer
        }

        #region Transfer Order
        public List<Product> LovProductList(string search, string prodtype, string partyid)
        {
            List<Product> lstProduct = null;
            Product product;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "LOV_PACK.GET_PRODUCT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", search);
                param.AddParamInput(2, "strPROD_SEQ_NO", DBNull.Value);
                param.AddParamInput(3, "strPROD_TYPE", prodtype);
                param.AddParamInput(4, "strPARTY_ID", partyid);
                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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
                        //product.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        product.UNIT = OraDataReader.Instance.GetString("UNIT");
                        //  product.FREE_STOCK = OraDataReader.Instance.GetInteger("FREE_STOCK");
                        //use for unit price
                        //  product.COST_PRICE = OraDataReader.Instance.GetDecimal("UNIT_PRICE");
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

        public Product LovProductByNo( string prodNo)
        {
            Product product = null;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_PRODUCT_NO" };
                param.AddParamRefCursor(0, "io_cursor");
                // param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(1, "strPROD_NO", prodNo);
                param.AddParamInput(2, "strPROD_TYPE", DBNull.Value);
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
                      //  product.FREE_STOCK = OraDataReader.Instance.GetInteger("FREE_STOCK");
                        //use for unit price
                      //  product.COST_PRICE = OraDataReader.Instance.GetDecimal("UNIT_PRICE");
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

        public List<TransferOrderList> GetQCTransferOrderList(string findValue, string ProdTypeID, DateTime? formDate, DateTime? toDate)
        {
            List<TransferOrderList> lstQCTO_list = null;
            TransferOrderList TO_list;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "TO_PACK.GET_TO_LIST" };
               
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPROD_TYPE", ProdTypeID);
                if (formDate.HasValue)
                {
                    param.AddParamInput(3, "strDT_FROM", formDate.Value);
                }
                else
                {
                    param.AddParamInput(3, "strDT_FROM", DBNull.Value);
                }
                if (toDate.HasValue)
                {
                    param.AddParamInput(4, "strDT_TO", toDate.Value);
                }
                else
                {
                    param.AddParamInput(4, "strDT_TO", DBNull.Value);
                }


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstQCTO_list = new List<TransferOrderList>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        TO_list = new TransferOrderList();

                        TO_list.PRODUCT_TYPE = OraDataReader.Instance.GetString("PRODUCT_TYPE");
                        TO_list.TO_NO = OraDataReader.Instance.GetString("TO_NO");
                        TO_list.TO_DATE = OraDataReader.Instance.GetDateTime("TO_DATE");
                        TO_list.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        TO_list.REMARK = OraDataReader.Instance.GetString("REMARK");
                        TO_list.DESTINATION = OraDataReader.Instance.GetString("DELIVERY_PLACE");
                        TO_list.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        TO_list.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        TO_list.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        TO_list.QTY = OraDataReader.Instance.GetInteger("QTY");
                        TO_list.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        TO_list.DEV_QTY = OraDataReader.Instance.GetInteger("DEV_QTY");
                        TO_list.DEV_BOX = OraDataReader.Instance.GetInteger("DEV_BOX");
                        TO_list.REMARK = OraDataReader.Instance.GetString("REMARK");
                        TO_list.POST_REF = OraDataReader.Instance.GetString("POST_REF");
                        lstQCTO_list.Add(TO_list);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstQCTO_list = null;
                throw ex;
            }

            return lstQCTO_list;
        }

        public TransferOrderHdr GetTransferOrder(string qcReturnNo)
        {
            TransferOrderHdr TOhdr = null;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "TO_PACK.GET_TO_HDR" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strTO_NO", qcReturnNo);


                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        TOhdr = new TransferOrderHdr();

                        TOhdr.TO_NO = OraDataReader.Instance.GetString("TO_NO");
                        TOhdr.TO_DATE = OraDataReader.Instance.GetDateTime("TO_DATE");
                        TOhdr.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        TOhdr.PROD_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        TOhdr.TO_DEST = OraDataReader.Instance.GetString("TO_DEST");
                        TOhdr.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        TOhdr.REMARK = OraDataReader.Instance.GetString("REMARK");
                        TOhdr.ISSUED_BY = OraDataReader.Instance.GetString("ISSUED_BY");
                        TOhdr.ISSUED_DATE = OraDataReader.Instance.GetDateTime("ISSUED_DATE");
                        TOhdr.UPDATE_BY = OraDataReader.Instance.GetString("UPDATE_BY");
                        TOhdr.UPDATE_DATE = OraDataReader.Instance.GetDateTime("UPDATE_DATE");
                        TOhdr.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        TOhdr.POST_REF = OraDataReader.Instance.GetString("POST_REF");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                TOhdr = null;
                throw ex;
            }

            return TOhdr;
        }

        public DataTable GetQCTODetail(string qcReturnNo)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "TO_PACK.GET_TO_DTL";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strTO_NO", qcReturnNo);

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

        public string InsertTransferOrder(ref TransferOrderHdr TO_HDR, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10);

                paramHDR.ProcedureName = "TO_PACK.TO_HDR_INS";

                paramHDR.AddParamInOutput(0, "strTO_NO", OracleDbType.Varchar2, 255, TO_HDR.TO_NO);
                paramHDR.AddParamInput(1, "strTO_DATE ", TO_HDR.TO_DATE);
                paramHDR.AddParamInput(2, "strREF_NO", TO_HDR.REF_NO);
                paramHDR.AddParamInput(3, "strPROD_TYPE", TO_HDR.PROD_TYPE);
                paramHDR.AddParamInput(4, "strTO_DEST", TO_HDR.TO_DEST);
                paramHDR.AddParamInput(5, "strDELIVERY_DATE", TO_HDR.DELIVERY_DATE);
                paramHDR.AddParamInput(6, "strREMARK", TO_HDR.REMARK);


                paramHDR.AddParamInput(7, "strREC_STAT", (TO_HDR.REC_STAT ? "Y" : "N"));
                paramHDR.AddParamInput(8, "strUSER_ID", userid);

                paramHDR.AddParamOutput(9, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString resultDB = (OracleString)paramHDR.ReturnValue(0);
                OracleString result = (OracleString)paramHDR.ReturnValue(9);

                if (!result.IsNull)
                {
                    TO_HDR.TO_NO = resultDB.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateTransferOrder(TransferOrderHdr TO_HDR, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(10);

                paramHDR.ProcedureName = "TO_PACK.TO_HDR_UPD";

                paramHDR.AddParamInput(0, "strTO_NO",  TO_HDR.TO_NO);
                paramHDR.AddParamInput(1, "strTO_DATE ", TO_HDR.TO_DATE);
                paramHDR.AddParamInput(2, "strREF_NO", TO_HDR.REF_NO);
                paramHDR.AddParamInput(3, "strPROD_TYPE", TO_HDR.PROD_TYPE);
                paramHDR.AddParamInput(4, "strTO_DEST", TO_HDR.TO_DEST);
                paramHDR.AddParamInput(5, "strDELIVERY_DATE", TO_HDR.DELIVERY_DATE);
                paramHDR.AddParamInput(6, "strREMARK", TO_HDR.REMARK);


                paramHDR.AddParamInput(7, "strREC_STAT", (TO_HDR.REC_STAT ? "Y" : "N"));
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

        public string UpdateTransferOrderDtl(List<TransferOrderDtl> lstto_dtl, string userid)
        {
            List<ProcParam> lstParam = new List<ProcParam>(lstto_dtl.Count);
            ProcParam param = null;

            try
            {
                foreach (TransferOrderDtl todtl in lstto_dtl)
                {
                    switch (todtl.FLAG)
                    {
                        case 0: //delete
                            param = new ProcParam(4);
                            param.ProcedureName = "TO_PACK.TO_DTL_DEL";

                            param.AddParamInput(0, "strTO_NO", todtl.TO_NO);
                            param.AddParamInput(1, "strPROD_SEQ_NO", todtl.PROD_SEQ_NO);
                            param.AddParamInput(2, "strUSER_ID", userid);
                            param.AddParamOutput(3, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(param);

                            break;
                        case 1: //no change
                            //param = new ProcParam(8);
                            //param.ProcedureName = "TO_PACK.TO_DTL_UPD";

                            //param.AddParamInput(0, "strTO_NO", todtl.TO_NO);
                            //param.AddParamInput(1, "strPROD_SEQ_NO", todtl.PROD_SEQ_NO);
                            //param.AddParamInput(2, "strQTY", todtl.QTY);
                            //param.AddParamInput(3, "strUNIT_ID", todtl.UNIT_ID);
                            //param.AddParamInput(4, "strREMARK", todtl.REMARK);
                            //param.AddParamInput(5, "strREC_STAT", "Y");
                            //param.AddParamInput(6, "strUSER_ID", userid);
                            //param.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");


                            //lstParam.Add(param);

                            break;
                        case 2: //add new 

                            param = new ProcParam(8);
                            param.ProcedureName = "TO_PACK.TO_DTL_INS";

                            param.AddParamInput(0, "strTO_NO", todtl.TO_NO);
                            param.AddParamInput(1, "strPROD_SEQ_NO", todtl.PROD_SEQ_NO);
                            param.AddParamInput(2, "strQTY", todtl.QTY);
                            param.AddParamInput(3, "strUNIT_ID", todtl.UNIT_ID);
                            param.AddParamInput(4, "strREMARK", todtl.REMARK);
                            param.AddParamInput(5, "strREC_STAT", "Y");
                            param.AddParamInput(6, "strUSER_ID", userid);
                            param.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");

                            lstParam.Add(param);

                            break;
                        case 3: //update
                            param = new ProcParam(8);
                            param.ProcedureName = "TO_PACK.TO_DTL_UPD";

                            param.AddParamInput(0, "strTO_NO", todtl.TO_NO);
                            param.AddParamInput(1, "strPROD_SEQ_NO", todtl.PROD_SEQ_NO);
                            param.AddParamInput(2, "strQTY", todtl.QTY);
                            param.AddParamInput(3, "strUNIT_ID", todtl.UNIT_ID);
                            param.AddParamInput(4, "strREMARK", todtl.REMARK);
                            param.AddParamInput(5, "strREC_STAT", "Y");
                            param.AddParamInput(6, "strUSER_ID", userid);
                            param.AddParamOutput(7, "RESULTMSG", OracleDbType.NVarchar2, 255, "OK");


                            lstParam.Add(param);

                            break;
                        default:
                            break;
                    }
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

        public string DeleteTransferOrder(String tono, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam paramHDR = new ProcParam(3);

                paramHDR.ProcedureName = "TO_PACK.TO_HDR_DEL";

                paramHDR.AddParamInput(0, "strTO_NO", tono);
                paramHDR.AddParamInput(1, "strUSER_ID", userid);

                paramHDR.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                #endregion

                GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHDR);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

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

        public DataSet PrintTO(List<string> lstTO, string userid)
        {
            //declare dataset and name.
            DataSet dtsResult = new DataSet("DTS_TRANSFER_ORDER");
            int seqPrint = 0;

            try
            {
                seqPrint = PrintingBuilder.Instance.GeneratePrintSEQ();

                ProcParam paramPrint = new ProcParam(4) { ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS" };
                //param 0 - SEQ_NO
                paramPrint.AddParamInput(0, "strSEQ_NO", ArrayOf<object>.Create(lstTO.Count, seqPrint), OracleDbType.Varchar2);

                //param 1 - strTR1
                var arrTR_NO = (from trans in lstTO
                                    select trans).ToArray();
                paramPrint.AddParamInput(1, "strTR1", arrTR_NO, OracleDbType.Varchar2);

                //param 2 - strTR2
                paramPrint.AddParamInput(2, "strTR2", ArrayOf<object>.Create(lstTO.Count, null), OracleDbType.Varchar2);

                //param 3 - strTR3
                paramPrint.AddParamInput(3, "strTR3", ArrayOf<object>.Create(lstTO.Count, null), OracleDbType.Varchar2);

                //insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(paramPrint, lstTO.Count);

                //List<ProcParam> procInsTPrintList = new List<ProcParam>();
                //ProcParam procInsTPrint = null;
                //foreach (string tono in lstTO)
                //{
                //    procInsTPrint = new ProcParam(4);

                //    procInsTPrint.ProcedureName = "GLOBAL_FUNCTION_PACK.T_PRINT_TRANSACTION_INS";

                //    procInsTPrint.AddParamInput(0, "strSEQ_NO", seqPrint);
                //    procInsTPrint.AddParamInput(1, "strTR1", tono);
                //    procInsTPrint.AddParamInput(2, "strTR2", DBNull.Value);
                //    procInsTPrint.AddParamInput(3, "strTR3", DBNull.Value);

                //    procInsTPrintList.Add(procInsTPrint);
                //}

                ////insert value to print transaction.
                //PrintingBuilder.Instance.InsertTransactionPrint(procInsTPrintList);

               // DataTable dtHeader = PrintingBuilder.Instance.PrintTableResult("TO_PACK.RPT_TRANSFER_ORDER", seqPrint, "T_TRANSFER_ORDER");
                DataTable dtHeader = this.PrintTO(seqPrint, "T_TRANSFER_ORDER");

                dtsResult.Tables.Add(dtHeader);

                //PrintingBuilder.Instance.RemovePrintSEQ(seqPrint);

                return dtsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataTable PrintTO(int seqNo, string tableName)
        {
            List<TransferOrderRpt> lstTO = null;
            TransferOrderRpt TOrpt;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "TO_PACK.RPT_TRANSFER_ORDER" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strSEQ_NO", seqNo);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstTO = new List<TransferOrderRpt>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        TOrpt = new TransferOrderRpt();

                        TOrpt.TO_NO = OraDataReader.Instance.GetString("TO_NO");
                        TOrpt.PRODUCT_TYPE = OraDataReader.Instance.GetString("PRODUCT_TYPE");
                        TOrpt.TO_DATE = OraDataReader.Instance.GetDateTime("TO_DATE");
                        TOrpt.REF_NO = OraDataReader.Instance.GetString("REF_NO");
                        TOrpt.DESTINATION = OraDataReader.Instance.GetString("DESTINATION");
                        TOrpt.DELIVERY_DATE = OraDataReader.Instance.GetDateTime("DELIVERY_DATE");
                        TOrpt.HDR_REMARK = OraDataReader.Instance.GetString("HDR_REMARK");
                        TOrpt.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        TOrpt.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        TOrpt.QTY = OraDataReader.Instance.GetInteger("QTY");
                        TOrpt.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        TOrpt.QTY_PER_BOX = OraDataReader.Instance.GetInteger("QTY_PER_BOX");
                        TOrpt.NO_OF_BOX = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                       
                        //TOrpt.BARCODE = UtilityBLL.QRCode_Encode(TOrpt.TO_NO);
                        
                        TOrpt.DTL_REMARK = OraDataReader.Instance.GetString("DTL_REMARK");

                        lstTO.Add(TOrpt);
                    }
                }
                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstTO = null;
                throw ex;
            }

            return UtilityBLL.ListToDataTable(lstTO, tableName);

        }

        public DataTable GetCompletedTO(string tono, string userid)
        {
            DataTable dtResult = null;
            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "TO_PACK.GET_TO_CSV" };
                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strTO_NO", tono);
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

        public string UpdatePostTO(string tono, string postRef, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                #region "Transaction Header"

                ProcParam param = new ProcParam(4) { ProcedureName = "TO_PACK.TO_POSTREF_UPD" };

                param.AddParamInput(0, "strSO_NO", tono);
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

        # endregion
    }
}
