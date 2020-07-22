using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.OleDb;
using HTN.BITS.UIL.PLASESS.Component;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using DevExpress.Utils;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using HTN.BITS.UIL.PLASESS.AdvanceSearch;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.LOVForms;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using System.Globalization;
using DevExpress.XtraGrid.Columns;

namespace HTN.BITS.UIL.PLASESS.Transaction
{
    public partial class frmUploadPlan : BaseChildForm
    {
        OpenFileDialog ExcelBrowserDialog;
        private List<ProductionType> lstProductionType;
        private List<ULPlanDetail> lstulp_dtl;
        private List<Machine> lstMachine;
        private List<Product> lstprod;
        private List<Party> lstparty;
        private eFormState formState = eFormState.ReadOnly;
        private DateTime planDate;
     

     //   private List<ULPlanDetail> delPlanDtl;
       // private GridCheckMarksSelection chkSelect;
        public string FileName
        {
            get
            {
                

                return string.Format("{0}_{1:ddMMMyy}_{2:yyMMddHHmm}", txtPLAN_NO.Text, this.txtULPLAN_DATE.DateTime, DateTime.Now);
            }
        }
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

        private void bbiExportXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToXLS(this.FileName + ".xls", "Microsoft Excel Document", "Microsoft Excel|*.xls");
            base.ViewExportToExcel(this.PlanDTL_gv, GridExportType.XLS, this.FileName + ".xls", null, luePRODUCTION_TYPE.Text,false,null);
        }

