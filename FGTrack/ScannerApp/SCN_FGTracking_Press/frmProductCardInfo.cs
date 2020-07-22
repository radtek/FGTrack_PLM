using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using HTN.BITS.FGTRACK.PRESS.Components;
using Intermec.DataCollection;
using System.Net;

namespace HTN.BITS.FGTRACK.PRESS
{
    public partial class frmProductCardInfo : BaseFormFullMode, IDisposable
    {
        public frmProductCardInfo()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txtSERIAL_NO.Text = string.Empty;

                this.ClearDataOnScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FullScreenHandle.StartFullScreen(this);
        }

        #region "Variable Member"

        private string _USER_ID;

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
        #endregion

        #region "Method Member"

        private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        {
            if (barcodeReaderEvent == null) return;
            try
            {
                string resultData = barcodeReaderEvent.strDataBuffer.Trim();

                this.txtSERIAL_NO.Text = resultData;
                this.GetProductCardStatus(resultData);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }

        private void GetProductCardStatus(string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard_Status prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetProductCardStatus(serialNo, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblSERIAL_NO.Text = prdInfo.SERIAL_NO;
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMC_NO.Text = prdInfo.MC_NO;
                    this.lblLABEL_STATUS.Text = prdInfo.STATUS;
                    if (prdInfo.PROCESS_DATE.HasValue)
                        this.lblPROCESS_DATE.Text = prdInfo.PROCESS_DATE.Value.ToString("dd-MM-yyyy");
                    this.lblLabelQty.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lbl01UNIT_ID.Text = prdInfo.UNIT_ID;
                    this.lblNGQty.Text = string.Format("{0:#,##0}", prdInfo.NG_QTY);
                    this.lbl02UNIT_ID.Text = prdInfo.UNIT_ID;
                    this.lblREPQty.Text = string.Format("{0:#,##0}", prdInfo.REP_QTY);
                    this.lbl03UNIT_ID.Text = prdInfo.UNIT_ID;

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                this.ClearDataOnScreen();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txtSERIAL_NO.Text = string.Empty;
                this.txtSERIAL_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                this.ClearDataOnScreen();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtSERIAL_NO.Text = string.Empty;
                this.txtSERIAL_NO.Focus();
            }
        }

        private void ClearDataOnScreen()
        {
            this.lblSERIAL_NO.Text = string.Empty;
            this.lblPRODUCT_NO.Text = string.Empty;
            this.lblPRODUCT_NAME.Text = string.Empty;
            this.lblMC_NO.Text = string.Empty;
            this.lblLABEL_STATUS.Text = string.Empty;
            this.lblPROCESS_DATE.Text = string.Empty;


            this.lblLabelQty.Text = string.Empty;
            this.lbl01UNIT_ID.Text = string.Empty;

            this.lblNGQty.Text = string.Empty;
            this.lbl02UNIT_ID.Text = string.Empty;

            this.lblREPQty.Text = string.Empty;
            this.lbl03UNIT_ID.Text = string.Empty;
        }

        #endregion

        private void frmProductCardInfo_Load(object sender, EventArgs e)
        {
            this.txtSERIAL_NO.Focus();
        }

        private void txtSERIAL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetProductCardStatus(editor.Text);
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSERIAL_NO_GotFocus(object sender, EventArgs e)
        {
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtSERIAL_NO_LostFocus(object sender, EventArgs e)
        {
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.QR_BarcodeRead);
            BarReader.Instance.BarReader.ThreadedRead(false);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}