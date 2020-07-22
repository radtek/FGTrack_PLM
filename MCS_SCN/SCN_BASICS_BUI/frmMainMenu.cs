using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using HTN.BITS.MCS.SCN.LIB;
using HTN.BITS.MCS.SCN.UIL.Components;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using System.Net;
using System.IO;
using HTN.BITS.MCS.SCN.BLL;
using HTN.BITS.MCS.SCN.BEL;

namespace HTN.BITS.MCS.SCN.UIL
{
    public partial class frmMainMenu : BaseFormFullMode, IDisposable
    {
        public frmMainMenu()
        {
            
            this.Enabled = false;
            
            InitializeComponent();

            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            FullScreenHandle.StartFullScreen(this);

            this.lblVersion.Text = string.Format("V. {0}",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            this.lblIPAddress.Text = "IP: " + string.Format("{0}", GlobalVariable.GetIPAddress());
        }

        #region Method of PowerManagement

        private void OnPowerNotify(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(new EventHandler(UpdateGUI));
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            // Check Connection
            this.imgConnect.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Icon_Connect;
            //connect = CheckConnection(MobileConfiguration.Configuration.Settings["RMS_SCN_API"].ToString());

            //if (connect)
            //{
            //    this.imgConnect.Image = Properties.Resources.Icon_Connect;
            //}
            //else
            //{
            //    this.imgConnect.Image = Properties.Resources.Icon_Disconnect;
            //}

            // Check Battery
            PowerManagement.PowerInfo powerInfo = base.PowerMgr.GetNextPowerInfo();

            // Determine if we are on battery or AC
            if (powerInfo.Message == PowerManagement.MessageTypes.Status)
            {
                if (powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine)
                {
                    this.imgMode.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Icon_AC;
                }
                else
                {
                    this.imgMode.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Icon_Battery;
                }

                this.checkBattery(Convert.ToInt32(powerInfo.BatteryLifePercent));
            }
            else if (powerInfo.Flags == PowerManagement.SystemPowerStates.Suspend)
            {
                lblErrorMessageStatus.Text = "Device resumed from a suspend. ";
            }
        }

        private void checkBattery(int batterryPercent)
        {
            if (batterryPercent <= 20)
            {
                this.Totalbattery.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Battery_5;
                this.lblpercent.Text = batterryPercent.ToString() + "%";
            }
            else if ((batterryPercent > 20) && (batterryPercent <= 40))
            {
                this.Totalbattery.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Battery_4;
                this.lblpercent.Text = batterryPercent.ToString() + "%";
            }
            else if ((batterryPercent > 40) && (batterryPercent <= 60))
            {
                this.Totalbattery.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Battery_3;
                this.lblpercent.Text = batterryPercent.ToString() + "%";
            }
            else if ((batterryPercent > 60) && (batterryPercent <= 80))
            {
                this.Totalbattery.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Battery_2;
                this.lblpercent.Text = batterryPercent.ToString() + "%";
            }
            else if ((batterryPercent > 80) && (batterryPercent <= 100))
            {
                this.Totalbattery.Image = HTN.BITS.MSC.SCN.UIL.Properties.Resources.Battery_1;
                this.lblpercent.Text = batterryPercent.ToString() + "%";
            }
        }     

        #endregion Method of PowerManageMent

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.PowerMgr.PowerNotify -= new EventHandler(OnPowerNotify);
            base.PowerMgr.DisableNotifications();
            base.PowerMgr.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion

        # region Method

        //private void UseLanguage()
        //{
        //    if (GlobalVariable.LanguageSelect == "fr-CA")
        //    {
        //        this.lbl01Language.Text = "TH";
        //    }
        //    else if (GlobalVariable.LanguageSelect == "en-US")
        //    {
        //        this.lbl01Language.Text = "EN";
        //    }

        //}

        #endregion

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);
            //this.btnMenu1.Image = Properties.Resources.Icon_CargoIn;
            //this.btnMenu2.Image = Properties.Resources.Icon_CargoOut;
            //this.btnMenu9.Image = Properties.Resources.Icon_Exit;


