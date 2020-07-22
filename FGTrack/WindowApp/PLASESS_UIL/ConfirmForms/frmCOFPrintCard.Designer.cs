namespace HTN.BITS.UIL.PLASESS.ConfirmForms
{
    partial class frmCOFPrintCard
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grdPrintCard = new DevExpress.XtraGrid.GridControl();
            this.grvPrintCard = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvPrintCard_col_SERIAL_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPrintCard_col_NO_OF_PRINT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPrintCard_col_PIC_LAST_PRINT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvPrintCard_col_PRINT_LAST_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblPrintCount = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrintCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPrintCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnSelect);
            this.panelControl2.Location = new System.Drawing.Point(2, 327);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(427, 33);
            this.panelControl2.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonCancel;
            this.btnCancel.Location = new System.Drawing.Point(347, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Appearance.Options.UseFont = true;
            this.btnSelect.Image = global::HTN.BITS.UIL.PLASESS.Properties.Resources.ButtonSelect;
            this.btnSelect.Location = new System.Drawing.Point(267, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "&Con&firm";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grdPrintCard
            // 
            this.grdPrintCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPrintCard.Location = new System.Drawing.Point(2, 51);
            this.grdPrintCard.MainView = this.grvPrintCard;
            this.grdPrintCard.Name = "grdPrintCard";
            this.grdPrintCard.Size = new System.Drawing.Size(427, 273);
            this.grdPrintCard.TabIndex = 7;
            this.grdPrintCard.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPrintCard});
            // 
            // grvPrintCard
            // 
            this.grvPrintCard.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPrintCard.Appearance.FocusedCell.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPrintCard.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.grvPrintCard.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grvPrintCard.Appearance.FocusedCell.Options.UseForeColor = true;
            this.grvPrintCard.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPrintCard.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPrintCard.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.grvPrintCard.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvPrintCard.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvPrintCard.Appearance.HideSelectionRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPrintCard.Appearance.HideSelectionRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPrintCard.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.grvPrintCard.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvPrintCard.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.grvPrintCard.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grvPrintCard.Appearance.SelectedRow.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grvPrintCard.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvPrintCard.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvPrintCard.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvPrintCard.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvPrintCard_col_SERIAL_NO,
            this.grvPrintCard_col_NO_OF_PRINT,
            this.grvPrintCard_col_PIC_LAST_PRINT,
            this.grvPrintCard_col_PRINT_LAST_DATE});
            this.grvPrintCard.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvPrintCard.GridControl = this.grdPrintCard;
            this.grvPrintCard.Name = "grvPrintCard";
            this.grvPrintCard.OptionsBehavior.Editable = false;
            this.grvPrintCard.OptionsView.ShowGroupPanel = false;
            // 
            // grvPrintCard_col_SERIAL_NO
            // 
            this.grvPrintCard_col_SERIAL_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_SERIAL_NO.AppearanceCell.Options.UseFont = true;
            this.grvPrintCard_col_SERIAL_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_SERIAL_NO.AppearanceHeader.Options.UseFont = true;
            this.grvPrintCard_col_SERIAL_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPrintCard_col_SERIAL_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_SERIAL_NO.Caption = "SERIAL #";
            this.grvPrintCard_col_SERIAL_NO.FieldName = "SERIAL_NO";
            this.grvPrintCard_col_SERIAL_NO.Name = "grvPrintCard_col_SERIAL_NO";
            this.grvPrintCard_col_SERIAL_NO.Visible = true;
            this.grvPrintCard_col_SERIAL_NO.VisibleIndex = 0;
            this.grvPrintCard_col_SERIAL_NO.Width = 110;
            // 
            // grvPrintCard_col_NO_OF_PRINT
            // 
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.Options.UseFont = true;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.Options.UseForeColor = true;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.Options.UseTextOptions = true;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceHeader.Options.UseFont = true;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPrintCard_col_NO_OF_PRINT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_NO_OF_PRINT.Caption = "Printed Times";
            this.grvPrintCard_col_NO_OF_PRINT.DisplayFormat.FormatString = "{0:#,##0}";
            this.grvPrintCard_col_NO_OF_PRINT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grvPrintCard_col_NO_OF_PRINT.FieldName = "NO_OF_PRINT";
            this.grvPrintCard_col_NO_OF_PRINT.Name = "grvPrintCard_col_NO_OF_PRINT";
            this.grvPrintCard_col_NO_OF_PRINT.Visible = true;
            this.grvPrintCard_col_NO_OF_PRINT.VisibleIndex = 1;
            this.grvPrintCard_col_NO_OF_PRINT.Width = 85;
            // 
            // grvPrintCard_col_PIC_LAST_PRINT
            // 
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceCell.Options.UseFont = true;
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceHeader.Options.UseFont = true;
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPrintCard_col_PIC_LAST_PRINT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_PIC_LAST_PRINT.Caption = "USER Last Print.";
            this.grvPrintCard_col_PIC_LAST_PRINT.FieldName = "PIC_LAST_PRINT";
            this.grvPrintCard_col_PIC_LAST_PRINT.Name = "grvPrintCard_col_PIC_LAST_PRINT";
            this.grvPrintCard_col_PIC_LAST_PRINT.Visible = true;
            this.grvPrintCard_col_PIC_LAST_PRINT.VisibleIndex = 2;
            this.grvPrintCard_col_PIC_LAST_PRINT.Width = 100;
            // 
            // grvPrintCard_col_PRINT_LAST_DATE
            // 
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceCell.Options.UseFont = true;
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceHeader.Options.UseFont = true;
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.grvPrintCard_col_PRINT_LAST_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvPrintCard_col_PRINT_LAST_DATE.Caption = "Last Print Date";
            this.grvPrintCard_col_PRINT_LAST_DATE.DisplayFormat.FormatString = "{0:dd-MM-yyyy HH:mm}";
            this.grvPrintCard_col_PRINT_LAST_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.grvPrintCard_col_PRINT_LAST_DATE.FieldName = "PRINT_LAST_DATE";
            this.grvPrintCard_col_PRINT_LAST_DATE.Name = "grvPrintCard_col_PRINT_LAST_DATE";
            this.grvPrintCard_col_PRINT_LAST_DATE.Visible = true;
            this.grvPrintCard_col_PRINT_LAST_DATE.VisibleIndex = 3;
            this.grvPrintCard_col_PRINT_LAST_DATE.Width = 114;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(64, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(333, 16);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Product Card Already Print !!, Please Confirm Again";
            // 
            // lblPrintCount
            // 
            this.lblPrintCount.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrintCount.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblPrintCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPrintCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPrintCount.Location = new System.Drawing.Point(3, 11);
            this.lblPrintCount.Name = "lblPrintCount";
            this.lblPrintCount.Padding = new System.Windows.Forms.Padding(2);
            this.lblPrintCount.Size = new System.Drawing.Size(50, 21);
            this.lblPrintCount.TabIndex = 10;
            this.lblPrintCount.Text = "2";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lblPrintCount);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(428, 43);
            this.panelControl1.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(64, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(316, 16);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Product Card ทำการพิมพ์ไปแล้ว, กรุณายืนยันอีกครั้ง";
            // 
            // frmCOFPrintCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 362);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdPrintCard);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCOFPrintCard";
            this.ShowInTaskbar = false;
            this.Text = "PRINTED PRODUCT CARD";
            this.Load += new System.EventHandler(this.frmCOFPrintCard_Load);
            this.LoadCompleted += new HTN.BITS.UIL.PLASESS.Component.BaseForm.LoadCompletedEventHandler(this.frmCOFPrintCard_LoadCompleted);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCOFPrintCard_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPrintCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPrintCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grdPrintCard;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPrintCard;
        private DevExpress.XtraGrid.Columns.GridColumn grvPrintCard_col_SERIAL_NO;
        private DevExpress.XtraGrid.Columns.GridColumn grvPrintCard_col_NO_OF_PRINT;
        private DevExpress.XtraGrid.Columns.GridColumn grvPrintCard_col_PIC_LAST_PRINT;
        private DevExpress.XtraGrid.Columns.GridColumn grvPrintCard_col_PRINT_LAST_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblPrintCount;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}