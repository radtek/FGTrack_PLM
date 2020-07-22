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
using DevExpress.XtraReports;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Reports;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;
using System.Globalization;
using HTN.BITS.UIL.PLASESS.LOVForms;
using HTN.BITS.UIL.PLASESS.PopupForms;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.UIL.PLASESS.Query_Popup;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using HTN.BITS.UIL.PLASESS.Component.CSV;
namespace HTN.BITS.UIL.PLASESS.Utility
{
    public partial class frmQCTransfer_Order : BaseChildForm
    {
        public frmQCTransfer_Order()
        {
            InitializeComponent();

            this.CustomInitializeComponent();
            base.LoadFormLayout();

            base.LoadGridLayout(this.grdQCTransferOrder);
            this.chkSelect = new GridCheckMarksSelection(this.grvQCTransferOrder);
            this.chkSelect.ClearSelection();
        
            base.LoadGridLayout(this.grdQCTransferOrderDetail);
            this.prodSelect = new GridCheckMarksSelection(this.grvQCTransferOrderDetail);
            this.prodSelect.ClearSelection();
            this.grvQCTransferOrderDetail.Columns["CheckMarkSelection"].OptionsColumn.AllowFocus = false;

        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private DataTable dtbQCTransferOrderDtl;
        private List<ProductionType> lstProductionType;
        private List<TransferOrderDtl> deltodtl;
        private GridCheckMarksSelection chkSelect;
        private GridCheckMarksSelection prodSelect;
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
                return string.Format("QCTransfer_Order_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("QCTransfer_Order_Detail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName_Detail + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName_Detail + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName_Detail + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName_Detail + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName_Detail + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdQCTransferOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdQCTransferOrderDetail.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName_Detail + ".html", columnNoExp);
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
                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];

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
                        this.btnDel_TO.Enabled = false;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);
                        

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                       // this.dntQCReturn.Enabled = true;

                        this.dntQCReturn.TextStringFormat = "      Edit Mode      ";
                        this.dntQCReturn.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnDel_TO.Enabled = true;
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
                        if (this.xtpQCTransferOrderList.PageEnabled == true) this.dntQCReturn.Enabled = true;

                        this.btnPostData.Enabled = true;
                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnDel_TO.Enabled = false;
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
            this.luePRODUCTION_TYPE.Properties.ReadOnly = state;
            this.txtREF_NO.Properties.ReadOnly = state;
            this.lueWarehouse.Properties.ReadOnly = state;
            this.dtpPRODUCTION_DATE.Properties.ReadOnly = state;
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
            this.txtTO_NO.Text = string.Empty;
            this.txtTO_DATE.EditValue = DateTime.Now;
            this.luePRODUCTION_TYPE.EditValue = null;
            this.txtREF_NO.EditValue = null;
            this.dtpPRODUCTION_DATE.EditValue = DateTime.Now;
            this.lueWarehouse.EditValue = null;
            this.txtREMARK.Text = string.Empty;

