using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB.Scanner;
using System.Threading;
using Bt;
using Bt.SysLib;

namespace HTN.BITS.FGTRACK.LIB
{
    public partial class BaseFormFullMode : LocalizedForm
    {
        //private bool isFullScreen;
        private PowerManagement powerManager;

        public BaseFormFullMode()
        {
            InitializeComponent();
            this.powerManager = new PowerManagement();
        }

        protected PowerManagement PowerMgr
        {
            get
            {
                return this.powerManager;
            }

            set
            {
                if (this.powerManager == value)
                    return;
                this.powerManager = value;
            }
        }

        //public bool IsFullScreen
        //{
        //    get
        //    {
        //        return isFullScreen;
        //    }
        //    set
        //    {
        //        if (isFullScreen == value)
        //            return;
        //        isFullScreen = value;
        //    }
        //}



        public void UpdateResourcesInForm(string lanuage)
        {
            try
            {
               // lanuage = "en-US";//en-GB
                if (lanuage == "fr-CA")
                    lanuage = "ja-JP";//"en-GB";
                ResourceManager.Instance.Culture = new System.Globalization.CultureInfo(lanuage);
                base.UpdateResources();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void ShowWaitProcess()
        {
            Cursor.Current = Cursors.WaitCursor;
            SplasherLoading.Loading(this);
        }

        protected void HideWaitProcess()
        {
            Cursor.Current = Cursors.Default;
            SplasherLoading.Unload(this);
            
        }

        public void StartPlayTon_Complete()
        {
            ThreadStart operation = new ThreadStart(PlayTone_Thread);
            Thread thr = new Thread(operation);
            thr.Start();
        }
        private void PlayTone_Thread()
        {
            try
            {
                PlayCompletelySound();

                //this.tone1.CurrentVolume = Intermec.Device.Audio.Tone.VOLUME.VERY_LOUD;
                //this.tone1.CurrentDuration = 120;
                //this.tone1.CurrentPitch = 1400;
                //for (int i = 0; i <= 1; i++)//2 time;
                //{
                //    this.tone1.Play();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        private void PlayCompletelySound()
        {
            Int32 ret = 0;
            String disp = "";



            LibDef.BT_BUZZER_PARAM stBuzzerSet = new LibDef.BT_BUZZER_PARAM();			// Buzzer control structure (Set)
            // Set to repeat "500 ms on, 500 ms off" 3 times
            stBuzzerSet.dwOn = 100;		// Rumble time [ms] (1 to 5000)
            stBuzzerSet.dwOff = 100;		// Stop time [ms] (0 to 5000)
            stBuzzerSet.dwCount = 2;	// Rumble count [times] (0 to 100)
            stBuzzerSet.bTone = 16;//16;		// Musical scale (1 to 16)
            stBuzzerSet.bVolume = 3;	// Buzzer volume (1 to 3)

            //LibDef.BT_VIBRATOR_PARAM stVibSet = new LibDef.BT_VIBRATOR_PARAM();		// Vibrator operation structure (Set)
            // //Set to repeat "500 ms on, 500 ms off" 3 times
            //stVibSet.dwOn = 100;		// Rumble time [ms] (1 to 5000)
            //stVibSet.dwOff = 100;			// Stop time [ms] (0 to 5000)
            //stVibSet.dwCount = 0;		// Rumble count [times] (0 to 100)

            LibDef.BT_LED_PARAM stLedSet = new LibDef.BT_LED_PARAM();				// LED control structure (Set)

            // Set to repeat "500 ms on, 500 ms off" 3 times
            stLedSet.dwOn = 100;					// Rumble time [ms] (1 to 5000)
            stLedSet.dwOff = 100;						// Stop time [ms] (0 to 5000)
            stLedSet.dwCount = 2;					// Rumble count [times] (0 to 100)
            stLedSet.bColor = LibDef.BT_LED_GREEN;	// Light color

            try
            {
                // btBuzzer Rumble
                ret = Device.btBuzzer(1, stBuzzerSet);
                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error stBuzzerSet");
                    return;
                }
                // btVibrator Rumble
                //     ret = Device.btVibrator(1, stVibSet);
                // btLED Light on
                ret = Device.btLED(1, stLedSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error stLedSet");
                    return;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        protected void ShowInformationBox(string message, string title)
        {
            using (fMessageBox fMsg = new fMessageBox(this))
            {
                fMsg.MESSAGE = message;
                fMsg.TITLE = title;
                fMsg.MSG_ICON = eMessageIcon.Safe;
                fMsg.MSG_TON = eMessageTon.Information;

                fMsg.ShowDialog();
            }
        }

        protected void ShowCompletelyBox(string message, string title)
        {
            using (fMessageBox fMsg = new fMessageBox(this))
            {
                fMsg.MESSAGE = message;
                fMsg.TITLE = title;
                fMsg.MSG_ICON = eMessageIcon.Safe;
                fMsg.MSG_TON = eMessageTon.Completely;

                fMsg.ShowDialog();
            }
        }

        protected void ShowWarningBox(string message, string title)
        {
            using (fMessageBox fMsg = new fMessageBox(this))
            {
                fMsg.MESSAGE = message;
                fMsg.TITLE = title;
                fMsg.MSG_ICON = eMessageIcon.Warning;
                fMsg.MSG_TON = eMessageTon.Warning;

                fMsg.ShowDialog();
            }
        }


        protected void ShowErrorBox(string message, string title)
        {
            using (fMessageBox fMsg = new fMessageBox(this))
            {
                fMsg.MESSAGE = message;
                fMsg.TITLE = title;
                fMsg.MSG_ICON = eMessageIcon.Danger;
                fMsg.MSG_TON = eMessageTon.Dangerous;

                fMsg.ShowDialog();
            }
        }

        protected DialogResult ShowQuestionBox(string message, string title)
        {
            DialogResult result;
            using (fMessageBox fMsg = new fMessageBox(this))
            {
                fMsg.MESSAGE = message;
                fMsg.TITLE = title;
                fMsg.MSG_ICON = eMessageIcon.Unknow;
                fMsg.MSG_TON = eMessageTon.Information;

                result = fMsg.ShowDialog();
            }

            return result;
        }





        //public void UpdateFullScreen(Form form)
        //{
        //    isFullScreen = true;
        //    FullScreenHandle.StartFullScreen(form);
        //    base.SuspendLayout();
        //}

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    if (isFullScreen)
        //    {
        //        isFullScreen = false;
        //        
        //    }
        //    base.OnClosing(e);
        //}
    }
}