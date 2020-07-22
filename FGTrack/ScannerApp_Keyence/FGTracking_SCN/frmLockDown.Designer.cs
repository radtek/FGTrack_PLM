namespace HTN.BITS.SCN.LOCKDOWN
{
    partial class frmLockDown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLockDown));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPressProgram = new System.Windows.Forms.Button();
            this.btnVeticalProgram = new System.Windows.Forms.Button();
            this.btnHorizontalProgram = new System.Windows.Forms.Button();
            this.btnFGWHSProgram = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblErrorMessageStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMTSTVERProgram = new System.Windows.Forms.Button();
            this.txtEsc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFGPressProgram = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTampoProgram = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBar1 = new MobileProcessBarControl.ProgressBar();
            this.btnAssemblyProgram = new System.Windows.Forms.Button();
            this.btnInitialSetup = new System.Windows.Forms.Button();
            this.lblInitialStatus = new System.Windows.Forms.Label();
            this.tmrProcess = new System.Windows.Forms.Timer();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.btnMTSTTAMPOProgram = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnMATProgram = new System.Windows.Forms.Button();
            this.Totalbattery2 = new System.Windows.Forms.PictureBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblSCN_SERIAL = new System.Windows.Forms.Label();
            this.btnMATCheck = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnPressProgram
            // 
            this.btnPressProgram.BackColor = System.Drawing.Color.Silver;
            this.btnPressProgram.Enabled = false;
            this.btnPressProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnPressProgram.ForeColor = System.Drawing.Color.Gray;
            this.btnPressProgram.Location = new System.Drawing.Point(130, 81);
            this.btnPressProgram.Name = "btnPressProgram";
            this.btnPressProgram.Size = new System.Drawing.Size(101, 21);
            this.btnPressProgram.TabIndex = 1;
            this.btnPressProgram.Text = "PRESS";
            this.btnPressProgram.Click += new System.EventHandler(this.btnPressProgram_Click);
            // 
            // btnVeticalProgram
            // 
            this.btnVeticalProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnVeticalProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnVeticalProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnVeticalProgram.Location = new System.Drawing.Point(130, 104);
            this.btnVeticalProgram.Name = "btnVeticalProgram";
            this.btnVeticalProgram.Size = new System.Drawing.Size(101, 21);
            this.btnVeticalProgram.TabIndex = 2;
            this.btnVeticalProgram.Text = "INJECTION";
            this.btnVeticalProgram.Click += new System.EventHandler(this.btnVeticalProgram_Click);
            // 
            // btnHorizontalProgram
            // 
            this.btnHorizontalProgram.BackColor = System.Drawing.Color.Silver;
            this.btnHorizontalProgram.Enabled = false;
            this.btnHorizontalProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnHorizontalProgram.ForeColor = System.Drawing.Color.Gray;
            this.btnHorizontalProgram.Location = new System.Drawing.Point(130, 128);
            this.btnHorizontalProgram.Name = "btnHorizontalProgram";
            this.btnHorizontalProgram.Size = new System.Drawing.Size(101, 21);
            this.btnHorizontalProgram.TabIndex = 3;
            this.btnHorizontalProgram.Text = "HORIZONTAL";
            this.btnHorizontalProgram.Click += new System.EventHandler(this.btnHorizontalProgram_Click);
            // 
            // btnFGWHSProgram
            // 
            this.btnFGWHSProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFGWHSProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnFGWHSProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnFGWHSProgram.Location = new System.Drawing.Point(18, 149);
            this.btnFGWHSProgram.Name = "btnFGWHSProgram";
            this.btnFGWHSProgram.Size = new System.Drawing.Size(92, 21);
            this.btnFGWHSProgram.TabIndex = 6;
            this.btnFGWHSProgram.Text = "FG WHS";
            this.btnFGWHSProgram.Click += new System.EventHandler(this.btnFGWHSProgram_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.MistyRose;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(112, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.Text = "1.";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MistyRose;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(112, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.Text = "2.";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.MistyRose;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(112, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.Text = "3.";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.MistyRose;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(0, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.Text = "6.";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(0, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 13);
            this.label4.Text = "@2011 Nittsu Logistics. All rights reserved";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblErrorMessageStatus
            // 
            this.lblErrorMessageStatus.BackColor = System.Drawing.Color.White;
            this.lblErrorMessageStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblErrorMessageStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblErrorMessageStatus.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblErrorMessageStatus.Location = new System.Drawing.Point(0, 287);
            this.lblErrorMessageStatus.Name = "lblErrorMessageStatus";
            this.lblErrorMessageStatus.Size = new System.Drawing.Size(238, 20);
            this.lblErrorMessageStatus.Text = " Device resued from suspend.";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.MistyRose;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(0, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.Text = "5.";
            // 
            // btnMTSTVERProgram
            // 
            this.btnMTSTVERProgram.BackColor = System.Drawing.Color.Silver;
            this.btnMTSTVERProgram.Enabled = false;
            this.btnMTSTVERProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMTSTVERProgram.ForeColor = System.Drawing.Color.Gray;
            this.btnMTSTVERProgram.Location = new System.Drawing.Point(18, 127);
            this.btnMTSTVERProgram.Name = "btnMTSTVERProgram";
            this.btnMTSTVERProgram.Size = new System.Drawing.Size(92, 21);
            this.btnMTSTVERProgram.TabIndex = 5;
            this.btnMTSTVERProgram.Text = "MTST VER";
            this.btnMTSTVERProgram.Click += new System.EventHandler(this.btnMTSTWHSProgram_Click);
            // 
            // txtEsc
            // 
            this.txtEsc.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.txtEsc.Location = new System.Drawing.Point(155, 17);
            this.txtEsc.Name = "txtEsc";
            this.txtEsc.PasswordChar = '*';
            this.txtEsc.Size = new System.Drawing.Size(77, 16);
            this.txtEsc.TabIndex = 20;
            this.txtEsc.TabStop = false;
            this.txtEsc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEsc_KeyUp);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.MistyRose;
            this.label7.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(155, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 11);
            this.label7.Text = "For Exit.";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.MistyRose;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.Text = "7.";
            // 
            // btnFGPressProgram
            // 
            this.btnFGPressProgram.BackColor = System.Drawing.Color.Silver;
            this.btnFGPressProgram.Enabled = false;
            this.btnFGPressProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnFGPressProgram.ForeColor = System.Drawing.Color.Gray;
            this.btnFGPressProgram.Location = new System.Drawing.Point(18, 173);
            this.btnFGPressProgram.Name = "btnFGPressProgram";
            this.btnFGPressProgram.Size = new System.Drawing.Size(92, 21);
            this.btnFGPressProgram.TabIndex = 7;
            this.btnFGPressProgram.Text = "FG PRESS";
            this.btnFGPressProgram.Click += new System.EventHandler(this.btnFGPressProgram_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.MistyRose;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(112, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 16);
            this.label9.Text = "4.";
            // 
            // btnTampoProgram
            // 
            this.btnTampoProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTampoProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnTampoProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTampoProgram.Location = new System.Drawing.Point(130, 152);
            this.btnTampoProgram.Name = "btnTampoProgram";
            this.btnTampoProgram.Size = new System.Drawing.Size(101, 21);
            this.btnTampoProgram.TabIndex = 4;
            this.btnTampoProgram.Text = "PAINTING";
            this.btnTampoProgram.Click += new System.EventHandler(this.btnTampoProgram_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.MistyRose;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(147, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.Text = "PRODUCTION";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.MistyRose;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(5, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.Text = "WAREHOUSE";
            // 
            // progressBar1
            // 
            this.progressBar1.BackgroundDrawMethod = MobileProcessBarControl.ProgressBar.DrawMethod.Stretch;
            this.progressBar1.BackgroundLeadingSize = 12;
            this.progressBar1.BackgroundPicture = ((System.Drawing.Image)(resources.GetObject("progressBar1.BackgroundPicture")));
            this.progressBar1.BackgroundTrailingSize = 12;
            this.progressBar1.ForegroundDrawMethod = MobileProcessBarControl.ProgressBar.DrawMethod.Stretch;
            this.progressBar1.ForegroundLeadingSize = 12;
            this.progressBar1.ForegroundPicture = ((System.Drawing.Image)(resources.GetObject("progressBar1.ForegroundPicture")));
            this.progressBar1.ForegroundTrailingSize = 12;
            this.progressBar1.Location = new System.Drawing.Point(130, 239);
            this.progressBar1.Marquee = MobileProcessBarControl.ProgressBar.MarqueeStyle.TileWrap;
            this.progressBar1.MarqueeWidth = 40;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.OverlayDrawMethod = MobileProcessBarControl.ProgressBar.DrawMethod.Stretch;
            this.progressBar1.OverlayLeadingSize = 12;
            this.progressBar1.OverlayPicture = ((System.Drawing.Image)(resources.GetObject("progressBar1.OverlayPicture")));
            this.progressBar1.OverlayTrailingSize = 12;
            this.progressBar1.Size = new System.Drawing.Size(100, 21);
            this.progressBar1.Type = MobileProcessBarControl.ProgressBar.BarType.Marquee;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            // 
            // btnAssemblyProgram
            // 
            this.btnAssemblyProgram.BackColor = System.Drawing.Color.LightCoral;
            this.btnAssemblyProgram.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAssemblyProgram.ForeColor = System.Drawing.Color.White;
            this.btnAssemblyProgram.Location = new System.Drawing.Point(130, 225);
            this.btnAssemblyProgram.Name = "btnAssemblyProgram";
            this.btnAssemblyProgram.Size = new System.Drawing.Size(101, 21);
            this.btnAssemblyProgram.TabIndex = 142;
            this.btnAssemblyProgram.Text = "ASSEMBLY";
            this.btnAssemblyProgram.Click += new System.EventHandler(this.btnAssemblyProgram_Click);
            // 
            // btnInitialSetup
            // 
            this.btnInitialSetup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnInitialSetup.Location = new System.Drawing.Point(130, 201);
            this.btnInitialSetup.Name = "btnInitialSetup";
            this.btnInitialSetup.Size = new System.Drawing.Size(100, 20);
            this.btnInitialSetup.TabIndex = 35;
            this.btnInitialSetup.Text = "Initial Setup";
            this.btnInitialSetup.Visible = false;
            this.btnInitialSetup.Click += new System.EventHandler(this.btnInitialSetup_Click);
            // 
            // lblInitialStatus
            // 
            this.lblInitialStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblInitialStatus.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblInitialStatus.Location = new System.Drawing.Point(131, 249);
            this.lblInitialStatus.Name = "lblInitialStatus";
            this.lblInitialStatus.Size = new System.Drawing.Size(98, 13);
            this.lblInitialStatus.Visible = false;
            // 
            // tmrProcess
            // 
            this.tmrProcess.Interval = 25;
            this.tmrProcess.Tick += new System.EventHandler(this.tmrProcess_Tick);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(131, 265);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(99, 8);
            this.progressBar2.Visible = false;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.MistyRose;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(0, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 16);
            this.label12.Text = "8.";
            // 
            // btnMTSTTAMPOProgram
            // 
            this.btnMTSTTAMPOProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnMTSTTAMPOProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMTSTTAMPOProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMTSTTAMPOProgram.Location = new System.Drawing.Point(18, 197);
            this.btnMTSTTAMPOProgram.Name = "btnMTSTTAMPOProgram";
            this.btnMTSTTAMPOProgram.Size = new System.Drawing.Size(92, 21);
            this.btnMTSTTAMPOProgram.TabIndex = 55;
            this.btnMTSTTAMPOProgram.Text = "WIP PAINT";
            this.btnMTSTTAMPOProgram.Click += new System.EventHandler(this.btnMTSTTAMPOProgram_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.MistyRose;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(0, 223);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.Text = "9.";
            // 
            // btnMATProgram
            // 
            this.btnMATProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnMATProgram.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMATProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMATProgram.Location = new System.Drawing.Point(18, 221);
            this.btnMATProgram.Name = "btnMATProgram";
            this.btnMATProgram.Size = new System.Drawing.Size(92, 21);
            this.btnMATProgram.TabIndex = 77;
            this.btnMATProgram.Text = "MATERIAL";
            this.btnMATProgram.Click += new System.EventHandler(this.btnMATProgram_Click);
            // 
            // Totalbattery2
            // 
            this.Totalbattery2.Image = ((System.Drawing.Image)(resources.GetObject("Totalbattery2.Image")));
            this.Totalbattery2.Location = new System.Drawing.Point(5, 6);
            this.Totalbattery2.Name = "Totalbattery2";
            this.Totalbattery2.Size = new System.Drawing.Size(38, 21);
            this.Totalbattery2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.BackColor = System.Drawing.Color.MistyRose;
            this.lblIPAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblIPAddress.ForeColor = System.Drawing.Color.Black;
            this.lblIPAddress.Location = new System.Drawing.Point(5, 251);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(128, 14);
            this.lblIPAddress.Text = "IP : 000.000.000.000";
            // 
            // lblSCN_SERIAL
            // 
            this.lblSCN_SERIAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCN_SERIAL.BackColor = System.Drawing.Color.MistyRose;
            this.lblSCN_SERIAL.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Bold);
            this.lblSCN_SERIAL.ForeColor = System.Drawing.Color.Maroon;
            this.lblSCN_SERIAL.Location = new System.Drawing.Point(8, 271);
            this.lblSCN_SERIAL.Name = "lblSCN_SERIAL";
            this.lblSCN_SERIAL.Size = new System.Drawing.Size(113, 11);
            this.lblSCN_SERIAL.Text = "BT-W85G XXXXXXXX";
            // 
            // btnMATCheck
            // 
            this.btnMATCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnMATCheck.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMATCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMATCheck.Location = new System.Drawing.Point(130, 194);
            this.btnMATCheck.Name = "btnMATCheck";
            this.btnMATCheck.Size = new System.Drawing.Size(102, 21);
            this.btnMATCheck.TabIndex = 95;
            this.btnMATCheck.Text = "MAT. CHECK";
            this.btnMATCheck.Click += new System.EventHandler(this.btnMATCheck_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.MistyRose;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(114, 197);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 16);
            this.label14.Text = "0.";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.label16.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.SystemColors.Window;
            this.label16.Location = new System.Drawing.Point(113, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 12);
            this.label16.Text = "F3";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmLockDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 320);
            this.ControlBox = false;
            this.Controls.Add(this.btnAssemblyProgram);
            this.Controls.Add(this.btnMATCheck);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblSCN_SERIAL);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.Totalbattery2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnMATProgram);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnMTSTTAMPOProgram);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.lblInitialStatus);
            this.Controls.Add(this.btnInitialSetup);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnTampoProgram);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnFGPressProgram);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnMTSTVERProgram);
            this.Controls.Add(this.lblErrorMessageStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFGWHSProgram);
            this.Controls.Add(this.btnHorizontalProgram);
            this.Controls.Add(this.btnVeticalProgram);
            this.Controls.Add(this.btnPressProgram);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLockDown";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLockDown_KeyUp);
            this.Load += new System.EventHandler(this.frmLockDown_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmLockDown_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPressProgram;
        private System.Windows.Forms.Button btnVeticalProgram;
        private System.Windows.Forms.Button btnHorizontalProgram;
        private System.Windows.Forms.Button btnFGWHSProgram;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblErrorMessageStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMTSTVERProgram;
        private System.Windows.Forms.TextBox txtEsc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFGPressProgram;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTampoProgram;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private MobileProcessBarControl.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnInitialSetup;
        private System.Windows.Forms.Label lblInitialStatus;
        private System.Windows.Forms.Timer tmrProcess;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnMTSTTAMPOProgram;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnMATProgram;
        private System.Windows.Forms.PictureBox Totalbattery2;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblSCN_SERIAL;
        private System.Windows.Forms.Button btnMATCheck;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnAssemblyProgram;
        private System.Windows.Forms.Label label16;

    }
}