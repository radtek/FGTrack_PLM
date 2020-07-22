using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.HORIZONTAL.Components;
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using Intermec.DataCollection;
using System.Net;
using System.Globalization;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.HORIZONTAL
{
    public partial class frmNGConfirmRep : BaseFormFullMode, IDisposable
    {
        public frmNGConfirmRep()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            InputContext.SetAutoSuggestion(this.txtQTY.Handle, false);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                BarReader.Instance.BarReader.ScannerEnable = false;

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
        private string _MODE;

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
        public string MODE
        {
            get
            {
                return _MODE;
            }
            set
            {
                if (_MODE == value)
                    return;
                _MODE = value;
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
                this.GetProductCardDetail(resultData);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }

        private void GetProductCardDetail(string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetPC_NGInfo(serialNo, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblJOB_NO.Text = prdInfo.JOB_NO;
                    this.lblSHIFT.Text = prdInfo.JOB_LOT;

                    this.lblQty.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;
                    this.lbl02UNIT_ID.Text = prdInfo.UNIT_ID;

                    DialogResult diaResult = base.ShowQuestionBox("Replenishment ?", "Confirm");

                    switch (diaResult)
                    {
                        case DialogResult.Yes:
                            this._MODE = "Y";
                            this.lblConfirmMsg.Text = "Rep. Qty :";
                            break;
                        case DialogResult.OK:
                            this._MODE = "Y";
                            this.lblConfirmMsg.Text = "Rep. Qty :";
                            break;
                        case DialogResult.No:
                            this._MODE = "N";
                            this.lblConfirmMsg.Text = "NG. Qty :";
                            break;
                        default:
                            this._MODE = "N";
                            this.lblConfirmMsg.Text = "NG. Qty :";
                            break;
                    }

                    this.txtQTY.Text = string.Empty;
                    this.txtQTY.Focus();

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

        private void UpdateReplenishment(string serialNo, int qty)
        {
            string resultMsg = string.Empty;
            try
            {
                base.ShowWaitProcess();

                resultMsg = ServiceProvider.Instance.Proxy.UpdateReplenishConfirm(serialNo, this.MODE, qty, this.USER_ID);

                base.HideWaitProcess();

                if (resultMsg.Equals("OK"))
                {
                    base.StartPlayTon_Complete();

                    this.ClearDataOnScreen();

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();
                }
                else
                {
                    base.ShowErrorBox(resultMsg,
                                      ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtQTY.Text = string.Empty;
                    this.txtQTY.Focus();

                }


            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");
                this.txtQTY.Text = string.Empty;
                this.txtQTY.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtQTY.Text = string.Empty;
                this.txtQTY.Focus();
            }
        }

        private void ClearDataOnScreen()
        {
            this.lblPRODUCT_NO.Text = string.Empty;
            this.lblPRODUCT_NAME.Text = string.Empty;
            this.lblMTL_TYPE.Text = string.Empty;
            this.lblJOB_NO.Text = string.Empty;
            this.lblSHIFT.Text = string.Empty;
            this.lblQty.Text = string.Empty;
            this.lblUNIT_ID.Text = string.Empty;

            this.txtQTY.Text = string.Empty;
            this.lbl02UNIT_ID.Text = string.Empty;
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

        private void frmNGConfirmRep_Load(object sender, EventArgs e)
        {
            this.txtSERIAL_NO.Focus();
        }

        private void txtSERIAL_NO_GotFocus(object sender, EventArgs e)
        {
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtSERIAL_NO_LostFocus(object sender, EventArgs e)
        {
            BarReader.Instance.BarReader.ScannerEnable = false;
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

                        this.GetProductCardDetail(editor.Text);
                        break;
                    case Keys.Escape:
                        //rollback
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

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            string serialNo = string.Empty;
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        serialNo = this.txtSERIAL_NO.Text;
                        if(string.IsNullOrEmpty(serialNo))
                        {
                            this.txtSERIAL_NO.Focus();
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;
                        //int qty = Convert.ToInt32(editor.Text, NumberFormatInfo.InvariantInfo);
                        int qty = int.Parse(editor.Text, NumberStyles.AllowThousands);
                        if (qty <= 0)
                        {
                            this.txtQTY.Text = "1";
                            this.txtQTY.Focus();
                            this.txtQTY.SelectAll();

                            return;
                        }

                        this.UpdateReplenishment(serialNo, qty);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}