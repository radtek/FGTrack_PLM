namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVDocumentOrder
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
            this.grdDocument = new DevExpress.XtraGrid.GridControl();
            this.grvDocument = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvDocument_col_DOC_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_DOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_Empty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_PROD_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MATERIAL_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).BeginInit();
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
            this.panelControl2.Location = new System.Drawing.Point(3, 585);
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
            // grdDocument
            // 
            this.grdDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocument.Location = new System.Drawing.Point(3, 37);
            this.grdDocument.MainView = this.grvDocument;
            this.grdDocument.Name = "grdDocument";
            this.grdDocument.Size = new System.Drawing.Size(559, 545);
            this.grdDocument.TabIndex = 7;
            this.grdDocument.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDocument});
            // 
            // grvDocument
            // 
            this.grvDocument.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvDocument.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvDocument.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvDocument.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvDocument.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvDocument.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvDocument.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvDocument.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvDocument.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvDocument.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvDocument.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvDocument.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvDocument.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvDocument.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvDocument.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvDocument.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvDocument.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvDocument.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvDocument.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvDocument.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvDocument.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvDocument_col_DOC_NO,
            this.grvDocument_col_DOC,
            this.grvDocument_col_Empty,
            this.grvProduct_col_PROD_SEQ_NO,
            this.grvProduct_col_MATERIAL_NAME});
            this.grvDocument.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvDocument.GridControl = this.grdDocument;
            this.grvDocument.Name = "grvDocument";
            this.grvDocument.OptionsBehavior.Editable = false;
            this.grvDocument.OptionsView.ShowGroupPanel = false;
            this.grvDocument.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvProduct_KeyDown);
            this.grvDocument.DoubleClick += new System.EventHandler(this.grvProduct_DoubleClick);
            // 
            // grvDocument_col_DOC_NO
            // 
            this.grvDocument_col_DOC_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_DOC_NO.AppearanceCell.Options.UseFont = true;
            this.grvDocument_col_DOC_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_DOC_NO.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_DOC_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_DOC_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_DOC_NO.Caption = "Document No.";
            this.grvDocument_col_DOC_NO.FieldName = "DOC_NO";
            this.grvDocument_col_DOC_NO.Name = "grvDocument_col_DOC_NO";
            this.grvDocument_col_DOC_NO.Visible = true;
            this.grvDocument_col_DOC_NO.VisibleIndex = 0;
            this.grvDocument_col_DOC_NO.Width = 250;
            // 
            // grvDocument_col_DOC
            // 
            this.grvDocument_col_DOC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_DOC.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_DOC.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_DOC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_DOC.Caption = "Document Date";
            this.grvDocument_col_DOC.FieldName = "DOC_DATE";
            this.grvDocument_col_DOC.Name = "grvDocument_col_DOC";
            this.grvDocument_col_DOC.Visible = true;
            this.grvDocument_col_DOC.VisibleIndex = 1;
            this.grvDocument_col_DOC.Width = 232;
            // 
            // grvDocument_col_Empty
            // 
            this.grvDocument_col_Empty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_Empty.AppearanceCell.Options.UseFont = true;
            this.grvDocument_col_Empty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_Empty.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_Empty.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_Empty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_Empty.Name = "grvDocument_col_Empty";
            this.grvDocument_col_Empty.Visible = true;
            this.grvDocument_col_Empty.VisibleIndex = 2;
            this.grvDocument_col_Empty.Width = 56;
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
            // frmLOVDocumentOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 621);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdDocument);
            this.Controls.Add(this.panelControl1);
            this.LookAndFeel.SkinName = "Caramel";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVDocumentOrder";
            this.ShowInTaskbar = false;
            this.Text = "Document List";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdDocument;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDocument;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_DOC_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_DOC;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_PROD_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MATERIAL_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_Empty;
    }
}