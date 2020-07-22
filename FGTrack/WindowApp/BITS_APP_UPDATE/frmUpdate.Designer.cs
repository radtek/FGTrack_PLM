namespace BITS_APP_UPDATE
{
    partial class frmUpdate
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
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.lblUpdateStatus = new DevExpress.XtraEditors.LabelControl();
            this.pgbDownload = new DevExpress.XtraEditors.ProgressBarControl();
            this.tmrStartDownload = new System.Windows.Forms.Timer(this.components);
            this.tmrFinishUpdate = new System.Windows.Forms.Timer(this.components);
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbDownload.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "The Asphalt World";
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.Location = new System.Drawing.Point(7, 99);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(71, 13);
            this.lblUpdateStatus.TabIndex = 5;
            this.lblUpdateStatus.Text = "Updating.......";
            // 
            // pgbDownload
            // 
            this.pgbDownload.Location = new System.Drawing.Point(3, 76);
            this.pgbDownload.Name = "pgbDownload";
            this.pgbDownload.Properties.ShowTitle = true;
            this.pgbDownload.Size = new System.Drawing.Size(492, 18);
            this.pgbDownload.TabIndex = 4;
            // 
            // tmrStartDownload
            // 
            this.tmrStartDownload.Interval = 1000;
            this.tmrStartDownload.Tick += new System.EventHandler(this.tmrStartDownload_Tick);
            // 
            // tmrFinishUpdate
            // 
            this.tmrFinishUpdate.Interval = 2000;
            this.tmrFinishUpdate.Tick += new System.EventHandler(this.tmrFinishUpdate_Tick);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::BITS_APP_UPDATE.Properties.Resources.TitleUpdateProgram;
            this.pictureEdit1.Location = new System.Drawing.Point(-1, 1);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(500, 70);
            this.pictureEdit1.TabIndex = 3;
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 115);
            this.ControlBox = false;
            this.Controls.Add(this.lblUpdateStatus);
            this.Controls.Add(this.pgbDownload);
            this.Controls.Add(this.pictureEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Program";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmUpdate_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUpdate_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pgbDownload.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.LabelControl lblUpdateStatus;
        private DevExpress.XtraEditors.ProgressBarControl pgbDownload;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.Timer tmrStartDownload;
        private System.Windows.Forms.Timer tmrFinishUpdate;
    }
}