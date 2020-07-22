using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPopTestReport : DevExpress.XtraEditors.XtraForm
    {
        public frmPopTestReport()
        {
            InitializeComponent();
        }

        private DataTable _tempTable;
        public DataTable tempTable
        {
            get
            {
                return _tempTable;
            }
            set
            {
                if (_tempTable == value)
                    return;

                _tempTable = value;
            }
        }

        private void frmPopTestReport_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = this._tempTable;
        }
    }
}