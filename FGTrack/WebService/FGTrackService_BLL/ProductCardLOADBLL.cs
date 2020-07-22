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
    public class ProductCardLOADBLL : IDisposable
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

        ~ProductCardLOADBLL()
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

        public ProductCardLOADBLL()
        {
            //Constructer
        }

        public LoadQty GetLoadInfo(string loadNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            LoadQty loadQty = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_FG.GET_LOADING_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strLOADING_NO", loadNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        loadQty = new LoadQty();

                        loadQty.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        loadQty.LOADED_BOX = OraDataReader.Instance.GetInteger("LOADED_BOX");
                        loadQty.LOADED_QTY = OraDataReader.Instance.GetInteger("LOADED_QTY");
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

            return loadQty;
        }

        public LoadQty FGPress_GetLoadInfo(string loadNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            LoadQty loadQty = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_FG_PRESS.GET_LOADING_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strLOADING_NO", loadNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        loadQty = new LoadQty();

                        loadQty.LOADING_NO = OraDataReader.Instance.GetString("LOADING_NO");
                        loadQty.LOADED_BOX = OraDataReader.Instance.GetInteger("LOADED_BOX");
                        loadQty.LOADED_QTY = OraDataReader.Instance.GetInteger("LOADED_QTY");
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

            return loadQty;
        }

        public ProductCard GetUpdatePCLoading(string loadNo, string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_FG.GET_UPD_PC_LOADING" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strLOADING_NO", loadNo);
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
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("LOADED_BOX");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("LOADED_QTY");
                    }
                    
                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(3);
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

        public Pallet GetUpdatePalletLoading(string loadNo, string palletno, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            Pallet pallet = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_FG.GET_UPD_PALLET_LOADING" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strPALLET_NO", palletno);
                procPara.AddParamInput(2, "strLOADING_NO", loadNo);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pallet = new Pallet();

                        pallet.PALLET_NO = OraDataReader.Instance.GetString("PALLET_NO");
                        pallet.SO_NO = OraDataReader.Instance.GetString("SO_NO");
                        pallet.PALLET_SEQ = OraDataReader.Instance.GetInteger("PALLET_SEQ");
                        pallet.PALLET_TOTAL = OraDataReader.Instance.GetInteger("PALLET_TOTAL");
                        pallet.PALLET_STATUS = OraDataReader.Instance.GetString("PALLET_STATUS");
                        pallet.PALLET_BOX = OraDataReader.Instance.GetInteger("PALLET_BOX");
                        pallet.PALLET_PCS = OraDataReader.Instance.GetInteger("PALLET_PCS");
                        pallet.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        pallet.ETA = OraDataReader.Instance.GetString("ETA");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(3);
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

            return pallet;
        }

        public ProductCard FGPress_GetUpdatePCLoading(string loadNo, string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_FG_PRESS.GET_UPD_PC_LOADING" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamInput(2, "strLOADING_NO", loadNo);
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
                        pcCard.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.SHIFT = OraDataReader.Instance.GetString("SHIFT");
                        pcCard.QTY = OraDataReader.Instance.GetInteger("QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("LOADED_BOX");
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("LOADED_QTY");
                    }

                }
                else
                {
                    OracleString result = (OracleString)procPara.ReturnValue(3);
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

        public string UpdateLoadingSeal(string loadNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_FG.UPD_LOADING_SEAL" };
                procPara.AddParamInput(0, "strLOADING_NO", loadNo);
                procPara.AddParamOutput(1, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(2, "strUser_id", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(1);

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

        public string FGPress_UpdateLoadingSeal(string loadNo, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_FG_PRESS.UPD_LOADING_SEAL" };
                procPara.AddParamInput(0, "strLOADING_NO", loadNo);
                procPara.AddParamOutput(1, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(2, "strUser_id", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(1);

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
