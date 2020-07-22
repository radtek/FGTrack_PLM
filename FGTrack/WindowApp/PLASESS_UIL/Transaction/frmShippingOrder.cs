using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
using HTN.BITS.UIL.PLASESS.Reports;
using DevExpress.XtraGrid.Columns;
using HTN.BITS.UIL.PLASESS.Component.CSV;
using HTN.BITS.UIL.PLASESS.ConfirmForms;

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

            base.LoadGridLayout(this.grdPalletHdr);

            this.palletSelect = new GridCheckMarksSelection(this.grvPalletHdr);
            this.SerialSelect = new GridCheckMarksSelection(this.grvPalletDtl, "CARGO_STATUS", "<>", "'L'");

            this.palletSelect.ClearSelection();

            //base.LoadGridLayout(this.Name, this.grdShippingOrder.Views[0]);
            //base.LoadGridLayout(this.Name, this.grdSODetail.Views[0]);
        }



        #region "Variable Member"

        private bool isEditPallet = false;

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbShippingOrderDtl;
        private DataTable dtbPalletHdr;
        private List<ShippingOrderDtl> delSODtl;

        private GridCheckMarksSelection palletSelect;

        private GridCheckMarksSelection SerialSelect;

        private ShippingOrder shipOrd = null;

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
                return string.Format("ShippingOrder_List_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("ShippingOrder_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Pallet_Detail
        {
            get
            {
                return string.Format("Pallet_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    //base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null, "Shipping_Order", true, this.shipOrd);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.XLS, this.FileName_Pallet_Detail + ".xls", null);
                }
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    //base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null, "Shipping_Order", true, this.shipOrd);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.XLSX, this.FileName_Pallet_Detail + ".xlsx", null);
                }
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.PDF, this.FileName_Pallet_Detail + ".pdf", null);
                }
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.RTF, this.FileName_Pallet_Detail + ".rtf", null);
                }
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.TEXT, this.FileName_Pallet_Detail + ".txt", null);
                }
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
                if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
                {
                    //detail
                    base.ViewExportToExcel(this.grdSODetail.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
                }
                else
                {
                    //Pallet Detail
                    base.ViewExportToExcel(this.grdPalletDtl.Views[0], GridExportType.HTML, this.FileName_Pallet_Detail + ".html", null);
                }
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

                        this.ChangeControlState(false,0);

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

                        //PRODUCT_NO
                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;

                        //PACKAGING
                        view.Columns["PACKAGING"].OptionsColumn.ReadOnly = false;
                        view.Columns["PACKAGING"].OptionsColumn.AllowFocus = true;
                        view.Columns["PACKAGING"].OptionsColumn.AllowEdit = true;

                        //QTY
                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = true;

                        //PO_NO
                        view.Columns["PO_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PO_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PO_NO"].OptionsColumn.AllowEdit = true;

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

                        this.ChangeControlState(false, view.RowCount);

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

                        //PRODUCT_NO
                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;

                        //PACKAGING
                        view.Columns["PACKAGING"].OptionsColumn.ReadOnly = false;
                        view.Columns["PACKAGING"].OptionsColumn.AllowFocus = true;
                        view.Columns["PACKAGING"].OptionsColumn.AllowEdit = true;

                        //QTY
                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = true;

                        //PO_NO
                        view.Columns["PO_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PO_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PO_NO"].OptionsColumn.AllowEdit = true;

                        //REMARK
                        view.Columns["REMARK"].OptionsColumn.ReadOnly = false;
                        view.Columns["REMARK"].OptionsColumn.AllowFocus = true;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = true;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true,0);

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

                        //PRODUCT_NO
                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = false;

                        //PACKAGING
                        view.Columns["PACKAGING"].OptionsColumn.ReadOnly = true;
                        view.Columns["PACKAGING"].OptionsColumn.AllowFocus = false;
                        view.Columns["PACKAGING"].OptionsColumn.AllowEdit = false;

                        //QTY
                        view.Columns["QTY"].OptionsColumn.ReadOnly = true;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = false;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = false;

                        //UNIT_PRICE
                        view.Columns["UNIT_PRICE"].OptionsColumn.ReadOnly = true;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowFocus = false;
                        view.Columns["UNIT_PRICE"].OptionsColumn.AllowEdit = false;

                        //PO_NO
                        view.Columns["PO_NO"].OptionsColumn.ReadOnly = true;
                        view.Columns["PO_NO"].OptionsColumn.AllowFocus = false;
                        view.Columns["PO_NO"].OptionsColumn.AllowEdit = false;

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

        private void ChangeControlState(bool state, int prdCount)
        {
            if (prdCount > 0)
            {
                this.btePARTY_ID.Properties.ReadOnly = true;
                this.btePARTY_ID.Properties.Buttons[0].Enabled = false;
            }
            else {
                this.btePARTY_ID.Properties.ReadOnly = state;
                this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;
            }
            this.lueWarehouse.Properties.ReadOnly = state;

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

        private void GridDetail_CheckPreviouseColumn(GridView view, GridColumn preColumn)
        {
            string resultValue = string.Empty;
            switch (preColumn.FieldName)
            {
                case "PRODUCT_NO":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                        view.ShowEditor();
                    }
                    break;
                case "QTY":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                        view.ShowEditor();
                    }
                    break;
                case "PO_NO":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                        view.ShowEditor();
                    }
                    break;
                default:
                    break;
            }


        }

        private void ClearDataOnScreen()
        {
            this.txtSO_NO.Text = string.Empty;
            this.txtSO_DATE.EditValue = DateTime.Now;
            this.txtPOST_REF.EditValue = null;
            this.lueWarehouse.EditValue = null;
            this.btePARTY_ID.EditValue = null;
            this.txtPARTY_NAME.EditValue = null;
            this.txtREF_NO.EditValue = null;
            this.dtpREF_DATE.EditValue = null;
            this.dtpREF_DATE.Properties.NullDate = string.Empty;
            this.dtpETA.EditValue = null;
            this.dtpETA.Properties.NullDate = string.Empty;
            this.txtREMARK.Text = string.Empty;

            this.icbREC_STAT.EditValue = true;

            this.shipOrd = null;

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

                    List<Packaging> lstPackaging = shipOrdBll.GetPackaging();
                    if (lstPackaging != null)
                    {
                        this.grvSODetail_rps_luePACKAGING.DataSource = lstPackaging;
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
            try
            {
                GridView view = (GridView)this.grdSODetail.MainView;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    this.shipOrd = shipOrdBll.GetShippingOrder(soNo);
                }

                if (shipOrd != null)
                {
                    base.ClearValidControls(this, ref this.dxErrorProvider1);

                    this.txtSO_NO.Text = this.shipOrd.SO_NO;

                    if (this.shipOrd.SO_DATE.HasValue)
                        this.txtSO_DATE.EditValue = this.shipOrd.SO_DATE.Value;
                    else
                        this.txtSO_DATE.EditValue = null;

                    this.txtPOST_REF.EditValue = this.shipOrd.POST_REF;
                    this.lueWarehouse.EditValue = this.shipOrd.WH_ID;
                    this.btePARTY_ID.EditValue = this.shipOrd.PARTY_ID;
                    this.txtPARTY_NAME.Text = this.shipOrd.PARTY_NAME;
                    this.txtREF_NO.Text = this.shipOrd.REF_NO;

                    if (shipOrd.PARTY_ID == "PST008")
                        view.Columns["LOADED_QTY"].Caption = "MTST Received";
                    else
                        view.Columns["LOADED_QTY"].Caption = "Loaded**";

                    if (this.shipOrd.REF_DATE.HasValue)
                        this.dtpREF_DATE.EditValue = this.shipOrd.REF_DATE.Value;
                    else
                        this.dtpREF_DATE.EditValue = null;

                    if (this.shipOrd.ETA.HasValue)
                        this.dtpETA.EditValue = this.shipOrd.ETA;
                    else
                        this.dtpETA.EditValue = null;

                    this.txtREMARK.Text = this.shipOrd.REMARK;

                    this.icbREC_STAT.EditValue = this.shipOrd.REC_STAT;

                    this.GetShippingOrderDetail(this.shipOrd.SO_NO, this.shipOrd.WH_ID);

                    this.GetPalletHdr(this.shipOrd.SO_NO);

                    this.xtcItemDetail.SelectedTabPage = this.xtpShippingItem;
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

                //this.CheckEnablePostData("[QTY] = [LOADED_QTY]");
                this.btnPostData.Enabled = this.CheckEnablePostCSV("[QTY] = [ASSIGN_QTY]");
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

        public void PostShippingOrderToCSV(string soNo)
        {
            string filename = string.Empty;
            try
            {
                DataTable dtb;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    dtb = shipOrdBll.GetCompletedSO(soNo, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (dtb != null)
                {

                    using (SaveFileDialog fdlg = new SaveFileDialog { Title = "Save Export CSV File", Filter = "CSV files (*.csv)|*.csv", FilterIndex = 1, RestoreDirectory = true, FileName = string.Format(@"{0}.CSV", soNo) })
                    {

                        if (fdlg.ShowDialog() == DialogResult.OK)
                        {
                            filename = fdlg.FileName;
                        }
                        else
                        {
                            filename = string.Empty;
                        }
                    }

                    if (!string.IsNullOrEmpty(filename))
                    {
                        using (CsvWriter writer = new CsvWriter() { Request_Header = true })
                        {
                            writer.WriteCsv(dtb, filename); //string.Format(@"C:\temp\{0}.CSV", soNo)
                        }

                        using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                        {
                            shipOrdBll.UpdatePostSO(soNo, filename, ((frmMainMenu)this.ParentForm).UserID);
                        }

                        this.txtPOST_REF.EditValue = filename;
                        this.btnPostData.Enabled = false;

                        GridView viewList = (GridView)this.grdShippingOrder.MainView;
                        viewList.SetFocusedRowCellValue("POST_REF", filename);

                        ICollection<string> files = new System.Collections.ObjectModel.Collection<string>()
                        {
                            filename
                        };

                        this.OpenPath(files);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //private void CheckEnablePostData(string expression)
        //{
        //    if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
        //    {
        //        if (this.dtbShippingOrderDtl != null)
        //        {
        //            DataRow[] rows = this.dtbShippingOrderDtl.Select(expression);
        //            if (rows.Length > 0 && this.dtbShippingOrderDtl.Rows.Count == rows.Length)
        //            {
        //                this.btnPostData.Enabled = true;
        //            }
        //            else
        //            {
        //                this.btnPostData.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            this.btnPostData.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        this.btnPostData.Enabled = false;
        //    }
        //}

        private bool CheckEnablePostCSV(string expression)
        {
            GridView view = this.grdSODetail.MainView as GridView;

            if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
            {
                if (this.dtbShippingOrderDtl != null)
                {

                    int qtyV = Convert.ToInt32(view.Columns["QTY"].SummaryItem.SummaryValue, NumberFormatInfo.CurrentInfo);
                    int assignV = Convert.ToInt32(view.Columns["ASSIGN_QTY"].SummaryItem.SummaryValue, NumberFormatInfo.CurrentInfo);

                    //DataRow[] rows = this.dtbShippingOrderDtl.Select(expression);
                    //if (rows.Length > 0 && this.dtbShippingOrderDtl.Rows.Count == rows.Length)
                    if(qtyV == assignV)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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

            //check validation ETA Date
            if (this.dtpETA.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.dtpETA, "ETA Date can't be Empty");
                this.dtpETA.Focus();
                return false;
            }
            else
            {
                if (this.FormState == eFormState.Add)
                {
                    TimeSpan diff = this.dtpETA.DateTime - DateTime.Now;
                    if (diff.Ticks < 0)
                    {
                        this.dxErrorProvider1.SetError(this.dtpETA, "ETA Date can't lower than current date/time!!");
                        this.dtpETA.Focus();
                        return false;
                    }
                }
            }

            //if (string.IsNullOrEmpty(this.txtREF_NO.Text))
            //{
            //    this.dxErrorProvider1.SetError(this.txtREF_NO, "PO Ref. can't be Empty");
            //    this.txtREF_NO.Focus();
            //    return false;
            //}

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
                if (this.dtpREF_DATE.EditValue != null)
                {
                    shippingOrd.REF_DATE = this.dtpREF_DATE.DateTime;
                }

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
                    shpOrdDtl.PACKAGING = dr["PACKAGING"].ToString();
                    shpOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.UNIT_PRICE = (dr["UNIT_PRICE"] as decimal?) ?? 0.0M;
                    //shpOrdDtl.UNIT_PRICE = Convert.ToDecimal(dr["UNIT_PRICE"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.PO_NO = dr["PO_NO"].ToString();
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
                    this.shipOrd = shippingOrd;
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

                if(shippingOrd.SO_DATE.HasValue)
                    this.txtSO_DATE.EditValue = shippingOrd.SO_DATE.Value;

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.txtSearch.Text = shippingOrd.SO_NO;
                this.lueSearchWH.EditValue = null;

                this.GetShippingOrderList(string.Empty, shippingOrd.SO_NO, null, null);
                this.GetShippingOrderDetail(shippingOrd.SO_NO, shippingOrd.WH_ID);

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
                if (this.dtpREF_DATE.EditValue != null)
                {
                    shippingOrd.REF_DATE = this.dtpREF_DATE.DateTime;
                }

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
                    shpOrdDtl.PACKAGING = dr["PACKAGING"].ToString();
                    shpOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.UNIT_PRICE = (dr["UNIT_PRICE"] as decimal?) ?? 0.0M;
                    //shpOrdDtl.UNIT_PRICE = Convert.ToDecimal(dr["UNIT_PRICE"], NumberFormatInfo.CurrentInfo);
                    shpOrdDtl.PO_NO = dr["PO_NO"].ToString();
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
                    this.shipOrd = shippingOrd;
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

                this.txtSearch.Text = shippingOrd.SO_NO;
                this.lueSearchWH.EditValue = null;

                this.GetShippingOrderList(string.Empty, shippingOrd.SO_NO, null, null);
                this.GetShippingOrderDetail(shippingOrd.SO_NO, shippingOrd.WH_ID);
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
                            view.SetFocusedRowCellValue("PACKAGING", "C"); //CARTON
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

        private void DeleteRecordDetail(GridView view)
        {
            try
            {
                int totalQty = 0;
                int rowHandle = view.FocusedRowHandle;

                int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                DialogResult isDelete;

                string productSeq = view.GetRowCellValue(rowHandle, "PROD_SEQ_NO").ToString();
                int assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                int curQty = Convert.ToInt32((view.GetRowCellValue(rowHandle, "QTY") as decimal?) ?? 0.0M, NumberFormatInfo.CurrentInfo);

                DataTable dt = this.grdSODetail.DataSource as DataTable;
                var results = from row in dt.AsEnumerable()
                              where row.Field<string>("PROD_SEQ_NO") == productSeq
                              group row by row.Field<string>("PROD_SEQ_NO") into grp
                              orderby grp.Key
                              select new
                              {
                                  SKU = grp.Key,
                                  QTY = grp.Sum(r => r.Field<decimal>("QTY"))
                              };   //.First().QTY;

                if (results.Any())
                {
                    totalQty = Convert.ToInt32(results.First().QTY, NumberFormatInfo.CurrentInfo);
                }


                switch (flag)
                {
                    case 1:
                        if ((totalQty - curQty) < assignQty)
                        {
                            XtraMessageBox.Show(this, "Order qty can't less than Assign Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteSODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                        }

                        //if (assignQty == 0)
                        //{
                        //    isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        //    if (isDelete == DialogResult.Yes)
                        //    {
                        //        this.DeleteSODetail(view, rowHandle);

                        //        SendKeys.Send("{F2}");
                        //    }
                        //}
                        //else
                        //{
                        //    XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        //}
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
                        if ((totalQty - curQty) < assignQty)
                        {
                            XtraMessageBox.Show(this, "Order qty can't less than Assign Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteSODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                        }
                        
                        //if (assignQty == 0)
                        //{
                        //    isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        //    if (isDelete == DialogResult.Yes)
                        //    {
                        //        this.DeleteSODetail(view, rowHandle);

                        //        SendKeys.Send("{F2}");
                        //    }
                        //}
                        //else
                        //{
                        //    XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        //}
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                            SODtl.LINE_NO = Convert.ToInt32(row["LINE_NO"], NumberFormatInfo.CurrentInfo);
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
                view.FocusedColumn = view.Columns["PRODUCT_NO"];
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
            DialogResult message = XtraMessageBox.Show("Post Data Completed\nDo you want to open directory?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

        //DETAILS ON THE PALLET

        private void GetPalletHdr(string so_no)
        {
            try
            {
                this.palletSelect.ClearSelection();
                this.SerialSelect.ClearSelection();

                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shipingOrdBll = new ShippingOrderBLL())
                {
                    this.dtbPalletHdr = shipingOrdBll.GetPalletHdr(so_no);
                }

                if (this.dtbPalletHdr != null)
                {
                    if (this.dtbPalletHdr.Rows.Count > 0)
                    {
                        this.txtNO_OF_PALLET.EditValue = this.dtbPalletHdr.Rows.Count;
                    }

                    this.btnAddPallet.Enabled = this.dtbPalletHdr.Rows.Count > 0;
                    this.txtNO_OF_PALLET.Enabled = !(this.dtbPalletHdr.Rows.Count > 0);
                    this.btnGenPallet.Enabled = !(this.dtbPalletHdr.Rows.Count > 0);
                }

                this.grdPalletHdr.DataSource = this.dtbPalletHdr;
                UiUtility.ClearSelection(this.grdPalletHdr.MainView);
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

        private void GetPalletDtl(string palletno)
        {
            try
            {
                this.SerialSelect.ClearSelection();

                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                DataTable dtbPalletDtl = null;

                using (ShippingOrderBLL shipingOrdBll = new ShippingOrderBLL())
                {
                    dtbPalletDtl = shipingOrdBll.GetPalletDtl(palletno);
                }

                GridView view = this.grdPalletDtl.MainView as GridView;

                view.ViewCaption = string.Format("PALLET # {0}", palletno);

                this.grdPalletDtl.DataSource = dtbPalletDtl;

                UiUtility.ClearSelection(this.grdPalletDtl.MainView);
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

        private void GridDetail_DeleteLine(ColumnView view, int rowSelect)
        {
            if (view == null || view.SelectedRowsCount == 0) return;

            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                rows[i] = view.GetDataRow(rowSelect);
            }


            view.BeginSort();
            try
            {
                int flag = 0;
                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        flag = Convert.ToInt32(row["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag.Equals(2))
                            row.Delete();
                        else
                            row["FLAG"] = 0;
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
                this.dtbPalletHdr.AcceptChanges();

                DataView dtv = new DataView(this.dtbPalletHdr);
                dtv.RowFilter = "[FLAG] <> 0";

                this.grdPalletHdr.DataSource = dtv;
            }
        }

        private void InsertPalletHdr()
        {
            string result = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.dtbPalletHdr.AcceptChanges();

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    result = shipOrdBll.InsertPalletHdr(this.dtbPalletHdr, ((frmMainMenu)this.ParentForm).UserID);
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
            }
        }

        private void UpdatePalletHdr()
        {
            string result = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.dtbPalletHdr.AcceptChanges();

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    result = shipOrdBll.UpdatePalletHdr(this.dtbPalletHdr, ((frmMainMenu)this.ParentForm).UserID);
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
                this.Cursor = Cursors.Default;
            }
        }

        private void PrintDetailsOnPallet(List<string> lstPallet, string whid)
        {
            //int printSeq = 0;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    ds = shipOrdBll.PrintDetailsOnPalletReport(lstPallet, whid, ((frmMainMenu)this.ParentForm).UserID);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;


                RPT_PALLET_DETAIL rpt = new RPT_PALLET_DETAIL();

                rpt.DataSource = ds;
                rpt.CreateDocument();
                //rpt.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

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

            }
        }

        private void DeletePalletDetail(GridView view, int rowSelect)
        {
            string result = string.Empty;

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
                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                        {
                            result = shipOrdBll.RollBackRepack(row["PALLET_NO"].ToString(), row["SERIAL_NO"].ToString(), ((frmMainMenu)this.ParentForm).UserID);
                        }

                        if(result.Equals("OK"))
                            row.Delete();
                        else
                            XtraMessageBox.Show(this, result, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); 
                            
                    }
                }
            }
            finally
            {
                view.EndSort();
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
            string wh = string.Empty;
            this.KeyPreview = true;
            this.xtpShippingOrderDetail.PageVisible = false;
            this.FormState = eFormState.ReadOnly;

            this.dteFromDate.DateTime = DateTime.Now;//UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now.AddDays(1d);

            this.InitializaLOVData();

            if (this.lueSearchWH.EditValue != null)
            {
                wh = (string)this.lueSearchWH.EditValue;
            }

            this.GetShippingOrderList(wh, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);
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
                this.GetShippingOrderDetail(txtSO_NO.Text, (string)lueWarehouse.EditValue);

                this.FormState = eFormState.Edit;
                this.lueWarehouse.Focus();
            }
            else
            {
                XtraMessageBox.Show(this, "This SO. already post data!!\nCan't Edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            //this.FormState = eFormState.Edit;
            //this.lueWarehouse.Focus();
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

            this.btnPostData.Enabled = this.CheckEnablePostCSV("[QTY] = [ASSIGN_QTY]");
        }

        private void btnPickingList_Click(object sender, EventArgs e)
        {
            try
            {
                frmPickingList fPrintPickingList = new frmPickingList { SO_NO = this.txtSO_NO.Text,PARTY_ID = this.btePARTY_ID.EditValue.ToString(), WH_ID = (string)this.lueWarehouse.EditValue, USER_ID = ((frmMainMenu)this.ParentForm).UserID };

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
            if (this.FormState == eFormState.ReadOnly) return;
            if (e.FocusedColumn == null) return;
            if (e.PrevFocusedColumn == null) return;

            try
            {
                GridView view = (GridView)sender;

                view.UpdateTotalSummary();

                switch (e.FocusedColumn.FieldName)
                {
                    case "PRODUCT_NO":
                        //no need to check
                        break;
                    case "PACKAGING":
                        //no need to check
                        if (e.PrevFocusedColumn != null)
                        {
                            switch (e.PrevFocusedColumn.FieldName)
                            {
                                case "PRODUCT_NO":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                default:
                                    break;
                            }
                            //if (e.PrevFocusedColumn.FieldName.Equals("PRODUCT_NO"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}
                        }
                        break;
                    case "QTY":
                        if (e.PrevFocusedColumn != null)
                        {
                            switch (e.PrevFocusedColumn.FieldName)
                            {
                                case "PRODUCT_NO":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "PACKAGING":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                default:
                                    break;
                            }
                            //if (e.PrevFocusedColumn.FieldName.Equals("PRODUCT_NO"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}
                        }
                        break;
                    case "PO_NO":
                        if (e.PrevFocusedColumn != null)
                        {
                            switch (e.PrevFocusedColumn.FieldName)
                            {
                                case "PRODUCT_NO":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "QTY":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "PACKAGING":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                default:
                                    break;
                            }
                            //if (e.PrevFocusedColumn.FieldName.Equals("PRODUCT_NO"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}
                            //if (e.PrevFocusedColumn.FieldName.Equals("QTY"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}
                        }
                        break;
                    case "REMARK":
                        if (e.PrevFocusedColumn != null)
                        {
                            switch (e.PrevFocusedColumn.FieldName)
                            {
                                case "PRODUCT_NO":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "PACKAGING":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "QTY":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                case "PO_NO":
                                    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                                    break;
                                default:
                                    break;
                            }

                            //if (e.PrevFocusedColumn.FieldName.Equals("PRODUCT_NO"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}

                            //if (e.PrevFocusedColumn.FieldName.Equals("QTY"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}

                            //if (e.PrevFocusedColumn.FieldName.Equals("PO_NO"))
                            //{
                            //    this.GridDetail_CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            //}
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "FocusedColumnChanged : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            //string result = string.Empty;
            //try
            //{
            //    GridView view = sender as GridView;

            //    if (e.PrevFocusedColumn != null)
            //    {
            //        switch (e.PrevFocusedColumn.FieldName)
            //        {
            //            case "PRODUCT_NO":
            //                result = view.GetFocusedRowCellDisplayText(e.PrevFocusedColumn);
            //                if (string.IsNullOrEmpty(result))
            //                {
            //                    view.FocusedColumn = e.PrevFocusedColumn;
            //                }

            //                break;
            //            case "QTY":
            //                result = view.GetFocusedRowCellDisplayText(e.PrevFocusedColumn);
            //                if (string.IsNullOrEmpty(result))
            //                {
            //                    view.FocusedColumn = e.PrevFocusedColumn;
            //                }
            //                break;
            //            case "PO_NO":
            //                result = view.GetFocusedRowCellDisplayText(e.PrevFocusedColumn);
            //                if (string.IsNullOrEmpty(result))
            //                {
            //                    view.FocusedColumn = e.PrevFocusedColumn;
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, "FocusedColumnChanged : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void grvSODetail_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;

                if(!string.IsNullOrEmpty(this.txtREF_NO.Text))
                    view.SetFocusedRowCellValue("PO_NO", this.txtREF_NO.Text);

                view.SetFocusedRowCellValue("ASSIGN_QTY", 0);
                view.SetFocusedRowCellValue("PICKED_QTY", 0);
                view.SetFocusedRowCellValue("LOADED_QTY", 0);
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
                    this.DeleteRecordDetail(view);
                    //int rowHandle = view.FocusedRowHandle;

                    //int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                    //DialogResult isDelete;
                    //int assignQty = 0;
                    //switch (flag)
                    //{
                    //    case 1:
                    //        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                    //        if (assignQty == 0)
                    //        {
                    //            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    //            if (isDelete == DialogResult.Yes)
                    //            {
                    //                this.DeleteSODetail(view, rowHandle);

                    //                SendKeys.Send("{F2}");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    //        }
                    //        break;
                    //    case 2:
                    //        isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    //        if (isDelete == DialogResult.Yes)
                    //        {
                    //            this.DeleteSODetail(view, rowHandle);

                    //            SendKeys.Send("{F2}");
                    //        }
                    //        break;
                    //    case 3:
                    //        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                    //        if (assignQty == 0)
                    //        {
                    //            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    //            if (isDelete == DialogResult.Yes)
                    //            {
                    //                this.DeleteSODetail(view, rowHandle);

                    //                SendKeys.Send("{F2}");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //int rowupdated = 0;
        private void grvSODetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;
            
            try
            {
                //if (rowupdated.Equals(0))
                //    rowupdated++;
                //else
                //{
                //    rowupdated = 0;
                //    return;
                //}

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

                if (rowView == null) return;

                if (rowView["PACKAGING"].ToString() == "")
                {
                    e.Valid = false;
                    view.FocusedColumn = view.Columns["PACKAGING"];
                    return;
                }

                if (rowView["QTY"].ToString() == "")
                {
                    e.Valid = false;
                    view.FocusedColumn = view.Columns["QTY"];
                    return;
                }

                if (rowView["PO_NO"].ToString() == "")
                {
                    e.Valid = false;
                    view.FocusedColumn = view.Columns["PO_NO"];
                    return;
                }
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
                            var pono = view.GetFocusedRowCellDisplayText("PO_NO");

                            var rowDup = (from row in this.dtbShippingOrderDtl.AsEnumerable()
                                          where ((string)row["PRODUCT_NO"] == fPrdList.PRODUCT_NO) && ((string)row["PO_NO"] == pono)
                                          select row).AsDataView(); 

                            //bool isDup = false;//UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                            //if (!isDup)
                            if(rowDup.Count.Equals(0))
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
                                XtraMessageBox.Show(this, "Duplicate Product# and PO# Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                            var pono = view.GetFocusedRowCellDisplayText("PO_NO");

                            var rowDup = (from row in this.dtbShippingOrderDtl.AsEnumerable()
                                          where ((string)row["PRODUCT_NO"] == fPrdList.PRODUCT_NO) && ((string)row["PO_NO"] == pono)
                                          select row).AsDataView(); 

                            //bool isDup = false;//UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                            //if (!isDup)
                            if(rowDup.Count.Equals(0))
                            {
                                editor.EditValue = fPrdList.PRODUCT_NO;
                                this.GetProductByNo(view, this.btePARTY_ID.EditValue.ToString(), this.txtREF_NO.Text, fPrdList.PRODUCT_NO, false);
                            }
                            else
                            {
                                e.Cancel = true;
                                editor.Undo();
                                XtraMessageBox.Show(this, "Duplicate Product# and PO# Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                            e.Cancel = true;
                    }
                }
                else
                {
                    var pono = view.GetFocusedRowCellDisplayText("PO_NO");

                    var rowDup = (from row in this.dtbShippingOrderDtl.AsEnumerable()
                                  where ((string)row["PRODUCT_NO"] == editor.Text) && ((string)row["PO_NO"] == pono)
                                  select row).AsDataView(); 

                    //bool isDup = false;//UiUtility.IsDuplicated(view, "PRODUCT_NO", editor.Text);
                    //if (isDup)
                    if (rowDup.Count > 0)
                    {
                        e.Cancel = true;
                        editor.Undo();
                        XtraMessageBox.Show(this, "Duplicate Product# and PO# Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

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
            //if (this.FormState == eFormState.ReadOnly) return;

            //TextEdit editor = (TextEdit)sender;
            //bool isValid = UiValidations.Validate_Empty(editor, ref this.dxErrorProvider1, "Value can't null", ErrorType.Warning);
            //if (!isValid)
            //{
            //    editor.Focus();
            //}
        }

        private void dtpREF_DATE_Validating(object sender, CancelEventArgs e)
        {
            //if (this.FormState == eFormState.ReadOnly) return;

            //DateEdit editor = (DateEdit)sender;
            //if (string.IsNullOrEmpty(editor.Text))
            //{
            //    UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Plan Date can't be null", ErrorType.Critical);
            //    editor.Focus();
            //}
            //else
            //{
            //    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            //}
        }

        private void grvSODetail_rps_PO_NO_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdSODetail.Views[0];
            TextEdit editor = (TextEdit)sender;

            try
            {
                int rowHandle = view.FocusedRowHandle;
                if (string.IsNullOrEmpty(editor.Text))
                {
                    e.Cancel = true;
                }
                else
                {
                    var prodno = view.GetFocusedRowCellDisplayText("PRODUCT_NO");

                    var rowDup = (from row in this.dtbShippingOrderDtl.AsEnumerable()
                                 where (row["PRODUCT_NO"].ToString() == prodno) && (row["PO_NO"].ToString() == editor.Text)
                                 select row).AsDataView(); 
                                  

                    if (rowDup.Count > 0)
                    {
                        e.Cancel = true;
                        editor.Undo();
                        XtraMessageBox.Show(this, "Duplicate Product# and PO# Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        e.Cancel = false;
                    }

                    //if (rowDup != null)
                    //{
                    //    object tes = rowDup;
                    //}


                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvSODetail_rps_QTY_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdSODetail.Views[0];
            TextEdit editor = (TextEdit)sender;

            int totalQty = 0;
            int boxQty = 0;

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

                    if (rowHandle != GridControl.NewItemRowHandle)
                    {
                        int assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                        int curQty = Convert.ToInt32((view.GetRowCellValue(rowHandle, "QTY") as decimal?) ?? 0.0M, NumberFormatInfo.CurrentInfo);

                        DataTable dt = this.grdSODetail.DataSource as DataTable;
                        var results = from row in dt.AsEnumerable()
                                      where row.Field<string>("PROD_SEQ_NO") == productSeq
                                      group row by row.Field<string>("PROD_SEQ_NO") into grp
                                      orderby grp.Key
                                      select new
                                      {
                                          SKU = grp.Key,
                                          QTY = grp.Sum(r => r.Field<decimal>("QTY"))
                                      };   //.First().QTY;

                        if (results.Any())
                        {
                            totalQty = Convert.ToInt32(results.First().QTY, NumberFormatInfo.CurrentInfo);
                        }


                        if ((totalQty - curQty + qty) < assignQty) //qty > freeQty || 
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

                this.DeleteRecordDetail(view);

                //int rowHandle = view.FocusedRowHandle;

                //int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                //DialogResult isDelete;
                //int assignQty = 0;
                //switch (flag)
                //{
                //    case 1:
                //        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                //        if (assignQty == 0)
                //        {
                //            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //            if (isDelete == DialogResult.Yes)
                //            {
                //                this.DeleteSODetail(view, rowHandle);

                //                SendKeys.Send("{F2}");
                //            }
                //        }
                //        else
                //        {
                //            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //        }
                //        break;
                //    case 2:
                //        isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //        if (isDelete == DialogResult.Yes)
                //        {
                //            this.DeleteSODetail(view, rowHandle);

                //            SendKeys.Send("{F2}");
                //        }
                //        break;
                //    case 3:
                //        assignQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "ASSIGN_QTY"), NumberFormatInfo.CurrentInfo);
                //        if (assignQty == 0)
                //        {
                //            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //            if (isDelete == DialogResult.Yes)
                //            {
                //                this.DeleteSODetail(view, rowHandle);

                //                SendKeys.Send("{F2}");
                //            }
                //        }
                //        else
                //        {
                //            XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //        }
                //        break;
                //    default:
                //        break;
                //}
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
            //Save Default WH
            if (this.lueSearchWH.EditValue != null)
            {
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Default_WH = (string)this.lueSearchWH.EditValue;
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
            }

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
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            //DialogResult isComfirm = XtraMessageBox.Show(this, "Do you want to post data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //if (isComfirm == DialogResult.Yes)
            //{
            //    List<ShippingOrder> lstShipOrd = new List<ShippingOrder>(1);
            //    ShippingOrder shipOrd = new ShippingOrder { SO_NO = this.txtSO_NO.Text };
            //    lstShipOrd.Add(shipOrd);
            //    this.ShippingOrderPostData(lstShipOrd);
            //}

            try
            {
                bool cando = this.CheckEnablePostCSV("[QTY] = [ASSIGN_QTY]"); //change by jack 9-Mar-2015 10:20 am

                if (string.IsNullOrEmpty(this.txtPOST_REF.Text) && cando)
                {
                    this.PostShippingOrderToCSV(this.txtSO_NO.Text);
                    /*
                    //check already send
                    if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
                    {
                        this.PostShippingOrderToCSV(this.txtSO_NO.Text);
                    }
                    else
                    {
                        DialogResult result = XtraMessageBox.Show(this, "This Shipping Order already Post!!\nDo you want to post again?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            DialogResult isAuthen = DialogResult.None;

                            using (frmCOFAuthen fAuthen = new frmCOFAuthen())
                            {
                                isAuthen = UiUtility.ShowPopupForm(fAuthen, this, true);
                            }

                            if (isAuthen == DialogResult.OK)
                            {
                                this.PostShippingOrderToCSV(this.txtSO_NO.Text);
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Can't Post CSV data (Second times)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                     * */
                }
                else
                {
                    XtraMessageBox.Show(this, "Can't Post Data to CSV File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnGenPallet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNO_OF_PALLET.Text))
            {
                this.dxErrorProvider1.SetError(this.txtNO_OF_PALLET, "No. of Pallet can't be Empty");
                this.txtNO_OF_PALLET.Focus();
                return;
            }

            string sono = this.txtSO_NO.Text;

            string palletno = "PL" + sono.Substring(2);
            int noOfPallet = Convert.ToInt32(this.txtNO_OF_PALLET.EditValue, NumberFormatInfo.CurrentInfo);

            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                //DataTable dtbPalletHdr = this.grdPalletHdr.DataSource as DataTable;
                //using (ShippingOrderBLL shipingOrdBll = new ShippingOrderBLL())
                //{
                //    dtbPalletHdr = shipingOrdBll.GetPalletHdr(sono);
                //}

                if (this.dtbPalletHdr != null)
                {
                    DataRow newRow;

                    this.dtbPalletHdr.BeginLoadData();

                    for (int i = 0; i < noOfPallet; i++)
                    {
                        newRow = this.dtbPalletHdr.NewRow();

                        newRow["SO_NO"] = sono;
                        newRow["PALLET_NO"] = string.Format("{0}-{1}", palletno, (i + 1));
                        newRow["PALLET_SEQ"] = (i + 1).ToString();
                        newRow["PALLET_TOTAL"] = noOfPallet;
                        newRow["PALLET_STATUS"] = "G";
                        newRow["WH_ID"] = this.lueWarehouse.EditValue.ToString();
                        newRow["FLAG"] = 2; //New Record.

                        this.dtbPalletHdr.Rows.Add(newRow);
                    }

                    this.dtbPalletHdr.EndLoadData();

                    this.dtbPalletHdr.AcceptChanges();

                    this.grdPalletHdr.DataSource = this.dtbPalletHdr;
                }
                else
                {
                    throw new Exception("Null Pallet!!");
                }
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

        private void btnSavePallet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isEditPallet)
                    this.InsertPalletHdr();
                else
                    this.UpdatePalletHdr();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvPalletHdr_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            
            try
            {
                if (this.grdPalletHdr.DataSource == null) return;

                if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    string palletno = (string)view.GetFocusedRowCellValue("PALLET_NO");

                    this.GetPalletDtl(palletno);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDelPallet_Click(object sender, EventArgs e)
        {
            this.palletSelect.ClearSelection();
            GridView view = this.grdPalletHdr.MainView as GridView;

            try
            {
                if (this.grdPalletHdr.DataSource == null) return;

                if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    DataTable dtbPalletDtl = this.grdPalletDtl.DataSource as DataTable;

                    if (dtbPalletDtl.Rows.Count > 0)
                    {
                        XtraMessageBox.Show(this, "Already Scan to Pallet\nCan't be Delete!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        this.isEditPallet = true;
                        int rowHandle = view.FocusedRowHandle;

                        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            this.GridDetail_DeleteLine(view, rowHandle);

                            //Re-Update 
                            this.txtNO_OF_PALLET.EditValue = view.RowCount;

                            DataRow[] rows = this.dtbPalletHdr.Select("[FLAG] <> 0", "[PALLET_SEQ] ASC");

                            for (int i = 0; i < rows.Length; i++)
                            {
                                rows[i]["PALLET_SEQ"] = (i + 1);
                                //view.SetRowCellValue(i, "PALLET_SEQ", (i + 1));
                                rows[i]["PALLET_TOTAL"] = view.RowCount;
                                //view.SetRowCellValue(i, "PALLET_TOTAL", view.RowCount);

                                if (Convert.ToInt32(view.GetRowCellValue(i, "FLAG"), NumberFormatInfo.CurrentInfo) != 2)
                                    rows[i]["FLAG"] = 3;
                            }

                            this.dtbPalletHdr.AcceptChanges();
                            //this.grdPalletHdr.RefreshDataSource();

                        }
                    }


                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnAddPallet_Click(object sender, EventArgs e)
        {
            this.palletSelect.ClearSelection();
            GridView view = this.grdPalletHdr.MainView as GridView;

            try
            {
                if (this.grdPalletHdr.DataSource == null) return;

                this.isEditPallet = true;
                string sono = this.txtSO_NO.Text;

                string palletno = "PL" + sono.Substring(2);
                //int noOfPallet = Convert.ToInt32(this.txtNO_OF_PALLET.EditValue, NumberFormatInfo.CurrentInfo);

                //Re-Update 
                this.txtNO_OF_PALLET.EditValue = view.RowCount + 1;

                DataRow[] rows = this.dtbPalletHdr.Select("[FLAG] <> 0", "[PALLET_SEQ] ASC");

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i]["PALLET_SEQ"] = (i + 1);
                    //view.SetRowCellValue(i, "PALLET_SEQ", (i + 1));

                    rows[i]["PALLET_TOTAL"] = view.RowCount + 1;
                    //view.SetRowCellValue(i, "PALLET_TOTAL", view.RowCount + 1);


                    if(Convert.ToInt32(view.GetRowCellValue(i, "FLAG"), NumberFormatInfo.CurrentInfo) != 2)
                        rows[i]["FLAG"] = 3;
                }

                this.dtbPalletHdr.AcceptChanges();

                DataRow newRow = this.dtbPalletHdr.NewRow();


                newRow["SO_NO"] = sono;
                newRow["PALLET_NO"] = string.Format("{0}-{1}", palletno, (rows.Length + 1));
                newRow["PALLET_SEQ"] = (rows.Length + 1).ToString();
                newRow["PALLET_TOTAL"] = (rows.Length + 1);
                newRow["PALLET_STATUS"] = "G";
                newRow["WH_ID"] = this.lueWarehouse.EditValue.ToString();
                newRow["FLAG"] = 2; //New Record.

                this.dtbPalletHdr.Rows.Add(newRow);
                this.dtbPalletHdr.AcceptChanges();

                DataView dtv = new DataView(this.dtbPalletHdr);
                dtv.RowFilter = "[FLAG] <> 0";

                this.grdPalletHdr.DataSource = dtv;

            }
            catch (Exception ex)
            {
            }
        }

        private void btnPrintPallet_Click(object sender, EventArgs e)
        {
            string whid = string.Empty;

            List<string> lstPalletPrint = null;
            try
            {
                whid = this.lueWarehouse.EditValue.ToString();
                if (this.palletSelect.SelectedCount > 0)
                {
                    lstPalletPrint = new List<string>(this.palletSelect.SelectedCount);
                    for (int i = 0; i < this.palletSelect.SelectedCount; i++)
                    {
                        DataRowView row = this.palletSelect.GetSelectedRow(i) as DataRowView;

                        lstPalletPrint.Add(row["PALLET_NO"].ToString());
                    }

                    this.PrintDetailsOnPallet(lstPalletPrint, whid);

                    this.palletSelect.ClearSelection();

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Pallet No. to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvPalletDtl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedRowHandle == GridControl.InvalidRowHandle) return;

                if (e.KeyCode == Keys.Delete)
                {
                    int rowHandle = view.FocusedRowHandle;

                    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (isDelete == DialogResult.Yes)
                    {
                        this.DeletePalletDetail(view, rowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDelBox_Click(object sender, EventArgs e)
        {
            //GridView view = this.grdPalletDtl.MainView as GridView;

            //try
            //{
            //    if (this.grdPalletDtl.DataSource == null) return;

            //    if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
            //    {
            //        int rowHandle = view.FocusedRowHandle;

            //        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //        if (isDelete == DialogResult.Yes)
            //        {
            //            this.DeletePalletDetail(view, rowHandle);
            //        }
            //        //DataTable dtbPalletDtl = this.grdPalletDtl.DataSource as DataTable;

            //        //if (dtbPalletDtl.Rows.Count > 0)
            //        //{
            //        //    XtraMessageBox.Show(this, "Already Scan to Pallet\nCan't be Delete!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //        //}
            //        //else
            //        //{
            //        //    int rowHandle = view.FocusedRowHandle;

            //        //    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //        //    if (isDelete == DialogResult.Yes)
            //        //    {

            //        //    }
            //        //}


            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(this, "Please Select Product Card to be Delete!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}

            //----Test 
            string resultMsg = string.Empty;

            try
            {
                if (this.SerialSelect.SelectedCount > 0)
                {
                    DialogResult result = XtraMessageBox.Show(this, "Do you wnat to rollback items?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {

                        for (int i = 0; i < this.SerialSelect.SelectedCount; i++)
                        {
                            DataRowView row = this.SerialSelect.GetSelectedRow(i) as DataRowView;

                            if (row != null)
                            {
                                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                                {
                                    resultMsg = shipOrdBll.RollBackRepack(row["PALLET_NO"].ToString(), row["SERIAL_NO"].ToString(), ((frmMainMenu)this.ParentForm).UserID);
                                }

                                if (resultMsg.Equals("OK"))
                                    row.Delete();
                            }
                        }

                        this.SerialSelect.ClearSelection();
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "You not priviledge for rollback items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Record to Rollback!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dResult;
                string soNo = string.Empty;

                using (frmPopUpload_SO exUpload = new frmPopUpload_SO {USER_ID = ((frmMainMenu)this.ParentForm).UserID }) // { SO_NO = this.txtSO_NO.Text, USER_ID = ((frmMainMenu)this.ParentForm).UserID }
                {
                    dResult = UiUtility.ShowPopupForm(exUpload, this, true);

                    soNo = exUpload.SO_NO;
                }

                if (dResult == DialogResult.OK)
                {
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

        //Merge Cell by Jack 21-Aug-2014 14:40
        private void grvSODetail_CellMerge(object sender, CellMergeEventArgs e)
        {
            string p1 = string.Empty, p2 = string.Empty;
            GridView editor = sender as GridView;

            //if (e.Column.FieldName.Equals("DELIVERY_WEEK"))
            //{
            //    string week1 = (string)editor.GetRowCellValue(e.RowHandle1, e.Column);
            //    string week2 = (string)editor.GetRowCellValue(e.RowHandle2, e.Column);
            //    if (week1.Equals(week2))
            //    {
            //        e.Merge = true;
            //    }
            //    else
            //    {
            //        e.Merge = false;
            //    }
            //}
            //else
            //{
            //    e.Merge = false;
            //}

            switch (e.Column.FieldName)
            {
                case "FREE_STOCK":
                    p1 = (string)editor.GetRowCellValue(e.RowHandle1, "PRODUCT_NO");
                    p2 = (string)editor.GetRowCellValue(e.RowHandle2, "PRODUCT_NO");

                    if (p1.Equals(p2))
                    {
                        e.Merge = true;
                    }
                    else
                    {
                        e.Merge = false;
                    }
                    break;
                case "ASSIGN_QTY":
                    p1 = (string)editor.GetRowCellValue(e.RowHandle1, "PRODUCT_NO");
                    p2 = (string)editor.GetRowCellValue(e.RowHandle2, "PRODUCT_NO");

                    if (p1.Equals(p2))
                    {
                        e.Merge = true;
                    }
                    else
                    {
                        e.Merge = false;
                    }
                    break;
                case "PICKED_QTY":
                    p1 = (string)editor.GetRowCellValue(e.RowHandle1, "PRODUCT_NO");
                    p2 = (string)editor.GetRowCellValue(e.RowHandle2, "PRODUCT_NO");

                    if (p1.Equals(p2))
                    {
                        e.Merge = true;
                    }
                    else
                    {
                        e.Merge = false;
                    }
                    break;
                case "LOADED_QTY":
                    p1 = (string)editor.GetRowCellValue(e.RowHandle1, "PRODUCT_NO");
                    p2 = (string)editor.GetRowCellValue(e.RowHandle2, "PRODUCT_NO");

                    if (p1.Equals(p2))
                    {
                        e.Merge = true;
                    }
                    else
                    {
                        e.Merge = false;
                    }
                    break;
                default:
                    e.Merge = false;
                    break;
            }

            e.Handled = true;
        }

        private void xtcItemDetail_Enter(object sender, EventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            if (this.xtcItemDetail.SelectedTabPage == this.xtpShippingItem)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dtpETA_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            DateEdit editor = (DateEdit)sender;
            if (string.IsNullOrEmpty(editor.Text))
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "ETA Date can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                if (this.FormState == eFormState.Add)
                {
                    TimeSpan diff = this.dtpETA.DateTime - DateTime.Now;
                    if (diff.Ticks < 0)
                    {
                        UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "ETA Date can't lower than current date/time!!", ErrorType.Critical);
                        editor.Focus();
                    }
                    else
                    {
                        UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
                    }
                }
                else //Edit
                {
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
                }
            }
        }

        private void grvSODetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridColumnSummaryItem item = e.Item as GridColumnSummaryItem;
            GridView view = sender as GridView;

            try
            {
                //Start
                //if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                //{
                //}

                ////Calculate
                //if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                //{
                //}

                //Finalize
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    switch (item.FieldName)
                    {
                        case "ASSIGN_QTY":
                            e.TotalValue = this.SummaryMaxValue(this.dtbShippingOrderDtl, "PROD_SEQ_NO", "ASSIGN_QTY");
                            break;
                        case "PICKED_QTY":
                            e.TotalValue = this.SummaryMaxValue(this.dtbShippingOrderDtl, "PROD_SEQ_NO", "PICKED_QTY");
                            break;
                        case "LOADED_QTY":
                            e.TotalValue = this.SummaryMaxValue(this.dtbShippingOrderDtl, "PROD_SEQ_NO", "LOADED_QTY");
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private int SummaryMaxValue(DataTable dt, string groupKey, string maxField)
        {
            int result = 0;
            try
            {
                var sumV = (from tab in dt.AsEnumerable()
                             group tab by tab[groupKey]
                                 into groupDt
                                 select new
                                 {
                                     Group = groupDt.Key,
                                     Max = groupDt.Max((r) => int.Parse(r[maxField].ToString()))
                                 }).Sum(x => x.Max);

                result = sumV;          
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        private void grvSODetail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            ////for update total summary
            //GridView view = sender as GridView;
            //if (view.UpdateCurrentRow())
            //{
            //    view.UpdateTotalSummary();
            //}
        }

        private void grvSODetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    view.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                    e.Cancel = false;
                }
                else
                {
                    int flag = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                    switch (view.FocusedColumn.FieldName)
                    {
                        case "PRODUCT_NO":

                            if (flag == 2)
                            {
                                view.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                                e.Cancel = false;
                            }
                            else
                            {
                                view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                                e.Cancel = true;
                            }

                            break;
                        default:
                            view.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                            e.Cancel = false;
                            break;
                    }

                    
                }  
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btePARTY_ID_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit btnEdt = (ButtonEdit)sender;
            string partyId = string.Empty;
            if (btnEdt.EditValue != null)
                partyId = btnEdt.EditValue.ToString();

            GridView view = (GridView)this.grdSODetail.MainView;
            if (partyId == "PST008")
                view.Columns["LOADED_QTY"].Caption = "MTST Received";
            else
                view.Columns["LOADED_QTY"].Caption = "Loaded**";
        }

    }
}