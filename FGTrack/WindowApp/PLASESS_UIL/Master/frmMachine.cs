using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.UIL.PLASESS.Component;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.UIL.PLASESS.Reports;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmMachine : BaseChildFormPopup
    {
        public frmMachine()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMachine);
            this.chkSelect = new GridCheckMarksSelection(this.grvMachine);
            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
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
                return string.Format("Machine_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcMachine.SelectedTabPage == this.xtpMachineList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMachine.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
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
                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntMachine.Enabled = true;

                        this.dntMachine.TextStringFormat = "      Add Mode      ";
                        this.dntMachine.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = @"&Cancel";

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);
                        this.txtMC_NO.Properties.ReadOnly = true;

                        this.dntMachine.Enabled = true;

                        this.dntMachine.TextStringFormat = "      Edit Mode      ";
                        this.dntMachine.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = @"&Cancel";

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntMachine.Enabled = false;

                        this.dntMachine.TextStringFormat = " Record {0} of {1} ";
                        this.dntMachine.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = @"&Back";

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
            this.txtMC_NO.Properties.ReadOnly = state;
            this.txtMACHINE_NAME.Properties.ReadOnly = state;
            this.lueMACHINE_TYPE.Properties.ReadOnly = state;
            this.txtMACHINE_SIZE.Properties.ReadOnly = state;
            this.txtREMARK.Properties.ReadOnly = state;
            this.icbREC_STAT.Properties.ReadOnly = state;
        }

        private void ClearDataOnScreen()
        {
            this.txtMC_NO.Text = string.Empty;
            this.txtMACHINE_NAME.Text = string.Empty;
            this.lueMACHINE_TYPE.EditValue = null;
            this.txtMACHINE_SIZE.Text = string.Empty;
            this.txtREMARK.Text = string.Empty;
            this.icbREC_STAT.EditValue = true;
        }

        private void InitializaLOVProductionType()
        {
            List<ProductionType> lstProductionType = null;
            try
            {
                using (ProductionTypeBLL pdtTypeBll = new ProductionTypeBLL())
                {
                    lstProductionType = pdtTypeBll.GetProductionTypeList();
                }

                if (lstProductionType != null)
                {
                    //for productionTypeList
                    this.lueProductType.Properties.DataSource = lstProductionType;
                    this.grvMachine_rps_lueMACHINE_TYPE.DataSource = lstProductionType;
                    this.lueMACHINE_TYPE.Properties.DataSource = lstProductionType;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); 
            }

        }

        private void GetMachineList(string mcType)
        {
            List<Machine> lstMachine = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MachineBLL mcBll = new MachineBLL())
                {
                    lstMachine = mcBll.GetMachineList(mcType);
                }

                this.grdMachine.DataSource = lstMachine;
                this.dntMachine.DataSource = lstMachine;

                this.chkSelect.ClearSelection();
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

        private void GetBindingMachine(string mcNo)
        {
            Machine mc = null;
            try
            {
                using (MachineBLL mcBll = new MachineBLL())
                {
                    mc = mcBll.GetMachine(mcNo);
                }

                if (mc != null)
                {
                    this.txtMC_NO.Text = mc.MC_NO;
                    this.txtMACHINE_NAME.Text = mc.MACHINE_NAME;
                    this.lueMACHINE_TYPE.EditValue = mc.MACHINE_TYPE;
                    this.txtMACHINE_SIZE.Text = mc.MACHINE_SIZE;
                    this.txtREMARK.Text = mc.REMARK;
                    this.icbREC_STAT.EditValue = mc.REC_STAT;
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

        private bool IsFormValidated()
        {
            return true;
        }

        private void InsertMachine()
        {
            string result = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Machine mc = new Machine();

                mc.MC_NO = this.txtMC_NO.Text;
                mc.MACHINE_NAME = this.txtMACHINE_NAME.Text;
                mc.MACHINE_TYPE = (string)this.lueMACHINE_TYPE.EditValue;
                mc.MACHINE_SIZE = this.txtMACHINE_SIZE.Text;
                mc.REMARK = this.txtREMARK.Text;
                mc.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                
                using (MachineBLL mcBll = new MachineBLL())
                {
                    result = mcBll.InsertMachine(mc, ((frmMainMenu)this.ParentForm).UserID);
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
                this.Cursor = Cursors.Default;
                this.FormState = eFormState.ReadOnly;

                this.lueMACHINE_TYPE.EditValue = null;
                this.GetMachineList(string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdMachine.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "MC_NO", this.txtMC_NO.Text);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntMachine.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }
        }

        private void UpdateMachine()
        {
            string result = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Machine mc = new Machine();

                mc.MC_NO = this.txtMC_NO.Text;
                mc.MACHINE_NAME = this.txtMACHINE_NAME.Text;
                mc.MACHINE_TYPE = (string)this.lueMACHINE_TYPE.EditValue;
                mc.MACHINE_SIZE = this.txtMACHINE_SIZE.Text;
                mc.REMARK = this.txtREMARK.Text;
                mc.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (MachineBLL mcBll = new MachineBLL())
                {
                    result = mcBll.UpdateMachine(mc, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdMachine.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("MACHINE_NAME", mc.MACHINE_NAME);
                    view.SetFocusedRowCellValue("MACHINE_TYPE", mc.MACHINE_TYPE);
                    view.SetFocusedRowCellValue("MACHINE_SIZE", mc.MACHINE_SIZE);
                    view.SetFocusedRowCellValue("REMARK", mc.REMARK);
                    view.SetFocusedRowCellValue("REC_STAT", mc.REC_STAT);

                    view.EndDataUpdate();

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
                this.Cursor = Cursors.Default;
                this.FormState = eFormState.ReadOnly;
            }
        }

        private void DeleteMachine()
        {

        }

        private void PrintMachine(List<Machine> lstMachine)
        {
            string userid = string.Empty;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");
                //UiUtility.BeginProcessing("Loading Report", this);
                userid = ((frmMainMenu)this.ParentForm).UserID;
                DataSet ds;

                using (MachineBLL mcBll = new MachineBLL())
                {
                    ds = mcBll.PrintMachineReport(lstMachine);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_MACHINE rpt = new RPT_MACHINE();

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


        private void frmMachine_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpMachineDetail.PageVisible = false;
            //this.InitializaLOVProductionType();

            //this.lueMACHINE_TYPE.EditValue = null;
            //this.GetMachineList(string.Empty);

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmMachine_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtpMachineDetail.PageVisible = false;
            this.InitializaLOVProductionType();

            this.lueMACHINE_TYPE.EditValue = null;
            this.GetMachineList(string.Empty);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvMachine_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string mcNo = view.GetRowCellValue(info.RowHandle, "MC_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpMachineList.PageEnabled = false;

                    this.xtpMachineDetail.PageVisible = true;
                    this.xtcMachine.SelectedTabPage = this.xtpMachineDetail;

                    //Call record detail.
                    this.GetBindingMachine(mcNo);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); 
            }
        }

        private void dntMachine_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdMachine.Views[0]; //this.gridView2

                if (this.xtcMachine.SelectedTabPage == this.xtpMachineDetail)
                {
                    string mcNo = view.GetFocusedRowCellValue("MC_NO").ToString();

                    this.GetBindingMachine(mcNo);
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
            this.txtMACHINE_NAME.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                switch (this.FormState)
                {
                    case eFormState.Add:
                        this.xtpMachineDetail.PageVisible = false;
                        this.xtpMachineList.PageEnabled = true;
                        this.xtcMachine.SelectedTabPage = this.xtpMachineList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;
                    case eFormState.Edit:

                        this.GetBindingMachine(this.txtMC_NO.Text);
                            
                        break;
                    case eFormState.ReadOnly:

                        this.xtpMachineDetail.PageVisible = false;
                        this.xtpMachineList.PageEnabled = true;
                        this.xtcMachine.SelectedTabPage = this.xtpMachineList;

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

            this.xtpMachineDetail.PageVisible = true;
            this.xtcMachine.SelectedTabPage = this.xtpMachineDetail;

            if (this.lueProductType.EditValue == null)
            {
                this.lueMACHINE_TYPE.EditValue = "V";
            }
            else
            {
                this.lueMACHINE_TYPE.EditValue = this.lueProductType.EditValue;
            }
                        
            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertMachine();
                    break;
                case eFormState.Edit:
                    this.UpdateMachine();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnPrintMC_Click(object sender, EventArgs e)
        {
            try
            {

                //XtraMessageBox.Show("Print Loading Order");
                List<Machine> lstMachine;
                GridView view = (GridView)this.grdMachine.Views[0];

                if (this.xtcMachine.SelectedTabPage == this.xtpMachineList)
                {
                    if (this.chkSelect.SelectedCount > 0)
                    {

                        //string[] arrivalNos = new string[check.SelectedCount];
                        //DataRow[] rows = new DataRow[check.SelectedCount];
                        lstMachine = new List<Machine>(this.chkSelect.SelectedCount);
                        for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                        {
                            lstMachine.Add((Machine)this.chkSelect.GetSelectedRow(i));
                            //arrivalNos[i] = view.GetRowCellDisplayText(i, "ARRIVAL_NO");
                        }

                        this.PrintMachine(lstMachine);

                    }
                    else
                    {
                        XtraMessageBox.Show(this, "PLEASE SELECT MACHINE TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    lstMachine = new List<Machine>(1);
                    //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                    Machine mc = new Machine();
                    mc.MC_NO = this.txtMC_NO.Text;

                    lstMachine.Add(mc);

                    this.PrintMachine(lstMachine);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmMachine_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcMachine.SelectedTabPage == this.xtpMachineList)
                        {
                            string mcType = string.Empty;
                            if (this.lueMACHINE_TYPE.EditValue != null)
                                mcType = (string)this.lueMACHINE_TYPE.EditValue;

                            this.GetMachineList(mcType);
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void frmMachine_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdMachine.Views[0]);
            this.Controls.Clear();
        }

        private void lueProductType_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = sender as LookUpEdit;
            if (editor.EditValue != null)
                this.GetMachineList((string)editor.EditValue);
            else
                this.GetMachineList(string.Empty);
        }



    }
}