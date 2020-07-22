namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLOVRole));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grdRole = new DevExpress.XtraGrid.GridControl();
            this.grvRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvRole_col_ROLE_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRole_col_ROLE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.btnSelect);
            this.groupControl1.Location = new System.Drawing.Point(3, 363);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(481, 36);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(402, 7);
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
            this.btnSelect.Location = new System.Drawing.Point(322, 7);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "&Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grdRole
            // 
            this.grdRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRole.Location = new System.Drawing.Point(2, 4);
            this.grdRole.MainView = this.grvRole;
            this.grdRole.Name = "grdRole";
            this.grdRole.Size = new System.Drawing.Size(482, 355);
            this.grdRole.TabIndex = 2;
            this.grdRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRole});
            // 
            // grvRole
            // 
            this.grvRole.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRole.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRole.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvRole.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvRole.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvRole.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRole.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRole.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvRole.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvRole.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvRole.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRole.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRole.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvRole.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvRole.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvRole.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRole.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRole.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvRole.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvRole.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvRole_col_ROLE_ID,
            this.grvRole_col_ROLE_NAME});
            this.grvRole.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvRole.GridControl = this.grdRole;
            this.grvRole.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvRole.Name = "grvRole";
            this.grvRole.OptionsBehavior.Editable = false;
            this.grvRole.OptionsBehavior.ReadOnly = true;
            this.grvRole.OptionsMenu.EnableColumnMenu = false;
            this.grvRole.OptionsMenu.EnableFooterMenu = false;
            this.grvRole.OptionsMenu.EnableGroupPanelMenu = false;
            this.grvRole.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grvRole.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grvRole.OptionsView.EnableAppearanceEvenRow = true;
            this.grvRole.OptionsView.ShowGroupPanel = false;
            this.grvRole.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.grvRole.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvRole_KeyDown);
            this.grvRole.DoubleClick += new System.EventHandler(this.grvRole_DoubleClick);
            // 
            // grvRole_col_ROLE_ID
            // 
            this.grvRole_col_ROLE_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvRole_col_ROLE_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRole_col_ROLE_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRole_col_ROLE_ID.AppearanceHeader.Options.UseFont = true;
            this.grvRole_col_ROLE_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRole_col_ROLE_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRole_col_ROLE_ID.Caption = "Role ID";
            this.grvRole_col_ROLE_ID.FieldName = "ROLE_ID";
            this.grvRole_col_ROLE_ID.Name = "grvRole_col_ROLE_ID";
            this.grvRole_col_ROLE_ID.OptionsColumn.FixedWidth = true;
            this.grvRole_col_ROLE_ID.Visible = true;
            this.grvRole_col_ROLE_ID.VisibleIndex = 0;
            this.grvRole_col_ROLE_ID.Width = 100;
            // 
            // grvRole_col_ROLE_NAME
            // 
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRole_col_ROLE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRole_col_ROLE_NAME.Caption = "Role Name";
            this.grvRole_col_ROLE_NAME.FieldName = "ROLE_NAME";
            this.grvRole_col_ROLE_NAME.Name = "grvRole_col_ROLE_NAME";
            this.grvRole_col_ROLE_NAME.Visible = true;
            this.grvRole_col_ROLE_NAME.VisibleIndex = 1;
            this.grvRole_col_ROLE_NAME.Width = 338;
            // 
            // frmLOVRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 402);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grdRole);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVRole";
            this.ShowInTaskbar = false;
            this.Text = "Role List";
            this.Load += new System.EventHandler(this.frmLOVRole_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVRole_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVRole_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grdRole;
        private DevExpress.XtraGrid.Views.Grid.GridView grvRole;
        private DevExpress.XtraGrid.Columns.GridColumn grvRole_col_ROLE_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvRole_col_ROLE_NAME;
    }
}