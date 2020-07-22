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
    public class ReplenishBLL : IDisposable
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

        ~ReplenishBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Variable Member

        private bool IsDisposed = false;
        private ResponseResult response;

        #endregion

        #region Property Member

        #endregion

        #region Method Member

        public ResponseResult CheckJobOrder(string jobno)
        {
            response = new ResponseResult();

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_MAT_CHK_PACK.CHK_JOB_ORDER" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 30);
                procPara.AddParamInput(1, "strJOB_NO", jobno);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var returnValue = (OracleString)procPara.ReturnValue(0);
                var resultMsg = (OracleString)procPara.ReturnValue(2);

                response.Message = resultMsg.ToString();
                response.Data = returnValue.ToString();

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Data = string.Empty;
            }

            return response;
        }

        public ResponseResult CheckMachine(string mcno)
        {
            response = new ResponseResult();

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_MAT_CHK_PACK.CHK_MACHINE" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Varchar2, 30);
                procPara.AddParamInput(1, "strMC_NO", mcno);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultMsg = (OracleString)procPara.ReturnValue(2);

                response.Message = resultMsg.ToString();
                response.Data = string.Empty;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Data = string.Empty;
            }

            return response;
        }

        public ResponseResult StartReplenish(string jobno, string mcno, int noOfBag, string userid)
        {
            response = new ResponseResult();

            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "SCANNER_MAT_CHK_PACK.START_REPLENISH" };

                procPara.AddParamInput(0, "strJOB_NO", jobno);
                procPara.AddParamInput(1, "strMC_NO", mcno);
                procPara.AddParamInput(2, "strNO_OF_BAG", noOfBag);
                procPara.AddParamInput(3, "strUSER_ID", userid);
                procPara.AddParamOutput(4, "strREP_NO", OracleDbType.Varchar2, 30);
                procPara.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultMsg = (OracleString)procPara.ReturnValue(5);

                response.Message = resultMsg.ToString();

                if (response.Message == "OK")
                {
                    response.Data = ((OracleString)procPara.ReturnValue(4)).ToString();
                }
                else
                {
                    response.Data = string.Empty;
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Data = string.Empty;
            }

            return response;
        }

        public ResponseResult ScanRepLabel(string serialno, string repno, string jobno, string mcno, string userid)
        {
            response = new ResponseResult();
            MaterialCard mCard = null;
            try
            {
                ProcParam procPara = new ProcParam(7) { ProcedureName = "SCANNER_MAT_CHK_PACK.SCAN_REP_LABEL" };
                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strREP_NO", repno);
                procPara.AddParamInput(3, "strJOB_NO", jobno);
                procPara.AddParamInput(4, "strMC_NO", mcno);
                procPara.AddParamInput(5, "strUSER_ID", userid);
                procPara.AddParamOutput(6, "RESULTMSG", OracleDbType.Varchar2, 255);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mCard = new MaterialCard()
                        {
                            SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO"),
                            PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID"),
                            PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME"),
                            MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE"),
                            MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME"),
                            MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE"),
                            MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR"),
                            QTY = OraDataReader.Instance.GetDecimal("QTY"),
                            UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID"),
                            ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO"),
                            CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS")
                        };
                    }
                }

                // Always call close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }
                else
                {
                    response.Message = procPara.ReturnValue(6).ToString();
                    response.Data = mCard;
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion
    }
}
