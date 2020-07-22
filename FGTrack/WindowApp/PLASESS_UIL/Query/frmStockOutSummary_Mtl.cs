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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Query_Popup;

namespace HTN.BITS.UIL.PLASESS.Query
{
    public partial class frmStockOutSummary_Mtl : BaseChildForm
    {
        public frmStockOutSummary_Mtl()
        {
            InitializeComponent();

            this.CustomInitializeComponent();

            //base.LoadGridLayoutMultipleView(this.Name, this.grdQrySummary);
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQrySummary);

            //this.gridController = new GridExportController(this.grdQrySummary.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbStockOutSummary;
        //GridExportController gridController;

        #endregion

        #region "Property Member"

        public string FileName
        {
            get
            {
                return string.Format("StockOutSummary_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        #endregion

        #region "Mathod Member"

        private void InitializaLOVData()
        {
            try
            {
                //for warehouse
                //using (QueryBLL queryBll = new QueryBLL())
                //{
                //    List<Warehouse> lstWH = queryBll.GetWarehouse();
                //    if (lstWH != null)
                //    {
                //        lstWH.Insert(0, new Warehouse { SEQ_NO = string.Empty, NAME = "(All)" });
                //        this.lueWarehouse.Properties.DataSource = lstWH;
                //        this.rps_lueWH.DataSource = lstWH;
                //    }
                //}
                using (MaterialBLL mtlBll = new MaterialBLL())
                {
                    List<Location> lstLoc = mtlBll.GetLocationList();
                    if (lstLoc != null)
                    {
                        // this.grvQrySummary_rps_lueLocation.DataSource = lstLoc;

                        lstLoc.Insert(0, new Location { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.lueWarehouse.Properties.DataSource = lstLoc;
                        this.rps_lueWH.DataSource = lstLoc;
                    }
                }

                //for party
                using (PartyBLL partyBll = new PartyBLL())
                {
                    List<Party> lstParty = partyBll.LovPratyList("V", string.Empty);
                    lstParty.Insert(0, new Party { PARTY_ID = string.Empty, PARTY_NAME = "(All)" });
                    this.lueCUSTOMER.Properties.DataSource = lstParty;
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

        private void Query_Stock_Out_Summary(string whid, string partyid, string product, DateTime? fromDate, DateTime? toDate, string shift)
        {
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    this.dtbStockOutSummary = queryBll.StockOutSummary_Mtl(whid, partyid, product, fromDate, toDate);
                }

                if (this.dtbStockOutSummary != null)
                {
                    this.ConditionsColumnView(this.grdQrySummary);
                }

                this.grdQrySummary.DataSource = this.dtbStockOutSummary;
                this.dntQryStkAsOn.DataSource = this.dtbStockOutSummary;

                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                this.grdQrySummary.DataSource = null;
                this.dntQryStkAsOn.DataSource = null;

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
                base.FinishedProcessing();
            }

        }

        
        private void ConditionsColumnView(GridControl grd)
        {
            try
            {
                //StyleFormatCondition[] cnArr = new StyleFormatCondition[2];

                //cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                //cnArr[0].Column = ((ColumnView)grd.MainView).Columns["PENDING_QTY"];
                //cnArr[0].Expression = @"[PENDING_QTY] > 0";
                //cnArr[0].Appearance.ForeColor = Color.Red;
                //cnArr[0].Appearance.Options.UseBackColor = true;
                //cnArr[0].Appearance.Options.UseForeColor = true;
                //cnArr[0].ApplyToRow = false;

                //cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
                //cnArr[1].Column = ((ColumnView)grd.MainView).Columns["PENDING_BOX"];
                //cnArr[1].Expression = @"[PENDING_BOX] > 0";
                //cnArr[1].Appearance.ForeColor = Color.Red;
                //cnArr[1].ApplyToRow = false;

                //((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //private void ShowStockOutSummaryDetail(string refNo, string productNo, DateTime outDate)
        //{
        //    try
        //    {
        //        using (frmQupStockOutSummaryDetial_Mtl fDetail = new frmQupStockOutSummaryDetial_Mtl() { REF_NO = refNo, PRODUCT_NO = productNo, OUT_DATE = outDate })
        //        {
        //            fDetail.ShowDialog(this);
        //            //UiUtility.ShowPopupForm(fDetail, this, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        private void Query_Stock_Out_Summary_Dtl(string mtlCode, string mtlName, string mtlType)
        {
            //TimeSpan executionTime = new TimeSpan();
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    UiUtility.BeginProcessing("Please wait", this);

            //    DataTable dtbStkAsOnDtl = null;
            //    using (QueryBLL queryBll = new QueryBLL())
            //    {
            //        //dtbStkAsOnDtl = queryBll.StockAsOn_Detail(mtlCode, mtlName, mtlType);
            //        //executionTime = queryBll.ExecutionTime;
            //    }

            //    this.grdQryDetail.DataSource = dtbStkAsOnDtl;
            //}
            //catch (Exception ex)
            //{
            //    Cursor.Current = Cursors.Default;
            //    UiUtility.EndProcessing();

            //    this.grdQryDetail.DataSource = null;

            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //    UiUtility.EndProcessing();
            //    ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = "Execute Time: " + UiUtility.TimeSpanInWords(executionTime);
            //    //GridView view = (GridView)this.grdQryDetail.Views[0];
            //    //UiUtility.RemoveActiveRow(view);
            //}

        }

        private void Query_AdvanceSearch()
        {
            //frmAdvQryStock fQryAdvSearch = new frmAdvQryStock();

            //DialogResult result = UiUtility.ShowPopupForm(fQryAdvSearch, this, true);

            //if (result == DialogResult.OK)
            //{
            //    this._MTL_CODE_SEARCH = fQryAdvSearch.MTL_CODE;
            //    this._MTL_NAME = fQryAdvSearch.MTL_NAME;
            //    this._MTL_TYPE = fQryAdvSearch.MTL_TYPE;

            //    try
            //    {
            //        if (this.IsTabSummarySelecting)
            //        {
            //            this.Query_StockAsOn(this._MTL_CODE_SEARCH, this._MTL_NAME, this._MTL_TYPE);
            //        }
            //        else
            //        {
            //            this.Query_StockAsOn_Dtl(this._MTL_CODE_SEARCH, this._MTL_NAME, this._MTL_TYPE);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //    }
            //    finally
            //    {

            //    }
            //}
        }

        #endregion

        #region "Custom Event Handle"

        private void GridControl_DoubleClick(object sender, EventArgs e)
        {
            //BandedGridView bView = sender as BandedGridView;
            ColumnView columnView = sender as ColumnView;

            Point pt = columnView.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
            if (info.InRow || info.InRowCell)
            {
              //  if (columnView.GetRowCellValue(info.RowHandle, "MTL_CODE") != null)
              //  {
                    string refNo = columnView.GetRowCellValue(info.RowHandle, "REF_NO").ToString();
                    string productNo = columnView.GetRowCellValue(info.RowHandle, "MTL_CODE").ToString();
                    DateTime outDate = Convert.ToDateTime(columnView.GetRowCellValue(info.RowHandle, "OUT_DATE"));

                    this.ShowStockOutSummaryDetail(refNo, productNo, outDate);
               // }
            }
        }

        private void ShowStockOutSummaryDetail(string refNo, string mtl_code, DateTime outDate)
        {
            try
            {
                using (frmQupStockOutSummaryDetial_Mtl fDetail = new frmQupStockOutSummaryDetial_Mtl())
                {
                    fDetail.REF_NO = refNo;
                    fDetail.PRODUCT_NO = mtl_code;
                    fDetail.OUT_DATE = outDate;

                    fDetail.ShowDialog(this);
                    //UiUtility.ShowPopupForm(fDetail, this, true);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
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

            //bool isAdjust = false;
            //BaseView baseView = sender as BaseView;

            //ColumnView colView = (ColumnView)this.grdQrySummary.MainView;
            //FontStyle fontStyle = FontStyle.Regular;
            //if (e.RowHandle == colView.FocusedRowHandle)
            //{
            //    switch (e.Column.FieldName)
            //    {
            //        case "PENDING_QTY":
            //            fontStyle = FontStyle.Bold;
            //            isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, e.Column.FieldName);
            //            break;
            //        case "PENDING_BOX":
            //            fontStyle = FontStyle.Bold;
            //            isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, e.Column.FieldName);
            //            break;
            //        default:
            //            break;
            //    }

            //    if (isAdjust)
            //    {
            //        //Apply the appearance of the SelectedRow
            //        if (baseView.GetType() == typeof(GridView))
            //        {
            //            e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
            //        }
            //        else if (baseView.GetType() == typeof(BandedGridView))
            //        {
            //            e.Appearance.Assign(((BandedGridView)baseView).PaintAppearance.SelectedRow);
            //        }
            //        else
            //        {
            //            //nothing
            //        }

            //        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //        //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
            //        //e.Appearance.Options.UseForeColor = true;
            //        e.Appearance.ForeColor = Color.Red;
            //        e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, fontStyle);
            //    }
            //}
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
            this.GridControl_DoubleClick(this.grdQrySummary.MainView, e);
        }

        #endregion

        #region "Button Export Referance"

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToXLS(this.FileName + ".xls", "Microsoft Excel Document", "Microsoft Excel|*.xls");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.XLS, this.FileName + ".xls", null);
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToXLSX(this.FileName + ".xlsx", "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToPDF(this.FileName + ".pdf", "PDF Document", "PDF Files|*.pdf");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToRTF(this.FileName + ".rtf", "RTF Document", "RTF Files|*.rtf");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToTEXT(this.FileName + ".txt", "Text Document", "Text Files|*.txt");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToHTML(this.FileName + ".html", "HTML Document", "HTML Files|*.html");
            base.ViewExportToExcel(this.grdQrySummary.Views[0], GridExportType.HTML, this.FileName + ".html", null);
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
            this.ChangeView(eViewType.GridView, this.grdQrySummary);
        }

        private void bbiCardView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.CardView, this.grdQrySummary);
        }

        private void bbiBandView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.BandedView, this.grdQrySummary);
        }

        private void bbiAdvView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.AdvanceView, this.grdQrySummary);
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
                    gridCtl.MainView = this.grvQrySummary;
                    this.grvQrySummary.ExpandAllGroups();

                    //GridView gView = (GridView)gridCtl.MainView;
                    //UiUtility.RemoveActiveRow(gView);

                    break;
                case eViewType.CardView:
                    //gridCtl.MainView = this.cdvQrySummary;
                    break;
                case eViewType.BandedView:
                    gridCtl.MainView = this.brvQrySummary;
                    this.brvQrySummary.ExpandAllGroups();

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

        private void frmStockOutSummary_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVData();
            //this.dteFromDate.EditValue = DateTime.Now;
            //this.dteToDate.EditValue = DateTime.Now;
            //this.btnRefresh.Focus();
        }

        private void frmStockOutSummary_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVData();
            this.dteFromDate.EditValue = DateTime.Now;
            this.dteToDate.EditValue = DateTime.Now;
            this.btnRefresh.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string whid, partyid, product, shift;
            DateTime? fromDate, toDate;

            if (this.lueWarehouse.EditValue != null)
                whid = (string)this.lueWarehouse.EditValue;
            else
                whid = string.Empty;

            if (this.lueCUSTOMER.EditValue != null)
                partyid = (string)this.lueCUSTOMER.EditValue;
            else
                partyid = string.Empty;

            product = this.txtProduct.Text;

            if (this.dteFromDate.EditValue != null)
                fromDate = this.dteFromDate.DateTime;
            else
                fromDate = null;

            if (this.dteToDate.EditValue != null)
                toDate = this.dteToDate.DateTime;
            else
                toDate = null;

            shift = string.Empty;

            this.Query_Stock_Out_Summary(whid, partyid, product, fromDate, toDate, shift);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStockOutSummary_KeyUp(object sender, KeyEventArgs e)
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

        private void frmStockOutSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }


    }
}