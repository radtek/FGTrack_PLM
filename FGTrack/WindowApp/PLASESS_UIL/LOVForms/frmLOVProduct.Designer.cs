namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVProduct
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grdProduct = new DevExpress.XtraGrid.GridControl();
            this.grvProduct = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvProduct_col_PRODUCT_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_PRODUCT_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_FREE_STOCK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_PROD_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MATERIAL_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnSelect);
            this.panelControl2.Location = new System.Drawing.Point(3, 326);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(559, 33);
            this.panelControl2.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonCancel;
            this.btnCancel.Location = new System.Drawing.Point(479, 5);
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
            this.btnSelect.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonSelect;
            this.btnSelect.Location = new System.Drawing.Point(399, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "&Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grdProduct
            // 
            this.grdProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProduct.Location = new System.Drawing.Point(3, 37);
            this.grdProduct.MainView = this.grvProduct;
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.Size = new System.Drawing.Size(559, 286);
            this.grdProduct.TabIndex = 7;
            this.grdProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProduct});
            // 
            // grvProduct
            // 
            this.grvProduct.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProduct.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProduct.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvProduct.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvProduct.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvProduct.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProduct.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProduct.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvProduct.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvProduct.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvProduct.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProduct.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProduct.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvProduct.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvProduct.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvProduct.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvProduct.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvProduct.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvProduct.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvProduct.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvProduct.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvProduct_col_PRODUCT_NO,
            this.grvProduct_col_PRODUCT_NAME,
            this.grvProduct_col_FREE_STOCK,
            this.grvProduct_col_PROD_SEQ_NO,
            this.grvProduct_col_MATERIAL_NAME});
            this.grvProduct.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvProduct.GridControl = this.grdProduct;
            this.grvProduct.Name = "grvProduct";
            this.grvProduct.OptionsBehavior.Editable = false;
            this.grvProduct.OptionsView.ShowGroupPanel = false;
            this.grvProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvProduct_KeyDown);
            this.grvProduct.DoubleClick += new System.EventHandler(this.grvProduct_DoubleClick);
            // 
            // grvProduct_col_PRODUCT_NO
            // 
            this.grvProduct_col_PRODUCT_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_PRODUCT_NO.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_PRODUCT_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_PRODUCT_NO.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_PRODUCT_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_PRODUCT_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_PRODUCT_NO.Caption = "Product No.";
            this.grvProduct_col_PRODUCT_NO.FieldName = "PRODUCT_NO";
            this.grvProduct_col_PRODUCT_NO.Name = "grvProduct_col_PRODUCT_NO";
            this.grvProduct_col_PRODUCT_NO.Visible = true;
            this.grvProduct_col_PRODUCT_NO.VisibleIndex = 0;
            this.grvProduct_col_PRODUCT_NO.Width = 133;
            // 
            // grvProduct_col_PRODUCT_NAME
            // 
            this.grvProduct_col_PRODUCT_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_PRODUCT_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_PRODUCT_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_PRODUCT_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_PRODUCT_NAME.Caption = "Product Name";
            this.grvProduct_col_PRODUCT_NAME.FieldName = "PRODUCT_NAME";
            this.grvProduct_col_PRODUCT_NAME.Name = "grvProduct_col_PRODUCT_NAME";
            this.grvProduct_col_PRODUCT_NAME.Visible = true;
            this.grvProduct_col_PRODUCT_NAME.VisibleIndex = 1;
            this.grvProduct_col_PRODUCT_NAME.Width = 324;
            // 
            // grvProduct_col_FREE_STOCK
            // 
            this.grvProduct_col_FREE_STOCK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_FREE_STOCK.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_FREE_STOCK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_FREE_STOCK.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_FREE_STOCK.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_FREE_STOCK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_FREE_STOCK.Caption = "Free";
            this.grvProduct_col_FREE_STOCK.FieldName = "FREE_STOCK";
            this.grvProduct_col_FREE_STOCK.Name = "grvProduct_col_FREE_STOCK";
            this.grvProduct_col_FREE_STOCK.Visible = true;
            this.grvProduct_col_FREE_STOCK.VisibleIndex = 2;
            // 
            // grvProduct_col_PROD_SEQ_NO
            // 
            this.grvProduct_col_PROD_SEQ_NO.FieldName = "PROD_SEQ_NO";
            this.grvProduct_col_PROD_SEQ_NO.Name = "grvProduct_col_PROD_SEQ_NO";
            // 
            // grvProduct_col_MATERIAL_NAME
            // 
            this.grvProduct_col_MATERIAL_NAME.FieldName = "MATERIAL_NAME";
            this.grvProduct_col_MATERIAL_NAME.Name = "grvProduct_col_MATERIAL_NAME";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Location = new System.Drawing.Point(3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(559, 30);
            this.panelControl1.TabIndex = 6;
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
            this.txtSearch.Size = new System.Drawing.Size(490, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmLOVProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 362);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdProduct);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVProduct";
            this.ShowInTaskbar = false;
            this.Text = "Products List";
            this.Load += new System.EventHandler(this.frmLOVProduct_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVProduct_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVProduct_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grdProduct;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProduct;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_PRODUCT_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_PRODUCT_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_PROD_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MATERIAL_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_FREE_STOCK;
    }
}