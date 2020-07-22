using HTN.BITS.MCS.SCN.UIL.Components;
namespace HTN.BITS.MCS.SCN.UIL
{
    partial class frmInputTRES
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
            this.lbl01TRES = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTRES = new HTN.BITS.MCS.SCN.UIL.Components.TextDecimalOnly();
            this.SuspendLayout();
            // 
            // lbl01TRES
            // 
            this.lbl01TRES.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01TRES.ForeColor = System.Drawing.Color.White;
            this.lbl01TRES.Location = new System.Drawing.Point(3, 20);
            this.lbl01TRES.Name = "lbl01TRES";
            this.lbl01TRES.Size = new System.Drawing.Size(70, 20);
            this.lbl01TRES.Text = "TRES:";
            this.lbl01TRES.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(191, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.Text = "%";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(0)))), ((int)(((byte)(174)))));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(76, 47);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(52, 20);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(0)))), ((int)(((byte)(174)))));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(134, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 20);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTRES
            // 
            this.txtTRES.Location = new System.Drawing.Point(76, 18);
            this.txtTRES.MaxLength = 5;
            this.txtTRES.Name = "txtTRES";
            this.txtTRES.Size = new System.Drawing.Size(112, 23);
            this.txtTRES.TabIndex = 1;
            this.txtTRES.Text = "25";
            this.txtTRES.GotFocus += new System.EventHandler(this.txtScanUserID_GotFocus);
            this.txtTRES.LostFocus += new System.EventHandler(this.txtScanUserID_LostFocus);
            this.txtTRES.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTRES_KeyDown);
            // 
            // frmInputTRES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(149)))));
            this.ClientSize = new System.Drawing.Size(213, 78);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl01TRES);
            this.Controls.Add(this.txtTRES);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmInputTRES";
            this.Text = "SCAN USER";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl01TRES;
        private TextDecimalOnly txtTRES;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}