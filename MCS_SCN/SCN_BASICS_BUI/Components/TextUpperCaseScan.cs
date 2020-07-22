using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection;
using BarReader = HTN.BITS.MCS.SCN.LIB.Scanner.clsBarcodeReader;

namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public partial class TextUpperCaseScan : TextBox
    {
        public TextUpperCaseScan()
        {
            InitializeComponent();
            this.ReadOnly = true;
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
