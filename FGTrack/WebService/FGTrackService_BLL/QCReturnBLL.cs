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
    public class QCReturnBLL : IDisposable
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

        ~QCReturnBLL()
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

        public QCReturnBLL()
        {
            //Constructer
        }

        public QCReturn GetQcReturnInfo(string qcReturnNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            QCReturn qcReturn = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_RETRUN_PACK.GET_FG_RETURN" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strRT_NO", qcReturnNo);
                procPara.AddParamOutput(2, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(3, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        qcReturn = new QCReturn();

                        qcReturn.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        qcReturn.RT_NO = OraDataReader.Instance.GetString("RT_NO");
                        qcReturn.NO_OF_LABEL = OraDataReader.Instance.GetInteger("NO_OF_LABEL");
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

            return qcReturn;
        }

        public ProductCard GetProductCardInfo(string qcReturnNo, string serialNo, string userid, out string resultMessage)
        {
            resultMessage = string.Empty;
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(5);
                procPara.ProcedureName = "SCANNER_RETRUN_PACK.GET_PC_FG_RETURN";
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strRT_NO", qcReturnNo);
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

        public string UpdateReturnProductCard(string qcReturnNo, string serialNo, string userid, out int totalBox)
        {
            totalBox = -1;
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5);
                procPara.ProcedureName = "SCANNER_RETRUN_PACK.UPD_PC_FG_RETURN";
                procPara.AddParamInput(0, "strRT_NO", qcReturnNo);
                procPara.AddParamInput(1, "strSERIAL_NO", serialNo);
                procPara.AddParamOutput(2, "strTOTAL_BOX", OracleDbType.Decimal, 255);
                procPara.AddParamOutput(3, "resultmsg", OracleDbType.Varchar2, 255);
                procPara.AddParamInput(4, "strUser_id", userid);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                OracleString result = (OracleString)procPara.ReturnValue(3);
                OracleDecimal resultDB = (OracleDecimal)procPara.ReturnValue(2);

                if (!result.IsNull)
                {
                    resultMsg = result.Value;
                    totalBox = resultDB.ToInt32();

                }
            }
            catch (Exception ex)
            {
                totalBox = -1;
                throw ex;
            }

            return resultMsg;
        }

    }
}
