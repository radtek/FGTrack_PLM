using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.ORACLE.DAL;
using Oracle.DataAccess.Types;

namespace HTN.BITS.ORACLE.BLL
{
    public class AdminBLL : IDisposable
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

        ~AdminBLL()
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

        #region Method Member

        public bool IsAuthenticationPass(string login, string password)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "ADM_PKG.CHECK_AUTHENTICATION_USER" };
                procPara.AddParamReturn(0, "ReturnValue", Oracle.DataAccess.Client.OracleDbType.Varchar2, 30);
                procPara.AddParamInput(1, "strLogin", login);
                procPara.AddParamInput(2, "strPwd", password);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

                OracleString result = (OracleString)procPara.ReturnValue(0);

                if (!result.IsNull)
                {
                    isValid = !string.IsNullOrEmpty(result.Value);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public string RegisterEStockUser(string userlogin, string userpass, out string cust_uid)
        {
            cust_uid = string.Empty;
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "ADM_PKG.CHECK_AUTHENTICATION_USER" };
                procPara.AddParamReturn(0, "ReturnValue", Oracle.DataAccess.Client.OracleDbType.Varchar2, 30);
                procPara.AddParamInput(1, "strLogin", userlogin);
                procPara.AddParamInput(2, "strPwd", userpass);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

                OracleString result = (OracleString)procPara.ReturnValue(0);

                if (!result.IsNull)
                {
                    resultMsg = "Info.REGISTER_SUCCESS";
                    cust_uid = result.Value;
                }
                else
                {
                    resultMsg = "Error.USER_NOT_REGISTER";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultMsg;
        }

        #endregion
    }
}
