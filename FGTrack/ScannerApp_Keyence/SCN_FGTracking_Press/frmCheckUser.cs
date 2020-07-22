using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.PRESS.Components;
using HTN.BITS.FGTRACK.LIB;

//declare for user barcode reader
using Bt;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.PRESS
{
    public partial class frmCheckUser : BaseFormDialogMode, IDisposable
    {
        public frmCheckUser()
        {
            InitializeComponent();
        }

        public frmCheckUser(Form form)
        {
            try
            {
                InitializeComponent();

                int iLocTop = ((form.Height / 2) - (this.Height / 2)) - 20;
                int iLocLeft = (form.Width / 2) - (this.Width / 2);

                this.Location = new Point(iLocLeft, iLocTop);

                base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);


                try
                {
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
                //    disp = "btScanGetStringSize error ret[" + codeLen + "]";
                 //   MessageBox.Show(disp, "Error");
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
              //  MessageBox.Show(e.ToString());
            }
        }
        #endregion

        private void txtScanUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void txtScanUserID_GotFocus(object sender, EventArgs e)
        {
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

        private void txtScanUserID_LostFocus(object sender, EventArgs e)
        {
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
            //remove event handle
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}