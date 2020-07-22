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
    public class BOMBLL : IDisposable
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

        ~BOMBLL()
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

        public BOMBLL()
        {
           
        }


        public List<BOM> GetBOMList(string findAll)
        {
            List<BOM> lstBOM = null;
            BOM bom;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MASTER_PACK.GET_M_BOM" };

                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strFindAll", findAll);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstBOM = new List<BOM>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        bom = new BOM();

                        bom.CUST_PROD_NO = OraDataReader.Instance.GetString("CUST_PROD_NO");
                        bom.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        bom.BOM_QTY = OraDataReader.Instance.GetDecimal("BOM_QTY");
                        bom.N_USER = OraDataReader.Instance.GetString("N_USER_ID");
                        bom.N_DATE = OraDataReader.Instance.GetDateTime("N_USER_DATE");

                        lstBOM.Add(bom);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstBOM = null;
                throw ex;
            }

            return lstBOM;
        }

        public string[] CheckIsBOMExists(string strProducts, string strMaterials)
        {
            string[] test = new string[3];

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "MASTER_PACK.CHECK_IS_BOM_EXISTS" };

                param.AddParamInput(0, "strPROD_COLLECTION", strProducts);
                param.AddParamInput(1, "strMAT_COLLECTION", strMaterials);
                param.AddParamOutput(2, "strPROD_OUT", OracleDbType.Varchar2, 255);
                param.AddParamOutput(3, "strMAT_OUT", OracleDbType.Varchar2, 255);
                param.AddParamOutput(4, "strOUT_BOM_EXISTS", OracleDbType.Varchar2, 255);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                test[0] = param.ReturnValue(2).GetHashCode() == 0 ? "" : ((OracleString)param.ReturnValue(2)).Value;
                test[1] = param.ReturnValue(3).GetHashCode() == 0 ? "" : ((OracleString)param.ReturnValue(3)).Value;
                test[2] = param.ReturnValue(4).GetHashCode() == 0 ? "" : ((OracleString)param.ReturnValue(4)).Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return test;
        }

        public string InsertBOM(DataTable dt, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                string productNo = dt.Rows[0][0].ToString().Trim();
                IEnumerable<string> productList = dt.AsEnumerable().ToList().Select(r => r.Field<string>("PRODUCT_NO")).Distinct();

                foreach (string product in productList)
                {
                    DataRow[] dr = dt.Select("PRODUCT_NO = '" + product + "'");


                    ProcParam paramDel = new ProcParam(2) { ProcedureName = "MASTER_PACK.DEL_M_BOM" };

                    paramDel.AddParamInput(0, "strPRODUCT_NO", product);
                    paramDel.AddParamOutput(1, "RESULTMSG", OracleDbType.Varchar2, 255);

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel);

                    ProcParam param = new ProcParam(5) { ProcedureName = "MASTER_PACK.BOM_INS" };

                    var arrProductNo = dr.Select(r => r.Field<string>("PRODUCT_NO")).ToArray();
                    param.AddParamInput(0, "strPRODUCT_NO", arrProductNo, OracleDbType.Varchar2);

                    var arrMatNo = dr.Select(r => r.Field<string>("MATERIAL_CODE")).ToArray();
                    param.AddParamInput(1, "strMATERIAL_NO", arrMatNo, OracleDbType.Varchar2);

                    var arrBomQty = (from bomCol in dr
                                     select (object)bomCol.Field<decimal>("BOM_QTY")).ToArray();
                    param.AddParamInput(2, "strBOM_QTY", arrBomQty, OracleDbType.Decimal);

                    param.AddParamInput(3, "strUSER", ArrayOf<object>.Create(dr.Count(), userid), OracleDbType.NVarchar2);

                    param.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dr.Count());

                    //GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHdr, paramDtl, dr.Count(), 0, 0);

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(param, dr.Count());

                    if (GlobalDB.Instance.LastException != null)
                        throw GlobalDB.Instance.LastException;
                    else
                    {
                        //OracleString lastArrivalNo = (OracleString)paramHdr.ReturnValue(0);
                        if (((Oracle.DataAccess.Types.OracleString[])(param.ReturnValue(4)))[0].Value == "OK")
                        {
                            resultMsg = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }
    }
}
