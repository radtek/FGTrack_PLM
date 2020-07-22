using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using HTN.BITS.BLL.PLASESS;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component
{

    public partial class ReportViewer : BaseForm
    {
        private bool logPrintTime = false;
        private bool autoCloseAfterPrint = true; //default
        private int seqNo;
        private XtraReport repotPrint;

        public ReportViewer()
        {
            InitializeComponent();
            //set idle time
            if (UiUtility.IsAppIdleTime)
            {
                this.rptViewerIdle.IdleTime = System.TimeSpan.Parse(UiUtility.RPTViewerIdleTime);
                this.rptViewerIdle.IdleAsync += new EventHandler(this.ReportViewerIdle_IdleAsync);

                if (!this.rptViewerIdle.IsRunning)
                {
                    this.rptViewerIdle.Start();
                }
            }
            else
            {
                this.rptViewerIdle.Stop();
            }
        }

        ~ReportViewer()
        {
            if (UiUtility.IsAppIdleTime)
            {
                this.rptViewerIdle.IdleAsync -= new EventHandler(this.ReportViewerIdle_IdleAsync);
            }
        }

        private void ReportViewerIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.ReportViewerIdle_Idle(sender, e); })
                );
        }

        private void ReportViewerIdle_Idle(object sender, EventArgs e)
        {
            if (this.rptViewerIdle.IsRunning)
            {
                this.rptViewerIdle.Stop();
            }

            this.rptViewerIdle.Dispose();

            this.Controls.Clear();
            this.Close();
        }

        public void LogPrintTime(int seqno)
        {
            this.logPrintTime = true;
            this.seqNo = seqno;
        }

        public bool AutoCloseAfterPrint
        {
            get
            {
                return this.autoCloseAfterPrint;
            }

            set
            {
            	this.autoCloseAfterPrint = value;
            }
        }

        // Used when displaying a single report
        public void SetReport(XtraReport report)
        {
            this.repotPrint = report;
            this.printControl1.PrintingSystem = report.PrintingSystem;
            //
            //report.CreateDocument();
            this.printControl1.UpdatePageView();
            //report.AfterPrint += new EventHandler(this.Report_AfterPrint);

        }

        /// <summary>
        /// Click on Print Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPreviewBarItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.logPrintTime)
            {
                //UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
            }

            if (this.autoCloseAfterPrint)
            {
                this.tmrClosePreview.Enabled = true;
            }
        }

        private void tmrClosePreview_Tick(object sender, EventArgs e)
        {
            this.Close();
            this.tmrClosePreview.Enabled = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.logPrintTime)
            {
                PrintingBuilder.Instance.RemovePrintSEQ(this.seqNo);
            }

            if (this.rptViewerIdle.IsRunning)
            {
                this.rptViewerIdle.Stop();
            }

            this.rptViewerIdle.Dispose();

            this.Controls.Clear();

            base.OnClosed(e);
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            //before show any dialog just clear pool
            //UiUtility.EndProcessing();
            this.Visible = true;
            Application.DoEvents();
           
        }

        private void printPreviewBarItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// Print and Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPreviewBarExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.logPrintTime)
            {
                //UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
            }
        }

        /// <summary>
        /// Click on Print Button with Question Mask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPreviewBarItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.logPrintTime)
            {
                //UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
            }
        }


    }
}