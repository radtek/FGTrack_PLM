using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using HTN.BITS.FGTDB.BEL;
using HTN.BITS.ORACLE.BLL;
using System.Threading;
using HTN.BITS.SQLITE.BLL;
using System.Configuration;

namespace FGTDB_Service
{
    public partial class FGTDB_Service : ServiceBase
    {
        public enum ExecCommands
        {
            cmdCust = 128,
            cmdMC = 129,
            cmdMiMx = 130,
            cmdDely = 131

        };

        #region Variable Member          
                
        private System.Threading.Timer oTimer;

        DateTime lastupdate;
        int iDuetime = 0;
        int iPeriod = 0;

        #endregion

        public FGTDB_Service()
        {

            InitializeComponent();

            CanStop = true;
            CanPauseAndContinue = true;
            CanShutdown = true;

            // Initialize eventLogSimple   
            if (!System.Diagnostics.EventLog.SourceExists("FGTDB_Source"))
                System.Diagnostics.EventLog.CreateEventSource("FGTDB_Source", "FGTDB_log");

            eventLog1.Source = "FGTDB_Source";
            eventLog1.Log = "FGTDB_log";
            //eventLog1.Clear();

            // Oracle GlobalDB
            try
            {
                eventLog1.WriteEntry("Initial Oracle Database", EventLogEntryType.Information);
                GlobalDB.Instance.Init();

                eventLog1.WriteEntry("Connection to Oracle Database", EventLogEntryType.Information);
                GlobalDB.Instance.Connect();

                // SQLite GlobalDB
                eventLog1.WriteEntry("Initial SQLite Database", EventLogEntryType.Information);
                GlobalSqliteDB.Instance.Init();

                eventLog1.WriteEntry("Connection to SQLite Database", EventLogEntryType.Information);
                GlobalSqliteDB.Instance.Connect();
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

        }

        #region Service Work

        protected override void OnStart(string[] args)
        {
            
            eventLog1.WriteEntry("FGTDB Service Started!", EventLogEntryType.Information);

            iDuetime = int.Parse(ConfigurationManager.AppSettings["DueTime"]);
            iPeriod = int.Parse(ConfigurationManager.AppSettings["PeriodTime"]);

            TimerCallback tmrCallBack = new TimerCallback(oTimer_TimerCallback);
            oTimer = new System.Threading.Timer(tmrCallBack);

            oTimer.Change(new TimeSpan(0, iDuetime, 0), new TimeSpan(0, iPeriod, 0));           
            
            
        }        

        protected override void OnPause()
        {
            try
            {
                eventLog1.WriteEntry("Pause FGTDB Service", EventLogEntryType.Information);
                //set auto timer for pause
                //SetAutoTimer(true);
                oTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("OnPause : " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnContinue()
        {
            try
            {
                eventLog1.WriteEntry("Continue FGTDB Service", EventLogEntryType.Information);
                //set auto timer for continue
                //SetAutoTimer(false);
                oTimer.Change(new TimeSpan(0, iDuetime, 0), new TimeSpan(0, iPeriod, 0));
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("OnContinue : " + ex.Message, EventLogEntryType.Error);
            }

        }

        protected override void OnStop()
        {
            try
            {
                eventLog1.WriteEntry("FGTDB Service stopped!", EventLogEntryType.Information);
                //set auto timer for stop
                //SetAutoTimer(true);
                oTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("OnContinue : " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnShutdown()
        {
            try
            {
                eventLog1.WriteEntry("Shutdown FGTDB Service", EventLogEntryType.Information);

                oTimer.Change(Timeout.Infinite, Timeout.Infinite);

                // Oracle GlobalDB
                GlobalDB.Instance.Disconenct();
                GlobalDB.Instance.Release();

                // SQLite GlobalDB
                GlobalSqliteDB.Instance.Disconenct();
                GlobalSqliteDB.Instance.Release();
            
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("OnShutdown : " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);

            lastupdate = DateTime.Now;

            switch (command)
            {
                case (int)ExecCommands.cmdCust:
                    eventLog1.WriteEntry("OnCustomCommand : Stock by Customer", EventLogEntryType.Information);
                    this.Interface_StockByCustomer(lastupdate);
                    break;
                case (int)ExecCommands.cmdMC:
                    eventLog1.WriteEntry("OnCustomCommand : Stock by Machine", EventLogEntryType.Information);
                    this.Interface_StockByMachine(lastupdate);
                    break;
                case (int)ExecCommands.cmdMiMx:
                    eventLog1.WriteEntry("OnCustomCommand : Stock by Min Max", EventLogEntryType.Information);
                    this.Interface_StockByMinMax(lastupdate);
                    break;
                case (int)ExecCommands.cmdDely:
                    eventLog1.WriteEntry("OnCustomCommand : Stock by Delivery", EventLogEntryType.Information);
                    this.Interface_DeliveryBoard(lastupdate);
                    this.Interface_DeliveryBoardDetail(lastupdate);
                    break;
                default:
                    eventLog1.WriteEntry("OnCustomCommand : Command not matching!!", EventLogEntryType.Error);
                    break;
            }
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        #endregion

        internal void DebugStart()
        {
            OnStart(null);
        }       


        #region Method Member

        private void oTimer_TimerCallback(object state)
        {
            eventLog1.WriteEntry("Timer Running...", EventLogEntryType.Information);

            oTimer.Change(Timeout.Infinite, Timeout.Infinite);

            lastupdate = DateTime.Now; 
            //Get data from Oracle Database to SQLite Database
            this.Interface_StockByCustomer(lastupdate);
            this.Interface_StockByMachine(lastupdate);
            this.Interface_StockByMinMax(lastupdate);
            this.Interface_DeliveryBoard(lastupdate);
            this.Interface_DeliveryBoardDetail(lastupdate);

            oTimer.Change(new TimeSpan(0, iPeriod, 0), new TimeSpan(0, iPeriod, 0));
        }

        private void Interface_StockByCustomer(DateTime lastupdate)
        {
            List<StockByCustomer> lstStkCust;
            try
            {
                //eventLog1.WriteEntry("Get Stock By Customer", EventLogEntryType.Information);
                //get data from oracle database
                using (QueryBLL oraQuery = new QueryBLL())
                {
                    lstStkCust = oraQuery.GetStockByCustomer();
                }

                // Insert data to SQLite database
                if (lstStkCust != null)
                {
                    //eventLog1.WriteEntry("SQLite Stock By Customer", EventLogEntryType.Information);
                    using (InterfaceBLL infBll = new InterfaceBLL())
                    {
                        infBll.InsertStockByCustomer(lstStkCust,lastupdate);

                        infBll.DeleteStockByCustomer();

                        infBll.UpdateStockByCustomer(); 
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void Interface_StockByMachine(DateTime lastupdate)
        {
            List<StockByMachine> lstStkMachine;
            try
            {
                //eventLog1.WriteEntry("Get Stock By Machine", EventLogEntryType.Information);
                //get data from oracle database
                using (QueryBLL oraQuery = new QueryBLL())
                {
                    lstStkMachine = oraQuery.GetStockByMachine();
                }

                // Insert data to SQLite database
                if (lstStkMachine != null)
                {
                    //eventLog1.WriteEntry("SQLite Stock By Machine", EventLogEntryType.Information);
                    using (InterfaceBLL infBll = new InterfaceBLL())
                    {
                        infBll.InsertStockByMachine(lstStkMachine,lastupdate);
                        infBll.DeleteStockByMachine(); 
                        infBll.UpdateStockByMachine();  
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void Interface_StockByMinMax(DateTime lastupdate)
        {
            List<StockByMinMax> lstStkMinMax;
            try
            {
                //eventLog1.WriteEntry("Get Stock By Min Max", EventLogEntryType.Information);
                //get data from oracle database
                using (QueryBLL oraQuery = new QueryBLL())
                {
                    lstStkMinMax = oraQuery.GetStockByMinMax();
                }

                // Insert data to SQLite database
                if (lstStkMinMax != null)
                {
                    //eventLog1.WriteEntry("SQLite Stock By MinMax", EventLogEntryType.Information);
                    using (InterfaceBLL infBll = new InterfaceBLL())
                    {
                        infBll.InsertStockByMinMax(lstStkMinMax, lastupdate);
                        infBll.DeleteStockByMinMax();
                        infBll.UpdateStockByMinMax();  
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void Interface_DeliveryBoard(DateTime lastupdate)
        {
            List<DeliveryBoard> lstDelivery;
            try
            {
                //eventLog1.WriteEntry("Get Stock By Delivery Board", EventLogEntryType.Information);
                //get data from oracle database
                using (QueryBLL oraQuery = new QueryBLL())
                {
                    lstDelivery = oraQuery.GetDeliveryBoard();
                }

                // Insert data to SQLite database
                if (lstDelivery != null)
                {
                    //eventLog1.WriteEntry("SQLite Stock By Delivery Board", EventLogEntryType.Information);
                    using (InterfaceBLL infBll = new InterfaceBLL())
                    {
                        infBll.InsertDeliveryBoard(lstDelivery, lastupdate);
                        infBll.DeleteDeliveryBoard();
                        infBll.UpdateDeliveryBoard();  
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void Interface_DeliveryBoardDetail(DateTime lastupdate)
        {
            List<DeliveryBoardDetail> lstDeliveryDetail;
            try
            {
                //eventLog1.WriteEntry("Get Stock By Delivery Board Detail", EventLogEntryType.Information);
                //get data from oracle database
                using (QueryBLL oraQuery = new QueryBLL())
                {
                    lstDeliveryDetail = oraQuery.GetDeliveryBoardDetail();
                }

                // Insert data to SQLite database
                if (lstDeliveryDetail != null)
                {
                    //eventLog1.WriteEntry("SQLite Stock By Delivery Board Detail", EventLogEntryType.Information);
                    using (InterfaceBLL infBll = new InterfaceBLL())
                    {
                        infBll.InsertDeliveryBoardDetail(lstDeliveryDetail, lastupdate);
                        infBll.DeleteDeliveryBoardDetail(); 
                        infBll.UpdateDeliveryBoardDetail();  
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        #endregion

    }
}
