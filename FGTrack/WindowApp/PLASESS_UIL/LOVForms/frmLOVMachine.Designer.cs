namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVMachine
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grdMachine = new DevExpress.XtraGrid.GridControl();
            this.grvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvMachine_col_MC_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvMachine_col_MACHINE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvMachine_col_MACHINE_SIZE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvMachine_col_MACHINE_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.btnSelect);
            this.panelControl2.Location = new System.Drawing.Point(3, 326);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(478, 33);
            this.panelControl2.TabIndex = 6;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonCancel;
            this.simpleButton2.Location = new System.Drawing.Point(398, 5);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "&Cancel";
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
            // grdMachine
            // 
            this.grdMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMachine.Location = new System.Drawing.Point(3, 37);
            this.grdMachine.MainView = this.grvMachine;
            this.grdMachine.Name = "grdMachine";
            this.grdMachine.Size = new System.Drawing.Size(478, 286);
            this.grdMachine.TabIndex = 5;
            this.grdMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMachine});
            // 
            // grvMachine
            // 
            this.grvMachine.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMachine.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMachine.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvMachine.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvMachine.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvMachine.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMachine.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMachine.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvMachine.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvMachine.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvMachine.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMachine.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMachine.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvMachine.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvMachine.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvMachine.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMachine.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMachine.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvMachine.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvMachine.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvMachine_col_MC_NO,
            this.grvMachine_col_MACHINE_NAME,
            this.grvMachine_col_MACHINE_SIZE,
            this.grvMachine_col_MACHINE_TYPE});
            this.grvMachine.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvMachine.GridControl = this.grdMachine;
            this.grvMachine.Name = "grvMachine";
            this.grvMachine.OptionsBehavior.Editable = false;
            this.grvMachine.OptionsView.ShowGroupPanel = false;
            this.grvMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvMachine_KeyDown);
            this.grvMachine.DoubleClick += new System.EventHandler(this.grvMachine_DoubleClick);
            // 
            // grvMachine_col_MC_NO
            // 
            this.grvMachine_col_MC_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvMachine_col_MC_NO.AppearanceCell.Options.UseFont = true;
            this.grvMachine_col_MC_NO.AppearanceCell.Options.UseTextOptions = true;
            this.grvMachine_col_MC_NO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMachine_col_MC_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvMachine_col_MC_NO.AppearanceHeader.Options.UseFont = true;
            this.grvMachine_col_MC_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvMachine_col_MC_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMachine_col_MC_NO.Caption = "Machine No.";
            this.grvMachine_col_MC_NO.FieldName = "MC_NO";
            this.grvMachine_col_MC_NO.Name = "grvMachine_col_MC_NO";
            this.grvMachine_col_MC_NO.Visible = true;
            this.grvMachine_col_MC_NO.VisibleIndex = 0;
            this.grvMachine_col_MC_NO.Width = 97;
            // 
            // grvMachine_col_MACHINE_NAME
            // 
            this.grvMachine_col_MACHINE_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvMachine_col_MACHINE_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvMachine_col_MACHINE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvMachine_col_MACHINE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMachine_col_MACHINE_NAME.Caption = "Machine Name";
            this.grvMachine_col_MACHINE_NAME.FieldName = "MACHINE_NAME";
            this.grvMachine_col_MACHINE_NAME.Name = "grvMachine_col_MACHINE_NAME";
            this.grvMachine_col_MACHINE_NAME.Visible = true;
            this.grvMachine_col_MACHINE_NAME.VisibleIndex = 1;
            this.grvMachine_col_MACHINE_NAME.Width = 237;
            // 
            // grvMachine_col_MACHINE_SIZE
            // 
            this.grvMachine_col_MACHINE_SIZE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvMachine_col_MACHINE_SIZE.AppearanceHeader.Options.UseFont = true;
            this.grvMachine_col_MACHINE_SIZE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvMachine_col_MACHINE_SIZE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMachine_col_MACHINE_SIZE.Caption = "Size";
            this.grvMachine_col_MACHINE_SIZE.FieldName = "MACHINE_SIZE";
            this.grvMachine_col_MACHINE_SIZE.Name = "grvMachine_col_MACHINE_SIZE";
            this.grvMachine_col_MACHINE_SIZE.Visible = true;
            this.grvMachine_col_MACHINE_SIZE.VisibleIndex = 2;
            this.grvMachine_col_MACHINE_SIZE.Width = 123;
            // 
            // grvMachine_col_MACHINE_TYPE
            // 
            this.grvMachine_col_MACHINE_TYPE.FieldName = "MACHINE_TYPE";
            this.grvMachine_col_MACHINE_TYPE.Name = "grvMachine_col_MACHINE_TYPE";
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
            this.panelControl1.TabIndex = 4;
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
            this.txtSearch.Size = new System.Drawing.Size(409, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmLOVMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdMachine);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVMachine";
            this.ShowInTaskbar = false;
            this.Text = "Machine";
            this.Load += new System.EventHandler(this.frmLOVMachine_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVMachine_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVMachine_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grdMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMachine;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.Columns.GridColumn grvMachine_col_MC_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvMachine_col_MACHINE_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvMachine_col_MACHINE_SIZE;
        private DevExpress.XtraGrid.Columns.GridColumn grvMachine_col_MACHINE_TYPE;
    }
}