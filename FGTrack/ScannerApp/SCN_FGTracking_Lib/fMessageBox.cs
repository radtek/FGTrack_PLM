using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HTN.BITS.FGTRACK.LIB
{
    public enum eMessageIcon
    {
        Safe,
        Warning,
        Danger,
        Unknow
    }

    public enum eMessageTon
    {
        None,
        Information,
        Warning,
        Dangerous,
        Completely
    }

    public partial class fMessageBox : Form, IDisposable
    {
        public fMessageBox(Form form)
        {
            try
            {
                InitializeComponent();

                int iLocTop = ((form.Height / 2) - (this.Height / 2)) - 20;
                int iLocLeft = (form.Width / 2) - (this.Width / 2);

                this.Location = new Point(iLocLeft, iLocTop);
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.Ignore;
            }
        }

        #region "Variable Member"

        private string _TITLE;
        private string _MESSAGE;
        private eMessageIcon _MSG_ICON;
        private eMessageTon _MSG_TON;

        #endregion

        #region "Property Member"

        public string TITLE
        {
            get
            {
                return _TITLE;
            }
            set
            {
                if (_TITLE == value)
                    return;
                _TITLE = value;
            }
        }
        public string MESSAGE
        {
            get
            {
                return _MESSAGE;
            }
            set
            {
                if (_MESSAGE == value)
                    return;
                _MESSAGE = value;
            }
        }
        public eMessageIcon MSG_ICON
        {
            get
            {
                return _MSG_ICON;
            }
            set
            {
                if (_MSG_ICON == value)
                    return;
                _MSG_ICON = value;
            }
        }
        public eMessageTon MSG_TON
        {
            get
            {
                return _MSG_TON;
            }
            set
            {
                if (_MSG_TON == value)
                    return;
                _MSG_TON = value;
            }
        }

        #endregion

        private void fMessageBox_Load(object sender, EventArgs e)
        {
            this.StartPlayTon();
            
            this.lblTitle.Text = this._TITLE;
            this.lblMessage.Text = this._MESSAGE;

            this.Text = string.Empty;

            switch (this._MSG_ICON)
            {
                case eMessageIcon.Safe:
                    this.picImage.Image = ResourceManager.Instance.GetBitmap("Safe_Shield");
                    this.btnOK.Visible = true;
                    break;
                case eMessageIcon.Warning:
                    this.picImage.Image = ResourceManager.Instance.GetBitmap("Warning_Shield");
                    this.btnOK.Visible = true;
                    break;
                case eMessageIcon.Danger:
                    this.picImage.Image = ResourceManager.Instance.GetBitmap("Danger_Shield");
                    this.btnOK.Visible = true;
                    break;
                case eMessageIcon.Unknow:
                    this.picImage.Image = ResourceManager.Instance.GetBitmap("Unknown_Shield");
                    this.btnYes.Visible = true;
                    this.btnNo.Visible = true;

                    break;
                default:
                    break; 
            }
        }

        public void StartPlayTon()
        {
            ThreadStart operation = new ThreadStart(PlayTone_Thread);
            Thread thr = new Thread(operation);
            thr.Start();
        }
        private void PlayTone_Thread()
        {
            try
            {
               this.tonMessage.CurrentVolume = Intermec.Device.Audio.Tone.VOLUME.VERY_LOUD;

                switch (this._MSG_TON)
                {
                    case eMessageTon.None:
                        //nothing
                        break;
                    case eMessageTon.Information:
                        this.tonMessage.CurrentDuration = 120;
                        this.tonMessage.CurrentPitch = 800;

                        for (int i = 0; i <= 1; i++)//2 time;
                        {
                            this.tonMessage.Play();
                        }
                        break;
                    case eMessageTon.Completely:
                        this.tonMessage.CurrentDuration = 120;
                        this.tonMessage.CurrentPitch = 1400;
                        for (int i = 0; i <= 1; i++)//2 time;
                        {
                            this.tonMessage.Play();
                        }
                        break;
                    case eMessageTon.Warning:
                        this.tonMessage.CurrentDuration = 150;
                        this.tonMessage.CurrentPitch = 300;
                        for (int i = 0; i <= 1; i++)//2 time;
                        {
                            this.tonMessage.Play();
                        }
                        break;
                    case eMessageTon.Dangerous:
                        this.tonMessage.CurrentDuration = 150;
                        this.tonMessage.CurrentPitch = 300;

                        for (int i = 0; i <= 1; i++)//2 time;
                        {
                            this.tonMessage.Play();
                        }
                        break;
                    default:
                        //nothing
                        break;
                }

                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void fMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (this._MSG_ICON == eMessageIcon.Unknow)
                    {
                        DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                    break;
                case Keys.Escape:
                    if (this._MSG_ICON == eMessageIcon.Unknow)
                    {
                        DialogResult = DialogResult.No;
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                    break;
                default:
                    break;
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            this.tonMessage.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}