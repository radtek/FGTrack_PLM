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
using System.Text.RegularExpressions;

namespace HTN.BITS.UIL.PLASESS.Component.CSV
{
    public enum eMaster_Form
    {
        FRM_PARTY_MASTER,
        FRM_PRODUCT_MASTER,
        FRM_PRODUCT_BOM_MASTER,
        FRM_MATERIAL_MASTER,
        FRM_ARRIVAL_RECEIVE,
        FRM_SHIPPING_ORDER
    }

    public partial class frmUploadCSV : BaseDialogForm
    {
        #region "Constructor"

        public frmUploadCSV()
        {
            InitializeComponent();
        }

        #endregion

        #region "Variable Member"
        #endregion

        #region "Properties"


        public eMaster_Form MASTER_FORM { get; set; }

        private DataTable dtCSVdata { get; set; }

        public string PARTY_TYPE { get; set; }

        public string USER_ID { get; set; }

        public string LOCATION_ID { get; set; }


        private string FileName
        {
            get
            {
                return string.Format("CSV_{0:yyyyMMddHHmmss}", DateTime.Now);
            }
        }

        public DataTable dtSrcUpload { get; set; }

        public bool IsUploadFileValidated { get; set; }                      

        private bool IsTabListSelected
        {
            get
            {
                return true;
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

        private void FocusTabUpload()
        {
            this.tpCSVUpload.PageVisible = true;
            
            this.btnExit.Visible = false;

            this.dntRowNavigator.Visible = false;
            this.ddbExport.Visible = false;

            this.IsUploadFileValidated = false;

            this.txtSrcPath.Text = String.Empty;
            this.grdCSVUpload.DataSource = null;
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = true;
        }

        private void FocusTabMaterialList()
        {
            this.btnExit.Visible = true;

            this.dntRowNavigator.Visible = true;
            this.ddbExport.Visible = true;

            this.IsUploadFileValidated = false;

            this.txtSrcPath.Text = String.Empty;
            this.grdCSVUpload.DataSource = null;
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = false;

        }

        private void OnUploadFail()
        {
            this.grdCSVUpload.DataSource = null;
            this.txtSrcPath.Text = String.Empty;
            this.btnSave.Enabled = false;
            this.btnExit.Visible = false;
            AlreadyAdded = false;
            AleradyAddedMessage = "";
        }

        private void LockUploadScreen()
        {
            this.btnSave.Enabled = false;
            this.btnBrowse.Enabled = false;
        }

        private DataTable CreateTableFromOutputStream(string outputStreamText, string tableName)
        {
            //Process output and return
            string[] split = outputStreamText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length >= 2)
            {
                int iteration = 0;
                DataTable table = null;

                foreach (string values in split)
                {
                    if (iteration == 0)
                    {
                        string[] columnNames = SplitString(values);
                        table = new DataTable(tableName);

                        List<DataColumn> columnList = new List<DataColumn>();

                        foreach (string columnName in columnNames)
                        {
                            columnList.Add(new DataColumn(columnName));
                        }

                        table.Columns.AddRange(columnList.ToArray());
                    }
                    else
                    {
                        string[] fields = SplitString(values);
                        if (table != null)
                        {
                            table.Rows.Add(fields);
                        }
                    }

                    iteration++;
                }

                return table;
            }

            return null;
        }

        private string[] SplitString(string inputString)
        {
            System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.Multiline)
                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Regex reg = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);
            MatchCollection coll = reg.Matches(inputString);
            string[] items = new string[coll.Count];
            int i = 0;
            foreach (Match m in coll)
            {
                items[i++] = m.Groups[0].Value.Trim('"').Trim(',').Trim('"').Trim();
            }
            return items;
        }

