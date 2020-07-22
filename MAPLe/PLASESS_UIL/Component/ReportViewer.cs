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
using HTN.BITS.UIL.PLASESS.ConfirmForms;

namespace HTN.BITS.UIL.PLASESS.Component
{

    public partial class ReportViewer : BaseForm, DevExpress.XtraPrinting.ICommandHandler
    {
        private bool logPrintTime = false;
        private bool autoCloseAfterPrint = true; //default
        private int seqNo;
        private XtraReport repotPrint;

        public ReportViewer()
        {
            InitializeComponent();

            //set idle time
            this.rptViewerIdle.IdleTime = System.TimeSpan.Parse(UiUtility.RPTViewerIdleTime);
            this.rptViewerIdle.IdleAsync += new EventHandler(this.ReportViewerIdle_IdleAsync);
        }

        ~ReportViewer()
        {
            this.rptViewerIdle.IdleAsync -= new EventHandler(this.ReportViewerIdle_IdleAsync);
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
            //test add by jack
            this.repotPrint.PrintingSystem.AddCommandHandler(this);

            this.printControl1.PrintingSystem = report.PrintingSystem;
            //
            //report.CreateDocument();
            this.printControl1.UpdatePageView();
            //report.AfterPrint += new EventHandler(this.Report_AfterPrint);


        }

        #region Method Customization

        public DialogResult GetLogPrintTime()
        {
            DialogResult diResult = DialogResult.None;
            try
            {
                DataTable dtResult;
                using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                {
                    dtResult = jobOrdBll.GetPrintTime(this.seqNo);
                }

                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        //show popup Message
                        frmCOFPrintCard fCofDocRef = new frmCOFPrintCard()
                        {
                            DT_PRINTED_TIME = dtResult
                        };
                        diResult = UiUtility.ShowPopupForm(fCofDocRef, this, true);
                    }
                    else
                        diResult = DialogResult.OK;
                }
                else
                    diResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error GetLogPrintTime", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return diResult;
        }

        #endregion

        /// <summary>
        /// Click on Print Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPreviewBarItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                switch (this.repotPrint.Report.ToString())
                {
                    case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD":
                        //nothing
                        break;
                    case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD_8545":
                        //nothing
                        break;
                    default:
                        if (this.autoCloseAfterPrint)
                        {
                            this.tmrClosePreview.Enabled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error printPreviewBarItem9_ItemClick", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            
            if (!this.rptViewerIdle.IsRunning)
            {
                this.rptViewerIdle.Start();
            }
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
            try
            {
                switch (this.repotPrint.Report.ToString())
                {
                    case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD":
                        if (this.logPrintTime)
                        {
                            UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
                        }
                        break;
                    case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD_8545":
                        if (this.logPrintTime)
                        {
                            UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
                        }
                        break;
                    default:
                        //nothing
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error printPreviewBarItem9_ItemClick", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Click on Print Button with Question Mask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPreviewBarItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



        #region ICommandHandler Members

        public bool CanHandleCommand(PrintingSystemCommand command, IPrintControl printControl)
        {
            bool result;

            switch (command)
            {
                case PrintingSystemCommand.Print:
                    //result = this.repotPrint.Report.ToString().Equals("HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD");
                    switch (this.repotPrint.Report.ToString())
                    {
                        case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD":
                            result = true;
                            break;
                        case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD_8545":
                            result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
                case PrintingSystemCommand.PrintDirect:
                    //result = this.repotPrint.Report.ToString().Equals("HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD");
                    switch (this.repotPrint.Report.ToString())
                    {
                        case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD":
                            result = true;
                            break;
                        case "HTN.BITS.UIL.PLASESS.Reports.RPT_PRODUCT_CARD_8545":
                            result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public void HandleCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl, ref bool handled)
        {
            ReportPrintTool printTool = new ReportPrintTool(this.repotPrint);
            
            switch (command)
            {
                case PrintingSystemCommand.Print:
                    try
                    {
                        DialogResult result = this.GetLogPrintTime();
                        if (result == DialogResult.OK)
                        {
                            if (printTool.PrintDialog() ?? false)
                            {
                                //if click print
                                if (this.logPrintTime)
                                {
                                    UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
                                }

                                if (this.autoCloseAfterPrint)
                                {
                                    this.tmrClosePreview.Enabled = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }

                    handled = true;
                    break;
                case PrintingSystemCommand.PrintDirect:
                    try
                    {
                        //DialogResult result = XtraMessageBox.Show(this, "Do you want to print?", "Please Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        DialogResult result = this.GetLogPrintTime();
                        if (result == DialogResult.OK)
                        {
                            printTool.Print();

                            if (this.logPrintTime)
                            {
                                UiUtility.UpdatePrintTime(this.seqNo, this.repotPrint.Parameters["paramUserPrint"].Value.ToString());
                            }

                            if (this.autoCloseAfterPrint)
                            {
                                this.tmrClosePreview.Enabled = true;
                            }
                        }
                    }
                    catch (Exception ex) { }

                    handled = true;
                    break;
                default:
                    break;
            }
            //if (command == PrintingSystemCommand.Print || command == PrintingSystemCommand.PrintDirect)
            //{
            //    try
            //    {
            //        DialogResult result = XtraMessageBox.Show(this, "Do you want to print?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result == DialogResult.Yes)
            //        {

            //        }
            //        else
            //        {

            //        }

            //        //ReportPrintTool printTool = new ReportPrintTool(this.repotPrint);
            //        //if (printTool.PrintDialog() ?? false)
            //        //{
            //        //    result = DialogResult.OK;
            //        //}
            //        //else
            //        //    result = DialogResult.Cancel;

            //        //result =printTool.PreviewForm.ShowDialog();

            //    }
            //    catch (Exception) { }
            //    finally
            //    {
            //        // Actually printed?
            //        //this.PrintResult = result;
            //    };

            //    // Set handled to true to avoid the standard procedure to be called.
            //    handled = true;
            //};
            //throw new NotImplementedException();
        }

        #endregion

        //public void StartProcess(string path)
        //{
        //    Process process = new Process();
        //    try
        //    {
        //        process.StartInfo.FileName = path;
        //        process.Start();
        //        process.WaitForInputIdle();
        //    }
        //    catch { }
        //}
    }
}