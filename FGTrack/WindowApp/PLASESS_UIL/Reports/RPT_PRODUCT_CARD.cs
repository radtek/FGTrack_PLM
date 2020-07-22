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
    public partial class RPT_PRODUCT_CARD : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_PRODUCT_CARD()
        {
            InitializeComponent();
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.paramPRODUCTION_TYPE.Value.Equals("P"))
            {
                xrTableCell1.Text = "PRESS";
                xrTableCell5.Text = @"100%";
            }
            else
            {
                xrTableCell1.Text = "INJ";
                xrTableCell5.Text = "ELC";
            }
            
            string processArr = this.GetCurrentColumnValue("PROCESS_SEQ").ToString();

            string[] lastViewArr = Regex.Split(processArr, @",");
            if (lastViewArr.Length == 5)
            {
                if (lastViewArr[0] == "N")
                {
                    cellINJ.BackColor = Color.Black;
                }

                if (lastViewArr[1] == "N")
                {
                    cellTRM.BackColor = Color.Black;
                }

                if (lastViewArr[2] == "N")
                {
                    cellELC.BackColor = Color.Black;
                }

                if (lastViewArr[3] == "N")
                {
                    cellPRE.BackColor = Color.Black;
                }

                if (lastViewArr[4] == "N")
                {
                    cellQC.BackColor = Color.Black;
                }
            }

            bool showPageInfo = false;

            if (DetailReport.CurrentRowIndex == 0)
            {
                showPageInfo = true;
            }
            else if ((DetailReport.CurrentRowIndex % 10) == 0)
            {
                showPageInfo = true;
            }
            else
            {
                showPageInfo = false;
            }

            if (showPageInfo)
            {
                this.xpiPageInfo.Visible = true;
                this.xpiPageInfo.LocationF = new PointF(11F, 539.85F);
            }
            else
                this.xpiPageInfo.Visible = false;

            //string srNo = DetailReport.GetCurrentColumnValue("SERIAL_NO").ToString();
            //imgBarcode.Image = UtilityBLL.QRCode_Encode(srNo);
        }

        //private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //string srNo = DetailReport.GetCurrentColumnValue("SERIAL_NO").ToString();
        //    //imgBarcode.Image = UtilityBLL.QRCode_Encode(srNo);
        //}

    }
}
