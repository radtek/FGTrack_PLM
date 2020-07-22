using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HTN.BITS.BLL.PLASESS
{
    public class MaterialTypeBLL : IDisposable
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

        ~MaterialTypeBLL()
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

        public MaterialTypeBLL()
        {
            //constructer
        }

        public List<MaterialType> GetMTLTypeList(string findValue)
        {
            List<MaterialType> lstMtltype = null;
            MaterialType mtlType;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_MTL_TYPE";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", findValue);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstMtltype = new List<MaterialType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtlType = new MaterialType();

                        mtlType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        mtlType.NAME = OraDataReader.Instance.GetString("NAME");
                        mtlType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        mtlType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstMtltype.Add(mtlType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstMtltype = null;
                throw ex;
            }

            return lstMtltype;
        }

        public MaterialType GetMTLType(string mtltypeID)
        {
            MaterialType mtlType = null;

            try
            {
                ProcParam param = new ProcParam(3);
                param.ProcedureName = "MASTER_PACK.GET_M_MTL_TYPE";
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", mtltypeID);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtlType = new MaterialType();

                        mtlType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        mtlType.NAME = OraDataReader.Instance.GetString("NAME");
                        mtlType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        mtlType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                mtlType = null;
                throw ex;
            }

            return mtlType;
        }

        public string InsertMTLType(MaterialType mtlType, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(6);

                param.ProcedureName = "MASTER_PACK.M_MTL_TYPE_INS";

                param.AddParamInput(0, "strSEQ_NO", mtlType.SEQ_NO);
                param.AddParamInput(1, "strNAME", mtlType.NAME);
                param.AddParamInput(2, "strREMARK", mtlType.REMARK);
                param.AddParamInput(3, "strREC_STAT", (mtlType.REC_STAT ? "Y" : "N"));
                param.AddParamInput(4, "strUSER_ID", userid);

                param.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                //this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(5);

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

        public string UpdateMTLType(MaterialType mtlType, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(6);

                param.ProcedureName = "MASTER_PACK.M_MTL_TYPE_UPD";

                param.AddParamInput(0, "strSEQ_NO", mtlType.SEQ_NO);
                param.AddParamInput(1, "strNAME", mtlType.NAME);
                param.AddParamInput(2, "strREMARK", mtlType.REMARK);
                param.AddParamInput(3, "strREC_STAT", (mtlType.REC_STAT ? "Y" : "N"));
                param.AddParamInput(4, "strUSER_ID", userid);

                param.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)param.ReturnValue(5);

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

        public string DeleteMTLType(string mtlTypeNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam param = new ProcParam(3);

                param.ProcedureName = "MASTER_PACK.M_MTL_TYPE_DEL";

                param.AddParamInput(0, "strSEQ_NO", mtlTypeNo);
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
    }
}
