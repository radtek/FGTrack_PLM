namespace HTN.BITS.UIL.PLASESS.Query_Popup
{
    partial class frmQupPickingInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQupPickingInfo));
            this.grdQryDetail = new DevExpress.XtraGrid.GridControl();
            this.grvPickingInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvPickingInfo_col_PICK_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rps_txtString = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvPickingInfo_col_SERIAL_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_PICKING_BY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_PICKING_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_UNIT_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_PALLET_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_LOADING_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_LOADING_BY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPickingInfo_col_LOADING_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnRollBack = new DevExpress.XtraEditors.SimpleButton();
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
            this.dntPickingDtl = new DevExpress.XtraEditors.DataNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.grdQryDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPickingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pumExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdQryDetail
            // 
            this.grdQryDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdQryDetail.Location = new System.Drawing.Point(3, 4);
            this.grdQryDetail.MainView = this.grvPickingInfo;
            this.grdQryDetail.Name = "grdQryDetail";
            this.grdQryDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rps_txtString});
            this.grdQryDetail.Size = new System.Drawing.Size(778, 519);
            this.grdQryDetail.TabIndex = 3;
            this.grdQryDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPickingInfo});
            // 
            // grvPickingInfo
            // 
            this.grvPickingInfo.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPickingInfo.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPickingInfo.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvPickingInfo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvPickingInfo.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvPickingInfo.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPickingInfo.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPickingInfo.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvPickingInfo.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvPickingInfo.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvPickingInfo.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo.Appearance.FooterPanel.Options.UseFont = true;
            this.grvPickingInfo.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPickingInfo.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPickingInfo.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvPickingInfo.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvPickingInfo.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvPickingInfo.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPickingInfo.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPickingInfo.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvPickingInfo.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvPickingInfo.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvPickingInfo.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo.Appearance.ViewCaption.Options.UseFont = true;
            this.grvPickingInfo.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.grvPickingInfo.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.grvPickingInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvPickingInfo_col_PICK_NO,
            this.grvPickingInfo_col_SERIAL_NO,
            this.grvPickingInfo_col_PICKING_BY,
            this.grvPickingInfo_col_PICKING_DATE,
            this.grvPickingInfo_col_QTY,
            this.grvPickingInfo_col_UNIT_ID,
            this.grvPickingInfo_col_PALLET_NO,
            this.grvPickingInfo_col_LOADING_NO,
            this.grvPickingInfo_col_LOADING_BY,
            this.grvPickingInfo_col_LOADING_DATE});
            this.grvPickingInfo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvPickingInfo.GridControl = this.grdQryDetail;
            this.grvPickingInfo.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "PICK_NO", this.grvPickingInfo_col_PICK_NO, "TOTAL LABELS :"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "SERIAL_NO", this.grvPickingInfo_col_SERIAL_NO, "{0:#,##0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "PICKING_DATE", this.grvPickingInfo_col_PICKING_DATE, "TOTAL QTY :"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", this.grvPickingInfo_col_QTY, "{0:#,##0}")});
            this.grvPickingInfo.Name = "grvPickingInfo";
            this.grvPickingInfo.OptionsBehavior.ReadOnly = true;
            this.grvPickingInfo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvPickingInfo.OptionsView.EnableAppearanceEvenRow = true;
            this.grvPickingInfo.OptionsView.ShowDetailButtons = false;
            this.grvPickingInfo.OptionsView.ShowFooter = true;
            this.grvPickingInfo.ViewCaptionHeight = 35;
            this.grvPickingInfo.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GridControl_CustomDrawRowIndicator);
            // 
            // grvPickingInfo_col_PICK_NO
            // 
            this.grvPickingInfo_col_PICK_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_PICK_NO.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_PICK_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_PICK_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_PICK_NO.Caption = "PICK NO";
            this.grvPickingInfo_col_PICK_NO.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_PICK_NO.FieldName = "PICK_NO";
            this.grvPickingInfo_col_PICK_NO.Name = "grvPickingInfo_col_PICK_NO";
            this.grvPickingInfo_col_PICK_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "PICK_NO", "TOTAL LABELS :")});
            this.grvPickingInfo_col_PICK_NO.Visible = true;
            this.grvPickingInfo_col_PICK_NO.VisibleIndex = 0;
            this.grvPickingInfo_col_PICK_NO.Width = 84;
            // 
            // rps_txtString
            // 
            this.rps_txtString.AutoHeight = false;
            this.rps_txtString.Name = "rps_txtString";
            this.rps_txtString.ReadOnly = true;
            // 
            // grvPickingInfo_col_SERIAL_NO
            // 
            this.grvPickingInfo_col_SERIAL_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_SERIAL_NO.AppearanceCell.Options.UseFont = true;
            this.grvPickingInfo_col_SERIAL_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_SERIAL_NO.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_SERIAL_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_SERIAL_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_SERIAL_NO.Caption = "SERIAL NO";
            this.grvPickingInfo_col_SERIAL_NO.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_SERIAL_NO.FieldName = "SERIAL_NO";
            this.grvPickingInfo_col_SERIAL_NO.Name = "grvPickingInfo_col_SERIAL_NO";
            this.grvPickingInfo_col_SERIAL_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SERIAL_NO", "{0:#,##0}")});
            this.grvPickingInfo_col_SERIAL_NO.Visible = true;
            this.grvPickingInfo_col_SERIAL_NO.VisibleIndex = 1;
            this.grvPickingInfo_col_SERIAL_NO.Width = 84;
            // 
            // grvPickingInfo_col_PICKING_BY
            // 
            this.grvPickingInfo_col_PICKING_BY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_PICKING_BY.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_PICKING_BY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_PICKING_BY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_PICKING_BY.Caption = "PICKED BY";
            this.grvPickingInfo_col_PICKING_BY.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_PICKING_BY.FieldName = "PICKING_BY";
            this.grvPickingInfo_col_PICKING_BY.Name = "grvPickingInfo_col_PICKING_BY";
            this.grvPickingInfo_col_PICKING_BY.Visible = true;
            this.grvPickingInfo_col_PICKING_BY.VisibleIndex = 2;
            this.grvPickingInfo_col_PICKING_BY.Width = 84;
            // 
            // grvPickingInfo_col_PICKING_DATE
            // 
            this.grvPickingInfo_col_PICKING_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.grvPickingInfo_col_PICKING_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvPickingInfo_col_PICKING_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_PICKING_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_PICKING_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_PICKING_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_PICKING_DATE.Caption = "PICKED DATE";
            this.grvPickingInfo_col_PICKING_DATE.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_PICKING_DATE.DisplayFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.grvPickingInfo_col_PICKING_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.grvPickingInfo_col_PICKING_DATE.FieldName = "PICKING_DATE";
            this.grvPickingInfo_col_PICKING_DATE.Name = "grvPickingInfo_col_PICKING_DATE";
            this.grvPickingInfo_col_PICKING_DATE.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "PICKING_DATE", "TOTAL QTY :")});
            this.grvPickingInfo_col_PICKING_DATE.Visible = true;
            this.grvPickingInfo_col_PICKING_DATE.VisibleIndex = 3;
            this.grvPickingInfo_col_PICKING_DATE.Width = 93;
            // 
            // grvPickingInfo_col_QTY
            // 
            this.grvPickingInfo_col_QTY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_QTY.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_QTY.Caption = "QTY";
            this.grvPickingInfo_col_QTY.DisplayFormat.FormatString = "{0:#,##0}";
            this.grvPickingInfo_col_QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvPickingInfo_col_QTY.FieldName = "QTY";
            this.grvPickingInfo_col_QTY.Name = "grvPickingInfo_col_QTY";
            this.grvPickingInfo_col_QTY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", "{0:#,##0}")});
            this.grvPickingInfo_col_QTY.Visible = true;
            this.grvPickingInfo_col_QTY.VisibleIndex = 4;
            this.grvPickingInfo_col_QTY.Width = 60;
            // 
            // grvPickingInfo_col_UNIT_ID
            // 
            this.grvPickingInfo_col_UNIT_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_UNIT_ID.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_UNIT_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_UNIT_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_UNIT_ID.Caption = "UNIT";
            this.grvPickingInfo_col_UNIT_ID.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_UNIT_ID.FieldName = "UNIT_ID";
            this.grvPickingInfo_col_UNIT_ID.Name = "grvPickingInfo_col_UNIT_ID";
            this.grvPickingInfo_col_UNIT_ID.Visible = true;
            this.grvPickingInfo_col_UNIT_ID.VisibleIndex = 5;
            this.grvPickingInfo_col_UNIT_ID.Width = 57;
            // 
            // grvPickingInfo_col_PALLET_NO
            // 
            this.grvPickingInfo_col_PALLET_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_PALLET_NO.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_PALLET_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_PALLET_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_PALLET_NO.Caption = "PALLET NO";
            this.grvPickingInfo_col_PALLET_NO.FieldName = "PALLET_NO";
            this.grvPickingInfo_col_PALLET_NO.Name = "grvPickingInfo_col_PALLET_NO";
            this.grvPickingInfo_col_PALLET_NO.Visible = true;
            this.grvPickingInfo_col_PALLET_NO.VisibleIndex = 6;
            // 
            // grvPickingInfo_col_LOADING_NO
            // 
            this.grvPickingInfo_col_LOADING_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_LOADING_NO.AppearanceCell.Options.UseFont = true;
            this.grvPickingInfo_col_LOADING_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_LOADING_NO.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_LOADING_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_LOADING_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_LOADING_NO.Caption = "LOADING NO";
            this.grvPickingInfo_col_LOADING_NO.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_LOADING_NO.FieldName = "LOADING_NO";
            this.grvPickingInfo_col_LOADING_NO.Name = "grvPickingInfo_col_LOADING_NO";
            this.grvPickingInfo_col_LOADING_NO.Visible = true;
            this.grvPickingInfo_col_LOADING_NO.VisibleIndex = 7;
            this.grvPickingInfo_col_LOADING_NO.Width = 96;
            // 
            // grvPickingInfo_col_LOADING_BY
            // 
            this.grvPickingInfo_col_LOADING_BY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_LOADING_BY.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_LOADING_BY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_LOADING_BY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_LOADING_BY.Caption = "LOADED BY";
            this.grvPickingInfo_col_LOADING_BY.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_LOADING_BY.FieldName = "LOADING_BY";
            this.grvPickingInfo_col_LOADING_BY.Name = "grvPickingInfo_col_LOADING_BY";
            this.grvPickingInfo_col_LOADING_BY.Visible = true;
            this.grvPickingInfo_col_LOADING_BY.VisibleIndex = 8;
            this.grvPickingInfo_col_LOADING_BY.Width = 96;
            // 
            // grvPickingInfo_col_LOADING_DATE
            // 
            this.grvPickingInfo_col_LOADING_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.grvPickingInfo_col_LOADING_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvPickingInfo_col_LOADING_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPickingInfo_col_LOADING_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvPickingInfo_col_LOADING_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPickingInfo_col_LOADING_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPickingInfo_col_LOADING_DATE.Caption = "LOADED DATE";
            this.grvPickingInfo_col_LOADING_DATE.ColumnEdit = this.rps_txtString;
            this.grvPickingInfo_col_LOADING_DATE.DisplayFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.grvPickingInfo_col_LOADING_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.grvPickingInfo_col_LOADING_DATE.FieldName = "LOADING_DATE";
            this.grvPickingInfo_col_LOADING_DATE.Name = "grvPickingInfo_col_LOADING_DATE";
            this.grvPickingInfo_col_LOADING_DATE.Visible = true;
            this.grvPickingInfo_col_LOADING_DATE.VisibleIndex = 9;
            this.grvPickingInfo_col_LOADING_DATE.Width = 103;
            // 
            // panelControl4
            // 
            this.panelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl4.Controls.Add(this.btnRollBack);
            this.panelControl4.Controls.Add(this.btnRefreshDetail);
            this.panelControl4.Controls.Add(this.ddbExportDetail);
            this.panelControl4.Controls.Add(this.btnExit);
            this.panelControl4.Controls.Add(this.dntPickingDtl);
            this.panelControl4.Location = new System.Drawing.Point(3, 527);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(778, 32);
            this.panelControl4.TabIndex = 12;
            // 
            // btnRollBack
            // 
            this.btnRollBack.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRollBack.Appearance.Options.UseFont = true;
            this.btnRollBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRollBack.Location = new System.Drawing.Point(260, 5);
            this.btnRollBack.Name = "btnRollBack";
            this.btnRollBack.Size = new System.Drawing.Size(136, 23);
            this.btnRollBack.TabIndex = 14;
            this.btnRollBack.Text = "Roll Back";
            this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
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
            // dntPickingDtl
            // 
            this.dntPickingDtl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dntPickingDtl.Appearance.Options.UseFont = true;
            this.dntPickingDtl.Buttons.Append.Visible = false;
            this.dntPickingDtl.Buttons.CancelEdit.Visible = false;
            this.dntPickingDtl.Buttons.EndEdit.Visible = false;
            this.dntPickingDtl.Buttons.Remove.Visible = false;
            this.dntPickingDtl.Location = new System.Drawing.Point(5, 6);
            this.dntPickingDtl.Name = "dntPickingDtl";
            this.dntPickingDtl.Size = new System.Drawing.Size(250, 20);
            this.dntPickingDtl.TabIndex = 1;
            this.dntPickingDtl.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            // 
            // frmQupPickingInfo
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
            this.Name = "frmQupPickingInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "NormalMode";
            this.Text = "PICKING INFO";
            this.Load += new System.EventHandler(this.frmQupPickingInfo_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmQupPickingInfo_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQupPickingInfo_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmQupPickingInfo_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grdQryDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPickingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps_txtString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pumExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdQryDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPickingInfo;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.DataNavigator dntPickingDtl;
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
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_PICK_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_SERIAL_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_PICKING_BY;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_PICKING_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_UNIT_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_LOADING_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_LOADING_BY;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_LOADING_DATE;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rps_txtString;
        private DevExpress.XtraEditors.SimpleButton btnRollBack;
        private DevExpress.XtraGrid.Columns.GridColumn grvPickingInfo_col_PALLET_NO;
    }
}