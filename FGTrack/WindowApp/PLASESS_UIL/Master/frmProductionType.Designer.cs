namespace HTN.BITS.UIL.PLASESS.Master
{
    partial class frmProductionType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductionType));
            this.grdProductionType = new DevExpress.XtraGrid.GridControl();
            this.grvProductionType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvProductionType_col_SEQ_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductionType_col_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductionType_col_REMARK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductionType_col_REC_STAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvProductionType_rps_REC_STAT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dntJobOrder = new DevExpress.XtraEditors.DataNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductionType_rps_REC_STAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdProductionType
            // 
            this.grdProductionType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductionType.Location = new System.Drawing.Point(3, 4);
            this.grdProductionType.MainView = this.grvProductionType;
            this.grdProductionType.Name = "grdProductionType";
            this.grdProductionType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grvProductionType_rps_REC_STAT});
            this.grdProductionType.Size = new System.Drawing.Size(728, 220);
            this.grdProductionType.TabIndex = 0;
            this.grdProductionType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProductionType});
            // 
            // grvProductionType
            // 
            this.grvProductionType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvProductionType_col_SEQ_NO,
            this.grvProductionType_col_NAME,
            this.grvProductionType_col_REMARK,
            this.grvProductionType_col_REC_STAT});
            this.grvProductionType.GridControl = this.grdProductionType;
            this.grvProductionType.Name = "grvProductionType";
            this.grvProductionType.OptionsView.ShowGroupPanel = false;
            // 
            // grvProductionType_col_SEQ_NO
            // 
            this.grvProductionType_col_SEQ_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductionType_col_SEQ_NO.AppearanceHeader.Options.UseFont = true;
            this.grvProductionType_col_SEQ_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductionType_col_SEQ_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductionType_col_SEQ_NO.Caption = "Code";
            this.grvProductionType_col_SEQ_NO.FieldName = "SEQ_NO";
            this.grvProductionType_col_SEQ_NO.Name = "grvProductionType_col_SEQ_NO";
            this.grvProductionType_col_SEQ_NO.Visible = true;
            this.grvProductionType_col_SEQ_NO.VisibleIndex = 0;
            this.grvProductionType_col_SEQ_NO.Width = 90;
            // 
            // grvProductionType_col_NAME
            // 
            this.grvProductionType_col_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductionType_col_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvProductionType_col_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductionType_col_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductionType_col_NAME.Caption = "Name";
            this.grvProductionType_col_NAME.FieldName = "NAME";
            this.grvProductionType_col_NAME.Name = "grvProductionType_col_NAME";
            this.grvProductionType_col_NAME.Visible = true;
            this.grvProductionType_col_NAME.VisibleIndex = 1;
            this.grvProductionType_col_NAME.Width = 218;
            // 
            // grvProductionType_col_REMARK
            // 
            this.grvProductionType_col_REMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductionType_col_REMARK.AppearanceHeader.Options.UseFont = true;
            this.grvProductionType_col_REMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductionType_col_REMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductionType_col_REMARK.Caption = "Remark";
            this.grvProductionType_col_REMARK.FieldName = "REMARK";
            this.grvProductionType_col_REMARK.Name = "grvProductionType_col_REMARK";
            this.grvProductionType_col_REMARK.Visible = true;
            this.grvProductionType_col_REMARK.VisibleIndex = 2;
            this.grvProductionType_col_REMARK.Width = 305;
            // 
            // grvProductionType_col_REC_STAT
            // 
            this.grvProductionType_col_REC_STAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvProductionType_col_REC_STAT.AppearanceHeader.Options.UseFont = true;
            this.grvProductionType_col_REC_STAT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvProductionType_col_REC_STAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductionType_col_REC_STAT.Caption = "Status";
            this.grvProductionType_col_REC_STAT.ColumnEdit = this.grvProductionType_rps_REC_STAT;
            this.grvProductionType_col_REC_STAT.FieldName = "REC_STAT";
            this.grvProductionType_col_REC_STAT.Name = "grvProductionType_col_REC_STAT";
            this.grvProductionType_col_REC_STAT.Visible = true;
            this.grvProductionType_col_REC_STAT.VisibleIndex = 3;
            this.grvProductionType_col_REC_STAT.Width = 93;
            // 
            // grvProductionType_rps_REC_STAT
            // 
            this.grvProductionType_rps_REC_STAT.AutoHeight = false;
            this.grvProductionType_rps_REC_STAT.Name = "grvProductionType_rps_REC_STAT";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.dntJobOrder);
            this.panelControl1.Location = new System.Drawing.Point(3, 228);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(728, 31);
            this.panelControl1.TabIndex = 1;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonExit;
            this.simpleButton3.Location = new System.Drawing.Point(645, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(78, 23);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "E&xit";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonSave;
            this.simpleButton2.Location = new System.Drawing.Point(563, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(78, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "&Save";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonEdit;
            this.simpleButton1.Location = new System.Drawing.Point(481, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(78, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "&Edit";
            // 
            // dntJobOrder
            // 
            this.dntJobOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dntJobOrder.Appearance.Options.UseFont = true;
            this.dntJobOrder.Buttons.Append.Visible = false;
            this.dntJobOrder.Buttons.CancelEdit.Visible = false;
            this.dntJobOrder.Buttons.EndEdit.Visible = false;
            this.dntJobOrder.Buttons.First.Enabled = false;
            this.dntJobOrder.Buttons.First.Visible = false;
            this.dntJobOrder.Buttons.Last.Enabled = false;
            this.dntJobOrder.Buttons.Last.Visible = false;
            this.dntJobOrder.Buttons.NextPage.Enabled = false;
            this.dntJobOrder.Buttons.NextPage.Visible = false;
            this.dntJobOrder.Buttons.PrevPage.Enabled = false;
            this.dntJobOrder.Buttons.PrevPage.Visible = false;
            this.dntJobOrder.Buttons.Remove.Visible = false;
            this.dntJobOrder.Location = new System.Drawing.Point(3, 5);
            this.dntJobOrder.Name = "dntJobOrder";
            this.dntJobOrder.Size = new System.Drawing.Size(182, 20);
            this.dntJobOrder.TabIndex = 3;
            this.dntJobOrder.Text = "dataNavigator1";
            this.dntJobOrder.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            // 
            // frmProductionType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 262);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grdProductionType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProductionType";
            this.Text = "Production Type";
            ((System.ComponentModel.ISupportInitialize)(this.grdProductionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductionType_rps_REC_STAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdProductionType;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProductionType;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DataNavigator dntJobOrder;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductionType_col_SEQ_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductionType_col_NAME;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductionType_col_REC_STAT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit grvProductionType_rps_REC_STAT;
        private DevExpress.XtraGrid.Columns.GridColumn grvProductionType_col_REMARK;
    }
}