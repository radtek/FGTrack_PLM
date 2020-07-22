using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.UIL.PLASESS.Reports;
using DevExpress.XtraEditors.DXErrorProvider;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmLoadingOrder : BaseChildForm
    {
        public frmLoadingOrder()
        {
            InitializeComponent();

            this.CustomInitializeComponent();
            base.LoadFormLayout();

            base.LoadGridLayout(this.grdLoadingOrder);
            this.chkSelect = new GridCheckMarksSelection(this.grvLoadingOrder);
            base.LoadGridLayout(this.grdLoadingDtl);
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbLoadingOrderDtl;

        private GridCheckMarksSelection chkSelect;

        #endregion

        #region "Property Member"

        public eFormState FormState
        {
            get
            {
                return formState;
            }

            set
            {
                formState = value;
                this.ChangeFormState(value);
            }
        }

        #endregion

        #region "Button Export Referance"

        private string FileName
        {
            get
            {
                return string.Format("LoadingOrder_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("LoadingOrder_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdLoadingOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdLoadingDtl.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
            }
        }

        private void ddbExport_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }

        #endregion "Button Export Referance"

        #region "Method Member"

        private void CustomInitializeComponent()
        {
            this.ddbExport.Image = base.Language.GetBitmap("ButtonExport");

            this.ddbExport.Image = base.Language.GetBitmap("ButtonExport");
            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            this.bbiExportPDF.Glyph = base.Language.GetBitmap("DropdownPDF");
            this.bbiExportRTF.Glyph = base.Language.GetBitmap("DropdownRTF");
            this.bbiExportText.Glyph = base.Language.GetBitmap("DropdownTXT");
            this.bbiExportHTML.Glyph = base.Language.GetBitmap("DropdownHTML");

            this.bbiLoadingOrder.Glyph = base.Language.GetBitmap("PrintLoading");
            this.bbiDeliverySlip.Glyph = base.Language.GetBitmap("PrintDelivery");
            this.bbiPartListInPallet.Glyph = base.Language.GetBitmap("PrintPartList");
            this.bbiPartListInPallet.Enabled = false;
        }

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = (GridView)this.grdLoadingDtl.Views[0];

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntLoadingOrder.Enabled = true;

                        this.dntLoadingOrder.TextStringFormat = "      Add Mode      ";
                        this.dntLoadingOrder.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntLoadingOrder.Enabled = true;

                        this.dntLoadingOrder.TextStringFormat = "      Edit Mode      ";
                        this.dntLoadingOrder.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntLoadingOrder.Enabled = false;

                        this.dntLoadingOrder.TextStringFormat = " Record {0} of {1} ";
                        this.dntLoadingOrder.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";

                        this.GridDetail_OptionsCustomization(view, true);

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeControlState(bool state)
        {

            this.lueWarehouse.Properties.ReadOnly = state;
            this.txtTRUCK_NO.Properties.ReadOnly = state;
            this.txtCONTAINER_NO.Properties.ReadOnly = state;
            this.dtpDELIVERY_DATE.Properties.ReadOnly = state;
            this.dtpDELIVERY_DATE.Properties.Buttons[0].Enabled = !state;
            this.txtREMARK.Properties.ReadOnly = state;
            this.icbREC_STAT.Properties.ReadOnly = state;
        }

        private void GridDetail_OptionsCustomization(GridView view, bool state)
        {
            try
            {
                view.OptionsCustomization.AllowColumnResizing = state;
                view.OptionsCustomization.AllowFilter = state;

                //option filter
                view.OptionsFilter.AllowFilterEditor = state;
                view.OptionsFilter.AllowMRUFilterList = state;
                view.OptionsFilter.AllowColumnMRUFilterList = state;

                view.OptionsCustomization.AllowSort = state;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ClearDataOnScreen()
        {
            this.txtLOADING_NO.Text = string.Empty;
            this.txtLOADING_DATE.EditValue = DateTime.Now;
            this.lueWarehouse.EditValue = null;
            this.txtTRUCK_NO.Text = string.Empty;
            this.txtCONTAINER_NO.Text = string.Empty;
            this.dtpDELIVERY_DATE.EditValue = DateTime.Now;
            this.txtREMARK.Text = string.Empty;

            this.icbREC_STAT.EditValue = true;

            this.GetLoadingOrderDetail(string.Empty);
        }

        private void InitializaLOVData()
        {
            try
            {
                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    List<Warehouse> lstWH = loadingOrdBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstWH;
                        this.lueSearchWH.Properties.DataSource = lstWH;
                        this.grvLoadingOrder_rps_lueWH_ID.DataSource = lstWH;
                    }
                }

                string defaultWH = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Default_WH;

                if (!string.IsNullOrEmpty(defaultWH))
                    this.lueSearchWH.EditValue = defaultWH;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetLoadingOrderList(string whid, string findValue, DateTime? fromDate, DateTime? toDate)
        {
            List<LoadingOrder> lstLoadingOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    lstLoadingOrd = loadingOrdBll.GetLoadingOrderList(findValue, whid, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdLoadingOrder.DataSource = lstLoadingOrd;
                this.dntLoadingOrder.DataSource = lstLoadingOrd;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
                ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
            }
        }

        public void AdvanceSearchLoadingOrder(string loadingNo, string whid, DateTime? fromDate, DateTime? toDate)
        {
            List<LoadingOrder> lstLoadingOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    lstLoadingOrd = loadingOrdBll.AdvLoadingOrder(loadingNo, whid, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdLoadingOrder.DataSource = lstLoadingOrd;
                this.dntLoadingOrder.DataSource = lstLoadingOrd;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ClearErrorControl()
        {
            this.dxErrorProvider1.SetError(this.lueWarehouse, string.Empty, ErrorType.None);
        }

        public void GetBindingLoadingOrder(string loadingNo)
        {
            LoadingOrder loadingOrd = null;
            try
            {
                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    loadingOrd = loadingOrdBll.GetLoadingOrder(loadingNo);
                }

                if (loadingOrd != null)
                {
                    this.ClearErrorControl();

                    this.txtLOADING_NO.Text = loadingOrd.LOADING_NO;
                    this.txtLOADING_DATE.EditValue = loadingOrd.LOADING_DATE;
                    this.lueWarehouse.EditValue = loadingOrd.WH_ID;
                    
                    this.txtTRUCK_NO.Text = loadingOrd.TRUCK_NO;
                    this.txtCONTAINER_NO.Text = loadingOrd.CONTAINER_NO;
                    this.dtpDELIVERY_DATE.EditValue = loadingOrd.DELIVERY_DATE;
                    this.txtREMARK.Text = loadingOrd.REMARK;
                    this.icbREC_STAT.EditValue = loadingOrd.REC_STAT;

                    this.GetLoadingOrderDetail(loadingOrd.LOADING_NO);
                }
                else
                {
                    this.ClearDataOnScreen();
                    XtraMessageBox.Show(this, "No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void GetLoadingOrderDetail(string loadingNo)
        {
            try
            {
                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    this.dtbLoadingOrderDtl = loadingOrdBll.GetLoadingOrderDetail(loadingNo);
                }

                if (this.dtbLoadingOrderDtl != null)
                {
                    this.grdLoadingDtl.DataSource = this.dtbLoadingOrderDtl;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool IsFormValidated()
        {
            //Check control empty
            if (this.lueWarehouse.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueWarehouse, "Warehouse can't be Empty", ErrorType.Warning);
                this.lueWarehouse.Focus();
                return false;
            }

            if (this.dxErrorProvider1.HasErrors)
            {
                string controlType = this.dxErrorProvider1.GetControlsWithError()[0].GetType().ToString();
                switch (controlType)
                {
                    case "DevExpress.XtraEditors.TextEdit":

                        TextEdit tError = (TextEdit)this.dxErrorProvider1.GetControlsWithError()[0];
                        tError.Focus();
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        XtraMessageBox.Show(this, "Plase Input Valid value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        DateEdit dError = (DateEdit)this.dxErrorProvider1.GetControlsWithError()[0];
                        dError.Focus();
                        break;
                    case "DevExpress.XtraEditors.LookUpEdit":
                        LookUpEdit lError = (LookUpEdit)this.dxErrorProvider1.GetControlsWithError()[0];
                        lError.Focus();
                        break;
                    default:
                        break;
                }


                XtraMessageBox.Show(this, "Plase Input Valid value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);



                return false;
            }
            else
            {
                return true;
            }
        }

        public void InsertLoadingOrder()
        {
            string result = string.Empty;
            LoadingOrder loadingOrd = new LoadingOrder();

            try
            {
                #region "Loading Order Header"

                loadingOrd.LOADING_NO = string.Empty;
                loadingOrd.LOADING_DATE = DateTime.Now;
                loadingOrd.WH_ID = this.lueWarehouse.EditValue.ToString();
                loadingOrd.TRUCK_NO = this.txtTRUCK_NO.Text;
                loadingOrd.CONTAINER_NO = this.txtCONTAINER_NO.Text;
                loadingOrd.DELIVERY_DATE = this.dtpDELIVERY_DATE.DateTime;
                loadingOrd.REMARK = this.txtREMARK.Text;
                loadingOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    result = loadingOrdBll.InsertLoadingOrder(ref loadingOrd, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.txtLOADING_NO.Text = loadingOrd.LOADING_NO;
                this.txtLOADING_DATE.EditValue = loadingOrd.LOADING_DATE;

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.txtSearch.Text = loadingOrd.LOADING_NO;
                this.lueSearchWH.EditValue = null;

                this.GetLoadingOrderList(string.Empty, loadingOrd.LOADING_NO, null, null);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdLoadingOrder.Views[0];


                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "LOADING_NO", loadingOrd.LOADING_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntLoadingOrder.Position = position;
                //        }
                //    }
                //    else
                //    {
                //        viewList.FocusedRowHandle = 0;
                //    }

                //}

                this.Cursor = Cursors.Default;
            }
        }

        public void UpdateLoadingOrder()
        {
            string result = string.Empty;
            LoadingOrder loadingOrd = new LoadingOrder();

            try
            {
                #region "Loading Order Header"

                loadingOrd.LOADING_NO = this.txtLOADING_NO.Text;
                loadingOrd.LOADING_DATE = Convert.ToDateTime(this.txtLOADING_DATE.EditValue, DateTimeFormatInfo.CurrentInfo);
                loadingOrd.WH_ID = this.lueWarehouse.EditValue.ToString();
                loadingOrd.TRUCK_NO = this.txtTRUCK_NO.Text;
                loadingOrd.CONTAINER_NO = this.txtCONTAINER_NO.Text;
                loadingOrd.DELIVERY_DATE = this.dtpDELIVERY_DATE.DateTime;
                loadingOrd.REMARK = this.txtREMARK.Text;
                loadingOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    result = loadingOrdBll.UpdateLoadingOrder(loadingOrd, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Update Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                //this.GetLoadingOrderList(this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdLoadingOrder.Views[0];


                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "LOADING_NO", loadingOrd.LOADING_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntLoadingOrder.Position = position;
                //        }
                //    }
                //    else
                //    {
                //        viewList.FocusedRowHandle = 0;
                //    }

                //}

                this.Cursor = Cursors.Default;
            }
        }

        private void PrintLoadingOrder(List<LoadingOrder> lstLoadingOrd)
        {
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    ds = loadingOrdBll.PrintLoadingOrderReport(lstLoadingOrd);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_LOADING_ORDER rpt = new RPT_LOADING_ORDER();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
                rpt.CreateDocument();
                viewer.SetReport(rpt);
                base.FinishedProcessing();
                viewer.ShowDialog();

            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }

        private void PrintDeliverySlip(List<LoadingOrder> lstLoadingOrd)
        {
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    ds = loadingOrdBll.PrintDeliverySlipReport(lstLoadingOrd);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_DELIVERY_SLIP rpt = new RPT_DELIVERY_SLIP();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
                rpt.CreateDocument();
                viewer.SetReport(rpt);
                base.FinishedProcessing();
                viewer.ShowDialog();

            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }

        private void PrintPartListInPallet(string loadingNo)
        {
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    ds = loadingOrdBll.PrintPartListInPalletReport(loadingNo);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_PARTLIST_IN_PALLET rpt = new RPT_PARTLIST_IN_PALLET();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
                rpt.CreateDocument();
                viewer.SetReport(rpt);
                base.FinishedProcessing();
                viewer.ShowDialog();

            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }


        #endregion

        private void frmLoadingOrder_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpLoadingOrderDetail.PageVisible = false;
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();
            //this.GetLoadingOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmLoadingOrder_LoadCompleted()
        {
            string wh = string.Empty;

            this.KeyPreview = true;
            

            this.xtpLoadingOrderDetail.PageVisible = false;
            this.dteFromDate.DateTime = DateTime.Now;//UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now.AddDays(1d);

            this.InitializaLOVData();

            if (this.lueSearchWH.EditValue != null)
            {
                wh = (string)this.lueSearchWH.EditValue;
            }

            this.GetLoadingOrderList(wh, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvLoadingOrder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string loadingNo = view.GetRowCellValue(info.RowHandle, "LOADING_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpLoadingOrderList.PageEnabled = false;

                    this.xtpLoadingOrderDetail.PageVisible = true;
                    this.xtcLoadingOrder.SelectedTabPage = this.xtpLoadingOrderDetail;

                    //Call record detail.
                    this.GetBindingLoadingOrder(loadingNo);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntLoadingOrder_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdLoadingOrder.Views[0]; //this.gridView2

                if (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderDetail)
                {
                    string loadingNo = view.GetFocusedRowCellValue("LOADING_NO").ToString();

                    this.GetBindingLoadingOrder(loadingNo);
                }
                else
                {
                    UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Edit;
            this.lueWarehouse.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:
                        if (this.dxErrorProvider1.HasErrors)
                        {
                            this.dxErrorProvider1.ClearErrors();
                        }

                        this.xtpLoadingOrderDetail.PageVisible = false;
                        this.xtpLoadingOrderList.PageEnabled = true;
                        this.xtcLoadingOrder.SelectedTabPage = this.xtpLoadingOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                        this.GetBindingLoadingOrder(this.txtLOADING_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpLoadingOrderDetail.PageVisible = false;
                        this.xtpLoadingOrderList.PageEnabled = true;
                        this.xtcLoadingOrder.SelectedTabPage = this.xtpLoadingOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.xtpLoadingOrderList.PageEnabled = false;
            this.xtpLoadingOrderDetail.PageVisible = true;
            this.xtcLoadingOrder.SelectedTabPage = this.xtpLoadingOrderDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.lueWarehouse.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertLoadingOrder();
                    break;
                case eFormState.Edit:
                    this.UpdateLoadingOrder();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bbiLoadingOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraMessageBox.Show("Print Loading Order");
            List<LoadingOrder> lstLoadingOrd;
            GridView view = (GridView)this.grdLoadingOrder.Views[0];

            if (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderList)
            {
                if (this.chkSelect.SelectedCount > 0)
                {

                    //string[] arrivalNos = new string[check.SelectedCount];
                    //DataRow[] rows = new DataRow[check.SelectedCount];
                    lstLoadingOrd = new List<LoadingOrder>(this.chkSelect.SelectedCount);
                    for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                    {
                        lstLoadingOrd.Add((LoadingOrder)this.chkSelect.GetSelectedRow(i));
                        //arrivalNos[i] = view.GetRowCellDisplayText(i, "ARRIVAL_NO");
                    }

                    this.PrintLoadingOrder(lstLoadingOrd);

                }
                else
                {
                    //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XtraMessageBox.Show(this, "PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                lstLoadingOrd = new List<LoadingOrder>(1);
                //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                LoadingOrder loadingOrd = new LoadingOrder();
                loadingOrd.LOADING_NO = this.txtLOADING_NO.Text;

                lstLoadingOrd.Add(loadingOrd);

                this.PrintLoadingOrder(lstLoadingOrd);
            }
        }

        private void bbiDeliverySlip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraMessageBox.Show("Print Delivery Slip");
            List<LoadingOrder> lstLoadingOrd;
            GridView view = (GridView)this.grdLoadingOrder.Views[0];

            if (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderList)
            {
                if (this.chkSelect.SelectedCount > 0)
                {

                    //string[] arrivalNos = new string[check.SelectedCount];
                    //DataRow[] rows = new DataRow[check.SelectedCount];
                    lstLoadingOrd = new List<LoadingOrder>(this.chkSelect.SelectedCount);
                    for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                    {
                        lstLoadingOrd.Add((LoadingOrder)this.chkSelect.GetSelectedRow(i));
                        //arrivalNos[i] = view.GetRowCellDisplayText(i, "ARRIVAL_NO");
                    }

                    this.PrintDeliverySlip(lstLoadingOrd);

                }
                else
                {
                    //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XtraMessageBox.Show(this, "PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                lstLoadingOrd = new List<LoadingOrder>(1);
                //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                LoadingOrder loadingOrd = new LoadingOrder();
                loadingOrd.LOADING_NO = this.txtLOADING_NO.Text;

                lstLoadingOrd.Add(loadingOrd);

                this.PrintDeliverySlip(lstLoadingOrd);
            }
        }

        private void bbiPartListInPallet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtLOADING_NO.Text))
            {
                XtraMessageBox.Show(this, "Please Finish Loading Process!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            this.PrintPartListInPallet(this.txtLOADING_NO.Text);
        }

        private void ddbPrint_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }

        private void lueWarehouse_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Warehouse Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void frmLoadingOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdLoadingOrder.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdLoadingDtl.Views[0]);
            if (this.lueSearchWH.EditValue != null)
            {
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Default_WH = (string)this.lueSearchWH.EditValue;
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
            }

            this.Controls.Clear();
        }

        private void frmLoadingOrder_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetLoadingOrderDetail(this.txtLOADING_NO.Text);
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string whID = string.Empty;
            if (this.lueSearchWH.EditValue != null)
            {
                whID = this.lueSearchWH.EditValue.ToString();
            }
            this.GetLoadingOrderList(whID, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAdvLoadingOrder advLoadOrd = new frmAdvLoadingOrder())
                {
                    UiUtility.ShowPopupForm(advLoadOrd, this, true);

                    this.AdvanceSearchLoadingOrder(advLoadOrd.LOADING_NO, advLoadOrd.WH_ID, advLoadOrd.FROM_DATE, advLoadOrd.TO_DATE);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            if (string.IsNullOrEmpty(editor.Text)) return;

            string whID = string.Empty;
            if (this.lueSearchWH.EditValue != null)
            {
                whID = this.lueSearchWH.EditValue.ToString();
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetLoadingOrderList(whID, editor.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
                    break;
                default:
                    break;
            }
        }

        private void xtcLoadingOrder_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            bool isTabSelect = (this.xtcLoadingOrder.SelectedTabPage == this.xtpLoadingOrderList);
            this.bbiPartListInPallet.Enabled = !isTabSelect;//this.xtcLoadingOrder.SelectedTabPage.Equals(this.xtpLoadingOrderDetail);
        }

        
    }
}