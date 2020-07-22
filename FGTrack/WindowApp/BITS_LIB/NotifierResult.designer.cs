namespace HTN.BITS.LIB
{
    partial class NotifierResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifierResult));
            this.grpCaption = new DevExpress.XtraEditors.GroupControl();
            this.lblMessaageResult = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.picNotifier = new DevExpress.XtraEditors.PictureEdit();
            this.imgNotifierList = new System.Windows.Forms.ImageList(this.components);
            this.tmrShow = new System.Windows.Forms.Timer(this.components);
            this.tmrHide = new System.Windows.Forms.Timer(this.components);
            this.tmrShowWait = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpCaption)).BeginInit();
            this.grpCaption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotifier.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCaption
            // 
            this.grpCaption.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11F);
            this.grpCaption.AppearanceCaption.Options.UseFont = true;
            this.grpCaption.Controls.Add(this.lblMessaageResult);
            this.grpCaption.Controls.Add(this.btnOK);
            this.grpCaption.Controls.Add(this.picNotifier);
            this.grpCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCaption.Location = new System.Drawing.Point(0, 0);
            this.grpCaption.Name = "grpCaption";
            this.grpCaption.Size = new System.Drawing.Size(289, 112);
            this.grpCaption.TabIndex = 0;
            this.grpCaption.Text = "<Caption Message>";
            this.grpCaption.Click += new System.EventHandler(this.grpCaption_Click);
            // 
            // lblMessaageResult
            // 
            this.lblMessaageResult.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblMessaageResult.Appearance.Options.UseFont = true;
            this.lblMessaageResult.Appearance.Options.UseTextOptions = true;
            this.lblMessaageResult.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
            this.lblMessaageResult.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMessaageResult.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMessaageResult.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMessaageResult.Location = new System.Drawing.Point(75, 29);
            this.lblMessaageResult.Name = "lblMessaageResult";
            this.lblMessaageResult.Size = new System.Drawing.Size(209, 50);
            this.lblMessaageResult.TabIndex = 2;
            this.lblMessaageResult.Text = "<Message Result> test by jack";
            this.lblMessaageResult.Click += new System.EventHandler(this.lblMessaageResult_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(114, 82);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // picNotifier
            // 
            this.picNotifier.Location = new System.Drawing.Point(8, 33);
            this.picNotifier.Name = "picNotifier";
            this.picNotifier.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picNotifier.Properties.Appearance.Options.UseBackColor = true;
            this.picNotifier.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picNotifier.Size = new System.Drawing.Size(60, 60);
            this.picNotifier.TabIndex = 0;
            this.picNotifier.Click += new System.EventHandler(this.picNotifier_Click);
            // 
            // imgNotifierList
            // 
            this.imgNotifierList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNotifierList.ImageStream")));
            this.imgNotifierList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNotifierList.Images.SetKeyName(0, "Danger_Shield.png");
            this.imgNotifierList.Images.SetKeyName(1, "Safe_Shield.png");
            this.imgNotifierList.Images.SetKeyName(2, "Unknown_Shield.png");
            this.imgNotifierList.Images.SetKeyName(3, "Warning_Shield.png");
            // 
            // tmrShow
            // 
            this.tmrShow.Enabled = true;
            this.tmrShow.Interval = 50;
            this.tmrShow.Tick += new System.EventHandler(this.tmrShow_Tick);
            // 
            // tmrHide
            // 
            this.tmrHide.Tick += new System.EventHandler(this.tmrHide_Tick);
            // 
            // tmrShowWait
            // 
            this.tmrShowWait.Interval = 200;
            this.tmrShowWait.Tick += new System.EventHandler(this.tmrShowWait_Tick);
            // 
            // NotifierResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 112);
            this.Controls.Add(this.grpCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifierResult";
            this.Opacity = 0.001;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotifierResult";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NotifierResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpCaption)).EndInit();
            this.grpCaption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNotifier.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpCaption;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PictureEdit picNotifier;
        private System.Windows.Forms.ImageList imgNotifierList;
        private DevExpress.XtraEditors.LabelControl lblMessaageResult;
        private System.Windows.Forms.Timer tmrShow;
        public System.Windows.Forms.Timer tmrHide;
        private System.Windows.Forms.Timer tmrShowWait;
    }
}