using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component;
using Winforms.Components;
using System.Threading;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.UIL.PLASESS_PRINT_PALLET.Reports;
using DevExpress.XtraGrid.Views.Grid;
using HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component.GridViewControl;
using HTN.BITS.LIB;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET
{
    public partial class frmPrintPallet : Form
    {
        private string _userid;

        public string UserID
        {
            get
            {
                return this._userid;
            }

            set
            {
                this._userid = value;
            }
        }

        private LanguageRes language;
        private bool isClose = false;
        private NotifyIconEx notifyIcon;
        private System.Threading.Timer processTimer;

        //private List<Pallet> lstPallet;

        public frmPrintPallet()
        {
            InitializeComponent();

            this.language = new LanguageRes(UiUtility.LanguageUse);

            this.InitializeNotification();
            this.InitializeAutoPrintPallet();
        }

        #region Variable Member

        #endregion

        #region Protected Member

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0011) //WM_QUERYENDSESSION
            {
                isClose = true;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Method Member

        private void InitializeNotification()
        {
            this.notifyIcon = new NotifyIconEx();
            this.notifyIcon.Text = "Print Details On Pallet Running";
            this.notifyIcon.Icon = this.language.GetIcon("LLC_PartialPrint");
            this.notifyIcon.Visible = true;
            //notifyIcon.ContextMenu = NotifyContextMenu;
            this.notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
        }

        private void AutoPrintDetailsOnPallet(List<string> lstPallet)
        {
            try
            {
                DataSet ds;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    ds = shipOrdBll.PrintDetailsOnPalletReport(lstPallet, UiUtility.WH_ID, "system");
                }

                RPT_PALLET_DETAIL rpt = new RPT_PALLET_DETAIL();

                rpt.DataSource = ds;
                //rpt.Parameters["paramUserPrint"].Value = "System";
                rpt.Print();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }
        }


        //private void UpdatePrintOutgoing(List<PartialTicket> lstTicket)
        //{
        //    try
        //    {
        //        string result = string.Empty;

        //        using (PrintPartialTicketBLL printParTickBll = new PrintPartialTicketBLL())
        //        {
        //            printParTickBll.UpdatePartialTicketPrint(lstTicket, "System");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //    }
        //}

        private void GetDetailsOnPalletPrint()
        {
            try
            {
                List<string> lstPallet;
                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstPallet = shipOrdBll.GetAutoPrintPallet(UiUtility.WH_ID);
                }

                if (lstPallet != null)
                {
                    if (lstPallet.Count > 0)
                    {
                        string messaage = String.Join(",", lstPallet.ToArray());

                        this.notifyIcon.ShowBalloon("Print Pallet", string.Format("Pallet : {0}", messaage), NotifyIconEx.NotifyInfoFlags.Info, 5);
                        this.AutoPrintPalletLabel(lstPallet);
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void AutoPrintPalletLabel(List<string> lstPallet)
        {
            try
            {
                DataSet ds;

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    ds = shipOrdBll.PrintDetailsOnPalletReport(lstPallet, UiUtility.WH_ID, "system");
                }

                RPT_PALLET_DETAIL rpt = new RPT_PALLET_DETAIL();

                rpt.DataSource = ds;
                //rpt.Parameters["paramUserPrint"].Value = "System";
                rpt.Print();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }
        }

        #endregion

        #region "Auto Print Partial Ticket"

        private void InitializeAutoPrintPallet()
        {
            TimerCallback timerDelegate = new TimerCallback(this.ProcessRoutine);
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            if (this.processTimer == null)
            {
                System.TimeSpan chkTime = System.TimeSpan.Parse("00:00:10");
                this.processTimer = new System.Threading.Timer(timerDelegate, autoEvent, chkTime, chkTime);
            }
            else
            {
                this.processTimer.Change(10000, 180000);
            }
        }

        private void ProcessRoutine(object data)
        {
            FindFuncLoc();
        }

        private void FindFuncLoc()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    //this.notifyIcon.ShowBalloon("Print Partial Ticket", "ticket : 'LLC123456789998'", NotifyIconEx.NotifyInfoFlags.Info, 5);
                    this.GetDetailsOnPalletPrint();
                }));

                return;

            }
        }

        #endregion

        private void SystemEvents_SessionEnded(object sender, Microsoft.Win32.SessionEndedEventArgs e)
        {
            this.isClose = true;
            this.Close();
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Width = UiUtility.SizeFormWidth;
            this.Height = UiUtility.SizeFormHeight;

            this.Show();
        }

        private void frmPrintPallet_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloon("Program", "Print Details On Pallet Running", NotifyIconEx.NotifyInfoFlags.Info, 1000);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                //this.notifyIcon.Visible = false;
            }

        }

        private void frmPrintPallet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.notifyIcon != null)
            {
                this.notifyIcon.Remove();
            }

            if (this.processTimer != null)
            {
                this.processTimer.Dispose();
            }
        }

        private void frmPrintPallet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isClose)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;

            }
            else
            {
                e.Cancel = false;
                
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit editor = sender as TextEdit;

            if (e.KeyCode == Keys.Enter)
            {
                if (editor.EditValue.ToString().ToLower().Equals(UiUtility.PASSWORD_EXIT))
                    this.isClose = true;

                this.Close();
            }
                
        }

        #region IDisposable Members


        #endregion

        private void frmPrintPallet_Load(object sender, EventArgs e)
        {
            //this.dtpSearchFromDate.DateTime = DateTime.Now.AddDays(-1).Date;
            //this.dtpSearchToDate.DateTime = DateTime.Now.Date;
            Microsoft.Win32.SystemEvents.SessionEnded += new Microsoft.Win32.SessionEndedEventHandler(SystemEvents_SessionEnded);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.isClose = false;
            this.Close();
        }
    }
}