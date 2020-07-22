using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.Vertical.Components;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.Vertical
{
    public partial class frmProduction : BaseFormFullMode, IDisposable
    {
        public frmProduction()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            FullScreenHandle.StartFullScreen(this);
        }

        #region "Variable Member"
        #endregion

        #region Method of PowerManageMent

        /// <summary>
        /// Power event delegate. Invokes the UpdateGUI delegate.
        /// </summary>
        private void OnPowerNotify(object sender, EventArgs e)
        {
            if (!this.IsDisposed)
            {
                // We are not in the UI thread so we need to Invoke to get there
                // Unfortunately we cannot pass arguements across the thread boundary
                // as this is a limitation of the CF
                this.Invoke(new EventHandler(UpdateGUI));
            }
        }

        /// <summary>
        /// UpdateGUI delegate. Processes and display the power information
        /// provided by the power management on the system.
        /// </summary>
        private void UpdateGUI(object sender, EventArgs e)
        {
            // Get the information
            PowerManagement.PowerInfo powerInfo = base.PowerMgr.GetNextPowerInfo();

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
                lblErrorMessageStatus.Text = "Device resumed from a suspend. ";
            }
        }

        #endregion Method of PowerManageMent

        #region "Method Member"

        private void ProductionProcess(eProcessMode process)
        {
            string resultMsg = string.Empty;
            string resultID = string.Empty;
            try
            {
                //CHECK VALIDATION USER
                DialogResult diaResult;
                using (frmCheckUser fCheckUser = new frmCheckUser(this))
                {
                    diaResult = fCheckUser.ShowDialog();
                    resultID = fCheckUser.USER_ID;
                }

                if (diaResult == DialogResult.OK)
                {
                    base.ShowWaitProcess();
                    resultMsg = ServiceProvider.Instance.Proxy.CheckValidationUser(resultID);
                    base.HideWaitProcess();

                    if (!string.IsNullOrEmpty(resultMsg))
                    {

                        using (frmProductFinish fProdFinish = new frmProductFinish())
                        {
                            fProdFinish.USER_ID = resultID;
                            fProdFinish.PROCESS_MODE = process;
                            fProdFinish.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("User : \"" + resultID + "\"\nAuthentication Fail!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }


            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                MessageBox.Show(ex.Message);
            }
        }

        private void AssignNG()
        {
            string resultMsg = string.Empty;
            string resultID = string.Empty;
            try
            {
                //CHECK VALIDATION USER
                DialogResult diaResult;
                using (frmCheckUser fCheckUser = new frmCheckUser(this))
                {
                    diaResult = fCheckUser.ShowDialog();
                    resultID = fCheckUser.USER_ID;
                }

                if (diaResult == DialogResult.OK)
                {
                    base.ShowWaitProcess();
                    resultMsg = ServiceProvider.Instance.Proxy.CheckValidationUser(resultID);
                    base.HideWaitProcess();

                    if (!string.IsNullOrEmpty(resultMsg))
                    {

                        using (frmAssignNG fasNG = new frmAssignNG())
                        {
                            fasNG.JOB_NO = string.Empty;
                            fasNG.LINE_NO = -1;
                            fasNG.USER_ID = resultID;
                            fasNG.IsFinishProd = false;

                            diaResult = fasNG.ShowDialog();
                        }
                    }
                    else
                    {
                        base.ShowErrorBox("User : \"" + resultID + "\"\nAuthentication Fail!!",
                                          "Warning");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btn01Injection_Click(object sender, EventArgs e)
        {
            this.ProductionProcess(eProcessMode.INJECTION);
        }

        private void btn01Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProduction_Load(object sender, EventArgs e)
        {
            try
            {
                #region Start Power Manager
                // Hook up the Power notify event. This event would not be activated 
                // until EnableNotifications is called. 
                base.PowerMgr.PowerNotify += new EventHandler(OnPowerNotify);

                // Enable power notifications. This will cause a thread to start
                // that will fire the PowerNotify event when any power notification 
                // is received.
                base.PowerMgr.EnableNotifications();

                // Get the current power state. 
                StringBuilder systemStateName = new StringBuilder(20);
                PowerManagement.SystemPowerStates systemState;
                int nError = base.PowerMgr.GetSystemPowerState(systemStateName, out systemState);

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

        private void frmProduction_Closing(object sender, CancelEventArgs e)
        {
            FullScreenHandle.StopFullScreen(this);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.PowerMgr.PowerNotify -= new EventHandler(OnPowerNotify);
            base.PowerMgr.DisableNotifications();
            base.PowerMgr.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion

        private void frmProduction_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1: //Injection
                    this.btn01Injection.Focus();
                    this.btn01Injection_Click(this.btn01Injection, new EventArgs());
                    break;
                case Keys.D2: //Trimming
                    this.btn01Trimming.Focus();
                    this.btn01Trimming_Click(this.btn01Trimming, new EventArgs());
                    break;
                case Keys.D3: //Electric Check
                    this.btn01ElectricCheck.Focus();
                    this.btn01ElectricCheck_Click(this.btn01ElectricCheck, new EventArgs());
                    break;
                case Keys.D4: //Pre QC
                    this.btnAllProcess.Focus();
                    this.btnAllProcess_Click(this.btnAllProcess, new EventArgs());
                    break;
                case Keys.D5: //All Process
                    this.btn01AllProcess.Focus();
                    this.btn01AllProcess_Click(this.btn01AllProcess, new EventArgs());
                    break;
                case Keys.D6: //Assign NG
                    this.btnAssignNG.Focus();
                    this.btnAssignNG_Click(this.btnAssignNG, new EventArgs());
                    break;
                case Keys.E: //Change Menu to English
                    this.pgbEng.Focus();
                    this.pgbEng_Click(this.pgbEng, new EventArgs());
                    break;
                case Keys.T: //Change menu to Thai
                    this.pgbThai.Focus();
                    this.pgbThai_Click(this.pgbThai, new EventArgs());
                    break;
                case Keys.Escape: //Logoff
                    this.btn01Exit.Focus();
                    this.btn01Exit_Click(this.btn01Exit, new EventArgs());
                    break;
                default:
                    break;
            }
            //if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            //{
            //    // Up
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            //{
            //    // Down
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            //{
            //    // Left
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            //{
            //    // Right
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            //{
            //    // Enter
            //}

        }

        private void pgbThai_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "fr-CA";
            base.UpdateResourcesInForm("fr-CA");
        }

        private void pgbEng_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "en-US";
            base.UpdateResourcesInForm("en-US");
        }

        private void btn01Trimming_Click(object sender, EventArgs e)
        {
            this.ProductionProcess(eProcessMode.TRIMMING);
        }

        private void btn01ElectricCheck_Click(object sender, EventArgs e)
        {
            this.ProductionProcess(eProcessMode.ELECTRIC);
        }

        private void btnAssignNG_Click(object sender, EventArgs e)
        {
            this.AssignNG();
        }

        private void btnAllProcess_Click(object sender, EventArgs e)
        {
            this.ProductionProcess(eProcessMode.PREQC);
        }

        private void btn01AllProcess_Click(object sender, EventArgs e)
        {
            this.ProductionProcess(eProcessMode.AllPROCESS);
        }
    }
}