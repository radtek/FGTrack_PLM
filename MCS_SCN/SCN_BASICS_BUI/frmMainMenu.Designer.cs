using HTN.BITS.MCS.SCN.UIL.Components;
using System.Windows.Forms;
namespace HTN.BITS.MCS.SCN.UIL
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lbl01Language = new System.Windows.Forms.Label();
            this.imgConnect = new System.Windows.Forms.PictureBox();
            this.imgMode = new System.Windows.Forms.PictureBox();
            this.Totalbattery = new System.Windows.Forms.PictureBox();
            this.lblpercent = new System.Windows.Forms.Label();
            this.btnMenuMixing = new System.Windows.Forms.PictureBox();
            this.btnMenu9 = new System.Windows.Forms.PictureBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblErrorMessageStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenuReplenish = new System.Windows.Forms.PictureBox();
            this.lblForMixing = new System.Windows.Forms.Label();
            this.lblForReplenish = new System.Windows.Forms.Label();
            this.lblForUtility = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblForExit = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnMenuExit = new System.Windows.Forms.PictureBox();
            this.pgbEng = new System.Windows.Forms.PictureBox();
            this.pgbThai = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.Black;
            this.pnlHeader.Controls.Add(this.lbl01Language);
            this.pnlHeader.Controls.Add(this.imgConnect);
            this.pnlHeader.Controls.Add(this.imgMode);
            this.pnlHeader.Controls.Add(this.Totalbattery);
            this.pnlHeader.Controls.Add(this.lblpercent);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(246, 24);
            // 
            // lbl01Language
            // 
            this.lbl01Language.BackColor = System.Drawing.Color.Black;
            this.lbl01Language.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01Language.ForeColor = System.Drawing.Color.White;
            this.lbl01Language.Location = new System.Drawing.Point(168, 5);
            this.lbl01Language.Name = "lbl01Language";
            this.lbl01Language.Size = new System.Drawing.Size(30, 12);
            this.lbl01Language.Text = "EN";
            this.lbl01Language.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // imgConnect
            // 
            this.imgConnect.BackColor = System.Drawing.Color.Black;
            this.imgConnect.Location = new System.Drawing.Point(200, 4);
            this.imgConnect.Name = "imgConnect";
            this.imgConnect.Size = new System.Drawing.Size(16, 16);
            this.imgConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // imgMode
            // 
            this.imgMode.BackColor = System.Drawing.Color.Black;
            this.imgMode.Location = new System.Drawing.Point(218, 4);
            this.imgMode.Name = "imgMode";
            this.imgMode.Size = new System.Drawing.Size(16, 16);
            this.imgMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // Totalbattery
            // 
            this.Totalbattery.BackColor = System.Drawing.Color.Black;
            this.Totalbattery.Location = new System.Drawing.Point(4, 4);
            this.Totalbattery.Name = "Totalbattery";
            this.Totalbattery.Size = new System.Drawing.Size(56, 16);
            this.Totalbattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblpercent
            // 
            this.lblpercent.BackColor = System.Drawing.Color.Black;
            this.lblpercent.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblpercent.ForeColor = System.Drawing.Color.DarkGray;
            this.lblpercent.Location = new System.Drawing.Point(62, 7);
            this.lblpercent.Name = "lblpercent";
            this.lblpercent.Size = new System.Drawing.Size(30, 12);
            this.lblpercent.Text = "100%";
            // 
            // btnMenuMixing
            // 
            this.btnMenuMixing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(149)))));
            this.btnMenuMixing.Location = new System.Drawing.Point(6, 106);
            this.btnMenuMixing.Name = "btnMenuMixing";
            this.btnMenuMixing.Size = new System.Drawing.Size(230, 58);
            this.btnMenuMixing.Click += new System.EventHandler(this.btnMenuMixing_Click);
            // 
            // btnMenu9
            // 
            this.btnMenu9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(25)))), ((int)(((byte)(61)))));
            this.btnMenu9.Location = new System.Drawing.Point(6, 234);
            this.btnMenu9.Name = "btnMenu9";
            this.btnMenu9.Size = new System.Drawing.Size(143, 58);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.lblIPAddress.Location = new System.Drawing.Point(45, 28);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(190, 15);
            this.lblIPAddress.Text = "IP: 000.000.000.000";
            this.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(161, 300);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(73, 14);
            this.lblVersion.Text = "V. 1.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblErrorMessageStatus
            // 
            this.lblErrorMessageStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblErrorMessageStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblErrorMessageStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblErrorMessageStatus.Location = new System.Drawing.Point(5, 300);
            this.lblErrorMessageStatus.Name = "lblErrorMessageStatus";
            this.lblErrorMessageStatus.Size = new System.Drawing.Size(174, 14);
            this.lblErrorMessageStatus.Text = "Device resued from suspend.";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(71)))), ((int)(((byte)(38)))));
            this.label1.Location = new System.Drawing.Point(18, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 32);
            this.label1.Text = "Material Checking System";
            // 
            // btnMenuReplenish
            // 
            this.btnMenuReplenish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.btnMenuReplenish.Location = new System.Drawing.Point(6, 170);
            this.btnMenuReplenish.Name = "btnMenuReplenish";
            this.btnMenuReplenish.Size = new System.Drawing.Size(230, 58);
            this.btnMenuReplenish.Click += new System.EventHandler(this.btnMenuReplenish_Click);
            // 
            // lblForMixing
            // 
            this.lblForMixing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblForMixing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(149)))));
            this.lblForMixing.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblForMixing.ForeColor = System.Drawing.Color.White;
            this.lblForMixing.Location = new System.Drawing.Point(83, 124);
            this.lblForMixing.Name = "lblForMixing";
            this.lblForMixing.Size = new System.Drawing.Size(130, 30);
            this.lblForMixing.Text = "Mixing";
            // 
            // lblForReplenish
            // 
            this.lblForReplenish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblForReplenish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.lblForReplenish.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblForReplenish.ForeColor = System.Drawing.Color.White;
            this.lblForReplenish.Location = new System.Drawing.Point(83, 190);
            this.lblForReplenish.Name = "lblForReplenish";
            this.lblForReplenish.Size = new System.Drawing.Size(133, 30);
            this.lblForReplenish.Text = "Replenish";
            // 
            // lblForUtility
            // 
            this.lblForUtility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblForUtility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(25)))), ((int)(((byte)(61)))));
            this.lblForUtility.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblForUtility.ForeColor = System.Drawing.Color.White;
            this.lblForUtility.Location = new System.Drawing.Point(83, 249);
            this.lblForUtility.Name = "lblForUtility";
            this.lblForUtility.Size = new System.Drawing.Size(64, 30);
            this.lblForUtility.Text = "Utility";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(0)))), ((int)(((byte)(174)))));
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(200, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.Text = "1";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(200, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 17);
            this.label6.Text = "2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(30)))), ((int)(((byte)(75)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(113, 274);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 17);
            this.label7.Text = "3";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 174);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 50);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 110);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(61, 50);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(179, 235);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 34);
            // 
            // lblForExit
            // 
            this.lblForExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblForExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(71)))), ((int)(((byte)(38)))));
            this.lblForExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblForExit.ForeColor = System.Drawing.Color.White;
            this.lblForExit.Location = new System.Drawing.Point(155, 269);
            this.lblForExit.Name = "lblForExit";
            this.lblForExit.Size = new System.Drawing.Size(79, 23);
            this.lblForExit.Text = "Exit";
            this.lblForExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(10, 239);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(61, 50);
            // 
            // btnMenuExit
            // 
            this.btnMenuExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(71)))), ((int)(((byte)(38)))));
            this.btnMenuExit.Location = new System.Drawing.Point(154, 234);
            this.btnMenuExit.Name = "btnMenuExit";
            this.btnMenuExit.Size = new System.Drawing.Size(82, 58);
            this.btnMenuExit.Click += new System.EventHandler(this.btnMenuExit_Click);
            // 
            // pgbEng
            // 
            this.pgbEng.Image = ((System.Drawing.Image)(resources.GetObject("pgbEng.Image")));
            this.pgbEng.Location = new System.Drawing.Point(63, 30);
            this.pgbEng.Name = "pgbEng";
            this.pgbEng.Size = new System.Drawing.Size(30, 25);
            this.pgbEng.Click += new System.EventHandler(this.pgbEng_Click);
            // 
            // pgbThai
            // 
            this.pgbThai.Image = ((System.Drawing.Image)(resources.GetObject("pgbThai.Image")));
            this.pgbThai.Location = new System.Drawing.Point(3, 30);
            this.pgbThai.Name = "pgbThai";
            this.pgbThai.Size = new System.Drawing.Size(30, 25);
            this.pgbThai.Visible = false;
            this.pgbThai.Click += new System.EventHandler(this.pgbThai_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(27, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.Text = "F3";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(86, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.Text = "F4";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(238, 317);
            this.ControlBox = false;
            this.Controls.Add(this.pgbEng);
            this.Controls.Add(this.pgbThai);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lblForExit);
            this.Controls.Add(this.btnMenuExit);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblForUtility);
            this.Controls.Add(this.lblForReplenish);
            this.Controls.Add(this.lblForMixing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.btnMenu9);
            this.Controls.Add(this.btnMenuReplenish);
            this.Controls.Add(this.btnMenuMixing);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblErrorMessageStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmMainMenu";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMainMenu_KeyDown);
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox btnMenuMixing;
        private System.Windows.Forms.PictureBox btnMenu9;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblErrorMessageStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl01Language;
        private System.Windows.Forms.PictureBox imgConnect;
        private System.Windows.Forms.PictureBox imgMode;
        private System.Windows.Forms.PictureBox Totalbattery;
        private System.Windows.Forms.Label lblpercent;
        private System.Windows.Forms.PictureBox btnMenuReplenish;
        private System.Windows.Forms.Label lblForMixing;
        private System.Windows.Forms.Label lblForReplenish;
        private System.Windows.Forms.Label lblForUtility;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblForExit;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox btnMenuExit;
        private System.Windows.Forms.PictureBox pgbEng;
        private System.Windows.Forms.PictureBox pgbThai;
        private Label label2;
        private Label label3;
    }
}