using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HTN.BITS.UIL.PLASESS.Reports
{
    public partial class RPT_FG_RETURN_WH : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_FG_RETURN_WH()
        {
            InitializeComponent();
        }

        //private void SuppressObjectData(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
        //    try
        //    {

        //        if (browser.Current == null)
        //        {
        //            e.Cancel = true;
        //            return;
        //        }

        //        if (browser.Position == 0)
        //        {
        //            return;
        //        }

        //        browser.Position--;
        //        //object groupCUSTOMERprev = browser.FindItemProperty("CUSTOMER", true).GetValue(browser.Current);
        //        object groupPRODUCT_NOprev = browser.FindItemProperty("PRODUCT_NO", true).GetValue(browser.Current);
                

        //        browser.Position++;
        //        //object groupCUSTOMERcurr = browser.FindItemProperty("CUSTOMER", true).GetValue(browser.Current);
        //        object groupPRODUCT_NOcurr = browser.FindItemProperty("PRODUCT_NO", true).GetValue(browser.Current);

        //        //if (groupCUSTOMERprev.ToString() == groupCUSTOMERcurr.ToString())
        //        //{
        //            if (groupPRODUCT_NOprev.ToString() == groupPRODUCT_NOcurr.ToString())
        //            {
        //                e.Cancel = true;
        //            }
        //            else
        //            {
        //                e.Cancel = false;
        //            }
        //        //}
        //        //else
        //        //{
        //        //    e.Cancel = false;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private int CUSTOMERpageidx = -1;
        private int ProductNopageidx = -1;
        private int ProductNamepageidx = -1;


        private void lblCUSTOMER_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
                if (browser.HasLastPosition)
                {
                    browser.Position = -1;
                }
                else
                {
                    browser.Position++;
                }

                if (e.PageIndex != CUSTOMERpageidx)
                {
                    CUSTOMERpageidx = e.PageIndex;
                    e.Cancel = false;
                }
                else
                {
                    object groupCUSTOMERprev = this.GetPreviousColumnValue("CUSTOMER");
                    object groupPRODUCT_NOprev = this.GetPreviousColumnValue("PRODUCT_NO");

                    object groupCUSTOMERcurr = this.GetCurrentColumnValue("CUSTOMER");
                    object groupPRODUCT_NOcurr = this.GetCurrentColumnValue("PRODUCT_NO");


                    if (groupCUSTOMERprev.ToString() == groupCUSTOMERcurr.ToString())
                    {
                        if (groupPRODUCT_NOprev.ToString() == groupPRODUCT_NOcurr.ToString())
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void lblProductNo_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                if (e.PageIndex != ProductNopageidx)
                {
                    ProductNopageidx = e.PageIndex;
                    e.Cancel = false;
                }
                else
                {
                    object groupCUSTOMERprev = this.GetPreviousColumnValue("CUSTOMER");
                    object groupPRODUCT_NOprev = this.GetPreviousColumnValue("PRODUCT_NO");

                    object groupCUSTOMERcurr = this.GetCurrentColumnValue("CUSTOMER");
                    object groupPRODUCT_NOcurr = this.GetCurrentColumnValue("PRODUCT_NO");


                    if (groupCUSTOMERprev.ToString() == groupCUSTOMERcurr.ToString())
                    {
                        if (groupPRODUCT_NOprev.ToString() == groupPRODUCT_NOcurr.ToString())
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void lblProductName_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                if (e.PageIndex != ProductNamepageidx)
                {
                    ProductNamepageidx = e.PageIndex;
                    e.Cancel = false;
                }
                else
                {
                    object groupCUSTOMERprev = this.GetPreviousColumnValue("CUSTOMER");
                    object groupPRODUCT_NOprev = this.GetPreviousColumnValue("PRODUCT_NO");

                    object groupCUSTOMERcurr = this.GetCurrentColumnValue("CUSTOMER");
                    object groupPRODUCT_NOcurr = this.GetCurrentColumnValue("PRODUCT_NO");


                    if (groupCUSTOMERprev.ToString() == groupCUSTOMERcurr.ToString())
                    {
                        if (groupPRODUCT_NOprev.ToString() == groupPRODUCT_NOcurr.ToString())
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
            try
            {

                if (browser.Current == null)
                {
                    e.Cancel = true;
                    return;
                }

                if (browser.HasLastPosition)
                {
                    xtbLineFooter.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
