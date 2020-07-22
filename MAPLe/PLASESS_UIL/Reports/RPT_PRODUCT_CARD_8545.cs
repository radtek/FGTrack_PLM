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
    public partial class RPT_PRODUCT_CARD_8545 : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_PRODUCT_CARD_8545()
        {
            InitializeComponent();
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string strN_SQUARE = DetailReport.GetCurrentColumnValue("N_SQUARE").ToString();
            string strN_TRIANGLE = DetailReport.GetCurrentColumnValue("N_TRIANGLE").ToString();

            //N_SQUARE
            if (!string.IsNullOrEmpty(strN_SQUARE))
            {
                xrVN_SQUARE.Text = strN_SQUARE;

                xrSN_SQUARE.Visible = true;
                xrVN_SQUARE.Visible = true;

            }
            else
            {
                xrSN_SQUARE.Visible = false;
                xrVN_SQUARE.Visible = false;
            }

            //N_TRIANGLE
            if (!string.IsNullOrEmpty(strN_TRIANGLE))
            {
                xrVN_TRIANGLE.Text = strN_TRIANGLE;

                xrSN_TRIANGLE.Visible = true;
                xrVN_TRIANGLE.Visible = true;

            }
            else
            {
                xrSN_TRIANGLE.Visible = false;
                xrVN_TRIANGLE.Visible = false;
            }

            //if (this.paramPRODUCTION_TYPE.Value.Equals("P"))
            //{
            //    xrTableCell1.Text = "PRESS";
            //    xrTableCell5.Text = @"100%";
            //}
            //else
            //{
            //    xrTableCell1.Text = "INJ";
            //    xrTableCell5.Text = "ELC";
            //}
            
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

            //bool showPageInfo = false;

            //if (DetailReport.CurrentRowIndex == 0)
            //{
            //    showPageInfo = true;
            //}
            //else if ((DetailReport.CurrentRowIndex % 10) == 0)
            //{
            //    showPageInfo = true;
            //}
            //else
            //{
            //    showPageInfo = false;
            //}


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
