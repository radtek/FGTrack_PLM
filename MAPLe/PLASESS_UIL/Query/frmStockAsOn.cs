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
using HTN.BITS.BLL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Columns;
using HTN.BITS.BEL.PLASESS;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.Query_Popup;


namespace HTN.BITS.UIL.PLASESS.Query
{
    public partial class frmStockAsOn : BaseChildForm
    {
        public frmStockAsOn()
        {
            InitializeComponent();

            this.CustomInitializeComponent();

            //base.LoadGridLayoutMultipleView(this.Name, this.grdQrySummary);
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQrySummary);

            //this.gridController = new GridExportController(this.grdQrySummary.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbStockAsOn;
        //private GridExportController gridController;

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
                return string.Format("StockAsOn_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        public string FileName_Detail
        {
            get
            {
                return string.Format("StockAsOnDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        #endregion

        #region "Mathod Member"

        private void InitializaLOVData()
        {
            try
            {
                //For Warehouse
                using (QueryBLL queryBll = new QueryBLL())
                {
                    List<Warehouse> lstWH = queryBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.grvQrySummary_rps_lueWH.DataSource = lstWH;

                        lstWH.Insert(0, new Warehouse { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.lueWarehouse.Properties.DataSource = lstWH;

                    }
                }

                //for party
                using (PartyBLL partyBll = new PartyBLL())
                {
                    List<Party> lstParty = partyBll.LovPratyList("C", string.Empty);

                    if (lstParty != null)
                    {
                        this.grvQrySummary_rps_luePARTY.DataSource = lstParty;

                        lstParty.Insert(0, new Party { PARTY_ID = string.Empty, PARTY_NAME = "(All)" });
                        this.lueCUSTOMER.Properties.DataSource = lstParty;
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

        //private void Query_StockAsOn(string whid, string partyid, string product)
        //{
        //    try
        //    {
        //        base.ExecutionStart();
        //        base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

        //        using (QueryBLL queryBll = new QueryBLL())
        //        {
        //            this.dtbStockAsOn = queryBll.StockAsOn(whid, partyid, product);
        //        }


        //        string expression = string.Empty;
        //        switch (this.GetDefaultViewType(this.grdQrySummary))
        //        {
        //            case eViewType.GridView:
        //                GridView gView = (GridView)this.grdQrySummary.MainView;
        //                foreach (GridColumn column in gView.VisibleColumns)
        //                {
        //                    switch (column.FieldName)
        //                    {
        //                        case "ALLOC_STK":
        //                            expression += "ALLOC_STK+";
        //                            break;
        //                        case "HOLD_QTY":
        //                            expression += "HOLD_QTY+";
        //                            break;
        //                        case "FREE_STK":
        //                            expression += "FREE_STK+";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                break;
        //            case eViewType.CardView:
        //               // CardView cView = (CardView)this.grdQrySummary.MainView;

        //                break;
        //            case eViewType.BandedView:
        //                BandedGridView bView = (BandedGridView)this.grdQrySummary.MainView;
        //                foreach (GridColumn column in bView.VisibleColumns)
        //                {
        //                    switch (column.FieldName)
        //                    {
        //                        case "ALLOC_STK":
        //                            expression += "ALLOC_STK+";
        //                            break;
        //                        case "HOLD_QTY":
        //                            expression += "HOLD_QTY+";
        //                            break;
        //                        case "FREE_STK":
        //                            expression += "FREE_STK+";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                break;
        //            case eViewType.AdvanceView:
        //                //AdvBandedGridView aView = (AdvBandedGridView)this.grdQryMainMTL.MainView;
        //                break;
        //            default:
        //                break;
        //        }

        //        if (!string.IsNullOrEmpty(expression))
        //        {
        //            expression = expression.Substring(0, expression.LastIndexOf("+"));

        //            if (this.dtbStockAsOn.Columns.IndexOf("TOTAL_QTY") == -1)
        //            {
        //                this.dtbStockAsOn.Columns.Add("TOTAL_QTY", typeof(int), expression);
        //            }
        //            else
        //            {
        //                this.dtbStockAsOn.Columns["TOTAL_QTY"].Expression = expression;
        //            }
        //        }

        //        if (dtbStockAsOn != null)
        //        {
        //            dtbStockAsOn.DefaultView.Sort = "WH_NAME,PRODUCT_NO";
        //        }

        //        this.grdQrySummary.DataSource = this.dtbStockAsOn;
        //        this.dntQryStkAsOn.DataSource = this.dtbStockAsOn;

        //        base.ExecutionStop();
        //    }
        //    catch (Exception ex)
        //    {
        //        base.FinishedProcessing();

        //        this.grdQrySummary.DataSource = null;
        //        this.dntQryStkAsOn.DataSource = null;

        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //    finally
        //    {
        //        ((frmMainMenu)this.ParentForm).ExecuteTime.Caption = base.ExecuteTime;
        //        base.FinishedProcessing();
        //    }

        //}

        private void Query_StockAsOn(string whid, string partyid, string product)
        {
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    this.dtbStockAsOn = queryBll.StockAsOn(whid, partyid, product);
                }

                string expressionPCS = string.Empty;
                string expressionBOX = string.Empty;

                switch (this.GetDefaultViewType(this.grdQrySummary))
                {
                    case eViewType.GridView:
                        GridView gView = (GridView)this.grdQrySummary.MainView;
                        foreach (GridColumn column in gView.VisibleColumns)
                        {
                            switch (column.FieldName)
                            {
                                case "ALLOC_QTY":
                                    expressionPCS += "ALLOC_QTY+";
                                    break;
                                case "FREE_QTY":
                                    expressionPCS += "FREE_QTY+";
                                    break;
                                case "ALLOC_BOX":
                                    expressionBOX += "ALLOC_BOX+";
                                    break;
                                case "FREE_BOX":
                                    expressionBOX += "FREE_BOX+";
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;
                    case eViewType.CardView:
                        // CardView cView = (CardView)this.grdQrySummary.MainView;

                        break;
                    case eViewType.BandedView:
                        BandedGridView bView = (BandedGridView)this.grdQrySummary.MainView;
                        foreach (GridColumn column in bView.VisibleColumns)
                        {
                            switch (column.FieldName)
                            {
                                case "ALLOC_QTY":
                                    expressionPCS += "ALLOC_QTY+";
                                    break;
                                case "FREE_QTY":
                                    expressionPCS += "FREE_QTY+";
                                    break;
                                case "ALLOC_BOX":
                                    expressionBOX += "ALLOC_BOX+";
                                    break;
                                case "FREE_BOX":
                                    expressionBOX += "FREE_BOX+";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case eViewType.AdvanceView:
                        //AdvBandedGridView aView = (AdvBandedGridView)this.grdQryMainMTL.MainView;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(expressionPCS))
                {
                    expressionPCS = expressionPCS.Substring(0, expressionPCS.LastIndexOf("+"));

                    if (this.dtbStockAsOn.Columns.IndexOf("TOTAL_QTY") == -1)
                    {
                        this.dtbStockAsOn.Columns.Add("TOTAL_QTY", typeof(int), expressionPCS);
                    }
                    else
                    {
                        this.dtbStockAsOn.Columns["TOTAL_QTY"].Expression = expressionPCS;
                    }
                }

                if (!string.IsNullOrEmpty(expressionBOX))
                {
                    expressionBOX = expressionBOX.Substring(0, expressionBOX.LastIndexOf("+"));

                    if (this.dtbStockAsOn.Columns.IndexOf("TOTAL_BOX") == -1)
                    {
                        this.dtbStockAsOn.Columns.Add("TOTAL_BOX", typeof(int), expressionBOX);
                    }
                    else
                    {
                        this.dtbStockAsOn.Columns["TOTAL_BOX"].Expression = expressionBOX;
                    }
                }

                if (dtbStockAsOn != null)
                {
                    dtbStockAsOn.DefaultView.Sort = "WH_ID, PARTY_ID, PRODUCT_NO";
                }

                this.grdQrySummary.DataSource = this.dtbStockAsOn;
                this.dntQryStkAsOn.DataSource = this.dtbStockAsOn;

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

        private void ShowStockAsOnDateDetail(string whid, string productno, DateTime? asOnDate)
        {
            try
            {
                using (frmQupStockAsOnDate fDetail = new frmQupStockAsOnDate { WH_ID = whid, PRODUCT_NO = productno, AS_ON_DATE = asOnDate })
                {
                    fDetail.ShowDialog(this);
                }
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

        //private void GridControl_DoubleClick(object sender, EventArgs e)
        //{
        //    //BandedGridView bView = sender as BandedGridView;
        //    ColumnView columnView = sender as ColumnView;

        //    Point pt = columnView.GridControl.PointToClient(Control.MousePosition);

        //    GridHitInfo info = columnView.CalcHitInfo(pt) as GridHitInfo;
        //    if (info.InRow || info.InRowCell)
        //    {
        //        string whid = string.Empty;
        //        if (this.lueWarehouse.EditValue != null)
        //        {
        //            whid = this.lueWarehouse.EditValue.ToString();
        //        }

        //        string productno = columnView.GetRowCellValue(info.RowHandle, "PRODUCT_NO").ToString();

        //        this.ShowStockAsOnDateDetail(whid, productno, DateTime.Now);
        //    }
        //}

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
                    string whid = columnView.GetRowCellValue(info.RowHandle, "WH_ID").ToString();
                    string productno = columnView.GetRowCellValue(info.RowHandle, "PRODUCT_NO").ToString();

                    //DateTime asOnDate = DateTime.ParseExact(dtpDateSelect.Text, "dd-MM-yyyy HH:mm", DateTimeFormatInfo.CurrentInfo);
                    this.ShowStockAsOnDateDetail(whid, productno, DateTime.Now);
                }
            }
            catch (Exception ex)
            { 
            }
        }

        private void GridControl_ColumnPositionChanged(object sender, EventArgs e)
        {
            try
            {
                ColumnView gView = this.grdQrySummary.MainView as ColumnView;

                string expression = string.Empty;

                //GridView gView = (GridView)this.grdQrySummary.MainView;

                foreach (GridColumn column in gView.VisibleColumns)
                {
                    switch (column.FieldName)
                    {
                        case "ALLOC_STK":
                            expression += "ALLOC_STK+";
                            break;
                        case "HOLD_QTY":
                            expression += "HOLD_QTY+";
                            break;
                        case "FREE_STK":
                            expression += "FREE_STK+";
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(expression))
                {
                    expression = expression.Substring(0, expression.LastIndexOf("+"));
                    //this.dtbStkAsOn.Columns["TOTAL_TEST"].Expression = expression;
                    if (this.dtbStockAsOn.Columns.IndexOf("TOTAL_QTY") == -1)
                    {
                        this.dtbStockAsOn.Columns.Add("TOTAL_QTY", typeof(int), expression);
                    }
                    else
                    {
                        this.dtbStockAsOn.Columns["TOTAL_QTY"].Expression = expression;
                    }
                }


                this.grdQrySummary.DataSource = this.dtbStockAsOn;
                gView.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
        }

        #endregion "Button Change View Referance"

        private void frmStockAsOn_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVData();
            //this.btnRefresh.Focus();
        }

        private void frmStockAsOn_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVData();
            this.btnRefresh.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string whid, partyid;

            if (this.lueWarehouse.EditValue != null)
                whid = (string)this.lueWarehouse.EditValue;
            else
                whid = string.Empty;

            if (this.lueCUSTOMER.EditValue != null)
                partyid = (string)this.lueCUSTOMER.EditValue;
            else
                partyid = string.Empty;


            this.Query_StockAsOn(whid, partyid, this.txtProduct.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStockAsOn_KeyUp(object sender, KeyEventArgs e)
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

        private void frmStockAsOn_FontChanged(object sender, EventArgs e)
        {
            base.SaveGridLayoutMultipleView(this.Name, this.grdQrySummary);
            this.Controls.Clear();
        }

        private void frmStockAsOn_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayoutMultipleView(this.Name, this.grdQrySummary);
            this.Controls.Clear();
        }





        
    }
}