        private void UploadPartyMaster()
        {
            try
            {
                string resultMsg = string.Empty;

                using (PartyBLL partyBll = new PartyBLL())
                {
                    if (this.PARTY_TYPE == "C")
                    {
                        resultMsg = partyBll.UploadCustomerMaster(this.dtCSVdata, this.PARTY_TYPE, this.USER_ID);
                    }
                    else if (this.PARTY_TYPE == "V")
                    {
                        resultMsg = partyBll.UploadVendorMaster(this.dtCSVdata, this.PARTY_TYPE, this.USER_ID);
                    }
                    else
                    {
                        resultMsg = "NOTHING UPDATE";
                    }
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void UploadProductMaster()
        {
            try
            {
                string resultMsg = string.Empty;

                using (ProductBLL productBll = new ProductBLL())
                {

                    resultMsg = productBll.UploadProductMaster(this.dtCSVdata, this.LOCATION_ID, this.USER_ID);
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UploadMaterialMaster()
        {
            try
            {
                string resultMsg = string.Empty;

                using (MaterialBLL materialBll = new MaterialBLL())
                {

                    resultMsg = materialBll.UploadMaterialMaster(this.dtCSVdata, this.LOCATION_ID, this.USER_ID);
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UploadPurchaseOrderSage50()
        {
            try
            {
                string resultMsg = string.Empty;

                using (ArrivalBLL arrivalBll = new ArrivalBLL())
                {

                    resultMsg = arrivalBll.UploadPurchaseOrder_SAGE50(this.dtCSVdata, this.USER_ID);
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UploadProductBomSage50()
        {
            try
            {
                string resultMsg = string.Empty;

                using (ProductBLL productBll = new ProductBLL())
                {
                    resultMsg = productBll.UploadProductBom_SAGE50(this.dtCSVdata, this.USER_ID);
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UploadSalesOrderSage50()
        {
            try
            {
                string resultMsg = string.Empty;

                using (ShippingOrderBLL shippingBll = new ShippingOrderBLL())
                {

                    resultMsg = shippingBll.UploadSalesOrder_SAGE50(this.dtCSVdata, this.USER_ID);
                }

                if (resultMsg == "OK")
                {
                    NotifierResult.Show("Upload CSV Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    XtraMessageBox.Show(this, resultMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


        #endregion

        #region "Events"

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLS, this.FileName + ".xls", columnNoExp);
            }
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;

                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.XLSX, this.FileName + ".xlsx", columnNoExp);
            }
        }

        private void bbiExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.PDF, this.FileName + ".pdf", columnNoExp);
            }
        }

        private void bbiExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);

                base.ViewExportToExcel(view, GridExportType.RTF, this.FileName + ".rtf", columnNoExp);
            }
        }

        private void bbiExportText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;
                string[] columnNoExp = new string[1];
                columnNoExp[0] = (view.Columns["REC_STAT"] != null ? "REC_STAT" : string.Empty);
                base.ViewExportToExcel(view, GridExportType.TEXT, this.FileName + ".txt", columnNoExp);
            }
        }

        private void bbiExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.IsTabListSelected)
            {
                GridView view = this.grdCSVUpload.Views[0] as GridView;
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
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            switch (e.KeyCode)
            {
                case Keys.Enter:
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

            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "CSV files (*.csv)|*.csv";

                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = file.FileName;
                    fileExt = Path.GetExtension(filePath).ToLower();

                    //=== 1.Check file extension
                    if (fileExt.CompareTo(".csv") == 0)
                    {
                        //new DataTable();
                        FileStream fs = File.OpenRead(filePath);
                        string content = new StreamReader(fs, Encoding.Default).ReadToEnd();

                        this.dtCSVdata = this.CreateTableFromOutputStream(content, "tblCSV_DATA");

                        this.grdCSVUpload.DataSource = this.dtCSVdata;
                    }
                    else
                    {
                        XtraMessageBox.Show(this, "Please choose .csv file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        this.OnUploadFail();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.MASTER_FORM)
                {
                    case eMaster_Form.FRM_PARTY_MASTER:
                        this.UploadPartyMaster();
                        break;
                    case eMaster_Form.FRM_PRODUCT_MASTER:
                        this.UploadProductMaster();
                        break;
                    case eMaster_Form.FRM_PRODUCT_BOM_MASTER:
                        this.UploadProductBomSage50();
                        break;
                    case eMaster_Form.FRM_MATERIAL_MASTER:
                        this.UploadMaterialMaster();
                        break;
                    case eMaster_Form.FRM_ARRIVAL_RECEIVE:
                        this.UploadPurchaseOrderSage50();
                        break;
                    case eMaster_Form.FRM_SHIPPING_ORDER:
                        this.UploadSalesOrderSage50();
                        break;
                    default:
                        break;
                }

                
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void frmUploadCSV_Load(object sender, EventArgs e)
        {

        }
    }
}