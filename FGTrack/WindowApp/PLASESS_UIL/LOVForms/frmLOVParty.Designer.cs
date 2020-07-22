namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVParty
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
            this.grdParty = new DevExpress.XtraGrid.GridControl();
            this.grvParty = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvParty_col_PARTY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvParty_col_PARTY_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvParty)).BeginInit();
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
            this.panelControl2.Size = new System.Drawing.Size(478, 33);
            this.panelControl2.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonCancel;
            this.btnCancel.Location = new System.Drawing.Point(398, 5);
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
            this.btnSelect.Location = new System.Drawing.Point(318, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "&Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grdParty
            // 
            this.grdParty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdParty.Location = new System.Drawing.Point(3, 37);
            this.grdParty.MainView = this.grvParty;
            this.grdParty.Name = "grdParty";
            this.grdParty.Size = new System.Drawing.Size(478, 286);
            this.grdParty.TabIndex = 4;
            this.grdParty.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvParty});
            // 
            // grvParty
            // 
            this.grvParty.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvParty.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvParty.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvParty.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvParty.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvParty.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvParty.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvParty.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvParty.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvParty.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvParty.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvParty.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvParty.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvParty.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvParty.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvParty.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvParty.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvParty.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvParty.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvParty.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvParty.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvParty_col_PARTY_ID,
            this.grvParty_col_PARTY_NAME});
            this.grvParty.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvParty.GridControl = this.grdParty;
            this.grvParty.Name = "grvParty";
            this.grvParty.OptionsBehavior.Editable = false;
            this.grvParty.OptionsView.ShowGroupPanel = false;
            this.grvParty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvParty_KeyDown);
            this.grvParty.DoubleClick += new System.EventHandler(this.grvParty_DoubleClick);
            // 
            // grvParty_col_PARTY_ID
            // 
            this.grvParty_col_PARTY_ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvParty_col_PARTY_ID.AppearanceCell.Options.UseFont = true;
            this.grvParty_col_PARTY_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvParty_col_PARTY_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvParty_col_PARTY_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvParty_col_PARTY_ID.AppearanceHeader.Options.UseFont = true;
            this.grvParty_col_PARTY_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvParty_col_PARTY_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvParty_col_PARTY_ID.Caption = "Code";
            this.grvParty_col_PARTY_ID.FieldName = "PARTY_ID";
            this.grvParty_col_PARTY_ID.Name = "grvParty_col_PARTY_ID";
            this.grvParty_col_PARTY_ID.Visible = true;
            this.grvParty_col_PARTY_ID.VisibleIndex = 0;
            this.grvParty_col_PARTY_ID.Width = 97;
            // 
            // grvParty_col_PARTY_NAME
            // 
            this.grvParty_col_PARTY_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvParty_col_PARTY_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvParty_col_PARTY_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvParty_col_PARTY_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvParty_col_PARTY_NAME.Caption = "Name";
            this.grvParty_col_PARTY_NAME.FieldName = "PARTY_NAME";
            this.grvParty_col_PARTY_NAME.Name = "grvParty_col_PARTY_NAME";
            this.grvParty_col_PARTY_NAME.Visible = true;
            this.grvParty_col_PARTY_NAME.VisibleIndex = 1;
            this.grvParty_col_PARTY_NAME.Width = 360;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Location = new System.Drawing.Point(3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(478, 30);
            this.panelControl1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
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
            this.txtSearch.Size = new System.Drawing.Size(409, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmLOVParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdParty);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVParty";
            this.ShowInTaskbar = false;
            this.Text = "Party";
            this.Load += new System.EventHandler(this.frmLOVParty_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVParty_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVParty_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvParty)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdParty;
        private DevExpress.XtraGrid.Views.Grid.GridView grvParty;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.Columns.GridColumn grvParty_col_PARTY_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvParty_col_PARTY_NAME;


    }
}