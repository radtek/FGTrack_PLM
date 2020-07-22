using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using HTN.BITS.BLL.PLASESS;
using System.Text.RegularExpressions;

namespace HTN.BITS.UIL.PLASESS.Reports
{
    public partial class RPT_MATERIAL_CARD_LABEL_PRESS : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_MATERIAL_CARD_LABEL_PRESS()
        {
            InitializeComponent();
        }

        

        //private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //string srNo = DetailReport.GetCurrentColumnValue("SERIAL_NO").ToString();
        //    //imgBarcode.Image = UtilityBLL.QRCode_Encode(srNo);
        //}

    }
}