            //this.BackColor = Color.FromArgb(140, 0, 149);
            //this.btnMenu1.BackColor = Color.FromArgb(140, 0, 149);
            ////this.btnMenu2.BackColor = Color.FromArgb(38, 114, 236);
            //this.btnMenu3.BackColor = Color.FromArgb(0, 138, 0);
            //this.btnMenu9.BackColor = Color.FromArgb(210, 71, 38);

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

            this.Enabled = true;
            this.Visible = true;
            Application.DoEvents();

            ////Close splash
            //HTN.BITS.MCS.SCN.UIL.Program.CloseInitial();
        }


        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad1:

                    this.btnMenuMixing.Focus();
                    this.btnMenuMixing_Click (this.btnMenuMixing, new EventArgs());
                    break;
                case Keys.NumPad2:
                    this.btnMenuReplenish.Focus();
                    this.btnMenuReplenish_Click(this.btnMenuReplenish, new EventArgs());
                    break;
                case Keys.F1: //Logoff
                    this.Close();                       
                    break;
                case Keys.F3: //Logoff
                    this.pgbThai_Click(this.pgbThai, new EventArgs());
                    break;
                case Keys.F4: //Logoff
                    this.pgbEng_Click(this.pgbEng, new EventArgs());
                    break;
                default:
                    break;
            }

        }

        private void btnMenuMixing_Click(object sender, EventArgs e)
        {
            try
            {
                Authentication("frmMixing");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void btnMenuReplenish_Click(object sender, EventArgs e)
        {
            try
            {
                Authentication("frmReplenish");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
        }


        private void Authentication(string targetForm)
        {
            M_USER objUser = null;
            string userName = String.Empty;
            string resultID = string.Empty;
            try
            {
                //CHECK VALIDATION USER
                DialogResult diaResult;
                using (frmCheckUser fCheckUser = new frmCheckUser(this, targetForm))
                {
                    diaResult = fCheckUser.ShowDialog();
                    resultID = fCheckUser.USER_ID;
                }

                if (diaResult == DialogResult.OK)
                {
                    //base.ShowWaitProcess();

                    using (AdminBLL adminInBLL = new AdminBLL())
                    {
                        userName = adminInBLL.CheckLogin(resultID, lblIPAddress.Text, "MIXING");

                        //objUser = new M_USER() { BRANCH_CODE = "B001", BRANCH_ID = 2, USER_FNAME = "JANUWAT", USER_ID = 2, USER_LNAME = "SURIKUL", USER_NAME = "JANUWAT" };
                    }

                    //base.HideWaitProcess();

                    if (!String.IsNullOrEmpty(userName))
                    {

                        if (targetForm == "frmMixing")
                        {
                            using (frmMixing fMixing = new frmMixing())
                                {
                                    fMixing.USER_ID = resultID;
                                    //fMixing.USER_ID = objUser.USER_ID.ToString();
                                    //fMixing.USER_NAME = objUser.USER_NAME.ToString().ToUpper();
                                    fMixing.USER_NAME = userName;
                                    //fMixing.HDR_TITLE = "PUT AWAY";
                                    //fMixing.PROCESS_TYPE = "PUT_AWAY";
                                    fMixing.ShowDialog();
                                }
                        }else if (targetForm == "frmReplenish")
                            using (frmReplenish fReplenish = new frmReplenish())
                                {
                                    fReplenish.USER_ID = resultID;
                                    //fMixing.USER_ID = objUser.USER_ID.ToString();
                                    //fMixing.USER_NAME = objUser.USER_NAME.ToString().ToUpper();
                                    fReplenish.USER_NAME = userName;
                                    //fMixing.HDR_TITLE = "PUT AWAY";
                                    //fMixing.PROCESS_TYPE = "PUT_AWAY";
                                    fReplenish.ShowDialog();
                                }
                        }
                    else
                    {
                        base.ShowErrorBox("User : \"" + resultID + "\"\n" + HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.GetString("MSG_AUTH_FAIL"),
                                          HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                    }
                }


            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
            }

        }


        private void btnMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pgbThai_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "fr-CA";
            base.UpdateResourcesInForm("fr-CA");
            this.Refresh();
        }

        private void pgbEng_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageSelect = "en-US";
            base.UpdateResourcesInForm("en-US");
            this.Refresh();
        }

        public void test()
        {
            string test = "";
        }
    }
}