namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    partial class frmPopUpload_SO
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdSO_DTL = new DevExpress.XtraGrid.GridControl();
            this.grvSO_DTL = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvSO_DTL_col_LINE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_PROD_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_PRODUCT_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_PRODUCT_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_NO_OF_BOX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_UNIT_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_FREE_STOCK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_PO_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_col_DTL_REMARK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSO_HDR = new DevExpress.XtraGrid.GridControl();
            this.grvSO_HDR = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvSO_HDR_col_WH_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_rps_WH = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.grvSO_HDR_col_PARTY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_col_PARTY_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_col_REF_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_col_REF_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_col_ETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_HDR_col_REMARK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblPOEmpty = new System.Windows.Forms.Label();
            this.lblDuplicate = new System.Windows.Forms.Label();
            this.lblMismatch = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grvSO_DTL_col_PACKAGING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvSO_DTL_rps_luePACKAGING = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSO_DTL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_DTL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSO_HDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_HDR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_HDR_rps_WH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_DTL_rps_luePACKAGING)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.grdSO_DTL);
            this.groupControl2.Controls.Add(this.grdSO_HDR);
            this.groupControl2.Location = new System.Drawing.Point(2, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(754, 493);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Shipping Order";
            // 
            // grdSO_DTL
            // 
            this.grdSO_DTL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSO_DTL.Location = new System.Drawing.Point(4, 144);
            this.grdSO_DTL.MainView = this.grvSO_DTL;
            this.grdSO_DTL.Name = "grdSO_DTL";
            this.grdSO_DTL.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grvSO_DTL_rps_luePACKAGING});
            this.grdSO_DTL.Size = new System.Drawing.Size(747, 345);
            this.grdSO_DTL.TabIndex = 1;
            this.grdSO_DTL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvSO_DTL});
            // 
            // grvSO_DTL
            // 
            this.grvSO_DTL.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvSO_DTL_col_LINE_NO,
            this.grvSO_DTL_col_PROD_SEQ_NO,
            this.grvSO_DTL_col_PRODUCT_NO,
            this.grvSO_DTL_col_PRODUCT_NAME,
            this.grvSO_DTL_col_NO_OF_BOX,
            this.grvSO_DTL_col_PACKAGING,
            this.grvSO_DTL_col_QTY,
            this.grvSO_DTL_col_UNIT_ID,
            this.grvSO_DTL_col_FREE_STOCK,
            this.grvSO_DTL_col_PO_NO,
            this.grvSO_DTL_col_DTL_REMARK});
            this.grvSO_DTL.GridControl = this.grdSO_DTL;
            this.grvSO_DTL.Name = "grvSO_DTL";
            this.grvSO_DTL.OptionsBehavior.Editable = false;
            this.grvSO_DTL.OptionsBehavior.ReadOnly = true;
            this.grvSO_DTL.OptionsCustomization.AllowColumnMoving = false;
            this.grvSO_DTL.OptionsCustomization.AllowGroup = false;
            this.grvSO_DTL.OptionsCustomization.AllowQuickHideColumns = false;
            this.grvSO_DTL.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvSO_DTL.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.grvSO_DTL.OptionsSelection.EnableAppearanceHideSelection = false;
            this.grvSO_DTL.OptionsSelection.UseIndicatorForSelection = false;
            this.grvSO_DTL.OptionsView.ShowGroupPanel = false;
            this.grvSO_DTL.OptionsView.ShowIndicator = false;
            this.grvSO_DTL.OptionsView.ShowViewCaption = true;
            this.grvSO_DTL.ViewCaption = "Shipping Order Detail";
            // 
            // grvSO_DTL_col_LINE_NO
            // 
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.ForeColor = System.Drawing.Color.DarkGray;
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.Options.UseForeColor = true;
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_DTL_col_LINE_NO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvSO_DTL_col_LINE_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_LINE_NO.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_LINE_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_LINE_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_LINE_NO.Caption = " #";
            this.grvSO_DTL_col_LINE_NO.FieldName = "LINE_NO";
            this.grvSO_DTL_col_LINE_NO.Name = "grvSO_DTL_col_LINE_NO";
            this.grvSO_DTL_col_LINE_NO.Visible = true;
            this.grvSO_DTL_col_LINE_NO.VisibleIndex = 0;
            this.grvSO_DTL_col_LINE_NO.Width = 28;
            // 
            // grvSO_DTL_col_PROD_SEQ_NO
            // 
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_PROD_SEQ_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_PROD_SEQ_NO.Caption = "PROD_SEQ_NO";
            this.grvSO_DTL_col_PROD_SEQ_NO.FieldName = "PROD_SEQ_NO";
            this.grvSO_DTL_col_PROD_SEQ_NO.Name = "grvSO_DTL_col_PROD_SEQ_NO";
            // 
            // grvSO_DTL_col_PRODUCT_NO
            // 
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_PRODUCT_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_PRODUCT_NO.Caption = "Product #";
            this.grvSO_DTL_col_PRODUCT_NO.FieldName = "PRODUCT_NO";
            this.grvSO_DTL_col_PRODUCT_NO.Name = "grvSO_DTL_col_PRODUCT_NO";
            this.grvSO_DTL_col_PRODUCT_NO.Visible = true;
            this.grvSO_DTL_col_PRODUCT_NO.VisibleIndex = 1;
            this.grvSO_DTL_col_PRODUCT_NO.Width = 119;
            // 
            // grvSO_DTL_col_PRODUCT_NAME
            // 
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_PRODUCT_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_PRODUCT_NAME.Caption = "Product Name";
            this.grvSO_DTL_col_PRODUCT_NAME.FieldName = "PRODUCT_NAME";
            this.grvSO_DTL_col_PRODUCT_NAME.Name = "grvSO_DTL_col_PRODUCT_NAME";
            this.grvSO_DTL_col_PRODUCT_NAME.Visible = true;
            this.grvSO_DTL_col_PRODUCT_NAME.VisibleIndex = 2;
            this.grvSO_DTL_col_PRODUCT_NAME.Width = 191;
            // 
            // grvSO_DTL_col_NO_OF_BOX
            // 
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.ForeColor = System.Drawing.Color.DarkGray;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.Options.UseForeColor = true;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_NO_OF_BOX.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_NO_OF_BOX.Caption = "No.Of Box";
            this.grvSO_DTL_col_NO_OF_BOX.DisplayFormat.FormatString = "{0:#,##0}";
            this.grvSO_DTL_col_NO_OF_BOX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvSO_DTL_col_NO_OF_BOX.FieldName = "NO_OF_BOX";
            this.grvSO_DTL_col_NO_OF_BOX.Name = "grvSO_DTL_col_NO_OF_BOX";
            this.grvSO_DTL_col_NO_OF_BOX.Visible = true;
            this.grvSO_DTL_col_NO_OF_BOX.VisibleIndex = 3;
            this.grvSO_DTL_col_NO_OF_BOX.Width = 60;
            // 
            // grvSO_DTL_col_QTY
            // 
            this.grvSO_DTL_col_QTY.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_QTY.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_QTY.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_DTL_col_QTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvSO_DTL_col_QTY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_QTY.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_QTY.Caption = "Qty";
            this.grvSO_DTL_col_QTY.DisplayFormat.FormatString = "{0:#,##0}";
            this.grvSO_DTL_col_QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvSO_DTL_col_QTY.FieldName = "QTY";
            this.grvSO_DTL_col_QTY.Name = "grvSO_DTL_col_QTY";
            this.grvSO_DTL_col_QTY.Visible = true;
            this.grvSO_DTL_col_QTY.VisibleIndex = 5;
            this.grvSO_DTL_col_QTY.Width = 60;
            // 
            // grvSO_DTL_col_UNIT_ID
            // 
            this.grvSO_DTL_col_UNIT_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_DTL_col_UNIT_ID.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_UNIT_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_DTL_col_UNIT_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_UNIT_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_UNIT_ID.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_UNIT_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_UNIT_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_UNIT_ID.Caption = "Unit";
            this.grvSO_DTL_col_UNIT_ID.FieldName = "UNIT_ID";
            this.grvSO_DTL_col_UNIT_ID.Name = "grvSO_DTL_col_UNIT_ID";
            this.grvSO_DTL_col_UNIT_ID.Visible = true;
            this.grvSO_DTL_col_UNIT_ID.VisibleIndex = 6;
            this.grvSO_DTL_col_UNIT_ID.Width = 55;
            // 
            // grvSO_DTL_col_FREE_STOCK
            // 
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.ForeColor = System.Drawing.Color.Gray;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.Options.UseBackColor = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.Options.UseFont = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.Options.UseForeColor = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_FREE_STOCK.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_FREE_STOCK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_FREE_STOCK.Caption = "Free Qty";
            this.grvSO_DTL_col_FREE_STOCK.DisplayFormat.FormatString = "{0:#,##0}";
            this.grvSO_DTL_col_FREE_STOCK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvSO_DTL_col_FREE_STOCK.FieldName = "FREE_STOCK";
            this.grvSO_DTL_col_FREE_STOCK.Name = "grvSO_DTL_col_FREE_STOCK";
            this.grvSO_DTL_col_FREE_STOCK.Visible = true;
            this.grvSO_DTL_col_FREE_STOCK.VisibleIndex = 7;
            this.grvSO_DTL_col_FREE_STOCK.Width = 60;
            // 
            // grvSO_DTL_col_PO_NO
            // 
            this.grvSO_DTL_col_PO_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PO_NO.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_PO_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_PO_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_PO_NO.Caption = "P/O REF. #";
            this.grvSO_DTL_col_PO_NO.FieldName = "PO_NO";
            this.grvSO_DTL_col_PO_NO.Name = "grvSO_DTL_col_PO_NO";
            this.grvSO_DTL_col_PO_NO.Visible = true;
            this.grvSO_DTL_col_PO_NO.VisibleIndex = 8;
            this.grvSO_DTL_col_PO_NO.Width = 100;
            // 
            // grvSO_DTL_col_DTL_REMARK
            // 
            this.grvSO_DTL_col_DTL_REMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_DTL_REMARK.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_DTL_REMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_DTL_REMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_DTL_REMARK.Caption = "Remark";
            this.grvSO_DTL_col_DTL_REMARK.FieldName = "REMARK";
            this.grvSO_DTL_col_DTL_REMARK.Name = "grvSO_DTL_col_DTL_REMARK";
            this.grvSO_DTL_col_DTL_REMARK.Visible = true;
            this.grvSO_DTL_col_DTL_REMARK.VisibleIndex = 9;
            this.grvSO_DTL_col_DTL_REMARK.Width = 156;
            // 
            // grdSO_HDR
            // 
            this.grdSO_HDR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSO_HDR.Location = new System.Drawing.Point(4, 23);
            this.grdSO_HDR.MainView = this.grvSO_HDR;
            this.grdSO_HDR.Name = "grdSO_HDR";
            this.grdSO_HDR.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grvSO_HDR_rps_WH});
            this.grdSO_HDR.Size = new System.Drawing.Size(747, 118);
            this.grdSO_HDR.TabIndex = 0;
            this.grdSO_HDR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvSO_HDR});
            // 
            // grvSO_HDR
            // 
            this.grvSO_HDR.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvSO_HDR_col_WH_ID,
            this.grvSO_HDR_col_PARTY_ID,
            this.grvSO_HDR_col_PARTY_NAME,
            this.grvSO_HDR_col_REF_NO,
            this.grvSO_HDR_col_REF_DATE,
            this.grvSO_HDR_col_ETA,
            this.grvSO_HDR_col_REMARK});
            this.grvSO_HDR.GridControl = this.grdSO_HDR;
            this.grvSO_HDR.Name = "grvSO_HDR";
            this.grvSO_HDR.OptionsBehavior.Editable = false;
            this.grvSO_HDR.OptionsBehavior.ReadOnly = true;
            this.grvSO_HDR.OptionsCustomization.AllowColumnMoving = false;
            this.grvSO_HDR.OptionsCustomization.AllowFilter = false;
            this.grvSO_HDR.OptionsCustomization.AllowGroup = false;
            this.grvSO_HDR.OptionsCustomization.AllowQuickHideColumns = false;
            this.grvSO_HDR.OptionsCustomization.AllowSort = false;
            this.grvSO_HDR.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvSO_HDR.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.grvSO_HDR.OptionsSelection.EnableAppearanceHideSelection = false;
            this.grvSO_HDR.OptionsView.EnableAppearanceEvenRow = true;
            this.grvSO_HDR.OptionsView.ShowDetailButtons = false;
            this.grvSO_HDR.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvSO_HDR.OptionsView.ShowGroupPanel = false;
            this.grvSO_HDR.OptionsView.ShowIndicator = false;
            this.grvSO_HDR.OptionsView.ShowViewCaption = true;
            this.grvSO_HDR.ViewCaption = "Shipping Order Header";
            // 
            // grvSO_HDR_col_WH_ID
            // 
            this.grvSO_HDR_col_WH_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_WH_ID.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_WH_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_HDR_col_WH_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_WH_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_WH_ID.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_WH_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_WH_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_WH_ID.Caption = "W/H";
            this.grvSO_HDR_col_WH_ID.ColumnEdit = this.grvSO_HDR_rps_WH;
            this.grvSO_HDR_col_WH_ID.FieldName = "WH_ID";
            this.grvSO_HDR_col_WH_ID.Name = "grvSO_HDR_col_WH_ID";
            this.grvSO_HDR_col_WH_ID.Visible = true;
            this.grvSO_HDR_col_WH_ID.VisibleIndex = 0;
            this.grvSO_HDR_col_WH_ID.Width = 55;
            // 
            // grvSO_HDR_rps_WH
            // 
            this.grvSO_HDR_rps_WH.AutoHeight = false;
            this.grvSO_HDR_rps_WH.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grvSO_HDR_rps_WH.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SEQ_NO", "SEQ_NO")});
            this.grvSO_HDR_rps_WH.DisplayMember = "SEQ_NO";
            this.grvSO_HDR_rps_WH.Name = "grvSO_HDR_rps_WH";
            this.grvSO_HDR_rps_WH.NullText = "";
            this.grvSO_HDR_rps_WH.ValueMember = "SEQ_NO";
            // 
            // grvSO_HDR_col_PARTY_ID
            // 
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.Options.UseBackColor = true;
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_HDR_col_PARTY_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_PARTY_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_PARTY_ID.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_PARTY_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_PARTY_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_PARTY_ID.Caption = "Code";
            this.grvSO_HDR_col_PARTY_ID.FieldName = "PARTY_ID";
            this.grvSO_HDR_col_PARTY_ID.Name = "grvSO_HDR_col_PARTY_ID";
            this.grvSO_HDR_col_PARTY_ID.Visible = true;
            this.grvSO_HDR_col_PARTY_ID.VisibleIndex = 1;
            this.grvSO_HDR_col_PARTY_ID.Width = 60;
            // 
            // grvSO_HDR_col_PARTY_NAME
            // 
            this.grvSO_HDR_col_PARTY_NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_HDR_col_PARTY_NAME.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_PARTY_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_PARTY_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_PARTY_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_PARTY_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_PARTY_NAME.Caption = "Customer Name";
            this.grvSO_HDR_col_PARTY_NAME.FieldName = "PARTY_NAME";
            this.grvSO_HDR_col_PARTY_NAME.Name = "grvSO_HDR_col_PARTY_NAME";
            this.grvSO_HDR_col_PARTY_NAME.Visible = true;
            this.grvSO_HDR_col_PARTY_NAME.VisibleIndex = 2;
            this.grvSO_HDR_col_PARTY_NAME.Width = 200;
            // 
            // grvSO_HDR_col_REF_NO
            // 
            this.grvSO_HDR_col_REF_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_HDR_col_REF_NO.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_REF_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_REF_NO.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_REF_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_REF_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_REF_NO.Caption = "P/O REF. #";
            this.grvSO_HDR_col_REF_NO.FieldName = "REF_NO";
            this.grvSO_HDR_col_REF_NO.Name = "grvSO_HDR_col_REF_NO";
            this.grvSO_HDR_col_REF_NO.Visible = true;
            this.grvSO_HDR_col_REF_NO.VisibleIndex = 3;
            this.grvSO_HDR_col_REF_NO.Width = 102;
            // 
            // grvSO_HDR_col_REF_DATE
            // 
            this.grvSO_HDR_col_REF_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_HDR_col_REF_DATE.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_REF_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_HDR_col_REF_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_REF_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_REF_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_REF_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_REF_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_REF_DATE.Caption = "P/O DATE";
            this.grvSO_HDR_col_REF_DATE.DisplayFormat.FormatString = "{0:dd-MM-yyyy}";
            this.grvSO_HDR_col_REF_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.grvSO_HDR_col_REF_DATE.FieldName = "REF_DATE";
            this.grvSO_HDR_col_REF_DATE.Name = "grvSO_HDR_col_REF_DATE";
            this.grvSO_HDR_col_REF_DATE.Visible = true;
            this.grvSO_HDR_col_REF_DATE.VisibleIndex = 4;
            this.grvSO_HDR_col_REF_DATE.Width = 70;
            // 
            // grvSO_HDR_col_ETA
            // 
            this.grvSO_HDR_col_ETA.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grvSO_HDR_col_ETA.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_ETA.AppearanceCell.Options.UseBackColor = true;
            this.grvSO_HDR_col_ETA.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_ETA.AppearanceCell.Options.UseTextOptions = true;
            this.grvSO_HDR_col_ETA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_ETA.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_ETA.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_ETA.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_ETA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_ETA.Caption = "ETD";
            this.grvSO_HDR_col_ETA.DisplayFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.grvSO_HDR_col_ETA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.grvSO_HDR_col_ETA.FieldName = "ETA";
            this.grvSO_HDR_col_ETA.Name = "grvSO_HDR_col_ETA";
            this.grvSO_HDR_col_ETA.Visible = true;
            this.grvSO_HDR_col_ETA.VisibleIndex = 5;
            this.grvSO_HDR_col_ETA.Width = 110;
            // 
            // grvSO_HDR_col_REMARK
            // 
            this.grvSO_HDR_col_REMARK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.grvSO_HDR_col_REMARK.AppearanceCell.Options.UseFont = true;
            this.grvSO_HDR_col_REMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.grvSO_HDR_col_REMARK.AppearanceHeader.Options.UseFont = true;
            this.grvSO_HDR_col_REMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_HDR_col_REMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_HDR_col_REMARK.Caption = "Remark";
            this.grvSO_HDR_col_REMARK.FieldName = "REMARK";
            this.grvSO_HDR_col_REMARK.Name = "grvSO_HDR_col_REMARK";
            this.grvSO_HDR_col_REMARK.Visible = true;
            this.grvSO_HDR_col_REMARK.VisibleIndex = 6;
            this.grvSO_HDR_col_REMARK.Width = 142;
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.lblPOEmpty);
            this.panelControl2.Controls.Add(this.lblDuplicate);
            this.panelControl2.Controls.Add(this.lblMismatch);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Location = new System.Drawing.Point(2, 499);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(754, 30);
            this.panelControl2.TabIndex = 3;
            // 
            // lblPOEmpty
            // 
            this.lblPOEmpty.BackColor = System.Drawing.Color.Black;
            this.lblPOEmpty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPOEmpty.ForeColor = System.Drawing.Color.White;
            this.lblPOEmpty.Location = new System.Drawing.Point(165, 3);
            this.lblPOEmpty.Name = "lblPOEmpty";
            this.lblPOEmpty.Size = new System.Drawing.Size(77, 23);
            this.lblPOEmpty.TabIndex = 23;
            this.lblPOEmpty.Text = "PO# EMPTY";
            this.lblPOEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPOEmpty.Visible = false;
            // 
            // lblDuplicate
            // 
            this.lblDuplicate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblDuplicate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDuplicate.Location = new System.Drawing.Point(85, 3);
            this.lblDuplicate.Name = "lblDuplicate";
            this.lblDuplicate.Size = new System.Drawing.Size(77, 23);
            this.lblDuplicate.TabIndex = 22;
            this.lblDuplicate.Text = "DUPLICATE";
            this.lblDuplicate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDuplicate.Visible = false;
            // 
            // lblMismatch
            // 
            this.lblMismatch.BackColor = System.Drawing.Color.Red;
            this.lblMismatch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMismatch.Location = new System.Drawing.Point(4, 3);
            this.lblMismatch.Name = "lblMismatch";
            this.lblMismatch.Size = new System.Drawing.Size(77, 23);
            this.lblMismatch.TabIndex = 21;
            this.lblMismatch.Text = "MISMATCH";
            this.lblMismatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMismatch.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonCancel;
            this.btnCancel.Location = new System.Drawing.Point(674, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonSave;
            this.btnSave.Location = new System.Drawing.Point(593, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grvSO_DTL_col_PACKAGING
            // 
            this.grvSO_DTL_col_PACKAGING.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvSO_DTL_col_PACKAGING.AppearanceHeader.Options.UseFont = true;
            this.grvSO_DTL_col_PACKAGING.AppearanceHeader.Options.UseTextOptions = true;
            this.grvSO_DTL_col_PACKAGING.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvSO_DTL_col_PACKAGING.Caption = "Packaging";
            this.grvSO_DTL_col_PACKAGING.ColumnEdit = this.grvSO_DTL_rps_luePACKAGING;
            this.grvSO_DTL_col_PACKAGING.FieldName = "PACKAGING";
            this.grvSO_DTL_col_PACKAGING.Name = "grvSO_DTL_col_PACKAGING";
            this.grvSO_DTL_col_PACKAGING.Visible = true;
            this.grvSO_DTL_col_PACKAGING.VisibleIndex = 4;
            // 
            // grvSO_DTL_rps_luePACKAGING
            // 
            this.grvSO_DTL_rps_luePACKAGING.AutoHeight = false;
            this.grvSO_DTL_rps_luePACKAGING.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grvSO_DTL_rps_luePACKAGING.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PACKAGE_NAME")});
            this.grvSO_DTL_rps_luePACKAGING.DisplayMember = "PACKAGE_NAME";
            this.grvSO_DTL_rps_luePACKAGING.Name = "grvSO_DTL_rps_luePACKAGING";
            this.grvSO_DTL_rps_luePACKAGING.NullText = "";
            this.grvSO_DTL_rps_luePACKAGING.ShowFooter = false;
            this.grvSO_DTL_rps_luePACKAGING.ShowHeader = false;
            this.grvSO_DTL_rps_luePACKAGING.ShowLines = false;
            this.grvSO_DTL_rps_luePACKAGING.ShowPopupShadow = false;
            this.grvSO_DTL_rps_luePACKAGING.ValueMember = "PACKAGE_ID";
            // 
            // frmPopUpload_SO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 531);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.groupControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopUpload_SO";
            this.ShowInTaskbar = false;
            this.Text = "UPLOAD SHIPPING ORDER";
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmPopUpload_SO_LoadCompleted);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSO_DTL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_DTL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSO_HDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_HDR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_HDR_rps_WH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvSO_DTL_rps_luePACKAGING)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdSO_HDR;
        private DevExpress.XtraGrid.Views.Grid.GridView grvSO_HDR;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdSO_DTL;
        private DevExpress.XtraGrid.Views.Grid.GridView grvSO_DTL;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_WH_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_PARTY_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_REF_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_REF_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_ETA;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_PARTY_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_HDR_col_REMARK;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_PROD_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_LINE_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_PRODUCT_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_PRODUCT_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_NO_OF_BOX;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_UNIT_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_FREE_STOCK;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_DTL_REMARK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label lblMismatch;
        private System.Windows.Forms.Label lblDuplicate;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_PO_NO;
        private System.Windows.Forms.Label lblPOEmpty;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grvSO_HDR_rps_WH;
        private DevExpress.XtraGrid.Columns.GridColumn grvSO_DTL_col_PACKAGING;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grvSO_DTL_rps_luePACKAGING;
    }
}