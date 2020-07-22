namespace HTN.BITS.FGTRACK.Vertical
{
    partial class frmNGConfirm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl01MainMenu = new System.Windows.Forms.Label();
            this.btn01Cancel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btn01Save = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.lbl01MainMenu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(203, 25);
            // 
            // lbl01MainMenu
            // 
            this.lbl01MainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbl01MainMenu.ForeColor = System.Drawing.Color.White;
            this.lbl01MainMenu.Location = new System.Drawing.Point(2, 4);
            this.lbl01MainMenu.Name = "lbl01MainMenu";
            this.lbl01MainMenu.Size = new System.Drawing.Size(171, 18);
            this.lbl01MainMenu.Text = "Confirm NG?";
            // 
            // btn01Cancel
            // 
            this.btn01Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn01Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btn01Cancel.Location = new System.Drawing.Point(105, 70);
            this.btn01Cancel.Name = "btn01Cancel";
            this.btn01Cancel.Size = new System.Drawing.Size(68, 27);
            this.btn01Cancel.TabIndex = 225;
            this.btn01Cancel.Text = "&No";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(129, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 14);
            this.label13.Text = "Esc.";
            // 
            // btn01Save
            // 
            this.btn01Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn01Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btn01Save.Location = new System.Drawing.Point(27, 70);
            this.btn01Save.Name = "btn01Save";
            this.btn01Save.Size = new System.Drawing.Size(68, 27);
            this.btn01Save.TabIndex = 248;
            this.btn01Save.Text = "&Yes";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(45, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 14);
            this.label14.Text = "Enter.";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(11, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 17);
            this.label8.Text = "Have NG ?";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn01Cancel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btn01Save);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 119);
            // 
            // frmNGConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(207, 124);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNGConfirm";
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl01MainMenu;
        private System.Windows.Forms.Button btn01Cancel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn01Save;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
    }
}