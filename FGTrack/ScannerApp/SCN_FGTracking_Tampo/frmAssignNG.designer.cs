namespace HTN.BITS.FGTRACK.TAMPO
{
    partial class frmAssignNG
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
            this.lblNG_QTY = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt01JOB_NO = new System.Windows.Forms.TextBox();
            this.lbl01LabelLineOrderNo = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.lblUNIT_ID = new System.Windows.Forms.Label();
            this.btn01Cancel = new System.Windows.Forms.Button();
            this.btn01Save = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbJOB_LOT_LIST = new System.Windows.Forms.ComboBox();
            this.lblJOB_LOT = new System.Windows.Forms.Label();
            this.lblPRODUCT_NO = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblMTL_TYPE = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPRODUCT_NAME = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
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
            this.lbl01MainMenu.Location = new System.Drawing.Point(2, 4);
            this.lbl01MainMenu.Name = "lbl01MainMenu";
            this.lbl01MainMenu.Size = new System.Drawing.Size(233, 18);
            this.lbl01MainMenu.Text = "Assign NG  [Tampo]";
            // 
            // lblNG_QTY
            // 
            this.lblNG_QTY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNG_QTY.Location = new System.Drawing.Point(88, 190);
            this.lblNG_QTY.Name = "lblNG_QTY";
            this.lblNG_QTY.Size = new System.Drawing.Size(146, 17);
            this.lblNG_QTY.Text = "0";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(3, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 17);
            this.label11.Text = "ASG. NG :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt01JOB_NO
            // 
            this.txt01JOB_NO.AcceptsReturn = true;
            this.txt01JOB_NO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txt01JOB_NO.Location = new System.Drawing.Point(88, 35);
            this.txt01JOB_NO.Name = "txt01JOB_NO";
            this.txt01JOB_NO.Size = new System.Drawing.Size(148, 21);
            this.txt01JOB_NO.TabIndex = 185;
            this.txt01JOB_NO.Text = "MC0110-00001";
            this.txt01JOB_NO.GotFocus += new System.EventHandler(this.txt01JOB_NO_GotFocus);
            this.txt01JOB_NO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt01JOB_NO_KeyDown);
            this.txt01JOB_NO.LostFocus += new System.EventHandler(this.txt01JOB_NO_LostFocus);
            // 
            // lbl01LabelLineOrderNo
            // 
            this.lbl01LabelLineOrderNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbl01LabelLineOrderNo.Location = new System.Drawing.Point(3, 37);
            this.lbl01LabelLineOrderNo.Name = "lbl01LabelLineOrderNo";
            this.lbl01LabelLineOrderNo.Size = new System.Drawing.Size(84, 17);
            this.lbl01LabelLineOrderNo.Text = "Job Order #";
            // 
            // lblQty
            // 
            this.lblQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblQty.Location = new System.Drawing.Point(3, 220);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(83, 17);
            this.lblQty.Text = "Qty #";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtQTY
            // 
            this.txtQTY.AcceptsReturn = true;
            this.txtQTY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtQTY.Location = new System.Drawing.Point(88, 218);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(52, 21);
            this.txtQTY.TabIndex = 205;
            this.txtQTY.Text = "0";
            this.txtQTY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQTY_KeyDown);
            this.txtQTY.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQTY_KeyUp);
            this.txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            // 
            // lblUNIT_ID
            // 
            this.lblUNIT_ID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUNIT_ID.Location = new System.Drawing.Point(141, 220);
            this.lblUNIT_ID.Name = "lblUNIT_ID";
            this.lblUNIT_ID.Size = new System.Drawing.Size(62, 17);
            this.lblUNIT_ID.Text = "Pcs.";
            // 
            // btn01Cancel
            // 
            this.btn01Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn01Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btn01Cancel.Location = new System.Drawing.Point(125, 260);
            this.btn01Cancel.Name = "btn01Cancel";
            this.btn01Cancel.Size = new System.Drawing.Size(68, 27);
            this.btn01Cancel.TabIndex = 225;
            this.btn01Cancel.Text = "Cancel";
            // 
            // btn01Save
            // 
            this.btn01Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btn01Save.Location = new System.Drawing.Point(46, 260);
            this.btn01Save.Name = "btn01Save";
            this.btn01Save.Size = new System.Drawing.Size(68, 27);
            this.btn01Save.TabIndex = 224;
            this.btn01Save.Text = "OK";
            this.btn01Save.Click += new System.EventHandler(this.btn01Save_Click);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(149, 289);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 14);
            this.label13.Text = "Esc.";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(64, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 14);
            this.label14.Text = "Enter.";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.Text = "Job Lot :";
            // 
            // cmbJOB_LOT_LIST
            // 
            this.cmbJOB_LOT_LIST.Location = new System.Drawing.Point(88, 58);
            this.cmbJOB_LOT_LIST.Name = "cmbJOB_LOT_LIST";
            this.cmbJOB_LOT_LIST.Size = new System.Drawing.Size(147, 22);
            this.cmbJOB_LOT_LIST.TabIndex = 250;
            this.cmbJOB_LOT_LIST.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbJOB_LOT_LIST_KeyUp);
            this.cmbJOB_LOT_LIST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbJOB_LOT_LIST_KeyDown);
            // 
            // lblJOB_LOT
            // 
            this.lblJOB_LOT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblJOB_LOT.Location = new System.Drawing.Point(90, 61);
            this.lblJOB_LOT.Name = "lblJOB_LOT";
            this.lblJOB_LOT.Size = new System.Drawing.Size(131, 17);
            this.lblJOB_LOT.Text = "20100201-1-1";
            // 
            // lblPRODUCT_NO
            // 
            this.lblPRODUCT_NO.Location = new System.Drawing.Point(89, 105);
            this.lblPRODUCT_NO.Name = "lblPRODUCT_NO";
            this.lblPRODUCT_NO.Size = new System.Drawing.Size(146, 17);
            this.lblPRODUCT_NO.Text = "TERMINAL BLOCK";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(3, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 17);
            this.label19.Text = "Part #";
            // 
            // lblMTL_TYPE
            // 
            this.lblMTL_TYPE.Location = new System.Drawing.Point(89, 148);
            this.lblMTL_TYPE.Name = "lblMTL_TYPE";
            this.lblMTL_TYPE.Size = new System.Drawing.Size(146, 16);
            this.lblMTL_TYPE.Text = "Toray Amilan CM3001";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(3, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.Text = "MTL Grade :";
            // 
            // lblPRODUCT_NAME
            // 
            this.lblPRODUCT_NAME.Location = new System.Drawing.Point(89, 126);
            this.lblPRODUCT_NAME.Name = "lblPRODUCT_NAME";
            this.lblPRODUCT_NAME.Size = new System.Drawing.Size(146, 17);
            this.lblPRODUCT_NAME.Text = "TERMINAL BLOCK";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(3, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.Text = "Part Name :";
            // 
            // frmAssignNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(238, 317);
            this.ControlBox = false;
            this.Controls.Add(this.lblPRODUCT_NO);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblMTL_TYPE);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPRODUCT_NAME);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblJOB_LOT);
            this.Controls.Add(this.cmbJOB_LOT_LIST);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn01Cancel);
            this.Controls.Add(this.btn01Save);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblUNIT_ID);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblNG_QTY);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt01JOB_NO);
            this.Controls.Add(this.lbl01LabelLineOrderNo);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssignNG";
            this.Load += new System.EventHandler(this.frmAssignNG_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl01MainMenu;
        private System.Windows.Forms.Label lblNG_QTY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt01JOB_NO;
        private System.Windows.Forms.Label lbl01LabelLineOrderNo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.Label lblUNIT_ID;
        private System.Windows.Forms.Button btn01Cancel;
        private System.Windows.Forms.Button btn01Save;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbJOB_LOT_LIST;
        private System.Windows.Forms.Label lblJOB_LOT;
        private System.Windows.Forms.Label lblPRODUCT_NO;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblMTL_TYPE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPRODUCT_NAME;
        private System.Windows.Forms.Label label6;
    }
}