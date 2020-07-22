using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using DevExpress.XtraGrid;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Query_Popup;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Skins;
using System.Globalization;
using DevExpress.XtraGrid.Columns;

namespace HTN.BITS.UIL.PLASESS.Query
{
    public partial class frmProductTracking : BaseChildForm
    {
        public frmProductTracking()
        {
            InitializeComponent();

            base.LoadFormLayout();
            this.CustomInitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQrySummary);

            //this.gridController = new GridExportController(this.grdQrySummary.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbProductionTrans;
        //private GridExportController gridController;
        //private Color selectedRowColor;

        #endregion

        #region "Property Member"

        private bool IsTabSummarySelecting
        {
            get
            {
                return (this.xtcStockAsOn.SelectedTabPage == this.xtpSummary);
            }
        }

        public string FileName
        {
            get
            {
                return string.Format("ProductionTrack_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        public string FileName_Detail
        {
            get
            {
                return string.Format("ProductionTrackDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        #endregion

        #region "Mathod Member"

        private void InitializaLOVData()
        {
            try
            {
                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                    List<ProductionType> lstProdType = pdtBll.GetProductionTypeList();
                    lstProdType.Insert(0, new ProductionType { SEQ_NO = string.Empty, NAME = "(All)" });
                    this.luePRODUCTION_TYPE.Properties.DataSource = lstProdType;
                }
                
                using (MachineBLL mcBll = new MachineBLL())
                {
                    List<Machine> lstMachine = mcBll.GetMachineList(string.Empty);
                    lstMachine.Insert(0, new Machine { MC_NO = string.Empty, MACHINE_NAME = "(All)" });
                    this.lueMC_NO.Properties.DataSource = lstMachine;
                }

                using (PartyBLL partyBll = new PartyBLL())
                {
                    List<Party> lstParty = partyBll.LovPratyList("C", string.Empty);
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

        private void Query_ProductionTrack(string jobNo, string partyid, DateTime? fromDate, DateTime? todate, 
                                           string prodType, string mcno, string product)
        {
            try
            {
                base.ExecutionStart();
                //UiUtility.BeginProcessing("Please wait", this);
                base.BeginProcessing("Begin Load data...", "Please wait for Loading data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    this.dtbProductionTrans = queryBll.JobTrackingList(jobNo, partyid, fromDate, todate, prodType, mcno, product);
                }

                if (dtbProductionTrans != null)
                {
                    dtbProductionTrans.DefaultView.Sort = "JOB_NO,JOB_LOT";
                    this.ConditionsColumnView(this.grdQrySummary);
                    //this.ConditionsAdjustment(this.grdQrySummary);
                }

                this.grdQrySummary.DataSource = this.dtbProductionTrans;
                this.dntQryStkAsOn.DataSource = this.dtbProductionTrans;

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
                base.FinishedProcessing();
                ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
            }

        }

        private void ShowJobDetail(string jobNo, string jobLot)
        {
            try
            {
                using (frmQupJobTracking fDetail = new frmQupJobTracking())
                {
                    fDetail.JOB_NO = jobNo;
                    fDetail.JOB_LOT = jobLot;
                    fDetail.ShowDialog(this);
                    //UiUtility.ShowPopupForm(fDetail, this, true);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ConditionsColumnView(GridControl grd)
        {
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[14];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["INJ"];
                cnArr[0].Expression = @"[INJ] > 0 AND [ASSIGN_QTY] > [INJ]";
                cnArr[0].Appearance.ForeColor = Color.Red;
                cnArr[0].Appearance.Options.UseBackColor = true;
                cnArr[0].Appearance.Options.UseForeColor = true;
                cnArr[0].ApplyToRow = false;

                cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[1].Column = ((ColumnView)grd.MainView).Columns["TRM"];
                cnArr[1].Expression = @"[TRM] > 0 AND [ASSIGN_QTY] > [TRM]";
                cnArr[1].Appearance.ForeColor = Color.Red;
                cnArr[1].ApplyToRow = false;

                cnArr[2] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[2].Column = ((ColumnView)grd.MainView).Columns["ELC"];
                cnArr[2].Expression = @"[ELC] > 0 AND [ASSIGN_QTY] > [ELC]";
                cnArr[2].Appearance.ForeColor = Color.Red;
                cnArr[2].ApplyToRow = false;

                cnArr[3] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[3].Column = ((ColumnView)grd.MainView).Columns["ALL_"];
                cnArr[3].Expression = @"[ALL_] > 0 AND [ASSIGN_QTY] > [ALL_]";
                cnArr[3].Appearance.ForeColor = Color.Red;
                cnArr[3].ApplyToRow = false;

                cnArr[4] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[4].Column = ((ColumnView)grd.MainView).Columns["QC"];
                cnArr[4].Expression = @"[QC] > 0 AND [ASSIGN_QTY] > [QC]";
                cnArr[4].Appearance.ForeColor = Color.Red;
                cnArr[4].ApplyToRow = false;

                cnArr[5] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[5].Column = ((GridView)grd.MainView).Columns["FINISH"];
                cnArr[5].Expression = @"[FINISH] > 0 AND [ASSIGN_QTY] > [FINISH]";
                cnArr[5].Appearance.ForeColor = Color.Red;
                cnArr[5].ApplyToRow = false;

                cnArr[6] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[6].Column = ((ColumnView)grd.MainView).Columns["STOCK_IN"];
                cnArr[6].Expression = @"[STOCK_IN] > 0 AND [FINISH] > [STOCK_IN]";
                cnArr[6].Appearance.ForeColor = Color.Red;
                cnArr[6].ApplyToRow = false;

                cnArr[7] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[7].Column = ((ColumnView)grd.MainView).Columns["LOAD"];
                cnArr[7].Expression = @"[LOAD] > 0 AND [PICK] > [LOAD]";
                cnArr[7].Appearance.ForeColor = Color.Red;
                cnArr[7].ApplyToRow = false;

                cnArr[8] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[8].Column = ((ColumnView)grd.MainView).Columns["INJ_WIP"];
                cnArr[8].Expression = @"[INJ_WIP] > 0";
                cnArr[8].Appearance.ForeColor = Color.Red;
                cnArr[8].ApplyToRow = false;

                cnArr[9] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[9].Column = ((ColumnView)grd.MainView).Columns["TRM_WIP"];
                cnArr[9].Expression = @"[TRM_WIP] > 0";
                cnArr[9].Appearance.ForeColor = Color.Red;
                cnArr[9].ApplyToRow = false;

                cnArr[10] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[10].Column = ((ColumnView)grd.MainView).Columns["ELC_WIP"];
                cnArr[10].Expression = @"[ELC_WIP] > 0";
                cnArr[10].Appearance.ForeColor = Color.Red;
                cnArr[10].ApplyToRow = false;

                cnArr[11] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[11].Column = ((ColumnView)grd.MainView).Columns["PRQ_WIP"];
                cnArr[11].Expression = @"[PRQ_WIP] > 0";
                cnArr[11].Appearance.ForeColor = Color.Red;
                cnArr[11].ApplyToRow = false;

                cnArr[12] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[12].Column = ((ColumnView)grd.MainView).Columns["QC_WIP"];
                cnArr[12].Expression = @"[QC_WIP] > 0";
                cnArr[12].Appearance.ForeColor = Color.Red;
                cnArr[12].ApplyToRow = false;

                cnArr[13] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[13].Column = ((ColumnView)grd.MainView).Columns["FG_WIP"];
                cnArr[13].Expression = @"[FG_WIP] > 0";
                cnArr[13].Appearance.ForeColor = Color.Red;
                cnArr[13].ApplyToRow = false;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
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
            try
            {
                //BandedGridView bView = sender as BandedGridView;
                ColumnView columnView = sender as ColumnView;

                Point pt = columnView.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
                if ((info.RowHandle >= 0) && (info.InRow || info.InRowCell))
                {
                    string jobNo = columnView.GetRowCellValue(info.RowHandle, "JOB_NO").ToString();
                    string jobLot = columnView.GetRowCellValue(info.RowHandle, "JOB_LOT").ToString();

                    this.ShowJobDetail(jobNo, jobLot);
                }
            }
            catch (Exception ex)
            {
            }
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

            ColumnView colView = (ColumnView)this.grdQrySummary.MainView;
            FontStyle fontStyle = FontStyle.Regular;
            if (e.RowHandle == colView.FocusedRowHandle)
            {
                switch (e.Column.FieldName)
                {
                    case "INJ":
                        fontStyle = FontStyle.Regular;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "TRM":
                        fontStyle = FontStyle.Regular;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "ELC":
                        fontStyle = FontStyle.Regular;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "ALL_":
                        fontStyle = FontStyle.Regular;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "QC":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "NG":
                        fontStyle = FontStyle.Regular;
                        isAdjust = true;
                        break;
                    case "REP":
                        fontStyle = FontStyle.Regular;
                        isAdjust = true;
                        break;
                    case "LOT_NG":
                        fontStyle = FontStyle.Regular;
                        isAdjust = true;
                        break;
                    case "FINISH":
                        isAdjust = true;
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ASSIGN_QTY", e.Column.FieldName);
                        break;
                    case "STOCK_IN":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "FINISH", e.Column.FieldName);
                        break;
                    case "LOAD":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "PICK", e.Column.FieldName);
                        break;
                    case "INJ_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "INJ_WIP");
                        break;
                    case "TRM_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "TRM_WIP");
                        break;
                    case "ELC_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "ELC_WIP");
                        break;
                    case "PRQ_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "PRQ_WIP");
                        break;
                    case "QC_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "QC_WIP");
                        break;
                    case "FG_WIP":
                        fontStyle = FontStyle.Bold;
                        isAdjust = this.ColumnViewIsAdjust(colView, e.RowHandle, "FG_WIP");
                        break;
                    default:
                        break;
                }

                if (isAdjust)
                {
                    //Apply the appearance of the SelectedRow
                    if (baseView.GetType() == typeof(GridView))
                    {
                        e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
                    }
                    else if (baseView.GetType() == typeof(BandedGridView))
                    {
                        e.Appearance.Assign(((BandedGridView)baseView).PaintAppearance.SelectedRow);
                    }
                    else
                    {
                        //nothing
                    }

                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                    //e.Appearance.Options.UseForeColor = true;
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, fontStyle);
                }
            }
        }

        private bool ColumnViewIsAdjust(ColumnView colView, int rowIndex, string columnNameCompare, string columnValue)
        {
            bool isAdjust = false;
            int iCompare = Convert.ToInt32(colView.GetRowCellValue(rowIndex, columnNameCompare), NumberFormatInfo.CurrentInfo);
            int iValue = Convert.ToInt32(colView.GetRowCellValue(rowIndex, columnValue), NumberFormatInfo.CurrentInfo);

            if (iValue > 0 && iValue < iCompare)
            {
                isAdjust = true;
            }

            return isAdjust;
        }

        private bool ColumnViewIsAdjust(ColumnView colView, int rowIndex, string columnNameCompare)
        {
            bool isAdjust = false;
            int iCompare = Convert.ToInt32(colView.GetRowCellValue(rowIndex, columnNameCompare), NumberFormatInfo.CurrentInfo);

            if (iCompare > 0)
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
            if (this.IsTabSummarySelecting)
            {
                this.ChangeView(eViewType.GridView, this.grdQrySummary);
            }
            //else
            //{
            //    this.ChangeView(eViewType.GridView, this.grdQrySubMTL);
            //}
        }

        private void bbiCardView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabSummarySelecting)
            {
                this.ChangeView(eViewType.CardView, this.grdQrySummary);
            }
            //else
            //{
            //    this.ChangeView(eViewType.CardView, this.grdQrySubMTL);
            //}
        }

        private void bbiBandView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (this.IsTabSummarySelecting)
            {
                this.ChangeView(eViewType.BandedView, this.grdQrySummary);
            }
            //else
            //{
            //    this.ChangeView(eViewType.BandedView, this.grdQrySubMTL);
            //}
        }

        private void bbiAdvView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (this.IsTabSummarySelecting)
            {
                this.ChangeView(eViewType.AdvanceView, this.grdQrySummary);
            }
            //else
            //{
            //    this.ChangeView(eViewType.AdvanceView, this.grdQrySubMTL);
            //}
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

        private void frmProductTracking_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;

            ////            selectedRowColor = SkinManager.Default.GetSkin(SkinProductId.Common,
            ////this.grdQrySummary.LookAndFeel).Colors["Highlight"];

            //this.dteFromDate.DateTime = DateTime.Now;
            //this.dteToDate.DateTime = DateTime.Now;
            //this.InitializaLOVData();

            //this.btnRefresh.Focus();            
        }

        private void frmProductTracking_LoadCompleted()
        {
            this.KeyPreview = true;

            this.dteFromDate.DateTime = DateTime.Now;
            this.dteToDate.DateTime = DateTime.Now;
            this.InitializaLOVData();

            this.btnRefresh.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string prodType = string.Empty;
            string mcNo = string.Empty;
            string partyID = string.Empty;

            if(this.luePRODUCTION_TYPE.EditValue != null)
            {
                prodType = (string)this.luePRODUCTION_TYPE.EditValue;
            }

            if(this.lueMC_NO.EditValue != null)
            {
                mcNo = (string)this.lueMC_NO.EditValue;
            }

            if(this.lueCUSTOMER.EditValue != null)
            {
                partyID = (string)this.lueCUSTOMER.EditValue;
            }

            this.Query_ProductionTrack(string.Empty, partyID, this.dteFromDate.DateTime, this.dteToDate.DateTime,
                                       prodType, mcNo, this.txtProduct.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductTracking_KeyUp(object sender, KeyEventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.xtpSummary.PageEnabled = true;
            this.xtcStockAsOn.SelectedTabPage = this.xtpSummary;
        }

        private void frmProductTracking_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveMainViewLayout(this.Name, this.grdQrySummary);
            //base.SaveGridLayoutMultipleView(this.Name, this.grdQrySummary); //, 
            this.Controls.Clear();
        }

        private void grvQrySummary_DataSourceChanged(object sender, EventArgs e)
        {
        }



    }
}