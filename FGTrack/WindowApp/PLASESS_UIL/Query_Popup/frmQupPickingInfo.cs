using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.UIL.PLASESS.ConfirmForms;

namespace HTN.BITS.UIL.PLASESS.Query_Popup
{
    public partial class frmQupPickingInfo : BaseDialogForm
    {
        public frmQupPickingInfo()
        {
            InitializeComponent();
            this.CustomInitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdQryDetail);

            
        }

        #region Variable Member

        private GridCheckMarksSelection picSelect;

        private string _SO_NO;
        private string _PROD_SEQ_NO;
        private string _TYPE;

        private string _USER_ID;

        #endregion

        #region Property Member

        public string SO_NO
        {
            get
            {
                return _SO_NO;
            }
            set
            {
                if (_SO_NO == value)
                    return;
                _SO_NO = value;
            }
        }
        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
            }
        }
        public string TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                if (_TYPE == value)
                    return;
                _TYPE = value;
            }
        }

        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }
        
        public string FileName_Detail
        {
            get
            {
                if(this._TYPE.Equals("L"))
                    return string.Format("LoadingDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
                else
                    return string.Format("PickingDetail_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        #endregion

        #region "Custom Event Handle"

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
            this.btnRollBack.Image = base.Language.GetBitmap("btnRollback");
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

        private void Query_Detail_Info()
        {
            //TimeSpan executionTime = new TimeSpan();
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                DataTable dtbStkAsOnDtl = null;
                using (ShippingOrderBLL shipingOrdBll = new ShippingOrderBLL())
                {
                    dtbStkAsOnDtl = shipingOrdBll.ShowPickingDetail(this._SO_NO, this._PROD_SEQ_NO, this._TYPE);
                    //executionTime = queryBll.ExecutionTime;
                }

                this.picSelect.ClearSelection();
                this.grdQryDetail.DataSource = dtbStkAsOnDtl;
                this.dntPickingDtl.DataSource = dtbStkAsOnDtl;

                this.btnRollBack.Enabled = (dtbStkAsOnDtl.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                this.grdQryDetail.DataSource = null;

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
                //((frmMainMenu)this.ParentForm).ExecuteTime.Caption = "Execute Time: " + UiUtility.TimeSpanInWords(executionTime);
            }

        }

        private void RollBackDocument(List<RollBackDocument> lstRollBackDoc)
        {
            try
            {
                string result = string.Empty;
                using (ShippingOrderBLL shipingOrdBll = new ShippingOrderBLL())
                {
                    if (this._TYPE.Equals("L"))
                        result = shipingOrdBll.RollBackLoading(lstRollBackDoc, this._USER_ID);
                    else
                        result = shipingOrdBll.RollBackPicking(lstRollBackDoc, this._USER_ID);
                }

                if (result.Equals("OK"))
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void frmQupPickingInfo_KeyUp(object sender, KeyEventArgs e)
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
            this.Query_Detail_Info();
        }

        private void frmQupPickingInfo_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            ////this.Visible = true;
            ////Application.DoEvents();


            //if (this._TYPE.Equals("L"))
            //{
            //    this.Text = "LOADING INFO";
            //    this.picSelect = new GridCheckMarksSelection(this.grvPickingInfo);
            //    this.btnRollBack.Text = "Roll Back Loading";
            //}
            //else
            //{
            //    this.Text = "PICKING INFO";
            //    this.picSelect = new GridCheckMarksSelection(this.grvPickingInfo, "LOADING_NO", "IS NULL");
            //    this.btnRollBack.Text = "Roll Back Picking";
            //}

            //this.Query_Detail_Info();
        }

        private void frmQupPickingInfo_LoadCompleted()
        {
            this.KeyPreview = true;

            if (this._TYPE.Equals("L"))
            {
                this.Text = "LOADING INFO";
                this.picSelect = new GridCheckMarksSelection(this.grvPickingInfo);
                this.btnRollBack.Text = "Roll Back Loading";
            }
            else
            {
                this.Text = "PICKING INFO";
                this.picSelect = new GridCheckMarksSelection(this.grvPickingInfo, "LOADING_NO", "IS", "NULL");
                this.btnRollBack.Text = "Roll Back Picking";
            }

            this.Query_Detail_Info();
        }

        private void frmQupPickingInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdQryDetail.Views[0]);
            this.Controls.Clear();
        }

        private void btnRollBack_Click(object sender, EventArgs e)
        {
            List<RollBackDocument> lstDocument = null;
            try
            {
                if (this.picSelect.SelectedCount > 0)
                {
                    DialogResult isAuthen = DialogResult.None; 

                    using (frmCOFAuthen fAuthen = new frmCOFAuthen())
                    {
                        isAuthen = UiUtility.ShowPopupForm(fAuthen, this, true);
                    }

                    if (isAuthen == DialogResult.OK)
                    {
                        DialogResult result = XtraMessageBox.Show(this, "Do you wnat to Roll Back Document?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            lstDocument = new List<RollBackDocument>(this.picSelect.SelectedCount);
                            for (int i = 0; i < this.picSelect.SelectedCount; i++)
                            {
                                DataRowView row = this.picSelect.GetSelectedRow(i) as DataRowView;
                                lstDocument.Add(new RollBackDocument()
                                {
                                    DOC_NO = (this._TYPE.Equals("L") ? row["LOADING_NO"].ToString() : row["PICK_NO"].ToString()),
                                    SERIAL_NO = row["SERIAL_NO"].ToString()
                                });
                            }

                            this.RollBackDocument(lstDocument);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "You not priviledge for rollback items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Record to Rollback!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


    }
}