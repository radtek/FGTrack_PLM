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
    public partial class frmInputTRES : BaseFormDialogMode, IDisposable
    {
        private frmMixing frmMixingMain;

        public frmInputTRES()
        {
            InitializeComponent();
        }

        public frmInputTRES(Form form)
        {
            try
            {
                frmMixingMain = (frmMixing)form;
                InitializeComponent();

                this.Height = 78;
                this.Width = 213;

                int iLocTop = ((form.Height / 2) - (this.Height / 2)) - 20;
                int iLocLeft = (form.Width / 2) - (this.Width / 2);


                this.Location = new Point(iLocLeft, iLocTop);

                base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

                this.txtTRES.Focus();
                this.txtTRES.SelectAll();

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

        private void txtTRES_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: //Logoff
                    this.confirmTRES();

                    break;
                default:
                    break;
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
            GC.SuppressFinalize(this);
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.confirmTRES();
        }

        private void confirmTRES()
        {
            try
            {
                if (String.IsNullOrEmpty(txtTRES.Text))
                {
                    MessageBox.Show(ResourceManager.Instance.GetString("MSG_TRES_VAL_OVER"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    this.txtTRES.ReadOnly = false;
                    this.txtTRES.Focus();
                }
                else if (Decimal.Parse(txtTRES.Text) > 100 && Decimal.Parse(txtTRES.Text) != 0)
                {
                    MessageBox.Show(ResourceManager.Instance.GetString("MSG_TRES_VAL_OVER"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    this.txtTRES.ReadOnly = false;
                    this.txtTRES.Focus();
                }
                else
                {
                    decimal decimalVal = 0;
                    decimalVal = System.Convert.ToDecimal(txtTRES.Text);
                    frmMixingMain.StartMixing(decimalVal);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TRES value must be numeric", "Please check", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }
    }
}