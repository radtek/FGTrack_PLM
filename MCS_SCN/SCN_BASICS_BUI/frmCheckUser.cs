using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.MCS.SCN.LIB;
using HTN.BITS.MCS.SCN.UIL.Components;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using Bt;

using BarReader = HTN.BITS.MCS.SCN.LIB.Scanner.clsBarcodeReader;
using Intermec.DataCollection;


namespace HTN.BITS.MCS.SCN.UIL
{
    public partial class frmCheckUser : BaseFormDialogMode, IDisposable
    {

        public frmCheckUser()
        {
            InitializeComponent();
        }

        public frmCheckUser(Form form, string targetForm)
        {
            try
            {
                InitializeComponent();

                switch (targetForm)
                {
                    case "frmMixing":
                        this.BackColor = Color.FromArgb(167, 0, 174);
                        this.imgUser.Image =  HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.GetBitmap("User_Purple");
                        break;
                    case "frmReplenish":
                        this.BackColor = Color.FromArgb(0, 166, 0);
                        this.imgUser.Image = HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.GetBitmap("User_Green");
                        break;

                    default:
                        break;
                }


                int iLocTop = ((form.Height / 2) - (this.Height / 2)) - 20;
                int iLocLeft = (form.Width / 2) - (this.Width / 2);

                this.Location = new Point(iLocLeft, iLocTop);

                base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);


                try
                {
                    //BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                    //BarReader.Instance.BarReader.ThreadedRead(true);

                    //tangmo Add
                    this.MsgWin = new MsgWindow();
                    this.MsgWin.BarcodeRead += new Action(MsgWin_BarcodeRead);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region "Variable Member"
        private MsgWindow MsgWin;
        private string _USER_ID;
        private string _USER_NAME;
        #endregion

        #region "Property Member"

        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }
        public string USER_NAME
        {
            get
            {
                return _USER_NAME;
            }
            set
            {
                if (_USER_NAME == value)
                    return;
                _USER_NAME = value;
            }
        }
        #endregion

        #region "Method Member"

        //private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        //{
        //    string result = string.Empty;
        //    if (barcodeReaderEvent == null) return;
        //    try
        //    {
        //        //user id
        //        this.txtScanUserID.Text = barcodeReaderEvent.strDataBuffer;

        //        this.txtScanUserID.SelectionStart = barcodeReaderEvent.strDataBuffer.Length;
        //        this._USER_ID = barcodeReaderEvent.strDataBuffer.Trim();

        //        DialogResult = DialogResult.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        void MsgWin_BarcodeRead()
        {
            Int32 ret = 0;
            String disp = "";
            Byte[] codedataGet;
            Int32 codeLen = 0;
            UInt16 symbolGet = 0;

            try
            {
                //-----------------------------------------------------------
                // Reading (batch)
                //-----------------------------------------------------------
                codeLen = Bt.ScanLib.Control.btScanGetStringSize();
                if (codeLen <= 0)
                {
                         disp = "btScanGetStringSize error ret[" + codeLen + "]";
                         MessageBox.Show(disp, "Error");
                }

                codedataGet = new Byte[codeLen];

                ret = Bt.ScanLib.Control.btScanGetString(codedataGet, ref symbolGet);
                if (ret != LibDef.BT_OK)
                {
                    disp = "btScanGetString error ret[" + ret + "]";
                    MessageBox.Show(disp, "Error");
                }

                ret = Bt.ScanLib.Control.btScanDisable();
                if (ret != LibDef.BT_OK)
                {
                    MessageBox.Show("btScanDisable error ret[" + ret + "]", "Error");
                }

                disp = System.Text.Encoding.GetEncoding(932).GetString(codedataGet, 0, codeLen);

                this.txtScanUserID.Text = disp;

                this.txtScanUserID.SelectionStart = disp.Length;
                this._USER_ID = disp.Trim();

                DialogResult = DialogResult.OK;
            }
            catch (Exception e)
            {
                 MessageBox.Show(e.ToString());
            }
        }

        #endregion

        private void frmCheckUser_Load(object sender, EventArgs e)
        {
            //this.imgUser.Image = Properties.Resources.Icon_User2; 
        }

        //private void frmCheckUser_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        DialogResult = DialogResult.Cancel;
        //    }
        //}

        private void txtScanUserID_GotFocus(object sender, EventArgs e)
        {
            //if (!BarReader.Instance.BarReader.ScannerEnable)
            //    BarReader.Instance.BarReader.ScannerEnable = true;

            // tangmo Add
            Int32 ret = 0;

            try
            {
                ret = Bt.ScanLib.Control.btScanEnable();
                if (ret != LibDef.BT_OK)
                {
                    MessageBox.Show("btScanEnable error ret[" + ret + "]", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtScanUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtScanUserID_LostFocus(object sender, EventArgs e)
        {
            //if (BarReader.Instance.BarReader.ScannerEnable)
            //    BarReader.Instance.BarReader.ScannerEnable = false;

            //tangmo Add

            Int32 ret = 0;

            try
            {

                ret = Bt.ScanLib.Control.btScanDisable();
                if (ret != LibDef.BT_OK)
                {
                    MessageBox.Show("btScanDisable error ret[" + ret + "]", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            /*
            BarReader.Instance.BarReader.ScannerEnable = false;
            //remove event handle
            BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.QR_BarcodeRead);
            BarReader.Instance.BarReader.ThreadedRead(false);
            */
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void txtScanUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}