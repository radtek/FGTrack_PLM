using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Winforms.Components;

namespace HTN.BITS.UIL.PLASESS.Component
{
    public partial class BaseDialogForm : BaseForm
    {
        public BaseDialogForm()
        {
            InitializeComponent();

            //this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            this.Visible = true;
            Application.DoEvents();
            base.OnShown(e);
        }

        public ApplicationIdle DialogIdle
        {
            get
            {
                return this.dialogIdle;
            }

            set
            {
                this.dialogIdle = value;
            }
        }

        private void BaseDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.dialogIdle.IsRunning)
                {
                    this.dialogIdle.Stop();
                }

                this.dialogIdle.Dispose();

                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}