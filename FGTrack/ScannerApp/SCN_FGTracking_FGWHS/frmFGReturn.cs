using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.FGWHS.Components;
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using Intermec.DataCollection;
using System.Net;

namespace HTN.BITS.FGTRACK.FGWHS
{
    public partial class frmFGReturn : BaseFormFullMode, IDisposable
    {
        public frmFGReturn()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txt01RT_NO.Text = string.Empty;
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
        private TextBox tempTextBox;

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

        private TextBox ActiveInputBox
        {
            get { return tempTextBox; }
            set { tempTextBox = value; }
        }
        #endregion

        #region "Method Member"

        private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        {
            if (barcodeReaderEvent == null) return;
            try
            {
                string resultData = barcodeReaderEvent.strDataBuffer.Trim();

                switch (this.ActiveInputBox.Name)
                {
                    case "txt01RT_NO":
                        this.ClearDataOnScreen();
                        this.txt01RT_NO.Text = resultData;
                        this.GetFGReturnInfo(resultData);
                        break;
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetProductCardDetail(this.txt01RT_NO.Text, resultData);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }

        private void GetFGReturnInfo(string qcReturnNo)
        {
            string outMsg = string.Empty;
            ServiceRef.QCReturn qcReturn = null;
            try
            {
                base.ShowWaitProcess();
                qcReturn = ServiceProvider.Instance.Proxy.GetQCReturnOrderInfo(qcReturnNo, this.USER_ID, out outMsg);
                base.HideWaitProcess();

                if (qcReturn != null)
                {
                    this.lblWH.Text = qcReturn.WH_ID;
                    this.lblTotalBoxQty.Text = string.Format("{0:#,##0}", qcReturn.NO_OF_LABEL);
                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();

                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txt01RT_NO.Text = string.Empty;
                    this.txt01RT_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txt01RT_NO.Text = string.Empty;
                this.txt01RT_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txt01RT_NO.Text = string.Empty;
                this.txt01RT_NO.Focus();
            }
        }

        private void GetProductCardDetail(string qcReturnNo, string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();
                prdInfo = ServiceProvider.Instance.Proxy.GetReturnProductCardInfo(qcReturnNo, serialNo, this.USER_ID, out outMsg);
                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblJOB_NO.Text = prdInfo.JOB_NO;
                    this.lblSHIFT.Text = prdInfo.SHIFT;
                    this.txtQTY.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;

                    this.txtQTY.Focus();
                    this.txtQTY.SelectAll();
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

        private void UpdateReturnProductCard(string qcReturnNo, string serialNo)
        {
            int totalBox = -1;
            string resultMsg = string.Empty;
            try
            {
                base.ShowWaitProcess();
                resultMsg = ServiceProvider.Instance.Proxy.UpdateReturnProductCard(qcReturnNo, serialNo, this.USER_ID, out totalBox);
                base.HideWaitProcess();

                if (resultMsg.Equals("OK"))
                {
                    this.ClearDataOnScreen();

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();

                    this.lblTotalBoxQty.Text = string.Format("{0:#,##0}", totalBox);
                }
                else
                {
                    base.ShowErrorBox(resultMsg,
                                ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtQTY.Focus();
                    this.txtQTY.SelectAll();
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");
                this.txtQTY.Focus();
                this.txtQTY.SelectAll();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");
                this.txtQTY.Focus();
                this.txtQTY.SelectAll();
            }
        }


        private void ClearDataOnScreen()
        {
            this.lblPRODUCT_NO.Text = string.Empty;
            this.lblPRODUCT_NAME.Text = string.Empty;
            this.lblMTL_TYPE.Text = string.Empty;
            this.lblJOB_NO.Text = string.Empty;
            this.lblSHIFT.Text = string.Empty;
            this.txtQTY.Text = string.Empty;
            this.lblUNIT_ID.Text = string.Empty;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            //remove event handle
            BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.QR_BarcodeRead);
            BarReader.Instance.BarReader.ThreadedRead(false);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void txt01RT_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = (TextBox)sender;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txt01RT_NO_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void txtSERIAL_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = (TextBox)sender;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtSERIAL_NO_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void txt01RT_NO_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetFGReturnInfo(editor.Text);
                        break;
                    case Keys.Escape:
                        //rollback
                        this.btn01Cancel.Focus();
                        this.btn01Cancel_Click(this.btn01Cancel, new EventArgs());
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

        private void txtSERIAL_NO_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(this.txt01RT_NO.Text))
                        {
                            this.txt01RT_NO.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetProductCardDetail(this.txt01RT_NO.Text, editor.Text);
                        break;
                    case Keys.Escape:
                        //rollback
                        this.lblWH.Text = string.Empty;
                        this.lblTotalBoxQty.Text = "0";

                        this.ClearDataOnScreen();

                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txt01RT_NO.Text = string.Empty;

                        this.txt01RT_NO.Focus();
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

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFGReturn_Load(object sender, EventArgs e)
        {
            this.txt01RT_NO.Focus();
        }

        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 8:
                    break;
                case 13:
                    break;
                case 27:
                    break;
                default:
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                        e.Handled = true;
                    break;
            }
        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:

                    this.UpdateReturnProductCard(this.txt01RT_NO.Text, this.txtSERIAL_NO.Text);
                    break;
                case Keys.Escape:
                    //rollback
                    this.ClearDataOnScreen();

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();
                    break;
                default:
                    break;
            }
        }

        private void btn01Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt01RT_NO.Text))
            {
                this.txt01RT_NO.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSERIAL_NO.Text))
            {
                this.txtSERIAL_NO.Focus();
                return;
            }

            try
            {
                this.UpdateReturnProductCard(this.txt01RT_NO.Text, this.txtSERIAL_NO.Text);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");

                this.txtQTY.Text = string.Empty;
                this.txtQTY.Focus();
            }
        }

    }
}