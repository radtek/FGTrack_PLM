namespace HTN.BITS.UIL.PLASESS.Administrator
{
    partial class frmRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRole));
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.dntRole = new DevExpress.XtraEditors.DataNavigator();
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdRole = new DevExpress.XtraGrid.GridControl();
            this.grvRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvRole_col_ROLE_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRole_rps_ROLE_ID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvRole_col_ROLE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRole_rps_ROLE_NAME = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvRole_col_REC_STAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRole_rps_chkREC_STAT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grdRoleProgram = new DevExpress.XtraGrid.GridControl();
            this.grvRoleProgram = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvRoleProgram_col_PROG_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRoleProgram_col_PROG_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grvRoleProgram_col_REC_STAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grvRoleProgram_col_FLAG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvRoleProgram_col_ROLE_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_ROLE_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_ROLE_NAME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_chkREC_STAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoleProgram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoleProgram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl4
            // 
            this.panelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl4.Controls.Add(this.btnExit);
            this.panelControl4.Controls.Add(this.dntRole);
            this.panelControl4.Location = new System.Drawing.Point(4, 403);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(762, 32);
            this.panelControl4.TabIndex = 17;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(683, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dntRole
            // 
            this.dntRole.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dntRole.Appearance.Options.UseFont = true;
            this.dntRole.Buttons.Append.Visible = false;
            this.dntRole.Buttons.CancelEdit.Visible = false;
            this.dntRole.Buttons.EndEdit.Visible = false;
            this.dntRole.Buttons.Remove.Visible = false;
            this.dntRole.Location = new System.Drawing.Point(5, 6);
            this.dntRole.Name = "dntRole";
            this.dntRole.Size = new System.Drawing.Size(284, 20);
            this.dntRole.TabIndex = 1;
            this.dntRole.Text = "dataNavigator1";
            this.dntRole.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.dntRole.PositionChanged += new System.EventHandler(this.dntRole_PositionChanged);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddNew.Appearance.Options.UseFont = true;
            this.btnAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.Location = new System.Drawing.Point(44, 367);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Text = "&New";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.grdRole);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.btnAddNew);
            this.groupControl1.Controls.Add(this.btnEdit);
            this.groupControl1.Location = new System.Drawing.Point(4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(358, 395);
            this.groupControl1.TabIndex = 16;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(278, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdRole
            // 
            this.grdRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRole.Location = new System.Drawing.Point(4, 5);
            this.grdRole.MainView = this.grvRole;
            this.grdRole.Name = "grdRole";
            this.grdRole.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grvRole_rps_ROLE_ID,
            this.grvRole_rps_ROLE_NAME,
            this.grvRole_rps_chkREC_STAT});
            this.grdRole.Size = new System.Drawing.Size(349, 358);
            this.grdRole.TabIndex = 8;
            this.grdRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRole});
            // 
            // grvRole
            // 
            this.grvRole.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
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
            this.grvRole_col_ROLE_NAME,
            this.grvRole_col_REC_STAT});
            this.grvRole.GridControl = this.grdRole;
            this.grvRole.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvRole.Name = "grvRole";
            this.grvRole.OptionsCustomization.AllowColumnMoving = false;
            this.grvRole.OptionsMenu.EnableColumnMenu = false;
            this.grvRole.OptionsMenu.EnableFooterMenu = false;
            this.grvRole.OptionsMenu.EnableGroupPanelMenu = false;
            this.grvRole.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grvRole.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grvRole.OptionsView.EnableAppearanceEvenRow = true;
            this.grvRole.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvRole.OptionsView.ShowGroupPanel = false;
            this.grvRole.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.grvRole.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvRole_InitNewRow);
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
            this.grvRole_col_ROLE_ID.ColumnEdit = this.grvRole_rps_ROLE_ID;
            this.grvRole_col_ROLE_ID.FieldName = "ROLE_ID";
            this.grvRole_col_ROLE_ID.Name = "grvRole_col_ROLE_ID";
            this.grvRole_col_ROLE_ID.Visible = true;
            this.grvRole_col_ROLE_ID.VisibleIndex = 0;
            this.grvRole_col_ROLE_ID.Width = 59;
            // 
            // grvRole_rps_ROLE_ID
            // 
            this.grvRole_rps_ROLE_ID.AutoHeight = false;
            this.grvRole_rps_ROLE_ID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grvRole_rps_ROLE_ID.Name = "grvRole_rps_ROLE_ID";
            // 
            // grvRole_col_ROLE_NAME
            // 
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvRole_col_ROLE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRole_col_ROLE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRole_col_ROLE_NAME.Caption = "Role Name";
            this.grvRole_col_ROLE_NAME.ColumnEdit = this.grvRole_rps_ROLE_NAME;
            this.grvRole_col_ROLE_NAME.FieldName = "ROLE_NAME";
            this.grvRole_col_ROLE_NAME.Name = "grvRole_col_ROLE_NAME";
            this.grvRole_col_ROLE_NAME.Visible = true;
            this.grvRole_col_ROLE_NAME.VisibleIndex = 1;
            this.grvRole_col_ROLE_NAME.Width = 156;
            // 
            // grvRole_rps_ROLE_NAME
            // 
            this.grvRole_rps_ROLE_NAME.AutoHeight = false;
            this.grvRole_rps_ROLE_NAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grvRole_rps_ROLE_NAME.Name = "grvRole_rps_ROLE_NAME";
            // 
            // grvRole_col_REC_STAT
            // 
            this.grvRole_col_REC_STAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRole_col_REC_STAT.AppearanceHeader.Options.UseFont = true;
            this.grvRole_col_REC_STAT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRole_col_REC_STAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRole_col_REC_STAT.Caption = "Status";
            this.grvRole_col_REC_STAT.ColumnEdit = this.grvRole_rps_chkREC_STAT;
            this.grvRole_col_REC_STAT.FieldName = "REC_STAT";
            this.grvRole_col_REC_STAT.Name = "grvRole_col_REC_STAT";
            this.grvRole_col_REC_STAT.Visible = true;
            this.grvRole_col_REC_STAT.VisibleIndex = 2;
            this.grvRole_col_REC_STAT.Width = 45;
            // 
            // grvRole_rps_chkREC_STAT
            // 
            this.grvRole_rps_chkREC_STAT.AutoHeight = false;
            this.grvRole_rps_chkREC_STAT.Name = "grvRole_rps_chkREC_STAT";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(200, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(122, 367);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.grdRoleProgram);
            this.panelControl3.Location = new System.Drawing.Point(364, 4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(402, 394);
            this.panelControl3.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(8, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Role Programs";
            // 
            // grdRoleProgram
            // 
            this.grdRoleProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRoleProgram.Location = new System.Drawing.Point(4, 27);
            this.grdRoleProgram.MainView = this.grvRoleProgram;
            this.grdRoleProgram.Name = "grdRoleProgram";
            this.grdRoleProgram.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemCheckEdit1});
            this.grdRoleProgram.Size = new System.Drawing.Size(394, 362);
            this.grdRoleProgram.TabIndex = 9;
            this.grdRoleProgram.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRoleProgram});
            // 
            // grvRoleProgram
            // 
            this.grvRoleProgram.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvRoleProgram.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvRoleProgram.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRoleProgram.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRoleProgram.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvRoleProgram.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvRoleProgram.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvRoleProgram.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRoleProgram.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRoleProgram.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvRoleProgram.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvRoleProgram.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvRoleProgram.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvRoleProgram.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvRoleProgram.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvRoleProgram.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvRoleProgram.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvRoleProgram.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvRoleProgram_col_PROG_ID,
            this.grvRoleProgram_col_PROG_NAME,
            this.grvRoleProgram_col_REC_STAT,
            this.grvRoleProgram_col_FLAG,
            this.grvRoleProgram_col_ROLE_ID});
            this.grvRoleProgram.GridControl = this.grdRoleProgram;
            this.grvRoleProgram.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvRoleProgram.Name = "grvRoleProgram";
            this.grvRoleProgram.OptionsCustomization.AllowColumnMoving = false;
            this.grvRoleProgram.OptionsMenu.EnableColumnMenu = false;
            this.grvRoleProgram.OptionsMenu.EnableFooterMenu = false;
            this.grvRoleProgram.OptionsMenu.EnableGroupPanelMenu = false;
            this.grvRoleProgram.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grvRoleProgram.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grvRoleProgram.OptionsView.EnableAppearanceEvenRow = true;
            this.grvRoleProgram.OptionsView.ShowGroupPanel = false;
            this.grvRoleProgram.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.grvRoleProgram.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.grvRoleProgram_RowUpdated);
            // 
            // grvRoleProgram_col_PROG_ID
            // 
            this.grvRoleProgram_col_PROG_ID.AppearanceCell.Options.UseTextOptions = true;
            this.grvRoleProgram_col_PROG_ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoleProgram_col_PROG_ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRoleProgram_col_PROG_ID.AppearanceHeader.Options.UseFont = true;
            this.grvRoleProgram_col_PROG_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRoleProgram_col_PROG_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoleProgram_col_PROG_ID.Caption = "Program ID";
            this.grvRoleProgram_col_PROG_ID.FieldName = "PROG_ID";
            this.grvRoleProgram_col_PROG_ID.Name = "grvRoleProgram_col_PROG_ID";
            this.grvRoleProgram_col_PROG_ID.OptionsColumn.AllowEdit = false;
            this.grvRoleProgram_col_PROG_ID.OptionsColumn.AllowFocus = false;
            this.grvRoleProgram_col_PROG_ID.Visible = true;
            this.grvRoleProgram_col_PROG_ID.VisibleIndex = 0;
            this.grvRoleProgram_col_PROG_ID.Width = 81;
            // 
            // grvRoleProgram_col_PROG_NAME
            // 
            this.grvRoleProgram_col_PROG_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRoleProgram_col_PROG_NAME.AppearanceHeader.Options.UseFont = true;
            this.grvRoleProgram_col_PROG_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRoleProgram_col_PROG_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoleProgram_col_PROG_NAME.Caption = "Program Name";
            this.grvRoleProgram_col_PROG_NAME.ColumnEdit = this.repositoryItemTextEdit2;
            this.grvRoleProgram_col_PROG_NAME.FieldName = "PROG_NAME";
            this.grvRoleProgram_col_PROG_NAME.Name = "grvRoleProgram_col_PROG_NAME";
            this.grvRoleProgram_col_PROG_NAME.OptionsColumn.AllowEdit = false;
            this.grvRoleProgram_col_PROG_NAME.OptionsColumn.AllowFocus = false;
            this.grvRoleProgram_col_PROG_NAME.Visible = true;
            this.grvRoleProgram_col_PROG_NAME.VisibleIndex = 1;
            this.grvRoleProgram_col_PROG_NAME.Width = 188;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // grvRoleProgram_col_REC_STAT
            // 
            this.grvRoleProgram_col_REC_STAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvRoleProgram_col_REC_STAT.AppearanceHeader.Options.UseFont = true;
            this.grvRoleProgram_col_REC_STAT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvRoleProgram_col_REC_STAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoleProgram_col_REC_STAT.Caption = "Active";
            this.grvRoleProgram_col_REC_STAT.ColumnEdit = this.repositoryItemCheckEdit1;
            this.grvRoleProgram_col_REC_STAT.FieldName = "REC_STAT";
            this.grvRoleProgram_col_REC_STAT.Name = "grvRoleProgram_col_REC_STAT";
            this.grvRoleProgram_col_REC_STAT.Visible = true;
            this.grvRoleProgram_col_REC_STAT.VisibleIndex = 2;
            this.grvRoleProgram_col_REC_STAT.Width = 56;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // grvRoleProgram_col_FLAG
            // 
            this.grvRoleProgram_col_FLAG.FieldName = "FLAG";
            this.grvRoleProgram_col_FLAG.Name = "grvRoleProgram_col_FLAG";
            // 
            // grvRoleProgram_col_ROLE_ID
            // 
            this.grvRoleProgram_col_ROLE_ID.FieldName = "ROLE_ID";
            this.grvRoleProgram_col_ROLE_ID.Name = "grvRoleProgram_col_ROLE_ID";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // frmRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 440);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "NormalMode";
            this.Text = "Role";
            this.Load += new System.EventHandler(this.frmRole_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmRole_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRole_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_ROLE_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_ROLE_NAME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRole_rps_chkREC_STAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoleProgram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoleProgram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.DataNavigator dntRole;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraGrid.GridControl grdRole;
        private DevExpress.XtraGrid.Views.Grid.GridView grvRole;
        private DevExpress.XtraGrid.Columns.GridColumn grvRole_col_ROLE_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit grvRole_rps_ROLE_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvRole_col_ROLE_NAME;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit grvRole_rps_ROLE_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grvRole_col_REC_STAT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit grvRole_rps_chkREC_STAT;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl grdRoleProgram;
        private DevExpress.XtraGrid.Views.Grid.GridView grvRoleProgram;
        private DevExpress.XtraGrid.Columns.GridColumn grvRoleProgram_col_PROG_ID;
        private DevExpress.XtraGrid.Columns.GridColumn grvRoleProgram_col_PROG_NAME;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn grvRoleProgram_col_REC_STAT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn grvRoleProgram_col_FLAG;
        private DevExpress.XtraGrid.Columns.GridColumn grvRoleProgram_col_ROLE_ID;
    }
}