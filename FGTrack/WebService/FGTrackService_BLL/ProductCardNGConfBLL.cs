using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace FGTrackService.BLL
{
    public class ProductCardNGConfBLL : IDisposable
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

        ~ProductCardNGConfBLL()
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

        public ProductCardNGConfBLL()
        {
            //Constructer
        }

        public ProductCard GetPC_NGInfo(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_VER.GET_PC_QC_NG" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(2);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public ProductCard Press_GetPC_NGInfo(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_PRESS.GET_PC_QC_NG" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(2);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public ProductCard Horizontal_GetPC_NGInfo(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_HOZ.GET_PC_QC_NG" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(2);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public ProductCard Tampo_GetPC_NGInfo(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_TAMPO.GET_PC_QC_NG" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(2);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public string UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_VER.UPD_QC_NG_CONF" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strMODE", mode);
                procPara.AddParamInput(2, "strQTY", qty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(4);

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

        public string Press_UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_PRESS.UPD_QC_NG_CONF" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strMODE", mode);
                procPara.AddParamInput(2, "strQTY", qty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(4);

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

        public string Horizontal_UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_HOZ.UPD_QC_NG_CONF" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strMODE", mode);
                procPara.AddParamInput(2, "strQTY", qty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(4);

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

        public string Tampo_UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_TAMPO.UPD_QC_NG_CONF" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strMODE", mode);
                procPara.AddParamInput(2, "strQTY", qty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(4);

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

        public ProductCard Assembly_GetPC_NGInfo(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_ASY.GET_PC_QC_NG" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(2);
                    if (!result.IsNull)
                    {
                        resultMessage = result.Value;
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pcCard;
        }

        public string Assembly_UpdateReplenishConfirm(string serialNo, string mode, int qty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_ASY.UPD_QC_NG_CONF" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strMODE", mode);
                procPara.AddParamInput(2, "strQTY", qty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(4);

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
