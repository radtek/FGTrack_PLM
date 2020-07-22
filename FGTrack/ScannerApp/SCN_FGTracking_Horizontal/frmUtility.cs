using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.HORIZONTAL.Components;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.HORIZONTAL
{
    public partial class frmUtility : BaseFormFullMode, IDisposable
    {
        public frmUtility()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            FullScreenHandle.StartFullScreen(this);
        }

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

        private void pgbEng_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "en-US";
            base.UpdateResourcesInForm("en-US");
        }

        private void pgbThai_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "fr-CA";
            base.UpdateResourcesInForm("fr-CA");
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
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

        private void frmMainMenu_Closing(object sender, CancelEventArgs e)
        {
            FullScreenHandle.StopFullScreen(this);
        }

        private void btn01Exit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1: //select Production Menu
                    this.btn01CheckStatus.Focus();
                    this.btn01CheckStatus_Click(this.btn01CheckStatus, new EventArgs());
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

        private void btn01CheckStatus_Click(object sender, EventArgs e)
        {
            using (frmProductCardInfo fCardInfo = new frmProductCardInfo())
            {
                fCardInfo.USER_ID = "SYSTEM";
                fCardInfo.ShowDialog();
            }
        }
    }
}