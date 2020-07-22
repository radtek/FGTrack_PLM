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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using System.Globalization;
using HTN.BITS.UIL.PLASESS.LOVForms;
using System.IO;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmMaterial : BaseChildForm
    {
        public frmMaterial()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMaterial);

            this.CustomInitializeComponent();
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
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

        #endregion

        #region "Button Export Referance"

        private string FileName
        {
            get
            {
                return string.Format("Material_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private bool IsTabListSelected
        {
            get
            {
                return (this.xtcMaterial.SelectedTabPage == this.xtpMaterialList);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdMaterial.Views[0] as GridView;
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

                        this.dntProduct.Enabled = true;

                        this.dntProduct.TextStringFormat = "      Add Mode      ";
                        this.dntProduct.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "&Cancel";

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.ChangeControlState(false);

                        this.dntProduct.Enabled = true;

                        this.dntProduct.TextStringFormat = "      Edit Mode      ";
                        this.dntProduct.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "&Cancel";

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
            this.txtMTL_SEQ_NO.Properties.ReadOnly = true;

            this.txtMTL_CODE.Properties.ReadOnly = state;
            this.txtMTL_NAME.Properties.ReadOnly = state;
            this.txtMTL_GRADE.Properties.ReadOnly = state;
            this.txtMTL_COLOR.Properties.ReadOnly = state;
            this.lueUNIT.Properties.ReadOnly = state;
            this.txtSTD_QTY.Properties.ReadOnly = state;
            this.txtMIN_QTY.Properties.ReadOnly = state;
            this.txtMAX_QTY.Properties.ReadOnly = state;

            this.btePARTY_ID.Properties.ReadOnly = state;
            this.btePARTY_ID.Properties.Buttons[0].Enabled = !state;

            this.lueLOCATION_ID.Properties.ReadOnly = state;

            this.txtREMARK.Properties.ReadOnly = state;
            this.btnBrowse.Enabled = !state;
            this.icbREC_STAT.Properties.ReadOnly = state;

        }

        private void ClearDataOnScreen()
        {
            this._emptyImage = base.Language.GetBitmap("EmptyImage");

            this.txtMTL_SEQ_NO.EditValue = null;
            this.txtMTL_CODE.EditValue = null;
            this.txtMTL_NAME.EditValue = null;
            this.txtMTL_GRADE.EditValue = null;
            this.txtMTL_COLOR.EditValue = null;
            this.lueUNIT.EditValue = null;
            this.txtSTD_QTY.EditValue = null;
            this.txtMIN_QTY.EditValue = null;
            this.txtMAX_QTY.EditValue = null;
            this.btePARTY_ID.EditValue = null;
            this.txtPARTY_NAME.EditValue = null;
            this.picMTL_IMAGE.Image = this._emptyImage;
            this.lueLOCATION_ID.EditValue = null;
            this.txtREMARK.EditValue = null;
            this.icbREC_STAT.EditValue = true;
        }

        private void InitializaLOVData()
        {
            this._emptyImage = base.Language.GetBitmap("EmptyImage");

            try
            {
                using (MaterialBLL mtlBll = new MaterialBLL())
                {
                    //for Unit
                    List<Unit> lstUnit = mtlBll.GetUnitList();
                    if (lstUnit != null)
                    {
                        this.lueUNIT.Properties.DataSource = lstUnit;

                        if (lstUnit.Count > 0)
                        {
                            Unit unitTemp = lstUnit.Find(delegate(Unit _unit)
                            {
                                return _unit.SEQ_NO == "KG";
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

                    //for Location
                    List<Location> lstLoc = mtlBll.GetLocationList();
                    if (lstLoc != null)
                    {
                        this.lueLOCATION_ID.Properties.DataSource = lstLoc;
                        this.grvProduct_rps_lueLOCATION_ID.DataSource = lstLoc;
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public void GetMaterialList(string findAll)
        {
            List<Material> lstMaterial = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MaterialBLL materialBll = new MaterialBLL())
                {
                    lstMaterial = materialBll.GetMaterialList(findAll);
                }

                this.grdMaterial.DataSource = lstMaterial;
                this.dntProduct.DataSource = lstMaterial;

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

        public void GetBindingMaterial(string mtlSeq)
        {
            Material material = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MaterialBLL materialBll = new MaterialBLL())
                {
                    material = materialBll.GetMaterial(mtlSeq);
                }

                if (material != null)
                {
                    this.txtMTL_SEQ_NO.EditValue = material.MTL_SEQ_NO;
                    this.txtMTL_CODE.EditValue = material.MTL_CODE;
                    this.txtMTL_NAME.EditValue = material.MTL_NAME;
                    this.txtMTL_GRADE.EditValue = material.MTL_GRADE;
                    this.txtMTL_COLOR.EditValue = material.MTL_COLOR;

                    this.lueUNIT.EditValue = material.UNIT;

                    this.txtSTD_QTY.EditValue = material.STD_QTY;
                    this.txtMIN_QTY.EditValue = material.MIN_QTY;
                    this.txtMAX_QTY.EditValue = material.MAX_QTY;

                    this.btePARTY_ID.EditValue = material.PARTY_ID;
                    this.txtPARTY_NAME.EditValue = material.PARTY_NAME;
                    
                    if (material.MTL_IMAGE != null)
                    {
                        this.picMTL_IMAGE.Image = material.MTL_IMAGE;
                    }
                    else
                    {
                        this.picMTL_IMAGE.Image = this._emptyImage;
                    }
                    this.lueLOCATION_ID.EditValue = material.LOCATION_ID;

                    this.txtREMARK.Text = material.REMARK;
                    this.icbREC_STAT.EditValue = material.REC_STAT;
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

        public void InsertMaterial()
        {
            string result = string.Empty;
            Material material = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                material = new Material();

                material.MTL_SEQ_NO = this.txtMTL_SEQ_NO.Text;
                material.MTL_CODE = this.txtMTL_CODE.Text;
                material.MTL_NAME = this.txtMTL_NAME.Text;
                material.MTL_GRADE = this.txtMTL_GRADE.Text;
                material.MTL_COLOR = this.txtMTL_COLOR.Text;
                if(this.lueUNIT.EditValue != null)
                    material.UNIT = (string)this.lueUNIT.EditValue;
                material.STD_QTY = Convert.ToDecimal(this.txtSTD_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.MIN_QTY = Convert.ToDecimal(this.txtMIN_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.MAX_QTY = Convert.ToDecimal(this.txtMAX_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.PARTY_ID = this.btePARTY_ID.Text;
                material.MTL_IMAGE = (Bitmap)this.picMTL_IMAGE.Image;
                if(this.lueLOCATION_ID.EditValue != null)
                    material.LOCATION_ID = (string)this.lueLOCATION_ID.EditValue;
                material.REMARK = this.txtREMARK.Text;
                material.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                string userid = ((frmMainMenu)this.ParentForm).UserID;
                using (MaterialBLL materialBll = new MaterialBLL())
                {
                    result = materialBll.InsertMaterial(ref material, userid);
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

                this.GetMaterialList(string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdMaterial.Views[0];

                    viewList.ClearSorting();

                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "MTL_SEQ_NO", material.MTL_SEQ_NO);
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

        public void UpdateMaterial()
        {
            string result = string.Empty;
            Material material = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                material = new Material();

                material.MTL_SEQ_NO = this.txtMTL_SEQ_NO.Text;
                material.MTL_CODE = this.txtMTL_CODE.Text;
                material.MTL_NAME = this.txtMTL_NAME.Text;
                material.MTL_GRADE = this.txtMTL_GRADE.Text;
                material.MTL_COLOR = this.txtMTL_COLOR.Text;
                if (this.lueUNIT.EditValue != null)
                    material.UNIT = (string)this.lueUNIT.EditValue;
                material.STD_QTY = Convert.ToDecimal(this.txtSTD_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.MIN_QTY = Convert.ToDecimal(this.txtMIN_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.MAX_QTY = Convert.ToDecimal(this.txtMAX_QTY.EditValue, NumberFormatInfo.InvariantInfo);
                material.PARTY_ID = this.btePARTY_ID.Text;
                if ((Bitmap)this.picMTL_IMAGE.Image != null)
                {
                    material.MTL_IMAGE = (Bitmap)this.picMTL_IMAGE.Image;
                }
                else
                {
                    material.MTL_IMAGE = this._emptyImage;
                }
                if (this.lueLOCATION_ID.EditValue != null)
                    material.LOCATION_ID = (string)this.lueLOCATION_ID.EditValue;
                material.REMARK = this.txtREMARK.Text;
                material.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                string userid = ((frmMainMenu)this.ParentForm).UserID;
                using (MaterialBLL materialBll = new MaterialBLL())
                {
                    result = materialBll.UpdateMaterial(material, userid);
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdMaterial.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("MTL_SEQ_NO", material.MTL_SEQ_NO);
                    view.SetFocusedRowCellValue("MTL_CODE", material.MTL_CODE);
                    view.SetFocusedRowCellValue("MTL_NAME", material.MTL_NAME);
                    view.SetFocusedRowCellValue("MTL_GRADE", material.MTL_GRADE);
                    view.SetFocusedRowCellValue("MTL_COLOR", material.MTL_COLOR);
                    view.SetFocusedRowCellValue("UNIT", material.UNIT);
                    view.SetFocusedRowCellValue("STD_QTY", material.STD_QTY);
                    view.SetFocusedRowCellValue("MIN_QTY", material.MIN_QTY);
                    view.SetFocusedRowCellValue("MAX_QTY", material.MAX_QTY);
                    view.SetFocusedRowCellValue("PARTY_ID", material.PARTY_ID);
                    view.SetFocusedRowCellValue("LOCATION_ID", material.LOCATION_ID);
                    view.SetFocusedRowCellValue("REMARK", material.REMARK);
                    view.SetFocusedRowCellValue("REC_STAT", material.REC_STAT);

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
                        this.txtSTD_QTY.Focus();
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

        #endregion

        private void frmMaterial_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            //this.xtpProductDetail.PageVisible = false;
            //this.InitializaLOVData();
            //this.GetProductList(string.Empty);
            //this.FormState = eFormState.ReadOnly;
        }

        private void frmMaterial_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtpMaterialDetail.PageVisible = false;

            this.InitializaLOVData();

            this.GetMaterialList(string.Empty);
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
                    string mtlSeq = view.GetRowCellValue(info.RowHandle, "MTL_SEQ_NO").ToString();

                    this.btnAddNew.Visible = false;
                    this.btnExit.Visible = false;

                    //Change tab view.
                    this.xtpMaterialList.PageEnabled = false;

                    this.xtpMaterialDetail.PageVisible = true;
                    this.xtcMaterial.SelectedTabPage = this.xtpMaterialDetail;

                    //Call record detail.
                    this.GetBindingMaterial(mtlSeq);
                    

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
                GridView view = (GridView)this.grdMaterial.Views[0]; //this.gridView2

                if (this.xtcMaterial.SelectedTabPage == this.xtpMaterialDetail)
                {
                    string mtlSeq = view.GetFocusedRowCellValue("MTL_SEQ_NO").ToString();

                    this.GetBindingMaterial(mtlSeq);
                    
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
            this.txtMTL_CODE.Focus();
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
                        this.xtpMaterialDetail.PageVisible = false;
                        this.xtpMaterialList.PageEnabled = true;
                        this.xtcMaterial.SelectedTabPage = this.xtpMaterialList;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;
                    case eFormState.Edit:

                        this.GetBindingMaterial(this.txtMTL_SEQ_NO.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpMaterialDetail.PageVisible = false;
                        this.xtpMaterialList.PageEnabled = true;
                        this.xtcMaterial.SelectedTabPage = this.xtpMaterialList;

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

            this.xtpMaterialDetail.PageVisible = true;
            this.xtcMaterial.SelectedTabPage = this.xtpMaterialDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.txtMTL_CODE.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertMaterial();
                    break;
                case eFormState.Edit:
                    this.UpdateMaterial();
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
                using (OpenFileDialog fdlg = new OpenFileDialog 
                {
                    Title = "Open File", 
                    //InitialDirectory = @"c:\", 
                    Filter = "All files (*.*)|*.*|All files (*.*)|*.*", 
                    FilterIndex = 2, 
                    RestoreDirectory = true 
                })
                {
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        //this.picPROD_IMAGE.Image = Image.FromFile(fdlg.FileName);
                        this.DisplayImage(fdlg.FileName, this.picMTL_IMAGE);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.GetMaterialList(this.txtSearch.Text);
        }

        private void frmMaterial_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (this.FormState == eFormState.ReadOnly)
                    {
                        if (this.xtcMaterial.SelectedTabPage == this.xtpMaterialList)
                        {
                            this.btnApply.PerformClick();
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
                    this.GetMaterialList(editor.Text);
                    break;
                default:
                    break;
            }
        }

        private void frmMaterial_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayoutMultipleView(this.Name, this.grdProduct);
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdProdProcess.Views[0]);

            this.Controls.Clear();
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

                this.txtSTD_QTY.Focus();
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
                editor.Focus();
            }
            else
            {
                bool isValid = this.GetCustomerByCode(editor.Text);
                if (!isValid)
                {
                    //e.Cancel = true;
                    editor.Focus();
                }
            }
        }

    }
}