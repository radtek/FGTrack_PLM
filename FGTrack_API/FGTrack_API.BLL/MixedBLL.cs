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
    public class MixedBLL : IDisposable
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

        ~MixedBLL()
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

        public ResponseResult StartMixing(decimal percen, int noOfBag, string userid)
        {
            response = new ResponseResult();

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_MAT_CHK_PACK.START_MIXING" };

                procPara.AddParamInput(0, "strSTD_PERCEN", percen);
                procPara.AddParamInput(1, "strNO_OF_BAG", noOfBag);
                procPara.AddParamInput(2, "strUSER_ID", userid);
                procPara.AddParamOutput(3, "strMIXED_NO", OracleDbType.Varchar2, 30);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultMsg = (OracleString)procPara.ReturnValue(4);

                response.Message = resultMsg.ToString();

                if (response.Message == "OK")
                {
                    response.Data = ((OracleString)procPara.ReturnValue(3)).ToString();
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

        public ResponseResult ScanMixingLabel(string serialno, string mixedno, string mtlCode, string userid)
        {
            response = new ResponseResult();
            MaterialCard mCard = null;
            try
            {
                ProcParam procPara = new ProcParam(6) { ProcedureName = "SCANNER_MAT_CHK_PACK.SCAN_MIXED_LABEL" };
                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strMIXED_NO", mixedno);
                procPara.AddParamInput(3, "strMTL_CODE", mtlCode);
                procPara.AddParamInput(4, "strUSER_ID", userid);
                procPara.AddParamOutput(5, "RESULTMSG", OracleDbType.Varchar2, 255);

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
                    response.Message = procPara.ReturnValue(5).ToString();
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

        public ResponseResult UpdateMixedQty(string serialno, string mixedno, decimal mixQty)
        {
            response = new ResponseResult();

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_MAT_CHK_PACK.UPDATE_MIXED_QTY" };
                procPara.AddParamInput(0, "strMIXED_NO", mixedno);
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strMIXED_QTY", mixQty);
                procPara.AddParamOutput(3, "RESULTMSG", OracleDbType.Varchar2, 255);

                GlobalDB.Instance.DataAc.ExecuteNonQuery(procPara);

                if (GlobalDB.Instance.DataAc.LastException != null)
                {
                    throw GlobalDB.Instance.DataAc.LastException;
                }

                var resultMsg = (OracleString)procPara.ReturnValue(3);

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

        #endregion
    }
}