            this.icbREC_STAT.EditValue = true;
            this.GetQCReturnDetail(string.Empty);
        }

        private void InitializaLOVData()
        {
            try
            {
                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                    this.lstProductionType = pdtBll.GetProductionTypeList();
                }

                this.luePRODUCTION_TYPE.Properties.DataSource = this.lstProductionType;
                this.lueSearchPROD_TYPE.Properties.DataSource = this.lstProductionType;

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    List<Warehouse> lstWH = qcReturnBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstWH;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetQCTransferOrderList(string findValue, string prodtypeID, DateTime? fromDate, DateTime? toDate)
        {
            List<TransferOrderList> lstTO_list = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (TransferOrderBLL TO_Bll = new TransferOrderBLL())
                {
                    lstTO_list = TO_Bll.GetQCTransferOrderList(findValue, prodtypeID, fromDate, toDate);
                }

                this.chkSelect.ClearSelection();

                this.grdQCTransferOrder.DataSource = lstTO_list;
                this.dntQCReturn.DataSource = lstTO_list;

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

        private bool GetProductByNo(GridView view, string prodNo, bool isCheck)
        {
            bool result = false;
            try
            {
                Product prod = null;
                using (TransferOrderBLL to_Bll = new TransferOrderBLL())
                {
                    prod = to_Bll.LovProductByNo(prodNo);
                }

                if (prod != null)
                {
                    if (!isCheck)
                    {
                        view.SetFocusedRowCellValue("LINE_NO", view.RowCount);
                        view.SetFocusedRowCellValue("PROD_SEQ_NO", prod.PROD_SEQ_NO);
                        view.SetFocusedRowCellValue("PRODUCT_NO", prod.PRODUCT_NO);
                        view.SetFocusedRowCellValue("PRODUCT_NAME", prod.PRODUCT_NAME);
                        view.SetFocusedRowCellValue("BOX_QTY", prod.BOX_QTY);
                        view.SetFocusedRowCellValue("UNIT_ID", prod.UNIT);
                        view.SetFocusedRowCellValue("DELIVERY_QTY", 0);
                        view.SetFocusedRowCellValue("STATUS", "GENERATE");
                       
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

        public void GetBindingQcTransferOrder(string returnNo)
        {
            TransferOrderHdr TOHdr = null;
            try
            {
                using (TransferOrderBLL TO_Bll = new TransferOrderBLL())
                {
                    TOHdr = TO_Bll.GetTransferOrder(returnNo);
                }

                if (TOHdr != null)
                {
                    this.txtTO_NO.Text = TOHdr.TO_NO;
                    this.txtTO_DATE.EditValue = TOHdr.TO_DATE;
                    this.luePRODUCTION_TYPE.EditValue = TOHdr.PROD_TYPE;
                    this.txtREF_NO.EditValue = TOHdr.REF_NO;
                    this.lueWarehouse.EditValue = TOHdr.TO_DEST;
                    this.dtpPRODUCTION_DATE.EditValue = TOHdr.DELIVERY_DATE;
                    this.txtREMARK.Text = TOHdr.REMARK;
                    this.icbREC_STAT.EditValue = TOHdr.REC_STAT;
                    this.txtPOST_REF.Text = TOHdr.POST_REF;


                    this.btnPostData.Enabled = this.CheckEnablePostCSV();

                    this.GetQCReturnDetail(TOHdr.TO_NO);
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
                using (TransferOrderBLL TO_Bll = new TransferOrderBLL())
                {
                    this.dtbQCTransferOrderDtl = TO_Bll.GetQCTODetail(returnNo);
                }

                if (this.dtbQCTransferOrderDtl != null)
                {
                    //this.dtbQCTransferOrderDtl.Columns["STATUS"].MaxLength = 10;
                    this.grdQCTransferOrderDetail.DataSource = this.dtbQCTransferOrderDtl;
                        int i = dtbQCTransferOrderDtl.Columns["STATUS"].MaxLength;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool IsFormValidated()
        {
            
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
            TransferOrderHdr Tohdr = new TransferOrderHdr();

            try
            {
                #region "QC Return Header"

                Tohdr.TO_NO = string.Empty;
                Tohdr.TO_DATE = DateTime.Now;
                Tohdr.REF_NO = this.txtREF_NO.Text;
                Tohdr.PROD_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                Tohdr.TO_DEST = this.lueWarehouse.EditValue.ToString();
                Tohdr.DELIVERY_DATE = (DateTime)this.dtpPRODUCTION_DATE.EditValue;
                Tohdr.REMARK = this.txtREMARK.Text;
                Tohdr.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (TransferOrderBLL to_Bll = new TransferOrderBLL())
                {
                    result = to_Bll.InsertTransferOrder(ref Tohdr, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                    this.btnEditTO_Detial.Enabled = true;
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
                this.txtTO_NO.Text = Tohdr.TO_NO;

                this.FormState = eFormState.ReadOnly;
                string findall = null;
                if (this.txtSearch.EditValue != null) findall = this.txtSearch.EditValue.ToString();
                this.GetQCTransferOrderList(findall, this.luePRODUCTION_TYPE.EditValue.ToString(), null, null);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdQCTransferOrder.Views[0];

                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "TO_NO", Tohdr.TO_NO);
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

        public void UpdateTO_HDR()
        {
            string result = string.Empty;
            TransferOrderHdr to_hdr = new TransferOrderHdr();

            try
            {
                #region "QC Return Header"

                 to_hdr.TO_NO = this.txtTO_NO.Text;
                 to_hdr.TO_DATE = DateTime.Now;
                 to_hdr.REF_NO = this.txtREF_NO.Text;
                 to_hdr.PROD_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                 to_hdr.TO_DEST = this.lueWarehouse.EditValue.ToString();
                 to_hdr.DELIVERY_DATE = (DateTime)this.dtpPRODUCTION_DATE.EditValue;
                 to_hdr.REMARK = this.txtREMARK.Text;
                 to_hdr.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                #endregion

                using (TransferOrderBLL to_Bll = new TransferOrderBLL())
                {
                    result = to_Bll.UpdateTransferOrder(to_hdr, ((frmMainMenu)this.ParentForm).UserID);
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
                string findall = null;
                if (this.txtSearch.EditValue != null) findall = this.txtSearch.EditValue.ToString();
                this.GetQCTransferOrderList(findall,this.luePRODUCTION_TYPE.EditValue.ToString(), null, null);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdQCTransferOrder.Views[0];
                    viewList.ClearSorting();

                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "TO_NO", to_hdr.TO_NO);
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

        public void UpdateTO_DTL()
        {
            string result = string.Empty;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                List<TransferOrderDtl> lstto_dtl = new List<TransferOrderDtl>();
                TransferOrderDtl todtl;

                ////Check Delete Recore
                if (this.deltodtl != null)
                {
                    foreach (TransferOrderDtl deldtl in this.deltodtl)
                    {
                        lstto_dtl.Add(deldtl);
                    }
                }


                int flag = 0;

                foreach (DataRow dr in this.dtbQCTransferOrderDtl.Rows)
                {
                    flag = Convert.ToInt32(dr["FLAG"].ToString());
                    //if (flag == 2) //new record
                    //{
                        todtl = new TransferOrderDtl();

                        todtl.TO_NO = dr["TO_NO"].ToString();
                        todtl.LINE_NO = dr["LINE_NO"].ToString();
                        todtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                        todtl.UNIT_ID = dr["UNIT_ID"].ToString();
                        todtl.REMARK = dr["REMARK"].ToString(); 
                        todtl.QTY = Convert.ToInt32(dr["QTY"], NumberFormatInfo.InvariantInfo);
                        todtl.FLAG = flag; //add new only
                        lstto_dtl.Add(todtl);
                   // }
                }



                if (lstto_dtl.Count != 0)
                {
                    using (TransferOrderBLL toBll = new TransferOrderBLL())
                    {
                        result = toBll.UpdateTransferOrderDtl(lstto_dtl, ((frmMainMenu)this.ParentForm).UserID);
                    }

                    if (!result.Equals("OK"))
                    {
                        XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        NotifierResult.Show("Update Transfer Order Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void Delete_TO(string tono)
        {
            string result = string.Empty;
            try
            {
                this.Cursor = Cursors.WaitCursor;
               

                List<TransferOrderDtl> lstto_dtl = new List<TransferOrderDtl>();
                TransferOrderDtl todtl;

              


                int flag = 0;

                foreach (DataRow dr in this.dtbQCTransferOrderDtl.Rows)
                {
                    flag = Convert.ToInt32(dr["FLAG"].ToString());
                    //if (flag == 2) //new record
                    //{
                        todtl = new TransferOrderDtl();

                        todtl.TO_NO = dr["TO_NO"].ToString();
                        todtl.PROD_SEQ_NO = dr["PROD_SEQ_NO"].ToString();
                        todtl.FLAG = flag; //add new only
                        lstto_dtl.Add(todtl);
                   // }
                }



                if (lstto_dtl.Count != 0)
                {
                    using (TransferOrderBLL toBll = new TransferOrderBLL())
                    {
                        result = toBll.UpdateTransferOrderDtl(lstto_dtl, ((frmMainMenu)this.ParentForm).UserID);
                    }

                    if (!result.Equals("OK"))
                    {

                        XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        using (TransferOrderBLL toBll = new TransferOrderBLL())
                        {
                            //result = toBll.UpdateTransferOrderDtl(lstto_dtl, ((frmMainMenu)this.ParentForm).UserID);
                            result = toBll.DeleteTransferOrder(tono, ((frmMainMenu)this.ParentForm).UserID);
                        }
                       
                    }
                    if (!result.Equals("OK"))
                    {

                        XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        NotifierResult.Show("Delete Transfer Order Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                        this.GetQCTransferOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);
                        this.FormState = eFormState.ReadOnly;
                        this.xtpQCTransferOrderDetail.PageVisible = false;
                        this.xtpQCTransferOrderList.PageEnabled = true;
                        this.xtcQCTransferOrder.SelectedTabPage = this.xtpQCTransferOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;
                        //this.Close();
                    }
                }
               
                    

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PrintTO(List<string> lstTO)
        {
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (TransferOrderBLL tobll = new TransferOrderBLL())
                {
                    ds = tobll.PrintTO(lstTO, ((frmMainMenu)this.ParentForm).UserID);
                }
                
                ReportViewer viewer = new ReportViewer() { AutoCloseAfterPrint = true };
                RPT_TRANSFER_ORDER rpt = new RPT_TRANSFER_ORDER();
                RPT_TRANSFER_ORDER rpt2 = new RPT_TRANSFER_ORDER();
                
                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
            //    rpt.Watermark.Text = "";
                rpt.CreateDocument();

                rpt2.DataSource = ds;
                rpt2.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;
                rpt2.CreateDocument();
                for (int i = 0; i < rpt2.Pages.Count; i++)
                {
                    rpt2.Pages[i].AssignWatermark(CreateTextWatermark("COPY"));
                }
               // rpt.Pages.AddRange(rpt2.Pages);
                int minPageCount = Math.Min(rpt.Pages.Count, rpt2.Pages.Count);
                for (int i = 0; i < minPageCount; i++)
                {
                    rpt.Pages.Insert(i * 2 + 1, rpt2.Pages[i]);
                }
                if (rpt2.Pages.Count != minPageCount)
                {
                    for (int i = minPageCount; i < rpt2.Pages.Count; i++)
                    {
                        rpt.Pages.Add(rpt2.Pages[i]);
                    }
                }
                viewer.SetReport(rpt);
                base.FinishedProcessing();
                //viewer.ShowDialog();
                viewer.Show();
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
        // Create a watermark with the specified text.
        private Watermark CreateTextWatermark(string text)
        {
            Watermark textWatermark = new Watermark();

            textWatermark.Text = text;
            textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
            textWatermark.Font = new Font("Tahoma", 108);
            textWatermark.ForeColor = Color.Red;
            textWatermark.TextTransparency = 150;
            textWatermark.ShowBehind = false;

            return textWatermark;
        }

        private void DeleteTODetail(GridView view, int rowSelect)
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
                TransferOrderDtl tODtl;
                int flag = 0;
                if (this.deltodtl == null)
                {
                    this.deltodtl = new List<TransferOrderDtl>();
                }

                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        flag = Convert.ToInt32(row["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 1 || flag == 3)
                        {
                            tODtl = new TransferOrderDtl();
                            tODtl.TO_NO = row["TO_NO"].ToString();
                            tODtl.PROD_SEQ_NO = row["PROD_SEQ_NO"].ToString();
                            tODtl.FLAG = 0;

                            this.deltodtl.Add(tODtl);
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
                this.dtbQCTransferOrderDtl.AcceptChanges();
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ShowEditor();
            }
        }

        private bool CheckEnablePostCSV()
        {
            if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PostTransferOrderToCSV(string toNo)
        {
            string filename = string.Empty;
            try
            {
                DataTable dtb;

                using (TransferOrderBLL toOrdBll = new TransferOrderBLL())
                {
                    dtb = toOrdBll.GetCompletedTO(toNo, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (dtb != null)
                {

                    using (SaveFileDialog fdlg = new SaveFileDialog { Title = "Save Export CSV File", Filter = "CSV files (*.csv)|*.csv", FilterIndex = 1, RestoreDirectory = true, FileName = string.Format(@"{0}.CSV", toNo) })
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

                        using (TransferOrderBLL toOrdBll = new TransferOrderBLL())
                        {
                            toOrdBll.UpdatePostTO(toNo, filename, ((frmMainMenu)this.ParentForm).UserID);
                        }

                        this.txtPOST_REF.EditValue = filename;
                        this.btnPostData.Enabled = false;

                        GridView viewList = (GridView)this.grdQCTransferOrder.MainView;
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

        #endregion

        private void frmQCTransfer_Order_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpQCTransferOrderDetail.PageVisible = false;
            //this.InitializaLOVData();
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;

            //this.GetQCTransferOrderList(string.Empty,string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            //this.FormState = eFormState.ReadOnly;

        }

        private void frmQCTransfer_Order_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtpQCTransferOrderDetail.PageVisible = false;
            this.InitializaLOVData();
            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;

            this.GetQCTransferOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            this.FormState = eFormState.ReadOnly;
        }

        private void grvQCReturn_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow && info.InRowCell)
                {
                    string returnNo = view.GetRowCellValue(info.RowHandle, "TO_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpQCTransferOrderList.PageEnabled = false;

                    this.xtpQCTransferOrderDetail.PageVisible = true;
                    this.xtcQCTransferOrder.SelectedTabPage = this.xtpQCTransferOrderDetail;
                    this.dntQCReturn.Enabled = false;

                    //Call record detail.
                    this.GetBindingQcTransferOrder(returnNo);
                    UiUtility.SetGridReadOnly(this.grvQCTransferOrderDetail, true);
                    this.btnEditTO_Detial.Enabled = true;
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
                GridView view = (GridView)this.grdQCTransferOrder.Views[0]; //this.gridView2
                
                if (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderDetail)
                {
                    string returnNo = view.GetFocusedRowCellValue("TO_NO").ToString();

                    this.GetBindingQcTransferOrder(returnNo);
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
           // this.lueWarehouse.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtpQCTransferOrderDetail.PageVisible = false;
                        this.xtpQCTransferOrderList.PageEnabled = true;
                        this.xtcQCTransferOrder.SelectedTabPage = this.xtpQCTransferOrderList;
                        this.dntQCReturn.Enabled = true;
                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                       // this.GetBindingQcReturn(this.txtRT_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpQCTransferOrderDetail.PageVisible = false;
                        this.xtpQCTransferOrderList.PageEnabled = true;
                        this.xtcQCTransferOrder.SelectedTabPage = this.xtpQCTransferOrderList;
                        this.dntQCReturn.Enabled = true;
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
                    this.UpdateTO_HDR();
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

            this.xtpQCTransferOrderList.PageEnabled = false;
            this.xtpQCTransferOrderDetail.PageVisible = true;
            this.xtcQCTransferOrder.SelectedTabPage = this.xtpQCTransferOrderDetail;
            this.dntQCReturn.Enabled = false;
            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;
            this.btnEditTO_Detial.Enabled = false;

            this.luePRODUCTION_TYPE.Focus();
            this.luePRODUCTION_TYPE.EditValue = "P";
            this.txtTO_NO.Text = "(AUTO)";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueWarehouse_Validating(object sender, CancelEventArgs e)
        {

        }

        private void frmQCTransfer_Order_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void frmQCTransfer_Order_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetBindingQcTransferOrder(this.txtTO_NO.Text);
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
            string protypeID = string.Empty;
            if (this.lueSearchPROD_TYPE.EditValue != null)
            {
                protypeID = this.lueSearchPROD_TYPE.EditValue.ToString();
            }
            string findall = null ;
            if (this.txtSearch.EditValue != null) findall = this.txtSearch.EditValue.ToString();
            this.GetQCTransferOrderList(findall,protypeID, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void bbiPrintFGReturnOrd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    try
        //    {

        //        //XtraMessageBox.Show("Print Loading Order");
        //        List<QCReturnHdr> lstQcReturn;
        //        GridView view = (GridView)this.grdQCTransferOrder.Views[0];

        //        if (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderList)
        //        {
        //            if (this.chkSelect.SelectedCount > 0)
        //            {
        //                lstQcReturn = new List<QCReturnHdr>(this.chkSelect.SelectedCount);
        //                for (int i = 0; i < this.chkSelect.SelectedCount; i++)
        //                {
        //                    lstQcReturn.Add((QCReturnHdr)this.chkSelect.GetSelectedRow(i));
        //                }

        //                this.PrintQCReturnOrder(lstQcReturn);

        //            }
        //            else
        //            {
        //                //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                XtraMessageBox.Show(this, "PLEASE SELECT RETURN ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //            }
        //        }
        //        else
        //        {
        //            //lstQcReturn = new List<QCReturnHdr>(1);
        //            ////this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
        //            //QCReturnHdr qcReturn = new QCReturnHdr { RT_NO = this.txtRT_NO.Text };

        //            //lstQcReturn.Add(qcReturn);

        //            //this.PrintQCReturnOrder(lstQcReturn);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        }

        private void bbiPrintFGReturnDtl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    try
        //    {

        //        //XtraMessageBox.Show("Print Loading Order");
        //        List<QCReturnHdr> lstQcReturn;
        //        GridView view = (GridView)this.grdQCTransferOrder.Views[0];

        //        if (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderList)
        //        {
        //            if (this.chkSelect.SelectedCount > 0)
        //            {
        //                lstQcReturn = new List<QCReturnHdr>(this.chkSelect.SelectedCount);
        //                for (int i = 0; i < this.chkSelect.SelectedCount; i++)
        //                {
        //                    lstQcReturn.Add((QCReturnHdr)this.chkSelect.GetSelectedRow(i));
        //                }

        //                this.PrintQCReturnOrderDetail(lstQcReturn);

        //            }
        //            else
        //            {
        //                XtraMessageBox.Show(this, "PLEASE SELECT RETURN ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //            }
        //        }
        //        else
        //        {
        //            //lstQcReturn = new List<QCReturnHdr>(1);
                  
        //            //QCReturnHdr qcReturn = new QCReturnHdr { RT_NO = this.txtRT_NO.Text };

        //            //lstQcReturn.Add(qcReturn);

        //            //this.PrintQCReturnOrderDetail(lstQcReturn);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
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

            string prdtype = string.Empty;
            if (this.lueSearchPROD_TYPE.EditValue != null)
            {
                prdtype = this.lueSearchPROD_TYPE.EditValue.ToString();
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetQCTransferOrderList(editor.Text, prdtype, null, null);
                   // this.txtSearch.Text = string.Empty;
                    break;
                default:
                    break;
            }
        }
  

        private void btnEditTO_Detial_Click(object sender, EventArgs e)
        {
            this.btnEditTO_Detial.Enabled = false;
            this.btnDelete.Enabled = true;
            this.btnSaveTo_Detail.Enabled = true;
            this.btn02Cancel.Enabled = true;

            this.ddbPrint.Enabled = false;

            this.FormState = eFormState.Edit;
           
            try
            {

                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];

                
                UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                UiUtility.SetGridReadOnly(view, false);
                
               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSaveTo_Detail_Click(object sender, EventArgs e)
        {
            try
            {

                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];
                UiUtility.SetGridReadOnly(view, true);


                this.UpdateTO_DTL();

                //call job lot planning
                this.GetBindingQcTransferOrder(this.txtTO_NO.Text);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (this.deltodtl != null)
                {
                    this.deltodtl.Clear();
                    this.deltodtl = null;
                }

                this.btnEditTO_Detial.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnSaveTo_Detail.Enabled = false;
                this.btn02Cancel.Enabled = false;

                this.ddbPrint.Enabled = true;
                this.FormState = eFormState.ReadOnly;
            }
        }

        private void grvQCTransferOrderDetail_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                view.SetFocusedRowCellValue("TO_NO", this.txtTO_NO.Text);
                int maxRow = view.RowCount;
                view.SetFocusedRowCellValue("LINE_NO", maxRow);
                view.SetFocusedRowCellValue("QTY_DELIVERY", 0);
                view.SetFocusedRowCellValue("NO_OF_BOX", 0);
                view.SetFocusedRowCellValue("QTY", 0);
              // view.SetFocusedRowCellValue("STATUS", "PROCESS");
                view.SetFocusedRowCellValue("FLAG", 2);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InitNewRo : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvQCTransferOrderDetail_GotFocus(object sender, EventArgs e)
        {
            try
            {

                GridView view = (GridView)sender;

                view.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvTO_Detail_rps_txtQTY_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0]; //this.gridView2
                TextEdit editor = (TextEdit)sender;

                if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    decimal devQTY = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "QTY_DELIVERY"), NumberFormatInfo.InvariantInfo);
                    decimal QTY = Convert.ToInt32(editor.Text, NumberFormatInfo.InvariantInfo);
                    decimal boxqty = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "BOX_QTY"), NumberFormatInfo.InvariantInfo);
                    decimal no_box = 0;
                    if (QTY < devQTY)
                    {
                        XtraMessageBox.Show(this, "QTY canot less than Delivery Qty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        e.Cancel = true; 
                    }
                    else no_box = Convert.ToInt32(Math.Ceiling(QTY / boxqty));

                            view.SetFocusedRowCellValue("NO_OF_BOX", no_box);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvTO_Detail_rps_btePRODUCT_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];

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
                    fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                    fPrdList.FormCalling = eFormCalling.fTransferOrder;
                    fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                    fPrdList.PARTY_ID = string.Empty;

                    fPrdList.TransferOrder_GetProductList();
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

        private void btn02Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.deltodtl != null)
                {
                    this.deltodtl.Clear();
                    this.deltodtl = null;
                }

                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];
                UiUtility.SetGridReadOnly(view, true);

                //call job lot planning
                this.GetBindingQcTransferOrder(this.txtTO_NO.Text);
               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (this.deltodtl != null)
                {
                    this.deltodtl.Clear();
                    this.deltodtl = null;
                }

                this.btnEditTO_Detial.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnSaveTo_Detail.Enabled = false;
                this.btn02Cancel.Enabled = false;

                this.ddbPrint.Enabled = true;
                this.FormState = eFormState.ReadOnly;
            }
        }

       

        private void btnPrintTransferOrder_Click(object sender, EventArgs e)
        {
            try
            {

                //XtraMessageBox.Show("Print Loading Order");
                List<string> lsttohdr;
                GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];

                if (this.xtcQCTransferOrder.SelectedTabPage == this.xtpQCTransferOrderList)
                {
                    if (this.chkSelect.SelectedCount > 0)
                    {

                        
                        lsttohdr = new List<string>();
                       
                        for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                        {
                           
                            lsttohdr.Add((this.chkSelect.GetSelectedRow(i) as TransferOrderList).TO_NO);
                      
                        }

                        this.PrintTO(lsttohdr);
                        this.chkSelect.ClearSelection();

                    }
                    else
                    {
                        //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        XtraMessageBox.Show(this, "PLEASE SELECT TRANSFER ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    lsttohdr = new List<string>(1);

                    lsttohdr.Add(this.txtTO_NO.Text);

                    this.PrintTO(lsttohdr);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];
            TransferOrderDtl todtl = null;
            try
            {
                if (this.prodSelect.SelectedCount > 0)
                {
                    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete Product?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (isDelete == DialogResult.Yes)
                    {
                        view.BeginSort();

                        if (this.deltodtl == null)
                        {
                            this.deltodtl = new List<TransferOrderDtl>();
                        }

                        for (int i = 0; i < this.prodSelect.SelectedCount; i++)
                        {
                            int qty_delivery = 0;
                            DataRowView row = (DataRowView)this.prodSelect.GetSelectedRow(i);
                            if (row.Row != null)
                            {
                                todtl = new TransferOrderDtl();
                                todtl.TO_NO = row.Row["TO_NO"].ToString();
                                todtl.PROD_SEQ_NO = row.Row["PROD_SEQ_NO"].ToString();
                                todtl.FLAG = 0;

                                qty_delivery = Convert.ToInt32(row.Row["QTY_DELIVERY"], NumberFormatInfo.InvariantInfo);

                                if (qty_delivery == 0) this.deltodtl.Add(todtl);
                            }

                            if (qty_delivery == 0) row.Delete();
                        }

                        view.EndSort();
                        this.dtbQCTransferOrderDtl.AcceptChanges();
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "PLEASE SELECT RECORD TO DELETE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally {
                this.prodSelect.ClearSelection();

            }
        }

  

        private void grvQCTransferOrderDetail_RowUpdated(object sender, RowObjectEventArgs e)
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

        private void grvQCTransferOrderDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {

        }

        private void grvQCTransferOrderDetail_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            GridView view = (GridView)sender;
        }

        private void grvQCTransferOrderDetail_KeyDown(object sender, KeyEventArgs e)
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
                    int dQty = 0;
                    switch (flag)
                    {
                        case 1:
                            dQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "QTY_DELIVERY"), NumberFormatInfo.CurrentInfo);
                            if (dQty == 0)
                            {
                                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (isDelete == DialogResult.Yes)
                                {
                                    this.DeleteTODetail(view, rowHandle);

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
                                this.DeleteTODetail(view, rowHandle);

                                SendKeys.Send("{F2}");
                            }
                           break;
                        case 3:
                           dQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "QTY_DELIVERY"), NumberFormatInfo.CurrentInfo);
                           if (dQty == 0)
                           {
                               isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                               if (isDelete == DialogResult.Yes)
                               {
                                   this.DeleteTODetail(view, rowHandle);

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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDel_TO_Click(object sender, EventArgs e)
        {
            // check detail all must 0
            //delete detail 
            GridView view = (GridView)this.grdQCTransferOrderDetail.Views[0];
            bool candel = true;
            try
            {
                DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete Transfer Order?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (isDelete == DialogResult.Yes)
                    {
                       // view.BeginSort();

                        

                        for (int i = 0; i < view.RowCount ; i++)
                        {
                            int qty_delivery = Convert.ToInt32(view.GetRowCellValue(i,"QTY_DELIVERY"));



                            if (qty_delivery > 0) candel = false;
                        }

                        if (candel)
                        {
                            for (int i = 0; i < view.RowCount; i++)
                            {
                               
                                view.SetRowCellValue(i, "FLAG", 0);
                            }

                            
                            this.Delete_TO(this.txtTO_NO.Text);
                        }
                        else XtraMessageBox.Show(this, "CANNOT DELETE THIS TRANSFER ORDER, ALL DELEIVERY QTY MUST = 0 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                      
                    }

                   
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
               

            }
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
                {
                    this.PostTransferOrderToCSV(this.txtTO_NO.Text);
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

    }
}