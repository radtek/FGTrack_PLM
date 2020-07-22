namespace HTN.BITS.UIL.PLASESS.Component.SysFileInfo
{
    partial class DIA_CopyFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DIA_CopyFiles));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelCopy = new DevExpress.XtraEditors.SimpleButton();
            this.Lab_CurrentFile = new DevExpress.XtraEditors.LabelControl();
            this.Prog_CurrentFile = new DevExpress.XtraEditors.ProgressBarControl();
            this.Lab_TotalFiles = new DevExpress.XtraEditors.LabelControl();
            this.Prog_TotalFiles = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prog_CurrentFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prog_TotalFiles.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancelCopy);
            this.groupControl1.Controls.Add(this.Lab_CurrentFile);
            this.groupControl1.Controls.Add(this.Prog_CurrentFile);
            this.groupControl1.Controls.Add(this.Lab_TotalFiles);
            this.groupControl1.Controls.Add(this.Prog_TotalFiles);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(378, 136);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Copy Files";
            // 
            // btnCancelCopy
            // 
            this.btnCancelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelCopy.Image")));
            this.btnCancelCopy.Location = new System.Drawing.Point(249, 103);
            this.btnCancelCopy.Name = "btnCancelCopy";
            this.btnCancelCopy.Size = new System.Drawing.Size(115, 23);
            this.btnCancelCopy.TabIndex = 4;
            this.btnCancelCopy.Text = "&Cancel Copy";
            this.btnCancelCopy.Click += new System.EventHandler(this.btnCancelCopy_Click);
            // 
            // Lab_CurrentFile
            // 
            this.Lab_CurrentFile.Location = new System.Drawing.Point(12, 64);
            this.Lab_CurrentFile.Name = "Lab_CurrentFile";
            this.Lab_CurrentFile.Size = new System.Drawing.Size(56, 13);
            this.Lab_CurrentFile.TabIndex = 3;
            this.Lab_CurrentFile.Text = "Current File";
            // 
            // Prog_CurrentFile
            // 
            this.Prog_CurrentFile.Location = new System.Drawing.Point(10, 78);
            this.Prog_CurrentFile.Name = "Prog_CurrentFile";
            this.Prog_CurrentFile.Size = new System.Drawing.Size(357, 18);
            this.Prog_CurrentFile.TabIndex = 2;
            // 
            // Lab_TotalFiles
            // 
            this.Lab_TotalFiles.Location = new System.Drawing.Point(12, 25);
            this.Lab_TotalFiles.Name = "Lab_TotalFiles";
            this.Lab_TotalFiles.Size = new System.Drawing.Size(48, 13);
            this.Lab_TotalFiles.TabIndex = 1;
            this.Lab_TotalFiles.Text = "Total Files";
            // 
            // Prog_TotalFiles
            // 
            this.Prog_TotalFiles.Location = new System.Drawing.Point(10, 39);
            this.Prog_TotalFiles.Name = "Prog_TotalFiles";
            this.Prog_TotalFiles.Size = new System.Drawing.Size(357, 18);
            this.Prog_TotalFiles.TabIndex = 0;
            // 
            // DIA_CopyFiles
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 136);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DIA_CopyFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIA_CopyFiles";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prog_CurrentFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prog_TotalFiles.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl Lab_CurrentFile;
        private DevExpress.XtraEditors.ProgressBarControl Prog_CurrentFile;
        private DevExpress.XtraEditors.LabelControl Lab_TotalFiles;
        private DevExpress.XtraEditors.ProgressBarControl Prog_TotalFiles;
        private DevExpress.XtraEditors.SimpleButton btnCancelCopy;
    }
}