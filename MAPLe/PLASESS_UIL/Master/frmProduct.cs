using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using System.Globalization;
using HTN.BITS.UIL.PLASESS.LOVForms;
using System.IO;
using HTN.BITS.UIL.PLASESS.Component.CSV;
using HTN.BITS.UIL.PLASESS.ConfirmForms;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmProduct : BaseChildForm
    {
        public frmProduct()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProduct);
            base.LoadGridLayout(this.grdProdProcess);

            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;

        private List<Machine> lstMachine;
        private List<MaterialType> lstMtlType;
        private List<ProductionType> lstProductionType;

        private DataTable dtbProdProcess = null;
        private Bitmap _emptyImage = null;

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

        public string MachineName(string mcNo)
        {
            if (this.lstMachine == null)
                return string.Empty;

            Machine mcTemp = lstMachine.Find(delegate(Machine _mc)
            {
                return _mc.MC_NO == mcNo;
            });

            if(mcTemp != null)
            {
                return mcTemp.MACHINE_NAME;
            }
            else
            {
                return string.Empty;
            }
        }

        public string MtlTypeName(string mtlTypeNo)
        {
            if (this.lstMtlType == null)
                return string.Empty;

            MaterialType mtlTypeTemp = lstMtlType.Find
            (
                delegate(MaterialType mtlType)
                {
                    return mtlType.SEQ_NO == mtlTypeNo;
                }
            );

            if (mtlTypeTemp != null)
            {
                return mtlTypeTemp.NAME;
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

            if(pdtType != null)
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
                return string.Format("Product_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcProduct.SelectedTabPage == this.xtpProductList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdProduct.Views[0] as GridView;
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
                GridView view = (GridView)this.grdProdProcess.Views[0];

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntProduct.Enabled = true;

                        this.dntProduct.TextStringFormat = "      Add Mode      ";
                        this.dntProduct.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "&Cancel";

                        view.OptionsBehavior.Editable = true;

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.txtPRODUCT_NO.Properties.ReadOnly = true;
                        this.txtPRODUCT_NAME.Properties.ReadOnly = true;
                        this.luePRODUCTION_TYPE.Properties.ReadOnly = true;

                        this.dntProduct.Enabled = true;

                        this.dntProduct.TextStringFormat = "      Edit Mode      ";
                        this.dntProduct.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "&Cancel";

                        view.OptionsBehavior.Editable = true;
                        UiUtility.SetGridEditOnly(view, true, 1);

                        //if (this.luePRODUCTION_TYPE.EditValue != null)
                        //{
                        //    switch (this.luePRODUCTION_TYPE.EditValue.ToString().ToUpper())
                        //    {
                        //        case "H":
                        //            view.OptionsBehavior.Editable = false;
                        //            UiUtility.SetGridReadOnly(view, true);
                        //            break;
                        //        default:
                        //            view.OptionsBehavior.Editable = true;
                        //            UiUtility.SetGridEditOnly(view, true, 1);
                        //            break;
                        //    }
                        //}
                        

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.dntProduct.Enabled = false;

                        this.dntProduct.TextStringFormat = " Record {0} of {1} ";
                        this.dntProduct.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "&Back";

                        view.OptionsBehavior.Editable = false;

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
            this.txtPRODUCT_NO.Properties.ReadOnly = state;
            this.txtPRODUCT_NAME.Properties.ReadOnly = state;
            //---------------------------------------
            //this.bteMATERIAL_TYPE.Properties.ReadOnly = state;
            this.bteMATERIAL_TYPE.Properties.Buttons[0].Enabled = !state;
            //----------------------------------------------------
            this.luePRODUCTION_TYPE.Properties.ReadOnly = state;
            //this.bteMC_NO.Properties.ReadOnly = state;
            this.bteMC_NO.Properties.Buttons[0].Enabled = !state;
            this.txtBOX_QTY.Properties.ReadOnly = state;
            this.lueUNIT.Properties.ReadOnly = state;
            this.txtREMARK.Properties.ReadOnly = state;
            this.btnBrowse.Enabled = !state;
            this.icbREC_STAT.Properties.ReadOnly = state;

            //add new control on 07-Jun-2011
            this.txtCUST_PROD_NO.Properties.ReadOnly = state;
            this.txtBUYER_PRICE.Properties.ReadOnly = state;
            this.txtSELLING_PRICE.Properties.ReadOnly = state;
            this.txtCOST_PRICE.Properties.ReadOnly = state;
        }

        private void ClearDataOnScreen()
        {
            this.txtPROD_SEQ_NO.Text = string.Empty;
            this.txtPRODUCT_NO.Text = string.Empty;
            this.txtPRODUCT_NAME.Text = string.Empty;
            //---------------------------------------
            this.lblMATERIAL_TYPE_CODE.Text = string.Empty;
            this.bteMATERIAL_TYPE.EditValue = string.Empty;
            //----------------------------------------------------
            this.luePRODUCTION_TYPE.EditValue = string.Empty;
            this.lblMC_NO_VALUE.Text = string.Empty;
            this.bteMC_NO.EditValue = string.Empty;
            this.txtBOX_QTY.EditValue = string.Empty;
            //this.lueUNIT.EditValue = string.Empty;
            this.txtREMARK.Text = string.Empty;
            this.picPROD_IMAGE.Image = this._emptyImage;
            this.icbREC_STAT.EditValue = true;

            //add new control on 07-Jun-2011
            this.txtCUST_PROD_NO.EditValue = string.Empty;
            this.txtBUYER_PRICE.EditValue = 0.0M;
            this.txtSELLING_PRICE.EditValue = 0.0M;
            this.txtCOST_PRICE.EditValue = 0.0M;
        }

        private void InitializaLOVData()
        {
            this._emptyImage = base.Language.GetBitmap("EmptyImage");
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

                using (MaterialTypeBLL mtltypeBll = new MaterialTypeBLL())
                {
                    this.lstMtlType = mtltypeBll.GetMTLTypeList(string.Empty);
                }

                this.grvProduct_rps_lueMACHINE.DataSource = this.lstMachine;
                this.grvProduct_rps_lueMATERIAL_TYPE.DataSource = this.lstMtlType;
                this.grvProduct_rps_luePRODUCTION_TYPE.DataSource = this.lstProductionType;
                this.luePRODUCTION_TYPE.Properties.DataSource = this.lstProductionType;
                this.luePdTypeUpload.Properties.DataSource = this.lstProductionType;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetProductList(string findAll)
        {
            List<Product> lstProduct = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prodBll = new ProductBLL())
                {
                    lstProduct = prodBll.GetProductList(findAll);
                }

                this.grdProduct.DataSource = lstProduct;
                this.dntProduct.DataSource = lstProduct;
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

        public void GetBindingProduct(string prodID)
        {
            Product prod = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prodBll = new ProductBLL())
                {
                    prod = prodBll.GetProduct(prodID);
                }

                if (prod != null)
                {
                    this.txtPROD_SEQ_NO.Text = prod.PROD_SEQ_NO;
                    this.txtPRODUCT_NO.Text = prod.PRODUCT_NO;
                    this.txtPRODUCT_NAME.Text = prod.PRODUCT_NAME;
                    //---------------------------------------
                    this.lblMATERIAL_TYPE_CODE.Text = prod.MATERIAL_TYPE;
                    this.bteMATERIAL_TYPE.EditValue = this.MtlTypeName(prod.MATERIAL_TYPE);
                    //----------------------------------------------------
                    this.luePRODUCTION_TYPE.EditValue = prod.PRODUCTION_TYPE;
                    this.lblMC_NO_VALUE.Text = prod.MC_NO;
                    this.bteMC_NO.EditValue = this.MachineName(prod.MC_NO);
                    this.txtBOX_QTY.EditValue = prod.BOX_QTY;
                    this.lueUNIT.EditValue = prod.UNIT;
                    this.txtREMARK.Text = prod.REMARK;
                    this.picPROD_IMAGE.Image = prod.PROD_IMAGE;
                    this.icbREC_STAT.EditValue = prod.REC_STAT;

                    //add new binding control on 07-Jun-2011
                    this.txtCUST_PROD_NO.EditValue = prod.CUST_PROD_NO;
                    this.txtBUYER_PRICE.EditValue = prod.BUYER_PRICE;
                    this.txtSELLING_PRICE.EditValue = prod.SELLING_PRICE;
                    this.txtCOST_PRICE.EditValue = prod.COST_PRICE;

                    this.GetProdProcess(prod.PROD_SEQ_NO);
                }
                else
                {
                    this.ClearDataOnScreen();
                    XtraMessageBox.Show(this, "No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        private bool IsFormValidated()
        {
            return true;
        }

        public void InsertProduct()
        {
            string result = string.Empty;
            List<ProdProcess> lstProdProcess = null;
            Product prod = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                prod = new Product();

                prod.PROD_SEQ_NO = this.txtPROD_SEQ_NO.Text;
                prod.PRODUCT_NO = this.txtPRODUCT_NO.Text;
                prod.PRODUCT_NAME = this.txtPRODUCT_NAME.Text;
                prod.MATERIAL_TYPE = this.lblMATERIAL_TYPE_CODE.Text;
                prod.PRODUCTION_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;
                prod.MC_NO = this.lblMC_NO_VALUE.Text;
                prod.BOX_QTY = Convert.ToInt32(this.txtBOX_QTY.EditValue, NumberFormatInfo.InvariantInfo); 
                prod.UNIT = (string)this.lueUNIT.EditValue;
                prod.PROD_IMAGE = (Bitmap)this.picPROD_IMAGE.Image;
                prod.REMARK = this.txtREMARK.Text;
                prod.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                //add new insert property on 07-Jun-2011
                prod.CUST_PROD_NO = this.txtCUST_PROD_NO.Text;
                prod.BUYER_PRICE = (this.txtBUYER_PRICE.EditValue as decimal?) ?? 0.0M;
                prod.SELLING_PRICE = (this.txtSELLING_PRICE.EditValue as decimal?) ?? 0.0M;
                prod.COST_PRICE = (this.txtCOST_PRICE.EditValue as decimal?) ?? 0.0M;

                if (this.dtbProdProcess.Rows.Count != 0)
                {
                    lstProdProcess = new List<ProdProcess>();
                    int flag = 0;
                    ProdProcess prodProcess;
                    foreach (DataRow rowProdProcess in this.dtbProdProcess.Rows)
                    {
                        flag = Convert.ToInt32(rowProdProcess["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 2)
                        {
                            prodProcess = new ProdProcess();
                            prodProcess.PROD_SEQ_NO = rowProdProcess["PROD_SEQ_NO"].ToString();
                            prodProcess.PROCESS_NO = rowProdProcess["PROCESS_NO"].ToString();
                            prodProcess.STEP_NO = (int)rowProdProcess["STEP_NO"];
                            prodProcess.REC_STAT = (bool)rowProdProcess["REC_STAT"];

                            lstProdProcess.Add(prodProcess);
                        }
                    }
                }

                string userid = ((frmMainMenu)this.ParentForm).UserID;
                using (ProductBLL prodBll = new ProductBLL())
                {
                    result = prodBll.InsertProduct(ref prod, userid);

                    if (lstProdProcess != null)
                    {
                        prodBll.UpdateProductionProcess(lstProdProcess, prod.PROD_SEQ_NO, userid);
                    }
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

                this.GetProductList(string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdProduct.Views[0];

                    viewList.ClearSorting();

                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PROD_SEQ_NO", prod.PROD_SEQ_NO);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntProduct.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }
        }

        public void UpdateProduct()
        {
            string result = string.Empty;
            List<ProdProcess> lstProdProcess = null;
            Product prod = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                prod = new Product();

                prod.PROD_SEQ_NO = this.txtPROD_SEQ_NO.Text;
                prod.PRODUCT_NO = this.txtPRODUCT_NO.Text;
                prod.PRODUCT_NAME = this.txtPRODUCT_NAME.Text;
                prod.MATERIAL_TYPE = this.lblMATERIAL_TYPE_CODE.Text;
                prod.PRODUCTION_TYPE = (string)this.luePRODUCTION_TYPE.EditValue;
                prod.MC_NO = this.lblMC_NO_VALUE.Text;
                prod.BOX_QTY = Convert.ToInt32(this.txtBOX_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                prod.UNIT = (string)this.lueUNIT.EditValue;
                if ((Bitmap)this.picPROD_IMAGE.Image != null)
                {
                    prod.PROD_IMAGE = (Bitmap)this.picPROD_IMAGE.Image;
                }
                else
                {
                    prod.PROD_IMAGE = this._emptyImage;
                }
                prod.REMARK = this.txtREMARK.Text;
                prod.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                //add new Update property on 07-Jun-2011
                prod.CUST_PROD_NO = this.txtCUST_PROD_NO.Text;
                prod.BUYER_PRICE = (this.txtBUYER_PRICE.EditValue as decimal?) ?? 0.0M;
                prod.SELLING_PRICE = (this.txtSELLING_PRICE.EditValue as decimal?) ?? 0.0M;
                prod.COST_PRICE = (this.txtCOST_PRICE.EditValue as decimal?) ?? 0.0M;

                if (this.dtbProdProcess.Rows.Count != 0)
                {
                    lstProdProcess = new List<ProdProcess>();
                    int flag = 0;
                    ProdProcess prodProcess;
                    foreach (DataRow rowProdProcess in this.dtbProdProcess.Rows)
                    {
                        flag = Convert.ToInt32(rowProdProcess["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 2)
                        {
                            prodProcess = new ProdProcess();
                            prodProcess.PROD_SEQ_NO = rowProdProcess["PROD_SEQ_NO"].ToString();
                            prodProcess.PROCESS_NO = rowProdProcess["PROCESS_NO"].ToString();
                            prodProcess.STEP_NO = (int)rowProdProcess["STEP_NO"];
                            prodProcess.REC_STAT = (bool)rowProdProcess["REC_STAT"];

                            lstProdProcess.Add(prodProcess);
                        }
                    }
                }

                string userid = ((frmMainMenu)this.ParentForm).UserID;
                using (ProductBLL prodBll = new ProductBLL())
                {
                    result = prodBll.UpdateProduct(prod, userid);

                    if (lstProdProcess != null)
                    {
                        prodBll.UpdateProductionProcess(lstProdProcess, userid);
                    }
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdProduct.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("PROD_SEQ_NO", prod.PROD_SEQ_NO);
                    view.SetFocusedRowCellValue("PRODUCT_NO", prod.PRODUCT_NO);
                    view.SetFocusedRowCellValue("PRODUCT_NAME", prod.PRODUCT_NAME);
                    view.SetFocusedRowCellValue("MATERIAL_TYPE", prod.MATERIAL_TYPE);
                    view.SetFocusedRowCellValue("PRODUCTION_TYPE", prod.PRODUCTION_TYPE);
                    view.SetFocusedRowCellValue("MC_NO", prod.MC_NO);
                    view.SetFocusedRowCellValue("BOX_QTY", prod.BOX_QTY);
                    view.SetFocusedRowCellValue("REMARK", prod.REMARK);
                    view.SetFocusedRowCellValue("REC_STAT", prod.REC_STAT);

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

        private void GetProdProcess(string prodSEQ)
        {
            try
            {
                using (ProductBLL prodBll = new ProductBLL())
                {
                    this.dtbProdProcess = prodBll.GetProductionProcess(prodSEQ);
                }

                if (dtbProdProcess != null)
                {
                    dtbProdProcess.DefaultView.Sort = "STEP_NO";
                }

                this.grdProdProcess.DataSource = this.dtbProdProcess;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void DisplayImage(string ImagePath, PictureEdit control)
        {
            byte[] imageBytes = this.GetBytesFromImage(ImagePath);
            Image myImage = Image.FromStream(new MemoryStream(imageBytes));
            Size fitImageSize = ScaledImageDimensions(myImage.Width, myImage.Height, control.Width, control.Height);

            Bitmap imgOutput = new Bitmap(myImage, fitImageSize.Width, fitImageSize.Height);

            control.Image = null;
            control.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            control.Image = imgOutput;
        }

        private byte[] GetBytesFromImage(String imageFile)
        {
            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(imageFile);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        private Size ScaledImageDimensions(int currentImageWidth, int currentImageHeight, int desiredImageWidth, int desiredImageHeight)
        {
            /* First, we must calculate a multiplier that will be used
             * to get the dimensions of the new, scaled image. */
            double scaleImageMultiplier = 0;
            /* This multiplier is defined as the ratio of the
             * Desired Dimension to the Current Dimension.
             * Specifically which dimension is used depends on the larger
             * dimension of the image, as this will be the constraining dimension
             * when we fit to the window. */
            /* Determine if Image is Portrait or Landscape. */
            if (currentImageHeight > currentImageWidth) /* Image is Portrait */
            {
                /* Calculate the multiplier based on the heights. */
                if (desiredImageHeight > desiredImageWidth)
                {
                    scaleImageMultiplier = (double)desiredImageWidth / (double)currentImageWidth;
                }
                else
                {
                    scaleImageMultiplier = (double)desiredImageHeight / (double)currentImageHeight;
                }
            }
            else /* Image is Landscape */
            {
                /* Calculate the multiplier based on the widths. */
                if (desiredImageWidth > desiredImageHeight)
                {
                    scaleImageMultiplier = (double)desiredImageWidth / (double)currentImageWidth;
                }
                else
                {
                    scaleImageMultiplier = (double)desiredImageHeight / (double)currentImageHeight;
                }
            }
            /* Generate and return the new scaled dimensions.
             * Essentially, we multiply each dimension of the original image
             * by the multiplier calculated above to yield the dimensions
             * of the scaled image. The scaled image can be larger or smaller
             * than the original. */
            int outputWidth = (int)(currentImageWidth * scaleImageMultiplier);
            int outputHight = (int)(currentImageHeight * scaleImageMultiplier);
            return new Size((currentImageWidth > outputWidth) ? outputWidth : currentImageWidth, (currentImageHeight > outputHight) ? outputHight : currentImageHeight);
        }


        #endregion

        private void frmProduct_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpProductDetail.PageVisible = false;
            //this.InitializaLOVData();
            //this.GetProductList(string.Empty);
            //this.FormState = eFormState.ReadOnly;
        }

        private void frmProduct_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtpProductDetail.PageVisible = false;
            this.InitializaLOVData();
            this.GetProductList(string.Empty);
            this.FormState = eFormState.ReadOnly;
        }

        private void grvProduct_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    string prod = view.GetRowCellValue(info.RowHandle, "PROD_SEQ_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpProductList.PageEnabled = false;

                    this.xtpProductDetail.PageVisible = true;
                    this.xtcProduct.SelectedTabPage = this.xtpProductDetail;

                    //Call record detail.
                    this.GetBindingProduct(prod);
                    

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dntProduct_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)this.grdProduct.Views[0]; //this.gridView2

                if (this.xtcProduct.SelectedTabPage == this.xtpProductDetail)
                {
                    string prod = view.GetFocusedRowCellValue("PROD_SEQ_NO").ToString();

                    this.GetBindingProduct(prod);
                    
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
            this.txtPRODUCT_NO.Focus();
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
                        this.xtpProductDetail.PageVisible = false;
                        this.xtpProductList.PageEnabled = true;
                        this.xtcProduct.SelectedTabPage = this.xtpProductList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;
                    case eFormState.Edit:

                        this.GetBindingProduct(this.txtPROD_SEQ_NO.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpProductDetail.PageVisible = false;
                        this.xtpProductList.PageEnabled = true;
                        this.xtcProduct.SelectedTabPage = this.xtpProductList;

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
            try
            {
                DialogResult isAuthen = DialogResult.None;

                using (frmCOFAuthen fAuthen = new frmCOFAuthen())
                {
                    isAuthen = UiUtility.ShowPopupForm(fAuthen, this, true);
                }

                if (isAuthen == DialogResult.OK)
                {
                    DialogResult result = XtraMessageBox.Show(this, "Do you want to add new product master?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        this.FormState = eFormState.Add;

                        this.GetProdProcess("DEMO");

                        this.ClearDataOnScreen();

                        this.xtpProductDetail.PageVisible = true;
                        this.xtcProduct.SelectedTabPage = this.xtpProductDetail;

                        this.luePRODUCTION_TYPE.EditValue = "INJ";



                        this.btnAddNew.Visible = false;
                        this.btnExit.Visible = false;

                        this.txtPRODUCT_NO.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "You do not have the right to add new product master.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertProduct();
                    break;
                case eFormState.Edit:
                    this.UpdateProduct();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog fdlg = new OpenFileDialog { 
                    Title = "Open File", 
                    //InitialDirectory = @"c:\", 
                    Filter = "All files (*.*)|*.*|All files (*.*)|*.*", 
                    FilterIndex = 2, 
                    RestoreDirectory = true })
                {
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        //this.picPROD_IMAGE.Image = Image.FromFile(fdlg.FileName);
                        this.DisplayImage(fdlg.FileName, this.picPROD_IMAGE);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bteMATERIAL_TYPE_Validated(object sender, EventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //this.lblMATERIAL_TYPE_CODE.Text = btnEdit.EditValue.ToString();
        }

        private void bteMC_NO_Validated(object sender, EventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //this.lblMC_NO_VALUE.Text = btnEdit.EditValue.ToString();
        }

        private void bteMATERIAL_TYPE_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string mcname = string.Empty;
            string mcno = string.Empty;
            using (frmLOVMtlType fMtlTypeList = new frmLOVMtlType())
            {
                result = UiUtility.ShowPopupForm(fMtlTypeList, this, true);
                mcname = fMtlTypeList.MTLTYPT_NAME;
                mcno = fMtlTypeList.SEQ_NO;
            }

            if (result == DialogResult.OK)
            {
                btnEdit.Text = mcname;
                this.lblMATERIAL_TYPE_CODE.Text = mcno;

                this.luePRODUCTION_TYPE.Focus();
            }
            
        }

        private void bteMC_NO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit btnEdit = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string mcno = string.Empty;
            string mcname = string.Empty;
            using(frmLOVMachine fMcList = new frmLOVMachine())
            {
                object value = this.luePRODUCTION_TYPE.EditValue;
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    fMcList.MACHINE_TYPE = value.ToString();
                }
                else
                {
                    fMcList.MACHINE_TYPE = "V";
                }

                result = UiUtility.ShowPopupForm(fMcList, this, true);

                mcno = fMcList.MC_NO;
                mcname = fMcList.MACHINE_NAME;
            }

            if (result == DialogResult.OK)
            {
                btnEdit.Text = mcname;
                this.lblMC_NO_VALUE.Text = mcno;

                this.txtBOX_QTY.Focus();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.GetProductList(this.txtSearch.Text);
        }

        private void grvProdProcess_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                if(view.Editable)
                    view.SetFocusedRowCellValue("FLAG", 2); //UPDATE
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmProduct_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcProduct.SelectedTabPage == this.xtpProductList)
                        {
                            this.btnApply.PerformClick();
                        }
                        else
                        {
                            this.GetProdProcess(this.txtPROD_SEQ_NO.Text);
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //TextEdit editor = (TextEdit)sender;

            //this.GetProductList(editor.Text);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetProductList(editor.Text);
                    break;
                default:
                    break;
            }
        }

        private void frmProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayoutMultipleView(this.Name, this.grdProduct);
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdProdProcess.Views[0]);

            this.Controls.Clear();
        }

        private void luePRODUCTION_TYPE_Validated(object sender, EventArgs e)
        {
            //LookUpEdit control = sender as LookUpEdit;
            //GridView view = (GridView)this.grdProdProcess.Views[0];
            //if (control != null)
            //{
            //    switch(control.EditValue.ToString().ToUpper())
            //    {
            //        case "PNT":

            //            //disable Grid
            //            for (int i = 0; i < view.RowCount; i++)
            //            {
            //                if (i.Equals(0))
            //                {
            //                    view.SetRowCellValue(i, "REC_STAT", true);
            //                    view.SetRowCellValue(i, "FLAG", 2);
            //                }
            //                else
            //                {
            //                    view.SetRowCellValue(i, "REC_STAT", false);
            //                    view.SetRowCellValue(i, "FLAG", 1);
            //                }
            //            }
            //            view.OptionsBehavior.Editable = false;
            //            UiUtility.SetGridReadOnly(view, true);

            //            break;
            //        default:
            //            UiUtility.SetGridEditOnly(view, true, 1);
            //            view.OptionsBehavior.Editable = true;
            //            break;
            //    }
            //}
        }

        private void luePRODUCTION_TYPE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;
            try
            {
                LookUpEdit editor = sender as LookUpEdit;

                //dtbProdPrtTmp.Select().ToList<DataRow>().ForEach(r => r["PROD_SEQ_NO"] = prodSeq.Value);
                //dtbProdPrtTmp.AcceptChanges();
                this.dtbProdProcess.Select().ToList<DataRow>().ForEach(r => r["REC_STAT"] = false);

                this.dtbProdProcess.Select("[FLAG] = 1").ToList<DataRow>().ForEach(r => r["FLAG"] = 3);

                this.dtbProdProcess.AsEnumerable().Where(r => r.Field<string>("PROCESS_NO") == (string)editor.EditValue)
                                                  .Select(ru => ru["REC_STAT"] = true)
                                                  .ToList();

                this.dtbProdProcess.AcceptChanges();

                this.grdProdProcess.RefreshDataSource();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvProdProcess_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = (GridView)this.grdProdProcess.Views[0];

            try
            {
                switch (this.luePRODUCTION_TYPE.EditValue.ToString().ToUpper())
                {
                    case "PNT":
                        if (view.IsRowSelected(0))
                            e.Cancel = true;
                        break;
                    case "LSR":
                        if (view.IsRowSelected(0) || view.IsRowSelected(1))
                            e.Cancel = true;
                        break;
                    case "TMP":
                        if (view.IsRowSelected(0) || view.IsRowSelected(1) || view.IsRowSelected(2))
                            e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnUploadCSV_Click(object sender, EventArgs e)
        {
            string prodType = string.Empty;

            if (this.luePdTypeUpload.EditValue != null)
                prodType = (string)this.luePdTypeUpload.EditValue;
            else
                prodType = "INJ";

            DialogResult result;
            using (frmUploadCSV uploadCSV = new frmUploadCSV() { MASTER_FORM = eMaster_Form.FRM_PRODUCT_MASTER, 
                                                                 LOCATION_ID = prodType, 
                                                                 USER_ID = ((frmMainMenu)this.ParentForm).UserID})
            {
                result = UiUtility.ShowPopupForm(uploadCSV, this, true);
            }
        }

        private void btnUploadBOM_Click(object sender, EventArgs e)
        {
            DialogResult result;
            using (frmUploadCSV uploadCSV = new frmUploadCSV()
            {
                MASTER_FORM = eMaster_Form.FRM_PRODUCT_BOM_MASTER,
                USER_ID = ((frmMainMenu)this.ParentForm).UserID
            })
            {
                result = UiUtility.ShowPopupForm(uploadCSV, this, true);
            }
        }



    }
}