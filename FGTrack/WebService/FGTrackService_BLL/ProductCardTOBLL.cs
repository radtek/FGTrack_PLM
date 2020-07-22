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
    public class ProductCardTOBLL : IDisposable
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

        ~ProductCardTOBLL()
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

        public ProductCardTOBLL()
        {
            //Constructer
        }
        public TransferOrderInfo GetTOInfo(string toNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            TransferOrderInfo tOrder = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_MTST_VER.GET_TO_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strTO_NO", toNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        tOrder = new TransferOrderInfo();

                        tOrder.TO_NO = OraDataReader.Instance.GetString("TO_NO");
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

            return tOrder;
        }

        public ProductCard GetUpdatePC_MTSTIn(string toNo, string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_MTST_VER.GET_UPD_PC_FG_IN" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strTO_NO", toNo);
                procPara.AddParamInput(2, "strSERIAL_NO", serialNo);
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
                        //use for SCN_QTY
                        pcCard.ASG_NG = OraDataReader.Instance.GetInteger("SCN_QTY");
                        //use for SCN_BOX
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("SCN_BOX");
                        //use for DO_QTY
                        pcCard.BOX_QTY = OraDataReader.Instance.GetInteger("DO_QTY");
                        //use for DO_BOX
                        pcCard.BOX_SCANNED = OraDataReader.Instance.GetInteger("DO_BOX");
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

        public ProductCard GetUpdatePC_MTSTOut(string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_MTST_VER.GET_UPD_PC_FG_OUT" };
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

    }
}
