namespace CheckUpdateApp
{
    partial class frmUpdateProgram
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.tmrStartDownload = new System.Windows.Forms.Timer();
            this.tmrFinishDownload = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(4, 8);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(211, 11);
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.Location = new System.Drawing.Point(6, 22);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(201, 17);
            this.lblUpdateStatus.Text = "Update.....";
            // 
            // tmrStartDownload
            // 
            this.tmrStartDownload.Interval = 1000;
            this.tmrStartDownload.Tick += new System.EventHandler(this.tmrStartDownload_Tick);
            // 
            // tmrFinishDownload
            // 
            this.tmrFinishDownload.Interval = 1000;
            this.tmrFinishDownload.Tick += new System.EventHandler(this.tmrFinishDownload_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblUpdateStatus);
            this.panel1.Location = new System.Drawing.Point(1, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 43);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.Text = "Update Scanner Program";
            // 
            // frmUpdateProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(222, 70);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateProgram";
            this.Text = "Update Program";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmUpdateProgram_Load);
            this.Closed += new System.EventHandler(this.frmUpdateProgram_Closed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblUpdateStatus;
        private System.Windows.Forms.Timer tmrStartDownload;
        private System.Windows.Forms.Timer tmrFinishDownload;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}

