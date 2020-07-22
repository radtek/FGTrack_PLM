namespace HTN.BITS.UIL.PLASESS.AdvanceSearch
{
    partial class frmAdvQCReturn
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lueWarehouse = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtRT_NO = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dteToDate = new DevExpress.XtraEditors.DateEdit();
            this.dteFromDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lueWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRT_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(21, 13);
            this.labelControl3.TabIndex = 86;
            this.labelControl3.Text = "W/H";
            // 
            // lueWarehouse
            // 
            this.lueWarehouse.EnterMoveNextControl = true;
            this.lueWarehouse.Location = new System.Drawing.Point(122, 12);
            this.lueWarehouse.Name = "lueWarehouse";
            this.lueWarehouse.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lueWarehouse.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueWarehouse.Properties.AppearanceDropDownHeader.Options.UseTextOptions = true;
            this.lueWarehouse.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lueWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueWarehouse.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SEQ_NO", "Code", 25, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", 40, "NAME")});
            this.lueWarehouse.Properties.DisplayMember = "NAME";
            this.lueWarehouse.Properties.DropDownRows = 4;
            this.lueWarehouse.Properties.NullText = "";
            this.lueWarehouse.Properties.ValueMember = "SEQ_NO";
            this.lueWarehouse.Size = new System.Drawing.Size(114, 20);
            this.lueWarehouse.TabIndex = 77;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(66, 13);
            this.labelControl5.TabIndex = 85;
            this.labelControl5.Text = "QC. Return #";
            // 
            // txtRT_NO
            // 
            this.txtRT_NO.EnterMoveNextControl = true;
            this.txtRT_NO.Location = new System.Drawing.Point(122, 38);
            this.txtRT_NO.Name = "txtRT_NO";
            this.txtRT_NO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRT_NO.Size = new System.Drawing.Size(114, 20);
            this.txtRT_NO.TabIndex = 78;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 82;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnApply
            // 
            this.btnApply.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApply.Appearance.Options.UseFont = true;
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.Location = new System.Drawing.Point(122, 104);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 81;
            this.btnApply.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(234, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 84;
            this.labelControl2.Text = "To Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 13);
            this.labelControl1.TabIndex = 83;
            this.labelControl1.Text = "Return  From Date";
            // 
            // dteToDate
            // 
            this.dteToDate.EditValue = null;
            this.dteToDate.EnterMoveNextControl = true;
            this.dteToDate.Location = new System.Drawing.Point(278, 63);
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
            this.dteToDate.TabIndex = 80;
            // 
            // dteFromDate
            // 
            this.dteFromDate.EditValue = null;
            this.dteFromDate.EnterMoveNextControl = true;
            this.dteFromDate.Location = new System.Drawing.Point(122, 63);
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
            this.dteFromDate.TabIndex = 79;
            // 
            // frmAdvQCReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 139);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lueWarehouse);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtRT_NO);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dteToDate);
            this.Controls.Add(this.dteFromDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdvQCReturn";
            this.Text = "QC. Return (Advance Search)";
            this.Load += new System.EventHandler(this.frmAdvQCReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRT_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lueWarehouse;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtRT_NO;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dteToDate;
        private DevExpress.XtraEditors.DateEdit dteFromDate;
    }
}