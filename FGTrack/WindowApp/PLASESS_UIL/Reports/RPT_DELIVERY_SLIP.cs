using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace HTN.BITS.UIL.PLASESS.Reports
{
    public partial class RPT_DELIVERY_SLIP : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_DELIVERY_SLIP()
        {
            InitializeComponent();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DataSet ds1 = this.DataSource as DataSet;
            //DataSet ds2 = this.DetailReport.DataSource as DataSet;
        }

    }
}
