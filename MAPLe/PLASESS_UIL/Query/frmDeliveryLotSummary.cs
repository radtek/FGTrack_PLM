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

namespace HTN.BITS.UIL.PLASESS.Query
{
    public partial class frmDeliveryLotSummary : BaseChildForm
    {
        public frmDeliveryLotSummary()
        {
            InitializeComponent();
            this.CustomInitializeComponent();

            //base.LoadGridLayoutMultipleView(this.Name, this.grdQrySummary);
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQrySummary);

            //this.gridController = new GridExportController(this.grdQrySummary.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbDeliveryLotSum;
        //GridExportController gridController;

        #endregion

        #region "Property Member"

        public string FileName
        {
            get
            {
                return string.Format("DeliveryLotSummary_{0:yyyyMMddHHmmss}", DateTime.Now);
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

        private void Query_Delivery_Lot_Summary(string prodtype, string product, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    this.dtbDeliveryLotSum = queryBll.DeliveryLotSummary(prodtype, product, fromDate, toDate);
                }

                if (this.dtbDeliveryLotSum != null)
                {
                    this.ConditionsColumnView(this.grdQrySummary);
                }

                this.grdQrySummary.DataSource = this.dtbDeliveryLotSum;
                this.dntQryStkAsOn.DataSource = this.dtbDeliveryLotSum;

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
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void Query_Production_Summary_Daily_Dtl(string mtlCode, string mtlName, string mtlType)
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

        private void ShowStockAsOnDateDetail(string whid, string productno, DateTime? asOnDate)
        {
            //try
            //{
            //    using (frmQupStockAsOnDate fDetail = new frmQupStockAsOnDate())
            //    {
            //        fDetail.WH_ID = whid;
            //        fDetail.PRODUCT_NO = productno;
            //        fDetail.AS_ON_DATE = asOnDate;

            //        fDetail.ShowDialog(this);
            //        //UiUtility.ShowPopupForm(fDetail, this, true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
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

        private void frmDeliveryLotSummary_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVData();
            //this.dteFromDate.EditValue = DateTime.Now;
            //this.dteToDate.EditValue = DateTime.Now;
            //this.btnRefresh.Focus();
        }

        private void frmDeliveryLotSummary_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVData();
            this.dteFromDate.EditValue = DateTime.Now;
            this.dteToDate.EditValue = DateTime.Now;
            this.btnRefresh.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string prodType, product, shift;
            DateTime? fromDate, toDate;

            if (this.luePRODUCTION_TYPE.EditValue != null)
                prodType = (string)this.luePRODUCTION_TYPE.EditValue;
            else
                prodType = string.Empty;

            product = this.txtProduct.Text;

            if (this.dteFromDate.EditValue != null)
                fromDate = this.dteFromDate.DateTime;
            else
                fromDate = null;

            if (this.dteToDate.EditValue != null)
                toDate = this.dteToDate.DateTime;
            else
                toDate = null;

            this.Query_Delivery_Lot_Summary(prodType, product, fromDate, toDate);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDeliveryLotSummary_KeyUp(object sender, KeyEventArgs e)
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

        private void frmDeliveryLotSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }


    }
}