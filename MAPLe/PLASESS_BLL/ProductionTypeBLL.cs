using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.DAL;

namespace HTN.BITS.BLL.PLASESS
{
    public class ProductionTypeBLL: IDisposable
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

        ~ProductionTypeBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        ////private TimeSpan executionTime;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        #endregion

        public ProductionTypeBLL()
        {
            //constructer
        }

        public List<ProductionType> GetProductionTypeList()
        {
            List<ProductionType> lstProductionType = null;
            ProductionType productType;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_M_PRODUCTION_TYPE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstProductionType = new List<ProductionType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        productType = new ProductionType();

                        productType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        productType.NAME = OraDataReader.Instance.GetString("NAME");
                        productType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        productType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstProductionType.Add(productType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstProductionType = null;
                throw ex;
            }

            return lstProductionType;
        }

        public List<UnplanInType> GetUnplanInTypeList()
        {
            List<UnplanInType> lstUnplanInType = null;
            UnplanInType unplanInType;

            try
            {
                ProcParam param = new ProcParam(3) { ProcedureName = "MASTER_PACK.GET_M_UNPLANIN_TYPE" };
                param.AddParamRefCursor(0, "io_cursor");
                param.AddParamInput(1, "strFindAll", DBNull.Value);
                param.AddParamInput(2, "strSEQ_NO", DBNull.Value);

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(param);

                ////this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstUnplanInType = new List<UnplanInType>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        unplanInType = new UnplanInType();

                        unplanInType.SEQ_NO = OraDataReader.Instance.GetString("SEQ_NO");
                        unplanInType.NAME = OraDataReader.Instance.GetString("NAME");
                        unplanInType.REMARK = OraDataReader.Instance.GetString("REMARK");
                        unplanInType.REC_STAT = (OraDataReader.Instance.GetString("REC_STAT").ToUpper() == "Y");

                        lstUnplanInType.Add(unplanInType);
                    }
                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();
            }
            catch (Exception ex)
            {
                lstUnplanInType = null;
                throw ex;
            }

            return lstUnplanInType;
        }
    }
}
