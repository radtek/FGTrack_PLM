namespace HTN.BITS.MCS.SCN.UIL
{
    partial class frmCheckUser
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
            this.lblScanUser = new System.Windows.Forms.Label();
            this.txtScanUserID = new System.Windows.Forms.TextBox();
            this.imgUser = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // lblScanUser
            // 
            this.lblScanUser.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblScanUser.ForeColor = System.Drawing.Color.White;
            this.lblScanUser.Location = new System.Drawing.Point(70, 31);
            this.lblScanUser.Name = "lblScanUser";
            this.lblScanUser.Size = new System.Drawing.Size(165, 20);
            this.lblScanUser.Text = "SCAN USER";
            // 
            // txtScanUserID
            // 
            this.txtScanUserID.Location = new System.Drawing.Point(71, 54);
            this.txtScanUserID.Name = "txtScanUserID";
            this.txtScanUserID.Size = new System.Drawing.Size(136, 23);
            this.txtScanUserID.TabIndex = 1;
            this.txtScanUserID.GotFocus += new System.EventHandler(this.txtScanUserID_GotFocus);
            this.txtScanUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanUserID_KeyDown);
            this.txtScanUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanUserID_KeyPress);
            this.txtScanUserID.LostFocus += new System.EventHandler(this.txtScanUserID_LostFocus);
            // 
            // imgUser
            // 
            this.imgUser.BackColor = System.Drawing.Color.Transparent;
            this.imgUser.Location = new System.Drawing.Point(11, 25);
            this.imgUser.Name = "imgUser";
            this.imgUser.Size = new System.Drawing.Size(55, 55);
            this.imgUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // frmCheckUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(164)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(238, 105);
            this.ControlBox = false;
            this.Controls.Add(this.imgUser);
            this.Controls.Add(this.lblScanUser);
            this.Controls.Add(this.txtScanUserID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmCheckUser";
            this.Text = "SCAN USER";
            this.Load += new System.EventHandler(this.frmCheckUser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblScanUser;
        private System.Windows.Forms.TextBox txtScanUserID;
        private System.Windows.Forms.PictureBox imgUser;
    }
}