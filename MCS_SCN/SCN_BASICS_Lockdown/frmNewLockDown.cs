using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.IO;
using HTN.BITS.MCS.SCN.LIB;
using HTN.BITS.MCS.SCN.LOCKDOWN.Components;
using System.Xml;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;

namespace HTN.BITS.MCS.SCN.LOCKDOWN
{
    public partial class frmNewLockDown : BaseFormFullMode, IDisposable
    {
        static Bitmap backgroundBmp = null;
        static Bitmap batteryBmp = null;
        static Bitmap totalbattery = null;
        static Bitmap connection = null;

        static string mainBattery = string.Empty;
        static bool connect = false;
        static int batterryPercen = 100;

        public frmNewLockDown()
        {
            InitializeComponent();
            base.UpdateResourcesInForm("en-US");

            this.powerManager = new PowerManagement();

            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            FullScreenHandle.StartFullScreen(this);

            this.txtEsc.Text = string.Empty;
        }

        private void WriteIPAddress()
        {
            //Graphics gr = this.CreateGraphics();
            //gr.DrawString(string.Format("IP : {0}", GlobalVariable.GetIPAddress()), new Font("Tahoma", 9, FontStyle.Bold), new SolidBrush(Color.Yellow), 5, 5);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Empty);

            connect = CheckConnection(MobileConfiguration.Configuration.Settings["BPRO_Service"].ToString());

            #region Battery

           
            if (batterryPercen <= 20)
            {
                totalbattery = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Battery_5;
            }
            else if ((batterryPercen > 20) && (batterryPercen <= 40))
            {
                totalbattery = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Battery_4;
            }
            else if ((batterryPercen > 40) && (batterryPercen <= 60))
            {
                totalbattery = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Battery_3;
            }
            else if ((batterryPercen > 60) && (batterryPercen <= 80))
            {
                totalbattery = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Battery_2;
            }
            else if ((batterryPercen > 80) && (batterryPercen <= 100))
            {
                totalbattery = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Battery_1;
            }
            

            if (totalbattery != null) e.Graphics.DrawImage(totalbattery, 3, 3);

            e.Graphics.DrawString(batterryPercen.ToString() + "%", new Font("Tahoma", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(178, 195, 224)),
                        new RectangleF(42, 5, 40, 12), new StringFormat() { Alignment = StringAlignment.Far });

            #endregion         

            #region Connection

            if (connect)
            {
                connection = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Icon_Connect;
            }
            else
            {
                connection = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Icon_Disconnect;
            }            

            if (connection != null) e.Graphics.DrawImage(connection, 199, 3);

            #endregion

            #region Language

            e.Graphics.DrawString("EN", new Font("Tahoma", 7, FontStyle.Bold), new SolidBrush(Color.White),
                        new RectangleF(187, 4, 49, 12), new StringFormat() { Alignment = StringAlignment.Near });

            #endregion

            #region Power Type

            if (mainBattery == "AC")
            {
                batteryBmp = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Icon_AC;
            }
            else
            {
                batteryBmp = HTN.BITS.MSC.SCN.LOCKDOWN.Properties.Resources.Icon_Battery;
            }

            if (batteryBmp != null) e.Graphics.DrawImage(batteryBmp, 215, 3);

            #endregion
          
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (backgroundBmp == null)
                backgroundBmp = ResourceManager.Instance.GetBitmap("BG_01"); //(Bitmap)Properties.Resources.ResourceManager.GetObject("SplashBitmap");
            if (backgroundBmp != null) e.Graphics.DrawImage(backgroundBmp, 0, 0);

            string date = string.Empty;
            string month = string.Empty;
            string year = string.Empty;

            date = DateTime.Today.ToString("dd");
            month = DateTime.Today.ToString("MMMM", CultureInfo.InvariantCulture);
            year = DateTime.Today.ToString("yyyy", CultureInfo.InvariantCulture);

            e.Graphics.DrawString(DateTime.Now.ToString("HH:mm", CultureInfo.InvariantCulture), new Font("Century Gothic", 22, FontStyle.Regular), new SolidBrush(Color.White), 10, 205);
            e.Graphics.DrawString(DateTime.Now.DayOfWeek.ToString(), new Font("Century Gothic", 16, FontStyle.Regular), new SolidBrush(Color.White), 10, 240);
            e.Graphics.DrawString(month + " " + date + ", " + year, new Font("Century Gothic", 16, FontStyle.Regular), new SolidBrush(Color.White), 10, 265);

            e.Graphics.DrawString(string.Format("IP : {0}", GlobalVariable.GetIPAddress()), new Font("Century Gothic", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(1, 4, 65)), 9, 20);
            //e.Graphics.DrawString("For Exit", new Font("Tahoma", 7, FontStyle.Bold), new SolidBrush(Color.Yellow), 155, 5);
            e.Graphics.DrawString("LOCKDOWN SCREEN", new Font("Century Gothic", 15, FontStyle.Bold), new SolidBrush(Color.FromArgb(3, 1, 89)), 18, 60);

            e.Graphics.DrawString("BASICS PRO [1]", new Font("Century Gothic", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(2, 16, 150)), 122, 100);
            //e.Graphics.DrawString("BASICS VMI [1]", new Font("Century Gothic", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(2, 16, 150)), 123, 100);
            //e.Graphics.DrawString("TEMPORARY MHM [2]", new Font("Century Gothic", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(2, 16, 150)), 85, 125);
            //e.Graphics.DrawString("BASICS PRO [3]", new Font("Century Gothic", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(2, 16, 150)), 122, 150);

            e.Graphics.DrawString("@2015 Hi-Tech Nittsu. All rights reserved", new Font("Tahoma", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(100, 100, 100)), 10, 305);
            
           
        
        }

