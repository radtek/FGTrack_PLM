using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Reports;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;

namespace HTN.BITS.UIL.PLASESS.Utility
{
    public partial class frmQCReturn_WH : BaseChildForm
    {
        public frmQCReturn_WH()
        {
            InitializeComponent();

            this.CustomInitializeComponent();
            base.LoadFormLayout();

            base.LoadGridLayout(this.grdQCReturn);
            this.chkSelect = new GridCheckMarksSelection(this.grvQCReturn);
            this.chkSelect.ClearSelection();

            base.LoadGridLayout(this.grdQCReturnDetail);

        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbQCReturnDtl;

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
                return string.Format("QCReturn_WH_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("QCReturn_WH_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCReturn.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdQCReturnDetail.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
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
            this.bbiPrintFGReturnOrd.Glyph = base.Language.GetBitmap("PrintOrder");
            this.bbiPrintFGReturnDtl.Glyph = base.Language.GetBitmap("ReturnDocument");
            //this.bbiLoadingOrder.Glyph = base.Language.GetBitmap("ButtonLoadingNo");
            this.ddbExport.Image = base.Language.GetBitmap("ButtonExport");
            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            this.bbiExportPDF.Glyph = base.Language.GetBitmap("DropdownPDF");
            this.bbiExportRTF.Glyph = base.Language.GetBitmap("DropdownRTF");
            this.bbiExportText.Glyph = base.Language.GetBitmap("DropdownTXT");
            this.bbiExportHTML.Glyph = base.Language.GetBitmap("DropdownHTML");
            
        }

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = (GridView)this.grdQCReturnDetail.Views[0];
                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntQCReturn.Enabled = true;

                        this.dntQCReturn.TextStringFormat = "      Add Mode      ";
                        this.dntQCReturn.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnCancel.Enabled = true;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntQCReturn.Enabled = true;

                        this.dntQCReturn.TextStringFormat = "      Edit Mode      ";
                        this.dntQCReturn.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnCancel.Enabled = true;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntQCReturn.Enabled = false;

                        this.dntQCReturn.TextStringFormat = " Record {0} of {1} ";
                        this.dntQCReturn.Enabled = true;

                        this.btnPostData.Enabled = true;
                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Text = "Back";
                        this.btnCancel.Enabled = true;

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
            this.lueReturnType.Properties.ReadOnly = state;
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
            this.txtRT_NO.Text = string.Empty;
            this.txtRT_DATE.EditValue = DateTime.Now;
            this.lueWarehouse.EditValue = null;
            this.txtREMARK.Text = string.Empty;

            this.icbREC_STAT.EditValue = true;
            this.GetQCReturnDetail(string.Empty);
        }

