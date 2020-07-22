namespace HTN.BITS.FGTRACK.FGPRESS
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
            this.txtScanUserID = new System.Windows.Forms.TextBox();
            this.lblScanUserBarcode = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAuthenticationCheck = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScanUserID
            // 
            this.txtScanUserID.Location = new System.Drawing.Point(11, 36);
            this.txtScanUserID.Name = "txtScanUserID";
            this.txtScanUserID.PasswordChar = '*';
            this.txtScanUserID.Size = new System.Drawing.Size(164, 23);
            this.txtScanUserID.TabIndex = 0;
            this.txtScanUserID.GotFocus += new System.EventHandler(this.txtScanUserID_GotFocus);
            this.txtScanUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanUserID_KeyDown);
            this.txtScanUserID.LostFocus += new System.EventHandler(this.txtScanUserID_LostFocus);
            // 
            // lblScanUserBarcode
            // 
            this.lblScanUserBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblScanUserBarcode.Location = new System.Drawing.Point(14, 16);
            this.lblScanUserBarcode.Name = "lblScanUserBarcode";
            this.lblScanUserBarcode.Size = new System.Drawing.Size(161, 18);
            this.lblScanUserBarcode.Text = "SCAN USER BARCODE";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblScanUserBarcode);
            this.panel1.Controls.Add(this.txtScanUserID);
            this.panel1.Location = new System.Drawing.Point(1, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 79);
            // 
            // lblAuthenticationCheck
            // 
            this.lblAuthenticationCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAuthenticationCheck.ForeColor = System.Drawing.Color.White;
            this.lblAuthenticationCheck.Location = new System.Drawing.Point(5, 5);
            this.lblAuthenticationCheck.Name = "lblAuthenticationCheck";
            this.lblAuthenticationCheck.Size = new System.Drawing.Size(171, 18);
            this.lblAuthenticationCheck.Text = "Authentication Check";
            // 
            // frmCheckUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(193, 105);
            this.ControlBox = false;
            this.Controls.Add(this.lblAuthenticationCheck);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmCheckUser";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtScanUserID;
        private System.Windows.Forms.Label lblScanUserBarcode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAuthenticationCheck;
    }
}