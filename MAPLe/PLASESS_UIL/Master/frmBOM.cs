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
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.IO;
using System.Data.OleDb;

namespace HTN.BITS.UIL.PLASESS.Master
{
    public partial class frmBOM : BaseChildFormPopup
    {
        #region "Constructor"
        public frmBOM()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.gcBOM);
            this.CustomInitializeComponent();

            dtSrcUpload = new DataTable();
            dtSrcUpload.Columns.Add(new DataColumn() { ColumnName = "PRODUCT_NO", DataType = typeof(string) });
            dtSrcUpload.Columns.Add(new DataColumn() { ColumnName = "MATERIAL_CODE", DataType = typeof(string) });
            dtSrcUpload.Columns.Add(new DataColumn() { ColumnName = "BOM_QTY", DataType = typeof(Decimal) });
            AlreadyAdded = false;
        }
        #endregion

        #region "Properties"
        private string FileName
        {
            get
            {
                return string.Format("BOM_Master_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        public DataTable dtSrcUpload { get; set; }

        public bool IsUploadFileValidated { get; set; }                      

        private bool IsTabListSelected
        {
            get
            {
                return (this.tcBOM.SelectedTabPage == this.tpBOMList);
            }
        }

        public bool AlreadyAdded { get; set; }

        public string AleradyAddedMessage { get; set; }

        #endregion

        #region "Methods"

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

        private void GetBOMList(string findValue)
        {
            List<BOM> lstBOM = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (BOMBLL BOMBLL = new BOMBLL())
                {
                    lstBOM = BOMBLL.GetBOMList(findValue);
                }

                this.gcBOM.DataSource = lstBOM;
                this.dntRowNavigator.DataSource = lstBOM;

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

        private void FocusTabUpload()
        {
            this.tpBOMUpload.PageVisible = true;
            this.tpBOMList.PageVisible = false;
            this.tcBOM.SelectedTabPage = this.tpBOMList;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.dntRowNavigator.Visible = false;
            this.ddbExport.Visible = false;

            this.IsUploadFileValidated = false;

            this.txtSrcPath.Text = String.Empty;
            this.gcBOMUpload.DataSource = null;
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = true;
        }

        private void FocusTabMaterialList()
        {
            this.tpBOMUpload.PageVisible = false;
            this.tpBOMList.PageVisible = true;
            this.tcBOM.SelectedTabPage = this.tpBOMList;

            this.btnAddNew.Visible = true;
            this.btnExit.Visible = true;

            this.dntRowNavigator.Visible = true;
            this.ddbExport.Visible = true;

            this.IsUploadFileValidated = false;

            this.txtSrcPath.Text = String.Empty;
            this.gcBOMUpload.DataSource = null;
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = false;

            this.GetBOMList(String.Empty);
        }

        private void OnUploadFail()
        {
            this.gcBOMUpload.DataSource = null;
            this.txtSrcPath.Text = String.Empty;
            this.btnSave.Enabled = false;
            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;
            AlreadyAdded = false;
            AleradyAddedMessage = "";
        }

        private void LockUploadScreen()
        {
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = false;
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch (Exception ex)
                {
                    string exMessage = ex.Message;
                }
            }
            return dtexcel;
        }

        #endregion

        #region "Events"
        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.gcBOM.Views[0] as GridView;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FocusTabUpload();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.GetBOMList(this.txtSearch.Text);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.GetBOMList(editor.Text);
                    break;
                default:
                    break;
            }
        }

        private void frmMaterialType_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = file.FileName;
                fileExt = Path.GetExtension(filePath);

                //=== 1.Check file extension
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        txtSrcPath.Text = filePath;
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);

                        //=== 2. Check template
                        if (dtExcel.Rows.Count > 0)
                        {
                            if (dtExcel.Rows[0][0].ToString() == "PRODUCT_NO" && dtExcel.Rows[0][1].ToString() == "MATERIAL_CODE" && dtExcel.Rows[0][2].ToString() == "BOM_QTY")
                            { 
                                int counter = 0;
                                List<string> productNoCollection = new List<string>();
                                List<string> materialCodeCollection = new List<string>();
                                dtSrcUpload.Rows.Clear();

                                foreach (DataRow row in dtExcel.Rows)
                                {
                                    if (counter == 0)
                                    {
                                        counter++;
                                        continue;
                                    }
                                    else
                                    {
                                        //=== 3. Check each rows contains iilegal values or not
                                        if (row[0].Equals(System.DBNull.Value) || row[1].Equals(System.DBNull.Value))
                                        {
                                            XtraMessageBox.Show(this, "Value at row# " + (counter + 1) + " is invalid, Please check", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                            this.OnUploadFail();
                                            return;
                                        }
                                        else
                                        {
                                            string value = row[2].ToString();
                                            decimal number;
                                            if (Decimal.TryParse(value, out number))
                                            {
                                                productNoCollection.Add(row[0] == null ? "" : row[0].ToString());
                                                string matCodes = row[1].ToString();
                                                string[] arrMatCodes = matCodes.Split(',');

                                                for (int i = 0; i < arrMatCodes.Length; i++)
                                                {
                                                    materialCodeCollection.Add(arrMatCodes[i].Trim());
                                                    dtSrcUpload.Rows.Add(row.ItemArray[0], arrMatCodes[i].Trim(), row.ItemArray[2]);
                                                }
                                            }
                                            else
                                            {
                                                XtraMessageBox.Show(this, "BOM QTY at row# " + (counter + 1) + " is invalid, Please check", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                                this.OnUploadFail();
                                                return;
                                            }
                                        }

                                        counter++;
                                    }
                                }

                                using (BOMBLL bomBll = new BOMBLL())
                                {
                                    string products = String.Join(",", productNoCollection.ToArray());
                                    string materials = String.Join(",", materialCodeCollection.ToArray());
                                    string[] exitsItems = bomBll.CheckIsBOMExists(products, materials);

                                    if (!String.IsNullOrEmpty(exitsItems[0]))
                                    {
                                        XtraMessageBox.Show(this, "Product No.  '" + exitsItems[0] + "'  is not exists in product master", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        this.OnUploadFail();
                                    }
                                    else if (!String.IsNullOrEmpty(exitsItems[1]))
                                    {
                                        XtraMessageBox.Show(this, "Material No.  '" + exitsItems[1] + "'  is not exists in material master", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        this.OnUploadFail();
                                    }
                                    else
                                    {

                                        if (!String.IsNullOrEmpty(exitsItems[2]))
                                        {
                                            AlreadyAdded = true;
                                            AleradyAddedMessage = "Product No.  '" + exitsItems[2] + "'  is already added to BOM Master, Do you want replace it anyway?";
                                        }
                                        else
                                        {
                                            AlreadyAdded = false;
                                            AleradyAddedMessage = "";
                                        }

                                        this.btnSave.Enabled = true;
                                        gcBOMUpload.DataSource = dtSrcUpload;
                                    }
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(this, "Excel template is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                this.OnUploadFail();
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(this, "Excel template is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            this.OnUploadFail();
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(this, "Excel template is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        this.OnUploadFail();
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    this.OnUploadFail();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FocusTabMaterialList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string result = String.Empty;
            //using (BOMBLL bomBll = new BOMBLL())
            //{
            //    result = bomBll.InsertBOM(dtSrcUpload, ((frmMainMenu)this.ParentForm).UserID);
            //}

            //if (result == "OK")
            //{
            //    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
            //}

            //this.LockUploadScreen();

            if (AlreadyAdded)
            {
                DialogResult dCliResTest = XtraMessageBox.Show(this, AleradyAddedMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dCliResTest == DialogResult.No)
                {
                    return;
                }
            }

            string result = String.Empty;
            using (BOMBLL bomBll = new BOMBLL())
            {
                result = bomBll.InsertBOM(dtSrcUpload, ((frmMainMenu)this.ParentForm).UserID);
            }

            if (result == "OK")
            {
                NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                AlreadyAdded = false;
                AleradyAddedMessage = "";
            }

            this.LockUploadScreen();
        }

        #endregion

        private void frmBOM_Load(object sender, EventArgs e)
        {
            this.GetBOMList(String.Empty);
        }
    }
}