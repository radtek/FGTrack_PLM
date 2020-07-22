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

namespace HTN.BITS.BLL.PLASESS
{
    public class ProductBLL : IDisposable
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

        ~ProductBLL()
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

        public ProductBLL()
        {
            //constructer
        }


        public List<Product> GetProductList(string findAll)
        {
            List<Product> lstProd = null;
            Product prod;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_PRODUCT";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findAll);
                param.AddParamInput(2, "strPROD_SEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstProd = new List<Product>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prod = new Product();

                        prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prod.MATERIAL_TYPE = OraDataReader.Instance.GetString("MATERIAL_TYPE");
                        prod.PRODUCTION_TYPE = OraDataReader.Instance.GetString("PRODUCTION_TYPE");
                        prod.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        prod.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        //prod.PROD_IMAGE = OraDataReader.Instance.GetBitmap("PROD_IMAGE");
                        prod.REMARK = OraDataReader.Instance.GetString("REMARK");
                        prod.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        //add new field 07-Jun-2011
                        prod.CUST_PROD_NO = OraDataReader.Instance.GetString("CUST_PROD_NO");
                        if (OraDataReader.Instance.IsDBNull("BUYER_PRICE")) prod.BUYER_PRICE = OraDataReader.Instance.GetDecimal("BUYER_PRICE");
                        if (OraDataReader.Instance.IsDBNull("SELLING_PRICE")) prod.SELLING_PRICE = OraDataReader.Instance.GetDecimal("SELLING_PRICE");
                        if (OraDataReader.Instance.IsDBNull("COST_PRICE")) prod.COST_PRICE = OraDataReader.Instance.GetDecimal("COST_PRICE");

                        lstProd.Add(prod);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProd = null;
                throw ex;
            }

            return lstProd;
        }

        public Product GetProduct(string prodID)
        {
            Product prod = null;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_M_PRODUCT" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPROD_SEQ_NO", prodID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prod = new Product();

                        prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prod.MATERIAL_TYPE = OraDataReader.Instance.GetString("MATERIAL_TYPE");
                        prod.PRODUCTION_TYPE = OraDataReader.Instance.GetString("PRODUCTION_TYPE");
                        prod.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        prod.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        prod.UNIT = OraDataReader.Instance.GetString("UNIT");
                        prod.PROD_IMAGE = OraDataReader.Instance.GetBitmap("PROD_IMAGE");
                        prod.REMARK = OraDataReader.Instance.GetString("REMARK");
                        prod.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        //add new field 07-Jun-2011
                        prod.CUST_PROD_NO = OraDataReader.Instance.GetString("CUST_PROD_NO");
                        if (OraDataReader.Instance.IsDBNull("BUYER_PRICE")) prod.BUYER_PRICE = OraDataReader.Instance.GetDecimal("BUYER_PRICE");
                        if (OraDataReader.Instance.IsDBNull("SELLING_PRICE")) prod.SELLING_PRICE = OraDataReader.Instance.GetDecimal("SELLING_PRICE");
                        if (OraDataReader.Instance.IsDBNull("COST_PRICE")) prod.COST_PRICE = OraDataReader.Instance.GetDecimal("COST_PRICE");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                prod = null;
                throw ex;
            }

