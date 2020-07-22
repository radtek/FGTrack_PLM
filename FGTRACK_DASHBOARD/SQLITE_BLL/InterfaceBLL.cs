using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.FGTDB.BEL;
using HTN.BITS.SQLITE.DAL;
using System.Globalization;

namespace HTN.BITS.SQLITE.BLL
{
    public class InterfaceBLL : IDisposable
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

        ~InterfaceBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        ////private TimeSpan executionTime;
        private int userid;

        #endregion

        #region Property Member

        //public TimeSpan ExecutionTime
        //{
        //    get
        //    {
        //        return this.executionTime;
        //    }
        //}

        public int UserID
        {
            get
            {
                return this.userid;
            }
        }

        #endregion

        #region Method Member

        #region Check Min Flag

        public int GetMinFlagSTKCustomer()
        {
            int resultFlag = 1;

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultFlag;

        }

        #endregion

        #region Insert Data

        public void InsertStockByCustomer(List<StockByCustomer> lstStockByCustomer,DateTime lastupdate)
        {
            int flag = 0;

            try
            {
                object result = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT IFNULL(MIN(FLAG), 1) + 1 FROM STOCK_BY_CUSTOMER");

                flag = Convert.ToInt32(result, NumberFormatInfo.CurrentInfo);

                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (StockByCustomer SQLiteInsert in lstStockByCustomer)
                {
                    param = new SQLiteParam(12) { CommandText = StoreProcedure.Instance.GetScript("InsertStockByCustomer") };

                    param.ParamStringFixedLength(0, "@PARTY_ID", SQLiteInsert.PARTY_ID);
                    param.ParamStringFixedLength(1, "@PARTY_NAME", SQLiteInsert.PARTY_NAME);
                    param.ParamStringFixedLength(2, "@WH_ID", SQLiteInsert.WH_ID);
                    param.ParamStringFixedLength(3, "@PRODUCT_NO", SQLiteInsert.PRODUCT_NO);
                    param.ParamStringFixedLength(4, "@PRODUCT_NAME", SQLiteInsert.PRODUCT_NAME);
                    param.ParamStringFixedLength(5, "@PRODUCT_TYPE_ID", SQLiteInsert.PRODUCT_TYPE_ID);
                    param.ParamStringFixedLength(6, "@PRODUCT_TYPE_NAME", SQLiteInsert.PRODUCT_TYPE_NAME);
                    param.ParamInt32(7, "@BOX_QTY", SQLiteInsert.BOX_QTY);
                    param.ParamInt32(8, "@QTY", SQLiteInsert.QTY);
                    param.ParamInt32(9, "@NO_OF_BOX", SQLiteInsert.NO_OF_BOX);
                    param.ParamInt32(10, "@FLAG", flag);
                    param.ParamDateTime(11, "@lastupdate", lastupdate); 

                    //if (stkUpload.N_USER_DATE.HasValue)
                    //    param.ParamDate(9, "@N_USER_DATE", stkUpload.N_USER_DATE.Value);
                    //else
                    //    param.ParamNull(9, "@N_USER_DATE");                  

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertStockByMachine(List<StockByMachine> lstStockByMachine, DateTime lastupdate)
        {
            int flag = 0;

            try
            {
                object result = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT IFNULL(MIN(FLAG), 1) + 1 FROM STOCK_BY_MACHINE");
                flag = Convert.ToInt32(result, NumberFormatInfo.CurrentInfo);

                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (StockByMachine SQLiteInsert in lstStockByMachine)
                {
                    param = new SQLiteParam(18) { CommandText = StoreProcedure.Instance.GetScript("InsertStockByMachine") };

                    param.ParamStringFixedLength(0, "@PARTY_ID", SQLiteInsert.PARTY_ID);
                    param.ParamStringFixedLength(1, "@PARTY_NAME", SQLiteInsert.PARTY_NAME);
                    if (SQLiteInsert.START_DATE.HasValue)
                        param.ParamDateTime(2, "@START_DATE", SQLiteInsert.START_DATE.Value);
                    else
                        param.ParamNull(2, "@START_DATE");

                    if (SQLiteInsert.END_DATE.HasValue)
                        param.ParamDateTime(3, "@END_DATE", SQLiteInsert.END_DATE.Value);
                    else
                        param.ParamNull(3, "@END_DATE");

                    param.ParamStringFixedLength(4, "@STATUS", SQLiteInsert.STATUS);
                    param.ParamInt32(5, "@PLAN_QTY", SQLiteInsert.PLAN_QTY);
                    param.ParamStringFixedLength(6, "@PRODUCT_TYPE", SQLiteInsert.PRODUCT_TYPE);
                    param.ParamStringFixedLength(7, "@PROD_TYPE_S", SQLiteInsert.PROD_TYPE_S);
                    param.ParamStringFixedLength(8, "@PRODUCT_NO", SQLiteInsert.PRODUCT_NO);
                    param.ParamStringFixedLength(9, "@PRODUCT_NAME", SQLiteInsert.PRODUCT_NAME);
                    param.ParamInt32(10, "@STOCK_PCS", SQLiteInsert.STOCK_PCS);
                    param.ParamInt32(11, "@STOCK_BOX", SQLiteInsert.STOCK_BOX);
                    param.ParamStringFixedLength(12, "@MC_NO", SQLiteInsert.MC_NO);
                    param.ParamStringFixedLength(13, "@MACHINE_NAME", SQLiteInsert.MACHINE_NAME);
                    param.ParamInt32(14, "@MIN_BOX", SQLiteInsert.MIN_BOX);
                    param.ParamInt32(15, "@MAX_BOX", SQLiteInsert.MAX_BOX);
                    param.ParamInt32(16, "@FLAG", flag);
                    param.ParamDateTime(17, "@lastupdate", lastupdate);

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertStockByMinMax(List<StockByMinMax> lstStockByMinMax, DateTime lastupdate)
        {
            int flag = 0;

            try
            {
                object result = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT IFNULL(MIN(FLAG), 1) + 1 FROM STOCK_BY_MINMAX");
                flag = Convert.ToInt32(result, NumberFormatInfo.CurrentInfo);

                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (StockByMinMax SQLiteInsert in lstStockByMinMax)
                {
                    param = new SQLiteParam(20) { CommandText = StoreProcedure.Instance.GetScript("InsertStockByMinMax") };

                    param.ParamStringFixedLength(0, "@PARTY_ID", SQLiteInsert.PARTY_ID);
                    param.ParamStringFixedLength(1, "@PARTY_NAME", SQLiteInsert.PARTY_NAME);
                    param.ParamStringFixedLength(2, "@WH_ID", SQLiteInsert.WH_ID);
                    param.ParamStringFixedLength(3, "@PRODUCT_NO", SQLiteInsert.PRODUCT_NO);
                    param.ParamStringFixedLength(4, "@PRODUCT_SEQ_NO", SQLiteInsert.PRODUCT_SEQ_NO);
                    param.ParamStringFixedLength(5, "@PRODUCT_NAME", SQLiteInsert.PRODUCT_NAME);
                    param.ParamStringFixedLength(6, "@PRODUCT_TYPE_ID", SQLiteInsert.PRODUCT_TYPE_ID);
                    param.ParamStringFixedLength(7, "@PRODUCT_TYPE_NAME", SQLiteInsert.PRODUCT_TYPE_NAME);
                    param.ParamInt32(8, "@BOX_QTY", SQLiteInsert.BOX_QTY);
                    param.ParamInt32(9, "@STOCK_PCS", SQLiteInsert.STOCK_PCS);
                    param.ParamInt32(10, "@STOCK_BOX", SQLiteInsert.STOCK_BOX);
                    param.ParamStringFixedLength(11, "@STATUS_RUNNING_MC", SQLiteInsert.STATUS_RUNNING_MC);
                    param.ParamInt32(12, "@PICK_PENDING", SQLiteInsert.PICK_PENDING);
                    param.ParamInt32(13, "@EXPECTED_DELAY", SQLiteInsert.EXPECTED_DELAY);
                    param.ParamInt32(14, "@FORECAST", SQLiteInsert.FORECAST);
                    param.ParamInt32(15, "@MIN_BOX", SQLiteInsert.MIN_BOX);
                    param.ParamInt32(16, "@MAX_BOX", SQLiteInsert.MAX_BOX);
                    param.ParamStringFixedLength(17, "@STATUS", SQLiteInsert.STATUS);
                    param.ParamInt32(18, "@FLAG", flag);
                    param.ParamDateTime(19, "@lastupdate", lastupdate);

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertDeliveryBoard(List<DeliveryBoard> lstDeliveryBoard, DateTime lastupdate)
        {
            int flag = 0;

            try
            {
                object result = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT IFNULL(MIN(FLAG), 1) + 1 FROM DELIVERY_BOARD");
                flag = Convert.ToInt32(result, NumberFormatInfo.CurrentInfo);

                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (DeliveryBoard SQLiteInsert in lstDeliveryBoard)
                {
                    param = new SQLiteParam(9) { CommandText = StoreProcedure.Instance.GetScript("InsertDeliveryBoard") };

                    param.ParamStringFixedLength(0, "@PARTY_ID", SQLiteInsert.PARTY_ID);
                    param.ParamStringFixedLength(1, "@PARTY_NAME", SQLiteInsert.PARTY_NAME);
                    param.ParamStringFixedLength(2, "@WH_ID", SQLiteInsert.WH_ID);
                    param.ParamDateTime(3, "@ETD_DATE", SQLiteInsert.ETD_DATE);
                    param.ParamStringFixedLength(4, "@ETD_TIME", SQLiteInsert.ETD_TIME);
                    param.ParamStringFixedLength(5, "@STATUS", SQLiteInsert.STATUS);
                    param.ParamStringFixedLength(6, "@RESPONSIBLE", SQLiteInsert.RESPONSIBLE);
                    param.ParamInt32(7, "@FLAG", flag);
                    param.ParamDateTime(8, "@lastupdate", lastupdate);

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertDeliveryBoardDetail(List<DeliveryBoardDetail> lstDeliveryBoardDetail, DateTime lastupdate)
        {
            int flag = 0;

            try
            {
                object result = GlobalSqliteDB.Instance.DataAc.ExecuteScalar(@"SELECT IFNULL(MIN(FLAG), 1) + 1 FROM DELIVERY_DETAIL");
                flag = Convert.ToInt32(result, NumberFormatInfo.CurrentInfo);

                List<SQLiteParam> lstParam = new List<SQLiteParam>();
                SQLiteParam param = null;

                foreach (DeliveryBoardDetail SQLiteInsert in lstDeliveryBoardDetail)
                {
                    param = new SQLiteParam(16) { CommandText = StoreProcedure.Instance.GetScript("InsertDeliveryBoardDetail") };

                    param.ParamStringFixedLength(0, "@PARTY_ID", SQLiteInsert.PARTY_ID);
                    param.ParamStringFixedLength(1, "@WH_ID", SQLiteInsert.WH_ID);
                    param.ParamDateTime(2, "@ETD_DATE", SQLiteInsert.ETD_DATE);
                    param.ParamStringFixedLength(3, "@PRODUCT_NO", SQLiteInsert.PRODUCT_NO);
                    param.ParamStringFixedLength(4, "@PRODUCT_NAME", SQLiteInsert.PRODUCT_NAME);                    
                    param.ParamInt32(5, "@QTY", SQLiteInsert.QTY);
                    param.ParamStringFixedLength(6, "@UNIT", SQLiteInsert.UNIT);
                    param.ParamInt32(7, "@NO_OF_BOX", SQLiteInsert.NO_OF_BOX);
                    param.ParamInt32(8, "@FREE_STOCK", SQLiteInsert.FREE_STOCK);
                    param.ParamInt32(9, "@ASSIGN_QTY", SQLiteInsert.ASSIGN_QTY);
                    param.ParamInt32(10, "@PICKED_QTY", SQLiteInsert.PICKED_QTY);
                    param.ParamInt32(11, "@LOADED_QTY", SQLiteInsert.LOADED_QTY);
                    param.ParamStringFixedLength(12, "@STATUS", SQLiteInsert.STATUS);
                    param.ParamStringFixedLength(13, "@REMARK", SQLiteInsert.REMARK);
                    param.ParamInt32(14, "@FLAG", flag);
                    param.ParamDateTime(15, "@lastupdate", lastupdate);

                    lstParam.Add(param);
                }

                if (lstParam.Count <= 0) return;

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(lstParam);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        #region Delete Data

        public void DeleteStockByCustomer()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("DeleteStockByCustomer") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteStockByMachine()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("DeleteStockByMachine") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteStockByMinMax()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("DeleteStockByMinMax") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDeliveryBoard()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("DeleteDeliveryBoard") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDeliveryBoardDetail()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("DeleteDeliveryBoardDetail") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update Data

        public void UpdateStockByCustomer()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("UpdateStockByCustomer") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStockByMachine()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("UpdateStockByMachine") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStockByMinMax()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("UpdateStockByMinMax") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDeliveryBoard()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("UpdateDeliveryBoard") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDeliveryBoardDetail()
        {
            try
            {
                SQLiteParam param = new SQLiteParam(1) { CommandText = StoreProcedure.Instance.GetScript("UpdateDeliveryBoardDetail") };

                param.ParamStringFixedLength(0, "@FLAG", "1");

                int rowAfect = GlobalSqliteDB.Instance.DataAc.ExecuteNonQuery(param);

                if (GlobalSqliteDB.Instance.LastException != null)
                    throw GlobalSqliteDB.Instance.LastException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion             

        #endregion
    }
}
