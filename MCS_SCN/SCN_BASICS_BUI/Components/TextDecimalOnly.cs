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
    public partial class TextDecimalOnly : TextBox
    {
        public TextDecimalOnly()
        {
            InitializeComponent();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 8:
                    break;
                case 13:
                    break;
                case 27:
                    break;
                case 46: // .
                    break;
                default:
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                        e.Handled = true;
                    break;
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
