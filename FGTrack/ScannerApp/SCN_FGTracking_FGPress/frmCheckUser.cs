using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.FGPRESS.Components;
using HTN.BITS.FGTRACK.LIB;

//declare for user barcode reader
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using Intermec.DataCollection;

namespace HTN.BITS.FGTRACK.FGPRESS
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
                    BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                    BarReader.Instance.BarReader.ThreadedRead(true);
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

        private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        {
            string result = string.Empty;
            if (barcodeReaderEvent == null) return;
            try
            {
                //user id
                this.txtScanUserID.Text = barcodeReaderEvent.strDataBuffer;

                this.txtScanUserID.SelectionStart = barcodeReaderEvent.strDataBuffer.Length;
                this._USER_ID = barcodeReaderEvent.strDataBuffer.Trim();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void txtScanUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void txtScanUserID_GotFocus(object sender, EventArgs e)
        {
            if (!BarReader.Instance.BarReader.ScannerEnable)
                BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtScanUserID_LostFocus(object sender, EventArgs e)
        {
            if (BarReader.Instance.BarReader.ScannerEnable)
                BarReader.Instance.BarReader.ScannerEnable = false;
        }



        #region IDisposable Members

        void IDisposable.Dispose()
        {
            BarReader.Instance.BarReader.ScannerEnable = false;
            //remove event handle
            BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.QR_BarcodeRead);
            BarReader.Instance.BarReader.ThreadedRead(false);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}