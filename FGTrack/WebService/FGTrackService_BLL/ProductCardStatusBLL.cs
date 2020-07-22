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
    public class ProductCardStatusBLL : IDisposable
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

        ~ProductCardStatusBLL()
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

        public ProductCardStatusBLL()
        {
            //Constructer
        }

        public ProductCard_Status GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_VER.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if(!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

        public ProductCardStatusFG GetProductCardStatusFG(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCardStatusFG pcCardfg = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_FG.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCardfg = new ProductCardStatusFG();

                        pcCardfg.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCardfg.WH = OraDataReader.Instance.GetString("WH");
                        pcCardfg.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCardfg.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCardfg.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCardfg.STATUS = OraDataReader.Instance.GetString("STATUS");
                        pcCardfg.PROCESS_NO = OraDataReader.Instance.GetString("PROCESS_NO");
                        pcCardfg.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCardfg.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCardfg.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCardfg.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        pcCardfg.ORI_LABEL = OraDataReader.Instance.GetString("ORI_LABEL");
                        pcCardfg.BREAK_QTY = OraDataReader.Instance.GetInteger("BREAK_QTY");
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
                pcCardfg = null;
                throw ex;
            }

            return pcCardfg;
        }

        public ProductCard_Status Press_GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_PRESS.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

        public ProductCard_Status Horizontal_GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_HOZ.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

        public ProductCard_Status Tampo_GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_TAMPO.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

        public ProductCardStatusFG FGPress_GetProductCardStatusFG(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCardStatusFG pcCardfg = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_FG_PRESS.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCardfg = new ProductCardStatusFG();

                        pcCardfg.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCardfg.WH = OraDataReader.Instance.GetString("WH");
                        pcCardfg.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCardfg.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCardfg.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCardfg.STATUS = OraDataReader.Instance.GetString("STATUS");
                        pcCardfg.PROCESS_NO = OraDataReader.Instance.GetString("PROCESS_NO");
                        pcCardfg.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCardfg.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCardfg.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCardfg.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        pcCardfg.ORI_LABEL = OraDataReader.Instance.GetString("ORI_LABEL");
                        pcCardfg.BREAK_QTY = OraDataReader.Instance.GetInteger("BREAK_QTY");
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
                pcCardfg = null;
                throw ex;
            }

            return pcCardfg;
        }

        public ProductCard_Status MtstVertical_GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_MTST_VER.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

        public ProductCard_Status Assembly_GetProductCardStatus(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard_Status pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_ASY.GET_PC_STATUS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard_Status();

                        pcCard.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.STATUS = OraDataReader.Instance.GetString("STATUS");
                        if (!OraDataReader.Instance.IsDBNull("PROCESS_DATE"))
                            pcCard.PROCESS_DATE = OraDataReader.Instance.GetDateTime("PROCESS_DATE");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.REP_QTY = OraDataReader.Instance.GetInteger("REP_QTY");
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
                pcCard = null;
                throw ex;
            }

            return pcCard;
        }

    }
}