        //define and Initialize object 
        //private ServiceRef.Service_Center srvCenter = new ServiceRef.Service_Center();
        //private TransferFile transferFile = new TransferFile();

        #region Variable Member

        private PowerManagement powerManager;
        private bool IsRunning = false;

        #endregion

        #region Method of PowerManageMent

        /// <summary>
        /// Power event delegate. Invokes the UpdateGUI delegate.
        /// </summary>
        private void OnPowerNotify(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    // We are not in the UI thread so we need to Invoke to get there
                    // Unfortunately we cannot pass arguements across the thread boundary
                    // as this is a limitation of the CF
                    this.Invoke(new EventHandler(UpdateGUI));
                }
            }
            catch (Exception ex)
            {
                //nothing
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// UpdateGUI delegate. Processes and display the power information
        /// provided by the power management on the system.
        /// </summary>
        private void UpdateGUI(object sender, EventArgs e)
        {
            this.Refresh();

            Graphics gr = this.CreateGraphics();
            gr.Clear(Color.Empty);

            // Get the information
            PowerManagement.PowerInfo powerInfo = this.powerManager.GetNextPowerInfo();

            // Determine if we are on battery or AC
            if (powerInfo.Message == PowerManagement.MessageTypes.Status)
            {
                if (powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine)
                {
                    mainBattery = "AC";
                }
                else
                {
                    mainBattery = "Battery";
                }
                // Update Main Battery information
                batterryPercen = powerInfo.BatteryLifePercent;
                //batteryProgressBar.Value = batterryPercen;
            }
            else if (powerInfo.Flags == PowerManagement.SystemPowerStates.Suspend)
            {
                if (powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine)
                {
                    mainBattery = "AC";
                }
                else
                {
                    mainBattery = "Battery";
                }

                batterryPercen = powerInfo.BatteryLifePercent;
                //batteryProgressBar.Value = batterryPercen;
            }
        }      

        #endregion Method of PowerManageMent

        #region Method Member

        private bool CheckConnection(string targetAddress)
        {
            HttpWebRequest request;
            HttpWebResponse response;

            bool isConnected = false;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(targetAddress);
                request.Proxy = null;
                response = (HttpWebResponse)request.GetResponse();
                request.Abort();

                // success?
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    isConnected = true;
                    connect = true;
                }
            }
            catch (WebException we)
            {
                isConnected = false;
                connect = false;
            }
            catch (Exception ex)
            {
                isConnected = false;
                connect = false; 
            }