        private void InitializaLOVData()
        {
            try
            {
                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    List<ReturnType> lstReturnType = qcReturnBll.GetReturnTypeList();

                    List<Warehouse> lstWH = qcReturnBll.GetWarehouse();

                    if (lstReturnType != null)
                    {
                        this.lueReturnType.Properties.DataSource = lstReturnType;
                        this.grvQCReturn_rps_lueReturnType.DataSource = lstReturnType;
                    }

                    if (lstWH != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstWH;
                        this.lueSearchWH.Properties.DataSource = lstWH;
                        this.grvQCReturn_rps_lueWH.DataSource = lstWH;
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

        public void GetQCReturnList(string whID, DateTime? fromDate, DateTime? toDate)
        {
            List<QCReturnHdr> lstQcReturn = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    lstQcReturn = qcReturnBll.GetQCReturnList(whID, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdQCReturn.DataSource = lstQcReturn;
                this.dntQCReturn.DataSource = lstQcReturn;
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

        public void GetQCReturnList(string findValue, string whID, DateTime? fromDate, DateTime? toDate)
        {
            List<QCReturnHdr> lstQcReturn = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    lstQcReturn = qcReturnBll.GetQCReturnList(findValue, whID, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdQCReturn.DataSource = lstQcReturn;
                this.dntQCReturn.DataSource = lstQcReturn;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void AdvanceSearchQCReturn(string returnNo, string whID, DateTime? fromDate, DateTime? toDate)
        {
            List<QCReturnHdr> lstQcReturn = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    lstQcReturn = qcReturnBll.AdvQCReturn(whID, returnNo, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdQCReturn.DataSource = lstQcReturn;
                this.dntQCReturn.DataSource = lstQcReturn;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void GetBindingQcReturn(string returnNo)
        {
            QCReturnHdr qcReturnHdr = null;
            try
            {
                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    qcReturnHdr = qcReturnBll.GetQCReturn(returnNo);
                }

                if (qcReturnHdr != null)
                {
                    this.txtRT_NO.Text = qcReturnHdr.RT_NO;
                    this.txtRT_DATE.EditValue = qcReturnHdr.RT_DATE;
                    this.txtPOST_REF.EditValue = qcReturnHdr.POST_REF;
                    this.lueWarehouse.EditValue = qcReturnHdr.WH_ID;
                    this.lueReturnType.EditValue = qcReturnHdr.RT_TYPE;
                    this.txtREMARK.Text = qcReturnHdr.REMARK;
                    this.icbREC_STAT.EditValue = qcReturnHdr.REC_STAT;

                    this.GetQCReturnDetail(qcReturnHdr.RT_NO);
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

        public void GetQCReturnDetail(string returnNo)
        {
            try
            {
                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    this.dtbQCReturnDtl = qcReturnBll.GetQCReturnDetail(returnNo);
                }

                if (this.dtbQCReturnDtl != null)
                {
                    this.grdQCReturnDetail.DataSource = this.dtbQCReturnDtl;
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

            if (this.lueReturnType.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueReturnType, "Return Type can't be Empty", ErrorType.Warning);
                this.lueReturnType.Focus();
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

        public void InsertQCReturn()
        {
            string result = string.Empty;
            QCReturnHdr qcReturn = new QCReturnHdr();

            try
            {
                #region "QC Return Header"

                qcReturn.RT_NO = string.Empty;
                qcReturn.RT_DATE = this.txtRT_DATE.DateTime;
                qcReturn.WH_ID = this.lueWarehouse.EditValue.ToString();
                qcReturn.RT_TYPE = this.lueReturnType.EditValue.ToString();
                qcReturn.REMARK = this.txtREMARK.Text;
                qcReturn.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    result = qcReturnBll.InsertQCReturn(ref qcReturn, ((frmMainMenu)this.ParentForm).UserID);
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
                this.txtRT_NO.Text = qcReturn.RT_NO;
                //this.txtRT_DATE.EditValue = qcReturn.RT_DATE;

                this.FormState = eFormState.ReadOnly;

                this.GetQCReturnList(this.lueWarehouse.EditValue.ToString(), null, null);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdQCReturn.Views[0];

                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "RT_NO", qcReturn.RT_NO);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntQCReturn.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }

                this.Cursor = Cursors.Default;
            }
        }

        public void UpdateQCReturn()
        {
            string result = string.Empty;
            QCReturnHdr qcReturn = new QCReturnHdr();

            try
            {
                #region "QC Return Header"

                qcReturn.RT_NO = this.txtRT_NO.Text;
                qcReturn.RT_DATE = this.txtRT_DATE.DateTime;
                qcReturn.WH_ID = this.lueWarehouse.EditValue.ToString();
                qcReturn.RT_TYPE = this.lueReturnType.EditValue.ToString();
                qcReturn.REMARK = this.txtREMARK.Text;
                qcReturn.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    result = qcReturnBll.UpdateQCReturn(qcReturn, ((frmMainMenu)this.ParentForm).UserID);
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
                this.GetQCReturnList(this.lueWarehouse.EditValue.ToString(), null, null);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdQCReturn.Views[0];
                    viewList.ClearSorting();

                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "RT_NO", qcReturn.RT_NO);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntQCReturn.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }

                this.Cursor = Cursors.Default;
            }
        }

        private void PrintQCReturnOrder(List<QCReturnHdr> lstQcReturn)
        {
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    ds = qcReturnBll.PrintQCReturnReport(lstQcReturn);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_FG_QC_RETURN rpt = new RPT_FG_QC_RETURN();

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

        private void PrintQCReturnOrderDetail(List<QCReturnHdr> lstQcReturn)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //UiUtility.BeginProcessing("Loading Report", this);

                DataSet ds;

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    ds = qcReturnBll.PrintQCReturnOrderDtlReport(lstQcReturn);
                }

                ReportViewer viewer = new ReportViewer { AutoCloseAfterPrint = true };

                RPT_FG_RETURN_WH rpt = new RPT_FG_RETURN_WH { DataSource = ds };
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
                rpt.CreateDocument();
                viewer.SetReport(rpt);
                viewer.ShowDialog();


            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                UiUtility.EndProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReturnWHPostData(List<QCReturnHdr> lstQcReturn)
        {
            try
            {
                ICollection<string> files;
                string selectPath = string.Empty;
                string resultMsg = string.Empty;

                DialogResult result = this.fdbSelectFilePath.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    selectPath = this.fdbSelectFilePath.SelectedPath;
                    try
                    {
                        using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                        {
                            resultMsg = qcReturnBll.GetPostData(lstQcReturn, selectPath, ((frmMainMenu)this.ParentForm).UserID, out files);
                        }

                        if (files.Count > 0)
                        {
                            //copy to server
                            UiUtility.CopyFilesToServer(files);

                            this.OpenPath(files);

                            //refresh data
                            if (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnList)
                            {
                                this.btnApply.PerformClick();
                            }
                            else
                            {
                                this.GetBindingQcReturn(this.txtRT_NO.Text);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void OpenPath(ICollection<string> files)
        {
            DialogResult message = XtraMessageBox.Show("Do you want to open directory?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (message == DialogResult.Yes)
                {
                    ShowSelectedInExplorer.FilesOrFolders(files);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        private void frmQCReturn_WH_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpQCReturnDetail.PageVisible = false;
            //this.InitializaLOVData();
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;

            //this.GetQCReturnList(string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmQCReturn_WH_LoadCompleted()
        {
            string wh = string.Empty;

            this.KeyPreview = true;

            this.xtpQCReturnDetail.PageVisible = false;

            this.InitializaLOVData();

            this.dteFromDate.DateTime = DateTime.Now;//UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now.AddDays(1d);

            if (this.lueSearchWH.EditValue != null)
            {
                wh = (string)this.lueSearchWH.EditValue;
            }

            this.GetQCReturnList(wh, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvQCReturn_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string returnNo = view.GetRowCellValue(info.RowHandle, "RT_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpQCReturnList.PageEnabled = false;

                    this.xtpQCReturnDetail.PageVisible = true;
                    this.xtcQCReturnWH.SelectedTabPage = this.xtpQCReturnDetail;

                    //Call record detail.
                    this.GetBindingQcReturn(returnNo);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntQCReturn_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdQCReturn.Views[0]; //this.gridView2

                if (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnDetail)
                {
                    string returnNo = view.GetFocusedRowCellValue("RT_NO").ToString();

                    this.GetBindingQcReturn(returnNo);
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

                        this.xtpQCReturnDetail.PageVisible = false;
                        this.xtpQCReturnList.PageEnabled = true;
                        this.xtcQCReturnWH.SelectedTabPage = this.xtpQCReturnList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                        this.GetBindingQcReturn(this.txtRT_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpQCReturnDetail.PageVisible = false;
                        this.xtpQCReturnList.PageEnabled = true;
                        this.xtcQCReturnWH.SelectedTabPage = this.xtpQCReturnList;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertQCReturn();
                    break;
                case eFormState.Edit:
                    this.UpdateQCReturn();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;

            this.ClearDataOnScreen();

            this.xtpQCReturnList.PageEnabled = false;
            this.xtpQCReturnDetail.PageVisible = true;
            this.xtcQCReturnWH.SelectedTabPage = this.xtpQCReturnDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.lueWarehouse.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void frmQCReturn_WH_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdQCReturn.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdQCReturnDetail.Views[0]);

            if (this.lueSearchWH.EditValue != null)
            {
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Default_WH = (string)this.lueSearchWH.EditValue;
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
            }

            this.Controls.Clear();
        }

        private void frmQCReturn_WH_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetBindingQcReturn(this.txtRT_NO.Text);
                            //this.GetQCReturnDetail(this.txtRT_NO.Text);
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
            this.GetQCReturnList(whID, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void bbiPrintFGReturnOrd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                //XtraMessageBox.Show("Print Loading Order");
                List<QCReturnHdr> lstQcReturn;
                GridView view = (GridView)this.grdQCReturn.Views[0];

                if (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnList)
                {
                    if (this.chkSelect.SelectedCount > 0)
                    {
                        lstQcReturn = new List<QCReturnHdr>(this.chkSelect.SelectedCount);
                        for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                        {
                            lstQcReturn.Add((QCReturnHdr)this.chkSelect.GetSelectedRow(i));
                        }

                        this.PrintQCReturnOrder(lstQcReturn);

                    }
                    else
                    {
                        //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        XtraMessageBox.Show(this, "PLEASE SELECT RETURN ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    lstQcReturn = new List<QCReturnHdr>(1);
                    //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                    QCReturnHdr qcReturn = new QCReturnHdr { RT_NO = this.txtRT_NO.Text };

                    lstQcReturn.Add(qcReturn);

                    this.PrintQCReturnOrder(lstQcReturn);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintFGReturnDtl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                //XtraMessageBox.Show("Print Loading Order");
                List<QCReturnHdr> lstQcReturn;
                GridView view = (GridView)this.grdQCReturn.Views[0];

                if (this.xtcQCReturnWH.SelectedTabPage == this.xtpQCReturnList)
                {
                    if (this.chkSelect.SelectedCount > 0)
                    {
                        lstQcReturn = new List<QCReturnHdr>(this.chkSelect.SelectedCount);
                        for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                        {
                            lstQcReturn.Add((QCReturnHdr)this.chkSelect.GetSelectedRow(i));
                        }

                        this.PrintQCReturnOrderDetail(lstQcReturn);

                    }
                    else
                    {
                        //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        XtraMessageBox.Show(this, "PLEASE SELECT RETURN ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    lstQcReturn = new List<QCReturnHdr>(1);
                    //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                    QCReturnHdr qcReturn = new QCReturnHdr { RT_NO = this.txtRT_NO.Text };

                    lstQcReturn.Add(qcReturn);

                    this.PrintQCReturnOrderDetail(lstQcReturn);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ddbPrint_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
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
                    this.GetQCReturnList(editor.Text, whID, null, null);
                    this.txtSearch.Text = string.Empty;
                    break;
                default:
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAdvQCReturn advQcReturn = new frmAdvQCReturn())
                {
                    UiUtility.ShowPopupForm(advQcReturn, this, true);

                    this.AdvanceSearchQCReturn(advQcReturn.RT_NO, advQcReturn.WH_ID, advQcReturn.FROM_DATE, advQcReturn.TO_DATE);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            List<QCReturnHdr> lstReturnOrd = new List<QCReturnHdr>(1);
            QCReturnHdr returnOrd = new QCReturnHdr { RT_NO = this.txtRT_NO.Text };
            lstReturnOrd.Add(returnOrd);
            this.ReturnWHPostData(lstReturnOrd);
        }

        private void lueReturnType_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Return Type Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }



        
    }
}