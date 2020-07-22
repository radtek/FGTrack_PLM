namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVPartyProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLOVPartyProduct));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grdProductList = new DevExpress.XtraGrid.GridControl();
            this.grvProductList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvProductList_col_PROD_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductList_col_PRODUCT_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductList_col_PRODUCT_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.btnSelect);
            this.groupControl1.Location = new System.Drawing.Point(3, 423);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(513, 33);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(434, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Appearance.Options.UseFont = true;
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(354, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "&Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grdProductList
            // 
            this.grdProductList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductList.Location = new System.Drawing.Point(3, 36);
            this.grdProductList.MainView = this.grvProductList;
            this.grdProductList.Name = "grdProductList";
            this.grdProductList.Size = new System.Drawing.Size(513, 384);
            this.grdProductList.TabIndex = 2;
            this.grdProductList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProductList});
            // 
            // grvProductList
            // 
            this.grvProductList.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProductList.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProductList.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvProductList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvProductList.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvProductList.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProductList.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProductList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvProductList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvProductList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvProductList.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProductList.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProductList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvProductList.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvProductList.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvProductList.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProductList.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProductList.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvProductList.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvProductList.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvProductList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvProductList_col_PROD_SEQ_NO,
            this.grvProductList_col_PRODUCT_NO,
            this.grvProductList_col_PRODUCT_NAME});
            this.grvProductList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvProductList.GridControl = this.grdProductList;
            this.grvProductList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvProductList.Name = "grvProductList";
            this.grvProductList.OptionsBehavior.Editable = false;
            this.grvProductList.OptionsBehavior.ReadOnly = true;
            this.grvProductList.OptionsMenu.EnableColumnMenu = false;
            this.grvProductList.OptionsMenu.EnableFooterMenu = false;
            this.grvProductList.OptionsMenu.EnableGroupPanelMenu = false;
            this.grvProductList.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grvProductList.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grvProductList.OptionsView.EnableAppearanceEvenRow = true;
            this.grvProductList.OptionsView.ShowGroupPanel = false;
            this.grvProductList.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            // 
            // grvProductList_col_PROD_SEQ_NO
            // 
            this.grvProductList_col_PROD_SEQ_NO.FieldName = "PROD_SEQ_NO";
            this.grvProductList_col_PROD_SEQ_NO.Name = "grvProductList_col_PROD_SEQ_NO";
            // 
            // grvProductList_col_PRODUCT_NO
            // 
            this.grvProductList_col_PRODUCT_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductList_col_PRODUCT_NO.AppearanceCell.Options.UseFont = true;
            this.grvProductList_col_PRODUCT_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductList_col_PRODUCT_NO.AppearanceHeader.Options.UseFont = true;
            this.grvProductList_col_PRODUCT_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductList_col_PRODUCT_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductList_col_PRODUCT_NO.Caption = "PRODUCT #";
            this.grvProductList_col_PRODUCT_NO.FieldName = "PRODUCT_NO";
            this.grvProductList_col_PRODUCT_NO.Name = "grvProductList_col_PRODUCT_NO";
            this.grvProductList_col_PRODUCT_NO.Visible = true;
            this.grvProductList_col_PRODUCT_NO.VisibleIndex = 0;
            // 
            // grvProductList_col_PRODUCT_NAME
            // 
            this.grvProductList_col_PRODUCT_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductList_col_PRODUCT_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvProductList_col_PRODUCT_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductList_col_PRODUCT_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductList_col_PRODUCT_NAME.Caption = "PRODUCT NAME";
            this.grvProductList_col_PRODUCT_NAME.FieldName = "PRODUCT_NAME";
            this.grvProductList_col_PRODUCT_NAME.Name = "grvProductList_col_PRODUCT_NAME";
            this.grvProductList_col_PRODUCT_NAME.Visible = true;
            this.grvProductList_col_PRODUCT_NAME.VisibleIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(513, 30);
            this.panelControl1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(10, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Search :";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(64, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(444, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmLOVPartyProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 459);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grdProductList);
            this.LookAndFeel.SkinName = "Caramel";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVPartyProduct";
            this.ShowInTaskbar = false;
            this.Text = "Product List";
            this.Load += new System.EventHandler(this.frmLOVPartyProduct_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVPartyProduct_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVPartyProduct_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grdProductList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProductList;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductList_col_PROD_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductList_col_PRODUCT_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductList_col_PRODUCT_NAME;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
    }
}