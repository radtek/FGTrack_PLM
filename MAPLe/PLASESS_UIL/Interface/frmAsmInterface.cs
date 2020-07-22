using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
using System.Data.OleDb;
using System.IO;
using System.Collections.ObjectModel;
using HTN.BITS.UIL.PLASESS.PopupForms;
using HTN.BITS.UIL.PLASESS.Component.SysFileInfo;
using System.Diagnostics;
using DevExpress.XtraPrinting;

namespace HTN.BITS.UIL.PLASESS.Interface
{
    public partial class frmAsmInterface : BaseChildForm
    {
        public frmAsmInterface()
        {
            InitializeComponent();
            this.CustomInitializeComponent();

            //base.LoadGridLayoutMultipleView(this.Name, this.grdQrySummary);
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdAsmIn);

            //new GridCheckMarksSelection(this.grvPickingInfo, "LOADING_NO", "IS NULL");
            this.chkSelect = new GridCheckMarksSelection(this.grvAsmIn, "DATA_TYPE", "<>", "'POSTED'");

            //this.gridController = new GridExportController(this.grdAsmIn.Views[0]);
        }

        #region "Variable Member"

        private DataTable dtbAssemblyInfData;
        //GridExportController gridController;

        private List<String> FilesDownload = null;
        private string DestinationPath = string.Empty;

        private GridCheckMarksSelection chkSelect;

        #endregion

        #region "Property Member"

        public string FileName
        {
            get
            {
                return string.Format("FGAssemblyInterfaceData_{0:yyyyMMddHHmmss}", DateTime.Now);
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

                    List<Warehouse> lstWH = inDataBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        lstWH.Insert(0, new Warehouse { SEQ_NO = string.Empty, NAME = "(All)" });
                        this.lueWarehouse.Properties.DataSource = lstWH;
                    }
                }

                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                   this.grvAsmIn_rps_PRODUCTION_TYPE.DataSource = pdtBll.GetProductionTypeList();
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

