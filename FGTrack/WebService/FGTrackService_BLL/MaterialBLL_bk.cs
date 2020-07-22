using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL;
using HTN.BITS.DAL;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using FGTrackService.BLL;

namespace FGTrackService.BLL //FGTrackService.BLL
{
    public class MaterialBLL : IDisposable
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

        ~MaterialBLL()
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

        public MaterialBLL()
        {
            //Constructer
        }

        #region Method Member

        public bool CheckExistLocation(string location)
        {
            bool isValid = false;

            try
            {
                ProcParam procPara = new ProcParam(2) { ProcedureName = "SCANNER_MAT_PACK.CHK_EXISTS_LOCATION" };

                procPara.AddParamReturn(0, "ReturnValue", OracleDbType.Int32, 120);
                procPara.AddParamInput(1, "strLOCATION", location);


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

        public MaterialCard ScanMatIn_Complete(string serialno, string location, string userid, out string resultMsg)
        {
            resultMsg = string.Empty;
            MaterialCard mtl = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_MAT_PACK.SCAN_MATIN_COMPLETE" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strLOCATION", location);
                procPara.AddParamInput(3, "strUSER_ID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 30);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtl = new MaterialCard();

                        mtl.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        mtl.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        mtl.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtl.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtl.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtl.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtl.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtl.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtl.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mtl.CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(4);
                if (!resultDB.IsNull)
                {
                    resultMsg = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mtl;
        }

        public MaterialCard ScanMatOut_Complete(string serialno, string userid, out string resultMsg)
        {
            resultMsg = string.Empty;
            MaterialCard mtl = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_MAT_PACK.SCAN_MATOUT_COMPLETE" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strUSER_ID", userid);
                procPara.AddParamOutput(3, "RESULTMSG", OracleDbType.Varchar2, 30);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtl = new MaterialCard();

                        mtl.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        mtl.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        mtl.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtl.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtl.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtl.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtl.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtl.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtl.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mtl.CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(3);
                if (!resultDB.IsNull)
                {
                    resultMsg = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mtl;
        }

        public MaterialCard ScanMat_Status(string serialno, out string resultMsg)
        {
            resultMsg = string.Empty;
            MaterialCard mtl = null;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_MAT_PACK.SCAN_MAT_STATUS" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 30);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtl = new MaterialCard();

                        mtl.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        mtl.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        mtl.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtl.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtl.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtl.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtl.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtl.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtl.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mtl.CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS");

                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(2);
                if (!resultDB.IsNull)
                {
                    resultMsg = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mtl;
        }

        public MaterialCard ScanMat_Stock(string serialno, out string resultMsg)
        {
            resultMsg = string.Empty;
            MaterialCard mtl = null;

            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_MAT_PACK.SCAN_MAT_STOCK" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamOutput(2, "RESULTMSG", OracleDbType.Varchar2, 30);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtl = new MaterialCard();

                        mtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        mtl.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        mtl.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtl.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtl.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtl.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtl.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtl.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtl.MIN_QTY = OraDataReader.Instance.GetDecimal("MIN_QTY");
                        mtl.MAX_QTY = OraDataReader.Instance.GetDecimal("MAX_QTY");

                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(2);
                if (!resultDB.IsNull)
                {
                    resultMsg = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mtl;
        }

        public MaterialCard ScanMat_ChangeLocation(string serialno, string location, string userid, out string resultMsg)
        {
            resultMsg = string.Empty;
            MaterialCard mtl = null;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_MAT_PACK.SCAN_MAT_CHANGELOCATION" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                procPara.AddParamInput(1, "strSERIAL_NO", serialno);
                procPara.AddParamInput(2, "strLOCATION", location);
                procPara.AddParamInput(3, "strUSER_ID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 30);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                if (GlobalDB.Instance.LastException != null)
                    throw GlobalDB.Instance.LastException;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        mtl = new MaterialCard();

                        mtl.SERIAL_NO = OraDataReader.Instance.GetString("SERIAL_NO");
                        mtl.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        mtl.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        mtl.MTL_CODE = OraDataReader.Instance.GetString("MTL_CODE");
                        mtl.MTL_NAME = OraDataReader.Instance.GetString("MTL_NAME");
                        mtl.MTL_GRADE = OraDataReader.Instance.GetString("MTL_GRADE");
                        mtl.MTL_COLOR = OraDataReader.Instance.GetString("MTL_COLOR");
                        mtl.QTY = OraDataReader.Instance.GetDecimal("QTY");
                        mtl.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        mtl.ARRIVAL_NO = OraDataReader.Instance.GetString("ARRIVAL_NO");
                        mtl.CARGO_STATUS = OraDataReader.Instance.GetString("CARGO_STATUS");
                        mtl.LOCATION_ID = OraDataReader.Instance.GetString("LOCATION_ID");
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                OracleString resultDB = (OracleString)procPara.ReturnValue(4);
                if (!resultDB.IsNull)
                {
                    resultMsg = resultDB.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mtl;
        }

        #endregion
    }
}
