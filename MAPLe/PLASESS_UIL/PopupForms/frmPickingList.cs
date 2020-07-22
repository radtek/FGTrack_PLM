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
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.UIL.PLASESS.Reports;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting.Drawing;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPickingList : BaseDialogForm
    {
        public frmPickingList()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdPickingList);
            this.picSelect = new GridCheckMarksSelection(this.grvPickingList);

            this.CustomInitializeComponent();
        }

        #region "Dialog Idle Time"

        ~frmPickingList()
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }
            base.DialogIdle.IdleAsync -= new EventHandler(this.DialogIdle_IdleAsync);
        }

        private void DialogIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.DialogIdle_Idle(sender, e); })
                );
        }

        private void DialogIdle_Idle(object sender, EventArgs e)
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion 

        #region "Variable Member"

        private GridCheckMarksSelection picSelect;
        private eFormState formState = eFormState.ReadOnly;

        private string _SO_NO;
        private string _WH_ID;
        private string _USER_ID;

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

        public string PARTY_ID { get; set; }
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
        public string WH_ID
        {
            get
            {
                return _WH_ID;
            }
            set
            {
                if (_WH_ID == value)
                    return;
                _WH_ID = value;
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

        #endregion

        #region "Method Member"

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = this.grdPickingList.Views[0] as GridView;

                switch (fState)
                {
                    case eFormState.Edit:

                        this.dntPickingList.Enabled = true;
                        this.dntPickingList.TextStringFormat = "      Edit Mode      ";
                        this.dntPickingList.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnDelete.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnClose.Enabled = true;
                        this.btnClose.Text = "Cancel";

                        this.ddbOptions.Enabled = false;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridEditOnly(view, false, "QTY");

                        break;
                    case eFormState.ReadOnly:

                        this.dntPickingList.Enabled = false;
                        this.dntPickingList.TextStringFormat = " Record {0} of {1} ";
                        this.dntPickingList.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnDelete.Enabled = true;
                        this.btnSave.Enabled = false;
                        this.btnClose.Enabled = true;
                        this.btnClose.Text = "Close";
                        this.ddbOptions.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        UiUtility.SetGridEditOnly(view, true, "QTY");

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

        private void CustomInitializeComponent()
        {
            this.ddbOptions.Image = base.Language.GetBitmap("ButtonExport");

            this.bbiPrintToReport.Glyph = base.Language.GetBitmap("DropdownPrint");
            //--------------------------------------------------------------------
            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            this.bbiExportPDF.Glyph = base.Language.GetBitmap("DropdownPDF");
            this.bbiExportRTF.Glyph = base.Language.GetBitmap("DropdownRTF");
            this.bbiExportText.Glyph = base.Language.GetBitmap("DropdownTXT");
            this.bbiExportHTML.Glyph = base.Language.GetBitmap("DropdownHTML");
        }

        private void GetPickingList()
        {
            List<PickingDtl> lstPicking = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstPicking = shipOrdBll.GetPickingList(this.SO_NO, this.WH_ID);
                }

                this.grdPickingList.DataSource = lstPicking;
                this.dntPickingList.DataSource = lstPicking;

                if (lstPicking != null)
                {
                    if (lstPicking.Count > 0)
                    {
                        this.ConditionsColumnView(this.grdPickingList);
                        //lstPicking[0].


                    }

                    //default check all
                    this.picSelect.SelectAll();
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

        private void ConditionsColumnView(GridControl grd)
        {
            int index = 0;
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[2];

                cnArr[index] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[index].Column = ((ColumnView)grd.MainView).Columns["PICKED_QTY"];
                cnArr[index].Expression = @"[PICKED_QTY] > 0 AND [QTY] > [PICKED_QTY]";
                cnArr[index].Appearance.ForeColor = Color.Red;
                cnArr[index].ApplyToRow = false;

                //next column Condition;
                index++;


                cnArr[index] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[index].Column = ((ColumnView)grd.MainView).Columns["LOADED_QTY"];
                cnArr[index].Expression = @"[PICKED_QTY] > 0 AND [PICKED_QTY] <> [LOADED_QTY]";
                cnArr[index].Appearance.ForeColor = Color.Red;
                cnArr[index].ApplyToRow = false;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void PrintPickingList(string soNo, List<PickingDtl> lstPicking)
        {
                ShippingOrder soHDR = null;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    ds = shipOrdBll.PrintPickingListReport(soNo, lstPicking, string.Empty);
                    soHDR = shipOrdBll.GetShippingOrder(soNo);

                }
                


                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                RPT_PICKING_LIST rpt = new RPT_PICKING_LIST();

                //RPT_PICKING_LIST_REPORT rpt = new RPT_PICKING_LIST_REPORT();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
                rpt.CreateDocument();
                if (soHDR.PARTY_ID == "PST008")
                {
                    RPT_PICKING_LIST rpt2 = new RPT_PICKING_LIST();
                rpt2.DataSource = ds;
                rpt2.CreateDocument();

                
                for (int i = 0; i < rpt2.Pages.Count; i++)
                {
                    rpt2.Pages[i].AssignWatermark(CreateTextWatermark("COPY"));
                }
                // rpt.Pages.AddRange(rpt2.Pages);
                int minPageCount = Math.Min(rpt.Pages.Count, rpt2.Pages.Count);
                for (int i = 0; i < minPageCount; i++)
                {
                    rpt.Pages.Insert(i * 2 + 1, rpt2.Pages[i]);
                }
                if (rpt2.Pages.Count != minPageCount)
                {
                    for (int i = minPageCount; i < rpt2.Pages.Count; i++)
                    {
                        rpt.Pages.Add(rpt2.Pages[i]);
                    }
                }
                }

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

        private Watermark CreateTextWatermark(string text)
        {
            Watermark textWatermark = new Watermark();

            textWatermark.Text = text;
            textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
            textWatermark.Font = new Font("Tahoma", 108);
            textWatermark.ForeColor = Color.Red;
            textWatermark.TextTransparency = 150;
            textWatermark.ShowBehind = false;

            return textWatermark;
        }

        private int GetPickedQty(string pickno, int lineno)
        {
            int result = 0;
            try
            {
                using (ShippingOrderBLL soBll = new ShippingOrderBLL())
                {
                    result = soBll.GetPickedQty(pickno, lineno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private string DeletePicking(GridView view, int rowSelect, string pickno, int lineno)
        {
            if (view == null || view.SelectedRowsCount == 0) return string.Empty;
            string resultMsg = string.Empty;

            view.BeginSort();
            try
            {
                //string pickNo = view.GetRowCellValue(rowSelect, "PICK_NO").ToString();
                //int lineNo = Convert.ToInt32(view.GetRowCellValue(rowSelect, "LINE_NO"), NumberFormatInfo.CurrentInfo);

                using (ShippingOrderBLL soBll = new ShippingOrderBLL())
                {
                    resultMsg = soBll.DeletePickingHDR(pickno, lineno, this.USER_ID);
                }

                if (resultMsg.Equals("OK"))
                {
                    view.DeleteRow(rowSelect);
                }
            }
            catch (Exception ex)
            {
                resultMsg = ex.Message;
            }
            finally
            {
                view.EndSort();
            }

            return resultMsg;
        }


        #endregion

        #region "Button Export Referance"

        private string FileName
        {
            get
            {
                return string.Format("PickingList_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToXLS(this.FileName + ".xls", "Microsoft Excel Document", "Microsoft Excel|*.xls");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.XLS, this.FileName + ".xls", null);
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToXLSX(this.FileName + ".xlsx", "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToPDF(this.FileName + ".pdf", "PDF Document", "PDF Files|*.pdf");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToRTF(this.FileName + ".rtf", "RTF Document", "RTF Files|*.rtf");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToTEXT(this.FileName + ".txt", "Text Document", "Text Files|*.txt");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GridExportController gridController = new GridExportController(this.grdPickingList.Views[0]);
            //gridController.ExportToHTML(this.FileName + ".html", "HTML Document", "HTML Files|*.html");
            base.ViewExportToExcel(this.grdPickingList.Views[0], GridExportType.HTML, this.FileName + ".html", null);
        }

        private void bbiPrintToReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<PickingDtl> lstPicking = null;
            try
            {
                if (this.picSelect.SelectedCount > 0)
                {
                    lstPicking = new List<PickingDtl>(this.picSelect.SelectedCount);
                    for (int i = 0; i < this.picSelect.SelectedCount; i++)
                    {
                        lstPicking.Add((PickingDtl)this.picSelect.GetSelectedRow(i));
                    }

                    this.PrintPickingList(this.SO_NO, lstPicking);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Product Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ddbOptions_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }

        #endregion "Button Export Referance"

        private void frmPickingList_Load(object sender, EventArgs e)
        {
            //this.GetPickingList();
            //this.FormState = eFormState.ReadOnly;

            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmPickingList_LoadCompleted()
        {
            this.GetPickingList();
            this.FormState = eFormState.ReadOnly;

            GridView view = (GridView)this.grdPickingList.MainView;
            if (PARTY_ID == "PST008")
                view.Columns["LOADED_QTY"].Caption = "MTST Received";
            else
                view.Columns["LOADED_QTY"].Caption = "Loaded**";

            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Edit:
                        this.GetPickingList();
                        this.FormState = eFormState.ReadOnly;
                        break;
                    case eFormState.ReadOnly:
                        this.Close();
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

        private void grvPickingList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (e.KeyCode == Keys.Delete)
                {
                    int rowHandle = view.FocusedRowHandle;
                    //int pickQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "PICKED_QTY"), NumberFormatInfo.CurrentInfo);

                    string pickno = view.GetRowCellValue(rowHandle, "PICK_NO").ToString();
                    int lineno = Convert.ToInt32(view.GetRowCellValue(rowHandle, "LINE_NO"), NumberFormatInfo.CurrentInfo);

                    int pickedQty = this.GetPickedQty(pickno, lineno);
                    view.SetRowCellValue(rowHandle, "PICKED_QTY", pickedQty);

                    if (pickedQty == 0)
                    {
                        DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this picking?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (isDelete == DialogResult.Yes)
                        {
                            string msg = this.DeletePicking(view, rowHandle, pickno, lineno);
                            if (!msg.Equals("OK"))
                            {
                                XtraMessageBox.Show(this, msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                    else
                    {
                        
                        XtraMessageBox.Show(this, "This picking already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "KeyDown : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = this.grdPickingList.Views[0] as GridView;

                int rowHandle = view.FocusedRowHandle;
                //int pickQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "PICKED_QTY"), NumberFormatInfo.CurrentInfo);

                string pickno = view.GetRowCellValue(rowHandle, "PICK_NO").ToString();
                int lineno = Convert.ToInt32(view.GetRowCellValue(rowHandle, "LINE_NO"), NumberFormatInfo.CurrentInfo);

                int pickedQty = this.GetPickedQty(pickno, lineno);
                view.SetRowCellValue(rowHandle, "PICKED_QTY", pickedQty);

                if (pickedQty == 0)
                {
                    DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this picking?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (isDelete == DialogResult.Yes)
                    {
                        string msg = this.DeletePicking(view, rowHandle, pickno, lineno);
                        if (!msg.Equals("OK"))
                        {
                            XtraMessageBox.Show(this, msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "This picking already start!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmPickingList_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdPickingList.Views[0]);
            this.Controls.Clear();
        }

        private void grvPickingList_rps_PICK_QTY_Validating(object sender, CancelEventArgs e)
        {

            GridView view = this.grdPickingList.Views[0] as GridView;
            TextEdit editor = (TextEdit)sender;

            if (editor.Text == string.Empty || Convert.ToInt32(editor.Text) <= 0)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int rowHandle = view.FocusedRowHandle;

                int qty = Convert.ToInt32(editor.EditValue, NumberFormatInfo.CurrentInfo);
                int iniQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "INI_QTY"), NumberFormatInfo.CurrentInfo);
                //int pickedQty = Convert.ToInt32(view.GetRowCellValue(rowHandle, "PICKED_QTY"), NumberFormatInfo.CurrentInfo);

                string pickno = view.GetRowCellValue(rowHandle, "PICK_NO").ToString();
                int lineno = Convert.ToInt32(view.GetRowCellValue(rowHandle, "LINE_NO"), NumberFormatInfo.CurrentInfo);

                int pickedQty = this.GetPickedQty(pickno, lineno);

                if (qty != iniQty) //value have to change
                {
                    if (qty > iniQty || qty < pickedQty)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        view.SetRowCellValue(rowHandle, "FLAG", "1");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvPickingList_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //GridView view = sender as GridView;
            //view.SetRowCellValue(view.FocusedRowHandle, "FLAG", "1");//UPDATE FLAG TO "1"

        }

        private void grvPickingList_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "QTY")
            {

                int iniQty = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "INI_QTY"), NumberFormatInfo.CurrentInfo);
                int pickedQty = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "PICKED_QTY"), NumberFormatInfo.CurrentInfo);

                if (iniQty == pickedQty)
                {
                    view.FocusedColumn.OptionsColumn.ReadOnly = false;
                    //already finish
                    view.HideEditor();
                    e.Cancel = true;
                }
            }
            else
            {
                view.FocusedColumn.OptionsColumn.ReadOnly = true;
            }
        }

        private void grvPickingList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            bool isAdjust = false;
            BaseView baseView = sender as BaseView;

            ColumnView colView = (ColumnView)this.grdPickingList.MainView;
            int iCompare, iValue;
            try
            {
                if (e.RowHandle == colView.FocusedRowHandle)
                {
                    switch (e.Column.FieldName)
                    {
                        case "PICKED_QTY":
                            iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                            iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

                            if (iCompare > 0 && iCompare != iValue)
                            {
                                isAdjust = true;
                            }
                            break;
                        case "LOADED_QTY":
                            iCompare = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "LOADED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);
                            iValue = Convert.ToInt32((colView.GetRowCellValue(e.RowHandle, "PICKED_QTY") as decimal?) ?? 0, NumberFormatInfo.CurrentInfo);

                            if (iValue > 0 && iValue != iCompare)
                            {
                                isAdjust = true;
                            }
                            break;
                        default:
                            break;
                    }

                    if (isAdjust)
                    {
                        //Apply the appearance of the SelectedRow
                        e.Appearance.Assign(((GridView)baseView).PaintAppearance.SelectedRow);
                        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                        //e.Appearance.Options.UseForeColor = true;
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Edit;
            this.grdPickingList.Focus();
            GridView view = this.grdPickingList.Views[0] as GridView;
            view.FocusedColumn = view.Columns["QTY"];
            view.ShowEditor();
            
        }

        private void grvPickingList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            if (this.FormState == eFormState.ReadOnly)
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (!view.FocusedColumn.FieldName.Equals("QTY"))
                {
                    e.Info.ImageIndex = -1;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            GridView view = this.grdPickingList.Views[0] as GridView;
            List<PickingDtl> lstPicking = null;
            try
            {
                lstPicking = new List<PickingDtl>();
                int flag = 0;
                for (int i = 0; i < view.RowCount; i++)
                {
                    flag = Convert.ToInt32(view.GetRowCellValue(i, "FLAG"), NumberFormatInfo.CurrentInfo);
                    if (flag.Equals(1))
                    {
                        lstPicking.Add((PickingDtl)view.GetRow(i));
                    }
                }

                if (lstPicking.Count > 0)
                {
                    using (ShippingOrderBLL soBll = new ShippingOrderBLL())
                    {
                        resultMsg = soBll.UpdatePickingHDR(lstPicking, this.USER_ID);
                    }

                    if (resultMsg.Equals("OK"))
                    {
                        NotifierResult.Show("Update Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                    }
                    else
                    {
                        NotifierResult.Show(resultMsg, "Error", 100, 1000, 0, NotifyType.Warning);
                    }
                }
                else
                {
                    NotifierResult.Show("No Data Update", "Result", 50, 1000, 50, NotifyType.Warning);
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

    }
}