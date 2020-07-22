using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HTN.BITS.SQLITE.DAL;
using HTN.BITS.FGTDB.BEL;

namespace HTN.BITS.SQLITE.BLL
{
    public class QuerySqliteBLL : IDisposable
    {
        public QuerySqliteBLL()
        { }

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

        ~QuerySqliteBLL()
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

        public DataTable GetStockbyCustomer()
        {
            try
            {

                string sql = StoreProcedure.Instance.GetScript("GetStockByCustomer");
                DataTable dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStockbyMachine()
        {
           try
            {

                string sql = StoreProcedure.Instance.GetScript("GetStockByMachine");
                DataTable dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStockbyMinmax(string s1,string s2, string s3)
        {   
            try
            {
                DataTable dtResult;

                string sql = string.Empty;

                    SQLiteParam param = new SQLiteParam(3) { CommandText = StoreProcedure.Instance.GetScript("GetStockByMinMax_Status") };
                    param.ParamStringFixedLength(0, "@strS1", s1);
                    param.ParamStringFixedLength(1, "@strS2", s2);
                    param.ParamStringFixedLength(2, "@strS3", s3);
                    dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(param);                  
                              

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDelivery()
        {
            try
            {

                string sql = StoreProcedure.Instance.GetScript("GetDeliveryBoard");
                DataTable dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }       

        public DataTable GetDeliveryBoardControl()
        {

            DataTable dtResult = null;

            try
            {

                string sql = StoreProcedure.Instance.GetScript("GetDeliveryBoard");
                DataTable dtTemp = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);

                dtResult = this.GetPivotDeliveryBoard(dtTemp, "ETD_DATE", "PARTY_ID", "ETD_DATE", "-",-1,string.Empty);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private DataTable GetPivotDeliveryBoard(DataTable table, string columnX, string columnY, string columnZ, string nullValue, int seqFilter, string wh)
        {
            List<DateTime> listDate = this.Get3DayWithoutSun(DateTime.Now);

            DataTable dtCust = this.GetCustomerWH_ETD();

            //List<string> lstExpression = new List<string>(listDate.Count);

            //Create a DataTable to Return
            DataTable returnTable = new DataTable();

            try
            {
                returnTable.BeginInit();

                //Add a Column at the beginning of the table
                DataColumn[] colFix = new DataColumn[4];

                colFix[0] = new DataColumn("PARTY_ID", typeof(string));
                colFix[1] = new DataColumn("PARTY_NAME", typeof(string));
                colFix[2] = new DataColumn("WH_ID", typeof(string));
                colFix[3] = new DataColumn("ETD_TIME", typeof(string));

                returnTable.Columns.AddRange(colFix);

                returnTable.EndInit();

                //string colName = string.Empty;
                int colIndex = 0;
                DataColumn[] colExtra = new DataColumn[listDate.Count * 5];

                foreach (DateTime dt in listDate)
                {
                    colExtra[colIndex++] = new DataColumn(string.Format("D{0:ddMMyy}_ETD_DATE", dt), typeof(string));
                    colExtra[colIndex++] = new DataColumn(string.Format("D{0:ddMMyy}_ETD_TIME", dt), typeof(string));
                    colExtra[colIndex++] = new DataColumn(string.Format("D{0:ddMMyy}_WH_ID", dt), typeof(string));
                    colExtra[colIndex++] = new DataColumn(string.Format("D{0:ddMMyy}_STATUS", dt), typeof(string));
                    colExtra[colIndex++] = new DataColumn(string.Format("D{0:ddMMyy}_RESPONSIBLE", dt), typeof(string));
                }

                returnTable.BeginInit();

                returnTable.Columns.AddRange(colExtra);

                returnTable.EndInit();

                //Verify if Y and Z Axis columns re provided
                if (columnY != "" && columnZ != "")
                {
                    List<PartyDeliTime> lstPartyDeliTime = new List<PartyDeliTime>();
                    PartyDeliTime partyDeliTime;

                    foreach (DataRow dr in dtCust.Rows)
                    {
                        partyDeliTime = new PartyDeliTime();

                        partyDeliTime.PARTY_ID = dr["PARTY_ID"].ToString();
                        partyDeliTime.PARTY_NAME = dr["PARTY_NAME"].ToString();
                        partyDeliTime.WH_ID = dr["WH_ID"].ToString();
                        partyDeliTime.ETD_TIME = dr["ETD_TIME"].ToString();

                        if (!lstPartyDeliTime.Contains(partyDeliTime))
                            lstPartyDeliTime.Add(partyDeliTime);
                    }

                    string query = string.Empty;
                    string tmpStatus = string.Empty, curStatus = string.Empty;

                    foreach (PartyDeliTime partyDelTi in lstPartyDeliTime)
                    {
                        DataRow drReturn = returnTable.NewRow();

                        drReturn["PARTY_ID"] = partyDelTi.PARTY_ID;
                        drReturn["PARTY_NAME"] = partyDelTi.PARTY_NAME;
                        drReturn["WH_ID"] = partyDelTi.WH_ID;
                        drReturn["ETD_TIME"] = partyDelTi.ETD_TIME;

                        if (!string.IsNullOrEmpty(partyDelTi.WH_ID))
                        {
                            query = string.Format("[PARTY_ID] = '{0}' AND [WH_ID] = '{1}' AND [ETD_TIME] = '{2}'", partyDelTi.PARTY_ID, partyDelTi.WH_ID, partyDelTi.ETD_TIME);
                            DataRow[] rows = table.Select(query);

                            if (rows.Any())
                            {
                                //not over 3 rows
                                foreach (DataRow dr in rows)
                                {
                                    //string rowColumnTitle = string.Format("D{0:ddMMyy}", dr[columnX]);
                                    drReturn[string.Format("D{0:ddMMyy}_ETD_DATE", dr[columnX])] = string.Format("{0:yyyyMMdd}", dr[columnX]);
                                    drReturn[string.Format("D{0:ddMMyy}_ETD_TIME", dr[columnX])] = dr["ETD_TIME"];
                                    drReturn[string.Format("D{0:ddMMyy}_WH_ID", dr[columnX])] = dr["WH_ID"];

                                    tmpStatus = drReturn[string.Format("D{0:ddMMyy}_STATUS", dr[columnX])].ToString().ToLower();
                                    curStatus = dr["STATUS"].ToString();

                                    switch (tmpStatus)
                                    {
                                        case "delay":

                                            break;
                                        case "not compt":
                                            if (curStatus.ToLower().Equals("delay"))
                                            {
                                                drReturn[string.Format("D{0:ddMMyy}_STATUS", dr[columnX])] = curStatus;
                                            }
                                            break;
                                        case "pick-load":
                                            if (curStatus.ToLower().Equals("delay") || curStatus.ToLower().Equals("not compt"))
                                            {
                                                drReturn[string.Format("D{0:ddMMyy}_STATUS", dr[columnX])] = curStatus;
                                            }
                                            break;
                                        case "complete":
                                            if (curStatus.ToLower().Equals("delay") || curStatus.ToLower().Equals("not compt") || curStatus.ToLower().Equals("pick-load"))
                                            {
                                                drReturn[string.Format("D{0:ddMMyy}_STATUS", dr[columnX])] = curStatus;
                                            }
                                            break;
                                        default:
                                            drReturn[string.Format("D{0:ddMMyy}_STATUS", dr[columnX])] = curStatus;
                                            break;

                                    }


                                    drReturn[string.Format("D{0:ddMMyy}_RESPONSIBLE", dr[columnX])] = dr["RESPONSIBLE"];

                                }
                            }

                        }
                        else
                        {
                            foreach (DateTime dt in listDate)
                            {
                                drReturn[string.Format("D{0:ddMMyy}_ETD_DATE", dt)] = string.Empty;
                                drReturn[string.Format("D{0:ddMMyy}_ETD_TIME", dt)] = string.Empty;
                                drReturn[string.Format("D{0:ddMMyy}_WH_ID", dt)] = string.Empty;
                                drReturn[string.Format("D{0:ddMMyy}_STATUS", dt)] = "No plan";
                                drReturn[string.Format("D{0:ddMMyy}_RESPONSIBLE", dt)] = string.Empty;
                            }
                        }

                        returnTable.Rows.Add(drReturn);
                    }

                    returnTable.EndLoadData();

                }
                else
                {
                    throw new Exception("The columns to perform inversion are not provided");
                }



                int colInx = 1;
                foreach (DateTime dt in listDate)
                {
                    returnTable.Columns[string.Format("D{0:ddMMyy}_ETD_DATE", dt)].ColumnName = string.Format("D{0}_ETD_DATE", colInx);
                    returnTable.Columns[string.Format("D{0:ddMMyy}_ETD_TIME", dt)].ColumnName = string.Format("D{0}_ETD_TIME", colInx);
                    returnTable.Columns[string.Format("D{0:ddMMyy}_WH_ID", dt)].ColumnName = string.Format("D{0}_WH_ID", colInx); ;
                    returnTable.Columns[string.Format("D{0:ddMMyy}_STATUS", dt)].ColumnName = string.Format("D{0}_STATUS", colInx);
                    returnTable.Columns[string.Format("D{0:ddMMyy}_RESPONSIBLE", dt)].ColumnName = string.Format("D{0}_RESPONSIBLE", colInx);

                    colInx++;
                }

                //if a nullValue is provided, fill the datable with it
                if (nullValue != "")
                {
                    foreach (DataRow dr in returnTable.Rows)
                    {
                        foreach (DataColumn dc in returnTable.Columns)
                        {
                            if (dc.ColumnName.Substring(2) == "_STATUS")
                            {
                                if (dr[dc.ColumnName].ToString() == "")
                                    dr[dc.ColumnName] = "No plan";
                            }
                            else
                            {
                                if (dr[dc.ColumnName].ToString() == "")
                                    dr[dc.ColumnName] = nullValue;
                            }
                        }
                    }
                }



                returnTable.AcceptChanges();
                returnTable.DefaultView.Sort = "PARTY_ID, WH_ID, ETD_TIME";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnTable;

        }

        private DataTable GetCustomerWH_ETD()
        {
            DataTable dtResult = null;

            try
            {
                string sql = StoreProcedure.Instance.GetScript("GetDeliveryCustomer");
                dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(sql);               

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public DataTable GetDeliveryDTL(string party, DateTime date, string wh,string status)
        {
            try
            {
                SQLiteParam param;

                if (status.ToLower() == "delay")
                {
                    param = new SQLiteParam(3) { CommandText = StoreProcedure.Instance.GetScript("GetDeliveryBoardDetail_Delay") };
                    param.ParamStringFixedLength(0, "@strWH_ID", wh);
                    param.ParamStringFixedLength(1, "@strPARTY_ID", party);
                    param.ParamDateTime(2, "@strETD_DATE", date);
                }
                else
                {
                    param = new SQLiteParam(3) { CommandText = StoreProcedure.Instance.GetScript("GetDeliveryBoardDetail") };
                    param.ParamStringFixedLength(0, "@strWH_ID", wh);
                    param.ParamStringFixedLength(1, "@strPARTY_ID", party);
                    param.ParamDateTime(2, "@strETD_DATE", date);
                }
                 
                DataTable dtResult = GlobalSqliteDB.Instance.DataAc.GetDataTable(param);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DateTime> Get3DayWithoutSun(DateTime date)
        {
            List<DateTime> listDt = new List<DateTime>();
            
            Int32 d = 1;
            for (d = 0; d <= 2; d++)
            {
                if (date.DayOfWeek.ToString() == "Sunday")
                {
                    d = d - 1;
                    date = date.AddDays(1);
                }
                else
                {
                    listDt.Add(date);
                    date = date.AddDays(1);
                }

            }

            return listDt;

        }

        #endregion

    }
}
