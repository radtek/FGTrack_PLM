using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace FGTrackService.BLL
{
    public class UserBLL : IDisposable
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

        ~UserBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        private TimeSpan executionTime;
        private string userid;

        #endregion

        #region Property Member

        public TimeSpan ExecutionTime
        {
            get
            {
                return this.executionTime;
            }
        }

        #endregion

        public string FGPress_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_FG_PRESS.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string FGWHS_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_FG.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Horizontal_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_HOZ.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string MtstTampo_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_MTST_TAMPO.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string MtstVertical_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_MTST_VER.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Press_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_PRESS.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Tampo_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_TAMPO.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Vertical_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_VER.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Material_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_MAT_PACK.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string Assembly_CheckValidationUser(string userid)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_ASY.USER_VALIDATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                //OracleString objResult = (OracleString)procPara.ReturnValue(0);
                OracleBinary objResult = (OracleBinary)procPara.ReturnValue(0);

                if (!objResult.IsNull)
                {
                    result = UtilityBLL.GetReturnRawData(objResult);
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
