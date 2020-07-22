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
    public class AssignNGBLL : IDisposable
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

        ~AssignNGBLL()
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

        public AssignNGBLL()
        {
            //Constructer
        }

        public List<JobLot> GetJobLotList(string jobNo, string userid)
        {
            List<JobLot> lstJobLot = null;
            JobLot jobLot;
            try
            {
                ProcParam procPara = new ProcParam(3);
                procPara.ProcedureName = "SCANNER_PACK.GET_JOB_LOT_LIST";
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJobNo", jobNo);
                procPara.AddParamInput(2, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstJobLot = new List<JobLot>();

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        jobLot = new JobLot();
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

        public ProductCard GetJobLotInfo(string jobNo, string jobLot, string userid)
        {
            ProductCard pcCard = null;

            try
            {
                ProcParam procPara = new ProcParam(4);
                procPara.ProcedureName = "SCANNER_PACK.GET_JOB_LOT_INFO";
                procPara.AddParamRefCursor(0, "io_cursor");
                procPara.AddParamInput(1, "strJobNo", jobNo);
                procPara.AddParamInput(2, "strJobLot", jobLot);
                procPara.AddParamInput(3, "strUserID", userid);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        pcCard = new ProductCard();

                        pcCard.JOB_NO = OraDataReader.Instance.GetString("JOB_NO");
                        pcCard.JOB_LOT = OraDataReader.Instance.GetString("JOB_LOT");
                        pcCard.PRODUCT_NAME = OraDataReader.Instance.GetString("PROD_NAME");
                        pcCard.MTL_TYPE = OraDataReader.Instance.GetString("PROD_TYPE");
                        pcCard.ASG_NG = OraDataReader.Instance.GetInteger("ASG_NG");
                        pcCard.NG_QTY = OraDataReader.Instance.GetInteger("NG_QTY");
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

        public string UpdNGQty(string jobNo, string jobLot, int nQty, string userid)
        {
            string resultMsg = string.Empty;

            try
            {
                ProcParam procPara = new ProcParam(5);
                procPara.ProcedureName = "SCANNER_PACK.UPD_NG_QTY";
                procPara.AddParamInput(0, "strJobNo", jobNo);
                procPara.AddParamInput(1, "strJobLot", jobLot);
                procPara.AddParamInput(2, "strQty", nQty);
                procPara.AddParamInput(3, "strUserID", userid);
                procPara.AddParamOutput(4, "RESULTMSG", OracleDbType.Varchar2, 120);

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
