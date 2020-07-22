namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    partial class frmLOVMtl
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
            this.grdMaterial = new DevExpress.XtraGrid.GridControl();
            this.grvMaterial = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvProduct_col_MTL_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MTL_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MTL_GRADE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MTL_COLOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_UNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_STD_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProduct_col_MTL_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaterial)).BeginInit();
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
            // grdMaterial
            // 
            this.grdMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMaterial.Location = new System.Drawing.Point(3, 37);
            this.grdMaterial.MainView = this.grvMaterial;
            this.grdMaterial.Name = "grdMaterial";
            this.grdMaterial.Size = new System.Drawing.Size(559, 286);
            this.grdMaterial.TabIndex = 7;
            this.grdMaterial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaterial});
            // 
            // grvMaterial
            // 
            this.grvMaterial.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMaterial.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMaterial.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvMaterial.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvMaterial.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvMaterial.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMaterial.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMaterial.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvMaterial.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvMaterial.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvMaterial.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMaterial.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMaterial.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvMaterial.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvMaterial.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvMaterial.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvMaterial.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvMaterial.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvMaterial.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvMaterial.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvMaterial.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvProduct_col_MTL_CODE,
            this.grvProduct_col_MTL_NAME,
            this.grvProduct_col_MTL_GRADE,
            this.grvProduct_col_MTL_COLOR,
            this.grvProduct_col_UNIT,
            this.grvProduct_col_STD_QTY,
            this.grvProduct_col_MTL_SEQ_NO});
            this.grvMaterial.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvMaterial.GridControl = this.grdMaterial;
            this.grvMaterial.Name = "grvMaterial";
            this.grvMaterial.OptionsBehavior.Editable = false;
            this.grvMaterial.OptionsView.ShowGroupPanel = false;
            this.grvMaterial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvProduct_KeyDown);
            this.grvMaterial.DoubleClick += new System.EventHandler(this.grvProduct_DoubleClick);
            // 
            // grvProduct_col_MTL_CODE
            // 
            this.grvProduct_col_MTL_CODE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_CODE.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_MTL_CODE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_CODE.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_MTL_CODE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_MTL_CODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_MTL_CODE.Caption = "MAT. Code";
            this.grvProduct_col_MTL_CODE.FieldName = "MTL_CODE";
            this.grvProduct_col_MTL_CODE.Name = "grvProduct_col_MTL_CODE";
            this.grvProduct_col_MTL_CODE.Visible = true;
            this.grvProduct_col_MTL_CODE.VisibleIndex = 0;
            this.grvProduct_col_MTL_CODE.Width = 110;
            // 
            // grvProduct_col_MTL_NAME
            // 
            this.grvProduct_col_MTL_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_MTL_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_MTL_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_MTL_NAME.Caption = "MAT. Name";
            this.grvProduct_col_MTL_NAME.FieldName = "MTL_NAME";
            this.grvProduct_col_MTL_NAME.Name = "grvProduct_col_MTL_NAME";
            this.grvProduct_col_MTL_NAME.Visible = true;
            this.grvProduct_col_MTL_NAME.VisibleIndex = 1;
            this.grvProduct_col_MTL_NAME.Width = 180;
            // 
            // grvProduct_col_MTL_GRADE
            // 
            this.grvProduct_col_MTL_GRADE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_GRADE.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_MTL_GRADE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_GRADE.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_MTL_GRADE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_MTL_GRADE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_MTL_GRADE.Caption = "Grade";
            this.grvProduct_col_MTL_GRADE.FieldName = "MTL_GRADE";
            this.grvProduct_col_MTL_GRADE.Name = "grvProduct_col_MTL_GRADE";
            this.grvProduct_col_MTL_GRADE.Visible = true;
            this.grvProduct_col_MTL_GRADE.VisibleIndex = 2;
            this.grvProduct_col_MTL_GRADE.Width = 102;
            // 
            // grvProduct_col_MTL_COLOR
            // 
            this.grvProduct_col_MTL_COLOR.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_COLOR.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_MTL_COLOR.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_MTL_COLOR.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_MTL_COLOR.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_MTL_COLOR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_MTL_COLOR.Caption = "Color";
            this.grvProduct_col_MTL_COLOR.FieldName = "MTL_COLOR";
            this.grvProduct_col_MTL_COLOR.Name = "grvProduct_col_MTL_COLOR";
            this.grvProduct_col_MTL_COLOR.Visible = true;
            this.grvProduct_col_MTL_COLOR.VisibleIndex = 3;
            this.grvProduct_col_MTL_COLOR.Width = 85;
            // 
            // grvProduct_col_UNIT
            // 
            this.grvProduct_col_UNIT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_UNIT.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_UNIT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_UNIT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_UNIT.Caption = "Unit";
            this.grvProduct_col_UNIT.FieldName = "UNIT";
            this.grvProduct_col_UNIT.Name = "grvProduct_col_UNIT";
            this.grvProduct_col_UNIT.Visible = true;
            this.grvProduct_col_UNIT.VisibleIndex = 4;
            this.grvProduct_col_UNIT.Width = 64;
            // 
            // grvProduct_col_STD_QTY
            // 
            this.grvProduct_col_STD_QTY.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_STD_QTY.AppearanceCell.Options.UseFont = true;
            this.grvProduct_col_STD_QTY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProduct_col_STD_QTY.AppearanceHeader.Options.UseFont = true;
            this.grvProduct_col_STD_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProduct_col_STD_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProduct_col_STD_QTY.Caption = "STD QTY";
            this.grvProduct_col_STD_QTY.FieldName = "STD_QTY";
            this.grvProduct_col_STD_QTY.Name = "grvProduct_col_STD_QTY";
            // 
            // grvProduct_col_MTL_SEQ_NO
            // 
            this.grvProduct_col_MTL_SEQ_NO.Caption = "MTL SEQ NO";
            this.grvProduct_col_MTL_SEQ_NO.FieldName = "MTL_SEQ_NO";
            this.grvProduct_col_MTL_SEQ_NO.Name = "grvProduct_col_MTL_SEQ_NO";
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
            this.txtSearch.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Size = new System.Drawing.Size(490, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmLOVMtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 362);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdMaterial);
            this.Controls.Add(this.panelControl1);
            this.LookAndFeel.SkinName = "Caramel";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLOVMtl";
            this.ShowInTaskbar = false;
            this.Text = "Material List";
            this.Load += new System.EventHandler(this.frmLOVMtl_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmLOVMtl_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLOVMtl_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaterial)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaterial;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MTL_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MTL_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MTL_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_STD_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_UNIT;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MTL_GRADE;
        private DevExpress.XtraGrid.Columns.GridColumn grvProduct_col_MTL_COLOR;
    }
}