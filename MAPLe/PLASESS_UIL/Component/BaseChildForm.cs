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
    public partial class BaseChildForm : BaseForm
    {
        public BaseChildForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.WindowsDefaultLocation;
            MaximizeBox = false;
            MinimizeBox = false;
            
        }

        private bool isClosing;

        protected override CreateParams CreateParams
        {
            get
            {
                //return base.CreateParams;
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        private void DefaultPropertyForm()
        {
            base.Tag = "FullMode";
            this.Opacity = 100.0d;
            this.WindowState = FormWindowState.Maximized;
            base.PerformLayout();
            //base.UpdatePerformLayout();
        }



        protected override void OnLoad(EventArgs e)
        {
            
            this.isClosing = false;
            this.DefaultPropertyForm();
            //base.UpdatePerformLayout();
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

        protected override void OnClosing(CancelEventArgs e)
        {
            this.isClosing = true;
            DevExpress.Skins.SkinManager.DisableMdiFormSkins();

            if (((frmMainMenu)this.ParentForm).LockMenu)
                ((frmMainMenu)this.ParentForm).LockMenu = false;

            base.OnClosing(e);
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    if (!this.isClosing)
        //    {
        //        base.UpdatePerformLayout();
        //    }

        //    base.OnPaint(e);
        //}

        //Not for get this
        //protected override void OnResize(EventArgs e)
        //{
        //    if (HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime)
        //    {
        //        if (!this.isClosing)
        //        {
        //            if (this.WindowState != FormWindowState.Maximized)
        //            {
        //                this.DefaultPropertyForm();
        //            }
        //        }
        //    }

        //    base.OnResize(e);
        //}
        protected override void OnResizeEnd(EventArgs e)
        {
            if (HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime)
            {
                if (!this.isClosing)
                {
                    if (this.WindowState != FormWindowState.Maximized)
                    {
                        this.DefaultPropertyForm();
                    }
                }
            }

            base.OnResizeEnd(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            if (HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime)
            {
                if (!this.isClosing)
                {
                    this.Opacity = 20.0d;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.Location = new Point(0, 0);
                    this.Width = ((frmMainMenu)this.ParentForm).ParentForm_Width;
                    this.Height = this.MdiParent.Height - UiUtility.MinusFormHeight;
                }
            }


            base.OnDeactivate(e);
        }

    }
}