using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using System.Data.OleDb;
using System.Linq;
using System.IO;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;
using HTN.BITS.LIB;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPopUpload_SO : BaseDialogForm, IDisposable
    {
        public frmPopUpload_SO()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdSO_HDR);
            base.LoadGridLayout(this.grdSO_DTL);
        }

        #region "Dialog Idle Time"

        ~frmPopUpload_SO()
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }
            base.DialogIdle.IdleAsync -= new EventHandler(this.DialogIdle_IdleAsync);
        }

        private void DialogIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.DialogIdle_Idle(sender, e); })
                );
        }

        private void DialogIdle_Idle(object sender, EventArgs e)
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion 

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region "Variable Member"

        private string _USER_ID;
        private string _SO_NO;

        #endregion

        #region "Property Member"

        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }

        public string SO_NO
        {
            get
            {
                return _SO_NO;
            }
            set
            {
                if (_SO_NO == value)
                    return;
                _SO_NO = value;
            }
        }

        #endregion

        #region Method Member

        private void InitializaLOVData()
        {
            try
            {
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    List<Warehouse> lstWH = shipOrdBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.grvSO_HDR_rps_WH.DataSource = lstWH;
                    }

                    List<Packaging> lstPackaging = shipOrdBll.GetPackaging();
                    if (lstPackaging != null)
                    {
                        this.grvSO_DTL_rps_luePACKAGING.DataSource = lstPackaging;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void UploadExcelFileToTemp()
        {
            string resultMsg = string.Empty;
            string filename = string.Empty;
            string connectionString = string.Empty;

            using (OpenFileDialog fdlg = new OpenFileDialog { Title = "Open Shipping Order Data File", InitialDirectory = @"My Documents:\", Filter = "New Excel files (*.xlsx)|*.xlsx|Excel files (*.xls)|*.xls", FilterIndex = 2, RestoreDirectory = true })
            {

                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    filename = fdlg.FileName;

                    //clear temp data
                    using (ShippingOrderBLL soBll = new ShippingOrderBLL())
                    {
                        soBll.ClearTempUploadSo();
                    }
                }
                else
                {
                    filename = string.Empty;
                }
            }


            if (filename != string.Empty)
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                switch(Path.GetExtension(filename).ToLower())
                {
                    case ".xls":
                        connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""", filename);
                        break;
                    case ".xlsx":
                        connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", filename);
                        break;
                    default:
                        break;
                }

                

                try
                {
                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        con.Open();
                        string sheetname = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

                        string query = string.Format(UiUtility.UPLOAD_SO_QUERY, sheetname);
                        OleDbDataAdapter da = new OleDbDataAdapter(query, con);

                        using (DataTable dtbTempUpload = new DataTable())
                        {
                            da.Fill(dtbTempUpload);
                            if (dtbTempUpload != null)
                            {
                                using (ShippingOrderBLL soBll = new ShippingOrderBLL())
                                {
                                    resultMsg = soBll.UploadSOToTemp(dtbTempUpload);
                                }
                            }
                        }
                    }

                    if (resultMsg.Equals("OK"))
                    {
                        this.GetUploadSO();
                    }
                }
                catch (OleDbException odex)
                {
                    base.FinishedProcessing();
                    XtraMessageBox.Show(odex.Message);
                }
                catch (Exception ex)
                {
                    base.FinishedProcessing();
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    base.FinishedProcessing();
                }
            }
        }

        private void GetUploadSO()
        {
            List<ShippingOrder> lstShipOrd = null;
            DataTable dtbShipDtl = null;
            int dupCount = 0;
            try
            {
                //base.ExecutionStart();
                //base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data", this);

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstShipOrd = shipOrdBll.GetUploadSO_HDR();

                    dtbShipDtl = shipOrdBll.GetUploadSO_DTL();
                }   
            
               

                this.grdSO_HDR.DataSource = lstShipOrd;

                if (dtbShipDtl != null)
                {
                    dupCount = this.CheckDuplicated(ref dtbShipDtl);
                }

                this.grdSO_DTL.DataSource = dtbShipDtl;

                this.ConditionsColumnView(this.grdSO_DTL);

                base.FinishedProcessing();

                if (lstShipOrd != null)
                {
                    if (lstShipOrd.Count == 1)
                    {
                        //check W/H
                        var wh = this.grvSO_HDR.GetRowCellDisplayText(0, "WH_ID");
                        if (string.IsNullOrEmpty(wh))
                        {
                            string message = string.Format("'{0}' W/H not Matching!!", lstShipOrd[0].WH_ID);
                            this.btnSave.Enabled = false;
                            XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        //check ETA date
                        var shippingOrd = from sp in lstShipOrd
                                          where (!sp.ETA.HasValue)
                                          select sp;

                        if (shippingOrd.Any())
                        {
                            string message = string.Format("{0} Shipping Order has no ETD.!!", shippingOrd.Count());
                            this.btnSave.Enabled = false;
                            XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    { 
                        this.btnSave.Enabled = false;
                        XtraMessageBox.Show(this, "Shipping Order Header Incorrect!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (dtbShipDtl != null)
                {

                    var rows = from row in dtbShipDtl.AsEnumerable()
                               where string.IsNullOrEmpty(row.Field<string>("PROD_SEQ_NO"))
                               select row;

                    if (rows.Any())
                    {
                        this.lblMismatch.Visible = true;

                        string message = string.Format("{0} Products not matching!!", rows.Count());
                        this.btnSave.Enabled = false;
                        XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    var rowEmpty = from row in dtbShipDtl.AsEnumerable()
                                   where string.IsNullOrEmpty(row.Field<string>("PO_NO"))
                                   select row;

                    if(rowEmpty.Any())
                    {
                        this.lblPOEmpty.Visible = true;
                        string message = string.Format("{0} Records has Empty PO#!!", rowEmpty.Count());
                        this.btnSave.Enabled = false;
                        XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                    
                }

                if (dupCount > 0)
                {
                    this.lblDuplicate.Visible = true;

                    string message = string.Format("{0} Duplicate Product# and PO# Record!!", dupCount);
                    this.btnSave.Enabled = false;
                    XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                               

                //base.ExecutionStop();
            }
            catch (Exception ex)
            {
                //base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                //((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
                
            }
        }

        private void ConditionsColumnView(GridControl grd)
        {
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[3];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["PROD_SEQ_NO"];
                cnArr[0].Expression = @"[PROD_SEQ_NO] IS NULL";
                cnArr[0].Appearance.BackColor = Color.Red;
                cnArr[0].Appearance.Options.UseBackColor = true;
                cnArr[0].Appearance.Options.UseForeColor = true;
                cnArr[0].ApplyToRow = true;

                cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[1].Column = ((ColumnView)grd.MainView).Columns["DUP_FLAG"];
                cnArr[1].Expression = @"[DUP_FLAG] = 'Y'";
                cnArr[1].Appearance.BackColor = Color.FromArgb(255, 128, 0);
                cnArr[1].Appearance.Options.UseBackColor = true;
                cnArr[1].Appearance.Options.UseForeColor = true;
                cnArr[1].ApplyToRow = true;

                cnArr[2] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[2].Column = ((ColumnView)grd.MainView).Columns["PO_NO"];
                cnArr[2].Expression = @"[EMPTY_FLAG] = 'Y'";
                cnArr[2].Appearance.BackColor = Color.Black;
                cnArr[2].Appearance.Options.UseBackColor = true;
                cnArr[2].Appearance.Options.UseForeColor = true;
                cnArr[2].ApplyToRow = false;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void InsertShippingOrder()
        {
            string result = string.Empty;
            ShippingOrder shippingOrd;

            try
            {
                #region "Shipping Order Header"

                var lstShippingOrd = this.grdSO_HDR.DataSource as List<ShippingOrder>;

                shippingOrd = lstShippingOrd[0];

                #endregion

                #region "Shipping Order Detail"

                DataTable dtbShippingDtl = this.grdSO_DTL.DataSource as DataTable;

                var drSelect = dtbShippingDtl.Select("[PROD_SEQ_NO] IS NOT NULL", "[LINE_NO]");

                ShippingOrderDtl shpOrdDtl;
                foreach (DataRow dr in drSelect)
                {
                    shpOrdDtl = new ShippingOrderDtl();

                    shpOrdDtl.SO_NO = dr["SO_NO"].ToString();
                    shpOrdDtl.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                    shpOrdDtl.UNIT_ID = dr["UNIT_ID"].ToString();
                    shpOrdDtl.PACKAGING = dr["PACKAGING"].ToString();
                    shpOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.UNIT_PRICE = (dr["UNIT_PRICE"] as decimal?) ?? 0.0M;
                    //shpOrdDtl.UNIT_PRICE = Convert.ToDecimal(dr["UNIT_PRICE"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.REMARK = dr["REMARK"].ToString();
                    shpOrdDtl.PO_NO = dr["PO_NO"].ToString();
                    shpOrdDtl.REC_STAT = true;

                    shippingOrd.AddItem(shpOrdDtl);
                }
                #endregion

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    result = shipOrdBll.InsertShippingOrder(ref shippingOrd, this.USER_ID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                    this._SO_NO = shippingOrd.SO_NO;

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private int CheckDuplicated(ref DataTable dtb)
        {
            int dupCount = 0;

            try
            {
                dtb.Columns.Add(new DataColumn("DUP_FLAG", typeof(System.String)) { DefaultValue = "N" });
                dtb.Columns.Add(new DataColumn("EMPTY_FLAG", typeof(System.String)) { DefaultValue = "N" });
                dtb.AcceptChanges();


                dtb.AsEnumerable()
                .Where(row => string.IsNullOrEmpty(row.Field<string>("PO_NO")))
                .ToList().ForEach(row => row["EMPTY_FLAG"] = "Y");


                var dupRow = dtb.AsEnumerable()
                             .GroupBy(row => new { prodno = row["PRODUCT_NO"], pono = row["PO_NO"]}) 
                             .Where(group => (group.Count() > 1)) 
                             .Select(g => g.Key);

                foreach (var prod in dupRow)
                {
                    dupCount++;

                    DataRow[] rows = dtb.Select(string.Format("[PRODUCT_NO] = '{0}' AND [PO_NO] = '{1}'", prod.prodno, prod.pono ));
                    foreach (DataRow row in rows)
                    {
                        row["DUP_FLAG"] = "Y";
                    }
                }

                dtb.AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return dupCount;
        }

        #endregion

        private void frmPopUpload_SO_LoadCompleted()
        {
            this.InitializaLOVData();
            this.UploadExcelFileToTemp();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.InsertShippingOrder();
        }
    }
}