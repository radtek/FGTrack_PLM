using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.UIL.PLASESS.PopupForms;
using HTN.BITS.UIL.PLASESS.LOVForms;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using System.Globalization;
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.UIL.PLASESS.Reports;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.UIL.PLASESS.Component.CSV;
using HTN.BITS.UIL.PLASESS.ConfirmForms;
using HTN.BITS.UIL.PLASESS.Query;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmArrival : BaseChildForm
    {
        public frmArrival()
        {
            InitializeComponent();

            this.CustomInitializeComponent();

            base.LoadGridLayout(this.grdLotPlaning);
            this.lotSelect = new GridCheckMarksSelection(this.grvLotPlaning);
            this.lotSelect.ClearSelection();
            this.grvLotPlaning.Columns["CheckMarkSelection"].OptionsColumn.AllowFocus = false;

        }

        #region "Variable Member"

        private GridCheckMarksSelection lotSelect;
        //private GridExportController gridController;

        private eFormState formState = eFormState.ReadOnly;

        private List<Machine> lstMachine;

        private List<ProductionType> lstProductionType;

        private DataTable dtbLotPlaning;

        private GridCheckMarksSelection chkSelect;

        private DateTime? ShiftDateSelect;

        // add by praew

        private DateTime? LotDateSelect;

        private List<T_ARRIVAL_HDR> lstArrivalHdr = new List<T_ARRIVAL_HDR>();
        private List<T_ARRIVAL_DTL> lstArrivalDtl = new List<T_ARRIVAL_DTL>();

        private List<Material> lstMaterial = null;

        private Material objMaterial;

        DataTable dtbArrival;

        private bool IsLabelGenerated;

        private List<M_ARRIVAL_TYPE> lstArrival_type;

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

        public string MachineID(string mcName)
        {
            if (this.lstMachine == null)
                return string.Empty;

            Machine mcTemp = lstMachine.Find(delegate(Machine _mc)
            {
                return _mc.MACHINE_NAME == mcName;
            });

            if (mcTemp != null)
            {
                return mcTemp.MC_NO;
            }
            else
            {
                return string.Empty;
            }
        }

        public string MachineName(string mcNo)
        {
            if (this.lstMachine == null)
                return string.Empty;

            Machine mcTemp = lstMachine.Find(delegate(Machine _mc)
            {
                return _mc.MC_NO == mcNo;
            });

            if (mcTemp != null)
            {
                return mcTemp.MACHINE_NAME;
            }
            else
            {
                return string.Empty;
            }
        }

        public string ProductionTypeName(string prdtypeNo)
        {
            if (this.lstProductionType == null)
                return string.Empty;

            ProductionType pdtType = lstProductionType.Find(delegate(ProductionType _pdtType)
            {
                return _pdtType.SEQ_NO == prdtypeNo;
            });

            if (pdtType != null)
            {
                return pdtType.NAME;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region "Button Export Referance"

        private string FileName
        {
            get
            {
                return string.Format("ArrivalList_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("ArrivalDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName_Detail + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName_Detail + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName_Detail + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName_Detail + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName_Detail + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                //columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["LABEL"] != null ? "LABEL" : string.Empty);

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
            this.bbiPrintJobOrder.Glyph = base.Language.GetBitmap("PrintOrder");
            this.bbiPrintProductCard.Glyph = base.Language.GetBitmap("PrintCard");

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
                GridView view = (GridView)this.grdLotPlaning.MainView;

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.lueWarehouse.Properties.ReadOnly = false;
                        this.lueWarehouse.Properties.Buttons[0].Enabled = true;

                        
                        this.btePARTY_ID.Properties.Buttons[0].Enabled = true;

                        this.bteDOC_NO.Properties.ReadOnly = false;
                        this.bteDOC_NO.Properties.Buttons[0].Enabled = true;

                        this.dntJobOrder.Enabled = true;

                        this.dntJobOrder.TextStringFormat = "      Add Mode      ";
                        this.dntJobOrder.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnUnLockPost.Visible = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnExit.Enabled = false;

                        //this.btnEdit.Enabled = false;
                        this.ddbPrint.Enabled = false;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        view.Columns["MTL_CODE"].OptionsColumn.ReadOnly = false;
                        view.Columns["MTL_CODE"].OptionsColumn.AllowEdit = true;

                        view.Columns["LOT_DATE"].OptionsColumn.ReadOnly = false;
                        view.Columns["LOT_DATE"].OptionsColumn.AllowEdit = true;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        view.Columns["REMARK"].OptionsColumn.ReadOnly = false;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = true;


                        this.grvLotPlaning_rps_bte_label.Buttons[0].Enabled = false;

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.lueWarehouse.Properties.ReadOnly = true;
                        this.lueWarehouse.Properties.Buttons[0].Enabled = false;

                        this.btePARTY_ID.Properties.ReadOnly = true;
                        this.btePARTY_ID.Properties.Buttons[0].Enabled = false;

                        this.bteDOC_NO.Properties.ReadOnly = false;
                        this.bteDOC_NO.Properties.Buttons[0].Enabled = true;

                        this.dntJobOrder.Enabled = true;

                        this.dntJobOrder.TextStringFormat = "      Edit Mode      ";
                        this.dntJobOrder.Enabled = false;

                        this.btnPostData.Enabled = false;
                        this.btnUnLockPost.Visible = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnExit.Enabled = false;

                        this.btnDelete.Enabled = true;

                        //this.btnEdit.Enabled = false;
                        this.ddbPrint.Enabled = false;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        view.Columns["MTL_CODE"].OptionsColumn.ReadOnly = false;
                        view.Columns["MTL_CODE"].OptionsColumn.AllowEdit = true;

                        view.Columns["LOT_DATE"].OptionsColumn.ReadOnly = false;
                        view.Columns["LOT_DATE"].OptionsColumn.AllowEdit = true;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = true;

                        view.Columns["REMARK"].OptionsColumn.ReadOnly = false;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = true;

                        view.Columns["LABEL"].OptionsColumn.ReadOnly = true;
                        view.Columns["LABEL"].OptionsColumn.AllowEdit = false;
                        view.Columns["LABEL"].OptionsColumn.AllowFocus = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridReadOnly(view, false);

                        this.grvLotPlaning_rps_bte_label.Buttons[0].Enabled = false;

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.lueWarehouse.Properties.ReadOnly = true;
                        this.lueWarehouse.Properties.Buttons[0].Enabled = false;

                        this.btePARTY_ID.Properties.ReadOnly = true;
                        this.btePARTY_ID.Properties.Buttons[0].Enabled = false;

                        this.bteDOC_NO.Properties.ReadOnly = true;
                        this.bteDOC_NO.Properties.Buttons[0].Enabled = false;

                        this.dntJobOrder.TextStringFormat = " Record {0} of {1} ";
                        this.dntJobOrder.Enabled = true;

                        //this.btnPostData.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";
                        this.btnExit.Enabled = true;

                        this.btnCancel.Enabled = true;
                        this.ddbPrint.Enabled = true;

                        this.btnDelete.Enabled = false;

                        
                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);
                        //UiUtility.SetGridReadOnly(view, true);
                        UiUtility.SetGridEditOnly(view, false, "LABEL");

                        view.Columns["MTL_CODE"].OptionsColumn.ReadOnly = true;
                        view.Columns["MTL_CODE"].OptionsColumn.AllowEdit = false;

                        //view.Columns["MTL_NAME"].OptionsColumn.ReadOnly = true;
                        //view.Columns["MTL_NAME"].OptionsColumn.AllowEdit = false;

                        //view.Columns["UNIT_ID"].OptionsColumn.ReadOnly = true;
                        //view.Columns["UNIT_ID"].OptionsColumn.AllowEdit = false;

                        view.Columns["LOT_DATE"].OptionsColumn.ReadOnly = true;
                        view.Columns["LOT_DATE"].OptionsColumn.AllowEdit = false;

                        view.Columns["QTY"].OptionsColumn.ReadOnly = true;
                        view.Columns["QTY"].OptionsColumn.AllowEdit = false;

                        view.Columns["REC_QTY"].OptionsColumn.ReadOnly = true;
                        view.Columns["REC_QTY"].OptionsColumn.AllowEdit = false;

                        //view.Columns["NO_OF_LABEL"].OptionsColumn.ReadOnly = true;
                        //view.Columns["NO_OF_LABEL"].OptionsColumn.AllowEdit = false;

                        view.Columns["REMARK"].OptionsColumn.ReadOnly = true;
                        view.Columns["REMARK"].OptionsColumn.AllowEdit = false;

                        view.Columns["STATUS"].OptionsColumn.ReadOnly = true;
                        view.Columns["STATUS"].OptionsColumn.AllowEdit = false;

                        this.GridDetail_OptionsCustomization(view, true);

                        this.grvLotPlaning_rps_bte_label.Buttons[0].Enabled = true;

                        break;
                    default:
                        break;
                }

                
                //this.btnSave.Enabled = false;
                //this.btnCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeControlState(bool state)
        {
           
            //this.btePARTY_ID.Properties.ReadOnly = state;
            //this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;

            //this.txtPARTY_NAME.Properties.ReadOnly = state;
            this.txtREF_NO.Properties.ReadOnly = state;

            this.dteREF_DATE.Properties.ReadOnly = state;
            this.dteREF_DATE.Properties.Buttons[0].Enabled = !state;

            //this.lueWarehouse.Properties.ReadOnly = state;
            //this.lueWarehouse.Properties.Buttons[0].Enabled = !state;
            this.btnGenerate.Enabled = state;

            this.txtREMARK.Properties.ReadOnly = state;

            this.lueARR_TYPE.Properties.ReadOnly = state;
            this.lueARR_TYPE.Properties.Buttons[0].Enabled = !state;

            //this.txtARRIVAL_NO.Properties.ReadOnly = state;

            this.dteARRIVAL_DATE.Properties.ReadOnly = state;
            this.dteARRIVAL_DATE.Properties.Buttons[0].Enabled = !state;

            this.icbREC_STAT.Properties.ReadOnly = state;
            this.icbREC_STAT.Properties.Buttons[0].Enabled = !state;
            
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
            try
            {

                this.btePARTY_ID.EditValue = null;
                this.bteDOC_NO.EditValue = null;
                this.txtPARTY_NAME.Text = string.Empty;
                this.txtREF_NO.Text = string.Empty;
                this.dteREF_DATE.EditValue=null;
                this.lueWarehouse.EditValue=null;
                this.txtREMARK.Text = string.Empty;
                this.lueARR_TYPE.EditValue="N";
                this.txtARRIVAL_NO.Text = string.Empty;
                this.dteARRIVAL_DATE.EditValue=null;
                this.icbREC_STAT.EditValue = true;

                //this.GetJobLotPlanning(string.Empty);

                //clear Gridview
                this.GetArrivalDtl("xxxx");
                //this.grdLotPlaning.DataSource = this.dtbArrival;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void InitializaLOVData()
        {

            try
            {

                //using (MachineBLL mcBll = new MachineBLL())
                //{
                //    this.lstMachine = mcBll.GetMachineList(string.Empty);
                //}

                //using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                //{
                //    this.lstProductionType = pdtBll.GetProductionTypeList();
                //}

                //using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                //{
                //    List<Warehouse> lstWH = shipOrdBll.GetWarehouse();
                //    if (lstWH != null)
                //    {
                //        this.lueWarehouse.Properties.DataSource = lstWH;
                //        //this.lueSearchWH.Properties.DataSource = lstWH;
                //    }
                //}

                using (ArrivalBLL ArrivalBll = new ArrivalBLL())
                {
                    this.lstArrival_type = ArrivalBll.GetArrivalTypeList();
                    if (lstArrival_type != null)
                    {
                        this.lueARR_TYPE.Properties.DataSource = lstArrival_type;
                        this.grvJobOrder_rps_ARRIVAL_TYPE.DataSource = lstArrival_type;
                    }
                }
                using (MaterialBLL mtlBll = new MaterialBLL())
                {
                    List<Location> lstLoc = mtlBll.GetLocationList();
                    if (lstLoc != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstLoc;
                        this.lueSearchWH.Properties.DataSource = lstLoc;
                        this.grvJobOrder_rps_WH.DataSource = lstLoc;
                    }
                }
                //this.grvJobOrder_rps_MACHINE.DataSource = this.lstMachine;
                //this.grvJobOrder_rps_PRODUCTION_TYPE.DataSource = this.lstProductionType;
                //this.luePRODUCTION_TYPE.Properties.DataSource = this.lstProductionType;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                        this.bteDOC_NO.Focus();
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

        private bool GetMTLByNo(GridView view, string partyid, string mtl_seq_no ,bool isCheck)
        {
            bool result = false;
            try
            {
                using (ArrivalBLL shipOrdBll = new ArrivalBLL())
                {
                     this.objMaterial = shipOrdBll.GetMaterialCode(mtl_seq_no, partyid);
                     if (objMaterial != null)
                    {
                        if (!isCheck)
                        {
                            view.SetFocusedRowCellValue("MTL_CODE", objMaterial.MTL_CODE);
                            view.SetFocusedRowCellValue("MTL_NAME", objMaterial.MTL_NAME);
                            view.SetFocusedRowCellValue("MTL_GRADE", objMaterial.MTL_GRADE);
                            view.SetFocusedRowCellValue("MTL_COLOR", objMaterial.MTL_COLOR);
                            view.SetFocusedRowCellValue("UNIT_ID", objMaterial.UNIT);
                            view.SetFocusedRowCellValue("MTL_SEQ_NO", objMaterial.MTL_SEQ_NO);

                            LotDateSelect = DateTime.Now;
                            view.SetFocusedRowCellValue("LOT_DATE", LotDateSelect.Value);
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

        private bool IsFormValidated()
        {
            //Check control empty
            if (this.btePARTY_ID.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.btePARTY_ID, "Supplier can't be Empty");
                this.btePARTY_ID.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(this.txtREF_NO.Text))
            //{
            //    this.dxErrorProvider1.SetError(this.txtREF_NO, "Ref. No. can't be Empty");
            //    this.txtREF_NO.Focus();
            //    return false;
            //}

            if (this.lueWarehouse.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueWarehouse, "Warehouse can't be Empty");
                this.lueWarehouse.Focus();
                return false;
            }

            if (this.dteARRIVAL_DATE.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.dteARRIVAL_DATE, "Arrival date can't be Empty");
                this.dteARRIVAL_DATE.Focus();
                return false;
            }


            if (this.lueARR_TYPE.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueARR_TYPE, "Arrival type can't be Empty");
                this.lueARR_TYPE.Focus();
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


        private void GetMaterial(string sup_code, string locId)
        {
            
            
            try
            {
                using (ArrivalBLL arrBLL = new ArrivalBLL())
                {
                    //var lst = masterbll.GetItem(string.Empty);

                    //this.lstItem = (from b in lst
                    //                    where b.CUST_CODE == customer_code
                    //                    select b).ToList<M_ITEM>();

                    this.lstMaterial = arrBLL.GetMaterialList(string.Empty, string.Empty, sup_code,locId);

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error GetItem", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetArrivalHdr(string FindAll, string arrival_no, DateTime? formDate, DateTime? todate,string whId)
        {

            try
            {

                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    this.lstArrivalHdr = arrivalBLL.GetArrivalHeaderList(FindAll, string.Empty, formDate, todate, whId);
                }

                if (this.lstArrivalHdr == null)
                    this.lstArrivalHdr = new List<T_ARRIVAL_HDR>();

                this.dntJobOrder.DataSource = this.lstArrivalHdr;
                this.grdJobOrder.DataSource = this.lstArrivalHdr;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error GetArrivalHdr", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private List<T_ARRIVAL_HDR> GetArrivalHdr(string arrival_no)
        {
            List<T_ARRIVAL_HDR> lstArrHdr = null;

            try
            {

                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    lstArrHdr = arrivalBLL.GetArrivalHeaderList(string.Empty, arrival_no, null, null, string.Empty);
                }

                if (lstArrHdr == null)
                    lstArrHdr = new List<T_ARRIVAL_HDR>();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error GetArrivalHdr", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return lstArrHdr;

        }

        private void GetArrivalDtl(string arrival_no)
        {
            try
            {
                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    //List<T_ARRIVAL_DTL> lstArrival = arrivalBLL.GetArrivalDetailList(arrival_no);
                    this.lstArrivalDtl = arrivalBLL.GetArrivalDetailList(arrival_no);

                    this.lotSelect.ClearSelection();

                    //this.grdUNIT.DataSource = lstUnit;
                    if (lstArrivalDtl == null)
                        lstArrivalDtl = new List<T_ARRIVAL_DTL>();
                    //DataTable table = ConvertToDataTable(lstUnit);
                    //this.grdUNIT.DataSource = table;
                    this.dtbArrival = UiUtility.ConvertToDataTable(lstArrivalDtl);

                    this.grdLotPlaning.DataSource = dtbArrival;
                }

                this.btnPostData.Enabled = this.CheckEnablePostCSV(arrival_no);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error GetArrivalDtl", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FillData(T_ARRIVAL_HDR objArrival)
        {

            try
            {

                this.btePARTY_ID.EditValue = objArrival.PARTY_ID;
                this.txtPARTY_NAME.Text = objArrival.PARTY_NAME;
                this.bteDOC_NO.Text = objArrival.PO_NO;
                this.txtREF_NO.Text = objArrival.REF_NO;
                this.dteREF_DATE.EditValue = objArrival.REF_DATE;
                this.lueWarehouse.EditValue = objArrival.WH_ID;
                this.txtREMARK.Text = objArrival.REMARK;
                this.lueARR_TYPE.EditValue = objArrival.ARR_TYPE;
                this.txtARRIVAL_NO.Text = objArrival.ARRIVAL_NO;
                this.dteARRIVAL_DATE.EditValue = objArrival.ARRIVAL_DATE;
                this.icbREC_STAT.EditValue = objArrival.REC_STAT;

                this.txtPOST_REF.Text = objArrival.POST_REF;

                if (!string.IsNullOrEmpty(this.txtPOST_REF.Text))
                {
                    this.btnUnLockPost.Image = base.Language.GetBitmap("imgLock");
                    this.btnUnLockPost.Text = "Lock";
                    this.btnUnLockPost.Visible = true;
                }
                else
                {
                    this.btnUnLockPost.Visible = false;
                }

                this.GetArrivalDtl(objArrival.ARRIVAL_NO);
            }

            catch (Exception ex)
            {
                //base.CloseWaitForm();
                XtraMessageBox.Show(this, ex.Message, "Error FillData", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void InsertArrival(string userid)
        {
            string resultMSG = string.Empty;
            string result = string.Empty;
            string arrival_no = string.Empty;

            T_ARRIVAL_HDR objArrHdr = null;

            try
            {
               // base.ShowWaitForm();
                  objArrHdr = new T_ARRIVAL_HDR();
            
                  objArrHdr.ARRIVAL_DATE = this.dteARRIVAL_DATE.DateTime;
                  objArrHdr.WH_ID = (string)this.lueWarehouse.EditValue;
                  objArrHdr.PARTY_ID = (string)this.btePARTY_ID.EditValue;

                  if(!string.IsNullOrEmpty(this.bteDOC_NO.Text))
                    objArrHdr.PO_NO = (string)this.bteDOC_NO.EditValue;

                  objArrHdr.REF_NO = this.txtREF_NO.Text;
           
                  DateTime? REF_DATE;
                  if (this.dteREF_DATE.EditValue != null)
                  {
                      REF_DATE = this.dteREF_DATE.DateTime;
                  }
                  else
                  {
                      REF_DATE = null;
                  }

                  objArrHdr.REF_DATE = REF_DATE;

                  objArrHdr.REMARK = this.txtREMARK.Text;
                  objArrHdr.USER_ID = userid;
                  objArrHdr.REC_STAT = (bool)icbREC_STAT.EditValue;
                  objArrHdr.ARR_TYPE = (string)this.lueARR_TYPE.EditValue;
               

                  var lstView = UiUtility.ConvertToList<T_ARRIVAL_DTL>(this.dtbArrival);

                //Filter only data affected modified.
                var lstArrival = (from arrival in lstView
                                  where arrival.FLAG != 1
                                  select arrival).ToList();

                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    result = arrivalBLL.InsertArrival(objArrHdr, lstArrival, userid, out arrival_no);

                    objArrHdr.ARRIVAL_NO = arrival_no;

                    if (result == "OK")
                    {
                        this.lstArrivalHdr.Add(objArrHdr);

                        this.grdJobOrder.RefreshDataSource();

                        this.txtARRIVAL_NO.Text = arrival_no;

                        this.GetArrivalDtl(arrival_no);


                        resultMSG = "Insert Complete.";

                        this.FormState = eFormState.ReadOnly;

                        //this.btnGenerate.Enabled = true;
                        this.ddbExport.Enabled = true;
                        this.ddbPrint.Enabled = true;

                    }
                    else
                    {
                        resultMSG = "Error" + result;


                    }
                }



                //base.CloseWaitForm();
                XtraMessageBox.Show(this, resultMSG, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {

                //base.CloseWaitForm();
                XtraMessageBox.Show(this, ex.Message, "Error InsertArrival", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            }
        }

        private void UpdatetArrival(string userid)
        {
            string resultMSG = string.Empty;
            string result = string.Empty;

            T_ARRIVAL_HDR objArrHdr = null;

            try
            {
                //base.ShowWaitForm();

                objArrHdr = new T_ARRIVAL_HDR();

                objArrHdr.ARRIVAL_NO = this.txtARRIVAL_NO.Text;
                objArrHdr.ARRIVAL_DATE = this.dteARRIVAL_DATE.DateTime;
                objArrHdr.WH_ID = (string)this.lueWarehouse.EditValue;
                objArrHdr.PARTY_ID = (string)this.btePARTY_ID.EditValue;

                if (!string.IsNullOrEmpty(this.bteDOC_NO.Text))
                    objArrHdr.PO_NO = (string)this.bteDOC_NO.EditValue;

                objArrHdr.REF_NO = this.txtREF_NO.Text;

                DateTime? REF_DATE;
                if (this.dteREF_DATE.EditValue != null)
                {
                    REF_DATE = this.dteREF_DATE.DateTime;
                }
                else
                {
                    REF_DATE = null;
                }
                objArrHdr.REF_DATE = REF_DATE;

               objArrHdr.REMARK = this.txtREMARK.Text;
               objArrHdr.USER_ID = userid;
               objArrHdr.REC_STAT = (bool)icbREC_STAT.EditValue;
               objArrHdr.ARR_TYPE = (string)this.lueARR_TYPE.EditValue;

                


                var lstView = UiUtility.ConvertToList<T_ARRIVAL_DTL>(dtbArrival);

                //Filter only data affected modified.
                var lstArrival = (from arrival in lstView
                                  where arrival.FLAG != 1
                                  select arrival).ToList();

                this.dtbArrival.AcceptChanges();

                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    result = arrivalBLL.UpdateArrival(objArrHdr, lstArrival, userid);

                    if (result == "OK")
                    {
                        //this.lstBranch.Add(branch);

                        this.dtbArrival.Select().ToList<DataRow>().ForEach(r => r["FLAG"] = 1);

                        this.dtbArrival.AcceptChanges();

                        this.grdLotPlaning.RefreshDataSource();

                        resultMSG = "Update Complete.";

                        this.GetArrivalDtl(objArrHdr.ARRIVAL_NO);

                        this.FormState = eFormState.ReadOnly;


                    }
                    else
                    {
                        resultMSG = "Error" + result;


                    }
                }

                //base.CloseWaitForm();
                XtraMessageBox.Show(this, resultMSG, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {

                //base.CloseWaitForm();
                XtraMessageBox.Show(this, ex.Message, "Error UpdatetArrival", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            }
        }

        //////////////

        private void Grid_DeleteLine(ColumnView view, int rowSelect)
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
                        {
                            row.Delete();

                        }
                        else
                        {
                            row["FLAG"] = 0;

                        }
                    }
                }

                if (view.IsEmpty) {

                    this.lueWarehouse.Properties.ReadOnly = false;
                    this.lueWarehouse.Properties.Buttons[0].Enabled = true;


                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error Grid_DeleteLine", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                view.EndSort();

                DataView dtv = new DataView(this.dtbArrival);
                dtv.RowFilter = "[FLAG] <> 0";

                this.grdLotPlaning.DataSource = dtv;
                //this.grdArrival.DataSource = dtv;
            }
        }

        private void PrintProductCard()
        {
            decimal totalQty = 0;
            List<T_ARRIVAL_DTL> lstJLotPlan = null;
            T_ARRIVAL_DTL jLotPlan;
            try
            {
                if (this.lotSelect.SelectedCount > 0)
                {
                    lstJLotPlan = new List<T_ARRIVAL_DTL>(this.lotSelect.SelectedCount);

                    for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                    {
                        jLotPlan = new T_ARRIVAL_DTL();
                        DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                        jLotPlan.ARRIVAL_NO = row.Row["ARRIVAL_NO"].ToString();
                        jLotPlan.LINE_NO = Convert.ToInt32(row.Row["LINE_NO"], NumberFormatInfo.InvariantInfo);
                        jLotPlan.MTL_SEQ_NO = row.Row["MTL_SEQ_NO"].ToString();

                        totalQty += Convert.ToDecimal(row.Row["QTY"], NumberFormatInfo.InvariantInfo);

                        lstJLotPlan.Add(jLotPlan);


                    }

                    int seq = PrintingBuilder.Instance.GeneratePrintSEQ();

                    using (ArrivalBLL jobOrdBll = new ArrivalBLL())
                    {
                        jobOrdBll.InsertLotPlaningToPrint(seq, lstJLotPlan);
                    }

                    using (frmPrintMaterialCard fPrintCard = new frmPrintMaterialCard())
                    {
                        
                        fPrintCard.ARRIVAL_NO = this.txtARRIVAL_NO.Text;
                        //fPrintCard.MTL_SEQ_NO = row.Row["ARRIVAL_NO"].ToString();
                        //fPrintCard.MTL_CODE = this.btePROD_SEQ_NO.Text;
                        fPrintCard.LIST_ARRIVAL_DTL = lstJLotPlan;
                        fPrintCard.TOTAL_QTY = totalQty;
                        fPrintCard.SEQ_NO = seq;
                        fPrintCard.ARRIVAL_TYPE = (string)this.lueARR_TYPE.Text;
                        fPrintCard.ARRIVAL_DATE = dteARRIVAL_DATE.DateTime;
                        //fPrintCard.PRODUCTION_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;

                        fPrintCard.USER_ID = ((frmMainMenu)this.ParentForm).UserID;

                        UiUtility.ShowPopupForm(fPrintCard, this, true);
                    }

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Material to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //private void PrintProductCardPress()
        //{
        //    decimal totalQty = 0;
        //    List<T_ARRIVAL_DTL> lstJLotPlan = null;
        //    T_ARRIVAL_DTL jLotPlan;
        //    try
        //    {
        //        if (this.lotSelect.SelectedCount > 0)
        //        {
        //            lstJLotPlan = new List<T_ARRIVAL_DTL>(this.lotSelect.SelectedCount);

        //            for (int i = 0; i < this.lotSelect.SelectedCount; i++)
        //            {
        //                jLotPlan = new T_ARRIVAL_DTL();
        //                DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);

        //                jLotPlan.ARRIVAL_NO = row.Row["ARRIVAL_NO"].ToString();
        //                jLotPlan.LINE_NO = Convert.ToInt32(row.Row["LINE_NO"], NumberFormatInfo.InvariantInfo);
        //                jLotPlan.MTL_SEQ_NO = row.Row["MTL_SEQ_NO"].ToString();

        //                totalQty += Convert.ToDecimal(row.Row["QTY"], NumberFormatInfo.InvariantInfo);

        //                lstJLotPlan.Add(jLotPlan);


        //            }

        //            int seq = PrintingBuilder.Instance.GeneratePrintSEQ();

        //            using (ArrivalBLL jobOrdBll = new ArrivalBLL())
        //            {
        //                jobOrdBll.InsertLotPlaningToPrint(seq, lstJLotPlan);
        //            }

        //            using (frmPrintMaterialCard fPrintCard = new frmPrintMaterialCard())
        //            {

        //                fPrintCard.ARRIVAL_NO = this.txtARRIVAL_NO.Text;
        //                //fPrintCard.MTL_SEQ_NO = row.Row["ARRIVAL_NO"].ToString();
        //                //fPrintCard.MTL_CODE = this.btePROD_SEQ_NO.Text;
        //                fPrintCard.LIST_ARRIVAL_DTL = lstJLotPlan;
        //                fPrintCard.TOTAL_QTY = totalQty;
        //                fPrintCard.SEQ_NO = seq;
        //                fPrintCard.ARRIVAL_TYPE = (string)this.lueARR_TYPE.Text;
        //                fPrintCard.ARRIVAL_DATE = dteARRIVAL_DATE.DateTime;
        //                //fPrintCard.PRODUCTION_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;

        //                fPrintCard.USER_ID = ((frmMainMenu)this.ParentForm).UserID;

        //                UiUtility.ShowPopupForm(fPrintCard, this, true);
        //            }

        //        }
        //        else
        //        {
        //            XtraMessageBox.Show(this, "Please Select Material to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        private void PrintCargoCheckSheet(string arrival_no, string userid)
        {
            try
            {

                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet dtsResult = new DataSet("DTS_CARGO_CS");

                //get print value header

                //this.GetArrivalHdr(string.Empty, arrival_no, null, null);

                //var lstArrival = (from b in lstArrivalHdr
                //                  where b.ARRIVAL_NO == arrival_no
                //                  select b).ToList<T_ARRIVAL_HDR>();

                var lstArrival = this.GetArrivalHdr(arrival_no);

                DataTable dtHeader = this.ConvertToDataTable(lstArrival);
                dtsResult.Tables.Add(dtHeader);

                //List<T_PICK_DTL> lstpickDTL = this.GetRPTPickingDTL(pickno);
                DataTable dtDetail = this.ConvertToDataTable(this.lstArrivalDtl);//this.dtbArrival;//this.ConvertToDataTable(this.lstArrivalDtl);

                /*
                string expression;
                expression = "ARRIVAL_NO = '" + arrival_no + "'";
                int count = 0;

                DataRow[] foundRows;
                foundRows = dtDetail.Select(expression);
                foreach (DataRow row in foundRows)
                {
                    count += 1;
                }

                int irow = 16 - (count % 16);

                for (int a = 1; a <= irow; a++)
                {
                    dtDetail.Rows.Add(arrival_no, DBNull.Value, "", "", "", DBNull.Value, "", DBNull.Value, "", DBNull.Value, "", "", DBNull.Value, DBNull.Value);
                }
                 * */


                dtsResult.Tables.Add(dtDetail);

                dtsResult.Relations.Add("T_ARRIVAL_HDR_T_ARRIVAL_DTL",
                dtsResult.Tables["T_ARRIVAL_HDR"].Columns["ARRIVAL_NO"],
                dtsResult.Tables["T_ARRIVAL_DTL"].Columns["ARRIVAL_NO"]
                    );

                dtsResult.AcceptChanges();



                ReportViewer viewer = new ReportViewer() { AutoCloseAfterPrint = true };
                //RPT_CARGO_CS rpt = new RPT_CARGO_CS();
                RPT_MTL_IN_SHEET rpt = new RPT_MTL_IN_SHEET();

                rpt.DataSource = dtsResult;
                rpt.Parameters["paramUserPrint"].Value = ((frmMainMenu)this.ParentForm).UserID;//UiUtility.USER_NAME;//((frmMainMenu)this.ParentForm).UserID;
                //rpt.Parameters["paramUserPrint"].Visible = false;
                rpt.CreateDocument();
                viewer.SetReport(rpt);

                base.FinishedProcessing();

                viewer.ShowDialog();

                
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error PrintCargoCheckSheet", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private DataTable ConvertToDataTable<T>(List<T> list)
        {
            var entityType = typeof(T);

            // Lists of type System.String and System.Enum (which includes enumerations and structs) must be handled differently 
            // than primitives and custom objects (e.g. an object that is not type System.Object).
            if (entityType == typeof(String))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else if (entityType.BaseType == typeof(Enum))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (string namedConstant in Enum.GetNames(entityType))
                {
                    var row = dataTable.NewRow();
                    row[0] = namedConstant;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }

            // Check if the type of the list is a primitive type or not. Note that if the type of the list is a custom 
            // object (e.g. an object that is not type System.Object), the underlying type will be null.
            var underlyingType = Nullable.GetUnderlyingType(entityType);
            var primitiveTypes = new List<Type>
            {
                typeof (Byte),
                typeof (Char),
                typeof (Decimal),
                typeof (Double),
                typeof (Int16),
                typeof (Int32),
                typeof (Int64),
                typeof (SByte),
                typeof (Single),
                typeof (UInt16),
                typeof (UInt32),
                typeof (UInt64),
            };

            var typeIsPrimitive = primitiveTypes.Contains(underlyingType);

            // If the type of the list is a primitive, perform a simple conversion.
            // Otherwise, map the object's properties to columns and fill the cells with the properties' values.
            if (typeIsPrimitive)
            {
                var dataTable = new DataTable(underlyingType.Name);
                dataTable.Columns.Add(underlyingType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else
            {
                // TODO:
                // 1. Convert lists of type System.Object to a data table.
                // 2. Handle objects with nested objects (make the column name the name of the object and print "system.object" as the value).

                var dataTable = new DataTable(entityType.Name);
                var propertyDescriptorCollection = TypeDescriptor.GetProperties(entityType);

                // Iterate through each property in the object and add that property name as a new column in the data table.
                foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                {
                    // Data tables cannot have nullable columns. The cells can have null values, but the actual columns themselves cannot be nullable.
                    // Therefore, if the current property type is nullable, use the underlying type (e.g. if the type is a nullable int, use int).
                    var propertyType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) ?? propertyDescriptor.PropertyType;
                    dataTable.Columns.Add(propertyDescriptor.Name, propertyType);
                }

                // Iterate through each object in the list adn add a new row in the data table.
                // Then iterate through each property in the object and add the property's value to the current cell.
                // Once all properties in the current object have been used, add the row to the data table.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();

                    foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                    {
                        var value = propertyDescriptor.GetValue(item);
                        row[propertyDescriptor.Name] = value ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
        }

        private bool CheckEnablePostCSV(string arrNo)
        {
            string resultMsg = string.Empty;

            try
            {
                bool result = false;
                

                if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
                {
                    using (ArrivalBLL docArrBll = new ArrivalBLL())
                    {
                        result = docArrBll.IsArrivalReceiveComplete(arrNo, out resultMsg);
                    }
                }
                else
                {
                    result = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void PostArrivalReceiveToCSV(string arrivalno)
        {
            try
            {
                string seqno = string.Empty;
                DataTable dtbPosted = null;
                string fullFileName = string.Empty;
                string fileName = string.Format("IFPORECEIVE{0}-{1:yyyyMMddHHmm}.CSV", arrivalno, DateTime.Now);

                using (SaveFileDialog fdlg = new SaveFileDialog { Title = "Save Export CSV File", Filter = "CSV files (*.csv)|*.csv", FilterIndex = 1, RestoreDirectory = true, FileName = fileName })
                {
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        fullFileName = fdlg.FileName;


                        using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                        {
                            dtbPosted = inDataBll.PostPurchaseReceiveInterface(arrivalno, ((frmMainMenu)this.ParentForm).UserID, out seqno);
                        }

                        if (dtbPosted != null)
                        {
                            DataTable dtCsv = new DataTable();
                            dtCsv.Columns.Add("Vendor ID");
                            dtCsv.Columns.Add("Invoice/CM #");
                            dtCsv.Columns.Add("Apply to Invoice Number");
                            dtCsv.Columns.Add("Credit Memo");
                            dtCsv.Columns.Add("Date");
                            dtCsv.Columns.Add("Drop Ship");
                            dtCsv.Columns.Add("Customer SO #");
                            dtCsv.Columns.Add("Waiting on Bill");
                            dtCsv.Columns.Add("Customer ID");
                            dtCsv.Columns.Add("Customer Invoice #");
                            dtCsv.Columns.Add("Ship to Name");
                            dtCsv.Columns.Add("Ship to Address-Line One");
                            dtCsv.Columns.Add("Ship to Address-Line Two");
                            dtCsv.Columns.Add("Ship to City");
                            dtCsv.Columns.Add("Ship to State");
                            dtCsv.Columns.Add("Ship to Zipcode");
                            dtCsv.Columns.Add("Ship to Country");
                            dtCsv.Columns.Add("Date Due");
                            dtCsv.Columns.Add("Discount Date");
                            dtCsv.Columns.Add("Discount Amount");
                            dtCsv.Columns.Add("Accounts Payable Account");
                            dtCsv.Columns.Add("Ship Via");
                            dtCsv.Columns.Add("P.O. Note");
                            dtCsv.Columns.Add("Note Prints After Line Items");
                            dtCsv.Columns.Add("Beginning Balance Transaction");
                            dtCsv.Columns.Add("Applied To Purchase Order");
                            dtCsv.Columns.Add("Number of Distributions");
                            dtCsv.Columns.Add("Invoice/CM Distribution");
                            dtCsv.Columns.Add("Apply to Invoice Distribution");
                            dtCsv.Columns.Add("PO Number");
                            dtCsv.Columns.Add("PO Distribution");
                            dtCsv.Columns.Add("Quantity");
                            dtCsv.Columns.Add("Stocking Quantity");
                            dtCsv.Columns.Add("Item ID");
                            dtCsv.Columns.Add("Serial Number");
                            dtCsv.Columns.Add("U/M ID");
                            dtCsv.Columns.Add("U/M No. of Stocking Units");
                            dtCsv.Columns.Add("Description");
                            dtCsv.Columns.Add("G/L Account");
                            dtCsv.Columns.Add("Unit Price");
                            dtCsv.Columns.Add("Stocking Unit Price");
                            dtCsv.Columns.Add("UPC / SKU");
                            dtCsv.Columns.Add("Weight");
                            dtCsv.Columns.Add("Amount");
                            dtCsv.Columns.Add("Job ID");
                            dtCsv.Columns.Add("Used for Reimbursable Expense");
                            dtCsv.Columns.Add("Transaction Period");
                            dtCsv.Columns.Add("Transaction Number");
                            dtCsv.Columns.Add("Displayed Terms");
                            dtCsv.Columns.Add("Return Authorization");
                            dtCsv.Columns.Add("Row Type");
                            dtCsv.Columns.Add("Recur Number");
                            dtCsv.Columns.Add("Recur Frequency");

                            foreach (DataRow row in dtbPosted.Rows)
                            {
                                dtCsv.Rows.Add(
                                    row.ItemArray[0], // 1. Vendor ID
                                    row.ItemArray[1], // 2. Invoice/CM #
                                    "",               // 3. Apply to Invoice Number
                                    "FALSE",          // 4. Credit Memo  
                                    row.ItemArray[2], // 5. Date
                                    "FALSE",          // 6. Drop Ship   
                                    row.ItemArray[3], // 7. Customer SO #
                                    "FALSE",          // 8. Waiting on Bill
                                    row.ItemArray[4], // 9. Customer ID
                                    row.ItemArray[5], //10. Customer Invoice #
                                    row.ItemArray[6], //11. Ship to Name
                                    "",               //12. Ship to Address-Line One
                                    "",               //13. Ship to Address-Line Two
                                    "",               //14. Ship to City
                                    "",               //15. Ship to State
                                    "",               //16. Ship to Zipcode
                                    "",               //17. Ship to Country
                                    row.ItemArray[7], //18. Date Due
                                    row.ItemArray[7], //19. Discount Date
                                    "0",              //20. Discount Amount
                                    row.ItemArray[8], //21. Accounts Payable Account
                                    "Truck",          //22. Ship Via
                                    "",               //23. P.O. Note
                                    "FALSE",          //24. Note Prints After Line Items
                                    "FALSE",          //25. Beginning Balance Transaction
                                    "TRUE",           //26. Applied To Purchase Order
                                    row.ItemArray[9], //27. Number of Distributions
                                    row.ItemArray[10],//28. Invoice/CM Distribution 
                                    "0",              //29. Apply to Invoice Distribution
                                    row.ItemArray[11],//30. PO Number
                                    row.ItemArray[12],//31. PO Distribution
                                    row.ItemArray[13],//32. Quantity
                                    row.ItemArray[13],//33. Stocking Quantity
                                    row.ItemArray[14],//34. Item ID
                                    "",               //35. Serial Number
                                    row.ItemArray[15],//36. U/M ID
                                    "1",              //37. U/M No. of Stocking Units
                                    row.ItemArray[16],//38. Description
                                    row.ItemArray[17],//39. G/L Account
                                    row.ItemArray[18],//40. Unit Price
                                    row.ItemArray[18],//41. Stocking Unit Price
                                    "",               //42. UPC / SKU
                                    "0",              //43. Weight
                                    row.ItemArray[19],//44. Amount
                                    "",               //45. Job ID
                                    "FALSE",          //46. Used for Reimbursable Expense
                                    "36",             //47. Transaction Period
                                    row.ItemArray[20],//48. Transaction Number
                                    row.ItemArray[21],//49. Displayed Terms
                                    "",               //50. Return Authorization
                                    "0",              //51. Row Type
                                    "0",              //52. Recur Number
                                    "0"               //53. Recur Frequenc
                                    );
                            }

                            if (!string.IsNullOrEmpty(fullFileName))
                            {
                                using (CsvWriter writer = new CsvWriter() { Request_Header = true, Spliter = "," })
                                {
                                    writer.WriteCsv(dtCsv, fullFileName);
                                }
                            }

                            using (ArrivalBLL arrivalBll = new ArrivalBLL())
                            {
                                arrivalBll.UpdatePostCSV(arrivalno, fullFileName, ((frmMainMenu)this.ParentForm).UserID);
                            }

                            this.txtPOST_REF.EditValue = fullFileName;
                            this.btnPostData.Enabled = false;

                            this.btnUnLockPost.Image = base.Language.GetBitmap("imgLock");
                            this.btnUnLockPost.Text = "Lock";
                            this.btnUnLockPost.Visible = true;

                            GridView viewList = (GridView)this.grdJobOrder.MainView;
                            viewList.SetFocusedRowCellValue("POST_REF", fullFileName);

                            ICollection<string> files = new System.Collections.ObjectModel.Collection<string>()
                            {
                                fullFileName
                            };

                            this.OpenPath(files);

                        }
                    }
                }

            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UnlockAlreadyPostCSV(string arrivalno)
        {
            string resultMsg = string.Empty;

            try
            {
                using (ArrivalBLL arrivalBll = new ArrivalBLL())
                {
                    resultMsg = arrivalBll.UnlockAlreadyPostCSV(arrivalno, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (resultMsg == "OK")
                {
                    this.btnUnLockPost.Image = base.Language.GetBitmap("imgUnlock");
                    this.btnUnLockPost.Text = "Unlock";

                    this.btnPostData.Enabled = true;
                    this.txtPOST_REF.Text = string.Empty;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Get_InitialTest_Value()
        {
            //this.dtpProductionPlanDateSelect.EditValue = DateTime.Now;
            //this.dtpJOB_DATE.EditValue = DateTime.Now;
            //this.dtpPRODUCTION_DATE.EditValue = DateTime.Now;
            //this.dtpMP_START_DATE.EditValue = DateTime.Now;
            //this.dtpMP_STOP_DATE.EditValue = DateTime.Now.AddDays(1);

            //this.txtPLAN_DAY.Text = "2";
        }

       //***************************************************************************************************************************************//

        private void frmJobOrder_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            
            //this.xtpJobOrderDetail.PageVisible = false;
            //this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            //this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();
            //this.GetJobOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            //this.FormState = eFormState.ReadOnly;

            ////test show column customization.
            ////GridView view = this.grdJobOrder.Views[0] as GridView;
            ////view.ShowCustomization();
        }

        private void frmJobOrder_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtpJobOrderDetail.PageVisible = false;
            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;
            this.InitializaLOVData();

            DateTime? fromDt, toDt;
            string branch = string.Empty;
            string whId = string.Empty;
            if (this.dteFromDate.EditValue != null)
                fromDt = this.dteFromDate.DateTime;
            else
                fromDt = null;

            if (this.dteToDate.EditValue != null)
                toDt = this.dteToDate.DateTime;
            else
                toDt = null;

            if (this.lueSearchWH.EditValue != null)
            {
                whId = (string)this.lueSearchWH.EditValue;
            }

            this.GetArrivalHdr(this.txtFindAll.Text, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime, whId);

            //this.FormState = eFormState.ReadOnly;

            

            //test show column customization.
            //GridView view = this.grdJobOrder.Views[0] as GridView;
            //view.ShowCustomization();
        }

        private void btePARTY_ID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string partyid = string.Empty;
            string partyname = string.Empty;

            using (frmLOVParty fCustList = new frmLOVParty())
            {
                fCustList.PARTY_TYPE = "V"; //find only Customer
                //result = UiUtility.ShowPopupForm(fCustList, this, true);
                result = fCustList.ShowDialog(this);

                partyid = fCustList.PARTY_ID;
                partyname = fCustList.PARTY_NAME;
            }

            if (result == DialogResult.OK)
            {
                btnEdit.Text = partyid;
                this.txtPARTY_NAME.Text = partyname;

                this.bteDOC_NO.Focus();
            }
        }

        private void btePARTY_ID_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            ButtonEdit editor = (ButtonEdit)sender;
            if (editor.Text == string.Empty)
            {
                //e.Cancel = true;
                this.txtPARTY_NAME.Text = string.Empty;

                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Value can't be null", ErrorType.Warning);
                editor.Focus();
            }
            else
            {
                bool isValid = this.GetCustomerByCode(editor.Text);
                if (!isValid)
                {
                    //e.Cancel = true;
                    UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Invalid Customer", ErrorType.Warning);
                    editor.Focus();
                }
                else
                {
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
                }
            }
        }

        //private void bteMC_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    ButtonEdit btnEdit = (ButtonEdit)sender;
        //    try
        //    {
        //        //Open Popup For Select Supplier.
        //        DialogResult result;
        //        string mcno = string.Empty;
        //        string mcname = string.Empty;
        //        using (frmLOVMachine fMcList = new frmLOVMachine())
        //        {
        //            object value = this.luePRODUCTION_TYPE.EditValue;
        //            if (this.luePRODUCTION_TYPE.EditValue != null)
        //            {
        //                fMcList.MACHINE_TYPE = value.ToString();
        //            }
        //            else
        //            {
        //                fMcList.MACHINE_TYPE = string.Empty;
        //            }

        //            result = UiUtility.ShowPopupForm(fMcList, this, true);

        //            mcname = fMcList.MACHINE_NAME;
        //            mcno = fMcList.MC_NO;
        //        }

        //        if (result == DialogResult.OK)
        //        {
        //            btnEdit.Text = mcname;
        //            this.lblMC_NO_VALUE.Text = mcno;

        //            this.btePROD_SEQ_NO.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        //private void btePROD_SEQ_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    ButtonEdit editor = (ButtonEdit)sender;
        //    //Open Popup For Select Supplier.
        //    DialogResult result;
        //    string productno = string.Empty;
        //    using (frmLOVProduct fPrdList = new frmLOVProduct())
        //    {
        //        fPrdList.FormCalling = eFormCalling.fJobOrder;
        //        fPrdList.PARTY_ID = this.btePARTY_ID.Text;

        //        if(this.luePRODUCTION_TYPE.EditValue != null)
        //        {
        //            fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
        //        }
        //        else
        //        {
        //            this.luePRODUCTION_TYPE.EditValue = "V";
        //            fPrdList.PRODUCTION_TYPE = "V";
        //        }

        //        fPrdList.JobOrder_GetProductList();

        //        result = UiUtility.ShowPopupForm(fPrdList, this, true);
        //        productno = fPrdList.PRODUCT_NO;
        //    }

        //    if (result == DialogResult.OK)
        //    {
        //        editor.Text = productno;
        //        SendKeys.Send("{TAB}");
        //    }
        //}

        private void grvJobOrder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                base.SuspendLayout();

                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    //string jobOrdNo = view.GetRowCellValue(info.RowHandle, "ARRIVAL_NO").ToString();

                    T_ARRIVAL_HDR objArrival = view.GetRow(info.RowHandle) as T_ARRIVAL_HDR;

                    this.btnAddNew.Visible = false;

                    //Change tab view.
                    this.xtpJobOrderList.PageEnabled = false;

                    this.xtpJobOrderDetail.PageVisible = true;
                    this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderDetail;

                    //Call record detail.
                    //this.GetArrivalDtl(jobOrdNo);

                    this.FillData(objArrival);

                    this.FormState = eFormState.ReadOnly;

                    this.btnDelete.Enabled = false;

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                    this.dntJobOrder.Focus();

                    base.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntJobOrder_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (this.FormState != eFormState.ReadOnly)
                {
                    return;
                }
                
                GridView view = (GridView)this.grdJobOrder.Views[0]; //this.gridView2

                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderDetail)
                {
                    //string jobOrdNo = view.GetFocusedRowCellValue("ARRIVAL_NO").ToString();
                    //this.GetArrivalHdr(string.Empty, this.txtARRIVAL_NO.Text,
                    //this.GetArrivalDtl(jobOrdNo);

                    T_ARRIVAL_HDR objArrival = view.GetRow(view.FocusedRowHandle) as T_ARRIVAL_HDR;

                    this.FillData(objArrival);
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

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtpJobOrderDetail.PageVisible = false;
                        this.xtpJobOrderList.PageEnabled = true;
                        this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                    

                        break;

                    case eFormState.Edit:

                        this.GetArrivalDtl(this.txtARRIVAL_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpJobOrderDetail.PageVisible = false;
                        this.xtpJobOrderList.PageEnabled = true;
                        this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderList;

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

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    this.btnEdit.Enabled = false;
        //    this.btnDelete.Enabled = true;
        //    this.btnSave.Enabled = true;
        //    this.btnCancel.Enabled = true;

        //    this.ddbPrint.Enabled = false;

        //    try
        //    {

        //        GridView view = (GridView)this.grdLotPlaning.Views[0];

        //        //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = false;
        //        UiUtility.SetGridReadOnly(view, false);
        //        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);

        //        this.grdLotPlaning.Focus();

                
        //        view.FocusedRowHandle = GridControl.NewItemRowHandle;

        //        if (this.dtbLotPlaning.Rows.Count == 0)
        //        {
        //            view.SetFocusedRowCellValue("SHIFT_DATE", this.dtpMP_START_DATE.DateTime);
        //        }

        //        view.FocusedColumn = view.VisibleColumns[2];
        //        view.ShowEditor();
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }

        //}

        private void btnSaveLotPlan_Click(object sender, EventArgs e)
        {
            try
            {

                GridView view = this.grdLotPlaning.MainView as GridView;

                this.grdLotPlaning.FocusedView.PostEditor();

                if (view.State == GridState.Editing && view.FocusedRowHandle == GridControl.NewItemRowHandle && view.FocusedColumn.FieldName != "ITEM_CUST_CODE")
                {
                    XtraMessageBox.Show(this, "Please Press Enter to complete Entry Process!!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;

                }

                if (!this.IsFormValidated()) return;

                switch (this.FormState)
                {
                    case eFormState.Add:
                        this.InsertArrival(((frmMainMenu)this.ParentForm).UserID);
                        break;
                    case eFormState.Edit:
                        this.UpdatetArrival(((frmMainMenu)this.ParentForm).UserID);
                        break;
                    case eFormState.ReadOnly:
                        //nothing
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
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = true;

                this.ddbPrint.Enabled = true;
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.xtpJobOrderList.PageEnabled = false;
            this.xtpJobOrderDetail.PageVisible = true;
            this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderDetail;

            this.dteARRIVAL_DATE.DateTime = DateTime.Now;

            

            this.btnAddNew.Visible = false;
            //this.btePARTY_ID.EditValue = "VST002";
            this.lueWarehouse.Focus();
            //this.btePARTY_ID.Focus();

           // this.btnGenerate.Enabled = false;
            this.ddbExport.Enabled = false;
            this.ddbPrint.Enabled = false;

            this.lueARR_TYPE.EditValue = "V";
           // this.lueWarehouse.EditValue = "MTL";
            this.dteREF_DATE.DateTime = DateTime.Now;

        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!this.IsFormValidated()) return;

        //    switch (this.FormState)
        //    {
        //        case eFormState.Add:
        //            this.InsertJobOrder();
        //            break;
        //        case eFormState.Edit:
        //            this.UpdateJobOrder();
        //            break;
        //        case eFormState.ReadOnly:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private void dtpMP_START_DATE_Validating(object sender, CancelEventArgs e)
        //{
        //    DateEdit dateEdit = (DateEdit)sender;
        //    //int leadTime = this.dtpMP_STOP_DATE.DateTime.Day - dateEdit.DateTime.Day;
        //    double leadDay = this.dtpMP_START_DATE.DateTime.Subtract(dateEdit.DateTime).TotalDays;

        //    this.txtPLAN_DAY.EditValue = Convert.ToInt32(leadDay) + 1;
        //}

        //private void dtpMP_STOP_DATE_Validating(object sender, CancelEventArgs e)
        //{
        //    DateEdit dateEdit = (DateEdit)sender;

        //    //int leadTime = dateEdit.DateTime.Day - this.dtpMP_START_DATE.DateTime.Day;

        //    double leadDay = dateEdit.DateTime.Subtract(this.dtpMP_START_DATE.DateTime).TotalDays;
        //    //TimeSpan leadTime = dateEdit.DateTime.Subtract(this.dtpMP_START_DATE.DateTime);

        //    this.txtPLAN_DAY.EditValue = Convert.ToInt32(leadDay) + 1;
        //}

        private void grvLotPlaning_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                int maxRow = Convert.ToInt32(view.Columns["LINE_NO"].SummaryItem.SummaryValue, NumberFormatInfo.CurrentInfo);
                view.SetFocusedRowCellValue("LINE_NO", maxRow + 1);

                //if (LotDateSelect.HasValue)
                //    view.SetFocusedRowCellValue("LOT_DATE", LotDateSelect.Value);

                //view.SetFocusedRowCellValue("QTY_PER_LABEL", Convert.ToInt32(this.lblStandardQty.Text, NumberFormatInfo.InvariantInfo));
                //view.SetFocusedRowCellValue("QTY_PER_LABEL", Convert.ToInt32(this.txtStandardQty.EditValue, NumberFormatInfo.InvariantInfo));
                view.SetFocusedRowCellValue("ARRIVAL_NO", this.txtARRIVAL_NO.Text);
                view.SetFocusedRowCellValue("STATUS", "NEW");
                view.SetFocusedRowCellValue("FLAG", 2);
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InitNewRow : " + ex.Message, "Error grvLotPlaning_InitNewRow", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //private void grvLotPlaning_rps_txtNO_OF_LABEL_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        GridView view = (GridView)this.grdLotPlaning.Views[0]; //this.gridView2
        //        TextEdit editor = (TextEdit)sender;

        //        if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
        //        {
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            int qtyLabel = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "QTY_PER_LABEL"), NumberFormatInfo.InvariantInfo);
        //            view.SetFocusedRowCellValue("TOTAL_QTY", Convert.ToInt32(editor.EditValue, NumberFormatInfo.InvariantInfo) * qtyLabel);

        //            //UiUtility.UpdateGroupSummaryEnh(ref view);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        private void grvLotPlaning_rps_txtQTY_PER_LABEL_Validating(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdLotPlaning.MainView;

            try
            {
                TextEdit editor = sender as TextEdit;

                if (editor.Text == "0")
                {
                    XtraMessageBox.Show(this, "Qty should be greater than 0 !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    editor.Text = "";
                }
                else if (editor.Text == string.Empty)
                {
                    e.Cancel = true;
                    editor.Focus();
                }
                else
                {
                    if (Convert.ToInt32(view.GetFocusedRowCellValue("FLAG"), NumberFormatInfo.CurrentInfo) != 2)
                        view.SetFocusedRowCellValue("FLAG", 3); //UPDATE

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "Validating : " + ex.Message, "Error grvLotPlaning_rps_txtQTY_PER_LABEL_Validating", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlaning_GotFocus(object sender, EventArgs e)
        {
            try
            {

                GridView view = (GridView)sender;
                
                //if (view.HasColumnErrors) return;

                //view.FocusedRowHandle = GridControl.NewItemRowHandle;

                //if (this.dtbLotPlaning.Rows.Count == 0)
                //{
                //    view.SetFocusedRowCellValue("SHIFT_DATE", this.dtpMP_START_DATE.DateTime);
                //}

                //view.FocusedColumn = view.VisibleColumns[1];
                view.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLotPlaning_GotFocus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlaning_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                //this.CalculateSummary();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLotPlaning_RowUpdated", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintProductCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderDetail)
                {
                    //here
                  //  if (this.lueWarehouse.EditValue.ToString() == "LC00001")//INJ
                        this.PrintProductCard();
                 //   else if (this.lueWarehouse.EditValue.ToString() == "LC00002")//PRS
                  //      this.PrintProductCardPress();
                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Detail Page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintJobOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderDetail)
                {
                    
                    var pickhdr = this.lstArrivalHdr.Single(b => b.ARRIVAL_NO == this.txtARRIVAL_NO.Text);
                    this.PrintCargoCheckSheet(pickhdr.ARRIVAL_NO, ((frmMainMenu)this.ParentForm).UserID);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Detail Page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        private void btn02Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtpJobOrderDetail.PageVisible = false;
                        this.xtpJobOrderList.PageEnabled = true;
                        this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                       UiValidations.Validate_Clear(this.lueWarehouse, ref this.dxErrorProvider1);
                       UiValidations.Validate_Clear(this.btePARTY_ID, ref this.dxErrorProvider1);
                        break;

                    case eFormState.Edit:

                        this.GetArrivalDtl(this.txtARRIVAL_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtpJobOrderDetail.PageVisible = false;
                        this.xtpJobOrderList.PageEnabled = true;
                        this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderList;

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
                //if (this.delSODtl != null)
                //{
                //    this.delSODtl.Clear();
                //    this.delSODtl = null;
                //}

                this.FormState = eFormState.ReadOnly;
            }

        }

        private void grvLotPlaning_ShowingEditor(object sender, CancelEventArgs e)
        {

            GridView view = sender as GridView;
            try
            {
                if (view.FocusedRowHandle == GridControl.InvalidRowHandle || view.FocusedRowHandle == GridControl.NewItemRowHandle)
                {
                    e.Cancel = false;
                    return;
                }

                string status = view.GetFocusedRowCellValue("GEN_LABEL_STATUS").ToString();
                switch (view.FocusedColumn.FieldName)
                {
                    case "MTL_CODE":
                        e.Cancel = !string.IsNullOrEmpty(status);
                        break;
                    case "LOT_DATE":
                        e.Cancel = !string.IsNullOrEmpty(status);
                        break;
                    case "QTY":
                        e.Cancel = !string.IsNullOrEmpty(status);
                        break;
                    case "REMARK":
                        e.Cancel = false;
                        break;
                    case "LABEL":
                        e.Cancel = false;
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void grvLotPlaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.btnEdit.Enabled) return;

            try
            {
                GridView view = sender as GridView;

                //if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;
                
                //if (e.KeyCode == Keys.Delete)
                //{
                //    int rowHandle = view.FocusedRowHandle;

                //    string status = view.GetRowCellValue(rowHandle, "STATUS").ToString();

                //    if (status.Equals("GENERATED") || status.Equals("PLANNED"))
                //    {
                //        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this Lot?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //        if (isDelete == DialogResult.Yes)
                //        {
                //            this.DeleteLotPlanning(view, rowHandle);

                //            view.FocusedRowHandle = GridControl.NewItemRowHandle;
                //            view.ShowEditor();
                //        }
                //    }
                //    else
                //    {
                //        XtraMessageBox.Show(this, "This Lot already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //    }
                //}


                if (e.KeyCode == Keys.Delete && this.formState != eFormState.ReadOnly)
                {

                    int rowHandle = view.FocusedRowHandle;

                    string status = view.GetFocusedRowCellDisplayText("STATUS");
                    if (status == "NEW" || status == "GENERATED")
                    {

                        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            this.Grid_DeleteLine(view, rowHandle);
                        }
                    }
                    else
                    {
                     XtraMessageBox.Show(this, " Can not delete this Record!! Receiving already complete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (this.formState == eFormState.Add)
                    {
                        if (this.grvLotPlaning.RowCount <= 1)
                        {
                            this.btePARTY_ID.Properties.ReadOnly = false;
                            this.btePARTY_ID.Properties.Buttons[0].Enabled = true;
                        }
                        else
                        {

                            this.btePARTY_ID.Properties.ReadOnly = true;
                            this.btePARTY_ID.Properties.Buttons[0].Enabled = false;
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error grvLotPlaning_KeyDown", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DateTime? fromDt, toDt;
            string whId= string.Empty;
            if (this.dteFromDate.EditValue != null)
                fromDt = this.dteFromDate.DateTime;
            else
                fromDt = null;

            if (this.dteToDate.EditValue != null)
                toDt = this.dteToDate.DateTime;
            else
                toDt = null;

            if (this.lueSearchWH.EditValue != null)
            {
                whId = (string)this.lueSearchWH.EditValue;
            }

            this.GetArrivalHdr(this.txtFindAll.Text, string.Empty, fromDt, toDt, whId);
        }

        private void frmJobOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdJobOrder.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdLotPlaning.Views[0]);

            this.Controls.Clear();
        }

        private void txtREF_NO_Validating(object sender, CancelEventArgs e)
        {
            //if (this.FormState == eFormState.ReadOnly) return;

            //TextEdit editor = (TextEdit)sender;
            //bool isValid = UiValidations.Validate_Empty(editor, ref this.dxErrorProvider1, "Value can't null", ErrorType.Warning);
            //if (!isValid)
            //{
            //    e.Cancel = false;
            //    editor.Focus();
            //}
        }

        private void frmJobOrder_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            if (this.btnEdit.Enabled)
                            {
                                this.GetArrivalDtl(this.txtARRIVAL_NO.Text);
                            }
                        }
                    }
                    
                    break;
                default:
                    break;
            }
        }

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    GridView view = (GridView)this.grdLotPlaning.Views[0];
        //    JobLotPlan jobLot = null;
        //    try
        //    {
        //        if (this.lotSelect.SelectedCount > 0)
        //        {
        //            DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete Lot?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        //            if (isDelete == DialogResult.Yes)
        //            {
        //                view.BeginSort();

        //                if (this.deljLotPlan == null)
        //                {
        //                    this.deljLotPlan = new List<JobLotPlan>();
        //                }

        //                for (int i = 0; i < this.lotSelect.SelectedCount; i++)
        //                {
        //                    DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);
        //                    if (row.Row != null)
        //                    {
        //                        jobLot = new JobLotPlan();
        //                        jobLot.JOB_NO = row.Row["JOB_NO"].ToString();
        //                        jobLot.LINE_NO = Convert.ToInt32(row.Row["LINE_NO"], NumberFormatInfo.CurrentInfo);
        //                        jobLot.FLAG = 0;

        //                        this.deljLotPlan.Add(jobLot);
        //                    }

        //                    row.Delete();
        //                }

        //                view.EndSort();
        //                this.dtbLotPlaning.AcceptChanges();
        //            }
        //        }
        //        else
        //        {
        //            XtraMessageBox.Show(this, "PLEASE SELECT RECORD TO DELETE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}


        private void btnEdit_Click(object sender, EventArgs e)
        {
            //this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.btePARTY_ID.Focus();

            this.ddbPrint.Enabled = false;

            try
            {

                //GridView view = (GridView)this.grdLotPlaning.Views[0];

                //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = false;
                //UiUtility.SetGridReadOnly(view, false);
                //UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
               
                //this.grdLotPlaning.Focus();

                this.FormState = eFormState.Edit;

                //view.FocusedRowHandle = GridControl.NewItemRowHandle;
                //view.FocusedColumn = view.VisibleColumns[2];
                //view.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdLotPlaning.Views[0];

            string messageQ = string.Empty;

            try
            {
                if (this.lotSelect.SelectedCount > 0)
                {
                    bool isStart = false;

                    DataRowView row;
                    int delCount = 0;

                    //DataRow[] rows = new DataRow[];

                    for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                    {
                        row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                        if (row.Row["STATUS"].ToString().Equals("IN PROGRESS") || row.Row["STATUS"].ToString().Equals("COMPLETED"))
                        {
                            isStart = true;
                        }
                        else
                        {
                            delCount++;
                        }
                    }



                    if (delCount > 0)
                    {
                        messageQ = (isStart ? "Do you want to delete this Arrival?\n(Some Material already Start that arrival can not be delete)" : "Do you want to delete this arrival?");

                        DialogResult isDelete = XtraMessageBox.Show(this, messageQ, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            DataRow[] rows = new DataRow[delCount];
                            int j = 0;

                            for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                            {
                                row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                                if (!(row.Row["STATUS"].ToString().Equals("IN PROGRESS") || row.Row["STATUS"].ToString().Equals("COMPLETED")))
                                {
                                    rows[j++] = row.Row;
                                }

                            }

                            view.BeginSort();
                            try
                            {
                                int flag = 0;
                                foreach (DataRow rowDel in rows)
                                {
                                    if (rowDel != null)
                                    {
                                        flag = Convert.ToInt32(rowDel["FLAG"], NumberFormatInfo.CurrentInfo);
                                        if (flag.Equals(2))
                                            rowDel.Delete();
                                        else
                                            rowDel["FLAG"] = 0;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(this, ex.Message, "Error btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                            finally
                            {
                                view.EndSort();
                                this.dtbArrival.AcceptChanges();

                                DataView dtv = new DataView(this.dtbArrival);
                                dtv.RowFilter = "[FLAG] <> 0";

                                this.grdLotPlaning.DataSource = dtv;
                            }

                            if (view.IsEmpty)
                            {

                                this.lueWarehouse.Properties.ReadOnly = false;
                                this.lueWarehouse.Properties.Buttons[0].Enabled = true;
                            }


                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "All of Select Material already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }

                }
                else
                {
                    XtraMessageBox.Show(this, "PLEASE SELECT RECORD TO DELETE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                //if (this.lotSelect.SelectedCount > 0)
                //{
                //    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete Lot?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //    if (isDelete == DialogResult.Yes)
                //    {
                //        view.BeginSort();

                //        if (this.deljLotPlan == null)
                //        {
                //            this.deljLotPlan = new List<JobLotPlan>();
                //        }

                //        for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                //        {
                //            DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);
                //            if (row.Row != null)
                //            {
                //                jobLot = new JobLotPlan();
                //                jobLot.JOB_NO = row.Row["JOB_NO"].ToString();
                //                jobLot.LINE_NO = Convert.ToInt32(row.Row["LINE_NO"], NumberFormatInfo.CurrentInfo);
                //                jobLot.FLAG = 0;

                //                this.deljLotPlan.Add(jobLot);
                //            }

                //            row.Delete();
                //        }

                //        view.EndSort();
                //        this.dtbLotPlaning.AcceptChanges();
                //    }
                //}
                //else
                //{
                //    XtraMessageBox.Show(this, "PLEASE SELECT RECORD TO DELETE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error btnDelete_Click", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlanning_rps_calNO_OF_LABEL_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdLotPlaning.Views[0]; //this.gridView2
                CalcEdit editor = (CalcEdit)sender;

                if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    int qtyLabel = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "QTY_PER_LABEL"), NumberFormatInfo.InvariantInfo);
                    view.SetFocusedRowCellValue("TOTAL_QTY", Convert.ToInt32(editor.EditValue, NumberFormatInfo.InvariantInfo) * qtyLabel);

                    //UiUtility.UpdateGroupSummaryEnh(ref view);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlaning_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            try
            {
                string resultValue = string.Empty;
                GridView view = (GridView)sender;

                switch (e.FocusedColumn.FieldName)
                {
                    case "MTL_CODE":
                        break;

                    case "LOT_DATE":
                        if (e.PrevFocusedColumn != null)
                        {
                            if (e.PrevFocusedColumn.FieldName.Equals("MTL_CODE"))
                            {
                                this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            }
                        }
                        else
                        {

                            view.FocusedRowHandle = GridControl.NewItemRowHandle;
                            view.FocusedColumn = view.Columns["MTL_CODE"];
                            view.ShowEditor();
                        }

                        break;

                    case "QTY":
                        if (e.PrevFocusedColumn != null)
                        {
                            if (e.PrevFocusedColumn.FieldName.Equals("MTL_CODE"))
                            {
                                this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            }

                            if (e.PrevFocusedColumn.FieldName.Equals("LOT_DATE"))
                            {
                                this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            }
                        }
                        else
                        {
                            view.FocusedRowHandle = GridControl.NewItemRowHandle;
                            view.FocusedColumn = view.Columns["MTL_CODE"];
                            view.ShowEditor();
                        }

                        break;

                    case "REMARK":
                        if (e.PrevFocusedColumn != null)
                        {
                            if (e.PrevFocusedColumn.FieldName.Equals("MTL_CODE"))
                            {
                                this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            }

                            if (e.PrevFocusedColumn.FieldName.Equals("QTY"))
                            {
                                this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                            }
                        }
                        else
                        {
                            view.FocusedRowHandle = GridControl.NewItemRowHandle;
                            view.FocusedColumn = view.Columns["MTL_CODE"];
                            view.ShowEditor();
                        }

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLotPlaning_FocusedColumnChanged", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void CheckPreviouseColumn(GridView view, GridColumn preColumn)
        {


            string resultValue = string.Empty;
            switch (preColumn.FieldName)
            {
                case "MTL_CODE":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                        view.ShowEditor();
                    }
                    break;
                case "LOT_DATE":
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

                default:
                    break;
            }


        }

        private void grvLotPlaning_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            
            GridView view = (GridView)sender;
            
            try
            {
                if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    if (e.Row == null) return;

                    DataRowView rowView = (DataRowView)e.Row;

                    if (rowView["QTY"].ToString() == "")
                    {
                        e.Valid = false;
                        view.FocusedColumn = view.Columns["QTY"];
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "ValidateRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

                

        private void grvLotPlaning_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = ExceptionMode.NoAction;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "grvLotPlaning_InvalidRowException " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlaning_rps_dtpSHIFT_DATE_EditValueChanging(object sender, ChangingEventArgs e)
        {
            DateEdit editor = sender as DateEdit;

            if (e.NewValue != null)
                LotDateSelect = Convert.ToDateTime(e.NewValue);
            else
                LotDateSelect = null;
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


                this.btePARTY_ID.Focus();
            }
        }

        private void grvLotPlaning_rps_bte_mtl_code_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            try
            {
                GridView view = (GridView)this.grdLotPlaning.Views[0];

                //int assignQty = Convert.ToInt32(UiUtility.IsNullValue(view.GetRowCellValue(view.FocusedRowHandle, "ASSIGN_QTY"), "0"), NumberFormatInfo.CurrentInfo);
                //if (assignQty > 0)
                //{
                //    XtraMessageBox.Show(this, "Already Assign to Picking", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //}
                //else
                //{

                    ButtonEdit editor = (ButtonEdit)sender;
                    editor.EditValue = null;
                    //Open Popup For Select Product.
                    using (frmLOVMtl fPrdList = new frmLOVMtl
                    {
                        FormCalling = eFormCalling.fArrivalEntry,
                        PARTY_ID = this.btePARTY_ID.EditValue.ToString(),
                        WH_ID = this.lueWarehouse.EditValue.ToString()
                    })
                    {
                        fPrdList.GetMaterialList();

                        DialogResult result = UiUtility.ShowPopupForm(fPrdList, this, true);
                        if (result == DialogResult.OK)
                        {
                            bool isDup = UiUtility.IsDuplicated(view, "MTL_SEQ_NO", fPrdList.MTL_SEQ_NO);
                            if (!isDup)
                            {
                                try
                                {
                                    this.grvLotPlaning.AddNewRow();

                                    editor.EditValue = fPrdList.MTL_CODE;
                                    editor.Update();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.Message);
                                }

                                
                                //this.Get .GetPoDetailRecord(view, fMtl.PO_NO, fMtl.PO_LINE);
                                this.GetMTLByNo(view, this.btePARTY_ID.EditValue.ToString(), fPrdList.MTL_CODE, false);
                                //editor.Update();

                                this.lueWarehouse.Properties.ReadOnly = true;
                                this.lueWarehouse.Properties.Buttons[0].Enabled = false;

                                this.btePARTY_ID.Properties.ReadOnly = true;
                                this.btePARTY_ID.Properties.Buttons[0].Enabled = false;

                                editor.SendKey(new KeyEventArgs(Keys.Tab));

                                //if (!view.IsEmpty)
                                //{

                                //    this.lueWarehouse.Properties.ReadOnly = true;
                                //    this.lueWarehouse.Properties.Buttons[0].Enabled = false;
                                //}
                            }
                            else
                            {
                                editor.Undo();
                                //MessageBox.Show("Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                XtraMessageBox.Show(this, "Duplicate Record!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

        }

        private void grvLotPlaning_rps_bte_mtl_code_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            GridView view = (GridView)this.grdLotPlaning.MainView;
            ButtonEdit editor = sender as ButtonEdit;

            string partyid = string.Empty;

            if (this.btePARTY_ID.EditValue != null)
            {
                partyid = (string)this.btePARTY_ID.EditValue;
            }

            if (editor.Text == string.Empty)
            {
                //e.Cancel = true;
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Value can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                this.GetMTLByNo(view, partyid, editor.Text, false);

                if (objMaterial != null)
                {
                    editor.EditValue = objMaterial.MTL_CODE;
                    view.SetFocusedRowCellValue("MTL_NAME", objMaterial.MTL_NAME);
                    view.SetFocusedRowCellValue("UNIT", objMaterial.UNIT);
                    view.SetFocusedRowCellValue("MTL_SEQ_NO", objMaterial.MTL_SEQ_NO);

                    LotDateSelect = DateTime.Now;
                    view.SetFocusedRowCellValue("LOT_DATE", LotDateSelect.Value);

                    this.lueWarehouse.Properties.ReadOnly = true;
                    this.lueWarehouse.Properties.Buttons[0].Enabled = false;

                    this.btePARTY_ID.Properties.ReadOnly = true;
                    this.btePARTY_ID.Properties.Buttons[0].Enabled = false;

                }

                bool isValid = true;

                if (objMaterial == null)
                {
                    isValid = false;
                }

                if (!isValid)
                {
                    //e.Cancel = true;
                    //UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Invalid Material", ErrorType.Critical);
                    XtraMessageBox.Show(this, "Invalid Material code. Please try another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    
                    editor.Focus();

                    view.SetFocusedRowCellValue("MTL_CODE", null);
                    view.SetFocusedRowCellValue("LOT_DATE", null);
                    view.SetFocusedRowCellValue("MTL_NAME", null);
                    view.SetFocusedRowCellValue("UNIT", null);
                    view.SetFocusedRowCellValue("MTL_SEQ_NO", null);

                }
                else
                {
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);

                    LotDateSelect = DateTime.Now;
                }
            }
        }

        private void grvLotPlaning_rps_bte_label_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            GridView view = (GridView)this.grdLotPlaning.Views[0];

            if (view.FocusedRowHandle == GridControl.NewItemRowHandle || view.FocusedRowHandle == GridControl.InvalidRowHandle)
            {
                return;
            }

        //    if (this.lstMaterial == null)
                this.GetMaterial((string)this.btePARTY_ID.EditValue, (string)this.lueWarehouse.EditValue);

            var itemSel = from item in this.lstMaterial
                          where item.MTL_SEQ_NO == (string)view.GetFocusedRowCellValue("MTL_SEQ_NO")
                          select item;

            if (itemSel.Any())
            {



                //string mtlCode = view.GetFocusedRowCellDisplayText("MTL_CODE");
                //string mtlCode = view.GetFocusedRowCellDisplayText("DOC_INNER_QTY");
                //MessageBox.Show("Label Options");
                frmPOPLabelOption fLableOpt = new frmPOPLabelOption();

                fLableOpt.ARRIVAL_NO = (string)view.GetFocusedRowCellValue("ARRIVAL_NO");

                fLableOpt.LINE_NO = Convert.ToInt32(view.GetFocusedRowCellValue("LINE_NO"));

                fLableOpt.ITEM = itemSel.First<Material>();//(string)view.GetFocusedRowCellValue("ITEM_ID");

                fLableOpt.TOTAL_INNER_QTY = Convert.ToDecimal(view.GetFocusedRowCellValue("QTY"));

                fLableOpt.USER_ID = ((frmMainMenu)this.ParentForm).UserID;

                this.IsLabelGenerated = !((string)view.GetFocusedRowCellValue("GEN_LABEL_STATUS") == string.Empty);

                fLableOpt.IsLabelGenerated = this.IsLabelGenerated;

                DialogResult result = fLableOpt.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    view.SetFocusedRowCellValue("DOC_PKG_QTY", fLableOpt.No_Of_Label);
                    view.UpdateCurrentRow();


                    this.GetArrivalDtl(fLableOpt.ARRIVAL_NO);



                }

            }
            else
            {
                XtraMessageBox.Show(this, "Data not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void dteARRIVAL_DATE_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            DateEdit editor = (DateEdit)sender;
            if (string.IsNullOrEmpty(editor.Text))
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Arrival Date can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void grvLotPlaning_rps_dtpSHIFT_DATE_Validating(object sender, CancelEventArgs e)
        {

            GridView view = (GridView)this.grdLotPlaning.MainView;
            DateEdit editor = (DateEdit)sender;

            if (editor.Text == string.Empty)
            {
                //e.Cancel = true;
                //editor.Focus();
            }
            else
            {
                if (Convert.ToInt32(view.GetFocusedRowCellValue("FLAG"), NumberFormatInfo.CurrentInfo) != 2)
                    view.SetFocusedRowCellValue("FLAG", 3); //UPDATE

            }

        }

        private void grvLotPlaning_rps_txtRemark_Validating(object sender, CancelEventArgs e)
        {

            GridView view = (GridView)this.grdLotPlaning.MainView;
            TextEdit editor = sender as TextEdit;

            if (editor.Text == string.Empty)
            {
                //e.Cancel = true;
                //editor.Focus();
            }
            else
            {
                if (Convert.ToInt32(view.GetFocusedRowCellValue("FLAG"), NumberFormatInfo.CurrentInfo) != 2)
                    view.SetFocusedRowCellValue("FLAG", 3); //UPDATE

            }
        }

        private void grvLotPlaning_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.Caption == "Mtl. Code")
            {
                int val = Convert.ToInt32(this.grvLotPlaning.GetRowCellValue(e.RowHandle, "FLAG"));
                if ((val == 1) || (val == 3))
                {
                    RepositoryItemButtonEdit ritem = new RepositoryItemButtonEdit();
                    //ritem.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    ritem.ReadOnly = true;
                    //ritem.Buttons[0].Enabled = false;
                    ritem.Buttons[0].Visible = false;
                    e.RepositoryItem = ritem;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            bool result;
            string resultMsg = string.Empty;
            string arrNo = this.txtARRIVAL_NO.Text;

            using (ArrivalBLL docArrBll = new ArrivalBLL())
            {
                result = docArrBll.IsNumberOfPalateMaching(arrNo, out resultMsg);
            }

            if (!result) // false = not yet gen
            {

                this.GenerateReceivingLabel(this.txtARRIVAL_NO.Text,(string)this.lueWarehouse.EditValue, ((frmMainMenu)this.ParentForm).UserID);

                //this.btnPrintLabel.Enabled = true;

            }
            else  // true = already gen
            {
                XtraMessageBox.Show(this, "The label already generated!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
        }


        private void GenerateReceivingLabel(string arrNo, string wh_id,string userid)
        {
            string result = string.Empty;

            try
            {
                using (ArrivalBLL docArrBll = new ArrivalBLL())
                {
                    result = docArrBll.GenerateRecevingLabels(arrNo, wh_id,userid);
                }

                if (result.Equals("OK"))
                {

                    //DateTime? fromDt, toDt;

                    //if (this.dteFromDate.EditValue != null)
                    //    fromDt = this.dteFromDate.DateTime;
                    //else
                    //    fromDt = null;

                    //if (this.dteToDate.EditValue != null)
                    //    toDt = this.dteToDate.DateTime;
                    //else
                    //    toDt = null;

                    this.GetArrivalDtl(arrNo);

                    // NotifierResult.Show("Generate Complete", "Result", 50, 1000, 50, NotifyType.Safe);

                    XtraMessageBox.Show(this, "Generate Complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
            }
            catch (Exception ex)
            {
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
        }

        private void lueARR_TYPE_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Arrival type Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void btnUploadCSV_Click(object sender, EventArgs e)
        {
            DialogResult result;
            using (frmUploadCSV uploadCSV = new frmUploadCSV() { MASTER_FORM = eMaster_Form.FRM_ARRIVAL_RECEIVE, USER_ID = ((frmMainMenu)this.ParentForm).UserID })
            {
                result = UiUtility.ShowPopupForm(uploadCSV, this, true);
            }
        }

        private void bteDOC_NO_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string documentNo = string.Empty;
            using (frmLOVDocumentOrder fDocList = new frmLOVDocumentOrder())
            {
                fDocList.FormCalling = eFormCalling.fArrivalEntry;
                fDocList.PARTY_ID = this.btePARTY_ID.Text;

                fDocList.Arrival_GetPurchaseOrderList();

                result = UiUtility.ShowPopupForm(fDocList, this, true);
                documentNo = fDocList.DOC_NO;
            }

            if (result == DialogResult.OK)
            {
                editor.Text = documentNo;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            try
            {
                bool result;
                string resultMsg = string.Empty;
                string arrNo = this.txtARRIVAL_NO.Text;

                if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
                {
                    using (ArrivalBLL docArrBll = new ArrivalBLL())
                    {
                        result = docArrBll.IsArrivalReceiveComplete(arrNo, out resultMsg);
                    }

                    if (result)
                    {
                        this.PostArrivalReceiveToCSV(arrNo);
                    }
                    else  // true = already gen
                    {
                        XtraMessageBox.Show(this, resultMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Already Post to CSV File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnUnLockPost_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult isAuthen = DialogResult.None;

                using (frmCOFAuthen fAuthen = new frmCOFAuthen())
                {
                    isAuthen = UiUtility.ShowPopupForm(fAuthen, this, true);
                }

                if (isAuthen == DialogResult.OK)
                {
                    DialogResult result = XtraMessageBox.Show(this, "Do you want to unlock shipping order?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        this.UnlockAlreadyPostCSV(this.txtARRIVAL_NO.Text);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "You do not have the right to unlock already post shipping order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnPO_List_Click(object sender, EventArgs e)
        {
            DialogResult result;
            using (frmDocListInfo docListInf = new frmDocListInfo())
            {
                docListInf.Doc_GetPurchaseOrderList();
                result = UiUtility.ShowPopupForm(docListInf, this, true);
            }
        }

        
        
    }
}