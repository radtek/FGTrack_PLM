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
    public class AdministratorBLL : IDisposable
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

        ~AdministratorBLL()
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

        public Version GetLastestVersion(string curVersion)
        {
            Version resultVersion = Version.Parse(curVersion);

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "ADMINISTRATOR_PACK.GET_APP_VERSION" };
                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 10);
                procPara.AddParamInput(1, "strCURV", resultVersion.ToString());

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultDB = (OracleString)procPara.ReturnValue(0);

                if (!resultDB.IsNull)
                {
                    resultVersion = Version.Parse(resultDB.ToString());
                }
            }
            catch (Exception ex)
            {
                resultVersion = Version.Parse(curVersion);
            }

            return resultVersion;
        }

        #endregion
    }
}
