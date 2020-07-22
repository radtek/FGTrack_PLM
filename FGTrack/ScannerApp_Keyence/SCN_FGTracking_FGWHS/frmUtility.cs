using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.LIB.Scanner;
using HTN.BITS.FGTRACK.FGWHS.Components;
using Bt;

namespace HTN.BITS.FGTRACK.FGWHS
{
    public partial class frmUtility : BaseFormFullMode, IDisposable
    {
        public frmUtility()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            FullScreenHandle.StartFullScreen(this);
            this.IP_ADDRESS = string.Format("IP: {0}", GlobalVariable.GetIPAddress());
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
                //nothing
            }
        }

        /// <summary>
        /// UpdateGUI delegate. Processes and display the power information
        /// provided by the power management on the system.
        /// </summary>
        private void UpdateGUI(object sender, EventArgs e)
        {
            try
            {
                // Get the information

                PowerManagement.PowerInfo powerInfo = base.PowerMgr.GetNextPowerInfo();

                this.ShowBattery();

                this.IP_ADDRESS = string.Format("IP: {0}", GlobalVariable.GetIPAddress());
                // Determine if we are on battery or AC
                //if (powerInfo.Message == PowerManagement.MessageTypes.Status)
                //{
                //    //if (powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine)
                //    //{
                //    //    this.imgMode.Image = Properties.Resources.Icon_AC;
                //    //}
                //    //else
                //    //{
                //    //    this.imgMode.Image = Properties.Resources.Icon_Battery;
                //    //}

                //    //this.checkBattery(Convert.ToInt32(powerInfo.BatteryLifePercent));

                //}
                //else if (powerInfo.Flags == PowerManagement.SystemPowerStates.Suspend)
                //{
                //    lblErrorMessageStatus.Text = "Device resumed from a suspend. ";
                //}
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void ShowBattery()
        {
            Int32 ret = 0;
            UInt32 mainlevelGet = 0;

            try
            {
                ret = Bt.SysLib.Device.btGetMainBatteryLevel(ref mainlevelGet);
                if (ret != LibDef.BT_OK)
                {
                    this.Totalbattery.Image = Properties.Resources.BatteryLE;
                }
                else
                {
                    switch (mainlevelGet)
                    {
                        case 0:
                            this.Totalbattery.Image = Properties.Resources.BatteryL0;
                            break;
                        case 1:
                            this.Totalbattery.Image = Properties.Resources.BatteryL1;
                            break;
                        case 2:
                            this.Totalbattery.Image = Properties.Resources.BatteryL2;
                            break;
                        case 3:
                            this.Totalbattery.Image = Properties.Resources.BatteryL3;
                            break;
                        case 9:
                            this.Totalbattery.Image = Properties.Resources.BatteryL9;
                            break;
                        default:
                            this.Totalbattery.Image = Properties.Resources.BatteryLE;
                            break;

                    }
                }
            }
            catch
            {
                this.Totalbattery.Image = Properties.Resources.BatteryLE;
            }
        }

        #endregion Method of PowerManageMent

        #region Variable Member

        private string _IP_ADDRESS;

        #endregion

        #region Property Member

        private string IP_ADDRESS
        {
            get
            {
                if (string.IsNullOrEmpty(_IP_ADDRESS) || _IP_ADDRESS == "127.0.0.1")
                    return string.Format(" {0}", GlobalVariable.GetIPAddress());
                else
                    return _IP_ADDRESS;
            }
            set
            {
                if (_IP_ADDRESS == value)
                    return;

                _IP_ADDRESS = value;

                this.lblIPAddress.Text = _IP_ADDRESS;
            }
        }

        #endregion
      
        #region Method Member

        private void FGReturn_Process()
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
                        using (frmFGReturn fQcReturn = new frmFGReturn())
                        {
                            fQcReturn.USER_ID = resultID;
                            fQcReturn.ShowDialog();
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

        #endregion

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

                //// Display the current power state on the status bar
                //if (nError != 0)
                //    lblErrorMessageStatus.Text = "GetSystemPowerState Failed. Error: " + nError.ToString();
                //else
                //    lblErrorMessageStatus.Text = "System Power State: " + systemStateName;
                lblErrorMessageStatus.Text = "";

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

            base.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad1: //select Production Menu
                    this.btn01CheckStatus.Focus();
                    this.btn01CheckStatus_Click(this.btn01CheckStatus, new EventArgs());
                    break;
                case Keys.NumPad2: //select Production Menu
                    this.btn01QCReturn.Focus();
                    this.btn01QCReturn_Click(this.btn01QCReturn, new EventArgs());
                    break;
                case Keys.E: //Change Menu to English
                    this.pgbEng.Focus();
                    this.pgbEng_Click(this.pgbEng, new EventArgs());
                    break;
                case Keys.T: //Change menu to Thai
                    this.pgbThai.Focus();
                    this.pgbThai_Click(this.pgbThai, new EventArgs());
                    break;
                case Keys.F1: //Logoff
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

        private void btn01QCReturn_Click(object sender, EventArgs e)
        {
            this.FGReturn_Process();
        }
    }
}