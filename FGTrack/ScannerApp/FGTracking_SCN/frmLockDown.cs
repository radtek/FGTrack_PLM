using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using HTN.BITS.FGTRACK.LIB.Scanner;
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using Ionic.Zip;
using System.Threading;
using SoapExtensionLib;
using HTN.BITS.FGTRACK.LIB;

namespace HTN.BITS.SCN.LOCKDOWN
{
    public partial class frmLockDown : BaseFormFullMode, IDisposable
    {
        public frmLockDown()
        {
            InitializeComponent();
            base.UpdateResourcesInForm("en-US");

            this.powerManager = new PowerManagement();
            FullScreenHandle.StartFullScreen(this);

            this.txtEsc.Text = string.Empty;
        }

        //define and Initialize object 
        //private ServiceRef.Service_Center srvCenter = new ServiceRef.Service_Center();
        private TransferFile transferFile = new TransferFile();

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
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// UpdateGUI delegate. Processes and display the power information
        /// provided by the power management on the system.
        /// </summary>
        private void UpdateGUI(object sender, EventArgs e)
        {
            // Get the information
            PowerManagement.PowerInfo powerInfo = this.powerManager.GetNextPowerInfo();

            // Determine if we are on battery or AC
            if (powerInfo.Message == PowerManagement.MessageTypes.Status)
            {
                if (powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine)
                    powerTypeLabel.Text = "AC";
                else
                {
                    powerTypeLabel.Text = "Battery";
                }
                // Update Main Battery information
                batteryProgressBar.Value = powerInfo.BatteryLifePercent;
                batteryPercentLabel.Text = powerInfo.BatteryLifePercent.ToString() + "%";

                // Update Backup Battery information
                //batteryProgressBar2.Value = powerInfo.BackupBatteryLifePercent;
                //batteryPercentLabel2.Text = powerInfo.BackupBatteryLifePercent.ToString() + "%";
            }
            else if (powerInfo.Flags == PowerManagement.SystemPowerStates.Suspend)
            {
                // The notification of the loss of power does not actually occur 
                // until immediately after it is back.
                lblErrorMessageStatus.Text = "Device resumed from a suspend.";
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
              response = (HttpWebResponse)request.GetResponse();
              request.Abort();

              // success?
              if(response.StatusCode == HttpStatusCode.OK)
              {
                isConnected = true;
              }
            }
            catch(WebException we)
            {
              isConnected = false;
            }
            catch(Exception ex)
            {
              isConnected = false;
            }

            return isConnected;
        }

        private void OpenApplication(string appName, Button btnActive)
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

                    //using (Process proc = new Process())
                    //{
                    //    proc.StartInfo.FileName = startAppName;
                    //    proc.StartInfo.Arguments = string.Empty;
                    //    proc.Start();
                    //    proc.WaitForExit();
                    //}

                    this.IsRunning = false;
                    FullScreenHandle.StartFullScreen(this);
                    this.txtEsc.Text = string.Empty;
                    btnActive.Focus();

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

