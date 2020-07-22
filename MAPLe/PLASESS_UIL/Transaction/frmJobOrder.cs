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
using HTN.BITS.UIL.PLASESS.Component.CSV;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmJobOrder : BaseChildForm
    {
        private bool isJobRun = false;

        public frmJobOrder()
        {
            InitializeComponent();

            this.CustomInitializeComponent();

            base.LoadFormLayout();

            base.LoadGridLayout(this.grdJobOrder);
            this.chkSelect = new GridCheckMarksSelection(this.grvJobOrder);
            this.chkSelect.ClearSelection();


            base.LoadGridLayout(this.grdLotPlaning);
            this.lotSelect = new GridCheckMarksSelection(this.grvLotPlaning);
            this.lotSelect.ClearSelection();
            this.grvLotPlaning.Columns["CheckMarkSelection"].OptionsColumn.AllowFocus = false;

            this.InitializaLOVData();
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
                return string.Format("JobOrder_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private string FileName_Detail
        {
            get
            {
                return string.Format("JobLot_{0:yyyyMMddHHmmss}", DateTime.Now);
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
                
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName_Detail + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;

                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName_Detail + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName_Detail + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName_Detail + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName_Detail + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdJobOrder.Views[0] as GridView;
                string[] columnNoExp = new string[2];
                columnNoExp[0] = (view.Columns["CheckMarkSelection"] != null ? "CheckMarkSelection" : string.Empty);
                columnNoExp[1] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.HTML, this.FileName + ".html", columnNoExp);
            }
            else
            {
                //detail
                GridView view = this.grdLotPlaning.Views[0] as GridView;

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
                GridView view = (GridView)this.grdLotPlaning.Views[0];

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false, 0);

                        this.dntJobOrder.Enabled = true;

                        this.dntJobOrder.TextStringFormat = "      Add Mode      ";
                        this.dntJobOrder.Enabled = false;

                        this.btnPostData.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnExit.Enabled = false;

                        this.btnEditLotPlan.Enabled = false;
                        this.ddbPrint.Enabled = false;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false, view.RowCount);

                        this.dntJobOrder.Enabled = true;

                        this.dntJobOrder.TextStringFormat = "      Edit Mode      ";
                        this.dntJobOrder.Enabled = false;

                        this.btnPostData.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnExit.Enabled = false;

                        this.btnEditLotPlan.Enabled = false;
                        this.ddbPrint.Enabled = false;

                        view.ClearColumnsFilter();
                        this.GridDetail_OptionsCustomization(view, false);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true, 0);

                        this.dntJobOrder.TextStringFormat = " Record {0} of {1} ";
                        this.dntJobOrder.Enabled = true;

                        this.btnPostData.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";
                        this.btnExit.Enabled = true;

                        this.btnEditLotPlan.Enabled = true;
                        this.ddbPrint.Enabled = true;

                        
                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);
                        UiUtility.SetGridReadOnly(view, true);

                        this.GridDetail_OptionsCustomization(view, true);

                        break;
                    default:
                        break;
                }

                
                this.btnSaveLotPlan.Enabled = false;
                this.btn02Cancel.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeControlState(bool state, int lotCount)
        {

            if (lotCount > 0)
            {
           
                this.btePARTY_ID.Properties.ReadOnly = true;
                this.btePARTY_ID.Properties.Buttons[0].Enabled = false;

                this.luePRODUCTION_TYPE.Properties.ReadOnly = true;

                this.btePROD_SEQ_NO.Properties.ReadOnly = true;
                this.btePROD_SEQ_NO.Properties.Buttons[0].Enabled = false;

                this.icbREC_STAT.Properties.ReadOnly = true;
            }
            else
            {
                this.btePARTY_ID.Properties.ReadOnly = state;
                this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;

                this.luePRODUCTION_TYPE.Properties.ReadOnly = state;

                

                this.btePROD_SEQ_NO.Properties.ReadOnly = state;
                this.btePROD_SEQ_NO.Properties.Buttons[0].Enabled = !state;

                this.icbREC_STAT.Properties.ReadOnly = state;
            }

            //this.btePARTY_ID.Properties.ReadOnly = state;
            //this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;

            this.bteMC_NO.Properties.ReadOnly = state;
            this.bteMC_NO.Properties.Buttons[0].Enabled = !state;
            
            this.txtREF_NO.Properties.ReadOnly = state;
            this.dtpPRODUCTION_DATE.Properties.ReadOnly = state;
            this.dtpMP_START_DATE.Properties.ReadOnly = state;
            this.dtpMP_STOP_DATE.Properties.ReadOnly = state;
            this.txtPLAN_DAY.Properties.ReadOnly = true;
            this.txtPLAN_QTY.Properties.ReadOnly = state;
            this.lueUNIT.Properties.ReadOnly = state; ;
            this.txtREMARK.Properties.ReadOnly = state;
            
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
                this.txtJOB_NO.Text = string.Empty; ;
                this.txtJOB_DATE.EditValue = DateTime.Now;

                this.btePARTY_ID.EditValue = null;
                this.txtPARTY_NAME.Text = string.Empty;

                this.txtREF_NO.Text = string.Empty;
                this.dtpPRODUCTION_DATE.EditValue = DateTime.Now;
                this.luePRODUCTION_TYPE.EditValue = null;
                this.bteMC_NO.EditValue = null;
                this.lblPROD_SEQ_NO_Value.Text = string.Empty;
                this.btePROD_SEQ_NO.EditValue = null;
                this.txtPRODUCT_NAME.Text = string.Empty;
                this.txtMATERIAL_NAME.Text = string.Empty;

                this.dtpMP_START_DATE.EditValue = DateTime.Now;
                this.dtpMP_STOP_DATE.EditValue = DateTime.Now;
                //this.lblStandardQty.Text = "0";
                this.txtStandardQty.EditValue = 0;
                this.txtPLAN_DAY.EditValue = null;
                this.txtPLAN_QTY.EditValue = null;
                this.lueUNIT.EditValue = "PCS";
                this.txtREMARK.Text = string.Empty;
                this.icbREC_STAT.EditValue = true;

                this.GetJobLotPlanning(string.Empty);

                //clear Gridview
                this.dtbLotPlaning.Rows.Clear();
                this.dtbLotPlaning.AcceptChanges();
                this.grdLotPlaning.DataSource = dtbLotPlaning;
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
                using (ProductBLL pdBll = new ProductBLL())
                {
                    List<Unit> lstUnit = pdBll.GetUnitList();
                    if (lstUnit != null)
                    {
                        this.lueUNIT.Properties.DataSource = lstUnit;
                        if (lstUnit.Count > 0)
                        {
                            Unit unitTemp = lstUnit.Find(delegate(Unit _unit)
                            {
                                return _unit.SEQ_NO == "PCS";
                            });
                            if (unitTemp != null)
                            {
                                this.lueUNIT.EditValue = unitTemp.SEQ_NO;
                            }
                            else
                            {
                                //default
                                this.lueUNIT.EditValue = lstUnit[0].SEQ_NO;
                            }
                        }
                    }
                }
                using (MachineBLL mcBll = new MachineBLL())
                {
                    this.lstMachine = mcBll.GetMachineList(string.Empty);
                }

                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                    this.lstProductionType = pdtBll.GetProductionTypeList();
                }

                this.grvJobOrder_rps_MACHINE.DataSource = this.lstMachine;
                this.grvJobOrder_rps_PRODUCTION_TYPE.DataSource = this.lstProductionType;
                this.luePRODUCTION_TYPE.Properties.DataSource = this.lstProductionType;
                this.lueSearchPROD_TYPE.Properties.DataSource = this.lstProductionType;
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

        private void GetProductBySeq(string prodSeq, string partyid, string prodType)
        {
            try
            {
                using (ProductBLL prodBll = new ProductBLL())
                {
                    Product prod = prodBll.LovProduct(prodType, partyid, prodSeq);
                    if (prod != null)
                    {
                        this.btePROD_SEQ_NO.EditValue = prod.PRODUCT_NO;
                        this.txtPRODUCT_NAME.Text = prod.PRODUCT_NAME;
                        this.txtMATERIAL_NAME.Text = prod.MATERIAL_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        //private bool GetProductByNo(string prodNo, string prodType)
        //{
        //    bool result = false;
        //    try
        //    {
        //        using (ProductBLL prodBll = new ProductBLL())
        //        {
        //            Product prod = prodBll.GetProductNo(prodNo, prodType);
        //            if (prod != null)
        //            {
        //                this.lblPROD_SEQ_NO_Value.Text = prod.PROD_SEQ_NO;
        //                this.txtPRODUCT_NAME.Text = prod.PRODUCT_NAME;
        //                this.txtMATERIAL_NAME.Text = prod.MATERIAL_NAME;

        //                result = true;
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(this.lblPROD_SEQ_NO_Value.Text))
        //        {
        //            //get box qty
        //            using (InfoBLL info = new InfoBLL())
        //            {
        //                //this.lblStandardQty.Text = info.ProductBoxQty(this.lblPROD_SEQ_NO_Value.Text).ToString("#,##0");
        //                this.txtStandardQty.EditValue = info.ProductBoxQty(this.lblPROD_SEQ_NO_Value.Text);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //        result = false;
        //    }
        //    finally
        //    {
        //    }

        //    return result;
        //}

        public void GetJobOrderList(string type, string findValue,DateTime? startDate, DateTime? toDate)
        {
            List<JobOrder> lstJobOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    lstJobOrd = jobOrdBll.GetJobOrderList(type, findValue, startDate, toDate);
                }

                //DataTable dtJobOrder = UiUtility.BuildDataTable<JobOrder>(lstJobOrd);
                //dtJobOrder.DefaultView.Sort = "JOB_DATE DESC";

                this.grdJobOrder.DataSource = lstJobOrd; //dtJobOrder;  //
                this.dntJobOrder.DataSource = lstJobOrd; //dtJobOrder;  //

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

        public void GetBindingJobOrder(string jobOrdNo)
        {
            JobOrder jobOrd = null;
            try
            {
                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    jobOrd = jobOrdBll.GetJobOrder(jobOrdNo);
                }

                if (jobOrd != null)
                {
                    base.ClearValidControls(this, ref this.dxErrorProvider1);
                    this.txtJOB_NO.Text = jobOrd.JOB_NO;
                    this.txtJOB_DATE.EditValue = jobOrd.JOB_DATE;

                    this.btePARTY_ID.EditValue = jobOrd.PARTY_ID;
                    this.GetCustomerByCode(jobOrd.PARTY_ID);

                    this.txtREF_NO.Text = jobOrd.REF_NO;
                    this.dtpPRODUCTION_DATE.EditValue = jobOrd.PRODUCTION_DATE;
                    this.luePRODUCTION_TYPE.EditValue = jobOrd.PROD_TYPE;
                    this.lblMC_NO_VALUE.Text = jobOrd.MC_NO;
                    this.bteMC_NO.EditValue = this.MachineName(jobOrd.MC_NO);
                    this.lblPROD_SEQ_NO_Value.Text = jobOrd.PROD_SEQ_NO;
                    //get box qty
                    using (InfoBLL info = new InfoBLL())
                    {
                        //this.lblStandardQty.Text = info.ProductBoxQty(jobOrd.PROD_SEQ_NO).ToString("#,##0");
                        this.txtStandardQty.EditValue = info.ProductBoxQty(jobOrd.PROD_SEQ_NO);
                    }

                    this.GetProductBySeq(jobOrd.PROD_SEQ_NO, jobOrd.PARTY_ID, jobOrd.PROD_TYPE);

                    if (jobOrd.MP_START_DATE.HasValue)
                    {
                        this.dtpMP_START_DATE.EditValue = jobOrd.MP_START_DATE.Value;
                    }
                    else
                    {
                        this.dtpMP_START_DATE.EditValue = null;
                    }

                    if (jobOrd.MP_STOP_DATE.HasValue)
                    {
                        this.dtpMP_STOP_DATE.EditValue = jobOrd.MP_STOP_DATE.Value;
                    }
                    else
                    {
                        this.dtpMP_STOP_DATE.EditValue = null;
                    }

                    this.txtPLAN_DAY.EditValue = jobOrd.PLAN_DAY;
                    this.txtPLAN_QTY.EditValue = jobOrd.PLAN_QTY;
                    this.lueUNIT.EditValue = jobOrd.UNIT_ID;
                    this.txtREMARK.Text = jobOrd.REMARK;
                    this.icbREC_STAT.EditValue = jobOrd.REC_STAT;

                    //call job lot planning
                    this.GetJobLotPlanning(this.txtJOB_NO.Text);

                    //Change by jack 04-Feb-2011
                    //No need to generate barcode image into database 
                    //performent not good 
                    //this.GenerateSerialNo(this.txtJOB_NO.Text);

                    this.CalculateSummary();
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

        public void AdvanceSearchJobOrder(string jobOrdNo, string type, DateTime? startDate, DateTime? toDate)
        {
            List<JobOrder> lstJobOrd = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    lstJobOrd = jobOrdBll.AdvJobOrder(jobOrdNo, type, startDate, toDate);
                }

                this.grdJobOrder.DataSource = lstJobOrd;
                this.dntJobOrder.DataSource = lstJobOrd;

                this.chkSelect.ClearSelection();

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool IsFormValidated()
        {
            //Check control empty
            if (string.IsNullOrEmpty(this.btePARTY_ID.Text))
            {
                this.dxErrorProvider1.SetError(this.btePARTY_ID, "Party ID can't be Empty");
                this.btePARTY_ID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtREF_NO.Text))
            {
                this.dxErrorProvider1.SetError(this.txtREF_NO, "Ref. No. can't be Empty");
                this.txtREF_NO.Focus();
                return false;
            }

            if (this.luePRODUCTION_TYPE.EditValue==null)
            {
                this.dxErrorProvider1.SetError(this.luePRODUCTION_TYPE, "Production Type can't be Empty");
                this.luePRODUCTION_TYPE.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.bteMC_NO.Text))
            {
                this.dxErrorProvider1.SetError(this.bteMC_NO, "Machine can't be Empty");
                this.bteMC_NO.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.btePROD_SEQ_NO.Text))
            {
                this.dxErrorProvider1.SetError(this.btePROD_SEQ_NO, "Product can't be Empty");
                this.btePROD_SEQ_NO.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtPLAN_QTY.Text))
            {
                this.dxErrorProvider1.SetError(this.txtPLAN_QTY, "Qty can't be Empty");
                this.txtPLAN_QTY.Focus();
                return false;
            }

            if (this.lueUNIT.EditValue == null)
            {
                this.dxErrorProvider1.SetError(this.lueUNIT, "Unit can't be Empty");
                this.lueUNIT.Focus();
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

        public void InsertJobOrder()
        {
            string result = string.Empty;
            JobOrder jobOrd = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                jobOrd = new JobOrder();

                jobOrd.JOB_NO = this.txtJOB_NO.Text;
                jobOrd.JOB_DATE = DateTime.Now;
                jobOrd.PROD_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;
                jobOrd.REF_NO = this.txtREF_NO.Text;
                jobOrd.PRODUCTION_DATE = this.dtpPRODUCTION_DATE.DateTime;
                jobOrd.MC_NO = this.lblMC_NO_VALUE.Text;
                jobOrd.PROD_SEQ_NO = this.lblPROD_SEQ_NO_Value.Text;
                jobOrd.PARTY_ID = this.btePARTY_ID.Text;
                if(this.dtpMP_START_DATE.EditValue != null)
                    jobOrd.MP_START_DATE = this.dtpMP_START_DATE.DateTime;
                if (this.dtpMP_STOP_DATE.EditValue != null)
                    jobOrd.MP_STOP_DATE = this.dtpMP_STOP_DATE.DateTime;

                jobOrd.PLAN_DAY = Convert.ToInt32(this.txtPLAN_DAY.EditValue, NumberFormatInfo.InvariantInfo);
                jobOrd.PLAN_QTY = Convert.ToInt32(this.txtPLAN_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                jobOrd.UNIT_ID = this.lueUNIT.EditValue.ToString();
                jobOrd.REMARK = this.txtREMARK.Text;
                jobOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    result = jobOrdBll.InsertJobOrder(ref jobOrd, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    this.txtRemainQty.EditValue = jobOrd.PLAN_QTY;
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
                this.txtJOB_NO.Text = jobOrd.JOB_NO;
                this.txtJOB_DATE.EditValue = jobOrd.JOB_DATE;
                this.FormState = eFormState.ReadOnly;

                this.txtSearch.Text = jobOrd.JOB_NO;
                this.lueSearchPROD_TYPE.EditValue = null;

                this.GetJobOrderList(string.Empty, jobOrd.JOB_NO, null, null);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdJobOrder.Views[0];

                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "JOB_NO", jobOrd.JOB_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntJobOrder.Position = position;
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

        public void UpdateJobOrder()
        {
            string result = string.Empty;
            JobOrder jobOrd = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                jobOrd = new JobOrder();

                jobOrd.JOB_NO = this.txtJOB_NO.Text;
                jobOrd.JOB_DATE = Convert.ToDateTime(this.txtJOB_DATE.EditValue, DateTimeFormatInfo.CurrentInfo);
                jobOrd.PROD_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;
                jobOrd.REF_NO = this.txtREF_NO.Text;
                jobOrd.PRODUCTION_DATE = this.dtpPRODUCTION_DATE.DateTime;
                jobOrd.MC_NO = this.lblMC_NO_VALUE.Text;
                jobOrd.PROD_SEQ_NO = this.lblPROD_SEQ_NO_Value.Text;
                jobOrd.PARTY_ID = this.btePARTY_ID.Text;
                if (this.dtpMP_START_DATE.EditValue != null)
                    jobOrd.MP_START_DATE = this.dtpMP_START_DATE.DateTime;
                if (this.dtpMP_STOP_DATE.EditValue != null)
                    jobOrd.MP_STOP_DATE = this.dtpMP_STOP_DATE.DateTime;

                jobOrd.PLAN_DAY = Convert.ToInt32(this.txtPLAN_DAY.EditValue, NumberFormatInfo.InvariantInfo);
                jobOrd.PLAN_QTY = Convert.ToInt32(this.txtPLAN_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                jobOrd.UNIT_ID = this.lueUNIT.EditValue.ToString();
                jobOrd.REMARK = this.txtREMARK.Text;
                jobOrd.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    result = jobOrdBll.UpdateJobOrder(jobOrd, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdJobOrder.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("JOB_NO", jobOrd.JOB_NO);
                    view.SetFocusedRowCellValue("PROD_TYPE", jobOrd.PROD_TYPE);
                    view.SetFocusedRowCellValue("PRODUCTION_DATE", jobOrd.PRODUCTION_DATE);
                    view.SetFocusedRowCellValue("MC_NO", jobOrd.MC_NO);
                    view.SetFocusedRowCellValue("PROD_SEQ_NO", jobOrd.PROD_SEQ_NO);
                    view.SetFocusedRowCellValue("PARTY_ID", jobOrd.PARTY_ID);
                    view.SetFocusedRowCellValue("MP_START_DATE", jobOrd.MP_START_DATE);
                    view.SetFocusedRowCellValue("MP_STOP_DATE", jobOrd.MP_STOP_DATE);
                    view.SetFocusedRowCellValue("PLAN_QTY", jobOrd.PLAN_QTY);
                    view.SetFocusedRowCellValue("REC_STAT", jobOrd.REC_STAT);

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
                
                this.FormState = eFormState.ReadOnly;

                ////Get all Invoice on Invoice List
                //this.GetJobOrderList(this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);

                //if (result.Equals("OK"))
                //{
                //    GridView viewList = (GridView)this.grdJobOrder.Views[0];

                //    int position = UiUtility.GetRowHandleByColumnValue(viewList, "JOB_NO", jobOrd.JOB_NO);
                //    if (position != 0)
                //    {
                //        if (position != GridControl.InvalidRowHandle)
                //        {
                //            this.dntJobOrder.Position = position;
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

        //public void UpdateJobLotPlaning()
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;

        //        List<JobLotPlan> lstJobLot = new List<JobLotPlan>();
        //        JobLotPlan jLotPlan;

        //        //Check Delete Recore
        //        int flag = 0;

        //        foreach (DataRow dr in this.dtbLotPlaning.Rows)
        //        {
        //            flag = Convert.ToInt32(dr["FLAG"].ToString());
        //            if (flag == 2) //new record
        //            {
        //                jLotPlan = new JobLotPlan();

        //                jLotPlan.JOB_NO = dr["JOB_NO"].ToString();
        //                jLotPlan.LINE_NO = Convert.ToInt32(dr["LINE_NO"], NumberFormatInfo.InvariantInfo);
        //                jLotPlan.SHIFT_ID = dr["SHIFT_ID"].ToString();
        //                jLotPlan.SHIFT_DATE = (DateTime)dr["SHIFT_DATE"];
        //                jLotPlan.NO_OF_LABEL = Convert.ToInt32(dr["NO_OF_LABEL"], NumberFormatInfo.InvariantInfo);
        //                jLotPlan.QTY_PER_LABEL = Convert.ToInt32(dr["QTY_PER_LABEL"], NumberFormatInfo.InvariantInfo);
        //                jLotPlan.FLAG = flag; //add new only

        //                lstJobLot.Add(jLotPlan);
        //            }
        //        }



        //        if (lstJobLot.Count != 0)
        //        {
        //            using (JobOrderBLL jOrdBll = new JobOrderBLL())
        //            {
        //                result = jOrdBll.UpdateJobLotPlaning(lstJobLot, ((frmMainMenu)this.ParentForm).UserID);
        //            }

        //            if (!result.Equals("OK"))
        //            {
        //                XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //            }
        //            else
        //            {
        //                NotifierResult.Show("Update Lot Planing Complete", "Result", 50, 1000, 50, NotifyType.Safe);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        public void UpdateJobLotPlant_Bulk(string prodType, string prodSeq)
        {
            string result = string.Empty;

            try
            {
                this.dtbLotPlaning.AcceptChanges();

                this.Cursor = Cursors.WaitCursor;

                using (JobOrderBLL jOrdBll = new JobOrderBLL())
                {
                    result = jOrdBll.UpdateJobLotPlaning(prodType, prodSeq, this.dtbLotPlaning, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (!result.Equals("OK"))
                {
                    XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    NotifierResult.Show("Update Lot Planing Complete", "Result", 50, 1000, 50, NotifyType.Safe);
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

        public void GetJobLotPlanning(string jobOrdNo)
        {
            try
            {
                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    this.dtbLotPlaning = jobOrdBll.GetJobLotPlaning(jobOrdNo);
                }

                if (this.dtbLotPlaning != null)
                {
                    this.lotSelect.ClearSelection();
                    this.grdLotPlaning.DataSource = this.dtbLotPlaning;

                    GridView view = (GridView)this.grdLotPlaning.Views[0];
                    UiUtility.SetGridReadOnly(view, true);

                    //this.lotSelect.View.Columns["CheckMarkSelection"].VisibleIndex = 0;
                    //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //Custom Method
        private void DeleteLotPlanning(GridView view, int rowSelect)
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
                        flag = Convert.ToInt32(row["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag.Equals(2))
                            row.Delete();
                        else
                            row["FLAG"] = 0;
                    }
                }
            }
            finally
            {
                view.EndSort();
                this.dtbLotPlaning.AcceptChanges();

                this.CalculateSummary();

                DataView dtv = new DataView(this.dtbLotPlaning);
                dtv.RowFilter = "[FLAG] <> 0";

                this.grdLotPlaning.DataSource = dtv;
            }
        }

        //public void GenerateSerialNo(string jobOrdNo)
        //{
        //    try
        //    {
        //        using (JobOrderBLL jobOrdBll = new JobOrderBLL())
        //        {
        //            jobOrdBll.GenerateSerialNo(jobOrdNo);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        public void CalculateSummary()
        {
            GridView view = (GridView)this.grdLotPlaning.Views[0]; //this.gridView2
            view.UpdateCurrentRow();

            int totalJobPlanQty = Convert.ToInt32(this.txtPLAN_QTY.EditValue, NumberFormatInfo.InvariantInfo);
            int totalLotPlanQty = Convert.ToInt32(view.Columns["TOTAL_QTY"].SummaryItem.SummaryValue, NumberFormatInfo.InvariantInfo);

            this.txtRemainQty.EditValue = totalJobPlanQty - totalLotPlanQty;
        }

        private void PrintProductCard()
        {
            int totalQty = 0;
            List<JobLotPlan> lstJLotPlan = null;
            JobLotPlan jLotPlan;
            try
            {
                if (this.lotSelect.SelectedCount > 0)
                {
                    lstJLotPlan = new List<JobLotPlan>(this.lotSelect.SelectedCount);

                    for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                    {
                        jLotPlan = new JobLotPlan();
                        DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                        jLotPlan.JOB_NO = row.Row["JOB_NO"].ToString();
                        jLotPlan.LINE_NO = Convert.ToInt32(row.Row["LINE_NO"], NumberFormatInfo.InvariantInfo);

                        totalQty += Convert.ToInt32(row.Row["TOTAL_QTY"], NumberFormatInfo.InvariantInfo);

                        lstJLotPlan.Add(jLotPlan);
                    }

                    int seq = PrintingBuilder.Instance.GeneratePrintSEQ();

                    using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                    {
                        jobOrdBll.InsertLotPlaningToPrint(seq, lstJLotPlan);
                    }

                    using (frmPrintProductCard fPrintCard = new frmPrintProductCard())
                    {
                        fPrintCard.JOB_NO = this.txtJOB_NO.Text;
                        fPrintCard.PROD_SEQ_NO = this.lblPROD_SEQ_NO_Value.Text;
                        fPrintCard.PRODUCT_NO = this.btePROD_SEQ_NO.Text;
                        fPrintCard.PRODUCT_NAME = this.txtPRODUCT_NAME.Text;
                        fPrintCard.TOTAL_QTY = totalQty;
                        fPrintCard.SEQ_NO = seq;
                        fPrintCard.PRODUCTION_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;

                        fPrintCard.USER_ID = ((frmMainMenu)this.ParentForm).UserID;

                        UiUtility.ShowPopupForm(fPrintCard, this, true);
                    }

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Lot Plan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void PrintJobOrder(List<JobOrder> lstJobOrd)
        {
            string userid = string.Empty;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                userid = ((frmMainMenu)this.ParentForm).UserID;
                DataSet ds;

                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    ds = jobOrdBll.PrintJobOrderReport(lstJobOrd, userid);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;
                

                RPT_JOB_ORDER rpt = new RPT_JOB_ORDER();
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

        private void PostJobOrderToWorkTicket()
        {
            List<JobLotPlan> lstJLotPlan = null;
            JobLotPlan jLotPlan;
            try
            {
                if (this.lotSelect.SelectedCount > 0)
                {
                    lstJLotPlan = new List<JobLotPlan>(this.lotSelect.SelectedCount);

                    for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                    {
                        jLotPlan = new JobLotPlan();
                        DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                        jLotPlan.JOB_NO = row.Row["JOB_NO"].ToString();
                        jLotPlan.SHIFT_DATE = (DateTime?)row.Row["SHIFT_DATE"];
                        jLotPlan.SHIFT_ID = row.Row["SHIFT_ID"].ToString();

                        lstJLotPlan.Add(jLotPlan);
                    }

                    int seq = PrintingBuilder.Instance.GeneratePrintSEQ();

                    using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                    {
                        jobOrdBll.InsertLotPlaningToPostCSV(seq, lstJLotPlan);
                    }

                    this.PostWorkTicketToCSV(this.txtJOB_NO.Text, seq);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Lot Plan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void PostWorkTicketToCSV(string jobno, int seq)
        {
            try
            {
                string prodType = (string)this.luePRODUCTION_TYPE.EditValue;
                string seqno = string.Empty;
                DataTable dtbPosted = null;
                string fullFileName = string.Empty;
                string fileName = string.Format("{1}-JOB-WORKTKT-{0:yyyyMMddHHmm}.CSV", DateTime.Now, prodType);

                using (SaveFileDialog fdlg = new SaveFileDialog { Title = "Save Export CSV File", Filter = "CSV files (*.csv)|*.csv", FilterIndex = 1, RestoreDirectory = true, FileName = fileName })
                {
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        fullFileName = fdlg.FileName;


                        using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                        {
                            dtbPosted = inDataBll.PostWorkTicketInterface(seq, ((frmMainMenu)this.ParentForm).UserID, out seqno);
                        }

                        if (dtbPosted != null)
                        {
                            DataTable dtCsv = new DataTable();
                            dtCsv.Columns.Add("Assembly ID");
                            dtCsv.Columns.Add("Assembly Description");
                            dtCsv.Columns.Add("Assembly Revision Number");
                            dtCsv.Columns.Add("Work Ticket No.");
                            dtCsv.Columns.Add("Date");
                            dtCsv.Columns.Add("Closed");
                            dtCsv.Columns.Add("Qty to Build");
                            dtCsv.Columns.Add("Component Item ID");
                            dtCsv.Columns.Add("Qty Required");
                            dtCsv.Columns.Add("Finished");

                            foreach (DataRow row in dtbPosted.Rows)
                            {
                                dtCsv.Rows.Add(
                                    row.ItemArray[0], // 1. Assembly ID
                                    row.ItemArray[1], // 2. Assembly Description
                                    row.ItemArray[2], // 3. Assembly Revision Number
                                    row.ItemArray[3], // 4. Work Ticket No.
                                    row.ItemArray[4], // 5. Date
                                    row.ItemArray[5], // 6. Closed
                                    row.ItemArray[6], // 7. Qty to Build
                                    row.ItemArray[7], // 8. Component Item ID
                                    "1",              // 9. Qty Required  row.ItemArray[8]
                                    row.ItemArray[9]  //10. Number of Distributions 
                                    );
                            }

                            if (!string.IsNullOrEmpty(fullFileName))
                            {
                                using (CsvWriter writer = new CsvWriter() { Request_Header = true, Spliter = "," })
                                {
                                    writer.WriteCsv(dtCsv, fullFileName);
                                }
                            }

                            using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                            {
                                jobOrdBll.UpdatePostCSV(jobno, seq, ((frmMainMenu)this.ParentForm).UserID);
                            }

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

            if (!isJobRun)
            {
                isJobRun = false;

                this.xtpJobOrderDetail.PageVisible = false;
                
            }

            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();
            this.GetJobOrderList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            this.FormState = eFormState.ReadOnly;
            


            
            //test show column customization.
            //GridView view = this.grdJobOrder.Views[0] as GridView;
            //view.ShowCustomization();
        }

        public void OpenJobNo(string jobno)
        {
            
            isJobRun = true;

            this.Validate();

            if (!string.IsNullOrEmpty(jobno))
            {
                //this.InitializaLOVData();
                //this.btnApply.PerformClick();
                this.btnAddNew.Visible = false;

                //Change tab view.
                this.xtpJobOrderList.PageEnabled = false;

                this.xtpJobOrderDetail.PageVisible = true;
                this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderDetail;

                //Call record detail.
                this.GetBindingJobOrder(jobno);

                this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                this.dntJobOrder.Focus();
            }
 
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
                fCustList.PARTY_TYPE = "C"; //find only Customer
                //result = UiUtility.ShowPopupForm(fCustList, this, true);
                result = fCustList.ShowDialog(this);

                partyid = fCustList.PARTY_ID;
                partyname = fCustList.PARTY_NAME;
            }

            if (result == DialogResult.OK)
            {
                btnEdit.Text = partyid;
                this.txtPARTY_NAME.Text = partyname;

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
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Value can't be null", ErrorType.Critical);
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

        private void bteMC_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            try
            {
                //Open Popup For Select Supplier.
                DialogResult result;
                string mcno = string.Empty;
                string mcname = string.Empty;
                using (frmLOVMachine fMcList = new frmLOVMachine())
                {
                    object value = this.luePRODUCTION_TYPE.EditValue;
                    if (this.luePRODUCTION_TYPE.EditValue != null)
                    {
                        fMcList.MACHINE_TYPE = value.ToString();
                    }
                    else
                    {
                        fMcList.MACHINE_TYPE = string.Empty;
                    }

                    result = UiUtility.ShowPopupForm(fMcList, this, true);

                    mcname = fMcList.MACHINE_NAME;
                    mcno = fMcList.MC_NO;
                }

                if (result == DialogResult.OK)
                {
                    btnEdit.Text = mcname;
                    this.lblMC_NO_VALUE.Text = mcno;

                    this.btePROD_SEQ_NO.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btePROD_SEQ_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string productno = string.Empty;
            using (frmLOVProduct fPrdList = new frmLOVProduct())
            {
                fPrdList.FormCalling = eFormCalling.fJobOrder;
                fPrdList.PARTY_ID = this.btePARTY_ID.Text;

                if(this.luePRODUCTION_TYPE.EditValue != null)
                {
                    fPrdList.PRODUCTION_TYPE = this.luePRODUCTION_TYPE.EditValue.ToString();
                }
                else
                {
                    this.luePRODUCTION_TYPE.EditValue = "V";
                    fPrdList.PRODUCTION_TYPE = "V";
                }

                fPrdList.JobOrder_GetProductList();

                result = UiUtility.ShowPopupForm(fPrdList, this, true);
                productno = fPrdList.PRODUCT_NO;
            }

            if (result == DialogResult.OK)
            {
                editor.Text = productno;
                SendKeys.Send("{TAB}");
            }
        }

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
                    string jobOrdNo = view.GetRowCellValue(info.RowHandle, "JOB_NO").ToString();

                    this.btnAddNew.Visible = false;

                    //Change tab view.
                    this.xtpJobOrderList.PageEnabled = false;

                    this.xtpJobOrderDetail.PageVisible = true;
                    this.xtcJobOrder.SelectedTabPage = this.xtpJobOrderDetail;

                    //Call record detail.
                    this.GetBindingJobOrder(jobOrdNo);

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
                GridView view = (GridView)this.grdJobOrder.Views[0]; //this.gridView2

                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderDetail)
                {
                    string jobOrdNo = view.GetFocusedRowCellValue("JOB_NO").ToString();

                    this.GetBindingJobOrder(jobOrdNo);
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
            this.btePARTY_ID.Focus();
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

                        this.GetBindingJobOrder(this.txtJOB_NO.Text);

                        

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

        private void btnEditLotPlan_Click(object sender, EventArgs e)
        {
            this.btngenCard.Enabled = false;
            this.btnEditLotPlan.Enabled = false;
            this.btnDelete.Enabled = true;
            this.btnSaveLotPlan.Enabled = true;
            this.btn02Cancel.Enabled = true;

            this.btnPostData.Enabled = false;

            this.ddbPrint.Enabled = false;

            try
            {

                GridView view = (GridView)this.grdLotPlaning.Views[0];

                //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = false;
              
                UiUtility.SetGridReadOnly(view, false);
               
                UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);

                this.grdLotPlaning.Focus();

                
                view.FocusedRowHandle = GridControl.NewItemRowHandle;

                if (this.dtbLotPlaning.Rows.Count == 0)
                {
                    view.SetFocusedRowCellValue("SHIFT_DATE", this.dtpMP_START_DATE.DateTime);
                }
                
                view.FocusedColumn = view.VisibleColumns[2];

                view.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void btnSaveLotPlan_Click(object sender, EventArgs e)
        {
            try
            {
                
                GridView view = (GridView)this.grdLotPlaning.Views[0];
                UiUtility.SetGridReadOnly(view, true);

                //this.lotSelect.View.Columns["CheckMarkSelection"].VisibleIndex = 0;
                //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = true;

                //this.UpdateJobLotPlaning();

                this.UpdateJobLotPlant_Bulk((string)this.luePRODUCTION_TYPE.EditValue, this.lblPROD_SEQ_NO_Value.Text);


                //call job lot planning
                this.GetJobLotPlanning(this.txtJOB_NO.Text);

                //Change by jack 04-Feb-2011
                //No need to generate barcode image into database 
                //performent not good 
                //this.GenerateSerialNo(this.txtJOB_NO.Text);

                this.CalculateSummary();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.btnEditLotPlan.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnSaveLotPlan.Enabled = false;
                this.btn02Cancel.Enabled = false;

                this.btnPostData.Enabled = true;

                this.btngenCard.Enabled = true;

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

            GridView view = (GridView)this.grdLotPlaning.Views[0];
            UiUtility.SetGridReadOnly(view, true);

            //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = false;

            this.btnAddNew.Visible = false;
            this.btePARTY_ID.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;

            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertJobOrder();
                    break;
                case eFormState.Edit:
                    this.UpdateJobOrder();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void dtpMP_START_DATE_Validating(object sender, CancelEventArgs e)
        {
            DateEdit dateEdit = (DateEdit)sender;
            //int leadTime = this.dtpMP_STOP_DATE.DateTime.Day - dateEdit.DateTime.Day;

            double leadTime = (dateEdit.DateTime.Date - this.dtpMP_START_DATE.DateTime.Date).TotalDays;

            this.txtPLAN_DAY.EditValue = leadTime + 1;
        }

        private void dtpMP_STOP_DATE_Validating(object sender, CancelEventArgs e)
        {
            DateEdit dateEdit = (DateEdit)sender;

            //int leadTime = dateEdit.DateTime.Day - this.dtpMP_START_DATE.DateTime.Day;

            double leadTime = (dateEdit.DateTime.Date - this.dtpMP_START_DATE.DateTime.Date).TotalDays;
            //TimeSpan leadTime = dateEdit.DateTime.Subtract(this.dtpMP_START_DATE.DateTime);

            this.txtPLAN_DAY.EditValue = leadTime + 1;
        }

        private void grvLotPlaning_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                int maxRow = Convert.ToInt32(view.Columns["LINE_NO"].SummaryItem.SummaryValue, NumberFormatInfo.CurrentInfo);
                view.SetFocusedRowCellValue("LINE_NO", maxRow + 1);

                if(ShiftDateSelect.HasValue)
                    view.SetFocusedRowCellValue("SHIFT_DATE", ShiftDateSelect.Value);
                //view.SetFocusedRowCellValue("QTY_PER_LABEL", Convert.ToInt32(this.lblStandardQty.Text, NumberFormatInfo.InvariantInfo));
                view.SetFocusedRowCellValue("QTY_PER_LABEL", Convert.ToInt32(this.txtStandardQty.EditValue, NumberFormatInfo.InvariantInfo));
                view.SetFocusedRowCellValue("STATUS", "PLANNED"); //CHANGE GENERATED TO PLANED
                view.SetFocusedRowCellValue("FLAG", 2);
                view.SetFocusedRowCellValue("JOB_NO", this.txtJOB_NO.Text);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InitNewRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            try
            {
                GridView view = (GridView)this.grdLotPlaning.Views[0]; //this.gridView2
                TextEdit editor = (TextEdit)sender;

                if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    int noOfLabel = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "NO_OF_LABEL"), NumberFormatInfo.InvariantInfo);
                    view.SetFocusedRowCellValue("TOTAL_QTY", Convert.ToInt32(editor.EditValue, NumberFormatInfo.InvariantInfo) * noOfLabel);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
       // private int rowupdated = 0;
        private void grvLotPlaning_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;
            try
            {
                this.CalculateSummary();
         
                //if (rowupdated.Equals(0))
                //    rowupdated++;
                //else
                //{
               // rowupdated = 0;
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
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintProductCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderDetail)
                {
                    this.PrintProductCard();
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

                //XtraMessageBox.Show("Print Loading Order");
                List<JobOrder> lstJobOrd;
                GridView view = (GridView)this.grdJobOrder.Views[0];

                if (this.xtcJobOrder.SelectedTabPage == this.xtpJobOrderList)
                {
                    if (this.chkSelect.SelectedCount > 0)
                    {

                        //string[] arrivalNos = new string[check.SelectedCount];
                        //DataRow[] rows = new DataRow[check.SelectedCount];
                        lstJobOrd = new List<JobOrder>(this.chkSelect.SelectedCount);
                        for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                        {
                            lstJobOrd.Add((JobOrder)this.chkSelect.GetSelectedRow(i));
                            //arrivalNos[i] = view.GetRowCellDisplayText(i, "ARRIVAL_NO");
                        }

                        this.PrintJobOrder(lstJobOrd);

                    }
                    else
                    {
                        //MessageBox.Show("PLEASE SELECT DOCUMENT ARRIVAL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        XtraMessageBox.Show(this, "PLEASE SELECT JOB ORDER TO PRINT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    lstJobOrd = new List<JobOrder>(1);
                    //this.PrintCargoCheckSheetReport(this.txtArrivalNo.Text);
                    JobOrder jobOrd = new JobOrder();
                    jobOrd.JOB_NO = this.txtJOB_NO.Text;

                    lstJobOrd.Add(jobOrd);

                    this.PrintJobOrder(lstJobOrd);
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

        private void dtpPRODUCTION_DATE_VisibleChanged(object sender, EventArgs e)
        {
            DateEdit editor = (DateEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Date Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void bteMC_NO_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            ButtonEdit editor = (ButtonEdit)sender;
            if (string.IsNullOrEmpty(editor.Text))
            {
                //e.Cancel = true;
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Machine can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                string mcNo = this.MachineID(editor.Text);
                if (string.IsNullOrEmpty(mcNo))
                {
                    //e.Cancel = true;
                    UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Invalid Machine", ErrorType.Critical);
                    editor.Focus();
                }
                else
                {
                    this.lblMC_NO_VALUE.Text = mcNo;
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
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

        private void dtpPRODUCTION_DATE_Validating(object sender, CancelEventArgs e)
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

        private void btePROD_SEQ_NO_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            ButtonEdit editor = (ButtonEdit)sender;

            if(string.IsNullOrEmpty(editor.Text))
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Product can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                string productionType;

                if (this.luePRODUCTION_TYPE.EditValue != null)
                    productionType = this.luePRODUCTION_TYPE.EditValue.ToString();
                else
                {
                    productionType = "V";
                    this.luePRODUCTION_TYPE.EditValue = "V";
                }
                bool isValid = this.GetProductByNo(editor.Text, productionType, this.btePARTY_ID.Text);
                if (!isValid)
                {
                    UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Invalid Product", ErrorType.Critical);
                    editor.Focus();
                }
                else
                {
                    UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
                }
            }
            
        }

        private bool GetProductByNo(string prodNo, string prodType, string partyid)
        {
            bool result = false;
            try
            {
                using (ProductBLL prodBll = new ProductBLL())
                {
                    Product prod = prodBll.GetProductNo(prodNo, prodType, partyid);

                    if (prod != null)
                    {
                        this.lblPROD_SEQ_NO_Value.Text = prod.PROD_SEQ_NO;
                        this.txtPRODUCT_NAME.Text = prod.PRODUCT_NAME;
                        this.txtMATERIAL_NAME.Text = prod.MATERIAL_NAME;

                        result = true;
                    }
                }

                if (!string.IsNullOrEmpty(this.lblPROD_SEQ_NO_Value.Text))
                {
                    //get box qty
                    using (InfoBLL info = new InfoBLL())
                    {
                        //this.lblStandardQty.Text = info.ProductBoxQty(this.lblPROD_SEQ_NO_Value.Text).ToString("#,##0");
                        this.txtStandardQty.EditValue = info.ProductBoxQty(this.lblPROD_SEQ_NO_Value.Text);
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

        private void txtPLAN_QTY_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            TextEdit editor = (TextEdit)sender;
            bool isValid = UiValidations.Validate_EmptyOrNotMorethan(editor, 0, ref this.dxErrorProvider1, "Invalid Value", ErrorType.Warning);
            if (!isValid)
            {
                editor.Focus();
            }

        }

        private void lueUNIT_Validating(object sender, CancelEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            LookUpEdit editor = (LookUpEdit)sender;
            if (editor.EditValue == null)
            {
                UiValidations.Validate_Detach(editor, ref this.dxErrorProvider1, "Unit Can't be null", ErrorType.Critical);
                editor.Focus();
            }
            else
            {
                UiValidations.Validate_Clear(editor, ref this.dxErrorProvider1);
            }
        }

        private void btn02Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdLotPlaning.Views[0];
                UiUtility.SetGridReadOnly(view, true);

                //this.lotSelect.View.Columns["CheckMarkSelection"].VisibleIndex = 0;
                //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = true;

                //call job lot planning
                this.GetJobLotPlanning(this.txtJOB_NO.Text);
                this.CalculateSummary();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.btnEditLotPlan.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnSaveLotPlan.Enabled = false;
                this.btn02Cancel.Enabled = false;
                this.btngenCard.Enabled = true;

                this.btnPostData.Enabled = true;

                this.ddbPrint.Enabled = true;
            }

        }

        private void grvLotPlaning_ShowingEditor(object sender, CancelEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;


            if (view.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
            {
                e.Cancel = false;
            }
            else
            {
                string cellValue = view.GetRowCellValue(view.FocusedRowHandle, "STATUS").ToString();
                if (cellValue == "PLANNED")
                    e.Cancel = false;
                else
                    e.Cancel = true;
                //int flag = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                //if (flag == 2)
                //{
                //    e.Cancel = false;
                //}
                //else
                //{
                //    view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                //    e.Cancel = true;
                //}


            }  
        }

        private void grvLotPlaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.btnEditLotPlan.Enabled || this.FormState != eFormState.ReadOnly) return;

            try
            {
                GridView view = sender as GridView;

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;
                
                if (e.KeyCode == Keys.Delete)
                {
                    int rowHandle = view.FocusedRowHandle;

                    string status = view.GetRowCellValue(rowHandle, "STATUS").ToString();

                    if (status.Equals("PLANNED") || status.Equals("FAILED"))
                    {
                        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this Lot?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            this.DeleteLotPlanning(view, rowHandle);

                            view.FocusedRowHandle = GridControl.NewItemRowHandle;
                            view.ShowEditor();

                            //SendKeys.Send("{F2}");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "This Lot already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string type = string.Empty;
            if (this.lueSearchPROD_TYPE.EditValue != null)
            {
                type = this.lueSearchPROD_TYPE.EditValue.ToString();
            }

            this.GetJobOrderList(type, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }

        private void frmJobOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdJobOrder.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdLotPlaning.Views[0]);

            this.Controls.Clear();
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
                            if (this.btnEditLotPlan.Enabled)
                            {
                                this.GetJobLotPlanning(this.txtJOB_NO.Text);
                            }
                        }
                    }
                    
                    break;
                default:
                    break;
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

                        if (row.Row["STATUS"].ToString().Equals("PRODUCTION") || row.Row["STATUS"].ToString().Equals("COMPLETED"))
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
                        messageQ = (isStart ? "Do you want to delete this Lot?\n(Some lot already Start that lot can not be delete)" : "Do you want to delete this Lot?");

                        DialogResult isDelete = XtraMessageBox.Show(this, messageQ, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            DataRow[] rows = new DataRow[delCount];
                            int j=0;

                            for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                            {
                                row = (DataRowView)this.lotSelect.GetSelectedRow(i);

                                if (!(row.Row["STATUS"].ToString().Equals("PRODUCTION") || row.Row["STATUS"].ToString().Equals("COMPLETED")))
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
                                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                            finally
                            {
                                view.EndSort();
                                this.dtbLotPlaning.AcceptChanges();

                                DataView dtv = new DataView(this.dtbLotPlaning);
                                dtv.RowFilter = "[FLAG] <> 0";

                                this.grdLotPlaning.DataSource = dtv;
                            }

                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "All of Select Lot already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                    case "SHIFT_DATE":
                        break;
                    case "SHIFT_ID":
                        if (e.PrevFocusedColumn.FieldName.Equals("SHIFT_DATE"))
                        {
                            this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                        }
                        break;
                    case "NO_OF_LABEL":
                        if (e.PrevFocusedColumn.FieldName.Equals("SHIFT_ID"))
                        {
                            this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                        }
                        break;
                    case "QTY_PER_LABEL":
                        if (e.PrevFocusedColumn.FieldName.Equals("NO_OF_LABEL"))
                        {
                            this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                        }
                        break;
                    case "N_SQUARE":
                        if (e.PrevFocusedColumn.FieldName.Equals("N_SQUARE"))
                        {
                            this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
                        }
                        break;
                    case "N_TRIANGLE":
                        if (e.PrevFocusedColumn.FieldName.Equals("N_TRIANGLE"))
                        {
                            this.CheckPreviouseColumn(view, e.PrevFocusedColumn);
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

        private void CheckPreviouseColumn(GridView view, GridColumn preColumn)
        {
            string resultValue = string.Empty;
            switch (preColumn.FieldName)
            {
                case "SHIFT_DATE":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                    }
                    break;
                case "SHIFT_ID":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                    }
                    break;
                case "NO_OF_LABEL":
                    resultValue = view.GetFocusedRowCellDisplayText(preColumn);
                    if (string.IsNullOrEmpty(resultValue))
                    {
                        view.FocusedColumn = preColumn;
                    }
                    break;
                default:
                    break;
            }


        }

        private void grvLotPlaning_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                DataRowView rowView = (DataRowView)e.Row;

                if (rowView["SHIFT_DATE"].ToString() == "")
                {
                    e.Valid = false;
                    view.SetColumnError(view.Columns["SHIFT_DATE"], "Error",
                        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["SHIFT_DATE"];
                    view.ShowEditor();
                    return;
                }

                if (rowView["SHIFT_ID"].ToString() == "")
                {
                    e.Valid = false;
                    view.SetColumnError(view.Columns["SHIFT_ID"], "Error",
                        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["SHIFT_ID"];
                    view.ShowEditor();
                    return;
                }

                if (rowView["NO_OF_LABEL"].ToString() == "")
                {
                    e.Valid = false;
                    view.SetColumnError(view.Columns["NO_OF_LABEL"], "Error",
                        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["NO_OF_LABEL"];
                    view.ShowEditor();
                    return;
                }

                if (rowView["QTY_PER_LABEL"].ToString() == "")
                {
                    e.Valid = false;
                    view.SetColumnError(view.Columns["QTY_PER_LABEL"], "Error",
                        DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["QTY_PER_LABEL"];
                    view.ShowEditor();
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "ValidateRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAdvJobOrder advJobOrd = new frmAdvJobOrder())
                {
                    UiUtility.ShowPopupForm(advJobOrd, this, true);

                    this.AdvanceSearchJobOrder(advJobOrd.JOB_NO, advJobOrd.PROD_TYPE, advJobOrd.FROM_DATE, advJobOrd.TO_DATE);
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

            string type = string.Empty;
            if (this.lueSearchPROD_TYPE.EditValue != null)
            {
                type = this.lueSearchPROD_TYPE.EditValue.ToString();
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetJobOrderList(type, editor.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
                    break;
                default:
                    break;
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
                XtraMessageBox.Show(this, "InvalidRowException " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLotPlaning_rps_dtpSHIFT_DATE_EditValueChanging(object sender, ChangingEventArgs e)
        {
            DateEdit editor = sender as DateEdit;

            if (e.NewValue != null)
                ShiftDateSelect = Convert.ToDateTime(e.NewValue);
            else
                ShiftDateSelect = null;
        }

        private void btngenCard_Click(object sender, EventArgs e)
        {

            //Check box
            try
            {
                //check statusด้วย
            //pgbgenProductCard.PerformStep();
            //pgbgenProductCard.Update();
                if (this.lotSelect.SelectedCount > 0)
                {
                    List<DataRow> lstRow = new List<DataRow>();

                    //DataRow[] rows = new DataRow[];
                    for (int i = 0; i < this.lotSelect.SelectedCount; i++)
                    {
                       
                        DataRowView row = (DataRowView)this.lotSelect.GetSelectedRow(i);
                        if ((string)row.Row["STATUS"] == "PLANNED")
                        {
                            lstRow.Add(row.Row);
                        }
                        //rows[i] = row.Row;
                    }


                    if (lstRow.Count > 0)
                    {
                        using (JobOrderBLL jobOrderBll = new JobOrderBLL())
                        {

                            jobOrderBll.InsertProductCard((string)this.luePRODUCTION_TYPE.EditValue, this.lblPROD_SEQ_NO_Value.Text, lstRow.ToArray());
                        }
                    }

                    GetJobLotPlanning(txtJOB_NO.Text);
                }
                else
                {

                    XtraMessageBox.Show(this, "Please Select Lot Plan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch(Exception ex){

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            
            }


        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            this.PostJobOrderToWorkTicket();
        }

        //public delegate void PassControl(object sender);

        //// Create instance (null)
        //public PassControl passControl;
        
    }
}