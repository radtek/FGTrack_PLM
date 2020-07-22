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
                ResourceManager.Instance.Culture = new System.Globalization.CultureInfo(lanuage);
                base.UpdateResources();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                this.tone1.CurrentVolume = Intermec.Device.Audio.Tone.VOLUME.VERY_LOUD;
                this.tone1.CurrentDuration = 120;
                this.tone1.CurrentPitch = 1400;
                for (int i = 0; i <= 1; i++)//2 time;
                {
                    this.tone1.Play();
                }
            }
            catch (Exception ex)
            {

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
        //        FullScreenHandle.StopFullScreen(this);
        //    }
        //    base.OnClosing(e);
        //}
    }
}