                BarReader.Instance.BarReader.ScannerEnable = false;
            }
            catch (Exception ex)
            {
                this.IsRunning = false;
                MessageBox.Show(ex.Message);
            }
        }

        //private TextBox TextFocusedFirstLoop()
        //{
        //    // Look through all the controls on this form.
        //    foreach (Control con in this.Controls)
        //    {
        //        // Every control has a Focused property.
        //        if (con.Focused == true)
        //        {
        //            // Try to cast the control to a TextBox.
        //            TextBox textBox = con as TextBox;
        //            if (textBox != null)
        //            {
        //                return textBox; // We have a TextBox that has focus.
        //            }
        //        }
        //    }
        //    return null; // No suitable TextBox was found.
        //}


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
                BarReader.Instance.BarReader.ScannerEnable = false;

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
                    lblErrorMessageStatus.Text = "GetSystemPowerState Failed. Error: " + nError.ToString();
                else
                    lblErrorMessageStatus.Text = "System Power State: " + systemStateName;

                #endregion Start Power Manager

                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPressProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["PRESS"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnVeticalProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["VERTICAL"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnHorizontalProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["HORIZONTAL"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnTampoProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["TAMPO"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnMTSTWHSProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["MTSTVER"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnFGWHSProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["FGWHS"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnFGPressProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["FGPRESS"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void frmLockDown_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1: //select Production Menu
                    if (!this.txtEsc.Focused)
                    {
                        this.btnPressProgram.Focus();
                        this.btnPressProgram_Click(this.btnPressProgram, new EventArgs());
                    }
                    break;
                case Keys.D2:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnVeticalProgram.Focus();
                        this.btnVeticalProgram_Click(this.btnVeticalProgram, new EventArgs());
                    }
                    break;
                case Keys.D3:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnHorizontalProgram.Focus();
                        this.btnHorizontalProgram_Click(this.btnHorizontalProgram, new EventArgs());
                    }
                    break;
                case Keys.D4:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnTampoProgram.Focus();
                        this.btnTampoProgram_Click(this.btnTampoProgram, new EventArgs());
                    }
                    break;
                case Keys.D5:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnMTSTVERProgram.Focus();
                        this.btnMTSTWHSProgram_Click(this.btnMTSTVERProgram, new EventArgs());
                    }
                    break;
                case Keys.D6:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnFGWHSProgram.Focus();
                        this.btnFGWHSProgram_Click(this.btnFGWHSProgram, new EventArgs());
                    }
                    break;
                case Keys.D7:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnFGPressProgram.Focus();
                        this.btnFGPressProgram_Click(this.btnFGPressProgram, new EventArgs());
                    }
                    break;
                case Keys.D8:
                    if (!this.txtEsc.Focused)
                    {
                        this.btnMTSTTAMPOProgram.Focus();
                        this.btnMTSTTAMPOProgram_Click(this.btnMTSTTAMPOProgram, new EventArgs());
                    }
                    break;
                case Keys.D9 :
                    if (!this.txtEsc.Focused)
                    {
                        this.btnMATProgram.Focus();
                        this.btnMATProgram_Click(this.btnMATProgram, new EventArgs());
                    }
                    break;
                case Keys.Escape:
                    if (this.txtEsc.Focused)
                    {
                        this.txtEsc.Text = string.Empty;
                        this.Focus();
                    }
                    else
                    {
                        this.txtEsc.Text = string.Empty;
                        this.txtEsc.Focus();
                    }
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
                default:
                    break;
            }
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {

        }

        public void ProgressUpdate(object sender, ProgressEventArgs e)
        {
            double progress = ((double)e.ProcessedSize / (double)e.TotalSize) * 100.00;
            int iProg = (int)progress;
            //this.progressBar1.Value = iProg;
            this.progressBar2.Value = iProg;

            this.lblInitialStatus.Text = iProg.ToString();

           
            // A long call should be made on a background thread and then
            // there's no need to call refresh, but it really slows things down 
            // making it easy to see progress on local machine :-)
            this.Refresh();
        }

        private void btnInitialSetup_Click(object sender, EventArgs e)
        {
            string plasessChk = MobileConfiguration.Configuration.Settings["PASSWORD"].ToString();

            if (plasessChk.Equals(this.txtEsc.Text))
            {
                //this.tmrProcess.Enabled = true;

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SplasherLoading.Loading(this);

                    
                    //this.progressBar2.Value = 0;

                    string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\";
                    this.lblInitialStatus.Text = "Start download file.";

                    ServiceRef.Service_Center srvCenter = new HTN.BITS.SCN.LOCKDOWN.ServiceRef.Service_Center();
                    //srvCenter.progressDelegate += ProgressUpdate;
                    //Application.DoEvents();

                    byte[] result = srvCenter.DownloadFile("FGTrack_SCN.zip");

                    if (result.Length != 0)
                    {
                        transferFile.WriteBinarFile(result, appPath, "FGTrack_SCN.zip");
                    }
                    this.lblInitialStatus.Text = "Download finished.";

                    this.lblInitialStatus.Text = "Start to Extract file.";

                    this.UnzipSelectedFile(appPath.Replace("BITS_LOCKDOWN", ""), appPath + "\\" + "FGTrack_SCN.zip");

                    this.lblInitialStatus.Text = "Extract finished.";

                    this.DeleteZipFile(appPath + "\\" + "FGTrack_SCN.zip");

                    Cursor.Current = Cursors.Default;
                    SplasherLoading.Unload(this);

                    //this.tmrProcess.Enabled = false;
                }
                catch (Exception ex)
                {
                    this.tmrProcess.Enabled = false;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Password Incurrect!!");
                this.txtEsc.Focus();
            }
        }

        private void UnzipSelectedFile(string appPath, string filename)
        {
            //string parent = System.IO.Path.GetDirectoryName(appPath);
            //string dir = System.IO.Path.Combine(parent,
            //    System.IO.Path.GetFileNameWithoutExtension(zipPath));
            try
            {
                if (File.Exists(filename))
                {
                    using (var zip1 = Ionic.Zip.ZipFile.Read(filename))
                    {
                        foreach (var entry in zip1)
                        {
                            entry.Extract(appPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Zip file not Exists!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeleteZipFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    var fZip = new FileInfo(filename);
                    fZip.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tmrProcess_Tick(object sender, EventArgs e)
        {
            this.progressBar1.MarqueeUpdate();
        }

        private void btnMTSTTAMPOProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["MTSTTAMPO"].ToString();
            this.OpenApplication(appName, btn);
        }

        private void btnMATProgram_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Focus();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            string appName = MobileConfiguration.Configuration.Settings["MATERIAL"].ToString();
            this.OpenApplication(appName, btn);
        }

        


    }
}