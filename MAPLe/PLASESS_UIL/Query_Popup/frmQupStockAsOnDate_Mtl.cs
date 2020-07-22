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
using DevExpress.XtraGrid.Views.Grid;

namespace HTN.BITS.UIL.PLASESS.Query_Popup
{
    public partial class frmQupStockAsOnDate_Mtl : BaseDialogForm
    {
        public frmQupStockAsOnDate_Mtl()
        {
            InitializeComponent();
            this.CustomInitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQryDetail);
        }

        #region Variable Member

        private string _MTL_CODE;

        #endregion

        #region Property Member

        public string FileName_Detail
        {
            get
            {
                return string.Format("StockAsOnDateDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        public string MTL_CODE
        {
            get
            {
                return _MTL_CODE;
            }
            set
            {
                if (_MTL_CODE == value)
                    return;
                _MTL_CODE = value;
            }
        }

        #endregion

        #region "Button Export Referance"


        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.xls);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.XLS, this.FileName_Detail + ".xls", null);
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.xlsx);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.XLSX, this.FileName_Detail + ".xlsx", null);
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.pdf);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.PDF, this.FileName_Detail + ".pdf", null);
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.rtf);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.RTF, this.FileName_Detail + ".rtf", null);
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.txt);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.TEXT, this.FileName_Detail + ".txt", null);
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ExportDocument(eExportType.html);
            base.ViewExportToExcel(this.grdQryDetail.Views[0], GridExportType.HTML, this.FileName_Detail + ".html", null);
        }

        private void ddbExport_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }

        #endregion "Button Export Referance"

        #region Method Member

        private void CustomInitializeComponent()
        {
            this.btnRefreshDetail.Image = base.Language.GetBitmap("ButtonRefresh");
            this.ddbExportDetail.Image = base.Language.GetBitmap("ButtonExport");

            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            this.bbiExportPDF.Glyph = base.Language.GetBitmap("DropdownPDF");
            this.bbiExportRTF.Glyph = base.Language.GetBitmap("DropdownRTF");
            this.bbiExportText.Glyph = base.Language.GetBitmap("DropdownTXT");
            this.bbiExportHTML.Glyph = base.Language.GetBitmap("DropdownHTML");
            this.bbiPrintPreview.Glyph = base.Language.GetBitmap("DropdownPrint");
            this.bbiPrintPreview.Enabled = false;
        }

        //private void ExportDocument(eExportType expType)
        //{

        //    try
        //    {
        //        GridExportController gridController = new GridExportController(this.grdQryDetail.Views[0]);
        //        switch (expType)
        //        {
        //            case eExportType.html:
        //                gridController.ExportToHTML(this.FileName_Detail + ".html", "HTML Document", "HTML Files|*.html");
        //                break;
        //            case eExportType.pdf:
        //                gridController.ExportToPDF(this.FileName_Detail + ".pdf", "PDF Document", "PDF Files|*.pdf");
        //                break;
        //            case eExportType.rtf:
        //                gridController.ExportToRTF(this.FileName_Detail + ".rtf", "RTF Document", "RTF Files|*.rtf");
        //                break;
        //            case eExportType.txt:
        //                gridController.ExportToTEXT(this.FileName_Detail + ".txt", "Text Document", "Text Files|*.txt");
        //                break;
        //            case eExportType.xls:
        //                gridController.ExportToXLS(this.FileName_Detail + ".xls", "Microsoft Excel Document", "Microsoft Excel|*.xls");
        //                break;
        //            case eExportType.xlsx:
        //                gridController.ExportToXLSX(this.FileName_Detail + ".xlsx", "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
        //                break;
        //            default:
        //                break;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        private void Query_StockAsOnDate_Detail()
        {
            //TimeSpan executionTime = new TimeSpan();
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                DataTable dtbStkAsOnDtl = null;
                using (QueryBLL queryBll = new QueryBLL())
                {
                    dtbStkAsOnDtl = queryBll.StockAsOnDateDetail_Mtl(this.MTL_CODE);
                    //executionTime = queryBll.ExecutionTime;
                }

                this.grdQryDetail.DataSource = dtbStkAsOnDtl;
                this.dntQryDetail.DataSource = dtbStkAsOnDtl;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing(); ;

                this.grdQryDetail.DataSource = null;

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
                //((frmMainMenu)this.ParentForm).ExecuteTime.Caption = "Execute Time: " + UiUtility.TimeSpanInWords(executionTime);
            }

        }

        #endregion

        private void frmQupStockAsOnDate_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.Query_StockAsOnDate_Detail();
        }

        private void frmQupStockAsOnDate_LoadCompleted()
        {
            this.KeyPreview = true;
            this.Query_StockAsOnDate_Detail();
        }

        private void frmQupStockAsOnDate_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void frmQupStockAsOnDate_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        this.btnRefreshDetail.PerformClick();
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

        private void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            this.Query_StockAsOnDate_Detail();
        }

        private void brvQryDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //bool isAdjust = false;
            //BaseView baseView = sender as BaseView;

            //ColumnView colView = (ColumnView)this.grdQryDetail.MainView;
            //FontStyle fontStyle = FontStyle.Regular;
            //if (e.RowHandle == colView.FocusedRowHandle)
            //{
            //    switch (e.Column.FieldName)
            //    {
            //        case "NG_QTY":
            //            fontStyle = FontStyle.Regular;
            //            isAdjust = true;
            //            break;
            //        case "REP_QTY":
            //            fontStyle = FontStyle.Regular;
            //            isAdjust = true;
            //            break;
            //        case "BREAK_QTY":
            //            fontStyle = FontStyle.Regular;
            //            isAdjust = true;
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

        private void brvQryDetail_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            GridView view = (GridView)sender;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void brvQryDetail_ShowingEditor(object sender, CancelEventArgs e)
        {

        }



    }
}