namespace HTN.BITS.MCS.SCN.UIL
{
    partial class frmReplenish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplenish));
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl01FormReplenish = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnFocusJobOrder = new System.Windows.Forms.Panel();
            this.txtJobNo = new HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan();
            this.lbl01JobOrder = new System.Windows.Forms.Label();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lbl01TotalQty = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lbl01Color = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblMtlCode = new System.Windows.Forms.Label();
            this.lblMtlName = new System.Windows.Forms.Label();
            this.lbl01MtlName = new System.Windows.Forms.Label();
            this.lblGrade = new System.Windows.Forms.Label();
            this.lbl01Grade = new System.Windows.Forms.Label();
            this.pnFocusReplenish = new System.Windows.Forms.Panel();
            this.lbl01ProductNo = new System.Windows.Forms.Label();
            this.lblProductNo = new System.Windows.Forms.Label();
            this.lbl01Qty = new System.Windows.Forms.Label();
            this.lbl01MtlCode = new System.Windows.Forms.Label();
            this.lblLabelScan = new System.Windows.Forms.Label();
            this.lbl01SerialNoScan = new System.Windows.Forms.Label();
            this.pnFocusMCNo = new System.Windows.Forms.Panel();
            this.txtNoOfLabel = new HTN.BITS.MCS.SCN.UIL.Components.TextNumberOnly();
            this.txtMCNo = new HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan();
            this.lbl01McNo = new System.Windows.Forms.Label();
            this.lbl01BagQty = new System.Windows.Forms.Label();
            this.pnFocusSerialNo = new System.Windows.Forms.Panel();
            this.lbl01SerialNo = new System.Windows.Forms.Label();
            this.txtSerialNo = new HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnFocusJobOrder.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.pnFocusReplenish.SuspendLayout();
            this.pnFocusMCNo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            this.pnlFooter.Controls.Add(this.panel1);
            this.pnlFooter.Controls.Add(this.lbl01FormReplenish);
            this.pnlFooter.Controls.Add(this.btnBack);
            this.pnlFooter.Location = new System.Drawing.Point(0, 285);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(240, 35);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(197, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 20);
            // 
            // lbl01FormReplenish
            // 
            this.lbl01FormReplenish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            this.lbl01FormReplenish.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lbl01FormReplenish.ForeColor = System.Drawing.Color.White;
            this.lbl01FormReplenish.Location = new System.Drawing.Point(10, 10);
            this.lbl01FormReplenish.Name = "lbl01FormReplenish";
            this.lbl01FormReplenish.Size = new System.Drawing.Size(181, 20);
            this.lbl01FormReplenish.Text = "REPLENISH";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(207, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 25);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Black;
            this.pnlHeader.Controls.Add(this.lblUserName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(240, 24);
            // 
            // lblUserName
            // 
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(85, 3);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(150, 18);
            this.lblUserName.Text = "User Name..";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnFocusJobOrder
            // 
            this.pnFocusJobOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnFocusJobOrder.BackColor = System.Drawing.Color.White;
            this.pnFocusJobOrder.Controls.Add(this.txtJobNo);
            this.pnFocusJobOrder.Controls.Add(this.lbl01JobOrder);
            this.pnFocusJobOrder.Location = new System.Drawing.Point(0, 24);
            this.pnFocusJobOrder.Name = "pnFocusJobOrder";
            this.pnFocusJobOrder.Size = new System.Drawing.Size(240, 28);
            // 
            // txtJobNo
            // 
            this.txtJobNo.Location = new System.Drawing.Point(96, 2);
            this.txtJobNo.MaxLength = 2;
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.ReadOnly = true;
            this.txtJobNo.Size = new System.Drawing.Size(141, 20);
            this.txtJobNo.TabIndex = 0;
            this.txtJobNo.GotFocus += new System.EventHandler(this.txtJobNo_GotFocus);
            this.txtJobNo.LostFocus += new System.EventHandler(this.txtJobNo_LostFocus);
            this.txtJobNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJobNo_KeyDown);
            this.txtJobNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtJobNo_KeyUp);
            // 
            // lbl01JobOrder
            // 
            this.lbl01JobOrder.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01JobOrder.Location = new System.Drawing.Point(4, 7);
            this.lbl01JobOrder.Name = "lbl01JobOrder";
            this.lbl01JobOrder.Size = new System.Drawing.Size(88, 18);
            this.lbl01JobOrder.Text = "JOB# :";
            this.lbl01JobOrder.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlTotal
            // 
            this.pnlTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.pnlTotal.Controls.Add(this.lbl01TotalQty);
            this.pnlTotal.Controls.Add(this.lblTotalQty);
            this.pnlTotal.Location = new System.Drawing.Point(0, 260);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(240, 27);
            // 
            // lbl01TotalQty
            // 
            this.lbl01TotalQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbl01TotalQty.Location = new System.Drawing.Point(4, 6);
            this.lbl01TotalQty.Name = "lbl01TotalQty";
            this.lbl01TotalQty.Size = new System.Drawing.Size(88, 16);
            this.lbl01TotalQty.Text = "Total QTY :";
            this.lbl01TotalQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalQty.Location = new System.Drawing.Point(95, 6);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(137, 16);
            this.lblTotalQty.Text = "0";
            // 
            // lbl01Color
            // 
            this.lbl01Color.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01Color.Location = new System.Drawing.Point(-1, 217);
            this.lbl01Color.Name = "lbl01Color";
            this.lbl01Color.Size = new System.Drawing.Size(93, 16);
            this.lbl01Color.Text = "Color :";
            this.lbl01Color.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblColor
            // 
            this.lblColor.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColor.Location = new System.Drawing.Point(96, 217);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(141, 16);
            this.lblColor.Text = "RENAK PS-1300M";
            // 
            // lblQty
            // 
            this.lblQty.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblQty.Location = new System.Drawing.Point(96, 239);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(141, 16);
            this.lblQty.Text = "RENAK PS-1300M";
            // 
            // lblMtlCode
            // 
            this.lblMtlCode.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMtlCode.Location = new System.Drawing.Point(96, 154);
            this.lblMtlCode.Name = "lblMtlCode";
            this.lblMtlCode.Size = new System.Drawing.Size(141, 16);
            // 
            // lblMtlName
            // 
            this.lblMtlName.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMtlName.Location = new System.Drawing.Point(96, 174);
            this.lblMtlName.Name = "lblMtlName";
            this.lblMtlName.Size = new System.Drawing.Size(141, 16);
            this.lblMtlName.Text = "655010";
            // 
            // lbl01MtlName
            // 
            this.lbl01MtlName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01MtlName.Location = new System.Drawing.Point(4, 174);
            this.lbl01MtlName.Name = "lbl01MtlName";
            this.lbl01MtlName.Size = new System.Drawing.Size(88, 16);
            this.lbl01MtlName.Text = "MTL Name :";
            this.lbl01MtlName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblGrade
            // 
            this.lblGrade.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGrade.Location = new System.Drawing.Point(96, 195);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(141, 16);
            this.lblGrade.Text = "SPUNBOND NONWOVEN 2017-2S 309MM";
            // 
            // lbl01Grade
            // 
            this.lbl01Grade.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01Grade.Location = new System.Drawing.Point(4, 195);
            this.lbl01Grade.Name = "lbl01Grade";
            this.lbl01Grade.Size = new System.Drawing.Size(88, 16);
            this.lbl01Grade.Text = "Grade :";
            this.lbl01Grade.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnFocusReplenish
            // 
            this.pnFocusReplenish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnFocusReplenish.BackColor = System.Drawing.Color.White;
            this.pnFocusReplenish.Controls.Add(this.lbl01ProductNo);
            this.pnFocusReplenish.Controls.Add(this.lblProductNo);
            this.pnFocusReplenish.Location = new System.Drawing.Point(0, 78);
            this.pnFocusReplenish.Name = "pnFocusReplenish";
            this.pnFocusReplenish.Size = new System.Drawing.Size(240, 28);
            // 
            // lbl01ProductNo
            // 
            this.lbl01ProductNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01ProductNo.Location = new System.Drawing.Point(4, 5);
            this.lbl01ProductNo.Name = "lbl01ProductNo";
            this.lbl01ProductNo.Size = new System.Drawing.Size(88, 18);
            this.lbl01ProductNo.Text = "Product# :";
            this.lbl01ProductNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblProductNo
            // 
            this.lblProductNo.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblProductNo.Location = new System.Drawing.Point(94, 7);
            this.lblProductNo.Name = "lblProductNo";
            this.lblProductNo.Size = new System.Drawing.Size(141, 16);
            // 
            // lbl01Qty
            // 
            this.lbl01Qty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01Qty.Location = new System.Drawing.Point(-1, 239);
            this.lbl01Qty.Name = "lbl01Qty";
            this.lbl01Qty.Size = new System.Drawing.Size(93, 16);
            this.lbl01Qty.Text = "Qty :";
            this.lbl01Qty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl01MtlCode
            // 
            this.lbl01MtlCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01MtlCode.Location = new System.Drawing.Point(4, 154);
            this.lbl01MtlCode.Name = "lbl01MtlCode";
            this.lbl01MtlCode.Size = new System.Drawing.Size(88, 16);
            this.lbl01MtlCode.Text = "MTL Code :";
            this.lbl01MtlCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLabelScan
            // 
            this.lblLabelScan.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLabelScan.Location = new System.Drawing.Point(96, 133);
            this.lblLabelScan.Name = "lblLabelScan";
            this.lblLabelScan.Size = new System.Drawing.Size(141, 16);
            // 
            // lbl01SerialNoScan
            // 
            this.lbl01SerialNoScan.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01SerialNoScan.Location = new System.Drawing.Point(4, 133);
            this.lbl01SerialNoScan.Name = "lbl01SerialNoScan";
            this.lbl01SerialNoScan.Size = new System.Drawing.Size(88, 16);
            this.lbl01SerialNoScan.Text = "Serial# :";
            this.lbl01SerialNoScan.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnFocusMCNo
            // 
            this.pnFocusMCNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnFocusMCNo.BackColor = System.Drawing.Color.White;
            this.pnFocusMCNo.Controls.Add(this.txtNoOfLabel);
            this.pnFocusMCNo.Controls.Add(this.txtMCNo);
            this.pnFocusMCNo.Controls.Add(this.lbl01McNo);
            this.pnFocusMCNo.Controls.Add(this.lbl01BagQty);
            this.pnFocusMCNo.Location = new System.Drawing.Point(0, 51);
            this.pnFocusMCNo.Name = "pnFocusMCNo";
            this.pnFocusMCNo.Size = new System.Drawing.Size(240, 28);
            // 
            // txtNoOfLabel
            // 
            this.txtNoOfLabel.Location = new System.Drawing.Point(207, 3);
            this.txtNoOfLabel.MaxLength = 2;
            this.txtNoOfLabel.Name = "txtNoOfLabel";
            this.txtNoOfLabel.Size = new System.Drawing.Size(30, 20);
            this.txtNoOfLabel.TabIndex = 2;
            this.txtNoOfLabel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfLabel_KeyDown);
            // 
            // txtMCNo
            // 
            this.txtMCNo.Location = new System.Drawing.Point(96, 3);
            this.txtMCNo.Name = "txtMCNo";
            this.txtMCNo.ReadOnly = true;
            this.txtMCNo.Size = new System.Drawing.Size(53, 20);
            this.txtMCNo.TabIndex = 1;
            this.txtMCNo.GotFocus += new System.EventHandler(this.txtMCNo_GotFocus);
            this.txtMCNo.LostFocus += new System.EventHandler(this.txtMCNo_LostFocus);
            this.txtMCNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMCNo_KeyDown);
            // 
            // lbl01McNo
            // 
            this.lbl01McNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01McNo.Location = new System.Drawing.Point(4, 7);
            this.lbl01McNo.Name = "lbl01McNo";
            this.lbl01McNo.Size = new System.Drawing.Size(88, 18);
            this.lbl01McNo.Text = "MC# :";
            this.lbl01McNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl01BagQty
            // 
            this.lbl01BagQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01BagQty.Location = new System.Drawing.Point(150, 6);
            this.lbl01BagQty.Name = "lbl01BagQty";
            this.lbl01BagQty.Size = new System.Drawing.Size(55, 18);
            this.lbl01BagQty.Text = "Bag qty :";
            this.lbl01BagQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnFocusSerialNo
            // 
            this.pnFocusSerialNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnFocusSerialNo.BackColor = System.Drawing.Color.White;
            this.pnFocusSerialNo.Location = new System.Drawing.Point(0, 103);
            this.pnFocusSerialNo.Name = "pnFocusSerialNo";
            this.pnFocusSerialNo.Size = new System.Drawing.Size(240, 28);
            // 
            // lbl01SerialNo
            // 
            this.lbl01SerialNo.BackColor = System.Drawing.Color.Transparent;
            this.lbl01SerialNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbl01SerialNo.Location = new System.Drawing.Point(3, 109);
            this.lbl01SerialNo.Name = "lbl01SerialNo";
            this.lbl01SerialNo.Size = new System.Drawing.Size(88, 18);
            this.lbl01SerialNo.Text = "Serial# :";
            this.lbl01SerialNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(95, 107);
            this.txtSerialNo.MaxLength = 2;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.ReadOnly = true;
            this.txtSerialNo.Size = new System.Drawing.Size(141, 20);
            this.txtSerialNo.TabIndex = 3;
            this.txtSerialNo.GotFocus += new System.EventHandler(this.txtSerialNo_GotFocus);
            this.txtSerialNo.LostFocus += new System.EventHandler(this.txtSerialNo_LostFocus);
            this.txtSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNo_KeyDown);
            // 
            // frmReplenish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.lbl01SerialNo);
            this.Controls.Add(this.pnFocusSerialNo);
            this.Controls.Add(this.pnFocusMCNo);
            this.Controls.Add(this.lblLabelScan);
            this.Controls.Add(this.lbl01SerialNoScan);
            this.Controls.Add(this.pnFocusReplenish);
            this.Controls.Add(this.lbl01Color);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lbl01Qty);
            this.Controls.Add(this.lblMtlCode);
            this.Controls.Add(this.lbl01MtlCode);
            this.Controls.Add(this.lblMtlName);
            this.Controls.Add(this.lbl01MtlName);
            this.Controls.Add(this.lblGrade);
            this.Controls.Add(this.lbl01Grade);
            this.Controls.Add(this.pnlTotal);
            this.Controls.Add(this.pnFocusJobOrder);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmReplenish";
            this.Text = "frmRelocation";
            this.Load += new System.EventHandler(this.frmReplenish_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmReplenish_Closing);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnFocusJobOrder.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.pnFocusReplenish.ResumeLayout(false);
            this.pnFocusMCNo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl01FormReplenish;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel pnFocusJobOrder;
        private HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan txtJobNo;
        private System.Windows.Forms.Label lbl01JobOrder;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lbl01TotalQty;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lbl01Color;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblMtlCode;
        private System.Windows.Forms.Label lblMtlName;
        private System.Windows.Forms.Label lbl01MtlName;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.Label lbl01Grade;
        private System.Windows.Forms.Panel pnFocusReplenish;
        private System.Windows.Forms.Label lbl01ProductNo;
        private System.Windows.Forms.Label lbl01Qty;
        private System.Windows.Forms.Label lbl01MtlCode;
        private System.Windows.Forms.Label lblLabelScan;
        private System.Windows.Forms.Label lbl01SerialNoScan;
        private System.Windows.Forms.Panel pnFocusMCNo;
        public HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan txtMCNo;
        private System.Windows.Forms.Label lbl01McNo;
        private System.Windows.Forms.Panel pnFocusSerialNo;
        public HTN.BITS.MCS.SCN.UIL.Components.TextNumberOnly txtNoOfLabel;
        private System.Windows.Forms.Label lblProductNo;
        private System.Windows.Forms.Label lbl01BagQty;
        private HTN.BITS.MCS.SCN.UIL.Components.TextUpperCaseScan txtSerialNo;
        private System.Windows.Forms.Label lbl01SerialNo;
    }
}