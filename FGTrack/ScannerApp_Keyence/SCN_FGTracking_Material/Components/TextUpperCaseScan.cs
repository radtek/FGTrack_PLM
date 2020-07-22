using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace HTN.BITS.FGTRACK.MATERIAL.Components
{
    public partial class TextUpperCaseScan : TextBox
    {
        public TextUpperCaseScan()
        {
            InitializeComponent();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.ReadOnly)
            {
                e.Handled = false;
                return;
            }

            if (Char.IsLetter(e.KeyChar))
            {
                int pos = this.SelectionStart;
                this.Text = this.Text.Insert(this.SelectionStart, Char.ToUpper(e.KeyChar).ToString());

                this.SelectionStart = pos + 1;
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.ReadOnly = true;
            base.OnLostFocus(e);
        }
    }
}
