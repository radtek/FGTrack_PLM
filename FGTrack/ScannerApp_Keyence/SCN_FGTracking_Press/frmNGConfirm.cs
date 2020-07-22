using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;

namespace HTN.BITS.FGTRACK.PRESS
{
    public partial class frmNGConfirm : BaseFormFullMode, IDisposable
    {
        public frmNGConfirm()
        {
            InitializeComponent();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}