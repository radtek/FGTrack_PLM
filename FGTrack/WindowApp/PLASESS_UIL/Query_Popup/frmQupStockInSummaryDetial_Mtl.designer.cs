namespace HTN.BITS.UIL.PLASESS.Query_Popup
{
    partial class frmQupStockINSummaryDetial_Mtl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQupStockINSummaryDetial_Mtl));
            this.grdQryDetail = new DevExpress.XtraGrid.GridControl();
            this.grvQryDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvQryDetail_col_OUT_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rps_txtDate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvQryDetail_col_CUSTOMER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rps_txtString = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvQryDetail_col_PRODUCT_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQryDetail_col_PRODUCT_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQryDetail_col_PRODUCT_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQryDetail_col_SERIAL_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQryDetail_col_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rps_txtNumber = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvQryDetail_col_PICK_BY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQryDetail_col_PICK_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rps_txtDateTime = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefreshDetail = new DevExpress.XtraEditors.SimpleButton();
            this.ddbExportDetail = new DevExpress.XtraEditors.DropDownButton();
            this.pumExport = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bbiExportXLS = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportXLSX = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportPDF = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportRTF = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportText = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportHTML = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.bbiGridView = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCardView = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBandView = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAdvView = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.dntProdTracDtl = new DevExpress.XtraEditors.DataNavigator();
            this.rps_txtDecimal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQryDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQryDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pumExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDecimal)).BeginInit();
            this.SuspendLayout();
            // 
            // grdQryDetail
            // 
            this.grdQryDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdQryDetail.Location = new System.Drawing.Point(3, 4);
            this.grdQryDetail.MainView = this.grvQryDetail;
            this.grdQryDetail.Name = "grdQryDetail";
            this.grdQryDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rps_txtString,
            this.rps_txtNumber,
            this.rps_txtDate,
            this.rps_txtDateTime,
            this.rps_txtDecimal});
            this.grdQryDetail.Size = new System.Drawing.Size(778, 519);
            this.grdQryDetail.TabIndex = 3;
            this.grdQryDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvQryDetail});
            // 
            // grvQryDetail
            // 
            this.grvQryDetail.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvQryDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvQryDetail.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvQryDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvQryDetail.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvQryDetail.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvQryDetail.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvQryDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvQryDetail.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvQryDetail.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvQryDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.grvQryDetail.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvQryDetail.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvQryDetail.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvQryDetail.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvQryDetail.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvQryDetail.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvQryDetail.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvQryDetail.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvQryDetail.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvQryDetail.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvQryDetail.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail.Appearance.ViewCaption.Options.UseFont = true;
            this.grvQryDetail.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.grvQryDetail.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.grvQryDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grvQryDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvQryDetail_col_OUT_DATE,
            this.grvQryDetail_col_CUSTOMER,
            this.grvQryDetail_col_PRODUCT_TYPE,
            this.grvQryDetail_col_PRODUCT_NO,
            this.grvQryDetail_col_PRODUCT_NAME,
            this.grvQryDetail_col_SERIAL_NO,
            this.grvQryDetail_col_QTY,
            this.grvQryDetail_col_PICK_BY,
            this.grvQryDetail_col_PICK_DATE});
            this.grvQryDetail.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvQryDetail.GridControl = this.grdQryDetail;
            this.grvQryDetail.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "SERIAL_NO", this.grvQryDetail_col_SERIAL_NO, "{0}. Record"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", this.grvQryDetail_col_QTY, "{0:#,##0}")});
            this.grvQryDetail.Name = "grvQryDetail";
            this.grvQryDetail.OptionsBehavior.ReadOnly = true;
            this.grvQryDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvQryDetail.OptionsView.ColumnAutoWidth = false;
            this.grvQryDetail.OptionsView.EnableAppearanceEvenRow = true;
            this.grvQryDetail.OptionsView.ShowDetailButtons = false;
            this.grvQryDetail.OptionsView.ShowFooter = true;
            this.grvQryDetail.OptionsView.ShowViewCaption = true;
            this.grvQryDetail.ViewCaption = "STOCK IN SUMMARY DETAIL ( MATERIAL )";
            this.grvQryDetail.ViewCaptionHeight = 35;
            this.grvQryDetail.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GridControl_CustomDrawRowIndicator);
            // 
            // grvQryDetail_col_OUT_DATE
            // 
            this.grvQryDetail_col_OUT_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_OUT_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_OUT_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_OUT_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_OUT_DATE.Caption = "DATE";
            this.grvQryDetail_col_OUT_DATE.ColumnEdit = this.rps_txtDate;
            this.grvQryDetail_col_OUT_DATE.FieldName = "IN_DATE";
            this.grvQryDetail_col_OUT_DATE.Name = "grvQryDetail_col_OUT_DATE";
            this.grvQryDetail_col_OUT_DATE.Visible = true;
            this.grvQryDetail_col_OUT_DATE.VisibleIndex = 0;
            // 
            // rps_txtDate
            // 
            this.rps_txtDate.AutoHeight = false;
            this.rps_txtDate.DisplayFormat.FormatString = "{0:dd-MM-yyyy}";
            this.rps_txtDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rps_txtDate.EditFormat.FormatString = "{0:dd-MM-yyyy}";
            this.rps_txtDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rps_txtDate.Name = "rps_txtDate";
            this.rps_txtDate.ReadOnly = true;
            // 
            // grvQryDetail_col_CUSTOMER
            // 
            this.grvQryDetail_col_CUSTOMER.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_CUSTOMER.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_CUSTOMER.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_CUSTOMER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_CUSTOMER.Caption = "CUSTOMER";
            this.grvQryDetail_col_CUSTOMER.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_CUSTOMER.FieldName = "CUSTOMER";
            this.grvQryDetail_col_CUSTOMER.Name = "grvQryDetail_col_CUSTOMER";
            this.grvQryDetail_col_CUSTOMER.Visible = true;
            this.grvQryDetail_col_CUSTOMER.VisibleIndex = 1;
            this.grvQryDetail_col_CUSTOMER.Width = 170;
            // 
            // rps_txtString
            // 
            this.rps_txtString.AutoHeight = false;
            this.rps_txtString.Name = "rps_txtString";
            this.rps_txtString.ReadOnly = true;
            // 
            // grvQryDetail_col_PRODUCT_TYPE
            // 
            this.grvQryDetail_col_PRODUCT_TYPE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PRODUCT_TYPE.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_PRODUCT_TYPE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_PRODUCT_TYPE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_PRODUCT_TYPE.Caption = "PRODUCT TYPE";
            this.grvQryDetail_col_PRODUCT_TYPE.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_PRODUCT_TYPE.FieldName = "PRODUCT_TYPE";
            this.grvQryDetail_col_PRODUCT_TYPE.Name = "grvQryDetail_col_PRODUCT_TYPE";
            this.grvQryDetail_col_PRODUCT_TYPE.Width = 100;
            // 
            // grvQryDetail_col_PRODUCT_NO
            // 
            this.grvQryDetail_col_PRODUCT_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PRODUCT_NO.AppearanceCell.Options.UseFont = true;
            this.grvQryDetail_col_PRODUCT_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PRODUCT_NO.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_PRODUCT_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_PRODUCT_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_PRODUCT_NO.Caption = "MATERIAL #";
            this.grvQryDetail_col_PRODUCT_NO.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_PRODUCT_NO.FieldName = "MTL_CODE";
            this.grvQryDetail_col_PRODUCT_NO.Name = "grvQryDetail_col_PRODUCT_NO";
            this.grvQryDetail_col_PRODUCT_NO.Visible = true;
            this.grvQryDetail_col_PRODUCT_NO.VisibleIndex = 2;
            this.grvQryDetail_col_PRODUCT_NO.Width = 103;
            // 
            // grvQryDetail_col_PRODUCT_NAME
            // 
            this.grvQryDetail_col_PRODUCT_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PRODUCT_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_PRODUCT_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_PRODUCT_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_PRODUCT_NAME.Caption = "MATERIAL NAME";
            this.grvQryDetail_col_PRODUCT_NAME.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_PRODUCT_NAME.FieldName = "MTL_NAME";
            this.grvQryDetail_col_PRODUCT_NAME.Name = "grvQryDetail_col_PRODUCT_NAME";
            this.grvQryDetail_col_PRODUCT_NAME.Visible = true;
            this.grvQryDetail_col_PRODUCT_NAME.VisibleIndex = 3;
            this.grvQryDetail_col_PRODUCT_NAME.Width = 169;
            // 
            // grvQryDetail_col_SERIAL_NO
            // 
            this.grvQryDetail_col_SERIAL_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_SERIAL_NO.AppearanceCell.Options.UseFont = true;
            this.grvQryDetail_col_SERIAL_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_SERIAL_NO.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_SERIAL_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_SERIAL_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_SERIAL_NO.Caption = "SERIAL #";
            this.grvQryDetail_col_SERIAL_NO.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_SERIAL_NO.FieldName = "SERIAL_NO";
            this.grvQryDetail_col_SERIAL_NO.Name = "grvQryDetail_col_SERIAL_NO";
            this.grvQryDetail_col_SERIAL_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "SERIAL_NO", "TOTAL :")});
            this.grvQryDetail_col_SERIAL_NO.Visible = true;
            this.grvQryDetail_col_SERIAL_NO.VisibleIndex = 4;
            // 
            // grvQryDetail_col_QTY
            // 
            this.grvQryDetail_col_QTY.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.grvQryDetail_col_QTY.AppearanceCell.BackColor2 = System.Drawing.Color.Gainsboro;
            this.grvQryDetail_col_QTY.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_QTY.AppearanceCell.Options.UseBackColor = true;
            this.grvQryDetail_col_QTY.AppearanceCell.Options.UseFont = true;
            this.grvQryDetail_col_QTY.AppearanceCell.Options.UseTextOptions = true;
            this.grvQryDetail_col_QTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvQryDetail_col_QTY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_QTY.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_QTY.Caption = "QTY";
            this.grvQryDetail_col_QTY.ColumnEdit = this.rps_txtDecimal;
            this.grvQryDetail_col_QTY.DisplayFormat.FormatString = "{0:#,##0.00}";
            this.grvQryDetail_col_QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvQryDetail_col_QTY.FieldName = "QTY";
            this.grvQryDetail_col_QTY.Name = "grvQryDetail_col_QTY";
            this.grvQryDetail_col_QTY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", "{0:#,##0.00}")});
            this.grvQryDetail_col_QTY.Visible = true;
            this.grvQryDetail_col_QTY.VisibleIndex = 5;
            // 
            // rps_txtNumber
            // 
            this.rps_txtNumber.AutoHeight = false;
            this.rps_txtNumber.DisplayFormat.FormatString = "{0:#,##0}";
            this.rps_txtNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rps_txtNumber.EditFormat.FormatString = "{0:#0}";
            this.rps_txtNumber.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rps_txtNumber.Mask.EditMask = "d";
            this.rps_txtNumber.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rps_txtNumber.Name = "rps_txtNumber";
            this.rps_txtNumber.ReadOnly = true;
            // 
            // grvQryDetail_col_PICK_BY
            // 
            this.grvQryDetail_col_PICK_BY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PICK_BY.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_PICK_BY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_PICK_BY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_PICK_BY.Caption = "IN BY";
            this.grvQryDetail_col_PICK_BY.ColumnEdit = this.rps_txtString;
            this.grvQryDetail_col_PICK_BY.FieldName = "IN_BY";
            this.grvQryDetail_col_PICK_BY.Name = "grvQryDetail_col_PICK_BY";
            this.grvQryDetail_col_PICK_BY.Visible = true;
            this.grvQryDetail_col_PICK_BY.VisibleIndex = 6;
            // 
            // grvQryDetail_col_PICK_DATE
            // 
            this.grvQryDetail_col_PICK_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvQryDetail_col_PICK_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvQryDetail_col_PICK_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvQryDetail_col_PICK_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQryDetail_col_PICK_DATE.Caption = "LOAD DATE";
            this.grvQryDetail_col_PICK_DATE.ColumnEdit = this.rps_txtDateTime;
            this.grvQryDetail_col_PICK_DATE.FieldName = "LOADING_DATE";
            this.grvQryDetail_col_PICK_DATE.Name = "grvQryDetail_col_PICK_DATE";
            // 
            // rps_txtDateTime
            // 
            this.rps_txtDateTime.AutoHeight = false;
            this.rps_txtDateTime.DisplayFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.rps_txtDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rps_txtDateTime.EditFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.rps_txtDateTime.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rps_txtDateTime.Name = "rps_txtDateTime";
            this.rps_txtDateTime.ReadOnly = true;
            // 
            // panelControl4
            // 
            this.panelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl4.Controls.Add(this.btnRefreshDetail);
            this.panelControl4.Controls.Add(this.ddbExportDetail);
            this.panelControl4.Controls.Add(this.btnExit);
            this.panelControl4.Controls.Add(this.dntProdTracDtl);
            this.panelControl4.Location = new System.Drawing.Point(3, 527);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(778, 32);
            this.panelControl4.TabIndex = 12;
            // 
            // btnRefreshDetail
            // 
            this.btnRefreshDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRefreshDetail.Appearance.Options.UseFont = true;
            this.btnRefreshDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshDetail.Location = new System.Drawing.Point(499, 5);
            this.btnRefreshDetail.Name = "btnRefreshDetail";
            this.btnRefreshDetail.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshDetail.TabIndex = 13;
            this.btnRefreshDetail.Text = "Refresh";
            this.btnRefreshDetail.Click += new System.EventHandler(this.btnRefreshDetail_Click);
            // 
            // ddbExportDetail
            // 
            this.ddbExportDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddbExportDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ddbExportDetail.Appearance.Options.UseFont = true;
            this.ddbExportDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ddbExportDetail.DropDownControl = this.pumExport;
            this.ddbExportDetail.Location = new System.Drawing.Point(580, 5);
            this.ddbExportDetail.Name = "ddbExportDetail";
            this.ddbExportDetail.Size = new System.Drawing.Size(109, 23);
            this.ddbExportDetail.TabIndex = 8;
            this.ddbExportDetail.Text = "Export";
            this.ddbExportDetail.Click += new System.EventHandler(this.ddbExport_Click);
            // 
            // pumExport
            // 
            this.pumExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportXLS),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportXLSX),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportPDF),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportRTF),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportText),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportHTML),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPrintPreview, true)});
            this.pumExport.Manager = this.barManager1;
            this.pumExport.Name = "pumExport";
            // 
            // bbiExportXLS
            // 
            this.bbiExportXLS.Caption = "Export to XLS";
            this.bbiExportXLS.Id = 9;
            this.bbiExportXLS.Name = "bbiExportXLS";
            this.bbiExportXLS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportXLS_ItemClick);
            // 
            // bbiExportXLSX
            // 
            this.bbiExportXLSX.Caption = "Export to XLSX";
            this.bbiExportXLSX.Id = 10;
            this.bbiExportXLSX.Name = "bbiExportXLSX";
            this.bbiExportXLSX.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportXLSX_ItemClick);
            // 
            // bbiExportPDF
            // 
            this.bbiExportPDF.Caption = "Export to PDF";
            this.bbiExportPDF.Id = 11;
            this.bbiExportPDF.Name = "bbiExportPDF";
            this.bbiExportPDF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportPDF_ItemClick);
            // 
            // bbiExportRTF
            // 
            this.bbiExportRTF.Caption = "Export to RTF";
            this.bbiExportRTF.Id = 12;
            this.bbiExportRTF.Name = "bbiExportRTF";
            this.bbiExportRTF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportRTF_ItemClick);
            // 
            // bbiExportText
            // 
            this.bbiExportText.Caption = "Export to TEXT";
            this.bbiExportText.Id = 13;
            this.bbiExportText.Name = "bbiExportText";
            this.bbiExportText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportText_ItemClick);
            // 
            // bbiExportHTML
            // 
            this.bbiExportHTML.Caption = "Export to HTML";
            this.bbiExportHTML.Id = 14;
            this.bbiExportHTML.Name = "bbiExportHTML";
            this.bbiExportHTML.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportHTML_ItemClick);
            // 
            // bbiPrintPreview
            // 
            this.bbiPrintPreview.Caption = "Print Preview";
            this.bbiPrintPreview.Id = 19;
            this.bbiPrintPreview.Name = "bbiPrintPreview";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiExportXLS,
            this.bbiExportXLSX,
            this.bbiExportPDF,
            this.bbiExportRTF,
            this.bbiExportText,
            this.bbiExportHTML,
            this.bbiPrintPreview,
            this.bbiGridView,
            this.bbiCardView,
            this.bbiBandView,
            this.bbiAdvView});
            this.barManager1.MaxItemId = 28;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 562);
            this.barDockControl2.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 562);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(784, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 562);
            // 
            // bbiGridView
            // 
            this.bbiGridView.Caption = "Grid View";
            this.bbiGridView.Id = 23;
            this.bbiGridView.Name = "bbiGridView";
            // 
            // bbiCardView
            // 
            this.bbiCardView.Caption = "Card View";
            this.bbiCardView.Id = 24;
            this.bbiCardView.Name = "bbiCardView";
            // 
            // bbiBandView
            // 
            this.bbiBandView.Caption = "Band View";
            this.bbiBandView.Id = 26;
            this.bbiBandView.Name = "bbiBandView";
            // 
            // bbiAdvView
            // 
            this.bbiAdvView.Caption = "Advance View";
            this.bbiAdvView.Id = 27;
            this.bbiAdvView.Name = "bbiAdvView";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(699, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dntProdTracDtl
            // 
            this.dntProdTracDtl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dntProdTracDtl.Appearance.Options.UseFont = true;
            this.dntProdTracDtl.Buttons.Append.Visible = false;
            this.dntProdTracDtl.Buttons.CancelEdit.Visible = false;
            this.dntProdTracDtl.Buttons.EndEdit.Visible = false;
            this.dntProdTracDtl.Buttons.Remove.Visible = false;
            this.dntProdTracDtl.Location = new System.Drawing.Point(5, 6);
            this.dntProdTracDtl.Name = "dntProdTracDtl";
            this.dntProdTracDtl.Size = new System.Drawing.Size(250, 20);
            this.dntProdTracDtl.TabIndex = 1;
            this.dntProdTracDtl.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            // 
            // rps_txtDecimal
            // 
            this.rps_txtDecimal.AutoHeight = false;
            this.rps_txtDecimal.Mask.EditMask = "f2";
            this.rps_txtDecimal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rps_txtDecimal.Name = "rps_txtDecimal";
            // 
            // frmQupStockINSummaryDetial_Mtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.grdQryDetail);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQupStockINSummaryDetial_Mtl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "NormalMode";
            this.Text = "STOCK IN SUMMARY DETAIL";
            this.Load += new System.EventHandler(this.frmQupStockOutSummaryDetial_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmQupStockOutSummaryDetial_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQupStockOutSummaryDetial_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmQupStockOutSummaryDetial_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grdQryDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQryDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pumExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtDecimal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdQryDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView grvQryDetail;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.DataNavigator dntProdTracDtl;
        private DevExpress.XtraEditors.DropDownButton ddbExportDetail;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem bbiExportXLS;
        private DevExpress.XtraBars.BarButtonItem bbiExportXLSX;
        private DevExpress.XtraBars.BarButtonItem bbiExportPDF;
        private DevExpress.XtraBars.BarButtonItem bbiExportRTF;
        private DevExpress.XtraBars.BarButtonItem bbiExportText;
        private DevExpress.XtraBars.BarButtonItem bbiExportHTML;
        private DevExpress.XtraBars.BarButtonItem bbiPrintPreview;
        private DevExpress.XtraBars.BarButtonItem bbiGridView;
        private DevExpress.XtraBars.BarButtonItem bbiCardView;
        private DevExpress.XtraBars.BarButtonItem bbiBandView;
        private DevExpress.XtraBars.BarButtonItem bbiAdvView;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.PopupMenu pumExport;
        private DevExpress.XtraEditors.SimpleButton btnRefreshDetail;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_OUT_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_CUSTOMER;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_PRODUCT_TYPE;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_PRODUCT_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_PRODUCT_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_SERIAL_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_PICK_BY;
        private DevExpress.XtraGrid.Columns.GridColumn grvQryDetail_col_PICK_DATE;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtString;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtDateTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtDecimal;
    }
}