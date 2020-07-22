using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HTN.BITS.UIL.PLASESS.Component
{
    public partial class BaseChildFormPopup : BaseForm
    {
        public BaseChildFormPopup()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            InitializeComponent();
        }

        private void DefaultPropertyForm()
        {
            base.Tag = "NormalMode";
            //this.ControlBox = false;
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;

            //this.MinimizeBox = false;
            //this.MaximizeBox = false;

            //this.ControlBox = false;

            

            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            //base.UpdatePerformLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.DefaultPropertyForm();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            this.Visible = true;
            base.PerformLayout();
            Application.DoEvents();
            base.OnShown(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            this.DefaultPropertyForm();
            base.OnActivated(e);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.UpdatePerformLayout();
        //    base.OnPaint(e);
        //}

        protected override void OnClosed(EventArgs e)
        {
            DevExpress.Skins.SkinManager.DisableMdiFormSkins();
            if (((frmMainMenu)this.ParentForm).LockMenu)
                ((frmMainMenu)this.ParentForm).LockMenu = false;
            base.OnClosed(e);
        }
    }
}