        private void bbiExportXLSX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridController.ExportToXLSX(this.FileName + ".xlsx", "Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx");
            base.ViewExportToExcel(this.PlanDTL_gv, GridExportType.XLSX, this.FileName + ".xlsx", null, luePRODUCTION_TYPE.Text, false, null);
        }
        public frmUploadPlan()
        {
            InitializeComponent();

          //  this.ddbExport.Image = base.Language.GetBitmap("ButtonExport");

            this.bbiExportXLS.Glyph = base.Language.GetBitmap("DropdownXLS");
            this.bbiExportXLSX.Glyph = base.Language.GetBitmap("DropdownXLSX");
            
        }
        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = (GridView)this.PlanDTL_gc.Views[0];
                object value = this.luePRODUCTION_TYPE.EditValue;
                switch (fState)
                {
                    //case eFormState.Add:
                    //    //Lock Main menu
                    //    ((frmMainMenu)this.ParentForm).LockMenu = true;

                    //    this.ChangeControlState(false);

                    //    this.btnEdit.Enabled = false;
                    //    this.btnDelete.Enabled = true;
                    //    this.btnSave.Enabled = true;
                    //    this.btnAddNew.Enabled = true;
                    //    this.btnCancel.Enabled = true;
                    //    this.btnCancel.Text = "Cancel";
                    //    this.btnExit.Enabled = false;
                    //    this.btnGenerateJobOrder.Enabled = false;

                    //    view.OptionsBehavior.ReadOnly = false;
                    //    view.OptionsBehavior.Editable = true;
                    //    //SetGridColToEdit(view, value.ToString());

                    //    this.GridDetail_OptionsCustomization(view, false);

                    //    break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;


                     

                        this.ddbExport.Enabled = false;
                        this.btnEdit.Enabled = false;
                        
                        
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Cancel";
                        this.btnExit.Enabled = false;
                        this.btnStatus.Enabled = true;
                        this.btnGenerateJobOrder.Enabled = false;

                       
                        if (btnStatus.Text == "Active")
                        {
                            this.btnSave.Enabled = true;
                            this.btnAddNew.Enabled = true;
                            this.btnDelete.Enabled = true;
                            this.ChangeControlState(false);

                            view.OptionsBehavior.ReadOnly = false;
                            view.OptionsBehavior.Editable = true;

                            //SetGridColToEdit(view, value.ToString());

                            this.GridDetail_OptionsCustomization(view, false);
                        }
                        else {
                            this.btnSave.Enabled = false;
                            this.btnAddNew.Enabled = false;
                            this.btnDelete.Enabled = false;
                            this.ChangeControlState(true);

                            view.OptionsBehavior.ReadOnly = true;
                            view.OptionsBehavior.Editable = false;

                            //SetGridColToEdit(view, value.ToString());

                            this.GridDetail_OptionsCustomization(view, true);
                        }
                       // view.Columns["JOB_NO"].OptionsColumn.ReadOnly = true;
                       // view.Columns["JOB_NO"].OptionsColumn.AllowEdit = false;
                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlState(true);

                        this.ddbExport.Enabled = true;
                        this.btnEdit.Enabled = true; ;
                        this.btnDelete.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnAddNew.Enabled = false;
                        this.btnCancel.Enabled = true;
                        this.btnCancel.Text = "Back";
                        this.btnExit.Enabled = true;

                        if (btnStatus.Text == "Inactive")
                            this.btnGenerateJobOrder.Enabled = false;
                        else
                            this.btnGenerateJobOrder.Enabled = true;

                        this.btnStatus.Enabled = false;
                       // view.Columns["JOB_NO"].OptionsColumn.ReadOnly = false;
                      //  view.Columns["JOB_NO"].OptionsColumn.AllowEdit = true;
                        view.OptionsBehavior.ReadOnly = true;
                        view.OptionsBehavior.Editable = true;
                        lueMc.EditValue = null; //"()";
                        this.GridDetail_OptionsCustomization(view, true);
                      //  view.Columns["JOB_NO"].OptionsColumn.ReadOnly = false;
                      //  view.Columns["JOB_NO"].OptionsColumn.AllowEdit = true;
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

        private void SetGridColToEdit(GridView grdview, string pType)
        {
            string ColName = string.Empty;          
            if(pType.ToUpper() == "H"){
                ColName = ConfigurationManager.AppSettings["ColHOZ"];
                string[] Colarr = ColName.Split(',');
                for (int i = 0; i < Colarr.Length; i++)
                {
                    grdview.Columns[Colarr[i]].OptionsColumn.ReadOnly = false;
                    grdview.Columns[Colarr[i]].OptionsColumn.AllowFocus = true;
                    grdview.Columns[Colarr[i]].OptionsColumn.AllowEdit = true;
                }
            }
            else{
                ColName = ConfigurationManager.AppSettings["ColVER"];
                string[] Colarr = ColName.Split(',');
                for (int i = 0; i < Colarr.Length; i++)
                {
                    grdview.Columns[Colarr[i]].OptionsColumn.ReadOnly = false;
                    grdview.Columns[Colarr[i]].OptionsColumn.AllowFocus = true;
                    grdview.Columns[Colarr[i]].OptionsColumn.AllowEdit = true;
                }
            }

            grdview.FocusedColumn = grdview.Columns["MC_SIZE_TON"];


            grdview.ClearColumnsFilter();
        }
        private void GridDetail_OptionsCustomization(GridView view, bool state)
        {
            try
            {
                view.OptionsCustomization.AllowColumnResizing = state;
                view.OptionsCustomization.AllowFilter = state;

                //option filter
                view.OptionsFilter.AllowFilterEditor = state;
                view.OptionsFilter.AllowMRUFilterList = state;
                view.OptionsFilter.AllowColumnMRUFilterList = state;

                view.OptionsCustomization.AllowSort = state;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeControlState(bool state)
        {
          //  this.btnStatus = state;
                this.lueMc.Properties.ReadOnly = state;
              //  this.txtULPLAN_DATE.Properties.ReadOnly = state;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            try
            {

                ExcelBrowserDialog = new OpenFileDialog();
                ExcelBrowserDialog.CheckFileExists = true;
                ExcelBrowserDialog.InitialDirectory = UiUtility.ExcelInput;
                ExcelBrowserDialog.Filter = "xls, xlsx files (*.xls, *.xlsx)|*.xls;*.xlsx";
                //ExcelBrowserDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                if (ExcelBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filename = ExcelBrowserDialog.FileName;
                    textEdit1.Text = filename;
                    UiUtility.SaveExcelInputLastPath(Path.GetDirectoryName(filename));


                    string ext = Path.GetExtension(ExcelBrowserDialog.FileName);
                    string connectionString = string.Empty;
                    System.Data.DataTable dt = new System.Data.DataTable();
                    if (ext.ToUpper() == ".XLS")
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MaxScanRows=0\"", ExcelBrowserDialog.FileName);
                    else
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MaxScanRows=0\"", ExcelBrowserDialog.FileName);

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        con.Open();

                        // var tableSheet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);



                        var tablerows = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows;
                        string sheetName;
                        if (tablerows.Count > 0)
                        {
                            this.Sheet_cb.Properties.Items.Clear();
                            this.Sheet_cb.SelectedIndex = -1;
                            ImageComboBoxItem item = null;
                            //  int rowindex = 0;
                            foreach (DataRow row in tablerows)
                            {
                                sheetName = row.Field<string>("TABLE_NAME");


                                if (sheetName.EndsWith("$'") || sheetName.EndsWith("$"))
                                {
                                    item = new ImageComboBoxItem();

                                    item.Value = sheetName.Trim('\'', '$');//sheetName.Trim('$');rowindex++;
                                    //item.Description = sheetName.Trim('\'', '$');

                                    this.Sheet_cb.Properties.Items.Add(item);
                                }
                                //add rowfield 
                            }
                            this.Sheet_cb.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

   

        private void Sheet_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ExcelBrowserDialog == null)
                {
                    XtraMessageBox.Show("Please Select File To Upload");
                    return;
                }
                planDate = DateTime.Now;
                string ext = Path.GetExtension(ExcelBrowserDialog.FileName);
                string connectionString = string.Empty;
                // System.Data.DataTable dt = new System.Data.DataTable();
                if (ext.ToUpper() == ".XLS")
                    connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=No;IMEX=1;MaxScanRows=0\"", ExcelBrowserDialog.FileName);
                else
                    connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;MaxScanRows=0\"", ExcelBrowserDialog.FileName);

                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();

                    string sheetName = this.Sheet_cb.SelectedItem.ToString();
                    string qMc = string.Empty;
                    string qCol = string.Empty;
                    string qMcDate = string.Empty;
                    
                    string ColName = string.Empty;
                    if (sheetName.ToUpper() == "HOZ")
                    {
                        ColName = ConfigurationManager.AppSettings["ColHOZ"];
                        qCol = string.Empty;

                        for (int i = 1; i < 38; i++)
                        {
                            if ((i >= 21 && i <= 34))
                                continue;
                            else if (i == 37)
                                qCol += "F" + i ;
                            else
                                qCol += "F" + i + ",";

                        }
                        qMc = string.Format(@"SELECT {0} FROM [{1}$A10:AK65536] WHERE F36 is not null and UCASE(F37) = 'N'", qCol, sheetName);

                        //qMcDate = string.Format(@"SELECT * FROM [{0}$E2:E2]", sheetName);
                        //OleDbDataAdapter daDate = new OleDbDataAdapter(qMcDate, con);
                        //using (System.Data.DataTable DtDate = new System.Data.DataTable(sheetName))
                        //{

                        //    daDate.Fill(DtDate);
                        //    planDate = Convert.ToDateTime(DtDate.Rows[0]["F1"]);
                        //}

                    }
                    else if (sheetName.ToUpper() == "VER")
                    {
                        ColName = ConfigurationManager.AppSettings["ColVER"];
                        qCol = string.Empty;
                        for (var i = 1; i < 38; i++)
                        {
                            if ((i >= 24 && i <= 31) || (i >= 33 && i <= 34))
                                continue;
                            else if (i == 37)
                                qCol += "F" + i;
                            // qCol += "cast([F" + i+"] AS VARCHAR(20)) AS [F"+i+"]";   //cast(Column_A as varchar) Column_A
                           // else if (i == 11 || i == 12)
                           //     qCol += "Cdate([F" + i + "]) As [F" + i + "]";
                            else
                                qCol += "F" + i + ",";


                        }
                     
                //a10
                        qMc = string.Format(@"SELECT {0} FROM [{1}$A11:AK65536] WHERE F36 is not null and UCASE(F37) = 'N'", qCol, sheetName);
                       // qMc = string.Format(@"SELECT {0} FROM [{1}$A11:AK65536] WHERE F36 is not null and UCASE(F37) = 'N'", qCol, sheetName);


            
                        //qMcDate = string.Format(@"SELECT * FROM [{0}$B3:B3]", sheetName);
                        //OleDbDataAdapter daDate = new OleDbDataAdapter(qMcDate, con);
                        //using (System.Data.DataTable DtDate = new System.Data.DataTable(sheetName))
                        //{

                        //    daDate.Fill(DtDate);
                        //    planDate = Convert.ToDateTime(DtDate.Rows[0]["F1"]);
                        //}
                        
                    }
                    else
                        XtraMessageBox.Show("Wrong format!Please check this file");

                    //string ColName = string.Empty;
                    string[] Colarr = ColName.Split(',');

                    OleDbDataAdapter da = new OleDbDataAdapter(qMc, con);
             
                    using (System.Data.DataTable Dt = new System.Data.DataTable(sheetName))
                    {

                        da.Fill(Dt);
                        DataColumn colPROD_SEQ_NO = new DataColumn("PROD_SEQ_NO");
                        DataColumn coltmpMC_NO = new DataColumn("tmpMC_NO");
                        DataColumn coltmpPARTY_ID = new DataColumn("tmpPARTY_ID");

                        for (int i = 0; i < Dt.Columns.Count; i++)
                        {
                            Dt.Columns[i].ColumnName = Colarr[i];

                        }
                        Dt.Columns.Add(colPROD_SEQ_NO);
                        Dt.Columns.Add(coltmpMC_NO);
                        Dt.Columns.Add(coltmpPARTY_ID);
                        string prodType = string.Empty;
                        if (sheetName.ToUpper() == "HOZ")
                            prodType = "H";
                        else if (sheetName.ToUpper() == "VER")
                            prodType = "V";

                        UploadPlanBLL UPL_BLL = new UploadPlanBLL();
                        lstprod = UPL_BLL.Getprod_all_ULP(prodType);

                        List<Machine> lstMc = null;
                        using (MachineBLL mcBll = new MachineBLL())
                        {

                            lstMc = mcBll.GetMachineList(prodType);
                        }
                        for (int j = 0; j < Dt.Rows.Count; j++)
                        {

                            if (string.IsNullOrEmpty(Dt.Rows[j].ItemArray[2].ToString()))
                            {
                                //Dt.Rows[j].ItemArray[2] = "aaa";//Dt.Rows[j - 1].ItemArray[2];
                                Dt.Rows[j][2] = Dt.Rows[j - 1].ItemArray[2];
                            }
                            var PROD_SEQ_NO = from a in lstprod
                                              where a.PRODUCT_NO == Dt.Rows[j]["PRODUCT_NO"].ToString()
                                              select a;

                            if (PROD_SEQ_NO.Any())
                            {
                                Dt.Rows[j]["PROD_SEQ_NO"] = PROD_SEQ_NO.First().PROD_SEQ_NO;
                            }
                            //else {
                            //    Dt.Rows[j]["PRODUCT_NO"] = DBNull.Value;
                            //}
                            if (prodType == "V")
                            {
                              string exMC_NO = Dt.Rows[j]["MC_NO"].ToString();
                              if (exMC_NO.Length == 3)
                              {
                                  Dt.Rows[j]["MC_NO"] = exMC_NO.Insert(2, "0");
                              }
                            }
                                var mc_no = from a in lstMc
                                            where a.MC_NO == Dt.Rows[j]["MC_NO"].ToString()
                                            select a;

                                if (mc_no.Any())
                                {
                                    Dt.Rows[j]["tmpMC_NO"] = mc_no.First().MC_NO;
                                }
                            var partyId = from a in lstparty
                                          where a.PARTY_ID == Dt.Rows[j]["PARTY_ID"].ToString()
                                          select a;

                            if (partyId.Any())
                            {
                                Dt.Rows[j]["tmpPARTY_ID"] = partyId.First().PARTY_ID;
                            }
                            //.ItemArray[2]
                        }

                        Dt.AcceptChanges();
                        //if (gridView1.Columns.Count > 0)
                        gridView1.Columns.Clear();
                        //if(sheetName.ToUpper() == "HOZ"){
                        //    Excel_gc.MainView = gridView1; 
                        //}
                        // else if (sheetName.ToUpper() == "VER") { 
                        //Excel_gc.MainView = gridView2; 
                        // }
                        var rows = from row in Dt.AsEnumerable()
                                   where string.IsNullOrEmpty(row.Field<string>("PROD_SEQ_NO")) || string.IsNullOrEmpty(row.Field<string>("tmpMC_NO")) || string.IsNullOrEmpty(row.Field<string>("tmpPARTY_ID"))
                                   select row;

                        if (rows.Any())
                        {

                            string message = string.Format("{0} Products No., MC No., Customer Id not matching!!", rows.Count());
                            this.simpleButton1.Enabled = false;
                            XtraMessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                          //  this.simpleButton1.Enabled = true;
                                this.simpleButton1.Enabled = (Dt.Rows.Count > 0); //upload button
                        }

                        ConditionsColumnView(Excel_gc);
                        Excel_gc.DataSource = Dt;
                        UiUtility.RemoveActiveRowTop(gridView1);
                        SetPlanDTLGridView(sheetName.ToUpper(), gridView1, true);

                        //Dt.Columns["ColumnName"].ColumnName = "newColumnName";

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
        }
        private void SetPlanDTLGridView(string ptype,GridView grdview,bool ulType)
        {

            try
            {
                grdview.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

                if (ulType)//excel
                {
                    //Hide
                    grdview.Columns["PLAN_STATE"].Visible = false;
                    grdview.Columns["PROD_SEQ_NO"].Visible = false;
                    grdview.Columns["tmpMC_NO"].Visible = false;
                    grdview.Columns["tmpPARTY_ID"].Visible = false;
                    //Bold
                    grdview.Columns["MC_NO"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MC_NO"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PRODUCT_NO"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PRODUCT_NO"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_START"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_START"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_FINISH"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_FINISH"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_DAY"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_DAY"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_PLAN"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_PLAN"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PARTY_ID"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PARTY_ID"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);

                    //Set Caption
                    grdview.Columns["MC_SIZE_TON"].Caption = "M/C SIZE(TON)";
                    grdview.Columns["PDTL_BLOCK"].Caption = "Block";
                    grdview.Columns["MC_NO"].Caption = "M/C NO.";
                    grdview.Columns["PDTL_SEQUENCE"].Caption = "SEQUENCE";
                    grdview.Columns["PRODUCT_NO"].Caption = "PRODUCT NO.";
                    grdview.Columns["MAT_TYPE"].Caption = "MATERIAL TYPE";
                    grdview.Columns["CAV_ACT"].Caption = "CAVITY_ACT";
                    grdview.Columns["CAV_FULL"].Caption = "CAVITY_FULL";
                    grdview.Columns["MP_START"].Caption = "MP. START DATE";
                    grdview.Columns["MP_FINISH"].Caption = "MP. FINISH DATE";
                    grdview.Columns["PLAN_MP_DAY"].Caption = "PLAN MP.(DAY)";
                    grdview.Columns["PRO_SHOT_WEIGHT"].Caption = "PRODUCT/SHOT WEIGHT";

                    grdview.Columns["CYCLE_TIME"].Caption = "CYCLE TIME";
                    grdview.Columns["QTY_DAY"].Caption = "Q'TY DAY";
                    grdview.Columns["TOTAL_MAT_USE_KG"].Caption = "TOTAL MATERIAL USAGE (Kg)";
                    grdview.Columns["TPCT_LOSS"].Caption = "+ 3% LOSS";
                    grdview.Columns["QTY_PLAN"].Caption = "Q'TY PLAN (Pcs.)";
                    grdview.Columns["PLAN_MAT_AVG_DAY_KG"].Caption = "PLAN MAT'L AVERAGE / DAY  (Kg)";
                    grdview.Columns["MAT_DRY"].Caption = "Mat'l for drying";
                    grdview.Columns["PDTL_REMARK"].Caption = "REMARK";
                    grdview.Columns["PARTY_ID"].Caption = "CUSTOMER ID";
                
             
                    if (ptype == "HOZ" || ptype == "H")
                        grdview.Columns["PARTNAME"].Caption = "PART NAME";
                    else if (ptype == "VER" || ptype == "V")
                    {
                        grdview.Columns["INSERT_1"].Caption = "INSERT-1";
                        grdview.Columns["INSERT_2"].Caption = "INSERT-2";
                        grdview.Columns["INSERT_3"].Caption = "INSERT-3";
                        grdview.Columns["TARGET_DAY"].Caption = "TARGET/DAY (10 HRS./SHIFT)";
                        grdview.Columns["PROD_LOT"].Caption = "PROD. LOT";
                    }
                }
                else
                {
                    grdview.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MC_NO"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PRODUCT_NO"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_START"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["MP_FINISH"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_DAY"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["QTY_PLAN"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);
                    grdview.Columns["PARTY_ID"].AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold);

                    //Hide
                    grdview.Columns["PLAN_DTL_ID"].Visible = false;
                    grdview.Columns["PLAN_NO"].Visible = false;
                    grdview.Columns["PROD_SEQ_NO"].Visible = false;

                    grdview.Columns["REC_STAT"].Visible = false;
                    grdview.Columns["PROD_SEQ_NO"].Visible = false;
                   // grdview.Columns["CHANGE_MOLD"].Visible = false;
                   // grdview.Columns["CONTINUE_ORDER"].Visible = false;
                    //grdview.Columns["REVISED_PLAN"].Visible = true;
                   // grdview.Columns["PLAN_STAT"].Visible = true;
                    grdview.Columns["PLAN_STAT"].ColumnEdit = this.rps_PLAN_STAT;
                    grdview.Columns["PLAN_STAT"].Fixed = FixedStyle.Right;

                    grdview.Columns["JOB_NO"].Visible = true;
                    grdview.Columns["JOB_NO"].ColumnEdit = this.rps_JOB_NO;
                    grdview.Columns["JOB_NO"].Fixed = FixedStyle.Right;
                    grdview.Columns["FLAG"].Visible = false;

                    grdview.Columns["N_USER_ID"].Visible = false;
                    grdview.Columns["N_USER_DATE"].Visible = false;
                    grdview.Columns["U_USER_ID"].Visible = false;
                    grdview.Columns["U_USER_DATE"].Visible = false;
                    grdview.Columns["MC_NAME"].Visible = false;
             
                    //Set Caption
                    grdview.Columns["MC_SIZE_TON"].Caption = "M/C SIZE(TON)";
                    grdview.Columns["PDTL_BLOCK"].Caption = "Block";

                    grdview.Columns["MC_NO"].Caption = "M/C NO.";
                    grdview.Columns["MC_NO"].ColumnEdit = this.rps_popMC;

                    grdview.Columns["PDTL_SEQUENCE"].Caption = "SEQUENCE";
                    grdview.Columns["PRODUCT_NO"].Caption = "PRODUCT NO.";
                    grdview.Columns["PRODUCT_NO"].ColumnEdit = this.rps_popPRODUCT;

                    grdview.Columns["MAT_TYPE"].Caption = "MATERIAL TYPE";
                    grdview.Columns["CAV_ACT"].Caption = "CAVITY_ACT";
                    grdview.Columns["CAV_ACT"].ColumnEdit = this.rps_txtInteger;
                    grdview.Columns["CAV_FULL"].Caption = "CAVITY_FULL";
                    grdview.Columns["CAV_FULL"].ColumnEdit = this.rps_txtInteger;

                    grdview.Columns["MP_START"].Caption = "MP. START DATE";
                    grdview.Columns["MP_START"].ColumnEdit = this.rps_dteDATE;

                    grdview.Columns["MP_FINISH"].Caption = "MP. FINISH DATE";
                    grdview.Columns["MP_FINISH"].ColumnEdit = this.rps_dteDATE;

                    grdview.Columns["PLAN_MP_DAY"].Caption = "PLAN MP.(DAY)";
                    grdview.Columns["PLAN_MP_DAY"].ColumnEdit = this.rps_txtDecimal_2;

                    grdview.Columns["PRO_SHOT_WEIGHT"].Caption = "PRODUCT/SHOT WEIGHT";
                    grdview.Columns["PRO_SHOT_WEIGHT"].ColumnEdit = this.rps_txtDecimal_4;

                    grdview.Columns["CYCLE_TIME"].Caption = "CYCLE TIME";
                    grdview.Columns["CYCLE_TIME"].ColumnEdit = this.rps_txtDecimal_2;

                    grdview.Columns["QTY_DAY"].Caption = "Q'TY DAY";
                    grdview.Columns["QTY_DAY"].ColumnEdit = this.rps_txtInteger;
                    grdview.Columns["TOTAL_MAT_USE_KG"].Caption = "TOTAL MATERIAL USAGE (Kg)";
                    grdview.Columns["TOTAL_MAT_USE_KG"].ColumnEdit = this.rps_txtDecimal_2;
                    grdview.Columns["TPCT_LOSS"].Caption = "+ 3% LOSS";
                    grdview.Columns["TPCT_LOSS"].ColumnEdit = this.rps_txtDecimal_2;
                    grdview.Columns["QTY_PLAN"].Caption = "Q'TY PLAN (Pcs.)";
                    grdview.Columns["QTY_PLAN"].ColumnEdit = this.rps_txtInteger;
                    grdview.Columns["PLAN_MAT_AVG_DAY_KG"].Caption = "PLAN MAT'L AVERAGE / DAY  (Kg)";
                    grdview.Columns["PLAN_MAT_AVG_DAY_KG"].ColumnEdit = this.rps_txtDecimal_2;
                    grdview.Columns["MAT_DRY"].Caption = "Mat'l for drying";
                    grdview.Columns["MAT_DRY"].ColumnEdit = this.rps_txtDecimal_2;
                    grdview.Columns["PDTL_REMARK"].Caption = "REMARK";
                    grdview.Columns["PARTY_ID"].Caption = "CUSTOMER ID";
                    grdview.Columns["PARTY_ID"].ColumnEdit = this.rps_popPARTY;

                    if (ptype == "HOZ" || ptype == "H")
                    {
                        grdview.Columns["PARTNAME"].Visible = true;
                        grdview.Columns["PARTNAME"].Caption = "PART NAME";

                        grdview.Columns["INSERT_1"].Visible = false;
                        grdview.Columns["INSERT_2"].Visible = false;
                        grdview.Columns["INSERT_3"].Visible = false;
                        grdview.Columns["TARGET_DAY"].Visible = false;
                        grdview.Columns["PROD_LOT"].Visible = false;
                    }
                    else if (ptype == "VER" || ptype == "V")
                    {
                        grdview.Columns["INSERT_1"].Visible = true;
                        grdview.Columns["INSERT_2"].Visible = true;
                        grdview.Columns["INSERT_3"].Visible = true;
                        grdview.Columns["TARGET_DAY"].Visible = true;
                        grdview.Columns["PROD_LOT"].Visible = true;

                        grdview.Columns["INSERT_1"].Caption = "INSERT-1";
                        grdview.Columns["INSERT_2"].Caption = "INSERT-2";
                        grdview.Columns["INSERT_3"].Caption = "INSERT-3";
                        grdview.Columns["TARGET_DAY"].Caption = "TARGET/DAY (10 HRS./SHIFT)";
                        grdview.Columns["TARGET_DAY"].ColumnEdit = this.rps_txtInteger;
                        grdview.Columns["PROD_LOT"].Caption = "PROD. LOT";

                        grdview.Columns["PARTNAME"].Visible = false;
                    }
                }

                //Set Format
                grdview.Columns["PLAN_MP_DAY"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["PLAN_MP_DAY"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["PRO_SHOT_WEIGHT"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["PRO_SHOT_WEIGHT"].DisplayFormat.FormatString = "{0:#,##0.0000}";
                grdview.Columns["CYCLE_TIME"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["CYCLE_TIME"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["TOTAL_MAT_USE_KG"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["TOTAL_MAT_USE_KG"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["TPCT_LOSS"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["TPCT_LOSS"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["TOTAL_MAT_USE_KG"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["TOTAL_MAT_USE_KG"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["PLAN_MAT_AVG_DAY_KG"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["PLAN_MAT_AVG_DAY_KG"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["MAT_DRY"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["MAT_DRY"].DisplayFormat.FormatString = "{0:#,##0.00}";
                grdview.Columns["QTY_DAY"].DisplayFormat.FormatType = FormatType.Numeric;
                grdview.Columns["QTY_DAY"].DisplayFormat.FormatString = "{0:#,##0}";

                grdview.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ConditionsColumnViewPLAN_STAT(GridControl grd)
        {


            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[1];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["PLAN_STAT"];
                cnArr[0].Expression = @"[PLAN_STAT] = 'F'";
                cnArr[0].Appearance.BackColor = Color.WhiteSmoke;
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

        private void ConditionsColumnView(GridControl grd)
        {
           
      
            try
            {
                StyleFormatCondition[] cnArr = new StyleFormatCondition[3];

                cnArr[0] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[0].Column = ((ColumnView)grd.MainView).Columns["PROD_SEQ_NO"];
                cnArr[0].Expression = @"[PROD_SEQ_NO] IS NULL";
                cnArr[0].Appearance.BackColor = Color.Red;
                cnArr[0].Appearance.Options.UseBackColor = true;
                cnArr[0].Appearance.Options.UseForeColor = true;
                cnArr[0].ApplyToRow = true;


                cnArr[1] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[1].Column = ((ColumnView)grd.MainView).Columns["tmpMC_NO"];
                cnArr[1].Expression = @"[tmpMC_NO] IS NULL";
                cnArr[1].Appearance.BackColor = Color.Orange;
                cnArr[1].Appearance.Options.UseBackColor = true;
                cnArr[1].Appearance.Options.UseForeColor = true;
                cnArr[1].ApplyToRow = true;

                cnArr[2] = new StyleFormatCondition(FormatConditionEnum.Expression);
                cnArr[2].Column = ((ColumnView)grd.MainView).Columns["tmpPARTY_ID"];
                cnArr[2].Expression = @"[tmpPARTY_ID] IS NULL";
                cnArr[2].Appearance.BackColor = Color.Yellow;
                cnArr[2].Appearance.Options.UseBackColor = true;
                cnArr[2].Appearance.Options.UseForeColor = true;
                cnArr[2].ApplyToRow = true;

                ((ColumnView)grd.MainView).FormatConditions.AddRange(cnArr);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InsertPlan();
           
        }

        private void frmUploadPlan_Load(object sender, EventArgs e)
        {
            this.simpleButton1.Enabled = false;
           
        }

        public void InsertPlan()
        {
            string resultMsg = string.Empty;
            string userId = ((frmMainMenu)this.ParentForm).UserID;
            string sheetName = this.Sheet_cb.SelectedItem.ToString();
            string planType = string.Empty;
            try
            {
                System.Data.DataTable dtPlan = this.Excel_gc.DataSource as System.Data.DataTable;

                using (UploadPlanBLL ulp_bll = new UploadPlanBLL())
                {
                    if(sheetName.ToUpper() == "HOZ")
                        planType = "H";
                    else if(sheetName.ToUpper() == "VER"){
                        planType = "V";
                    }

                    resultMsg = ulp_bll.InsertPlan(planType, dtPlan, planDate, userId);
                }

                if (resultMsg.Equals("OK"))
                {
              
                    XtraMessageBox.Show("Insert Plan Complete.");
                   // this.grdMtlSupplySheet.DataSource = null;
                    Sheet_cb.Properties.Items.Clear();
                    Sheet_cb.SelectedIndex = -1;
                    gridView1.Columns.Clear();
                    textEdit1.Text = null;
               
                    ExcelBrowserDialog = null;
                    Excel_gc.DataSource = null;
                    simpleButton1.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }
        private void GetULPlanList(string type, string findValue, DateTime? startDate, DateTime? toDate)
        {
            List<ULPlan> lstULPlan = null;
            try
            {
                base.ExecutionStart();
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (UploadPlanBLL ULPlanBll = new UploadPlanBLL())
                {
                    lstULPlan = ULPlanBll.GetULPlanList(type, findValue, startDate, toDate);
                }

                //DataTable dtJobOrder = UiUtility.BuildDataTable<JobOrder>(lstJobOrd);
                //dtJobOrder.DefaultView.Sort = "JOB_DATE DESC";

                this.ulplst_gc.DataSource = lstULPlan; //dtJobOrder;  //
               //this.dntJobOrder.DataSource = lstJobOrd; //dtJobOrder;  //

              //  this.chkSelect.ClearSelection();
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
        public void AdvanceSearchULPlan(string jobOrdNo, string type, DateTime? startDate, DateTime? toDate)
        {

        }
        private void apply_btn_Click(object sender, EventArgs e)
        {
            string type = string.Empty;

            if (this.lueSearchPROD_TYPE.EditValue != null)
            {
                type = this.lueSearchPROD_TYPE.EditValue.ToString();
            }
            GetULPlanList(type, this.txtSearch.Text, this.dteFromDate.DateTime, this.dteToDate.DateTime);
        }


        private void frmUploadPlan_LoadCompleted()
        {
            this.KeyPreview = true;

            this.xtra_Plan_Details_TabPage.PageVisible = false;
            this.dteFromDate.DateTime = UiUtility.FirstDayofMonth();
            this.dteToDate.DateTime = DateTime.Now;
            this.InitializaLOVData();
            this.GetULPlanList(string.Empty, string.Empty, this.dteFromDate.DateTime, this.dteToDate.DateTime);

            this.FormState = eFormState.ReadOnly;

        }
        private void InitializaLOVData()
        {

            try
            {
                /*
                using (ProductBLL pdBll = new ProductBLL())
                {
                    List<Unit> lstUnit = pdBll.GetUnitList();
                    if (lstUnit != null)
                    {
                        this.lueUNIT.Properties.DataSource = lstUnit;
                        if (lstUnit.Count > 0)
                        {
                            Unit unitTemp = lstUnit.Find(delegate(Unit _unit)
                            {
                                return _unit.SEQ_NO == "PCS";
                            });
                            if (unitTemp != null)
                            {
                                this.lueUNIT.EditValue = unitTemp.SEQ_NO;
                            }
                            else
                            {
                                //default
                                this.lueUNIT.EditValue = lstUnit[0].SEQ_NO;
                            }
                        }
                    }
                }
                 * 
                using (MachineBLL mcBll = new MachineBLL())
                {
                    this.rps_MC_NO.DataSource = mcBll.GetMachineList(string.Empty);
                }
                */
                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                    this.lstProductionType = pdtBll.GetProductionTypeList();
                }
                using (PartyBLL partyBll = new PartyBLL())
                {
                    lstparty = partyBll.LovPratyList("C",string.Empty);
                 
                }
       
               // this.grvJobOrder_rps_MACHINE.DataSource = this.lstMachine;
               // this.grvJobOrder_rps_PRODUCTION_TYPE.DataSource = this.lstProductionType;
                this.luePRODUCTION_TYPE.Properties.DataSource = this.lstProductionType;
                this.lueSearchPROD_TYPE.Properties.DataSource = this.lstProductionType;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

      

        private void ULPlan_gv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                base.SuspendLayout();

                GridView view = (GridView)sender;
                System.Drawing.Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {

                  

                    string ulplanNo = view.GetRowCellValue(info.RowHandle, "PLAN_NO").ToString();

                    //this.btnAddNew.Visible = false;

                    ////Change tab view.
                    this.xtra_Plan_List_TabPage.PageEnabled = false;

                    this.xtra_Plan_Details_TabPage.PageVisible = true;
                    this.xtraPlan_tab.SelectedTabPage = this.xtra_Plan_Details_TabPage;

                    ////Call record detail.
                    this.GetBindingPlanDetails(ulplanNo);

                    this.btnCancel.Text = "&Back";//base.Language.GetValue(string.Format("{0}_btnBack", this.Name)); //"Back";
                   // this.dntJobOrder.Focus();

                    base.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void GetBindingPlanDetails(string ulplanNo)
        {
            ULPlan planNo = null;
            try
            {
                using (UploadPlanBLL ULPlanBll = new UploadPlanBLL())
                {
                    planNo = ULPlanBll.GetULPlanByPlanNo(ulplanNo); //.GetJobOrder(jobOrdNo);
                }

                if (planNo != null)
                {
                    //base.ClearValidControls(this, ref this.dxErrorProvider1);
                    txtPLAN_NO.Text = planNo.PLAN_NO;
                    txtULPLAN_DATE.EditValue = planNo.PLAN_DATE;

                    if (planNo.REC_STAT)
                    {
                        btnStatus.Image = base.Language.GetBitmap("GreenIcon16");
                        btnStatus.Text = "Active";
                        //icbREC_STAT.EditValue = planNo.REC_STAT;
                    }
                    else {
                        btnStatus.Image = base.Language.GetBitmap("RedIcon16");
                        btnStatus.Text = "Inactive";
                    }
                    
                    luePRODUCTION_TYPE.EditValue = planNo.PROD_TYPE;

                    GetPlanDetails(planNo.PLAN_NO, planNo.PROD_TYPE);


                    //PREPARE TAB SELECT
                    UploadPlanBLL UPL_BLL = new UploadPlanBLL();
                    lstMachine = UPL_BLL.GetActiveMachineList(planNo.PROD_TYPE);

                

                    //using (MachineBLL mcBll = new MachineBLL())
                    //{
                    //    lstMachine = mcBll.GetMachineList(planNo.PROD_TYPE);
                    //}
                   // UploadPlanBLL UPL_BLL = new UploadPlanBLL();
                    lstprod = UPL_BLL.Getprod_all_ULP(planNo.PROD_TYPE);

                    this.lueMc.Properties.DataSource = lstMachine;

                    ConditionsColumnViewPLAN_STAT(PlanDTL_gc);

                    //GridView view = (GridView)this.PlanDTL_gc.Views[0];
                    //view.Columns["JOB_NO"].OptionsColumn.ReadOnly = false;
                    //view.Columns["JOB_NO"].OptionsColumn.AllowEdit = true;
                 //   this.PlanDTL_gv.ActiveFilterString = "[FLAG] <> 0";
                    //this.chkSelect = new GridCheckMarksSelection(this.PlanDTL_gv);
                }
                else
                {
                    ClearDataOnScreen();
                    XtraMessageBox.Show(this, "No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetPlanDetails(string planNo, string ptype)
        {
            try
            {
            //    var lst = new BindingList<ULPlanDetail>(null);
            //this.grd.datas = lst

                using (UploadPlanBLL ULPlanBll = new UploadPlanBLL())
                {
                    this.lstulp_dtl = ULPlanBll.GetPlanDetails(planNo);
                }

                if (this.lstulp_dtl != null)
                {
                    var Blstulp_dtl = new BindingList<ULPlanDetail>(lstulp_dtl);
                    
                    //this.lotSelect.ClearSelection();
                    PlanDTL_gc.DataSource = Blstulp_dtl;
                    
                    SetPlanDTLGridView(ptype, PlanDTL_gv,false);
                    this.PlanDTL_gv.ActiveFilterString = "[FLAG] <> 0";
                    //GridView view = (GridView)this.grdLotPlaning.Views[0];
                    //UiUtility.SetGridReadOnly(view, true);

                    //this.lotSelect.View.Columns["CheckMarkSelection"].VisibleIndex = 0;
                    //this.lotSelect.View.Columns["CheckMarkSelection"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool CheckExistMachine(string mcno, GridView view)
        {
            bool isExist = false;
            try
            {
                if (this.lstMachine == null)
                    return false;

                var isMc = from m in this.lstMachine
                           where m.MC_NO == mcno
                           select m;

                isExist = isMc.Any();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error");
            }
            return isExist;
        }
        private bool CheckExistProductNo(string prodNo, GridView view)
        {
            bool isExist = false;
            try
            {
                if (this.lstprod == null)
                    return false;

                var isProduct = from p in this.lstprod
                                where p.PRODUCT_NO == prodNo
                                select p;

                if (isProduct.Any())
                {
                    isExist = true;

                    view.SetFocusedRowCellValue("PROD_SEQ_NO", isProduct.First().PROD_SEQ_NO);
                    view.SetFocusedRowCellValue("PARTNAME", isProduct.First().PRODUCT_NAME);
                    view.SetFocusedRowCellValue("MAT_TYPE", isProduct.First().MATERIAL_NAME);

                    view.FocusedColumn = view.Columns["MAT_TYPE"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error");
            }
            return isExist;
        }
        private bool CheckExistParty(string partyId, GridView view)
        {
            bool isExist = false;
            try
            {
                if (this.lstparty == null)
                    return false;

                var isParty = from c in this.lstparty
                              where c.PARTY_ID == partyId
                              select c;

                if (isParty.Any())
                {
                    isExist = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error");
            }
            return isExist;
        }
        private void ClearDataOnScreen()
        {
            try
            {
                this.txtPLAN_NO.Text = string.Empty; ;
                this.txtULPLAN_DATE.EditValue = DateTime.Now;

                
                this.luePRODUCTION_TYPE.EditValue = null;
       

                //this.GetJobLotPlanning(string.Empty);

                ////clear Gridview
                //this.dtbLotPlaning.Rows.Clear();
                //this.dtbLotPlaning.AcceptChanges();
                //this.grdLotPlaning.DataSource = dtbLotPlaning;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnGeneratePlan_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string userId = ((frmMainMenu)this.ParentForm).UserID;
            try
            {
                using (UploadPlanBLL ulpBll = new UploadPlanBLL())
                {

                    resultMsg = ulpBll.GenerateJob(txtPLAN_NO.Text, userId);//luePRODUCTION_TYPE.EditValue.ToString()

                    if (resultMsg.Equals("OK"))
                    {
                        XtraMessageBox.Show(this, "Generate JobOrder Complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.GetBindingPlanDetails(txtPLAN_NO.Text);
                        this.FormState = eFormState.ReadOnly;

                    }
                }

                //var Genlst = from a in lstulp_dtl
                //             where a.PLAN_STAT == "I"
                //             select a;

                //if (Genlst.Any()) //Loop Generated
                //{

                //    foreach (ULPlanDetail planDTL in Genlst)
                //        {
                //            //planDTL.MC_NO;
                //            //planDTL.PRODUCT_NO;
                //            //planDTL.MP_START
                //            //planDTL.MP_FINISH
                //            //planDTL.QTY_PLAN
                //            //planDTL.PARTY_ID
                          
                //        }
                   

                //}
                //else
                //{
                //    XtraMessageBox.Show(this, "All Plan details already Generate JobOrder", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch(Exception ex){

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
           


        }

       


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRowFormValidated()) return;

                string resultMsg = string.Empty;
                string userId = ((frmMainMenu)this.ParentForm).UserID;
                using (UploadPlanBLL ulpBll = new UploadPlanBLL())
                {

                    resultMsg = ulpBll.InsUpdDelPlanListFLAG(lstulp_dtl, userId);//luePRODUCTION_TYPE.EditValue.ToString()

                    if (resultMsg.Equals("OK"))
                    {
                        this.GetBindingPlanDetails(txtPLAN_NO.Text);
                        this.FormState = eFormState.ReadOnly;

                    }
                }
            }
            catch(Exception ex){

            }
        

           
            //refresh
        }

      
  

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:

                        this.xtra_Plan_Details_TabPage.PageVisible = false;
                        this.xtra_Plan_List_TabPage.PageEnabled = true;
                        this.xtraPlan_tab.SelectedTabPage = this.xtra_Plan_List_TabPage;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;

                        break;

                    case eFormState.Edit:

                        this.GetBindingPlanDetails(this.txtPLAN_NO.Text);

                        break;
                    case eFormState.ReadOnly:

                        this.xtra_Plan_Details_TabPage.PageVisible = false;
                        this.xtra_Plan_List_TabPage.PageEnabled = true;
                        this.xtraPlan_tab.SelectedTabPage = this.xtra_Plan_List_TabPage;

                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;


                        break;
                    default:
                        break;
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

        #region Machine

        private void rps_popMC_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            
                GridView view = this.PlanDTL_gc.MainView as GridView;

                ButtonEdit editor = (ButtonEdit)sender;
                //Open Popup For Select Supplier.
                DialogResult result;
                string mcno = string.Empty;
                try
                {
                using (frmLOVMachine fMcList = new frmLOVMachine())
                {
                    object value = this.luePRODUCTION_TYPE.EditValue;

                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        fMcList.MACHINE_TYPE = value.ToString();
                    }
                    else
                    {
                        fMcList.MACHINE_TYPE = "V";
                    }

                    result = UiUtility.ShowPopupForm(fMcList, this, true);

                    mcno = fMcList.MC_NO;
                }

                if (result == DialogResult.OK)
                {
                    editor.Text = mcno;
                    view.SetFocusedValue(mcno);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void rps_popMC_Validating(object sender, CancelEventArgs e)
        {
            GridView view = this.PlanDTL_gc.MainView as GridView;
            ButtonEdit editor = (ButtonEdit)sender;

            try
            {
                if (editor.EditValue != null)
                {
                    bool isValid = this.CheckExistMachine(editor.Text, view);//true
                    if (!isValid)
                    {
                        DialogResult result;
                        string mcno = string.Empty;

                        using (frmLOVMachine fMcList = new frmLOVMachine())
                        {
                            object value = this.luePRODUCTION_TYPE.EditValue;

                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                fMcList.MACHINE_TYPE = value.ToString();
                            }
                            else
                            {
                                fMcList.MACHINE_TYPE = "V";
                            }

                            result = UiUtility.ShowPopupForm(fMcList, this, true);

                            mcno = fMcList.MC_NO;
                        }

                        if (result == DialogResult.OK)
                        {
                            editor.Text = mcno;
                            view.SetFocusedValue(mcno);
                        }
                        else
                        {
                            editor.Undo();
                            view.FocusedColumn = view.Columns["MC_NO"];
                        }
                    }
                  
                }
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        private void rps_popPRODUCT_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = this.PlanDTL_gc.MainView as GridView;

            ButtonEdit editor = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string prodNo = string.Empty;
            string prodSEQ = string.Empty;
            string prodName = string.Empty;
            string mtlName = string.Empty;
            try
            {
                using (frmLOVProduct fProdList = new frmLOVProduct())
                {
                    object value = this.luePRODUCTION_TYPE.EditValue;

                    fProdList.FormCalling = eFormCalling.fUploadPlan;

                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        fProdList.PRODUCTION_TYPE = value.ToString();
                    }
                    else
                    {
                        fProdList.PRODUCTION_TYPE = "V";
                    }

                    fProdList.ULPlan_GetProductList();

                    result = UiUtility.ShowPopupForm(fProdList, this, true);

                    prodNo = fProdList.PRODUCT_NO;
                    prodSEQ = fProdList.PROD_SEQ_NO;
                    mtlName = fProdList.MATERIAL_NAME;
                    prodName = fProdList.PRODUCT_NAME;
                }

                if (result == DialogResult.OK)
                {
                    editor.Text = prodNo;

                    view.SetFocusedValue(prodNo);
                    view.SetFocusedRowCellValue("PROD_SEQ_NO", prodSEQ);
                    view.SetFocusedRowCellValue("PARTNAME", prodName);
                    view.SetFocusedRowCellValue("MAT_TYPE", mtlName);

                    view.FocusedColumn = view.Columns["MAT_TYPE"];
                }

                if (result == DialogResult.OK)
                {
                    editor.Text = prodNo;
                    view.SetFocusedRowCellValue("PROD_SEQ_NO", prodSEQ);
                    view.SetFocusedValue(prodNo);
                }
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void rps_popPRODUCT_Validating(object sender, CancelEventArgs e)
        {
            GridView view = this.PlanDTL_gc.MainView as GridView;
            ButtonEdit editor = (ButtonEdit)sender;

            try
            {
                if (editor.EditValue != null)
                {
                    bool isValid = this.CheckExistProductNo(editor.Text, view);//true
                    if (!isValid)
                    {
                        DialogResult result;
                        string prodNo = string.Empty;
                        string prodSEQ = string.Empty;
                        string prodName = string.Empty;
                        string mtlName = string.Empty;

                        using (frmLOVProduct fProdList = new frmLOVProduct())
                        {
                            object value = this.luePRODUCTION_TYPE.EditValue;

                            fProdList.FormCalling = eFormCalling.fUploadPlan;

                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                fProdList.PRODUCTION_TYPE = value.ToString();
                            }
                            else
                            {
                                fProdList.PRODUCTION_TYPE = "V";
                            }

                            fProdList.ULPlan_GetProductList();

                            result = UiUtility.ShowPopupForm(fProdList, this, true);

                            prodNo = fProdList.PRODUCT_NO;
                            prodName = fProdList.PRODUCT_NAME;
                            mtlName = fProdList.MATERIAL_NAME;
                            prodSEQ = fProdList.PROD_SEQ_NO;
                        }

                        if (result == DialogResult.OK)
                        {
                            editor.Text = prodNo;
                            
                            view.SetFocusedValue(prodNo);
                            view.SetFocusedRowCellValue("PROD_SEQ_NO", prodSEQ);
                            view.SetFocusedRowCellValue("PARTNAME", prodName);
                            view.SetFocusedRowCellValue("MAT_TYPE", mtlName);

                            view.FocusedColumn = view.Columns["MAT_TYPE"];
                        }
                        else
                        {
                            editor.Undo();
                            view.FocusedColumn = view.Columns["PRODUCT_NO"];
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void rps_popPARTY_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = this.PlanDTL_gc.MainView as GridView;

            ButtonEdit editor = (ButtonEdit)sender;
            //Open Popup For Select Supplier.
            DialogResult result;
            string partyId = string.Empty;

            try
            {
                using (frmLOVParty fPartyList = new frmLOVParty())
                {
                    string value = "C";

                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        fPartyList.PARTY_TYPE = value.ToString();
                    }


                    result = UiUtility.ShowPopupForm(fPartyList, this, true);

                    partyId = fPartyList.PARTY_ID;
                }

                if (result == DialogResult.OK)
                {
                    editor.Text = partyId;
                    view.SetFocusedValue(partyId);
                }
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void rps_popPARTY_Validating(object sender, CancelEventArgs e)
        {
            GridView view = this.PlanDTL_gc.MainView as GridView;
            ButtonEdit editor = (ButtonEdit)sender;

            try
            {
                if (editor.EditValue != null)
                {
                    bool isValid = this.CheckExistParty(editor.Text, view);//true
                    if (!isValid)
                    {
                        DialogResult result;
                        string partyId = string.Empty;

                        using (frmLOVParty fPartyList = new frmLOVParty())
                        {
                            string value = "C";

                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                fPartyList.PARTY_TYPE = value.ToString();
                            }


                            result = UiUtility.ShowPopupForm(fPartyList, this, true);

                            partyId = fPartyList.PARTY_ID;
                        }

                        if (result == DialogResult.OK)
                        {
                            editor.Text = partyId;
                            view.SetFocusedValue(partyId);
                        }
                        else
                        {
                            editor.Undo();
                            view.FocusedColumn = view.Columns["PARTY_ID"];
                        }
                    }
                   
                }
            }

            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtPOST_REF.Text))
            
           //     XtraMessageBox.Show(this, "This SO. already post data!!\nCan't Edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                this.FormState = eFormState.Edit;

              //  GridView view = (GridView)this.PlanDTL_gc.Views[0];
              //  view.Columns["JOB_NO"].OptionsColumn.ReadOnly = true;
             //   view.Columns["JOB_NO"].OptionsColumn.AllowEdit = false;
          
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            object MCvalue = lueMc.EditValue;
            if (MCvalue == null)
            {
                XtraMessageBox.Show(this, "Please select M/C No.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
           // this.FormState = eFormState.Add;
            object row = lueMc.Properties.GetDataSourceRowByKeyValue(lueMc.EditValue);
            
            //string MCName= row["MACHINE_NAME"].ToString();
            //row as Machine
          //  Machine MCNamea = (row as Machine).MACHINE_NAME;
            string MCName = (row as Machine).MACHINE_NAME;
            //int pos = -1;
            //var lstActiveulpDtl = from a in lstulp_dtl
            //                  where a.FLAG != 0
            //                  select a;

            //if (lstActiveulpDtl.Any())
            //{
            //    //Dt.Rows[j]["PROD_SEQ_NO"] = PROD_SEQ_NO.First().PROD_SEQ_NO;
            //    //pos = lstActiveulpDtl.Count();
              
            //}
            //else
            //    pos = lstulp_dtl.Count;
            
            GridView view = this.PlanDTL_gc.Views[0] as GridView;
            int pos = lstulp_dtl.Count;
            //int pos = view.RowCount;
            int compareMC = 0;


            for (int i = 0; i < lstulp_dtl.Count; i++)
            {
               // compareMC = string.Compare(MCName, view.GetRowCellDisplayText(i, "MC_NAME"));
                compareMC = string.Compare(MCName, lstulp_dtl[i].MC_NAME);
                if (compareMC == -1) {
                    pos = i;
                    break;
                }
                //if (lstulp_dtl[i].MC_NO == MCvalue.ToString()) {
                //    pos = i;
                //}
            }

            //var newList = (from a in lstulp_dtl
            //              where a.FLAG != 0
            //              select a).ToList();
            //if (newList.Any())
            //{
            //    newList.Insert(pos, new ULPlanDetail()
            //    {
            //        MC_NO = MCvalue.ToString(),
            //        MC_NAME = MCName,
            //        PLAN_NO = txtPLAN_NO.Text,
            //        PLAN_STAT = "I",
            //        FLAG = 2

            //    });
            //}
            //else
            //{
            //}

            //this.lstulp_dtl.Insert(pos, new ULPlanDetail()
            //{
            //    MC_NO = MCvalue.ToString(),
            //    MC_NAME = MCName,
            //    PLAN_NO = txtPLAN_NO.Text,
            //    PLAN_STAT = "I",
            //    FLAG = 2

            //});

           int posview = view.RowCount;
            for (int i = 0; i < view.RowCount; i++)
            {
                compareMC = string.Compare(MCName, view.GetRowCellDisplayText(i, "MC_NAME"));
                //compareMC = string.Compare(MCName, lstulp_dtl[i].MC_NAME);
                if (compareMC == -1)
                {
                    posview = i;
                    break;
                }
                //if (lstulp_dtl[i].MC_NO == MCvalue.ToString()) {
                //    pos = i;
                //}
            }
            this.lstulp_dtl.Insert(pos, new ULPlanDetail()
            {
                MC_NO = MCvalue.ToString(),
                MC_NAME = MCName,
                PLAN_NO = txtPLAN_NO.Text,
                PLAN_STAT = "I",
                FLAG = 2

            });

            this.PlanDTL_gc.RefreshDataSource();

            //var delRow = (from d in this.lstulp_dtl
            //              where d.FLAG == 0
            //              select d).Count();
            this.PlanDTL_gv.FocusedRowHandle = posview;
            //this.PlanDTL_gv.FocusedRowHandle=
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            //check ว่า Gen ไปรึยัง 
            try
            {
                GridView view = this.PlanDTL_gc.Views[0] as GridView;

                

                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                int rowHandle = view.FocusedRowHandle;
                if (rowHandle < 0) {
                    XtraMessageBox.Show(this, "Plese select record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (view.GetRowCellValue(rowHandle, "PLAN_STAT").ToString().ToUpper() == "F")//F = Generate
                {
                    XtraMessageBox.Show(this, "This Plan. already generate Job Order!!\nCan't Delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                 }

                int flag = Convert.ToInt32(view.GetRowCellValue(rowHandle, "FLAG"), NumberFormatInfo.CurrentInfo);
                DialogResult isDelete;
                isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (isDelete == DialogResult.Yes)
                {
                    this.DeletePlanDetail(view, rowHandle);

                   // SendKeys.Send("{F2}");
                }
           
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void DeletePlanDetail(GridView view, int rowSelect)
        {
            if (view == null || view.SelectedRowsCount == 0) return;
            if (this.lstulp_dtl == null) return;

            try
            {
                var planDtl = view.GetRow(rowSelect) as ULPlanDetail;
                planDtl.FLAG = 0;

                this.PlanDTL_gc.RefreshDataSource();
               
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (rowSelect == view.RowCount)
                    rowSelect--;

                view.FocusedRowHandle = rowSelect;
                view.ShowEditor();
            }
        }

        private void PlanDTL_gv_CustomRowFilter(object sender, RowFilterEventArgs e)
        {
            //GridView view = sender as GridView;

            //if ((int)view.GetRowCellValue(e.ListSourceRow, "FLAG") == 0)
            //{
            //    e.Visible = false;

            //}

        }

        private void PlanDTL_gv_KeyDown(object sender, KeyEventArgs e)
        {
             if (this.FormState == eFormState.ReadOnly) return;

             try
             {
                 GridView view = sender as GridView;

                 if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                 if (e.KeyCode == Keys.Delete)
                 {
                     btnDelete_Click(sender, e);

                 }
             }
             catch (Exception ex) {
                 XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
             }
        }
       
        private int rowupdated = 0;
        private void PlanDTL_gv_RowUpdated(object sender, RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;

            try
            {
                if (rowupdated.Equals(0))
                    rowupdated++;
                else
                {
                    rowupdated = 0;
                    return;
                }

                if ((view.FocusedRowHandle != GridControl.InvalidRowHandle) && (view.FocusedRowHandle != GridControl.NewItemRowHandle))
                {
                    int flag = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "FLAG").ToString());
                    if (flag != 2)
                    {
                        view.SetFocusedRowCellValue("FLAG", 3);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "RowUpdated" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void PlanDTL_gv_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            GridView view = (GridView)sender;
            
            try
            {

                ULPlanDetail rowView = (ULPlanDetail)e.Row;

                if (lstulp_dtl == null) return;


                if (string.IsNullOrEmpty(rowView.MC_NO))//
                {
                    e.Valid = false;
                    view.FocusedRowHandle = e.RowHandle;
                    view.FocusedColumn = view.Columns["MC_NO"];
                    return;
                }

                if (string.IsNullOrEmpty(rowView.PRODUCT_NO))
                {
                    e.Valid = false;
                    view.FocusedRowHandle = e.RowHandle;
                    view.FocusedColumn = view.Columns["PRODUCT_NO"];
                    return;
                }

                if (!rowView.MP_START.HasValue)
                {
                    e.Valid = false;
                    view.FocusedRowHandle = e.RowHandle;
                    view.FocusedColumn = view.Columns["MP_START"];
                    return;
                }
                else { //Check date

                    if (!rowView.MP_FINISH.HasValue)
                    {
                        e.Valid = false;
                        view.FocusedRowHandle = e.RowHandle;
                        view.FocusedColumn = view.Columns["MP_FINISH"];
                        return;
                    }
                    else
                    { //check date
                        if (rowView.MP_START > rowView.MP_FINISH)
                        {

                            e.Valid = false;
                            view.FocusedRowHandle = e.RowHandle;
                            view.FocusedColumn = view.Columns["MP_START"];
                            return;
                        }
                    }
                   
                }
               
                if ((rowView.QTY_PLAN <= 0))
                {
                    e.Valid = false;
                    view.FocusedRowHandle = e.RowHandle;
                    view.FocusedColumn = view.Columns["QTY_PLAN"];
                    return;
                }
                if (string.IsNullOrEmpty(rowView.PARTY_ID))
                {
                    e.Valid = false;
                    view.FocusedRowHandle = e.RowHandle;
                    view.FocusedColumn = view.Columns["PARTY_ID"];
                    return;
                }

                e.Valid = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "ValidateRow : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

      
        private bool IsRowFormValidated()
        {
            //Check control empty
            bool isValid = true;
            int rowpos = 0;
            string colName = string.Empty;

            try{
                if(PlanDTL_gv.RowCount <= 0){
                return true;
                }

                for (int i = 0; i < lstulp_dtl.Count; i++)
                {
                    if (lstulp_dtl[i].FLAG == 2 || lstulp_dtl[i].FLAG == 3)
                    {
                        if (string.IsNullOrEmpty(lstulp_dtl[i].MC_NO))//
                        {
                            isValid = false;
                            rowpos = i;
                            colName = "MC_NO";
                            break;
                        }
                        if (string.IsNullOrEmpty(lstulp_dtl[i].PRODUCT_NO))
                        {
                            isValid = false;
                            rowpos = i;
                            colName = "PRODUCT_NO";
                            break;
                        }
                        if (!lstulp_dtl[i].MP_START.HasValue)
                        {
                            isValid = false;
                            rowpos = i;
                            colName = "MP_START";
                            break;
                        }
                        else
                        { //Check date

                            if (!lstulp_dtl[i].MP_FINISH.HasValue)
                            {
                                isValid = false;
                                rowpos = i;
                                colName = "MP_FINISH";
                                break;

                            }
                            else
                            { //check date
                                if (lstulp_dtl[i].MP_START > lstulp_dtl[i].MP_FINISH)
                                {
                                    isValid = false;
                                    rowpos = i;
                                    colName = "MP_START";
                                    break;
                                }
                            }

                        }

                        if ((lstulp_dtl[i].QTY_PLAN <= 0))
                        {
                            isValid = false;
                            rowpos = i;
                            colName = "QTY_PLAN";
                            break;

                        }
                        if (string.IsNullOrEmpty(lstulp_dtl[i].PARTY_ID))
                        {
                            isValid = false;
                            rowpos = i;
                            colName = "PARTY_ID";
                            break;

                        }

                    }
                }

                  PlanDTL_gv.FocusedRowHandle = rowpos;
                  PlanDTL_gv.FocusedColumn = PlanDTL_gv.Columns[colName];

                
            
         
            }
            catch(Exception ex){
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return isValid;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
          //update HDR
            string resultMsg = string.Empty;
            string userId = ((frmMainMenu)this.ParentForm).UserID;
            DialogResult isChange;
            isChange = XtraMessageBox.Show(this, "Do you want to change status this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            try
            {
                if (isChange == DialogResult.Yes)
                {
                    string status = string.Empty;
                    if (btnStatus.Text == "Active")
                         status = "N";
                    else
                        status = "Y";
                    using (UploadPlanBLL ulpBll = new UploadPlanBLL())
                    {

                        resultMsg = ulpBll.UpdateStatusPlanHDR(txtPLAN_NO.Text, status, userId);

                        if (resultMsg.Equals("OK"))
                        {
          
                          //  this.xtra_Plan_List_TabPage.PageEnabled = false;

                         //   this.xtra_Plan_Details_TabPage.PageVisible = true;
                          //  this.xtraPlan_tab.SelectedTabPage = this.xtra_Plan_Details_TabPage;

                            //this.FormState = eFormState.ReadOnly;
                            this.GetBindingPlanDetails(txtPLAN_NO.Text);
                            this.FormState = eFormState.ReadOnly;
             
                            //this.btnCancel.Text = "&Cancel";
                      
                        }
                    }
                    //update
                }
            }
            catch(Exception ex){

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
       
            }


            //refreshpage
        }

        private void PlanDTL_gv_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            object planStat = view.GetRowCellValue(view.FocusedRowHandle, "PLAN_STAT");
            if (planStat.ToString() == "F")
            {
                if (view.FocusedColumn.FieldName == "MC_NO" || view.FocusedColumn.FieldName == "PRODUCT_NO" || view.FocusedColumn.FieldName == "MP_START" || view.FocusedColumn.FieldName == "MP_FINISH" || view.FocusedColumn.FieldName == "QTY_PLAN" || view.FocusedColumn.FieldName == "PARTY_ID" || view.FocusedColumn.FieldName == "PLAN_STAT")
                {
                    view.FocusedColumn.OptionsColumn.ReadOnly = true;
                    view.HideEditor();
                    e.Cancel = true;
                }
            }
            else {
                if (view.FocusedColumn.FieldName == "PLAN_STAT")
                {
                    view.FocusedColumn.OptionsColumn.ReadOnly = true;
                    view.HideEditor();
                    e.Cancel = true;
                }
                else
                {
                    
                    view.FocusedColumn.OptionsColumn.ReadOnly = false;
                }
            }
            if (view.FocusedColumn.FieldName == "JOB_NO")
            {
                object jobNo = view.GetRowCellValue(view.FocusedRowHandle, "JOB_NO");
                if (string.IsNullOrEmpty(jobNo.ToString()))
                {
                    view.FocusedColumn.OptionsColumn.ReadOnly = true;
                    view.HideEditor();
                    e.Cancel = true;
                }
                else {
                    if (formState == eFormState.Edit) {
                        view.FocusedColumn.OptionsColumn.ReadOnly = true;
                        view.HideEditor();
                        e.Cancel = true;
                    
                    }
                
                }

            }
            //if (formState == eFormState.ReadOnly) {
            //    if (view.FocusedColumn.FieldName == "JOB_NO")
            //    {
            //        view.FocusedColumn.OptionsColumn.ReadOnly = true;
            //        view.HideEditor();
            //        e.Cancel = true;
            //    }
            
            //}
          
        }

        private void PlanDTL_gv_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = ExceptionMode.NoAction;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "InvalidRowException " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ddbExport_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }



        private void rps_JOB_NO_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = this.PlanDTL_gc.MainView as GridView;
                string jobNo = (string)view.GetRowCellValue(view.FocusedRowHandle, "JOB_NO");

                //open the form first
                ((frmMainMenu)this.ParentForm).OpenJobOrderForm();

                //call method
                var principalForm = System.Windows.Forms.Application.OpenForms.OfType<frmJobOrder>().Single();
                principalForm.OpenJobNo(jobNo);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error rps_JOB_NO_ButtonClick", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
//FLAG = 0 delete// 1 = Not update // 2 = new // 3 = Update