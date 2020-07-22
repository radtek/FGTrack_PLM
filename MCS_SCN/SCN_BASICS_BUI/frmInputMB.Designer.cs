using HTN.BITS.MCS.SCN.UIL.Components;
namespace HTN.BITS.MCS.SCN.UIL
{
    partial class frmInputMB
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
            this.lbl01MB = new System.Windows.Forms.Label();
            this.txtMBVol = new HTN.BITS.MCS.SCN.UIL.Components.TextDecimalOnly();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl01MB
            // 
            this.lbl01MB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01MB.ForeColor = System.Drawing.Color.White;
            this.lbl01MB.Location = new System.Drawing.Point(4, 21);
            this.lbl01MB.Name = "lbl01MB";
            this.lbl01MB.Size = new System.Drawing.Size(83, 20);
            this.lbl01MB.Text = "M/B Volume:";
            this.lbl01MB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMBVol
            // 
            this.txtMBVol.Location = new System.Drawing.Point(92, 17);
            this.txtMBVol.MaxLength = 5;
            this.txtMBVol.Name = "txtMBVol";
            this.txtMBVol.Size = new System.Drawing.Size(106, 23);
            this.txtMBVol.TabIndex = 1;
            this.txtMBVol.GotFocus += new System.EventHandler(this.txtScanUserID_GotFocus);
            this.txtMBVol.LostFocus += new System.EventHandler(this.txtScanUserID_LostFocus);
            this.txtMBVol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTRES_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(0)))), ((int)(((byte)(174)))));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(92, 46);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 20);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmInputMB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(149)))));
            this.ClientSize = new System.Drawing.Size(213, 78);
            this.ControlBox = false;
            this.Controls.Add(this.txtMBVol);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbl01MB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmInputMB";
            this.Text = "SCAN USER";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl01MB;
        private TextDecimalOnly txtMBVol;
        private System.Windows.Forms.Button btnOk;
    }
}