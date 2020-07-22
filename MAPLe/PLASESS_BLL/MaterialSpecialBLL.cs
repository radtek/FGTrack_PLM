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
    public class MaterialSpecialBLL : IDisposable
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

        ~MaterialSpecialBLL()
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

        public MaterialSpecialBLL()
        {
            //constructer
        }


        public List<MaterialSP> GetMaterialSPList(string findAll)
        {
            List<MaterialSP> lstMaterialSP = null;
            MaterialSP material;

            try
            {
                ProcParam param = new ProcParam(2) { ProcedureName = "MASTER_PACK.GET_M_MATERIAL_SP" };

                param.AddParamRefCursor(0, "IO_CURSOR");
                param.AddParamInput(1, "strFindAll", findAll);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMaterialSP = new List<MaterialSP>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        material = new MaterialSP();

                        material.SP_GROUP = OraDataReader.Instance.GetString("SP_GROUP");
                        material.CUST_PROD_NO = OraDataReader.Instance.GetString("CUST_PROD_NO");
                        material.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        material.N_USER = OraDataReader.Instance.GetString("N_USER");
                        material.N_DATE = OraDataReader.Instance.GetDateTime("N_DATE");
                        material.U_USER = OraDataReader.Instance.GetString("U_USER");

                        try
                        {
                            material.U_DATE = OraDataReader.Instance.GetDateTime("U_DATE");
                        }
                        catch (Exception)
                        {
                            material.U_DATE = null;
                        }

                        lstMaterialSP.Add(material);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMaterialSP = null;
                throw ex;
            }

            return lstMaterialSP;
        }

        public string[] CheckIsMatSPExists(string strProducts, string strMaterials)
        {
            string[] test = new string[3];

            try
            {
                ProcParam param = new ProcParam(5) { ProcedureName = "MASTER_PACK.CHECK_IS_MAT_SP_EXISTS" };

                param.AddParamInput(0, "strPROD_COLLECTION", strProducts);
                param.AddParamInput(1, "strMAT_COLLECTION", strMaterials);
                param.AddParamOutput(2, "strPROD_OUT", OracleDbType.Varchar2, 255);
                param.AddParamOutput(3, "strMAT_OUT", OracleDbType.Varchar2, 255);
                param.AddParamOutput(4, "strOUT_PROD_EXITST_IN_SP", OracleDbType.Varchar2, 255);


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

        public string InsertMaterialSP(DataTable dt, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                string productNo = dt.Rows[0][0].ToString().Trim();
                IEnumerable<string> productList = dt.AsEnumerable().ToList().Select(r => r.Field<string>("PRODUCT_NO")).Distinct();

                foreach (string product in productList)
                {

                    DataRow[] dr = dt.Select("PRODUCT_NO = '" + product + "'");


                    ProcParam paramDel = new ProcParam(2) { ProcedureName = "MASTER_PACK.DEL_M_MATERIAL_SP" };

                    paramDel.AddParamInput(0, "strPRODUCT_NO", product);
                    paramDel.AddParamOutput(1, "RESULTMSG", OracleDbType.Varchar2, 255);

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramDel);

                    ProcParam paramHdr = new ProcParam(1) { ProcedureName = "MASTER_PACK.GET_MAT_SP_GROUP_NO" };
                    paramHdr.AddParamOutput(0, "strMAT_SP_GROUP_NO", Oracle.DataAccess.Client.OracleDbType.NVarchar2, 255);

                    ProcParam paramDtl = new ProcParam(5) { ProcedureName = "MASTER_PACK.MATERIAL_SP_INS" };

                    paramDtl.AddParamInput(0, "strMAT_SP_GROUP_NO", ArrayOf<object>.Create(dt.Rows.Count, String.Empty), OracleDbType.NVarchar2);

                    var arrProductNo = dr.Select(r => r.Field<string>("PRODUCT_NO")).ToArray();
                    paramDtl.AddParamInput(1, "strPRODUCT_NO", arrProductNo, OracleDbType.Varchar2);

                    var arrMatNo = dr.Select(r => r.Field<string>("MATERIAL_CODE")).ToArray();
                    paramDtl.AddParamInput(2, "strMATERIAL_NO", arrMatNo, OracleDbType.Varchar2);

                    paramDtl.AddParamInput(3, "strUSER", ArrayOf<object>.Create(dr.Count(), userid), OracleDbType.NVarchar2);

                    paramDtl.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255, "OK", dr.Count());

                    GlobalDB.Instance.DataAc.ExecuteNonQuery(paramHdr, paramDtl, dr.Count(), 0, 0);

                    if (GlobalDB.Instance.LastException != null)
                        throw GlobalDB.Instance.LastException;
                    else
                    {
                        OracleString lastArrivalNo = (OracleString)paramHdr.ReturnValue(0);
                        if (((Oracle.DataAccess.Types.OracleString[])(paramDtl.ReturnValue(4)))[0].Value == "OK")
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
