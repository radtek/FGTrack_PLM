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
using HTN.BITS.UIL.PLASESS.Reports;
using DevExpress.XtraEditors.Controls;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmDeliveryOrder : BaseChildForm
    {
        public frmDeliveryOrder()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdDeliveryOrder);
            base.LoadGridLayout(this.grdDODetail);

            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbDeliveryOrderDtl;
        private List<DeliveryOrderDtl> delDeliDtl;

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
                return string.Format("DeliveryOrder_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("DeliveryOrder_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcDeliveryOrder.SelectedTabPage == this.xtpDeliveryOrderList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.XLS, this.FileName + ".xls", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                base.ViewExportToExcel(this.grdDeliveryOrder.Views[0], GridExportType.HTML, this.FileName + ".html", null);
            }
            else
            {
                //detail
                base.ViewExportToExcel(this.grdDODetail.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
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
                GridView view = (GridView)this.grdDODetail.Views[0];
                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntDeliveryOrder.Enabled = true;

                        this.dntDeliveryOrder.TextStringFormat = "      Add Mode      ";
                        this.dntDeliveryOrder.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnPrintDeliveryOrder.Enabled = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        view.FocusedColumn = view.Columns["PRODUCT_NO"];

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntDeliveryOrder.Enabled = true;

                        this.dntDeliveryOrder.TextStringFormat = "      Edit Mode      ";
                        this.dntDeliveryOrder.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnPrintDeliveryOrder.Enabled = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        view.FocusedColumn = view.Columns["PRODUCT_NO"];

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = true;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntDeliveryOrder.Enabled = false;

                        this.dntDeliveryOrder.TextStringFormat = " Record {0} of {1} ";
                        this.dntDeliveryOrder.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";
                        this.btnPrintDeliveryOrder.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, false);
                        UiUtility.SetGridReadOnly(view, true);

                        view.OptionsBehavior.Editable = true;

                        view.Columns["PRODUCT_NO"].OptionsColumn.ReadOnly = true;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowFocus = false;
                        view.Columns["PRODUCT_NO"].OptionsColumn.AllowEdit = false;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = true;
                        view.Columns["QTY"].OptionsColumn.AllowFocus = false;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = false;

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
            this.luePRODUCTION_TYPE.Properties.ReadOnly = state;
            this.lueDestination.Properties.ReadOnly = state;
            this.txtREF_NO.Properties.ReadOnly = state;
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
            this.txtDO_NO.Text = string.Empty;
            this.txtDO_DATE.EditValue = DateTime.Now;
            this.luePRODUCTION_TYPE.EditValue = null;
            this.lueDestination.EditValue = null;
            this.txtREF_NO.Text = string.Empty;
            this.dtpDELIVERY_DATE.EditValue = DateTime.Now;
            this.txtREMARK.Text = string.Empty;
            this.icbREC_STAT.EditValue = true;

            this.GetDeliveryOrderDetail(string.Empty);
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
                        this.grvDODetail_rps_lueUNIT_ID.DataSource = lstUnit;
                    }
                }

                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    List<ProductionType> lstProdType = delOrdBll.GetProductionTypeList();
                    if (lstProdType != null)
                    {
                        this.luePRODUCTION_TYPE.Properties.DataSource = lstProdType;
                    }


                    List<Warehouse> lstDest = delOrdBll.GetDestinationList();
                    if (lstDest != null)
                    {
                        this.lueDestination.Properties.DataSource = lstDest;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void InitializaLOVSearchData()
        {

            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    List<ProductionType> lstProdType = delOrdBll.GetProductionTypeList();
                    if (lstProdType != null)
                    {
                        lstProdType.Insert(0, new ProductionType { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.luePRODUCTION_TYPE_Search.Properties.DataSource = lstProdType;
                    }


                    List<Warehouse> lstDest = delOrdBll.GetDestinationList();
                    if (lstDest != null)
                    {
                        lstDest.Insert(0, new Warehouse { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.lueSearchDestination.Properties.DataSource = lstDest;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetDeliveryOrderList(string findValue, string doNo, string refNo, string prodType, string desTo,
                                                        DateTime? doFormDate, DateTime? doToDate, DateTime? deFormDate, DateTime? deToDate)
        {
            List<DeliveryOrder> lstDelOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    lstDelOrd = delOrdBll.GetDeliveryOrderList(findValue, doNo, refNo, prodType, desTo, doFormDate, doToDate, deFormDate, deToDate);
                }

                this.grdDeliveryOrder.DataSource = lstDelOrd;
                this.dntDeliveryOrder.DataSource = lstDelOrd;
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

        public void GetBindingDeliveryOrder(string doNo)
        {
            DeliveryOrder delOrd = null;
            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    delOrd = delOrdBll.GetDeliveryOrder(doNo);
                }

                if (delOrd != null)
                {
                    base.ClearValidControls(this, ref this.dxErrorProvider1);

                    this.txtDO_NO.Text = delOrd.DO_NO;
                    this.txtDO_DATE.EditValue = delOrd.DO_DATE;
                    this.luePRODUCTION_TYPE.EditValue = delOrd.PROD_TYPE;
                    this.lueDestination.EditValue = delOrd.TO_DEST;
                    this.txtREF_NO.Text = delOrd.REF_NO;
                    this.dtpDELIVERY_DATE.EditValue = delOrd.DELIVERY_DATE;
                    this.txtREMARK.Text = delOrd.REMARK;

                    this.icbREC_STAT.EditValue = delOrd.REC_STAT;

                    this.GetDeliveryOrderDetail(delOrd.DO_NO);
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

        public void GetDeliveryOrderDetail(string doNo)
        {
            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    this.dtbDeliveryOrderDtl = delOrdBll.GetDeliveryOrderDetail(doNo);
                }

                if (this.dtbDeliveryOrderDtl != null)
                {
                    this.grdDODetail.DataSource = this.dtbDeliveryOrderDtl;
                    this.ConditionsColumnView(this.grdDODetail);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ConditionsColumnView(GridControl grd)
        {
            //try
            //{
            //    StyleFormatCondition[] cnArr = new StyleFormatCondition[3];

            //    cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
            //    cnArr[0].Column = ((ColumnView)grd.MainView).Columns["ASSIGN_QTY"];
            //    cnArr[0].Expression = @"[QTY] <> [ASSIGN_QTY]";
            //    cnArr[0].Appearance.ForeColor = Color.Red;
            //    cnArr[0].Appearance.Options.UseBackColor = true;
            //    cnArr[0].Appearance.Options.UseForeColor = true;
            //    cnArr[0].ApplyToRow = false;

            //    cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
            //    cnArr[1].Column = ((ColumnView)grd.MainView).Columns["PICKED_QTY"];
            //    cnArr[1].Expression = @"[QTY] > [PICKED_QTY]";
            //    cnArr[1].Appearance.ForeColor = Color.Red;
            //    cnArr[1].ApplyToRow = false;

            //    cnArr[2] = new StyleFormatCondition(FormatConditionEnum.Expression);
            //    cnArr[2].Column = ((ColumnView)grd.MainView).Columns["LOADED_QTY"];
            //    cnArr[2].Expression = @"[PICKED_QTY] > 0 AND [PICKED_QTY] <> [LOADED_QTY]";
            //    cnArr[2].Appearance.ForeColor = Color.Red;
            //    cnArr[2].ApplyToRow = false;

            //    ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private bool IsFormValidated()
        {
            //Check control empty
            if (this.luePRODUCTION_TYPE.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.luePRODUCTION_TYPE, "Production Type can't be Empty");
                this.lueDestination.Focus();
                return false;
            }

            if (this.lueDestination.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueDestination, "To Destination can't be Empty");
                this.lueDestination.Focus();
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

        public void RefreshDeliveryOrderList()
        {
            try
            {
                string destination, prodType, refNo, doNo;
                DateTime? doFromDate, doToDate, deliFromDate, deliToDate;

                refNo = this.txtSearchRefNo.Text;
                doNo = this.txtSearchDoNo.Text;

                if (this.luePRODUCTION_TYPE_Search.EditValue != null)
                    prodType = this.luePRODUCTION_TYPE_Search.EditValue.ToString();
                else
                    prodType = string.Empty;

                if (this.lueSearchDestination.EditValue != null)
                    destination = this.lueSearchDestination.EditValue.ToString();
                else
                    destination = string.Empty;

                if (this.dteFromDate.EditValue != null)
                    doFromDate = this.dteFromDate.DateTime;
                else
                    doFromDate = null;

                if (this.dteToDate.EditValue != null)
                    doToDate = this.dteToDate.DateTime;
                else
                    doToDate = null;

                if (this.dteDeliveryFromDate.EditValue != null)
                    deliFromDate = this.dteDeliveryFromDate.DateTime;
                else
                    deliFromDate = null;

                if (this.dteDeliveryToDate.EditValue != null)
                    deliToDate = this.dteDeliveryToDate.DateTime;
                else
                    deliToDate = null;

                this.GetDeliveryOrderList(string.Empty, doNo, refNo, prodType, destination, doFromDate, doToDate, deliFromDate, deliToDate);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void InsertDeliveryOrder()
        {
            string result = string.Empty;
            DeliveryOrder delOrd = new DeliveryOrder();

            try
            {
                #region "Delivery Order Header"

                delOrd.DO_NO = string.Empty;
                delOrd.DO_DATE = DateTime.Now;
                delOrd.PROD_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                delOrd.TO_DEST = this.lueDestination.EditValue.ToString();
                delOrd.REF_NO = this.txtREF_NO.Text;
                delOrd.DELIVERY_DATE = this.dtpDELIVERY_DATE.DateTime;
                delOrd.REMARK = this.txtREMARK.Text;
                delOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                #region "Delivery Order Detail"
                DeliveryOrderDtl delOrdDtl;
                foreach (DataRow dr in this.dtbDeliveryOrderDtl.Rows)
                {
                    delOrdDtl = new DeliveryOrderDtl();

                    delOrdDtl.DO_NO = dr["DO_NO"].ToString();
                    delOrdDtl.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.InvariantInfo);
                    delOrdDtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                    delOrdDtl.UNIT_ID = dr["UNIT_ID"].ToString();
                    delOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.InvariantInfo);
                    

                    delOrd.AddItem(delOrdDtl);
                }
                #endregion

                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    result = delOrdBll.InsertDeliveryOrder(ref delOrd, ((frmMainMenu)this.ParentForm).UserID);
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
                this.txtDO_NO.Text = delOrd.DO_NO;
                this.txtDO_DATE.EditValue = delOrd.DO_DATE;

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.txtSearchDoNo.Text = delOrd.DO_NO;

                this.luePRODUCTION_TYPE_Search.EditValue = null;
                this.lueSearchDestination.EditValue = null;
                this.txtSearchRefNo.Text = string.Empty;
                this.dteFromDate.EditValue = null;
                this.dteToDate.EditValue = null;
                this.dteDeliveryFromDate.EditValue = null;
                this.dteDeliveryToDate.EditValue = null;

                this.GetDeliveryOrderList(string.Empty, delOrd.DO_NO, string.Empty, string.Empty, string.Empty,
                                          null, null, null, null);

                this.Cursor = Cursors.Default;
            }
        }

        public void UpdateDeliveryOrder()
        {
            string result = string.Empty;
            DeliveryOrder delOrd = new DeliveryOrder();

            try
            {
                #region "Delivery Order Header"

                delOrd.DO_NO = this.txtDO_NO.Text;
                delOrd.DO_DATE = Convert.ToDateTime(this.txtDO_DATE.EditValue, DateTimeFormatInfo.CurrentInfo);
                delOrd.PROD_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                delOrd.TO_DEST = this.lueDestination.EditValue.ToString();
                delOrd.REF_NO = this.txtREF_NO.Text;
                delOrd.DELIVERY_DATE = this.dtpDELIVERY_DATE.DateTime;
                delOrd.REMARK = this.txtREMARK.Text;
                delOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                #region "Delivery Order Detail"
                //Check Delete Recore
                if (this.delDeliDtl != null)
                {
                    foreach (DeliveryOrderDtl delDodtl in this.delDeliDtl)
                    {
                        delOrd.AddItem(delDodtl);
                    }
                }

                DeliveryOrderDtl delOrdDtl;
                foreach (DataRow dr in this.dtbDeliveryOrderDtl.Rows)
                {
                    delOrdDtl = new DeliveryOrderDtl();

                    delOrdDtl.DO_NO = dr["DO_NO"].ToString();
                    delOrdDtl.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.InvariantInfo);
                    delOrdDtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                    delOrdDtl.UNIT_ID = dr["UNIT_ID"].ToString();
                    delOrdDtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.InvariantInfo);
                    delOrdDtl.FLAG = Convert.ToInt32(dr["FLAG"], NumberFormatInfo.InvariantInfo);

                    delOrd.AddItem(delOrdDtl);
                }
                #endregion

                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    result = delOrdBll.UpdateDeliveryOrder(delOrd, ((frmMainMenu)this.ParentForm).UserID);
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
                if (this.delDeliDtl != null)
                {
                    this.delDeliDtl.Clear();
                    this.delDeliDtl = null;
                }

                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                //this.GetShippingOrderList(string.Empty, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdDeliveryOrder.Views[0];


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

        public void DeleteDeliveryOrder(string doNo)
        {
            string result = string.Empty;

            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    result = delOrdBll.DeleteDeliveryOrder(doNo, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Delete Complete", "Result", 50, 1000, 50, NotifyType.Safe);
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
                if (result.Equals("OK"))
                {
                    this.RefreshDeliveryOrderList();

                    this.xtpDeliveryOrderDetail.PageVisible = false;
                    this.xtpDeliveryOrderList.PageEnabled = true;
                    this.xtcDeliveryOrder.SelectedTabPage = this.xtpDeliveryOrderList;

                    this.btnAddNew.Visible = true;
                    this.btnExit.Visible = true;
                }

                this.Cursor = Cursors.Default;
            }
        }

        private bool GetProductByNo(GridView view, string prodNo, bool isCheck)
        {
            bool result = false;
            try
            {
                Product prod = null;
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    prod = delOrdBll.GetProductDetial(prodNo);
                }

                if (prod != null)
                {
                    if (!isCheck)
                    {
                        view.SetFocusedRowCellValue("DO_NO", this.txtDO_NO.Text);
                        view.SetFocusedRowCellValue("LINE_NO", view.RowCount);
                        view.SetFocusedRowCellValue("PROD_SEQ_NO", prod.PROD_SEQ_NO);
                        view.SetFocusedRowCellValue("PRODUCT_NO", prod.PRODUCT_NO);
                        view.SetFocusedRowCellValue("PRODUCT_NAME", prod.PRODUCT_NAME);
                        view.SetFocusedRowCellValue("QTY_PER_BOX", prod.BOX_QTY);
                        view.SetFocusedRowCellValue("UNIT_ID", prod.UNIT);
                        view.SetFocusedRowCellValue("DELIVERY_QTY", 0);
                        view.SetFocusedRowCellValue("STATUS", "PROCESS");
                        
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private void DeleteDeliveryOrdDetail(GridView view, int rowSelect)
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
                DeliveryOrderDtl doDtl;
                int flag = 0;
                if (this.delDeliDtl == null)
                {
                    this.delDeliDtl = new List<DeliveryOrderDtl>();
                }

                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        flag = Convert.ToInt32(row["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 1 || flag == 3)
                        {
                            doDtl = new DeliveryOrderDtl();
                            doDtl.DO_NO = row["SO_NO"].ToString();
                            doDtl.LINE_NO = (row["LINE_NO"] as int?) ?? 0;
                            doDtl.PROD_SEQ_NO = row["PROD_SEQ_NO"].ToString();
                            doDtl.FLAG = 0;

                            this.delDeliDtl.Add(doDtl);
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
                this.dtbDeliveryOrderDtl.AcceptChanges();
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ShowEditor();
            }
        }

        private void PrintDeliveryOrder(string doNo)
        {
            string userid = string.Empty;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                userid = ((frmMainMenu)this.ParentForm).UserID;
                DataSet ds;

                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    ds = delOrdBll.PrintDeliveryOrder(doNo, userid);
                }



                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_DELIVERY_ORDER rpt = new RPT_DELIVERY_ORDER();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = userid;
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


        #region "Custom Event Handle"

        private void GridControl_DoubleClick(object sender, EventArgs e)
        {
            //ColumnView columnView = sender as ColumnView;
            //Point pt = columnView.GridControl.PointToClient(Control.MousePosition);
           
            //GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
            //if (info.InRow || info.InRowCell)
            //{
            //    if (info.RowHandle == GridControl.NewItemRowHandle) return;

            //    if (info.Column.FieldName.Equals("PICKED_QTY") || info.Column.FieldName.Equals("LOADED_QTY"))
            //    {
            //        string sono = columnView.GetRowCellValue(info.RowHandle, "SO_NO").ToString();
            //        string prodseq = columnView.GetRowCellValue(info.RowHandle, "PROD_SEQ_NO").ToString();
            //        string type;
            //        if (info.Column.FieldName.Equals("LOADED_QTY"))
            //            type = "L";
            //        else
            //            type = string.Empty;

            //        this.ShowPopupDetailDetail(sono, prodseq, type);

            //    }
            //}
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

        private void frmDeliveryOrder_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVSearchData();
            //this.xtpDeliveryOrderDetail.PageVisible = false;
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();
            //this.GetDeliveryOrderList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
            //                          this.dteFromDate.DateTime, this.dteToDate.DateTime, null, null);

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmDeliveryOrder_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVSearchData();
            this.xtpDeliveryOrderDetail.PageVisible = false;
            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;
            this.InitializaLOVData();
            this.GetDeliveryOrderList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                      this.dteFromDate.DateTime, this.dteToDate.DateTime, null, null);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvDeliveryOrder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string doNo = view.GetRowCellValue(info.RowHandle, "DO_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpDeliveryOrderList.PageEnabled = false;

                    this.xtpDeliveryOrderDetail.PageVisible = true;
                    this.xtcDeliveryOrder.SelectedTabPage = this.xtpDeliveryOrderDetail;

                   
                    //Call record detail.
                    this.GetBindingDeliveryOrder(doNo);

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
                GridView view = (GridView)this.grdDeliveryOrder.Views[0]; //this.gridView2

                if (this.xtcDeliveryOrder.SelectedTabPage == this.xtpDeliveryOrderDetail)
                {
                    string doNo = view.GetFocusedRowCellValue("DO_NO").ToString();

                    this.GetBindingDeliveryOrder(doNo);
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

            this.lueDestination.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtpDeliveryOrderDetail.PageVisible = false;
                        this.xtpDeliveryOrderList.PageEnabled = true;
                        this.xtcDeliveryOrder.SelectedTabPage = this.xtpDeliveryOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                        this.GetBindingDeliveryOrder(this.txtDO_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpDeliveryOrderDetail.PageVisible = false;
                        this.xtpDeliveryOrderList.PageEnabled = true;
                        this.xtcDeliveryOrder.SelectedTabPage = this.xtpDeliveryOrderList;

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
                if (this.delDeliDtl != null)
                {
                    this.delDeliDtl.Clear();
                    this.delDeliDtl = null;
                }

                this.FormState = eFormState.ReadOnly;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.xtpDeliveryOrderList.PageEnabled = false;
            this.xtpDeliveryOrderDetail.PageVisible = true;
            this.xtcDeliveryOrder.SelectedTabPage = this.xtpDeliveryOrderDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.luePRODUCTION_TYPE.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;

            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertDeliveryOrder();
                    break;
                case eFormState.Edit:
                    this.UpdateDeliveryOrder();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void grvDODetail_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
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

        private void grvDODetail_InitNewRow(object sender, InitNewRowEventArgs e)
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

        private void grvDODetail_KeyDown(object sender, KeyEventArgs e)
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
                    int deliveryQty = 0;
                    switch (flag)
                    {
                        case 1:
                            deliveryQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "DELIVERY_QTY"), NumberFormatInfo.CurrentInfo);
                            if (deliveryQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteDeliveryOrdDetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                            break;
                        case 2:
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteDeliveryOrdDetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                            break;
                        case 3:
                            deliveryQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "DELIVERY_QTY"), NumberFormatInfo.CurrentInfo);
                            if (deliveryQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteDeliveryOrdDetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        private void grvDODetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;

            try
            {
                if ((view.FocusedRowHandle != GridControl.InvalidRowHandle) && (view.FocusedRowHandle != GridControl.NewItemRowHandle))
                {
                    int flag = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
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

        private void grvDODetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = (GridView)sender;

            try
            {
                DataRowView rowView = (DataRowView)e.Row;
                if (rowView["QTY"].ToString() == "")
                {
                    e.Valid = false;
                    view.FocusedColumn = view.Columns["QTY"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "ValidateRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvDODetail_rps_btePRODUCT_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdDODetail.Views[0];

                int deliveryQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "DELIVERY_QTY"), "0"), NumberFormatInfo.CurrentInfo);
                if (deliveryQty > 0)
                {
                    XtraMessageBox.Show(this, "Already Delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {

                    ButtonEdit btnEdit = (ButtonEdit)sender;
                    //Open Popup For Select Product.
                    frmLOVProduct fPrdList = new frmLOVProduct();
                    fPrdList.FormCalling = eFormCalling.fDeliveryOrder;
                    fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                    fPrdList.DeliveryOrder_GetProductList();
                    DialogResult result = UiUtility.ShowPopupForm(fPrdList, this, true);

                    if (result == DialogResult.OK)
                    {
                        bool isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                        if (!isDup)
                        {

                            btnEdit.EditValue = fPrdList.PRODUCT_NO;
                            btnEdit.Update();

                            //this.Get .GetPoDetailRecord(view, fMtl.PO_NO, fMtl.PO_LINE);
                            this.GetProductByNo(view, fPrdList.PRODUCT_NO, false);

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
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvDODetail_rps_btePRODUCT_NO_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdDODetail.Views[0];
            ButtonEdit editor = (ButtonEdit)sender;


            if (editor.EditValue != null)
            {
                if (this.FormState == eFormState.Edit)
                {
                    int deliveryQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "DELIVERY_QTY"), "0"), NumberFormatInfo.CurrentInfo);
                    if (deliveryQty > 0)
                    {
                        e.Cancel = true;

                        XtraMessageBox.Show(this, "Already Delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        view.CancelUpdateCurrentRow();
                        editor.SendKey(new KeyEventArgs(Keys.Escape));
                        return;
                    }
                }

                bool isValid = this.GetProductByNo(view, editor.EditValue.ToString(), true);//true
                if (!isValid)
                {
                    frmLOVProduct fPrdList = new frmLOVProduct();
                    fPrdList.FormCalling = eFormCalling.fDeliveryOrder;
                    fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                    fPrdList.DeliveryOrder_GetProductList();
                    DialogResult dialogResult = UiUtility.ShowPopupForm(fPrdList, this, true);

                    if (dialogResult == DialogResult.OK)
                    {
                        bool isDup = UiUtility.IsDuplicated(view, "PRODUCT_NO", fPrdList.PRODUCT_NO);
                        if (!isDup)
                        {
                            editor.EditValue = fPrdList.PRODUCT_NO;
                            this.GetProductByNo(view, fPrdList.PRODUCT_NO, false);
                        }
                        else
                        {
                            e.Cancel = true;
                            editor.Undo();
                            XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        }
                    }
                    else
                    {
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
                    }
                    else
                    {
                        this.GetProductByNo(view, editor.Text, false);
                    }
                }
            }

        }

        private void luePRODUCTION_TYPE_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Production Type Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void lueDestination_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Destination Can't be null", ErrorType.Critical);
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

        private void dtpDELIVERY_DATE_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            DateEdit editor = (DateEdit)sender;
            if (string.IsNullOrEmpty(editor.Text))
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Delivery Date can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void grvDODetail_rps_QTY_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdDODetail.Views[0];
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
                    int boxQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "QTY_PER_BOX"), NumberFormatInfo.CurrentInfo);

                    int noofbox = (int)Math.Ceiling((float)qty / (float)boxQty);


                    if (rowHandle != GridControl.NewItemRowHandle)
                    {
                        int deliveryQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "DELIVERY_QTY"), NumberFormatInfo.CurrentInfo);
                        if (qty < deliveryQty) //qty > freeQty || 
                        {
                            e.Cancel = true;

                            XtraMessageBox.Show(this, "Qty can't less than Delivery Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            view.FocusedRowHandle = rowHandle;
                            view.ShowEditor();

                            editor.Focus();
                            editor.SelectAll();
                        }
                        else
                        {
                            view.SetFocusedRowCellValue("NO_OF_BOX", noofbox);
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        view.SetFocusedRowCellValue("NO_OF_BOX", noofbox);
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

        }

        private void frmDeliveryOrder_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcDeliveryOrder.SelectedTabPage == this.xtpDeliveryOrderList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetDeliveryOrderDetail(this.txtDO_NO.Text);
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.RefreshDeliveryOrderList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult isDelete;

            if (this.FormState == eFormState.ReadOnly)
            {
                isDelete = XtraMessageBox.Show(this, "Do you want to delete this document?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (isDelete == DialogResult.Yes)
                {
                    this.DeleteDeliveryOrder(this.txtDO_NO.Text);
                }
            }
            else
            {

                try
                {
                    GridView view = this.grdDODetail.Views[0] as GridView;

                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                    int rowHandle = view.FocusedRowHandle;

                    int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                    
                    int deliveryQty = 0;
                    switch (flag)
                    {
                        case 1:
                            deliveryQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "DELIVERY_QTY"), NumberFormatInfo.CurrentInfo);
                            if (deliveryQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteDeliveryOrdDetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already Delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                            break;
                        case 2:
                            isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (isDelete == DialogResult.Yes)
                            {
                                this.DeleteDeliveryOrdDetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                            break;
                        case 3:
                            deliveryQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "DELIVERY_QTY"), NumberFormatInfo.CurrentInfo);
                            if (deliveryQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteDeliveryOrdDetail(view, rowHandle);

                                    SendKeys.Send("{F2}");
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Already Delivery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (frmAdvShippingOrder advShippingOrd = new frmAdvShippingOrder())
            //    {
            //        UiUtility.ShowPopupForm(advShippingOrd, this, true);

            //        this.AdvanceSearchShippingOrder(advShippingOrd.SO_NO, advShippingOrd.REF_NO, advShippingOrd.WH_ID, advShippingOrd.FROM_DATE, advShippingOrd.TO_DATE);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void frmDeliveryOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void grvDODetail_GotFocus(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            view.ShowEditor();
        }

        private void grvDODetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
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

        private void grvDODetail_rps_btePRODUCT_NO_KeyDown(object sender, KeyEventArgs e)
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

        private void grvDODetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //bool isAdjust = false;
            //BaseView baseView = sender as BaseView;

            //ColumnView colView = (ColumnView)this.grdDODetail.MainView;
            //int iCompare, iValue;
            //try
            //{
            //    if (e.RowHandle == colView.FocusedRowHandle)
            //    {
            //        if (e.RowHandle != GridControl.NewItemRowHandle)
            //        {
            //            switch (e.Column.FieldName)
            //            {
            //                case "ASSIGN_QTY":
            //                    iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "ASSIGN_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
            //                    iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
            //                    if (iCompare != iValue)
            //                    {
            //                        isAdjust = true;
            //                    }

            //                    break;
            //                case "PICKED_QTY":
            //                    iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
            //                    iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

            //                    if (iCompare != iValue)
            //                    {
            //                        isAdjust = true;
            //                    }
            //                    break;
            //                case "LOADED_QTY":
            //                    iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "LOADED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
            //                    iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

            //                    if (iValue > 0 && iValue != iCompare)
            //                    {
            //                        isAdjust = true;
            //                    }
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }

            //        if (isAdjust)
            //        {
            //            //Apply the appearance of the SelectedRow
            //            if (this.FormState != eFormState.ReadOnly)
            //            {
            //                e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
            //            }
            //            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
            //            //e.Appearance.Options.UseForeColor = true;
            //            e.Appearance.ForeColor = Color.Red;
            //            e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}
        }

        private void btnPrintDeliveryOrder_Click(object sender, EventArgs e)
        {
            this.PrintDeliveryOrder(this.txtDO_NO.Text);
        }

        
    }
}