namespace HTN.BITS.UIL.PLASESS
{
    partial class frmLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.picLoginHeader = new DevExpress.XtraEditors.PictureEdit();
            this.chkRememberMe = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.dxpErr = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.picUserAuthentication = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.LogInIdle = new Winforms.Components.ApplicationIdle();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoginHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberMe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxpErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAuthentication.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // txtUserName
            // 
            this.txtUserName.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // lblUserName
            // 
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            // 
            // picLoginHeader
            // 
            resources.ApplyResources(this.picLoginHeader, "picLoginHeader");
            this.picLoginHeader.Name = "picLoginHeader";
            this.picLoginHeader.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.picLoginHeader.Properties.Appearance.Options.UseBackColor = true;
            // 
            // chkRememberMe
            // 
            resources.ApplyResources(this.chkRememberMe, "chkRememberMe");
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Properties.Caption = resources.GetString("chkRememberMe.Properties.Caption");
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.imageComboBoxEdit1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lblUserName);
            this.groupControl1.Controls.Add(this.chkRememberMe);
            this.groupControl1.Controls.Add(this.lblPassword);
            this.groupControl1.Controls.Add(this.btnLogin);
            this.groupControl1.Controls.Add(this.txtUserName);
            this.groupControl1.Controls.Add(this.txtPassword);
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // imageComboBoxEdit1
            // 
            resources.ApplyResources(this.imageComboBoxEdit1, "imageComboBoxEdit1");
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("imageComboBoxEdit1.Properties.Buttons"))))});
            this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("imageComboBoxEdit1.Properties.Items"), resources.GetString("imageComboBoxEdit1.Properties.Items1"), ((int)(resources.GetObject("imageComboBoxEdit1.Properties.Items2")))),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("imageComboBoxEdit1.Properties.Items3"), resources.GetString("imageComboBoxEdit1.Properties.Items4"), ((int)(resources.GetObject("imageComboBoxEdit1.Properties.Items5"))))});
            this.imageComboBoxEdit1.SelectedValueChanged += new System.EventHandler(this.imageComboBoxEdit1_SelectedValueChanged);
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // dxpErr
            // 
            this.dxpErr.ContainerControl = this;
            // 
            // picUserAuthentication
            // 
            resources.ApplyResources(this.picUserAuthentication, "picUserAuthentication");
            this.picUserAuthentication.Name = "picUserAuthentication";
            this.picUserAuthentication.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 7F);
            this.labelControl2.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 7F);
            this.lblVersion.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // LogInIdle
            // 
            this.LogInIdle.IdleTime = System.TimeSpan.Parse("00:01:00");
            // 
            // frmLogin
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.picUserAuthentication);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.picLoginHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoginHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberMe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxpErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAuthentication.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.PictureEdit picLoginHeader;
        private DevExpress.XtraEditors.CheckEdit chkRememberMe;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxpErr;
        private DevExpress.XtraEditors.PictureEdit picUserAuthentication;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Winforms.Components.ApplicationIdle LogInIdle;
    }
}