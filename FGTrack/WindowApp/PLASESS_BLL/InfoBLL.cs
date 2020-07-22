using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HTN.BITS.BLL.PLASESS
{
    public class InfoBLL : IDisposable
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

        ~InfoBLL()
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

        public InfoBLL()
        {
        }

        public int ProductBoxQty(string prodSeq)
        {
            try
            {
                ProcParam param = new ProcParam(2);
                param.ProcedureName = "INFO.PRODUCT_BOX_QTY";
                param.AddParamReturn(0, "ReturnValue", OracleDbType.Decimal, 100);
                param.AddParamInput(1, "strNo", prodSeq);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(param);

                OracleDecimal result = (OracleDecimal)param.ReturnValue(0);

                return result.ToInt32();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
