using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmMaterialType : BaseChildFormPopup
    {
        public frmMaterialType()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMTLType);

            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;

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
                return string.Format("MaterialType_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcMaterial.SelectedTabPage == this.xtpMTLList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMTLType.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
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

                        this.dntMaterial.Enabled = true;

                        this.dntMaterial.TextStringFormat = "      Add Mode      ";
                        this.dntMaterial.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);
                        this.txtSEQ_NO.Properties.ReadOnly = true;

                        this.dntMaterial.Enabled = true;

                        this.dntMaterial.TextStringFormat = "      Edit Mode      ";
                        this.dntMaterial.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntMaterial.Enabled = false;

                        this.dntMaterial.TextStringFormat = " Record {0} of {1} ";
                        this.dntMaterial.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;

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
            this.txtSEQ_NO.Properties.ReadOnly = state;
            this.txtNAME.Properties.ReadOnly = state;
            this.txtREMARK.Properties.ReadOnly = state;
            this.icbREC_STAT.Properties.ReadOnly = state;
        }

        private void ClearDataOnScreen()
        {
            this.txtSEQ_NO.Text = string.Empty;
            this.txtNAME.Text = string.Empty;
            this.txtREMARK.Text = string.Empty;
            this.icbREC_STAT.EditValue = true;
        }

        private void GetMTLTypeList(string findValue)
        {
            List<MaterialType> lstMTLtype = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MaterialTypeBLL mtlTypeBll = new MaterialTypeBLL())
                {
                    lstMTLtype = mtlTypeBll.GetMTLTypeList(findValue);
                }

                this.grdMTLType.DataSource = lstMTLtype;
                this.dntMaterial.DataSource = lstMTLtype;

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

        private void GetBindingMTLType(string mtlTypeid)
        {
            MaterialType mtlType = null;
            try
            {
                using (MaterialTypeBLL mtlTypeBll = new MaterialTypeBLL())
                {
                    mtlType = mtlTypeBll.GetMTLType(mtlTypeid);
                }

                if (mtlType != null)
                {
                    this.txtSEQ_NO.Text = mtlType.SEQ_NO;
                    this.txtNAME.Text = mtlType.NAME;
                    this.txtREMARK.Text = mtlType.REMARK;
                    this.icbREC_STAT.EditValue = mtlType.REC_STAT;
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

                MaterialType mtlType = new MaterialType();

                mtlType.SEQ_NO = this.txtSEQ_NO.Text;
                mtlType.NAME = this.txtNAME.Text;
                mtlType.REMARK = this.txtREMARK.Text;
                mtlType.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (MaterialTypeBLL mtlTypeBll = new MaterialTypeBLL())
                {
                    result = mtlTypeBll.InsertMTLType(mtlType, ((frmMainMenu)this.ParentForm).UserID);
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

                this.GetMTLTypeList(string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdMTLType.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "SEQ_NO", this.txtSEQ_NO.Text);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntMaterial.Position = position;
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

                MaterialType mtlType = new MaterialType();

                mtlType.SEQ_NO = this.txtSEQ_NO.Text;
                mtlType.NAME = this.txtNAME.Text;
                mtlType.REMARK = this.txtREMARK.Text;
                mtlType.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (MaterialTypeBLL mtlTypeBll = new MaterialTypeBLL())
                {
                    result = mtlTypeBll.UpdateMTLType(mtlType, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdMTLType.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("NAME", mtlType.NAME);
                    view.SetFocusedRowCellValue("REMARK", mtlType.REMARK);
                    view.SetFocusedRowCellValue("REC_STAT", mtlType.REC_STAT);

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

        #endregion

        private void frmMaterialType_Load(object sender, EventArgs e)
        {
            //this.xtpMTLDetail.PageVisible = false;

            //this.GetMTLTypeList(string.Empty);

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmMaterialType_LoadCompleted()
        {
            this.xtpMTLDetail.PageVisible = false;

            this.GetMTLTypeList(string.Empty);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvMTLType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string mtlTypeID = view.GetRowCellValue(info.RowHandle, "SEQ_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpMTLList.PageEnabled = false;

                    this.xtpMTLDetail.PageVisible = true;
                    this.xtcMaterial.SelectedTabPage = this.xtpMTLDetail;

                    //Call record detail.
                    this.GetBindingMTLType(mtlTypeID);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntMaterial_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdMTLType.Views[0]; //this.gridView2

                if (this.xtcMaterial.SelectedTabPage == this.xtpMTLDetail)
                {
                    string mtltypeid = view.GetFocusedRowCellValue("SEQ_NO").ToString();

                    this.GetBindingMTLType(mtltypeid);
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
            this.txtNAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                switch (this.FormState)
                {
                    case eFormState.Add:
                        this.xtpMTLDetail.PageVisible = false;
                        this.xtpMTLList.PageEnabled = true;
                        this.xtcMaterial.SelectedTabPage = this.xtpMTLList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;
                    case eFormState.Edit:

                        this.GetBindingMTLType(this.txtSEQ_NO.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpMTLDetail.PageVisible = false;
                        this.xtpMTLList.PageEnabled = true;
                        this.xtcMaterial.SelectedTabPage = this.xtpMTLList;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.xtpMTLDetail.PageVisible = true;
            this.xtcMaterial.SelectedTabPage = this.xtpMTLDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.txtNAME.Focus();
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.GetMTLTypeList(this.txtSearch.Text);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetMTLTypeList(editor.Text);
                    break;
                default:
                    break;
            }
        }

        private void frmMaterialType_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdMTLType.Views[0]);
            this.Controls.Clear();
        }


    }
}