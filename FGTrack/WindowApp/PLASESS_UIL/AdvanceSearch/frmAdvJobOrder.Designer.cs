namespace HTN.BITS.UIL.PLASESS.AdvanceSearch
{
    partial class frmAdvJobOrder
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtJobOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dteToDate = new DevExpress.XtraEditors.DateEdit();
            this.dteFromDate = new DevExpress.XtraEditors.DateEdit();
            this.luePRODUCTION_TYPE = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtJobOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePRODUCTION_TYPE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 17);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(59, 13);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "Job Order #";
            // 
            // txtJobOrderNo
            // 
            this.txtJobOrderNo.EnterMoveNextControl = true;
            this.txtJobOrderNo.Location = new System.Drawing.Point(120, 14);
            this.txtJobOrderNo.Name = "txtJobOrderNo";
            this.txtJobOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJobOrderNo.Size = new System.Drawing.Size(125, 20);
            this.txtJobOrderNo.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(202, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnApply
            // 
            this.btnApply.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApply.Appearance.Options.UseFont = true;
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.Location = new System.Drawing.Point(120, 106);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(232, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 28;
            this.labelControl2.Text = "To Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(95, 13);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "(PLAN)*  From Date";
            // 
            // dteToDate
            // 
            this.dteToDate.EditValue = null;
            this.dteToDate.EnterMoveNextControl = true;
            this.dteToDate.Location = new System.Drawing.Point(276, 65);
            this.dteToDate.Name = "dteToDate";
            this.dteToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteToDate.Properties.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.dteToDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteToDate.Properties.EditFormat.FormatString = "dd-MM-yyyy";
            this.dteToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteToDate.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.dteToDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteToDate.Size = new System.Drawing.Size(100, 20);
            this.dteToDate.TabIndex = 3;
            // 
            // dteFromDate
            // 
            this.dteFromDate.EditValue = null;
            this.dteFromDate.EnterMoveNextControl = true;
            this.dteFromDate.Location = new System.Drawing.Point(120, 65);
            this.dteFromDate.Name = "dteFromDate";
            this.dteFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFromDate.Properties.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.dteFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFromDate.Properties.EditFormat.FormatString = "dd-MM-yyyy";
            this.dteFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteFromDate.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.dteFromDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFromDate.Size = new System.Drawing.Size(100, 20);
            this.dteFromDate.TabIndex = 2;
            // 
            // luePRODUCTION_TYPE
            // 
            this.luePRODUCTION_TYPE.EnterMoveNextControl = true;
            this.luePRODUCTION_TYPE.Location = new System.Drawing.Point(120, 40);
            this.luePRODUCTION_TYPE.Name = "luePRODUCTION_TYPE";
            this.luePRODUCTION_TYPE.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.luePRODUCTION_TYPE.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.luePRODUCTION_TYPE.Properties.AppearanceDropDownHeader.Options.UseTextOptions = true;
            this.luePRODUCTION_TYPE.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.luePRODUCTION_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePRODUCTION_TYPE.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SEQ_NO", "Code", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", 100, "Production Type")});
            this.luePRODUCTION_TYPE.Properties.DisplayMember = "NAME";
            this.luePRODUCTION_TYPE.Properties.DropDownRows = 4;
            this.luePRODUCTION_TYPE.Properties.NullText = "";
            this.luePRODUCTION_TYPE.Properties.ValueMember = "SEQ_NO";
            this.luePRODUCTION_TYPE.Size = new System.Drawing.Size(125, 20);
            this.luePRODUCTION_TYPE.TabIndex = 1;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(10, 43);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(78, 13);
            this.labelControl18.TabIndex = 65;
            this.labelControl18.Text = "Production Type";
            // 
            // frmAdvJobOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 142);
            this.Controls.Add(this.luePRODUCTION_TYPE);
            this.Controls.Add(this.labelControl18);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtJobOrderNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dteToDate);
            this.Controls.Add(this.dteFromDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdvJobOrder";
            this.Text = "Job Order (Advance Search)";
            this.Load += new System.EventHandler(this.frmAdvJobOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtJobOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePRODUCTION_TYPE.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtJobOrderNo;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dteToDate;
        private DevExpress.XtraEditors.DateEdit dteFromDate;
        private DevExpress.XtraEditors.LookUpEdit luePRODUCTION_TYPE;
        private DevExpress.XtraEditors.LabelControl labelControl18;
    }
}