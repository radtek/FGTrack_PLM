namespace FGTracking_SCN
{
    partial class frmMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl01MainMenu = new System.Windows.Forms.Label();
            this.lblErrorMessageStatus = new System.Windows.Forms.Label();
            this.batteryProgressBar = new System.Windows.Forms.ProgressBar();
            this.batteryPercentLabel = new System.Windows.Forms.Label();
            this.powerTypeLabel = new System.Windows.Forms.Label();
            this.pgbEng = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pgbThai = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl01TitleHeader = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl01MenuHeader = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.lbl01MainMenu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(238, 25);
            // 
            // lbl01MainMenu
            // 
            this.lbl01MainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl01MainMenu.ForeColor = System.Drawing.Color.White;
            this.lbl01MainMenu.Location = new System.Drawing.Point(4, 4);
            this.lbl01MainMenu.Name = "lbl01MainMenu";
            this.lbl01MainMenu.Size = new System.Drawing.Size(88, 18);
            this.lbl01MainMenu.Text = "Main Menu";
            // 
            // lblErrorMessageStatus
            // 
            this.lblErrorMessageStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblErrorMessageStatus.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblErrorMessageStatus.Location = new System.Drawing.Point(1, 301);
            this.lblErrorMessageStatus.Name = "lblErrorMessageStatus";
            this.lblErrorMessageStatus.Size = new System.Drawing.Size(200, 14);
            this.lblErrorMessageStatus.Text = "Device resued from suspend.";
            // 
            // batteryProgressBar
            // 
            this.batteryProgressBar.Location = new System.Drawing.Point(5, 77);
            this.batteryProgressBar.Name = "batteryProgressBar";
            this.batteryProgressBar.Size = new System.Drawing.Size(113, 10);
            this.batteryProgressBar.Value = 77;
            // 
            // batteryPercentLabel
            // 
            this.batteryPercentLabel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.batteryPercentLabel.Location = new System.Drawing.Point(42, 61);
            this.batteryPercentLabel.Name = "batteryPercentLabel";
            this.batteryPercentLabel.Size = new System.Drawing.Size(77, 20);
            this.batteryPercentLabel.Text = "92%";
            this.batteryPercentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // powerTypeLabel
            // 
            this.powerTypeLabel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.powerTypeLabel.Location = new System.Drawing.Point(4, 62);
            this.powerTypeLabel.Name = "powerTypeLabel";
            this.powerTypeLabel.Size = new System.Drawing.Size(48, 20);
            this.powerTypeLabel.Text = "Battery";
            // 
            // pgbEng
            // 
            this.pgbEng.Image = ((System.Drawing.Image)(resources.GetObject("pgbEng.Image")));
            this.pgbEng.Location = new System.Drawing.Point(200, 63);
            this.pgbEng.Name = "pgbEng";
            this.pgbEng.Size = new System.Drawing.Size(30, 25);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(188, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 19);
            this.label2.Text = "E.";
            // 
            // pgbThai
            // 
            this.pgbThai.Image = ((System.Drawing.Image)(resources.GetObject("pgbThai.Image")));
            this.pgbThai.Location = new System.Drawing.Point(158, 63);
            this.pgbThai.Name = "pgbThai";
            this.pgbThai.Size = new System.Drawing.Size(30, 25);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(145, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 19);
            this.label1.Text = "T.";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.lbl01TitleHeader);
            this.panel2.Location = new System.Drawing.Point(6, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 26);
            // 
            // lbl01TitleHeader
            // 
            this.lbl01TitleHeader.BackColor = System.Drawing.Color.Black;
            this.lbl01TitleHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl01TitleHeader.ForeColor = System.Drawing.Color.White;
            this.lbl01TitleHeader.Location = new System.Drawing.Point(3, 5);
            this.lbl01TitleHeader.Name = "lbl01TitleHeader";
            this.lbl01TitleHeader.Size = new System.Drawing.Size(221, 16);
            this.lbl01TitleHeader.Text = "TIMeSx2 SYSTEM";
            this.lbl01TitleHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Black;
            this.panel8.Controls.Add(this.lbl01MenuHeader);
            this.panel8.Location = new System.Drawing.Point(21, 104);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(195, 26);
            // 
            // lbl01MenuHeader
            // 
            this.lbl01MenuHeader.BackColor = System.Drawing.Color.Black;
            this.lbl01MenuHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl01MenuHeader.ForeColor = System.Drawing.Color.White;
            this.lbl01MenuHeader.Location = new System.Drawing.Point(3, 5);
            this.lbl01MenuHeader.Name = "lbl01MenuHeader";
            this.lbl01MenuHeader.Size = new System.Drawing.Size(189, 16);
            this.lbl01MenuHeader.Text = "Main Menu";
            this.lbl01MenuHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(238, 317);
            this.ControlBox = false;
            this.Controls.Add(this.lblErrorMessageStatus);
            this.Controls.Add(this.batteryProgressBar);
            this.Controls.Add(this.batteryPercentLabel);
            this.Controls.Add(this.powerTypeLabel);
            this.Controls.Add(this.pgbEng);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pgbThai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainMenu";
            this.Text = "frmMainMenu";
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl01MainMenu;
        private System.Windows.Forms.Label lblErrorMessageStatus;
        private System.Windows.Forms.ProgressBar batteryProgressBar;
        private System.Windows.Forms.Label batteryPercentLabel;
        private System.Windows.Forms.Label powerTypeLabel;
        private System.Windows.Forms.PictureBox pgbEng;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pgbThai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl01TitleHeader;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl01MenuHeader;
    }
}