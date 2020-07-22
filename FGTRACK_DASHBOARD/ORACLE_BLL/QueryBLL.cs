using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.ORACLE.DAL;
using HTN.BITS.FGTDB.BEL;

namespace HTN.BITS.ORACLE.BLL
{
    public class QueryBLL : IDisposable
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

        ~QueryBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        private TimeSpan executionTime;
        private string userid;

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

        #region Method Member


        #region Stock by Customer

        public List<StockByCustomer> GetStockByCustomer()
        {
            List<StockByCustomer> lstStockByCustomer = null;
            StockByCustomer STKCustomer = null;

            try
            {
                ProcParam procPara = new ProcParam(1) { ProcedureName = "DASHBOARD_NEW_PACK.GET_STOCK_BY_CUSTOMER" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");                             

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstStockByCustomer = new List<StockByCustomer>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        STKCustomer = new StockByCustomer();

                        STKCustomer.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        STKCustomer.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        STKCustomer.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        STKCustomer.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        STKCustomer.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        STKCustomer.PRODUCT_TYPE_ID = OraDataReader.Instance.GetString("PRODUCT_TYPE_ID");
                        STKCustomer.PRODUCT_TYPE_NAME = OraDataReader.Instance.GetString("PRODUCT_TYPE_NAME");
                        STKCustomer.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        STKCustomer.QTY = OraDataReader.Instance.GetInteger("QTY");
                        STKCustomer.NO_OF_BOX = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        //if (!OraDataReader.Instance.IsDBNull("N_USER_DATE"))
                        //    STKCustomer.N_USER_DATE = OraDataReader.Instance.GetDateTime("N_USER_DATE");
                        //if (!OraDataReader.Instance.IsDBNull("U_USER_DATE"))
                        //    STKCustomer.U_USER_DATE = OraDataReader.Instance.GetDateTime("U_USER_DATE");

                        lstStockByCustomer.Add(STKCustomer);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstStockByCustomer = null;
                throw ex;
            }

            return lstStockByCustomer;
        }

        #endregion

        #region Stock by Machine

        public List<StockByMachine> GetStockByMachine()
        {
            List<StockByMachine> lstStockByMachine = null;
            StockByMachine STKMachine = null;

            try
            {
                ProcParam procPara = new ProcParam(1) { ProcedureName = "DASHBOARD_NEW_PACK.GET_STOCK_BY_MACHINE" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstStockByMachine = new List<StockByMachine>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 1000;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        STKMachine = new StockByMachine();

                        STKMachine.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        STKMachine.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        if (!OraDataReader.Instance.IsDBNull("START_DATE"))
                            STKMachine.START_DATE = OraDataReader.Instance.GetDateTime("START_DATE");
                        if (!OraDataReader.Instance.IsDBNull("END_DATE"))
                            STKMachine.END_DATE = OraDataReader.Instance.GetDateTime("END_DATE");
                        STKMachine.STATUS = OraDataReader.Instance.GetString("STATUS");
                        STKMachine.PLAN_QTY = OraDataReader.Instance.GetInteger("PLAN_QTY");
                        STKMachine.PRODUCT_TYPE = OraDataReader.Instance.GetString("PRODUCT_TYPE");
                        STKMachine.PROD_TYPE_S = OraDataReader.Instance.GetString("PROD_TYPE_S");
                        STKMachine.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        STKMachine.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        STKMachine.STOCK_PCS = OraDataReader.Instance.GetInteger("STOCK_PCS");
                        STKMachine.STOCK_BOX = OraDataReader.Instance.GetInteger("STOCK_BOX");
                        STKMachine.MC_NO = OraDataReader.Instance.GetString("MC_NO");
                        STKMachine.MACHINE_NAME = OraDataReader.Instance.GetString("MACHINE_NAME");
                        STKMachine.MIN_BOX = OraDataReader.Instance.GetInteger("MIN_BOX");
                        STKMachine.MAX_BOX = OraDataReader.Instance.GetInteger("MAX_BOX");

                        lstStockByMachine.Add(STKMachine);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstStockByMachine = null;
                throw ex;
            }

            return lstStockByMachine;
        }

        #endregion

        #region Stock by MinMax

        public List<StockByMinMax> GetStockByMinMax()
        {
            List<StockByMinMax> lstStockByMinMax = null;
            StockByMinMax STKMinMax = null;

            try
            {
                ProcParam procPara = new ProcParam(1) { ProcedureName = "DASHBOARD_NEW_PACK.GET_STOCK_BY_MIN_MAX"};

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                
                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstStockByMinMax = new List<StockByMinMax>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        STKMinMax = new StockByMinMax();

                        STKMinMax.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        STKMinMax.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        STKMinMax.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        STKMinMax.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        STKMinMax.PRODUCT_SEQ_NO = OraDataReader.Instance.GetString("PRODUCT_SEQ_NO");
                        STKMinMax.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        STKMinMax.PRODUCT_TYPE_ID = OraDataReader.Instance.GetString("PRODUCT_TYPE_ID");
                        STKMinMax.PRODUCT_TYPE_NAME = OraDataReader.Instance.GetString("PRODUCT_TYPE_NAME");
                        STKMinMax.BOX_QTY = OraDataReader.Instance.GetInteger("BOX_QTY");
                        STKMinMax.STOCK_PCS = OraDataReader.Instance.GetInteger("STOCK_PCS");
                        STKMinMax.STOCK_BOX = OraDataReader.Instance.GetInteger("STOCK_BOX");
                        STKMinMax.STATUS_RUNNING_MC = OraDataReader.Instance.GetString("STATUS_RUNNING_MC");
                        STKMinMax.PICK_PENDING = OraDataReader.Instance.GetInteger("PICK_PENDING");
                        STKMinMax.EXPECTED_DELAY = OraDataReader.Instance.GetInteger("EXPECTED_DELAY");
                        STKMinMax.FORECAST = OraDataReader.Instance.GetInteger("FORECAST");
                        STKMinMax.MIN_BOX = OraDataReader.Instance.GetInteger("MIN_BOX");
                        STKMinMax.MAX_BOX = OraDataReader.Instance.GetInteger("MAX_BOX");
                        STKMinMax.STATUS = OraDataReader.Instance.GetString("STATUS");                       

                        lstStockByMinMax.Add(STKMinMax);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstStockByMinMax = null;
                throw ex;
            }

            return lstStockByMinMax;
        }

        #endregion

        #region Delivery Board

        public List<DeliveryBoard> GetDeliveryBoard()
        {
            List<DeliveryBoard> lstDelivery = null;
            DeliveryBoard Delivery = null;

            try
            {
                ProcParam procPara = new ProcParam(1) { ProcedureName = "DASHBOARD_NEW_PACK.GET_DELIVERY_BOARD" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");
                
                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstDelivery = new List<DeliveryBoard>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        Delivery = new DeliveryBoard();

                        Delivery.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        Delivery.PARTY_NAME = OraDataReader.Instance.GetString("PARTY_NAME");
                        Delivery.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        Delivery.ETD_DATE = OraDataReader.Instance.GetDateTime("ETD_DATE");
                        Delivery.ETD_TIME = OraDataReader.Instance.GetString("ETD_TIME");
                        Delivery.STATUS = OraDataReader.Instance.GetString("STATUS");
                        Delivery.RESPONSIBLE = OraDataReader.Instance.GetString("RESPONSIBLE");                      

                        lstDelivery.Add(Delivery);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstDelivery = null;
                throw ex;
            }

            return lstDelivery;
        }

        #endregion

        #region Delivery Board Detail

        public List<DeliveryBoardDetail> GetDeliveryBoardDetail()
        {
            List<DeliveryBoardDetail> lstDeliveryDetail = null;
            DeliveryBoardDetail DeliveryDetail = null;

            try
            {
                ProcParam procPara = new ProcParam(1) { ProcedureName = "DASHBOARD_NEW_PACK.GET_DELIVERY_BOARD_DETAIL" };

                procPara.AddParamRefCursor(0, "IO_CURSOR");                

                OraDataReader.Instance.OraReader = GlobalDB.Instance.DataAc.ExecuteDataReader(procPara);

                this.executionTime = GlobalDB.Instance.DataAc.ExecuteTime;

                if (OraDataReader.Instance.OraReader.HasRows)
                {
                    lstDeliveryDetail = new List<DeliveryBoardDetail>();

                    OraDataReader.Instance.OraReader.FetchSize = OraDataReader.Instance.OraReader.RowSize * 100;

                    while (OraDataReader.Instance.OraReader.Read())
                    {
                        DeliveryDetail = new DeliveryBoardDetail();

                        DeliveryDetail.PARTY_ID = OraDataReader.Instance.GetString("PARTY_ID");
                        DeliveryDetail.WH_ID = OraDataReader.Instance.GetString("WH_ID");
                        DeliveryDetail.ETD_DATE = OraDataReader.Instance.GetDateTime("ETD_DATE");
                        DeliveryDetail.PRODUCT_NO = OraDataReader.Instance.GetString("PRODUCT_NO");
                        DeliveryDetail.PRODUCT_NAME = OraDataReader.Instance.GetString("PRODUCT_NAME");
                        DeliveryDetail.QTY = OraDataReader.Instance.GetInteger("QTY");
                        DeliveryDetail.UNIT = OraDataReader.Instance.GetString("UNIT");
                        DeliveryDetail.NO_OF_BOX = OraDataReader.Instance.GetInteger("NO_OF_BOX");
                        DeliveryDetail.FREE_STOCK = OraDataReader.Instance.GetInteger("FREE_STOCK");
                        DeliveryDetail.ASSIGN_QTY = OraDataReader.Instance.GetInteger("ASSIGN_QTY");
                        DeliveryDetail.PICKED_QTY = OraDataReader.Instance.GetInteger("PICKED_QTY");
                        DeliveryDetail.LOADED_QTY = OraDataReader.Instance.GetInteger("LOADED_QTY");
                        DeliveryDetail.STATUS = OraDataReader.Instance.GetString("STATUS");
                        DeliveryDetail.REMARK = OraDataReader.Instance.GetString("REMARK");
                        
                        lstDeliveryDetail.Add(DeliveryDetail);
                    }

                }

                // always call Close when done reading.
                OraDataReader.Instance.Close();

                if (GlobalDB.Instance.DataAc.LastException != null)
                    throw GlobalDB.Instance.DataAc.LastException;

            }
            catch (Exception ex)
            {
                lstDeliveryDetail = null;
                throw ex;
            }

            return lstDeliveryDetail;
        }

        #endregion

        #endregion
    }
}