            return isConnected;
        }

        private void OpenApplication(string appName)
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
                string appPath = Path.GetDirectoryName(codeBase).Replace("bits_lockdown", string.Empty);

                string startAppName = string.Format("{0}\\{1}", appPath, appName);

                if (File.Exists(startAppName))
                {
                    this.IsRunning = true;

                    CreateLaunchApp.LaunchApp(startAppName, string.Empty);

                    this.IsRunning = false;
                    FullScreenHandle.StartFullScreen(this);
                    this.txtEsc.Text = string.Empty;

                    Cursor.Current = Cursors.Default;
                    Cursor.Show();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    Cursor.Show();

                    this.IsRunning = false;
                    MessageBox.Show(startAppName + "\nFile Not Exist");
                }
            }
            catch (Exception ex)
            {
                this.IsRunning = false;
                MessageBox.Show(ex.Message, "OpenApplication");
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            this.powerManager.PowerNotify -= new EventHandler(OnPowerNotify);
            this.powerManager.DisableNotifications();
            this.powerManager.Dispose();

            FullScreenHandle.StopFullScreen(this);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void frmLockDown_Load(object sender, EventArgs e)
        {
            try
            {
                //BarReader.Instance.BarReader.ScannerEnable = false;
                #region Start Power Manager
                // Hook up the Power notify event. This event would not be activated 
                // until EnableNotifications is called. 
                this.powerManager.PowerNotify += new EventHandler(OnPowerNotify);

                // Enable power notifications. This will cause a thread to start
                // that will fire the PowerNotify event when any power notification 
                // is received.
                this.powerManager.EnableNotifications();

                // Get the current power state. 
                StringBuilder systemStateName = new StringBuilder(20);
                PowerManagement.SystemPowerStates systemState;
                int nError = this.powerManager.GetSystemPowerState(systemStateName, out systemState);

                // Display the current power state on the status bar
                if (nError != 0)
                {
                    //lblErrorMessageStatus.Text = "GetSystemPowerState Failed. Error: " + nError.ToString();
                    //gr.DrawString("GetSystemPowerState Failed. Error: " + nError.ToString(), new Font("Tahoma", 8, FontStyle.Bold), new SolidBrush(Color.Yellow), 7, 265);
                }
                else
                {
                    //lblErrorMessageStatus.Text = "System Power State: " + systemStateName;
                    //gr.DrawString("System Power State: " + systemStateName, new Font("Tahoma", 8, FontStyle.Bold), new SolidBrush(Color.Yellow), 7, 265);
                }

                #endregion Start Power Manager

                //this.WriteIPAddress();
                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void frmLockDown_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                //case Keys.D1: //select production Menu Rack
                //    if (!this.txtEsc.Focused)
                //    {
                //        string appName = MobileConfiguration.Configuration.Settings["BASICS_VMI"].ToString();
                //        this.OpenApplication(appName);
                //    }
                //    break;
                //case Keys.D2: //select Production Menu
                //    if (!this.txtEsc.Focused)
                //    {
                //        string appName = MobileConfiguration.Configuration.Settings["TEMP_MHM"].ToString();
                //        this.OpenApplication(appName);
                //    }
                //    break;
                //case Keys.D3: //select Production Menu
                //    if (!this.txtEsc.Focused)
                //    {
                //        string appName = MobileConfiguration.Configuration.Settings["BASICS_PRO"].ToString();
                //        this.OpenApplication(appName);
                //    }
                //    break;
                //case Keys.Escape:
                //    if (!this.txtEsc.Focused)
                //        this.txtEsc.Focus();
                //    break;
                //default:
                //    break;

                case Keys.D1:
                    if (!this.txtEsc.Focused)
                    {
                        string appName = MobileConfiguration.Configuration.Settings["BASICS_PRO"].ToString();
                        this.OpenApplication(appName);
                    }
                    break;              
                case Keys.Escape:
                    if (!this.txtEsc.Focused)
                        this.txtEsc.Focus();
                    break;
                default:
                    break;
            }
        }

        private void frmLockDown_Closing(object sender, CancelEventArgs e)
        {
            string plasessChk = MobileConfiguration.Configuration.Settings["PASSWORD"].ToString();
            if (!plasessChk.Equals(this.txtEsc.Text))
            {
                //can't exit
                this.txtEsc.Text = string.Empty;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void frmLockDown_Closed(object sender, EventArgs e)
        {
            this.Controls.Clear();
        }

        private void txtEsc_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (!string.IsNullOrEmpty(this.txtEsc.Text))
                    {
                        this.Close();
                    }
                    break;
                case Keys.Escape:
                    this.txtEsc.Text = string.Empty;
                    this.btnActiveForm.Focus();
                    //this.Focus();
                    break;
                default:
                    break;
            }
        }

        private void frmLockDown_Click(object sender, EventArgs e)
        {
            this.txtEsc.Text = string.Empty;
            this.Focus();
        }

        private void btnActiveForm_Click(object sender, EventArgs e)
        {
            this.Focus();
        }


    }
}