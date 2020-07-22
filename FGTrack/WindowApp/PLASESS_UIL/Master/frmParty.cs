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
using HTN.BITS.UIL.PLASESS.LOVForms;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmParty : BaseChildFormPopup
    {
        public frmParty()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdParty);
            base.LoadGridLayout(this.grdProductList);

            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbPartyProduct = null;

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
                return string.Format("Party_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcParty.SelectedTabPage == this.xtpPartyList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdParty.Views[0] as GridView;

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
                GridView view = this.grdParty.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdParty.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdParty.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdParty.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdParty.Views[0] as GridView;
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

                        this.dntParty.Enabled = true;

                        this.dntParty.TextStringFormat = "      Add Mode      ";
                        this.dntParty.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        this.btnSelectProd.Enabled = false;

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntParty.Enabled = true;

                        this.dntParty.TextStringFormat = "      Edit Mode      ";
                        this.dntParty.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        this.btnSelectProd.Enabled = true;

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntParty.Enabled = false;

                        this.dntParty.TextStringFormat = " Record {0} of {1} ";
                        this.dntParty.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;

                        this.btnSelectProd.Enabled = false;

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
            this.txtPARTY_NAME.Properties.ReadOnly = state;
            this.icbPARTY_TYPE.Properties.ReadOnly = state;
            this.txtADD1.Properties.ReadOnly = state;
            this.txtADD2.Properties.ReadOnly = state;
            this.txtADD3.Properties.ReadOnly = state;
            this.txtADD4.Properties.ReadOnly = state;
            this.txtTEL.Properties.ReadOnly = state;
            this.txtFAX.Properties.ReadOnly = state;
            this.txtEMAIL.Properties.ReadOnly = state;
            this.txtPIC.Properties.ReadOnly = state;
            this.txtREMARK.Properties.ReadOnly = state;
            this.icbREC_STAT.Properties.ReadOnly = state;
        }

        private void ClearDataOnScreen()
        {
            this.txtPARTY_ID.Text = string.Empty;
            this.txtPARTY_NAME.Text = string.Empty;
            this.icbPARTY_TYPE.SelectedIndex = 1;
            this.txtADD1.Text = string.Empty;
            this.txtADD2.Text = string.Empty;
            this.txtADD3.Text = string.Empty;
            this.txtADD4.Text = string.Empty;
            this.txtTEL.Text = string.Empty;
            this.txtFAX.Text = string.Empty;
            this.txtEMAIL.Text = string.Empty;
            this.txtPIC.Text = string.Empty;
            this.txtREMARK.Text = string.Empty;
            this.icbREC_STAT.EditValue = true;
        }

        public void GetPartyList(string findAll)
        {
            List<Party> lstParty = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (PartyBLL partyBll = new PartyBLL())
                {
                    lstParty = partyBll.GetPartyList(findAll);
                }

                this.grdParty.DataSource = lstParty;
                this.dntParty.DataSource = lstParty;

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

        public void GetBindingParty(string partyID)
        {
            Party party = null;
            try
            {
                using (PartyBLL partyBll = new PartyBLL())
                {
                    party = partyBll.GetParty(partyID);
                }

                if (party != null)
                {
                    this.txtPARTY_ID.Text = party.PARTY_ID;
                    this.txtPARTY_NAME.Text = party.PARTY_NAME;
                    this.icbPARTY_TYPE.EditValue = party.PARTY_TYPE;
                    this.txtADD1.Text = party.ADD1;
                    this.txtADD2.Text = party.ADD2;
                    this.txtADD3.Text = party.ADD3;
                    this.txtADD4.Text = party.ADD4;
                    this.txtTEL.Text = party.TEL;
                    this.txtFAX.Text = party.FAX;
                    this.txtEMAIL.Text = party.EMAIL;
                    this.txtPIC.Text = party.PIC;
                    this.txtREMARK.Text = party.REMARK;
                    this.icbREC_STAT.EditValue = party.REC_STAT;


                    this.GetPartyProductList(party.PARTY_ID);
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

        public void InsertParty()
        {
            string result = string.Empty;
            Party party = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                party = new Party();

                party.PARTY_ID = string.Empty;
                party.PARTY_NAME = this.txtPARTY_NAME.Text;
                party.PARTY_TYPE = (string)this.icbPARTY_TYPE.EditValue;
                party.ADD1 = this.txtADD1.Text;
                party.ADD2 = this.txtADD2.Text;
                party.ADD3 = this.txtADD3.Text;
                party.ADD4 = this.txtADD4.Text;
                party.TEL = this.txtTEL.Text;
                party.FAX = this.txtFAX.Text;
                party.EMAIL = this.txtEMAIL.Text;
                party.PIC = this.txtPIC.Text;
                party.REMARK = this.txtREMARK.Text;
                party.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (PartyBLL partyBll = new PartyBLL())
                {
                    result = partyBll.InsertParty(ref party, ((frmMainMenu)this.ParentForm).UserID);
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

                this.GetPartyList(string.Empty);

                if (result.Equals("OK"))
                {
                    this.txtPARTY_ID.Text = party.PARTY_ID;
                    GridView viewList = (GridView)this.grdParty.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PARTY_ID", party.PARTY_ID);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntParty.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }
        }

        public void UpdateParty()
        {
            string result = string.Empty;
            Party party = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                party = new Party();

                party.PARTY_ID = this.txtPARTY_ID.Text;
                party.PARTY_NAME = this.txtPARTY_NAME.Text;
                party.PARTY_TYPE = (string)this.icbPARTY_TYPE.EditValue;
                party.ADD1 = this.txtADD1.Text;
                party.ADD2 = this.txtADD2.Text;
                party.ADD3 = this.txtADD3.Text;
                party.ADD4 = this.txtADD4.Text;
                party.TEL = this.txtTEL.Text;
                party.FAX = this.txtFAX.Text;
                party.EMAIL = this.txtEMAIL.Text;
                party.PIC = this.txtPIC.Text;
                party.REMARK = this.txtREMARK.Text;
                party.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (PartyBLL partyBll = new PartyBLL())
                {
                    result = partyBll.UpdateParty(party, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    this.UpdatePartyProductList_Bulk();

                    GridView view = (GridView)this.grdParty.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("PARTY_ID", party.PARTY_ID);
                    view.SetFocusedRowCellValue("PARTY_NAME", party.PARTY_NAME);
                    view.SetFocusedRowCellValue("PARTY_TYPE", party.PARTY_TYPE);
                    view.SetFocusedRowCellValue("TEL", party.TEL);
                    view.SetFocusedRowCellValue("FAX", party.FAX);
                    view.SetFocusedRowCellValue("EMAIL", party.EMAIL);
                    view.SetFocusedRowCellValue("REC_STAT", party.REC_STAT);

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

        private bool IsDuplicated(GridView view, string prodSeq)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                if (view.GetRowCellDisplayText(i, "PROD_SEQ_NO").Equals(prodSeq))
                {
                    return true;
                }
            }

            return false;
        }


        private void GetPartyProductList(string partyid)
        {
            try
            {
                using (PartyBLL partyBll = new PartyBLL())
                {
                    this.dtbPartyProduct = partyBll.GetProductList(partyid);   
                }

                this.grdProductList.DataSource = this.dtbPartyProduct;

                GridView view = (GridView)this.grdProductList.Views[0];
                UiUtility.RemoveActiveRow(view);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void UpdatePartyProductList_Bulk()
        {
            string result = string.Empty;

            try
            {
                this.dtbPartyProduct.AcceptChanges();

                using (PartyBLL partyBll = new PartyBLL())
                {
                    result = partyBll.UpdatePartyProduct(this.dtbPartyProduct, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (!result.Equals("OK"))
                {
                    throw new Exception("Exception when update party product!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeletePartyProduct(GridView view, int rowSelect)
        {
            if (view == null || view.SelectedRowsCount == 0) return;

            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                rows[i] = view.GetDataRow(rowSelect); //view.GetSelectedRows()[i]
            }


            view.BeginSort();

            try
            {
                int flag = 0;

                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                       row["FLAG"] = 0;
                    }
                }
            }
            finally
            {
                view.EndSort();
                this.dtbPartyProduct.AcceptChanges();

                DataView dtv = new DataView(this.dtbPartyProduct);
                dtv.RowFilter = "[FLAG] <> 0";

                this.grdProductList.DataSource = dtv;
            }
        }

        #endregion

        private void frmParty_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.GetPartyList(string.Empty);
            //this.FormState = eFormState.ReadOnly;
        }

        private void frmParty_LoadCompleted()
        {
            this.KeyPreview = true;

            this.GetPartyList(string.Empty);
            this.FormState = eFormState.ReadOnly;
        }

        private void grvParty_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string partyID = view.GetRowCellValue(info.RowHandle, "PARTY_ID").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpPartyList.PageEnabled = false;

                    this.xtpPartyDetail.PageVisible = true;
                    this.xtcParty.SelectedTabPage = this.xtpPartyDetail;

                    //Call record detail.
                    this.GetBindingParty(partyID);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntParty_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdParty.Views[0]; //this.gridView2

                if (this.xtcParty.SelectedTabPage == this.xtpPartyDetail)
                {
                    string partyID = view.GetFocusedRowCellValue("PARTY_ID").ToString();

                    this.GetBindingParty(partyID);
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
            this.txtPARTY_NAME.Focus();
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
                        this.xtpPartyDetail.PageVisible = false;
                        this.xtpPartyList.PageEnabled = true;
                        this.xtcParty.SelectedTabPage = this.xtpPartyList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;
                    case eFormState.Edit:

                        this.GetBindingParty(this.txtPARTY_ID.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpPartyDetail.PageVisible = false;
                        this.xtpPartyList.PageEnabled = true;
                        this.xtcParty.SelectedTabPage = this.xtpPartyList;

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

            this.xtpPartyList.PageEnabled = false;
            this.xtpPartyDetail.PageVisible = true;
            this.xtcParty.SelectedTabPage = this.xtpPartyDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertParty();
                    break;
                case eFormState.Edit:
                    this.UpdateParty();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.GetPartyList(this.txtSearch.Text);
        }

        private void frmParty_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcParty.SelectedTabPage == this.xtpPartyList)
                        {
                            this.btnApply.PerformClick();
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetPartyList(editor.Text);
                    break;
                default:
                    break;
            }
        }

        private void frmParty_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdParty.Views[0]);
            this.Controls.Clear();
        }

        private void btnSelectProd_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdProductList.Views[0];
                //Open Popup For Select Program.
                DialogResult dialogResult;
                int resultCount = 0;

                List<Product> lstProduct = null;

                using (frmLOVPartyProduct fProdList = new frmLOVPartyProduct())
                {
                    dialogResult = UiUtility.ShowPopupForm(fProdList, this, true);
                    resultCount = fProdList.PRODUCT_LIST.Count;
                    lstProduct = fProdList.PRODUCT_LIST;
                }

                if (dialogResult == DialogResult.OK)
                {
                    if (resultCount > 0)
                    {
                        view.CancelUpdateCurrentRow();

                        foreach (Product prodSelect in lstProduct)
                        {
                            if (!this.IsDuplicated(view, prodSelect.PROD_SEQ_NO))
                            {
                                view.AddNewRow();

                                view.SetFocusedRowCellValue("PARTY_ID", this.txtPARTY_ID.Text);
                                view.SetFocusedRowCellValue("PROD_SEQ_NO", prodSelect.PROD_SEQ_NO);
                                view.SetFocusedRowCellValue("PRODUCT_NO", prodSelect.PRODUCT_NO);
                                view.SetFocusedRowCellValue("PRODUCT_NAME", prodSelect.PRODUCT_NAME); //default
                                //for hidden field 2=NEW ROW
                                view.SetFocusedRowCellValue("FLAG", 2);

                                view.UpdateCurrentRow();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvProductList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                e.Info.ImageIndex = -1;
                ColumnView view = sender as ColumnView;

                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = string.Format("{0}", (e.RowHandle + 1).ToString().PadLeft(2, ' '));
                    e.Info.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Info.Appearance.ForeColor = Color.Gray;
                    e.Info.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error brvPickupSheetDetail_CustomDrawRowIndicator", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvProductList_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            try
            {
                GridView view = sender as GridView;

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                if (e.KeyCode == Keys.Delete)
                {
                    int rowHandle = view.FocusedRowHandle;

                    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this product?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (isDelete == DialogResult.Yes)
                    {
                        this.DeletePartyProduct(view, rowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


    }
}