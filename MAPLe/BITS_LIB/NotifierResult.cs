using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;

namespace HTN.BITS.LIB
{
    public partial class NotifierResult : DevExpress.XtraEditors.XtraForm
    {
        private static int m_nShowTime = 50; //show time in ms
        private static int m_nShowWait = 50; //show time in ms
        private static int m_nHideTime = 50; //hide time in ms

        public NotifierResult()
        {
            InitializeComponent();

            if (m_nHideTime > 0)
            {
                tmrHide.Interval = m_nHideTime;
            }

            tmrShow.Interval = m_nShowTime;
            tmrShowWait.Interval = m_nShowWait;

            Location = new Point(Screen.PrimaryScreen.Bounds.Width - (this.Width + 1), 22); //Screen.PrimaryScreen.Bounds.Height - this.Height - 60
        }

        private static NotifierResult g_Fio = null;
        private static string m_strPreviousMessage = "";

        /// <summary>
        /// Shows the specified STR message.
        /// </summary>
        /// <param name="strMessage">The STR message.</param>
        private static void Show(string strMessage, string caption, NotifyType notityType)
        {
            try
            {
                if (m_strPreviousMessage == strMessage)
                {
                    return;
                }
                else
                {
                    m_strPreviousMessage = strMessage;
                }

                NotifierResult theNotifio = new NotifierResult();
                g_Fio = theNotifio;

                theNotifio.grpCaption.Text = caption;
                theNotifio.lblMessaageResult.Text = strMessage;
                theNotifio.picNotifier.Image = theNotifio.imgNotifierList.Images[(int)notityType];
                theNotifio.Show();
                //theNotifio.panel1.Focus();
            }
            catch (Exception exc)
            {
                ;
            }

        }

        /// <summary>
        /// Shows the specified STR message.
        /// </summary>
        /// <param name="strMessage">The STR message.</param>
        /// <param name="nShowTime">The n show time.</param>
        /// <param name="nHideTime">The n hide time.</param>
        //public static void Show(string strMessage, int nShowTime, int nHideTime)
        //{
        //    m_nShowTime = nShowTime;
        //    m_nHideTime = nHideTime;

        //    Show(strMessage);
        //}

        public static void Show(string message, string caption, int nShowTime, int nWait, int nHideTime, NotifyType notifyType)
        {
            m_nShowTime = nShowTime;
            m_nShowWait = nWait;
            m_nHideTime = nHideTime;

            Show(message, caption, notifyType);
        }

        private void CloseWnd()
        {
            try
            {
                g_Fio.tmrHide.Stop();
                g_Fio.tmrShowWait.Stop();
                g_Fio.tmrShow.Stop();

                g_Fio.Close();

                m_strPreviousMessage = string.Empty;
                //g_Fio.Dispose();
                this.Close();
            }
            catch (Exception ex) 
            {
                ;
            }
        }

        private void tmrShowWait_Tick(object sender, EventArgs e)
        {
            tmrShowWait.Stop();
            if (m_nHideTime > 0)
            {
                tmrHide.Start();
            }
        }

        private void tmrHide_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.00)
            {
                this.Opacity -= 0.05;
                // this.Location = new Point(this.Location.X - 1, this.Location.Y-1);
            }
            else
            {
                tmrHide.Stop();
                CloseWnd();
                //this.Close();
            }
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 0.99)
            {
                this.Opacity += 0.05;
                //   this.Location = new Point(this.Location.X + 1, this.Location.Y+1);

            }
            else
            {
                tmrShow.Stop();
                tmrShowWait.Start();
                
            }
        }

        private void NotifierResult_Load(object sender, EventArgs e)
        {
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.CloseWnd();
        }

        private void grpCaption_Click(object sender, EventArgs e)
        {
            if (m_nHideTime > 0)
            {
                this.CloseWnd();
            }
        }

        private void picNotifier_Click(object sender, EventArgs e)
        {
            if (m_nHideTime > 0)
            {
                this.CloseWnd();
            }
        }

        private void lblMessaageResult_Click(object sender, EventArgs e)
        {
            if (m_nHideTime > 0)
            {
                this.CloseWnd();
            }
        }

        
    }

    public enum NotifyType
    {
        Danger = 0,
        Safe = 1,
        Unknow = 2,
        Warning = 3
    }
}