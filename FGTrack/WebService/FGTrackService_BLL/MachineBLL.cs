using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace FGTrackService.BLL
{
    public class MachineBLL : IDisposable
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

        ~MachineBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        private TimeSpan executionTime;

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

        public MachineBLL()
        {
            //Constructer
        }

        public bool CheckExistMachine(string mcNo)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_VER.CHK_EXISTS_MC" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strNo", mcNo);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal objResult = (OracleDecimal)procPara.ReturnValue(0);


                if (!objResult.IsNull)
                {
                    isValid = (objResult.ToInt32() > 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public bool Press_CheckExistMachine(string mcNo)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_PRESS.CHK_EXISTS_MC" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strNo", mcNo);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal objResult = (OracleDecimal)procPara.ReturnValue(0);


                if (!objResult.IsNull)
                {
                    isValid = (objResult.ToInt32() > 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public bool Horizontal_CheckExistMachine(string mcNo)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_HOZ.CHK_EXISTS_MC" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strNo", mcNo);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal objResult = (OracleDecimal)procPara.ReturnValue(0);


                if (!objResult.IsNull)
                {
                    isValid = (objResult.ToInt32() > 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public bool Tampo_CheckExistMachine(string mcNo)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_TAMPO.CHK_EXISTS_MC" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strNo", mcNo);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal objResult = (OracleDecimal)procPara.ReturnValue(0);


                if (!objResult.IsNull)
                {
                    isValid = (objResult.ToInt32() > 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public bool Assembly_CheckExistMachine(string mcNo)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_PACK_ASY.CHK_EXISTS_MC" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strNo", mcNo);


                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleDecimal objResult = (OracleDecimal)procPara.ReturnValue(0);


                if (!objResult.IsNull)
                {
                    isValid = (objResult.ToInt32() > 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        //public string GetPartyName(string partyID)
        //{
        //    try
        //    {
        //        ProcParam param = new ProcParam(2);
        //        param.ProcedureName = "PACKAGE_NAME.PROCEDURE_NAME";
        //        //if param have parameter
        //        param.AddParamReturn(0, "ReturnValue", OracleDbType.NVarchar2, 120);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