        private void FgAssemblyInterfaceData(string posttype, string wh_id, string productNo, string postRef,
            DateTime? fromDate, DateTime? toDate, DateTime? stkfDate, DateTime? stktDate)
        {
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                {
                    this.dtbAssemblyInfData = inDataBll.IfInAssemblyList(posttype, wh_id, productNo, postRef, fromDate, toDate, stkfDate, stktDate, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (this.dtbAssemblyInfData != null)
                {
                    this.ConditionsColumnView(this.grdAsmIn);
                }

                this.grdAsmIn.DataSource = this.dtbAssemblyInfData;
                this.dntQryStkAsOn.DataSource = this.dtbAssemblyInfData;

                this.grdPostedData.DataSource = null;

                //check enable button
                this.CheckEnablePostData("[DATA_TYPE] = 'NEW'");
                base.ExecutionStop();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                this.grdAsmIn.DataSource = null;
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
            if (this.dtbAssemblyInfData != null)
            {
                DataRow[] rows = this.dtbAssemblyInfData.Select(expression);
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

        private void FGAssemblyInterfacePostData(DataTable dtbSelect)
        {
            try
            {
                //ICollection<string> files;
                DataTable dtbPosted = null;
                string selectPath = string.Empty;
                string fullfilename = string.Empty;

                DialogResult result = this.fdbSelectFilePath.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    selectPath = this.fdbSelectFilePath.SelectedPath;
                    try
                    {
                        using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                        {
                            inDataBll.GetPostData("INTERFACE_PACK.IF_IN_ASEMBLY_LIST", string.Empty, dtbSelect, selectPath, ((frmMainMenu)this.ParentForm).UserID, out dtbPosted);
                        }

                        if (dtbPosted != null)
                        {
                            //    //get last update revision
                            this.GetRevisionLastUpdate();

                            this.BindingPostedData(dtbPosted, selectPath, out fullfilename);

                            UiUtility.CopyFilesToServer(new Collection<string>() { fullfilename });

                            this.OpenExcelFile(fullfilename);

                            this.btnRefresh.PerformClick();

                        }
                        //if (files.Count > 0)
                        //{
                        //    //get last update revision
                        //    this.GetRevisionLastUpdate();

                        //    //copy to server
                        //    UiUtility.CopyFilesToServer(files);

                        //    this.OpenPath(files);

                        //    //Refresh screen
                        //    this.btnRefresh.PerformClick();
                        //}

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

        private void OpenExcelFile(string filename)
        {
            DialogResult message = XtraMessageBox.Show("Do you want to open?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (message == DialogResult.Yes)
                {
                    if (File.Exists(filename))
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.FileName = filename;
                            myProcess.Start();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Could not find a part of the file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UploadRevision(string userid)
        {
            DataTable dtbBomData;
            string fullFilename, pathFile, filename;
            string connectionString,queryString;

            using (OpenFileDialog fdlg = new OpenFileDialog { Title = "Open BOM. Revision File", InitialDirectory = @"My Documents:\", Filter = "New Excel files (*.csv)|*.csv|BOM. File (*.csv)|*.csv", FilterIndex = 2, RestoreDirectory = true })
            {
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(fdlg.FileName))
                    {
                        fullFilename = fdlg.FileName;
                        FileInfo fi = new FileInfo(fullFilename);

                        pathFile = fi.DirectoryName;
                        filename = fi.Name;
                    }
                    else
                    {
                        fullFilename = string.Empty;
                        pathFile = string.Empty;
                        filename = string.Empty;
                    }
                }
                else
                {
                    fullFilename = string.Empty;
                    pathFile = string.Empty;
                    filename = string.Empty;
                }
            }


            if (!string.IsNullOrEmpty(fullFilename) && !string.IsNullOrEmpty(filename))
            {
                try
                {
                    StreamReader StrWer = File.OpenText(fullFilename);
                    ICollection<string> values = new Collection<string>();
                    while (StrWer.EndOfStream == false)
                    {
                        values.Add(StrWer.ReadLine());
                    }

                    string seq = string.Empty;
                    using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                    {
                        seq = inDataBll.UploadRevision(filename, userid, values);
                    }

                    if (!string.IsNullOrEmpty(seq))
                    {
                        using (frmPopResultUpRevision fResultRevision = new frmPopResultUpRevision { SEQ_NO = seq, USER_ID = userid })
                        {
                            UiUtility.ShowPopupForm(fResultRevision, this, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void GetRevisionLastUpdate()
        {
            try
            {
                string result = string.Empty;

                using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                {
                    result = inDataBll.GetRevisionLastUpdate();
                }

                this.txtRevisionLastUpdate.Text = result;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void BindingPostedData(DataTable dtb, string filepath, out string fullfilename)
        {
            fullfilename = string.Empty;
            try
            {
                //fullfilename = string.Format(@"{0}\\IF001-ASM-FG-{1:yyyyMMddHHmm}.XLS", filepath, DateTime.Now);

                var filename = (from fileid in dtb.AsEnumerable()
                                select fileid.Field<string>("FILE_ID")).First<string>();

                fullfilename = string.Format(@"{0}\\{1}", filepath, filename);

                this.grdPostedData.DataSource = dtb;

                var optXls = new XlsExportOptions() { SheetName = filename.Replace(".XLS", "")};
                this.grvPostedData.ExportToXls(fullfilename, optXls);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void CopyPoFile(string filename, List<String> lstFiles, string destinationPath)
        {
            try
            {
                if (this.FilesDownload != null)
                {
                    this.FilesDownload.Clear();
                    this.FilesDownload = null;
                }


                this.FilesDownload = new List<string>(1);
                this.FilesDownload.Add(filename); 
                this.DestinationPath = destinationPath;

                CopyFiles Temp = new CopyFiles(lstFiles, destinationPath);
                Temp.EV_copyComplete += new CopyFiles.DEL_copyComplete(Temp_EV_copyComplete);
                DIA_CopyFiles TempDiag = new DIA_CopyFiles();
                TempDiag.SynchronizationObject = this;
                Temp.CopyAsync(TempDiag);
            }
            catch (Exception ex)
            {
                
            }
        }

        void Temp_EV_copyComplete()
        {
            //throw new NotImplementedException();
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    //Check Open Path
                    DialogResult message = XtraMessageBox.Show("Do you want to open directory?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    try
                    {
                        if (message == DialogResult.Yes)
                        {
                            ShowSelectedInExplorer.FilesOrFolders(this.DestinationPath, this.FilesDownload);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }));
                return;
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

            ColumnView colView = (ColumnView)this.grdAsmIn.MainView;
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
            this.GridControl_DoubleClick(this.grdAsmIn.MainView, e);
        }

        #endregion

        #region "Button Export Referance"

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.XLS, this.FileName + ".xls", null);

        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.XLSX, this.FileName + ".xlsx", null);
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.PDF, this.FileName + ".pdf", null);
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.RTF, this.FileName + ".rtf", null);
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.TEXT, this.FileName + ".txt", null);
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.ViewExportToExcel(this.grdAsmIn.Views[0], GridExportType.HTML, this.FileName + ".html", null);
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
            this.ChangeView(eViewType.GridView, this.grdAsmIn);
        }

        private void bbiCardView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.CardView, this.grdAsmIn);
        }

        private void bbiBandView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.BandedView, this.grdAsmIn);
        }

        private void bbiAdvView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ChangeView(eViewType.AdvanceView, this.grdAsmIn);
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
                    gridCtl.MainView = this.grvAsmIn;
                    this.grvAsmIn.ExpandAllGroups();

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

        private void frmAsmInterface_Load(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            //this.InitializaLOVData();
            //this.GetRevisionLastUpdate();
            //this.dteFromDate.EditValue = null;//DateTime.Now;
            //this.dteToDate.EditValue = null;//DateTime.Now;
            //this.btnRefresh.Focus();
            //this.btnRefresh.PerformClick();
        }

        private void frmAsmInterface_LoadCompleted()
        {
            this.KeyPreview = true;
            this.InitializaLOVData();
            this.GetRevisionLastUpdate();
            this.dteFromDate.EditValue = null;//DateTime.Now;
            this.dteToDate.EditValue = null;//DateTime.Now;
            this.btnRefresh.Focus();
            this.btnRefresh.PerformClick();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string posttype, wh_id, productNo, postRef;
            DateTime? fromDate, toDate, stkFDate, stkTDate;


            if (this.luePostType.EditValue != null)
                posttype = (string)this.luePostType.EditValue;
            else
                posttype = string.Empty;

            if (this.lueWarehouse.EditValue != null)
                wh_id = (string)this.lueWarehouse.EditValue;
            else
                wh_id = string.Empty;

            productNo = this.txtProduct.Text;
            postRef = this.txtPostRef.Text;

            //Post Date
            if (this.dteFromDate.EditValue != null)
                fromDate = this.dteFromDate.DateTime;
            else
                fromDate = null;

            if (this.dteToDate.EditValue != null)
                toDate = this.dteToDate.DateTime;
            else
                toDate = null;

            //Stock In Date
            if (this.dteStkFDate.EditValue != null)
                stkFDate = this.dteStkFDate.DateTime;
            else
                stkFDate = null;

            if (this.dteStkTDate.EditValue != null)
                stkTDate = this.dteStkTDate.DateTime;
            else
                stkTDate = null;

            this.FgAssemblyInterfaceData(posttype, wh_id, productNo, postRef, fromDate, toDate, stkFDate, stkTDate);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAsmInterface_KeyUp(object sender, KeyEventArgs e)
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

        private void frmAsmInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void btnPostData_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.chkSelect.SelectedCount > 0)
                {
                    //base.BeginProcessing("Begin Load data...", "Please Waiting");

                    DataRow[] rows = new DataRow[this.chkSelect.SelectedCount];

                    for (int i = 0; i < this.chkSelect.SelectedCount; i++)
                    {
                        DataRowView rowView = this.chkSelect.GetSelectedRow(i) as DataRowView;

                        rows[i] = rowView.Row;
                    }

                    if (rows.Length > 0)
                    {
                        this.FGAssemblyInterfacePostData(rows.CopyToDataTable());
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Data to Post!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    this.grdAsmIn.Focus();
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error btnPostData_Click", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            
        }

        private void btnUploadRevision_Click(object sender, EventArgs e)
        {
            //this.UploadRevision(((frmMainMenu)this.ParentForm).UserID);
            try
            {
                GridView view = this.grdAsmIn.Views[0] as GridView;

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle ||
                    view.FocusedRowHandle == GridControl.InvalidRowHandle ||
                    view.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show(this, "No Record Select!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                int rowHandle = view.FocusedRowHandle;

                string filename = view.GetRowCellValue(rowHandle, "POST_REF_NO").ToString().Trim();
                if (!string.IsNullOrEmpty(filename))
                {
                    //Check file exist on History folder
                    string fileFullName = string.Empty;
                    try
                    {
                        using (XLSFileHistoryFileInfo xlsfileInfo = new XLSFileHistoryFileInfo())
                        {
                            fileFullName = xlsfileInfo.ExistXLSFileInHistoryPath(filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                    if (!string.IsNullOrEmpty(fileFullName))
                    {
                        //Dowload from history folder
                        this.fdbSaveFilePath.Description = string.Format("Select Path To Save '{0}'", filename);
                        DialogResult result = this.fdbSaveFilePath.ShowDialog(this);

                        if (result == DialogResult.OK)
                        {
                            List<String> lstDownloadFiles = new List<String>(1);
                            lstDownloadFiles.Add(fileFullName);

                            this.CopyPoFile(filename, lstDownloadFiles, this.fdbSaveFilePath.SelectedPath);
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show(this, string.Format("'{0}' not in History!!", filename), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


    }
}