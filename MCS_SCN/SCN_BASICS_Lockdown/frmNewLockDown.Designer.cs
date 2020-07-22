namespace HTN.BITS.MCS.SCN.LOCKDOWN
{
    partial class frmNewLockDown
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
            this.txtEsc = new System.Windows.Forms.TextBox();
            this.btnActiveForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.batteryProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtEsc
            // 
            this.txtEsc.BackColor = System.Drawing.Color.White;
            this.txtEsc.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.txtEsc.Location = new System.Drawing.Point(172, 19);
            this.txtEsc.Name = "txtEsc";
            this.txtEsc.PasswordChar = '*';
            this.txtEsc.Size = new System.Drawing.Size(60, 16);
            this.txtEsc.TabIndex = 20;
            this.txtEsc.TabStop = false;
            this.txtEsc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEsc_KeyUp);
            // 
            // btnActiveForm
            // 
            this.btnActiveForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(68)))), ((int)(((byte)(38)))));
            this.btnActiveForm.Location = new System.Drawing.Point(231, 309);
            this.btnActiveForm.Name = "btnActiveForm";
            this.btnActiveForm.Size = new System.Drawing.Size(5, 5);
            this.btnActiveForm.TabIndex = 22;
            this.btnActiveForm.Click += new System.EventHandler(this.btnActiveForm_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(68)))), ((int)(((byte)(38)))));
            this.button1.Location = new System.Drawing.Point(223, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(5, 5);
            this.button1.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(68)))), ((int)(((byte)(38)))));
            this.button2.Location = new System.Drawing.Point(215, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(5, 5);
            this.button2.TabIndex = 25;
            // 
            // batteryProgressBar
            // 
            this.batteryProgressBar.Location = new System.Drawing.Point(125, 294);
            this.batteryProgressBar.Name = "batteryProgressBar";
            this.batteryProgressBar.Size = new System.Drawing.Size(112, 10);
            this.batteryProgressBar.Value = 77;
            this.batteryProgressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Location = new System.Drawing.Point(102, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(137, 1);
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel2.Location = new System.Drawing.Point(102, 145);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(137, 1);
            this.panel2.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel3.Location = new System.Drawing.Point(102, 170);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(137, 1);
            this.panel3.Visible = false;
            // 
            // frmNewLockDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(242, 325);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnActiveForm);
            this.Controls.Add(this.txtEsc);
            this.Controls.Add(this.batteryProgressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewLockDown";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLockDown_KeyUp);
            this.Closed += new System.EventHandler(this.frmLockDown_Closed);
            this.Click += new System.EventHandler(this.frmLockDown_Click);
            this.Load += new System.EventHandler(this.frmLockDown_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmLockDown_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtEsc;
        private System.Windows.Forms.Button btnActiveForm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar batteryProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;

    }
}