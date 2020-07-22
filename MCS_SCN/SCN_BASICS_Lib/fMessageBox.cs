using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Bt;
using Bt.SysLib;

namespace HTN.BITS.MCS.SCN.LIB
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
                        if (true)
                        {
                            this.PlayInformationSound();
                        }
                        else
                        {
                            this.tonMessage.CurrentDuration = 120;
                            this.tonMessage.CurrentPitch = 800;

                            for (int i = 0; i <= 1; i++)//2 time;
                            {
                                this.tonMessage.Play();
                            }
                        }
                        
                        break;
                    case eMessageTon.Completely:
                        if (true)
                        {
                            this.PlayCompletelySound();
                        }
                        else
                        {
                            this.tonMessage.CurrentDuration = 120;
                            this.tonMessage.CurrentPitch = 1400;
                            for (int i = 0; i <= 1; i++)//2 time;
                            {
                                this.tonMessage.Play();
                            }
                        }
                        break;
                    case eMessageTon.Warning:
                        if (true)
                        {
                            this.PlayDangerousSound();
                        }
                        else
                        {
                            this.tonMessage.CurrentDuration = 150;
                            this.tonMessage.CurrentPitch = 300;
                            for (int i = 0; i <= 1; i++)//2 time;
                            {
                                this.tonMessage.Play();
                            }
                        }
                        break;
                    case eMessageTon.Dangerous:
                        if (true)
                        {
                            this.PlayDangerousSound();
                        }
                        else
                        {
                            this.tonMessage.CurrentDuration = 150;
                            this.tonMessage.CurrentPitch = 300;

                            for (int i = 0; i <= 1; i++)//2 time;
                            {
                                this.tonMessage.Play();
                            }
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

        private void PlayInformationSound()
        {
            Int32 ret = 0;
            String disp = "";



            LibDef.BT_BUZZER_PARAM stBuzzerSet = new LibDef.BT_BUZZER_PARAM();			// Buzzer control structure (Set)
            // Set to repeat "500 ms on, 500 ms off" 3 times
            stBuzzerSet.dwOn = 100;		// Rumble time [ms] (1 to 5000)
            stBuzzerSet.dwOff = 100;		// Stop time [ms] (0 to 5000)
            stBuzzerSet.dwCount = 1;	// Rumble count [times] (0 to 100)
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
            stLedSet.dwCount = 1;					// Rumble count [times] (0 to 100)
            stLedSet.bColor = LibDef.BT_LED_GREEN;	// Light color

            try
            {
                // btBuzzer Rumble
                ret = Device.btBuzzer(1, stBuzzerSet);
                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btBuzzer");
                    return;
                }
                // btVibrator Rumble
                //     ret = Device.btVibrator(1, stVibSet);
                // btLED Light on
                ret = Device.btLED(1, stLedSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btLED");
                    return;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    MessageBox.Show(disp, "Error btBuzzer");
                    return;
                }
                // btVibrator Rumble
                //     ret = Device.btVibrator(1, stVibSet);
                // btLED Light on
                ret = Device.btLED(1, stLedSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btLED");
                    return;
                }
                //if (ret != LibDef.BT_OK)
                //{
                //    disp = "error ret[" + ret + "]";
                //    MessageBox.Show(disp, "Error");
                //    return;
                //}




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PlayDangerousSound()
        {
            Int32 ret = 0;
            String disp = "";

            LibDef.BT_BUZZER_PARAM stBuzzerSet = new LibDef.BT_BUZZER_PARAM();			// Buzzer control structure (Set)
            // Set to repeat "500 ms on, 500 ms off" 3 times
            stBuzzerSet.dwOn = 100;		// Rumble time [ms] (1 to 5000)
            stBuzzerSet.dwOff = 100;		// Stop time [ms] (0 to 5000)
            stBuzzerSet.dwCount = 2;	// Rumble count [times] (0 to 100)
            stBuzzerSet.bTone = 1;//16;		// Musical scale (1 to 16)
            stBuzzerSet.bVolume = 3;	// Buzzer volume (1 to 3)

            LibDef.BT_VIBRATOR_PARAM stVibSet = new LibDef.BT_VIBRATOR_PARAM();		// Vibrator operation structure (Set)
            // Set to repeat "500 ms on, 500 ms off" 3 times
            stVibSet.dwOn = 100;		// Rumble time [ms] (1 to 5000)
            stVibSet.dwOff = 100;			// Stop time [ms] (0 to 5000)
            stVibSet.dwCount = 2;		// Rumble count [times] (0 to 100)

            LibDef.BT_LED_PARAM stLedSet = new LibDef.BT_LED_PARAM();				// LED control structure (Set)

            // Set to repeat "500 ms on, 500 ms off" 3 times
            stLedSet.dwOn = 100;					// Rumble time [ms] (1 to 5000)
            stLedSet.dwOff = 100;						// Stop time [ms] (0 to 5000)
            stLedSet.dwCount = 2;					// Rumble count [times] (0 to 100)
            stLedSet.bColor = LibDef.BT_LED_RED;	// Light color


            try
            {
                // btBuzzer Rumble
                ret = Device.btBuzzer(1, stBuzzerSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btBuzzer");
                    return;
                }
                // btVibrator Rumble
                ret = Device.btVibrator(1, stVibSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btVibrator");
                    return;
                }
                // btLED Light on
                ret = Device.btLED(1, stLedSet);

                if (ret != LibDef.BT_OK)
                {
                    disp = "error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error btLED");
                    return;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                case Keys.F1:
                    this.Close();
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