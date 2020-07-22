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
    public class ProductCardBLL : IDisposable
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

        ~ProductCardBLL()
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

        public ProductCardBLL()
        {
            //Constructer
        }
      
        public ProductCard GetProductCardInfo(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_VER.GET_PC_INFO_PROCESS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public ProductCard Press_GetProductCardInfo(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_PRESS.GET_PC_INFO_PROCESS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public ProductCard Horizontal_GetProductCardInfo(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_HOZ.GET_PC_INFO_PROCESS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public ProductCard Tampo_GetProductCardInfoOri(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_TAMPO.GET_PC_INFO_PROCESS_ORI" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public ProductCard Tampo_GetProductCardInfo(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_TAMPO.GET_PC_INFO_PROCESS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        pcCard.ORI_LABEL = OraDataReader.Instance.GetString("ORI_LABEL");
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public string UpdProductCard(string serialNo, string mcNo, string processMode, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(6);
                procPara.ProcedureName = "SCANNER_PACK_VER.UPD_PC_INFO_PROCESS";
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strPROCESS", processMode);
                procPara.AddParamInput(2, "strMC_NO", mcNo);
                procPara.AddParamInput(3, "strQty", nQty);
                procPara.AddParamOutput(4, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(5, "strUser_id", userid);
                
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

        public string Press_UpdProductCard(string serialNo, string mcNo, string processMode, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "SCANNER_PACK_PRESS.UPD_PC_INFO_PROCESS" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strPROCESS", processMode);
                procPara.AddParamInput(2, "strMC_NO", mcNo);
                procPara.AddParamInput(3, "strQty", nQty);
                procPara.AddParamOutput(4, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(5, "strUser_id", userid);


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

        public string Horizontal_UpdProductCard(string serialNo, string mcNo, string processMode, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "SCANNER_PACK_HOZ.UPD_PC_INFO_PROCESS" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strPROCESS", processMode);
                procPara.AddParamInput(2, "strMC_NO", mcNo);
                procPara.AddParamInput(3, "strQty", nQty);
                procPara.AddParamOutput(4, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(5, "strUser_id", userid);


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

        public string Tampo_UpdProductCard(string serialNo, string serialNoNew, string mcNo, string processMode, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(7) { ProcedureName = "SCANNER_PACK_TAMPO.UPD_PC_INFO_PROCESS" };
                procPara.AddParamInput(0, "strSERIAL_NO_ORI", serialNo);
                procPara.AddParamInput(1, "strSERIAL_NO", serialNoNew);
                procPara.AddParamInput(2, "strPROCESS", processMode);
                procPara.AddParamInput(3, "strMC_NO", mcNo);
                procPara.AddParamInput(4, "strQty", nQty);
                procPara.AddParamOutput(5, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(6, "strUser_id", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(5);

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

        public ProductCard Assembly_GetProductCardInfo(string serialNo, string mode, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_ASY.GET_PC_INFO_PROCESS" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strPROCESS", mode);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

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
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("TOT_BOX");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                    }

                }
                else
                {
                    OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                    if (!resultDB.IsNull)
                    {
                        resultMessage = resultDB.Value;
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

        public string Assembly_UpdProductCard(string serialNo, string mcNo, string processMode, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "SCANNER_PACK_ASY.UPD_PC_INFO_PROCESS" };
                procPara.AddParamInput(0, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(1, "strPROCESS", processMode);
                procPara.AddParamInput(2, "strMC_NO", mcNo);
                procPara.AddParamInput(3, "strQty", nQty);
                procPara.AddParamOutput(4, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(5, "strUser_id", userid);


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
