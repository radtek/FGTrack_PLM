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
using System.Globalization;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using HTN.BITS.UIL.PLASESS.LOVForms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.PopupForms;
using DevExpress.XtraEditors.DXErrorProvider;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.UIL.PLASESS.Query_Popup;
using DevExpress.XtraEditors.Controls;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmShippingOrder : BaseChildForm
    {
        public frmShippingOrder()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdShippingOrder);
            base.LoadGridLayout(this.grdSODetail);

            this.CustomInitializeComponent();

            //base.LoadGridLayout(this.Name, this.grdShippingOrder.Views[0]);
            //base.LoadGridLayout(this.Name, this.grdSODetail.Views[0]);
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbShippingOrderDtl;
        private List<ShippingOrderDtl> delSODtl;

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
                return string.Format("ShippingOrder_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("ShippingOrder_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcShippingOrder.SelectedTabPage == this.xtpShippingOrderList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                //GridExportController gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToXLS(this.FileName + ".xls", "Microsoft Excel Document", "Microsoft Excel|*.xls");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.XLS, this.FileName + ".xls", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController;
            if (this.IsTabListSelected)
            {
                //gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToXLSX(this.FileName + ".xlsx", "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController;
            if (this.IsTabListSelected)
            {
                //gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToPDF(this.FileName + ".pdf", "PDF Document", "PDF Files|*.pdf");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController;
            if (this.IsTabListSelected)
            {
                //gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToRTF(this.FileName + ".rtf", "RTF Document", "RTF Files|*.rtf");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController;
            if (this.IsTabListSelected)
            {
                //gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToTEXT(this.FileName + ".txt", "Text Document", "Text Files|*.txt");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController;
            if (this.IsTabListSelected)
            {
                //gridController = new GridExportController(this.grdShippingOrder.Views[0]);
                //gridController.ExportToHTML(this.FileName + ".html", "HTML Document", "HTML Files|*.html");
                base.ViewExportToExcel(this.grdShippingOrder.Views[0], GridExportType.HTML, this.FileName + ".html", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
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
                GridView view = (GridView)this.grdSODetail.Views[0];
                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntShippingOrder.Enabled = true;

                        this.dntShippingOrder.TextStringFormat = "      Add Mode      ";
                        this.dntShippingOrder.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnEdit.Enabled = false;
                        this.btnDelete.Enabled = true;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnPickingList.Enabled = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;

                        //qty
                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = true;

                        //REMARK
                        view.Columns["REMARK"].OptionsColumn.ReadOnly = false;
                        view.Columns["REMARK"].OptionsColumn.AllowFocus = true;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = true;



                        view.FocusedColumn = view.Columns["PRODUCT_NO"];

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntShippingOrder.Enabled = true;

                        this.dntShippingOrder.TextStringFormat = "      Edit Mode      ";
                        this.dntShippingOrder.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnEdit.Enabled = false;
                        this.btnDelete.Enabled = true;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnPickingList.Enabled = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        view.FocusedColumn = view.Columns["PRODUCT_NO"];

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;
                        //QTY
                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;
                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = true;
                        //REMARK
                        view.Columns["REMARK"].OptionsColumn.ReadOnly = false;
                        view.Columns["REMARK"].OptionsColumn.AllowFocus = true;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = true;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntShippingOrder.Enabled = false;

                        this.dntShippingOrder.TextStringFormat = " Record {0} of {1} ";
                        this.dntShippingOrder.Enabled = true;

                        this.btnPostData.Enabled = true;
                        this.btnEdit.Enabled = true; ;
                        this.btnDelete.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";
                        this.btnPickingList.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, false);
                        UiUtility.SetGridReadOnly(view, true);

                        view.OptionsBehavior.Editable = true;

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = false;
                        //QTY
                        view.Columns["QTY"].OptionsColumn.ReadOnly = true;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = false;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = false;
                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = false;
                        //REMARK
                        view.Columns["REMARK"].OptionsColumn.ReadOnly = true;
                        view.Columns["REMARK"].OptionsColumn.AllowFocus = false;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = false;

                        this.GridDetail_OptionsCustomization(view, true);

                        break;
                    default:
                        break;
                }

                //view.Columns.ColumnByFieldName("PICKED_QTY").OptionsColumn.ReadOnly = false;
                //view.Columns.ColumnByFieldName("PICKED_QTY").OptionsColumn.AllowEdit = true;

                //view.Columns.ColumnByFieldName("LOADED_QTY").OptionsColumn.ReadOnly = false;
                //view.Columns.ColumnByFieldName("LOADED_QTY").OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeControlState(bool state)
        {

            this.lueWarehouse.Properties.ReadOnly = state;
            this.btePARTY_ID.Properties.ReadOnly = state;
            this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;
            //this.txtPARTY_NAME.Properties.ReadOnly = state;
            this.txtREF_NO.Properties.ReadOnly = state;
            this.dtpREF_DATE.Properties.ReadOnly = state;
            this.dtpREF_DATE.Properties.Buttons[0].Enabled = !state;
            this.dtpETA.Properties.ReadOnly = state;
            this.dtpETA.Properties.Buttons[0].Enabled = !state;
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
            this.txtSO_NO.Text = string.Empty;
            this.txtSO_DATE.EditValue = DateTime.Now;
            this.txtPOST_REF.EditValue = string.Empty;
            this.lueWarehouse.EditValue = null;
            this.btePARTY_ID.EditValue = null;
            this.txtPARTY_NAME.Text = string.Empty;
            this.txtREF_NO.Text = string.Empty;
            this.dtpREF_DATE.EditValue = DateTime.Now;
            this.dtpETA.EditValue = null;
            this.dtpETA.Properties.NullDate = string.Empty;
            this.txtREMARK.Text = string.Empty;

            this.icbREC_STAT.EditValue = true;

            this.GetShippingOrderDetail(string.Empty, string.Empty);
        }

        private void InitializaLOVData()
        {

            try
            {
                using (ProductBLL pdBll = new ProductBLL())
                {
                    List<Unit> lstUnit = pdBll.GetUnitList();
                    if (lstUnit != null)
                    {
                        this.grvSODetail_rps_lueUNIT_ID.DataSource = lstUnit;
                    }
                }

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    List<Warehouse> lstWH = shipOrdBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstWH;
                        this.lueSearchWH.Properties.DataSource = lstWH;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetShippingOrderList(string whid, string findValue, DateTime? fromDate, DateTime? toDate)
        {
            List<ShippingOrder> lstShipOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data", this);

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstShipOrd = shipOrdBll.GetShippingOrderList(findValue, whid, fromDate, toDate);
                }

                DataTable dtshipOrd = UiUtility.BuildDataTable<ShippingOrder>(lstShipOrd);
                dtshipOrd.DefaultView.Sort = "SO_DATE DESC";

                this.grdShippingOrder.DataSource = dtshipOrd; //lstShipOrd
                this.dntShippingOrder.DataSource = dtshipOrd;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
                base.FinishedProcessing();
            }
        }

        public void AdvanceSearchShippingOrder(string soNo, string refNo, string whid, DateTime? fromDate, DateTime? toDate)
        {
            List<ShippingOrder> lstShipOrd = null;
            try
            {
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstShipOrd = shipOrdBll.AdvShippingOrder(soNo, refNo, whid, fromDate, toDate);
                }

                this.grdShippingOrder.DataSource = lstShipOrd;
                this.dntShippingOrder.DataSource = lstShipOrd;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void GetBindingShippingOrder(string soNo)
        {
            ShippingOrder shipOrd = null;
            try
            {
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    shipOrd = shipOrdBll.GetShippingOrder(soNo);
                }

                if (shipOrd != null)
                {
                    base.ClearValidControls(this, ref this.dxErrorProvider1);

                    this.txtSO_NO.Text = shipOrd.SO_NO;
                    this.txtSO_DATE.EditValue = shipOrd.SO_DATE;
                    this.txtPOST_REF.EditValue = shipOrd.POST_REF;
                    this.lueWarehouse.EditValue = shipOrd.WH_ID;
                    this.btePARTY_ID.EditValue = shipOrd.PARTY_ID;
                    this.txtPARTY_NAME.Text = shipOrd.PARTY_NAME;
                    this.txtREF_NO.Text = shipOrd.REF_NO;
                    this.dtpREF_DATE.EditValue = shipOrd.REF_DATE;
                    this.dtpETA.EditValue = shipOrd.ETA;
                    this.txtREMARK.Text = shipOrd.REMARK;

                    this.icbREC_STAT.EditValue = shipOrd.REC_STAT;

                    this.GetShippingOrderDetail(shipOrd.SO_NO, shipOrd.WH_ID);
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

        public void GetShippingOrderDetail(string soNo, string whID)
        {
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data", this);

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    this.dtbShippingOrderDtl = shipOrdBll.GetShippingOrderDetail(soNo, whID);
                }

                if (this.dtbShippingOrderDtl != null)
                {
                    this.grdSODetail.DataSource = this.dtbShippingOrderDtl;
                    this.ConditionsColumnView(this.grdSODetail);
                }

                this.CheckEnablePostData("[QTY] = [LOADED_QTY]");
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

        private void CheckEnablePostData(string expression)
        {
            if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
            {
                if (this.dtbShippingOrderDtl != null)
                {
                    DataRow[] rows = this.dtbShippingOrderDtl.Select(expression);
                    if (rows.Length > 0 && this.dtbShippingOrderDtl.Rows.Count == rows.Length)
                    {
                        this.btnPostData.Enabled = true;
                    }
                    else
                    {
                        this.btnPostData.Enabled = false;
                    }
                }
                else
                {
                    this.btnPostData.Enabled = false;
                }
            }
            else
            {
                this.btnPostData.Enabled = false;
            }
        }

        private void ConditionsColumnView(GridControl grd)
        {
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[3];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["ASSIGN_QTY"];
                cnArr[0].Expression = @"[QTY] <> [ASSIGN_QTY]";
                cnArr[0].Appearance.ForeColor = Color.Red;
                cnArr[0].Appearance.Options.UseBackColor = true;
                cnArr[0].Appearance.Options.UseForeColor = true;
                cnArr[0].ApplyToRow = false;

                cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[1].Column = ((ColumnView)grd.MainView).Columns["PICKED_QTY"];
                cnArr[1].Expression = @"[QTY] > [PICKED_QTY]";
                cnArr[1].Appearance.ForeColor = Color.Red;
                cnArr[1].ApplyToRow = false;

                cnArr[2] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[2].Column = ((ColumnView)grd.MainView).Columns["LOADED_QTY"];
                cnArr[2].Expression = @"[PICKED_QTY] > 0 AND [PICKED_QTY] <> [LOADED_QTY]";
                cnArr[2].Appearance.ForeColor = Color.Red;
                cnArr[2].ApplyToRow = false;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
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
                this.dxErrorProvider1.SetError(this.lueWarehouse, "Warehouse can't be Empty");
                this.lueWarehouse.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.btePARTY_ID.Text))
            {
                this.dxErrorProvider1.SetError(this.btePARTY_ID, "Customer can't be Empty");
                this.btePARTY_ID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtREF_NO.Text))
            {
                this.dxErrorProvider1.SetError(this.txtREF_NO, "PO Ref. can't be Empty");
                this.txtREF_NO.Focus();
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

        public void InsertShippingOrder()
        {
            string result = string.Empty;
            ShippingOrder shippingOrd = new ShippingOrder();

            try
            {
                #region "Shipping Order Header"

                shippingOrd.SO_NO = string.Empty;
                shippingOrd.SO_DATE = DateTime.Now;
                shippingOrd.PARTY_ID = this.btePARTY_ID.Text;
                shippingOrd.REF_NO = this.txtREF_NO.Text;
                shippingOrd.REF_DATE = this.dtpREF_DATE.DateTime;
                if (this.dtpETA.DateTime != DateTime.MinValue)
                {
                    shippingOrd.ETA = this.dtpETA.DateTime;
                }
                shippingOrd.WH_ID = this.lueWarehouse.EditValue.ToString();
                shippingOrd.REMARK = this.txtREMARK.Text;
                shippingOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                #region "Shipping Order Detail"
                ShippingOrderDtl shpOrdDtl;
                foreach (DataRow dr in this.dtbShippingOrderDtl.Rows)
                {
                    shpOrdDtl = new ShippingOrderDtl();

                    shpOrdDtl.SO_NO = dr["SO_NO"].ToString();
                    shpOrdDtl.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                    shpOrdDtl.UNIT_ID = dr["UNIT_ID"].ToString();
                    shpOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.UNIT_PRICE = (dr["UNIT_PRICE"] as decimal?) ?? 0.0M;
                    //shpOrdDtl.UNIT_PRICE = Convert.ToDecimal(dr["UNIT_PRICE"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.REMARK = dr["REMARK"].ToString();
                    shpOrdDtl.REC_STAT = true;

                    shippingOrd.AddItem(shpOrdDtl);
                }
                #endregion

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    result = shipOrdBll.InsertShippingOrder(ref shippingOrd, ((frmMainMenu)this.ParentForm).UserID);
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
                this.txtSO_NO.Text = shippingOrd.SO_NO;
                this.txtSO_DATE.EditValue = shippingOrd.SO_DATE.Value;

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.txtSearch.Text = shippingOrd.SO_NO;
                this.lueSearchWH.EditValue = null;

                this.GetShippingOrderList(string.Empty, shippingOrd.SO_NO, null, null);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdShippingOrder.Views[0];


                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "SO_NO", shippingOrd.SO_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntShippingOrder.Position = position;
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

        public void UpdateShippingOrder()
        {
            string result = string.Empty;
            ShippingOrder shippingOrd = new ShippingOrder();

            try
            {
                #region "Shipping Order Header"

                shippingOrd.SO_NO = this.txtSO_NO.Text;
                shippingOrd.SO_DATE = Convert.ToDateTime(this.txtSO_DATE.EditValue, DateTimeFormatInfo.CurrentInfo);
                shippingOrd.PARTY_ID = this.btePARTY_ID.Text;
                shippingOrd.PARTY_NAME = this.txtPARTY_NAME.Text;
                shippingOrd.REF_NO = this.txtREF_NO.Text;
                shippingOrd.REF_DATE = this.dtpREF_DATE.DateTime;
                if (this.dtpETA.DateTime != DateTime.MinValue)
                {
                    shippingOrd.ETA = this.dtpETA.DateTime;
                }
                shippingOrd.WH_ID = this.lueWarehouse.EditValue.ToString();
                shippingOrd.REMARK = this.txtREMARK.Text;
                shippingOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                #region "Shipping Order Detail"
                //Check Delete Recore
                if (this.delSODtl != null)
                {
                    foreach (ShippingOrderDtl delSo in this.delSODtl)
                    {
                        shippingOrd.AddItem(delSo);
                    }
                }

                ShippingOrderDtl shpOrdDtl;
                foreach (DataRow dr in this.dtbShippingOrderDtl.Rows)
                {
                    shpOrdDtl = new ShippingOrderDtl();

                    shpOrdDtl.SO_NO = dr["SO_NO"].ToString();
                    shpOrdDtl.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                    shpOrdDtl.UNIT_ID = dr["UNIT_ID"].ToString();
                    shpOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.UNIT_PRICE = (dr["UNIT_PRICE"] as decimal?) ?? 0.0M;
                    //shpOrdDtl.UNIT_PRICE = Convert.ToDecimal(dr["UNIT_PRICE"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.REMARK = dr["REMARK"].ToString();
                    shpOrdDtl.REC_STAT = true;
                    shpOrdDtl.FLAG = Convert.ToInt32(dr["FLAG"], NumberFormatInfo.CurrentInfo);

                    shippingOrd.AddItem(shpOrdDtl);
                }
                #endregion

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    result = shipOrdBll.UpdateShippingOrder(shippingOrd, ((frmMainMenu)this.ParentForm).UserID);
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
                if (this.delSODtl != null)
                {
                    this.delSODtl.Clear();
                    this.delSODtl = null;
                }

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                //this.GetShippingOrderList(string.Empty, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdShippingOrder.Views[0];


                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "SO_NO", shippingOrd.SO_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntShippingOrder.Position = position;
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

        private bool GetCustomerByCode(string custCode)
        {
            bool result = false;
            try
            {
                using (PartyBLL partyBll = new PartyBLL())
                {
                    Party party = partyBll.GetParty(custCode);
                    if (party != null)
                    {
                        this.txtPARTY_NAME.Text = party.PARTY_NAME;
                        this.txtREF_NO.Focus();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                result = false;
            }
            finally
            {
            }

            return result;
        }

        private bool GetProductByNo(GridView view, string partyid, string poNo, string prodNo, bool isCheck)
        {
            bool result = false;
            try
            {
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    Product prod = shipOrdBll.LovProductByNo(this.lueWarehouse.EditValue.ToString(), partyid, poNo, prodNo);
                    if (prod != null)
                    {
                        if (!isCheck)
                        {
                            view.SetFocusedRowCellValue("SO_NO", this.txtSO_NO.Text);
                            view.SetFocusedRowCellValue("LINE_NO", view.RowCount);
                            view.SetFocusedRowCellValue("PROD_SEQ_NO", prod.PROD_SEQ_NO);
                            view.SetFocusedRowCellValue("PRODUCT_NO", prod.PRODUCT_NO);
                            view.SetFocusedRowCellValue("PRODUCT_NAME", prod.PRODUCT_NAME);
                            view.SetFocusedRowCellValue("UNIT_ID", prod.UNIT);
                            view.SetFocusedRowCellValue("UNIT_PRICE", prod.COST_PRICE);
                            view.SetFocusedRowCellValue("FREE_STOCK", prod.FREE_STOCK);
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private void DeleteSODetail(GridView view, int rowSelect)
        {
            if (view == null || view.SelectedRowsCount == 0) return;

            int iTest = view.RowCount;

            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                rows[i] = view.GetDataRow(rowSelect); //view.GetSelectedRows()[i]
            }


            view.BeginSort();
            try
            {
                ShippingOrderDtl SODtl;
                int flag = 0;
                if (this.delSODtl == null)
                {
                    this.delSODtl = new List<ShippingOrderDtl>();
                }

                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        flag = Convert.ToInt32(row["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 1 || flag == 3)
                        {
                            SODtl = new ShippingOrderDtl();
                            SODtl.SO_NO = row["SO_NO"].ToString();
                            SODtl.PROD_SEQ_NO = row["PROD_SEQ_NO"].ToString();
                            SODtl.FLAG = 0;

                            this.delSODtl.Add(SODtl);
                        }

                        row.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                view.EndSort();
                this.dtbShippingOrderDtl.AcceptChanges();
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ShowEditor();
            }
        }

        private void ShowPopupDetailDetail(string sono, string prodseq, string type)
        {
            try
            {
                DialogResult result;
                using (frmQupPickingInfo fDetail = new frmQupPickingInfo { SO_NO = sono, PROD_SEQ_NO = prodseq, TYPE = type, USER_ID = ((frmMainMenu)this.ParentForm).UserID })
                {
                    result = fDetail.ShowDialog(this);
                }

                if (result == DialogResult.OK)
                {
                    this.GetShippingOrderDetail(this.txtSO_NO.Text, this.lueWarehouse.EditValue.ToString());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private int GetTotalBoxQty(string productSeq, int qty)
        {
            int boxQty = 0;
            try
            {
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    boxQty = shipOrdBll.GetBoxQty(productSeq, qty);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return boxQty;
        }

        private void ShippingOrderPostData(List<ShippingOrder> lstShipOrd)
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
                        using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                        {
                            resultMsg = shipOrdBll.GetPostData(lstShipOrd, selectPath, ((frmMainMenu)this.ParentForm).UserID, out files);
                        }

                        if (files.Count > 0)
                        {
                            //copy to server
                            UiUtility.CopyFilesToServer(files);

                            this.OpenPath(files);

                            //Refresh screen
                            if (this.xtcShippingOrder.SelectedTabPage == this.xtpShippingOrderList)
                            {
                                this.btnApply.PerformClick();
                            }
                            else
                            {
                                this.GetBindingShippingOrder(this.txtSO_NO.Text);
                            }
                        }
                        else
                        {

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


        #region "Custom Event Handle"

        private void GridControl_DoubleClick(object sender, EventArgs e)
        {
            //BandedGridView bView = sender as BandedGridView;
            if (this.FormState != eFormState.ReadOnly) return;

            ColumnView columnView = sender as ColumnView;
            Point pt = columnView.GridControl.PointToClient(Control.MousePosition);
           
            GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
            if (info.InRow || info.InRowCell)
            {
                if (info.RowHandle == GridControl.NewItemRowHandle) return;

                if (info.Column.FieldName.Equals("PICKED_QTY") || info.Column.FieldName.Equals("LOADED_QTY"))
                {
                    string sono = columnView.GetRowCellValue(info.RowHandle, "SO_NO").ToString();
                    string prodseq = columnView.GetRowCellValue(info.RowHandle, "PROD_SEQ_NO").ToString();
                    string type;
                    if (info.Column.FieldName.Equals("LOADED_QTY"))
                        type = "L";
                    else
                        type = string.Empty;

                   this.ShowPopupDetailDetail(sono, prodseq, type);

                }
            }
        }
        
        private void GridControl_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //ColumnView view = sender as ColumnView;
            //try
            //{
            //    if (e.RowHandle >= 0)
            //    {
            //        if (e.RowHandle == view.FocusedRowHandle)
                        
            //        {

            //            e.Info.ImageIndex = 0;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void Respository_DoubleClick(object sender, EventArgs e)
        {
            //this.GridControl_DoubleClick(this.grdQrySummary.MainView, e);
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShippingOrder_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.xtpShippingOrderDetail.PageVisible = false;
            //this.FormState = eFormState.ReadOnly;
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();

            //this.GetShippingOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            
        }

        private void frmShippingOrder_LoadCompleted()
        {
            this.KeyPreview = true;
            this.xtpShippingOrderDetail.PageVisible = false;
            this.FormState = eFormState.ReadOnly;
            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;
            this.InitializaLOVData();

            this.GetShippingOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void btePARTY_ID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            frmLOVParty fCustList = new frmLOVParty();
            fCustList.PARTY_TYPE = "C"; //find only Customer
            DialogResult result = UiUtility.ShowPopupForm(fCustList, this, true);

            if (result == DialogResult.OK)
            {
                btnEdit.Text = fCustList.PARTY_ID;
                this.txtPARTY_NAME.Text = fCustList.PARTY_NAME;

                this.txtREF_NO.Focus();
            }
        }

        private void btePARTY_ID_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            ButtonEdit editor = (ButtonEdit)sender;
            if (editor.Text == string.Empty)
            {
                //e.Cancel = true;
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Customer can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                bool isValid = this.GetCustomerByCode(editor.Text);
                if (!isValid)
                {
                    //e.Cancel = true;
                    UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Invalid Customer", ErrorType.Critical);
                    editor.Focus();
                }
                else
                {
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
                }
            }
        }

        private void grvShippingOrder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string soNo = view.GetRowCellValue(info.RowHandle, "SO_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpShippingOrderList.PageEnabled = false;

                    this.xtpShippingOrderDetail.PageVisible = true;
                    this.xtcShippingOrder.SelectedTabPage = this.xtpShippingOrderDetail;

                   
                    //Call record detail.
                    this.GetBindingShippingOrder(soNo);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntShippingOrder_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdShippingOrder.Views[0]; //this.gridView2

                if (this.xtcShippingOrder.SelectedTabPage == this.xtpShippingOrderDetail)
                {
                    string soNo = view.GetFocusedRowCellValue("SO_NO").ToString();

                    this.GetBindingShippingOrder(soNo);
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
            //Check Posted Data
            if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
            {
                this.FormState = eFormState.Edit;
                this.lueWarehouse.Focus();
            }
            else
            {
                XtraMessageBox.Show(this, "This SO. already post data!!\nCan't Edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtpShippingOrderDetail.PageVisible = false;
                        this.xtpShippingOrderList.PageEnabled = true;
                        this.xtcShippingOrder.SelectedTabPage = this.xtpShippingOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                        this.GetBindingShippingOrder(this.txtSO_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpShippingOrderDetail.PageVisible = false;
                        this.xtpShippingOrderList.PageEnabled = true;
                        this.xtcShippingOrder.SelectedTabPage = this.xtpShippingOrderList;

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
                if (this.delSODtl != null)
                {
                    this.delSODtl.Clear();
                    this.delSODtl = null;
                }

                this.FormState = eFormState.ReadOnly;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.xtpShippingOrderList.PageEnabled = false;
            this.xtpShippingOrderDetail.PageVisible = true;
            this.xtcShippingOrder.SelectedTabPage = this.xtpShippingOrderDetail;

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
                    this.InsertShippingOrder();
                    break;
                case eFormState.Edit:
                    this.UpdateShippingOrder();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnPickingList_Click(object sender, EventArgs e)
        {
            try
            {
                frmPickingList fPrintPickingList = new frmPickingList { SO_NO = this.txtSO_NO.Text, WH_ID = (string)this.lueWarehouse.EditValue, USER_ID = ((frmMainMenu)this.ParentForm).UserID };

                UiUtility.ShowPopupForm(fPrintPickingList, this, true);
                //refresh
                this.GetShippingOrderDetail(this.txtSO_NO.Text, (string)this.lueWarehouse.EditValue);
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;

                if (e.PrevFocusedColumn != null)
                {
                    switch (e.PrevFocusedColumn.FieldName)
                    {
                        case "PRODUCT_NO":
                            string result = view.GetFocusedRowCellDisplayText(e.PrevFocusedColumn);
                            if (string.IsNullOrEmpty(result))
                            {
                                view.FocusedColumn = e.PrevFocusedColumn;
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "FocusedColumnChanged : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                view.SetFocusedRowCellValue("FLAG", 2);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InitNewRo : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            try
            {
                GridView view = sender as GridView;

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                if (e.KeyCode == Keys.Delete)
                {
                    int rowHandle = view.FocusedRowHandle;

                    int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                    DialogResult isDelete;
                    int assignQty = 0;
                    switch (flag)
                    {
                        case 1:
                            assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                            if (assignQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteSODetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                            break;
                        case 2:
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteSODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                            break;
                        case 3:
                            assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                            if (assignQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteSODetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;

            try
            {
                if ((view.FocusedRowHandle != GridControl.InvalidRowHandle) && (view.FocusedRowHandle != GridControl.NewItemRowHandle))
                {
                    int flag = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "FLAG").ToString());
                    if (flag != 2)
                    {
                        view.SetFocusedRowCellValue("FLAG", 3);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "RowUpdated" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = (GridView)sender;

            try
            {
                DataRowView rowView = (DataRowView)e.Row;
                if (rowView["QTY"].ToString() == "")
                {
                    e.Valid = false;
                    //view.SetColumnError(view.Columns["QTY"],"Qty can't be null",
                    //    DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                    view.FocusedColumn = view.Columns["QTY"];
                }
                //else
                //{
                //    int qty = Convert.ToInt32(rowView["QTY"], NumberFormatInfo.CurrentInfo);
                //    //int freeQty = Convert.ToInt32(rowView["FREE_STOCK"], NumberFormatInfo.CurrentInfo);
                //    int flag = Convert.ToInt32(rowView["FLAG"], NumberFormatInfo.CurrentInfo);
                //    switch (flag)
                //    {
                //        case 1: //edit
                //            int assignQty = Convert.ToInt32(rowView["ASSIGN_QTY"], NumberFormatInfo.CurrentInfo);
                //            if (qty < assignQty) //qty > freeQty || 
                //            {
                //                e.Valid = false;
                //                view.SetColumnError(view.Columns["QTY"], "Qty can't less than Assign Qty",
                //                    DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                //                view.FocusedColumn = view.Columns["QTY"];
                //            }
                //            else
                //            {
                //                e.Valid = true;
                //            }
                //            break;
                //        case 2: //new line
                //            e.Valid = true;
                //            //if (qty > freeQty)
                //            //{
                //            //    e.Valid = false;
                //            //    view.SetColumnError(view.Columns["QTY"], "Qty can't more than Free Stock",
                //            //        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                //            //    view.FocusedColumn = view.Columns["QTY"];
                //            //}
                //            //else
                //            //{
                //            //    e.Valid = true;
                //            //}
                //            break;
                //        case 3: //edit after
                //            e.Valid = true;
                //            //if (qty > freeQty)
                //            //{
                //            //    e.Valid = false;
                //            //    view.SetColumnError(view.Columns["QTY"], "Qty can't more than Free Stock",
                //            //        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                //            //    view.FocusedColumn = view.Columns["QTY"];
                //            //}
                //            //else
                //            //{
                //            //    e.Valid = true;
                //            //}
                //            break;
                //    }
                //}

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "ValidateRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_rps_btePRODUCT_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdSODetail.Views[0];

                int assignQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "ASSIGN_QTY"), "0"), NumberFormatInfo.CurrentInfo);
                if (assignQty > 0)
                {
                    XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {

                    ButtonEdit btnEdit = (ButtonEdit)sender;
                    btnEdit.EditValue = string.Empty;
                    //Open Popup For Select Product.
                    using (frmLOVProduct fPrdList = new frmLOVProduct { FormCalling = eFormCalling.fShippingOrder, WH_ID = this.lueWarehouse.EditValue.ToString(),
                                                                        PARTY_ID = this.btePARTY_ID.EditValue.ToString(), PO_NO = this.txtREF_NO.Text})
                    {
                        fPrdList.ShippingOrder_GetProductList();
                        DialogResult result = UiUtility.ShowPopupForm(fPrdList, this, true);
                        if (result == DialogResult.OK)
                        {
                            bool isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                            if (!isDup)
                            {
                                btnEdit.EditValue = fPrdList.PRODUCT_NO;
                                btnEdit.Update();
                                //this.Get .GetPoDetailRecord(view, fMtl.PO_NO, fMtl.PO_LINE);
                                this.GetProductByNo(view, this.btePARTY_ID.EditValue.ToString(), this.txtREF_NO.Text, fPrdList.PRODUCT_NO, false);
                                //editor.Update();
                                btnEdit.SendKey(new KeyEventArgs(Keys.Tab));
                            }
                            else
                            {
                                btnEdit.Undo();
                                //MessageBox.Show("Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_rps_btePRODUCT_NO_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdSODetail.Views[0];
            ButtonEdit editor = (ButtonEdit)sender;


            if (editor.EditValue != null)
            {
                if (this.FormState == eFormState.Edit)
                {
                    int assignQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "ASSIGN_QTY"), "0"), NumberFormatInfo.CurrentInfo);
                    if (assignQty > 0)
                    {
                        e.Cancel = true;

                        XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        view.CancelUpdateCurrentRow();
                        editor.SendKey(new KeyEventArgs(Keys.Escape));
                        return;
                    }
                }

                bool isValid = this.GetProductByNo(view, this.btePARTY_ID.EditValue.ToString(), this.txtREF_NO.Text, editor.EditValue.ToString(), true);//true
                if (!isValid)
                {
                    using (frmLOVProduct fPrdList = new frmLOVProduct { FormCalling = eFormCalling.fShippingOrder, WH_ID = this.lueWarehouse.EditValue.ToString(),
                                                                        PARTY_ID = this.btePARTY_ID.EditValue.ToString(), PO_NO = this.txtREF_NO.Text})
                    {
                        fPrdList.ShippingOrder_GetProductList();
                        DialogResult dialogResult = UiUtility.ShowPopupForm(fPrdList, this, true);
                        if (dialogResult == DialogResult.OK)
                        {
                            bool isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                            if (!isDup)
                            {
                                editor.EditValue = fPrdList.PRODUCT_NO;
                                this.GetProductByNo(view, this.btePARTY_ID.EditValue.ToString(), this.txtREF_NO.Text, fPrdList.PRODUCT_NO, false);
                            }
                            else
                            {
                                e.Cancel = true;
                                editor.Undo();
                                XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                            e.Cancel = true;
                    }
                }
                else
                {
                    bool isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", editor.Text);
                    if (isDup)
                    {
                        e.Cancel = true;
                        editor.Undo();
                        XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        //view.CancelUpdateCurrentRow();

                        //view.FocusedRowHandle = GridControl.NewItemRowHandle;
                        //view.ShowEditor();

                        //e.Cancel = true;
                    }
                    else
                    {
                        this.GetProductByNo(view, this.btePARTY_ID.EditValue.ToString(), this.txtREF_NO.Text, editor.Text, false);
                    }
                }
            }

            //bool isDup = false;
            //try
            //{
                

            //    int assignQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "ASSIGN_QTY"), "0"), NumberFormatInfo.CurrentInfo);
            //    if (assignQty > 0)
            //    {
            //        e.Cancel = true;

            //        XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    
            //        view.CancelUpdateCurrentRow();
            //        editor.SendKey(new KeyEventArgs(Keys.Escape));
            //    }
            //    else
            //    {

            //        if (editor.EditValue != null)
            //        {
            //            bool isValid = this.GetProductByNo(view, editor.Text);
            //            if (!isValid)
            //            {
            //                e.Cancel = true;
                            
            //                //Open Popup For Select Product.
            //                frmLOVProduct fPrdList = new frmLOVProduct();
            //                fPrdList.FormCalling = eFormCalling.fShippingOrder;
            //                fPrdList.WH_ID = this.lueWarehouse.EditValue.ToString();
            //                fPrdList.ShippingOrder_GetProductList();
            //                DialogResult dialogResult = UiUtility.ShowPopupForm(fPrdList, this, true);

            //                if (dialogResult == DialogResult.OK)
            //                {
            //                    //this.GetProductByNo(view, fPrdList.PRODUCT_NO);
            //                    editor.EditValue = fPrdList.PRODUCT_NO;
            //                    editor.Update();

            //                    view.SetFocusedRowCellValue("QTY", 1);
            //                    view.SetFocusedRowCellValue("SO_NO", this.txtSO_NO.Text);
            //                    view.SetFocusedRowCellValue("LINE_NO", view.RowCount);
            //                    view.SetFocusedRowCellValue("PROD_SEQ_NO", fPrdList.PROD_SEQ_NO);
            //                    view.SetFocusedRowCellValue("PRODUCT_NO", fPrdList.PRODUCT_NO);
            //                    view.SetFocusedRowCellValue("PRODUCT_NAME", fPrdList.PRODUCT_NAME);
            //                    view.SetFocusedRowCellValue("UNIT_ID", string.Empty);
            //                    view.SetFocusedRowCellValue("FREE_STOCK", 0);
            //                    //editor.Update();

            //                    view.FocusedColumn = view.Columns["QTY"];
            //                    view.ShowEditor();
            //                }
            //                //{
            //                //    isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
            //                //    if (!isDup)
            //                //    {

            //                //        editor.EditValue = fPrdList.PRODUCT_NO;
            //                //        editor.Update();

            //                //        //this.Get .GetPoDetailRecord(view, fMtl.PO_NO, fMtl.PO_LINE);
            //                //        this.GetProductByNo(view, fPrdList.PRODUCT_NO);

            //                //        //editor.Update();
            //                //        editor.SendKey(new KeyEventArgs(Keys.Tab));
            //                //    }
            //                //    else
            //                //    {
            //                //        editor.Undo();
            //                //        //MessageBox.Show("Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                //        XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //                //    }
            //                //}
            //                //else
            //                //{
            //                //    e.Cancel = true;
            //                //    //editor.EditValue = null;
            //                //}
            //            }
            //            else
            //            {
            //                isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", editor.Text);
            //                if (isDup)
            //                {
            //                    editor.Undo();
            //                    //MessageBox.Show("Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //                    view.CancelUpdateCurrentRow();

            //                    view.FocusedRowHandle = GridControl.NewItemRowHandle;
            //                    view.ShowEditor();

            //                    e.Cancel = true;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //}
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

        private void txtREF_NO_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            TextEdit editor = (TextEdit)sender;
            bool isValid = UiValidations.Validate_Empty(editor, ref this.dxErrorProvider1, "Value can't null", ErrorType.Warning);
            if (!isValid)
            {
                editor.Focus();
            }
        }

        private void dtpREF_DATE_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            DateEdit editor = (DateEdit)sender;
            if (string.IsNullOrEmpty(editor.Text))
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Plan Date can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void grvSODetail_rps_QTY_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdSODetail.Views[0];
            TextEdit editor = (TextEdit)sender;

            try
            {
                int rowHandle = view.FocusedRowHandle;
                if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    string productSeq = view.GetRowCellValue(rowHandle, "PROD_SEQ_NO").ToString();
                    int qty = Convert.ToInt32(editor.Text, NumberFormatInfo.CurrentInfo);
                    int boxQty = 0;

                    if (rowHandle != GridControl.NewItemRowHandle)
                    {
                        int assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                        if (qty < assignQty) //qty > freeQty || 
                        {
                            e.Cancel = true;

                            XtraMessageBox.Show(this, "Qty can't less than Assign Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            view.FocusedRowHandle = rowHandle;
                            view.ShowEditor();

                            editor.Focus();
                            editor.SelectAll();
                        }
                        else
                        {

                            boxQty = this.GetTotalBoxQty(productSeq, qty);
                            view.SetFocusedRowCellValue("NO_OF_BOX", boxQty);
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        boxQty = this.GetTotalBoxQty(productSeq, qty);
                        view.SetFocusedRowCellValue("NO_OF_BOX", boxQty);
                        e.Cancel = false;
                    }

                    
                    //int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                    //switch (flag)
                    //{
                    //    case 1: //edit
                    //        int assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                    //        if (qty < assignQty) //qty > freeQty || 
                    //        {
                    //            e.Cancel = true;

                    //            XtraMessageBox.Show(this, "Qty can't less than Assign Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    //            view.FocusedRowHandle = rowHandle;
                    //            view.ShowEditor();

                    //            editor.Focus();
                    //            editor.SelectAll();
                    //        }
                    //        else
                    //        {
                    //            e.Cancel = false;
                    //        }
                    //        break;
                    //    case 2: //new line
                    //        e.Cancel = false;
                    //        //if (qty > freeQty)
                    //        //{
                    //        //    XtraMessageBox.Show(this, "Qty can't more than Free Stock", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    //        //    editor.Undo();

                    //        //    view.FocusedRowHandle = rowHandle;
                    //        //    view.ShowEditor();

                    //        //}
                    //        //else
                    //        //{
                    //        //    e.Cancel = false;
                    //        //}
                    //        break;
                    //    default:
                    //        break;
                    //}

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

        }

        private void frmShippingOrder_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcShippingOrder.SelectedTabPage == this.xtpShippingOrderList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetBindingShippingOrder(this.txtSO_NO.Text);
                            //this.GetShippingOrderDetail(this.txtSO_NO.Text, (string)this.lueWarehouse.EditValue);
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
            this.GetShippingOrderList(whID, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            try
            {
                GridView view = this.grdSODetail.Views[0] as GridView;

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                int rowHandle = view.FocusedRowHandle;

                int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                DialogResult isDelete;
                int assignQty = 0;
                switch (flag)
                {
                    case 1:
                        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                        if (assignQty == 0)
                        {
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteSODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                        break;
                    case 2:
                        isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            this.DeleteSODetail(view, rowHandle);

                            SendKeys.Send("{F2}");
                        }
                        break;
                    case 3:
                        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                        if (assignQty == 0)
                        {
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteSODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAdvShippingOrder advShippingOrd = new frmAdvShippingOrder())
                {
                    UiUtility.ShowPopupForm(advShippingOrd, this, true);

                    this.AdvanceSearchShippingOrder(advShippingOrd.SO_NO, advShippingOrd.REF_NO, advShippingOrd.WH_ID, advShippingOrd.FROM_DATE, advShippingOrd.TO_DATE);
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
                    this.GetShippingOrderList(whID, editor.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
                    break;
                default:
                    break;
            }
        }

        private void frmShippingOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdShippingOrder.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdSODetail.Views[0]);

            this.Controls.Clear();
        }

        private void grvSODetail_GotFocus(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            view.ShowEditor();
        }

        private void grvSODetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = ExceptionMode.NoAction;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InvalidRowException " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_rps_btePRODUCT_NO_KeyDown(object sender, KeyEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;

            if (e.Control == true && e.KeyCode == Keys.S)
            {
                btnSave.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (editor.OldEditValue == null) return;

                    if (editor.OldEditValue.ToString() == editor.EditValue.ToString())
                    {
                        editor.SendKey(new KeyEventArgs(Keys.Tab));
                    }
                }
            }
        }

        private void grvSODetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            bool isAdjust = false;
            BaseView baseView = sender as BaseView;

            ColumnView colView = (ColumnView)this.grdSODetail.MainView;
            int iCompare, iValue;
            try
            {
                if (e.RowHandle == colView.FocusedRowHandle)
                {
                    if (e.RowHandle != GridControl.NewItemRowHandle)
                    {
                        switch (e.Column.FieldName)
                        {
                            case "ASSIGN_QTY":
                                iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "ASSIGN_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                                iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                                if (iCompare != iValue)
                                {
                                    isAdjust = true;
                                }

                                break;
                            case "PICKED_QTY":
                                iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                                iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

                                if (iCompare != iValue)
                                {
                                    isAdjust = true;
                                }
                                break;
                            case "LOADED_QTY":
                                iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "LOADED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                                iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

                                if (iValue > 0 && iValue != iCompare)
                                {
                                    isAdjust = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (isAdjust)
                    {
                        //Apply the appearance of the SelectedRow
                        if (this.FormState != eFormState.ReadOnly)
                        {
                            e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
                        }
                        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                        //e.Appearance.Options.UseForeColor = true;
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            DialogResult isComfirm = XtraMessageBox.Show(this, "Do you want to post data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (isComfirm == DialogResult.Yes)
            {
                List<ShippingOrder> lstShipOrd = new List<ShippingOrder>(1);
                ShippingOrder shipOrd = new ShippingOrder { SO_NO = this.txtSO_NO.Text };
                lstShipOrd.Add(shipOrd);
                this.ShippingOrderPostData(lstShipOrd);
            }
            
        }

        

    }
}