            return prod;
        }

        public string InsertProduct(ref Product prod, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(17) { ProcedureName = "MASTER_PACK.M_PRODUCT_INS" };

                param.AddParamInOutput(0, "strPROD_SEQ_NO", OracleDbType.Varchar2, 255, prod.PROD_SEQ_NO); //OracleDbType.Varchar2, prod.PROD_SEQ_NO
                param.AddParamInput(1, "strPRODUCT_NO", prod.PRODUCT_NO);
                param.AddParamInput(2, "strPRODUCT_NAME", prod.PRODUCT_NAME);
                param.AddParamInput(3, "strMATERIAL_TYPE", prod.MATERIAL_TYPE);
                param.AddParamInput(4, "strPRODUCTION_TYPE", prod.PRODUCTION_TYPE);
                param.AddParamInput(5, "strMC_NO", prod.MC_NO);
                param.AddParamInput(6, "strBOX_QTY", prod.BOX_QTY);
                param.AddParamInput(7, "strUNIT", prod.UNIT);
                if (prod.PROD_IMAGE != null)
                {
                    param.AddParamBLOBInput(8, "strPROD_IMAGE", OracleDbType.Blob, this.BitmapToByteArray(prod.PROD_IMAGE)); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                else
                {
                    param.AddParamInput(8, "strPROD_IMAGE", DBNull.Value); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                param.AddParamInput(9, "strREMARK", prod.REMARK);

                param.AddParamInput(10, "strREC_STAT", (prod.REC_STAT ? "Y" : "N"));
                param.AddParamInput(11, "strUSER_ID", userid);

                //add new parameter on 07-Jun-2011
                param.AddParamInput(12, "strCUST_PROD_NO", prod.CUST_PROD_NO);
                param.AddParamInput(13, "strBUYER", prod.BUYER_PRICE);
                param.AddParamInput(14, "strSELLING", prod.SELLING_PRICE);
                param.AddParamInput(15, "strCOST", prod.COST_PRICE);

                param.AddParamOutput(16, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString prodSeq = (OracleString)param.ReturnValue(0);

                OracleString result = (OracleString)param.ReturnValue(16);

                if (!result.IsNull)
                {
                    prod.PROD_SEQ_NO = prodSeq.Value;
                    resultMsg = result.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        public string UpdateProduct(Product prod, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(17) { ProcedureName = "MASTER_PACK.M_PRODUCT_UPD" };

                param.AddParamInput(0, "strPROD_SEQ_NO", prod.PROD_SEQ_NO);
                param.AddParamInput(1, "strPRODUCT_NO", prod.PRODUCT_NO);
                param.AddParamInput(2, "strPRODUCT_NAME", prod.PRODUCT_NAME);
                param.AddParamInput(3, "strMATERIAL_TYPE", prod.MATERIAL_TYPE);
                param.AddParamInput(4, "strPRODUCTION_TYPE", prod.PRODUCTION_TYPE);
                param.AddParamInput(5, "strMC_NO", prod.MC_NO);
                param.AddParamInput(6, "strBOX_QTY", prod.BOX_QTY);
                param.AddParamInput(7, "strUNIT", prod.UNIT);
                if (prod.PROD_IMAGE != null)
                {
                    param.AddParamBLOBInput(8, "strPROD_IMAGE", OracleDbType.Blob, this.BitmapToByteArray(prod.PROD_IMAGE)); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                else
                {
                    param.AddParamInput(8, "strPROD_IMAGE", DBNull.Value); //this.BitmapToByteArray(prod.PROD_IMAGE)
                }
                //param.AddParamBLOBInput(8, "strPROD_IMAGE", OracleDbType.Blob, this.BitmapToByteArray(prod.PROD_IMAGE));
                param.AddParamInput(9, "strREMARK", prod.REMARK);

                param.AddParamInput(10, "strREC_STAT", (prod.REC_STAT ? "Y" : "N"));
                param.AddParamInput(11, "strUSER_ID", userid);

                //add new parameter on 07-Jun-2011
                param.AddParamInput(12, "strCUST_PROD_NO", prod.CUST_PROD_NO);
                param.AddParamInput(13, "strBUYER", prod.BUYER_PRICE);
                param.AddParamInput(14, "strSELLING", prod.SELLING_PRICE);
                param.AddParamInput(15, "strCOST", prod.COST_PRICE);

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

        private byte[] BitmapToByteArray(System.Drawing.Bitmap bitmapIn)
        {
            MemoryStream ms = new MemoryStream();
            bitmapIn.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }

        public string DeleteProduct(string prodID, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.M_PRODUCT_DEL" };

                param.AddParamInput(0, "strPROD_SEQ_NO", prodID);
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

        public List<Product> LovProductList(string prdType, string partyid, string findValue)
        {
            List<Product> lstProduct = null;
            Product product;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "LOV_PACK.GET_PRODUCT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strPROD_SEQ_NO", DBNull.Value);
                param.AddParamInput(3, "strPROD_TYPE", prdType);
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

        public Product LovProduct(string prdType, string partyid, string prdSeq)
        {
            Product product = null;

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "LOV_PACK.GET_PRODUCT_LIST" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strPROD_SEQ_NO", prdSeq);
                param.AddParamInput(3, "strPROD_TYPE", prdType);
                param.AddParamInput(4, "strPARTY_ID", partyid);

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

        //public Product GetProductNo(string prodNo, string prodType)
        //{
        //    Product prod = null;

        //    try
        //    {
        //        ProcParam param = new ProcParam(3) { ProcedureName = "LOV_PACK.GET_PRODUCT_NO" };
        //        param.AddParamRefCursor(0, "io_cursor");
        //        param.AddParamInput(1, "strPROD_NO", prodNo);
        //        param.AddParamInput(2, "strPROD_TYPE", prodType);

        //        OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

        //        //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

        //        if (OraDataReader.Instance.OraReader.HasRows)
        //        {
        //            OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

        //            while (OraDataReader.Instance.OraReader.Read())
        //            {
        //                prod = new Product();

        //                prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
        //                prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
        //                prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
        //                prod.MATERIAL_NAME = OraDataReader.Instance.GetString("MATERIAL_NAME");
        //            }
        //        }

        //        // always call Close when done reading.
        //        OraDataReader.Instance.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        prod = null;
        //        throw ex;
        //    }

        //    return prod;
        //}

        public Product GetProductNo(string prodNo, string prodType, string partyid)
        {
            Product prod = null;

            try
            {
                ProcParam param = new ProcParam(4) { ProcedureName = "LOV_PACK.GET_PRODUCT_NO" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROD_NO", prodNo);
                param.AddParamInput(2, "strPROD_TYPE", prodType);
                param.AddParamInput(3, "strPARTY_ID", partyid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prod = new Product();

                        prod.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prod.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        prod.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        prod.MATERIAL_NAME = OraDataReader.Instance.GetString("MATERIAL_NAME");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                prod = null;
                throw ex;
            }

            return prod;
        }

        public DataTable GetProductionProcess(string prodSEQ)
        {
            List<ProdProcess> lstProdProcess = null;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MASTER_PACK.GET_M_PROD_PROCESS" };

                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strPROD_SEQ_NO", prodSEQ);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.HasRows)
                {
                    lstProdProcess = new List<ProdProcess>();
                    ProdProcess prodProcess;

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        prodProcess = new ProdProcess();

                        prodProcess.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        prodProcess.PROCESS_NO = OraDataReader.Instance.GetString("PROCESS_NO");
                        prodProcess.PROCESS_NAME = OraDataReader.Instance.GetString("PROCESS_NAME");
                        prodProcess.STEP_NO = OraDataReader.Instance.GetInteger("STEP_NO");
                        prodProcess.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                        prodProcess.FLAG = OraDataReader.Instance.GetInteger("FLAG");

                        lstProdProcess.Add(prodProcess);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProdProcess = null;
            }

            return UtilityBLL.ListToDataTable(lstProdProcess, "M_PROD_PROCESS");
        }

        public void UpdateProductionProcess(List<ProdProcess> lstProdProcess, string userid)
        {
            
            try
            {
                List<ProcParam> paraList = new List<ProcParam>();
                ProcParam procDetail = null;

                foreach (ProdProcess prodPro in lstProdProcess)
                {
                    procDetail = new ProcParam(6);
                    procDetail.ProcedureName = "MASTER_PACK.M_PROD_PROCESS_UPD";

                    procDetail.AddParamInput(0, "strPROD_SEQ_NO", prodPro.PROD_SEQ_NO);
                    procDetail.AddParamInput(1, "strPROCESS_NO", prodPro.PROCESS_NO);
                    procDetail.AddParamInput(2, "strSTEP_NO", prodPro.STEP_NO);  //DEFAULT FOR REPORT ID '001'
                    procDetail.AddParamInput(3, "strREC_STAT ", (prodPro.REC_STAT ? "Y" : "N"));
                    procDetail.AddParamInput(4, "strUSER_ID", userid);
                    procDetail.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255, "OK");

                    paraList.Add(procDetail);
                }


                if (paraList.Count != 0)
                {
                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paraList);
                    //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProductionProcess(List<ProdProcess> lstProdProcess, string strNEW_PROD_SEQ_NO, string userid)
        {
            ProcParam procDetail = null;

            try
            {
                procDetail = new ProcParam(6) { ProcedureName = "MASTER_PACK.M_PROD_PROCESS_UPD" };

                //PROD_SEQ_NO
                procDetail.AddParamInput(0, "strPROD_SEQ_NO", ArrayOf<object>.Create(lstProdProcess.Count, strNEW_PROD_SEQ_NO), OracleDbType.Varchar2);

                //PROCESS_NO
                var arrPROCESS_NO = (from prodP in lstProdProcess
                                     select prodP.PROCESS_NO).ToArray();
                procDetail.AddParamInput(1, "strPROCESS_NO", arrPROCESS_NO, OracleDbType.Varchar2);

                //STEP_NO
                var arrSTEP_NO = (from prodP in lstProdProcess
                                  select (object)prodP.STEP_NO).ToArray();
                procDetail.AddParamInput(2, "strSTEP_NO", arrSTEP_NO, OracleDbType.Int32);

                //REC_STAT
                var arrREC_STAT = (from prodP in lstProdProcess
                                   select prodP.REC_STAT ? "Y" : "N").ToArray();
                procDetail.AddParamInput(3, "strREC_STAT", arrREC_STAT, OracleDbType.Varchar2);

                //USER_ID
                procDetail.AddParamInput(4, "strUSER_ID", ArrayOf<object>.Create(lstProdProcess.Count, userid), OracleDbType.Varchar2);

                //RESULTMSG
                procDetail.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", lstProdProcess.Count);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procDetail, lstProdProcess.Count);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadProductMaster(DataTable dt, string prodType, string userid)
        {
            try
            {
                ProcParam param = new ProcParam(14) { ProcedureName = "MASTER_PACK.UPLOAD_PRODUCT_MASTER" };

                var arrPRODUCT_CODE = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Item ID")).ToArray();
                param.AddParamInput(0, "strPRODUCT_CODE", arrPRODUCT_CODE, OracleDbType.Varchar2);

                var arrPRODUCT_NAME = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Item Description")).ToArray();
                param.AddParamInput(1, "strPRODUCT_NAME", arrPRODUCT_NAME, OracleDbType.Varchar2);

                var arrPRODUCT_NAME_REF = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Description for Sales")).ToArray();
                param.AddParamInput(2, "strPRODUCT_NAME_REF", arrPRODUCT_NAME_REF, OracleDbType.Varchar2);

                var arrPRICE1 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Price 1")).ToArray();
                param.AddParamInput(3, "strPRICE1", arrPRICE1, OracleDbType.Decimal);

                var arrPRICE2 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Price 2")).ToArray();
                param.AddParamInput(4, "strPRICE2", arrPRICE2, OracleDbType.Decimal);

                var arrPRICE3 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Price 3")).ToArray();
                param.AddParamInput(5, "strPRICE3", arrPRICE3, OracleDbType.Decimal);

                var arrPRICE4 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Price 4")).ToArray();
                param.AddParamInput(6, "strPRICE4", arrPRICE4, OracleDbType.Decimal);

                var arrPRICE5 = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Sales Price 5")).ToArray();
                param.AddParamInput(7, "strPRICE5", arrPRICE5, OracleDbType.Decimal);

                var arrSALE_ACC_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("G/L Sales Account")).ToArray();
                param.AddParamInput(8, "strSALE_ACC_NO", arrSALE_ACC_NO, OracleDbType.Varchar2);

                var arrCOS_ACC_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("G/L COGS/Salary Acct")).ToArray();
                param.AddParamInput(9, "strCOS_ACC_NO", arrCOS_ACC_NO, OracleDbType.Varchar2);

                var arrCURRENT_REV_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Revision Number")).ToArray();
                param.AddParamInput(10, "strCURRENT_REV_NO", arrCURRENT_REV_NO, OracleDbType.Varchar2);

                param.AddParamInput(11, "strUSER_ID", ArrayOf<object>.Create(dt.Rows.Count, userid), OracleDbType.Varchar2);

                param.AddParamInput(12, "strPROD_TYPE", ArrayOf<object>.Create(dt.Rows.Count, prodType), OracleDbType.Varchar2);

                param.AddParamOutput(13, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dt.Rows.Count);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param, dt.Rows.Count);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;
                else
                {
                    if (((Oracle.DataAccess.Types.OracleString[])(param.ReturnValue(13)))[0].Value == "OK")
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

        public string UploadProductBom_SAGE50(DataTable dt, string userid)
        {
            try
            {
                ProcParam param = new ProcParam(8) { ProcedureName = "MASTER_PACK.UPLOAD_PURCHASE_ORDER" };

                var arrASSEMBLY_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Assembly ID")).ToArray();
                param.AddParamInput(0, "strASSEMBLY_ID", arrASSEMBLY_ID, OracleDbType.Varchar2);

                var arrASSEMBLY_DESC = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Assembly Description")).ToArray();
                param.AddParamInput(1, "strASSEMBLY_DESC", arrASSEMBLY_DESC, OracleDbType.Varchar2);

                var arrREVISION_NO = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Revision Number")).ToArray();
                param.AddParamInput(2, "strREVISION_NO", arrREVISION_NO, OracleDbType.Decimal);

                var arrCOMPONENT_ID = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Component ID")).ToArray();
                param.AddParamInput(3, "strCOMPONENT_ID", arrCOMPONENT_ID, OracleDbType.Varchar2);

                var arrCOMPONENT_QTY_NEEDED = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Component Qty Needed")).ToArray();
                param.AddParamInput(4, "strCOMPONENT_QTY_NEEDED", arrCOMPONENT_QTY_NEEDED, OracleDbType.Decimal);

                var arrCOMPONENT_DESC = dt.AsEnumerable().ToList().Select(r => r.Field<string>("Component Description")).ToArray();
                param.AddParamInput(5, "strCOMPONENT_DESC", arrCOMPONENT_DESC, OracleDbType.Varchar2);

                param.AddParamInput(6, "strUSER_ID", ArrayOf<object>.Create(dt.Rows.Count, userid), OracleDbType.Varchar2);

                param.AddParamOutput(7, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dt.Rows.Count);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param, dt.Rows.Count);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;
                else
                {
                    if (((Oracle.DataAccess.Types.OracleString[])(param.ReturnValue(7)))[0].Value == "OK")
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

    }
}
