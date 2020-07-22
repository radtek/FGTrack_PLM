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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace HTN.BITS.UIL.PLASESS.Interface
{
    public partial class frmAdjustInInterface : BaseChildFormPopup
    {
        public frmAdjustInInterface()
        {
            InitializeComponent();
            this.CustomInitializeComponent();

            //base.LoadGridLayoutMultipleView(this.Name, this.grdQrySummary);
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdAdjustIn);

            //this.gridController = new GridExportController(this.grdAdjustIn.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbAdjustInInfData;
        //GridExportController gridController;

        #endregion

        #region "Property Member"

        public string FileName
        {
            get
            {
                return string.Format("FGAdjustInInterfaceData_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        #endregion

        #region "Mathod Member"

        private void InitializaLOVData()
        {
            try
            {
                //for post type
                using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                {
                    List<PostType> lstPostType = inDataBll.PostTypeList();
                    if (lstPostType != null)
                    {
                        lstPostType.Insert(0, new PostType { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.luePostType.Properties.DataSource = lstPostType;

                        //set defautl
                        this.luePostType.EditValue = "NEW";
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void CustomInitializeComponent()
        {
            this.btnRefresh.Image = base.Language.GetBitmap("ButtonRefresh");
            this.ddbExport.Image = base.Language.GetBitmap("ButtonExport");

            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            this.bbiExportPDF.Glyph = base.Language.GetBitmap("DropdownPDF");
            this.bbiExportRTF.Glyph = base.Language.GetBitmap("DropdownRTF");
            this.bbiExportText.Glyph = base.Language.GetBitmap("DropdownTXT");
            this.bbiExportHTML.Glyph = base.Language.GetBitmap("DropdownHTML");

            this.bbiPrintPreview.Glyph = base.Language.GetBitmap("DropdownPrint");
            this.bbiPrintPreview.Enabled = false;

            this.ddbView.Image = base.Language.GetBitmap("ButtonView");

            this.bbiGridView.Glyph = base.Language.GetBitmap("DropdownGridView");
            this.bbiCardView.Glyph = base.Language.GetBitmap("DropdownCardView");
            this.bbiBandView.Glyph = base.Language.GetBitmap("DropdownBandedView");
            this.bbiAdvView.Glyph = base.Language.GetBitmap("DropdownAdvView");

            this.bbiAdvView.Enabled = false;

        }

        private eViewType GetDefaultViewType(GridControl gridCtl)
        {
            eViewType result = eViewType.GridView;

            switch (gridCtl.Views[0].GetType().Name)
            {
                case "GridView":
                    result = eViewType.GridView;
                    break;
                case "CardView":
                    result = eViewType.CardView;
                    break;
                case "BandedGridView":
                    result = eViewType.BandedView;
                    break;
                case "AdvBandedGridView":
                    result = eViewType.AdvanceView;
                    break;
                default:
                    break;
            }

            return result;
        }

        private void FgAdjustInInterfaceData(string posttype, string productNo, string postRef, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Loading Data...", "Please Waiting for Loading Data");

                using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                {
                    this.dtbAdjustInInfData = inDataBll.IfInAdjustInList(posttype, productNo, postRef, fromDate, toDate, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (this.dtbAdjustInInfData != null)
                {
                    this.ConditionsColumnView(this.grdAdjustIn);
                }

                this.grdAdjustIn.DataSource = this.dtbAdjustInInfData;
                this.dntQryStkAsOn.DataSource = this.dtbAdjustInInfData;

                //check enable button
                this.CheckEnablePostData("[DATA_TYPE] = 'NEW'");
                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                
                UiUtility.EndProcessing();

                this.grdAdjustIn.DataSource = null;
                this.dntQryStkAsOn.DataSource = null;

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
                base.FinishedProcessing();
            }

        }

        private void CheckEnablePostData(string expression)
        {
            if (this.dtbAdjustInInfData != null)
            {
                DataRow[] rows = this.dtbAdjustInInfData.Select(expression);
                if (rows.Length > 0)
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

        private void ConditionsColumnView(GridControl grd)
        {
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[1];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["DATA_TYPE"];
                cnArr[0].Expression = @"[DATA_TYPE] == 'NEW'";
                cnArr[0].Appearance.ForeColor = Color.Red;
                cnArr[0].Appearance.Options.UseBackColor = true;
                cnArr[0].Appearance.Options.UseForeColor = true;
                cnArr[0].ApplyToRow = true;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void FGAdjustInInterfacePostData()
        {
            try
            {
                //ICollection<string> files;
                DataTable dtbPosted = null;
                string selectPath = string.Empty;
                string resultMsg = string.Empty;

                DialogResult result = this.fdbSelectFilePath.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    selectPath = this.fdbSelectFilePath.SelectedPath;
                    try
                    {
                        using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                        {
                            resultMsg = inDataBll.GetPostData("INTERFACE_PACK.IF_IN_ADJ_LIST", string.Empty, null, selectPath, ((frmMainMenu)this.ParentForm).UserID, out dtbPosted);
                        }

                        /*
                        if (files.Count > 0)
                        {
                            //copy to server
                            UiUtility.CopyFilesToServer(files);

                            this.OpenPath(files);

                            //Refresh screen
                            this.btnRefresh.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show(this, "Post Data has Problem!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                         * */

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
            //ColumnView columnView = sender as ColumnView;

            //Point pt = columnView.GridControl.PointToClient(Control.MousePosition);

            //GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
            //if (info.InRow || info.InRowCell)
            //{
            //    string whid = gView.GetRowCellValue(info.RowHandle, "WH_ID").ToString();
            //    string productno = gView.GetRowCellValue(info.RowHandle, "PRODUCT_NO").ToString();
            //    if (this.dtpDateSelect.EditValue != null)
            //    {
            //        DateTime asOnDate = DateTime.ParseExact(dtpDateSelect.Text, "dd-MM-yyyy HH:mm", DateTimeFormatInfo.CurrentInfo);
            //        this.ShowStockAsOnDateDetail(whid, productno, asOnDate);
            //    }
            //    else
            //        this.ShowStockAsOnDateDetail(whid, productno, null);
            //}
        }

        private void GridControl_ColumnPositionChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    ColumnView gView = this.grdQrySummary.MainView as ColumnView;

            //    string expressionPCS = string.Empty;
            //    string expressionBOX = string.Empty;

            //    //GridView gView = (GridView)this.grdQrySummary.MainView;

            //    foreach (GridColumn column in gView.VisibleColumns)
            //    {
            //        switch (column.FieldName)
            //        {
            //            case "ALLOC_QTY":
            //                expressionPCS += "ALLOC_QTY+";
            //                break;
            //            case "FREE_QTY":
            //                expressionPCS += "FREE_QTY+";
            //                break;
            //            case "ALLOC_BOX":
            //                expressionBOX += "ALLOC_BOX+";
            //                break;
            //            case "FREE_BOX":
            //                expressionBOX += "FREE_BOX+";
            //                break;
            //            default:
            //                break;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(expressionPCS))
            //    {
            //        expressionPCS = expressionPCS.Substring(0, expressionPCS.LastIndexOf("+"));

            //        if (this.dtbStockInSummary.Columns.IndexOf("TOTAL_QTY") == -1)
            //        {
            //            this.dtbStockInSummary.Columns.Add("TOTAL_QTY", typeof(int), expressionPCS);
            //        }
            //        else
            //        {
            //            this.dtbStockInSummary.Columns["TOTAL_QTY"].Expression = expressionPCS;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(expressionBOX))
            //    {
            //        expressionBOX = expressionBOX.Substring(0, expressionBOX.LastIndexOf("+"));

            //        if (this.dtbStockInSummary.Columns.IndexOf("TOTAL_BOX") == -1)
            //        {
            //            this.dtbStockInSummary.Columns.Add("TOTAL_BOX", typeof(int), expressionBOX);
            //        }
            //        else
            //        {
            //            this.dtbStockInSummary.Columns["TOTAL_BOX"].Expression = expressionBOX;
            //        }
            //    }


            //    this.grdQrySummary.DataSource = this.dtbStockInSummary;
            //    gView.RefreshData();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void GridControl_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.RowHandle >= 0)
            {
                //Row Active
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    //e.Info.DisplayText = string.Empty;
                    e.Info.ImageIndex = 0;
                }
            }
        }

        private void GridQrySummary_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            bool isAdjust = false;
            BaseView baseView = sender as BaseView;

            ColumnView colView = (ColumnView)this.grdAdjustIn.MainView;
            try
            {
                if (e.RowHandle == colView.FocusedRowHandle)
                {
                    if (colView.GetRowCellValue(e.RowHandle, "DATA_TYPE").ToString().Equals("NEW"))
                    {
                        isAdjust = true;
                    }

                    if (isAdjust)
                    {
                        //Apply the appearance of the SelectedRow
                        e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
                        e.Appearance.ForeColor = Color.Red;
                        
                        switch (e.Column.FieldName)
                        {
                            case "PRODUCT_NO":
                                e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
                                break;
                            case "QTY":
                                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool ColumnViewIsAdjust(ColumnView colView, int rowIndex, string columnValue)
        {
            bool isAdjust = false;
            int iValue = Convert.ToInt32(colView.GetRowCellValue(rowIndex, columnValue), NumberFormatInfo.CurrentInfo);

            if (iValue > 0)
            {
                isAdjust = true;
            }

            return isAdjust;
        }

        private void Respository_DoubleClick(object sender, EventArgs e)
        {
            this.GridControl_DoubleClick(this.grdAdjustIn.MainView, e);
        }

        #endregion

        #region "Button Export Referance"

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.XLS, this.FileName + ".xls", null);
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAdjustIn.Views[0], GridExportType.HTML, this.FileName + ".html", null);
        }

        private void ddbExport_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }

        #endregion "Button Export Referance"

        #region "Button Change View Referance"

        private void bbiGridView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.GridView, this.grdAdjustIn);
        }

        private void bbiCardView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.CardView, this.grdAdjustIn);
        }

        private void bbiBandView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.BandedView, this.grdAdjustIn);
        }

        private void bbiAdvView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.AdvanceView, this.grdAdjustIn);
        }

        private void ddbView_Click(object sender, EventArgs e)
        {
            DropDownButton ddbV = (DropDownButton)sender;
            ddbV.ShowDropDown();
        }

        private void ChangeView(eViewType viewType, GridControl gridCtl)
        {
            switch (viewType)
            {
                case eViewType.GridView:
                    gridCtl.MainView = this.grvAdjustIn;
                    this.grvAdjustIn.ExpandAllGroups();

                    //GridView gView = (GridView)gridCtl.MainView;
                    //UiUtility.RemoveActiveRow(gView);

                    break;
                case eViewType.CardView:
                    //gridCtl.MainView = this.cdvQrySummary;
                    break;
                case eViewType.BandedView:

                    //BandedGridView bView = (BandedGridView)gridCtl.MainView;
                    //UiUtility.RemoveActiveRow(bView);



                    break;
                case eViewType.AdvanceView:
                    break;
                default:
                    break;
            }

            this.ConditionsColumnView(gridCtl);
        }

        #endregion "Button Change View Referance"

        private void frmAdjustInInterface_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVData();
            //this.dteFromDate.EditValue = null;//DateTime.Now;
            //this.dteToDate.EditValue = null;//DateTime.Now;
            //this.btnRefresh.Focus();
            //this.btnRefresh.PerformClick();
        }

        private void frmAdjustInInterface_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVData();
            this.dteFromDate.EditValue = null;//DateTime.Now;
            this.dteToDate.EditValue = null;//DateTime.Now;
            this.btnRefresh.Focus();
            this.btnRefresh.PerformClick();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string posttype, productNo, postRef;
            DateTime? fromDate, toDate;


            if (this.luePostType.EditValue != null)
                posttype = (string)this.luePostType.EditValue;
            else
                posttype = string.Empty;

            productNo = this.txtProduct.Text;
            postRef = this.txtPostRef.Text;

            if (this.dteFromDate.EditValue != null)
                fromDate = this.dteFromDate.DateTime;
            else
                fromDate = null;

            if (this.dteToDate.EditValue != null)
                toDate = this.dteToDate.DateTime;
            else
                toDate = null;

            this.FgAdjustInInterfaceData(posttype, productNo, postRef, fromDate, toDate);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdjustInInterface_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        this.btnRefresh.PerformClick();
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

        private void frmAdjustInInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            this.FGAdjustInInterfacePostData();
        }


    }
}