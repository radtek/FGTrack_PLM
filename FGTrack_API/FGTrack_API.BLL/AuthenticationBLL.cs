using HTN.BITS.FGTRACK.BEL;
using HTN.BITS.FGTRACK.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN.BITS.FGTRACK.BLL
{
    public  class AuthenticationBLL : IDisposable
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

        ~AuthenticationBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Variable Member

        private bool IsDisposed = false;
        private int userid;

        #endregion

        #region Property Member

        public int UserID
        {
            get
            {
                return this.userid;
            }
        }

        #endregion

        #region Method Member

        public bool CheckApiAuthen(string username, string password, out string userRole)
        {
            userRole = string.Empty;
            bool result = false;

            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "ADMINISTRATOR_PACK.CHECK_AUTHENTICATION_API" };

                procPara.AddParamInput(0, "strUSER_USR", username);
                procPara.AddParamInput(1, "strUSER_PWD", password);
                procPara.AddParamOutput(2, "strUSER_ID", OracleDbType.Varchar2, 30);
                procPara.AddParamOutput(3, "strUSER_NAME", OracleDbType.Varchar2, 30);
                procPara.AddParamOutput(4, "strROLE", OracleDbType.Varchar2, 30);
                procPara.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultMsg = (OracleString)procPara.ReturnValue(5);

                if (resultMsg == "OK")
                {
                    userRole = ((OracleString)procPara.ReturnValue(4)).ToString();
                    result = true;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public string CheckValidationUser(string userid, string ipaddress, string serialno, string scanversion)
        {
            string result = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_MAT_CHK_PACK.USER_VALIDATION" };
                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Raw, 255);
                procPara.AddParamInput(1, "strUSER_ID", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

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

        #endregion
    }
}
