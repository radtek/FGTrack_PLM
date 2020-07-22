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
    public class ProductCardNGBLL : IDisposable
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

        ~ProductCardNGBLL()
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

        public ProductCardNGBLL()
        {
            //Constructer
        }

        public List<JobLot> GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_VER.GET_JOB_LOT_LIST" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
                        jobLot.JOB_NO = OraDataReader.Instance.GetString("JOB_LOT");
                        jobLot.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        jobLot.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");

                        lstJobLot.Add(jobLot);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstJobLot;
        }

        public List<JobLot> Press_GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_PRESS.GET_JOB_LOT_LIST" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
                        jobLot.JOB_NO = OraDataReader.Instance.GetString("JOB_LOT");
                        jobLot.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        jobLot.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");

                        lstJobLot.Add(jobLot);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstJobLot;
        }

        public List<JobLot> Horizontal_GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_HOZ.GET_JOB_LOT_LIST" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
                        jobLot.JOB_NO = OraDataReader.Instance.GetString("JOB_LOT");
                        jobLot.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        jobLot.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");

                        lstJobLot.Add(jobLot);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstJobLot;
        }

        public List<JobLot> Tampo_GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_TAMPO.GET_JOB_LOT_LIST" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
                        jobLot.JOB_NO = OraDataReader.Instance.GetString("JOB_LOT");
                        jobLot.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        jobLot.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");

                        lstJobLot.Add(jobLot);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstJobLot;
        }

        public ProductCard GetJobLotInfo(string jobNo, int lineNo, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_VER.GET_JOB_LOT_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strLINE_NO", lineNo);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");
                        
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

        public ProductCard Press_GetJobLotInfo(string jobNo, int lineNo, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_PRESS.GET_JOB_LOT_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strLINE_NO", lineNo);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");

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

        public ProductCard Horizontal_GetJobLotInfo(string jobNo, int lineNo, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_HOZ.GET_JOB_LOT_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strLINE_NO", lineNo);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");

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

        public ProductCard Tampo_GetJobLotInfo(string jobNo, int lineNo, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_TAMPO.GET_JOB_LOT_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strLINE_NO", lineNo);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");

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

        public string UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_VER.UPD_JOB_LOT_NG_QTY" };
                procPara.AddParamInput(0, "strJOB_NO", jobNo);
                procPara.AddParamInput(1, "strLINE_NO", lineNo);
                procPara.AddParamInput(2, "strQTY", nQty);
                procPara.AddParamInput(3, "strUSER_ID", userid);
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

        public string Press_UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_PRESS.UPD_JOB_LOT_NG_QTY" };
                procPara.AddParamInput(0, "strJOB_NO", jobNo);
                procPara.AddParamInput(1, "strLINE_NO", lineNo);
                procPara.AddParamInput(2, "strQTY", nQty);
                procPara.AddParamInput(3, "strUSER_ID", userid);
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

        public string Horizontal_UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_HOZ.UPD_JOB_LOT_NG_QTY" };
                procPara.AddParamInput(0, "strJOB_NO", jobNo);
                procPara.AddParamInput(1, "strLINE_NO", lineNo);
                procPara.AddParamInput(2, "strQTY", nQty);
                procPara.AddParamInput(3, "strUSER_ID", userid);
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

        public string Tampo_UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_TAMPO.UPD_JOB_LOT_NG_QTY" };
                procPara.AddParamInput(0, "strJOB_NO", jobNo);
                procPara.AddParamInput(1, "strLINE_NO", lineNo);
                procPara.AddParamInput(2, "strQTY", nQty);
                procPara.AddParamInput(3, "strUSER_ID", userid);
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

        public List<JobLot> Assembly_GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3) { ProcedureName = "SCANNER_PACK_ASY.GET_JOB_LOT_LIST" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strUser_id", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
                        jobLot.JOB_NO = OraDataReader.Instance.GetString("JOB_LOT");
                        jobLot.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        jobLot.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");

                        lstJobLot.Add(jobLot);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstJobLot;
        }

        public ProductCard Assembly_GetJobLotInfo(string jobNo, int lineNo, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4) { ProcedureName = "SCANNER_PACK_ASY.GET_JOB_LOT_INFO" };
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJOB_NO", jobNo);
                procPara.AddParamInput(2, "strLINE_NO", lineNo);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.LINE_NO = OraDataReader.Instance.GetInteger("LINE_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PROD_SEQ_NO = OraDataReader.Instance.GetString("PROD_SEQ_NO");
                        pcCard.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("MTL_TYPE");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
                        pcCard.UNIT_ID = OraDataReader.Instance.GetString("UNIT_ID");

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

        public string Assembly_UpdateNGQty(string jobNo, int lineNo, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5) { ProcedureName = "SCANNER_PACK_ASY.UPD_JOB_LOT_NG_QTY" };
                procPara.AddParamInput(0, "strJOB_NO", jobNo);
                procPara.AddParamInput(1, "strLINE_NO", lineNo);
                procPara.AddParamInput(2, "strQTY", nQty);
                procPara.AddParamInput(3, "strUSER_ID", userid);
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
