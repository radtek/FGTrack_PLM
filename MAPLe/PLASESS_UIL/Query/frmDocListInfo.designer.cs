namespace HTN.BITS.UIL.PLASESS.Query
{
    partial class frmDocListInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocListInfo));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.grdDocument = new DevExpress.XtraGrid.GridControl();
            this.grvDocument = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvDocument_col_DOC_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_DOC_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_PARTY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_LINE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_ITEM_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_QUANTITY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_UNIT_PRICE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvDocument_col_PENDING_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Location = new System.Drawing.Point(3, 585);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1091, 33);
            this.panelControl2.TabIndex = 8;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1012, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdDocument
            // 
            this.grdDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocument.Location = new System.Drawing.Point(3, 3);
            this.grdDocument.MainView = this.grvDocument;
            this.grdDocument.Name = "grdDocument";
            this.grdDocument.Size = new System.Drawing.Size(1090, 579);
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
            this.grvDocument_col_DOC_DATE,
            this.grvDocument_col_PARTY_ID,
            this.grvDocument_col_LINE_NO,
            this.grvDocument_col_ITEM_ID,
            this.grvDocument_col_DESCRIPTION,
            this.grvDocument_col_QUANTITY,
            this.grvDocument_col_UNIT_PRICE,
            this.grvDocument_col_PENDING_QTY});
            this.grvDocument.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvDocument.GridControl = this.grdDocument;
            this.grvDocument.Name = "grvDocument";
            this.grvDocument.OptionsBehavior.Editable = false;
            this.grvDocument.OptionsView.ShowGroupPanel = false;
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
            this.grvDocument_col_DOC_NO.Width = 150;
            // 
            // grvDocument_col_DOC_DATE
            // 
            this.grvDocument_col_DOC_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_DOC_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_DOC_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_DOC_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_DOC_DATE.Caption = "Document Date";
            this.grvDocument_col_DOC_DATE.FieldName = "DOC_DATE";
            this.grvDocument_col_DOC_DATE.Name = "grvDocument_col_DOC_DATE";
            this.grvDocument_col_DOC_DATE.Visible = true;
            this.grvDocument_col_DOC_DATE.VisibleIndex = 1;
            this.grvDocument_col_DOC_DATE.Width = 100;
            // 
            // grvDocument_col_PARTY_ID
            // 
            this.grvDocument_col_PARTY_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_PARTY_ID.AppearanceCell.Options.UseFont = true;
            this.grvDocument_col_PARTY_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_PARTY_ID.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_PARTY_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_PARTY_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_PARTY_ID.Caption = "Party ID";
            this.grvDocument_col_PARTY_ID.FieldName = "PARTY_ID";
            this.grvDocument_col_PARTY_ID.Name = "grvDocument_col_PARTY_ID";
            this.grvDocument_col_PARTY_ID.Visible = true;
            this.grvDocument_col_PARTY_ID.VisibleIndex = 2;
            this.grvDocument_col_PARTY_ID.Width = 110;
            // 
            // grvDocument_col_LINE_NO
            // 
            this.grvDocument_col_LINE_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_LINE_NO.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_LINE_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_LINE_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_LINE_NO.Caption = "Row #";
            this.grvDocument_col_LINE_NO.FieldName = "LINE_NO";
            this.grvDocument_col_LINE_NO.Name = "grvDocument_col_LINE_NO";
            this.grvDocument_col_LINE_NO.Visible = true;
            this.grvDocument_col_LINE_NO.VisibleIndex = 3;
            this.grvDocument_col_LINE_NO.Width = 55;
            // 
            // grvDocument_col_ITEM_ID
            // 
            this.grvDocument_col_ITEM_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_ITEM_ID.AppearanceCell.Options.UseFont = true;
            this.grvDocument_col_ITEM_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_ITEM_ID.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_ITEM_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_ITEM_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_ITEM_ID.Caption = "Item ID";
            this.grvDocument_col_ITEM_ID.FieldName = "ITEM_ID";
            this.grvDocument_col_ITEM_ID.Name = "grvDocument_col_ITEM_ID";
            this.grvDocument_col_ITEM_ID.Visible = true;
            this.grvDocument_col_ITEM_ID.VisibleIndex = 4;
            this.grvDocument_col_ITEM_ID.Width = 130;
            // 
            // grvDocument_col_DESCRIPTION
            // 
            this.grvDocument_col_DESCRIPTION.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_DESCRIPTION.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_DESCRIPTION.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_DESCRIPTION.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_DESCRIPTION.Caption = "Description";
            this.grvDocument_col_DESCRIPTION.FieldName = "DESCRIPTION";
            this.grvDocument_col_DESCRIPTION.Name = "grvDocument_col_DESCRIPTION";
            this.grvDocument_col_DESCRIPTION.Visible = true;
            this.grvDocument_col_DESCRIPTION.VisibleIndex = 5;
            this.grvDocument_col_DESCRIPTION.Width = 230;
            // 
            // grvDocument_col_QUANTITY
            // 
            this.grvDocument_col_QUANTITY.AppearanceCell.Options.UseTextOptions = true;
            this.grvDocument_col_QUANTITY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvDocument_col_QUANTITY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_QUANTITY.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_QUANTITY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_QUANTITY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_QUANTITY.Caption = "Quantity";
            this.grvDocument_col_QUANTITY.FieldName = "QUANTITY";
            this.grvDocument_col_QUANTITY.Name = "grvDocument_col_QUANTITY";
            this.grvDocument_col_QUANTITY.Visible = true;
            this.grvDocument_col_QUANTITY.VisibleIndex = 6;
            this.grvDocument_col_QUANTITY.Width = 85;
            // 
            // grvDocument_col_UNIT_PRICE
            // 
            this.grvDocument_col_UNIT_PRICE.AppearanceCell.Options.UseTextOptions = true;
            this.grvDocument_col_UNIT_PRICE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvDocument_col_UNIT_PRICE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_UNIT_PRICE.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_UNIT_PRICE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvDocument_col_UNIT_PRICE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument_col_UNIT_PRICE.Caption = "Price";
            this.grvDocument_col_UNIT_PRICE.FieldName = "UNIT_PRICE";
            this.grvDocument_col_UNIT_PRICE.Name = "grvDocument_col_UNIT_PRICE";
            this.grvDocument_col_UNIT_PRICE.Visible = true;
            this.grvDocument_col_UNIT_PRICE.VisibleIndex = 7;
            this.grvDocument_col_UNIT_PRICE.Width = 85;
            // 
            // grvDocument_col_PENDING_QTY
            // 
            this.grvDocument_col_PENDING_QTY.AppearanceCell.Options.UseTextOptions = true;
            this.grvDocument_col_PENDING_QTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grvDocument_col_PENDING_QTY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDocument_col_PENDING_QTY.AppearanceHeader.Options.UseFont = true;
            this.grvDocument_col_PENDING_QTY.Caption = "Pending Qty";
            this.grvDocument_col_PENDING_QTY.FieldName = "PENDING_QTY";
            this.grvDocument_col_PENDING_QTY.Name = "grvDocument_col_PENDING_QTY";
            this.grvDocument_col_PENDING_QTY.Visible = true;
            this.grvDocument_col_PENDING_QTY.VisibleIndex = 8;
            // 
            // frmDocListInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 621);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdDocument);
            this.LookAndFeel.SkinName = "Caramel";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDocListInfo";
            this.ShowInTaskbar = false;
            this.Text = "Document List";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdDocument;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDocument;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_DOC_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_DOC_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_PARTY_ID;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_LINE_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_ITEM_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_DESCRIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_QUANTITY;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_UNIT_PRICE;
        private DevExpress.XtraGrid.Columns.GridColumn grvDocument_col_PENDING_QTY;
    }
}