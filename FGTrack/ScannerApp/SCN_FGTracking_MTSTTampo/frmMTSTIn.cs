using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using HTN.BITS.FGTRACK.LIB;
using Intermec.DataCollection;
using System.Net;
using System.Globalization;
using HTN.BITS.FGTRACK.MTSTTAMPO.Components;

namespace HTN.BITS.FGTRACK.MTSTTAMPO
{
    public partial class frmMTSTIn : BaseFormFullMode, IDisposable
    {
        public frmMTSTIn()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txtNoOfBox.Text = string.Empty;
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

        private int _Pcs_Scann;
        private int _Pcs_Total;

        private int _Box_Scann;
        private int _Box_Total;

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
                   
                    //case "txtNoOfBox":
                    //    //this.ClearDataOnScreen();
                    //    //do some method
                    //    this.txtNoOfBox.Text = resultData;
                    //    this.GetDeliveryOrderInfo(resultData);

                    //    break;
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetUpdateProductCardDetail(this.txtSERIAL_NO.Text, resultData);
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

        //private void GetDeliveryOrderInfo(string doNo)
        //{
        //    string outMsg = string.Empty;
        //    ServiceRef.DeliveryOrderInfo doInfo = null;
        //    try
        //    {
        //        base.ShowWaitProcess();
        //        doInfo = ServiceProvider.Instance.Proxy.GetDeliveryOrderInfo(doNo, this.USER_ID, out outMsg);
        //        base.HideWaitProcess();

        //        if (doInfo != null)
        //        {
        //            this.ClearDataOnScreen();

        //            this.txtDO_NO.ReadOnly = true;

        //            this.txtSERIAL_NO.Text = string.Empty;
        //            this.txtSERIAL_NO.Focus();

        //        }
        //        else
        //        {
        //            base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

        //            this.txtDO_NO.Text = string.Empty;
        //            this.txtDO_NO.Focus();

        //        }
        //    }
        //    catch (WebException wex)
        //    {
        //        base.HideWaitProcess();

        //        base.ShowErrorBox(wex.Message, "WebException");

        //        this.txtDO_NO.Text = string.Empty;
        //        this.txtDO_NO.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        base.HideWaitProcess();

        //        base.ShowErrorBox(ex.Message, "Exception");

        //        this.txtDO_NO.Text = string.Empty;
        //        this.txtDO_NO.Focus();
        //    }
        //}

        private void GetUpdateProductCardDetail(string doNo, string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetUpdatePC_MTST_In_Tampo(serialNo, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    //this.lblSERIAL_NO.Text = serialNo;
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblJOB_NO.Text = prdInfo.JOB_NO;
                    this.lblSHIFT.Text = prdInfo.JOB_LOT;
                    this.lblQty.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;
                    this.lblMC_NO.Text = prdInfo.MC_NO;
                    //** Remark : ASG_NG = Scan Qty , BOX_QTY for Total DO Qty
                  //  this.lblTotalPCS.Text = string.Format("{0:#,##0} / {1:#,##0}", prdInfo.ASG_NG, prdInfo.BOX_QTY);
                    //** Remark : NG_QTY = Scan Box , BOX_SCANNED for Total DO Box
                    //this.lblTotalBox.Text = string.Format("{0:#,##0} / {1:#,##0}", prdInfo.NG_QTY, prdInfo.BOX_SCANNED);
                    this._Box_Scann++;

                   this.lblNo_Box_Scaned.Text = string.Format("{0}/{1}", this._Box_Scann, this._Box_Total);
                   this.txtSERIAL_NO.Text = string.Empty;
                    if (this._Box_Scann == this._Box_Total)
                    {
                        base.StartPlayTon_Complete();

                        this._Box_Scann = 0;
                        this._Box_Total = 0;
                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txtNoOfBox.Text = string.Empty;
                        this.txtNoOfBox.Focus();
                    }

                    //if (prdInfo.ASG_NG == prdInfo.BOX_QTY)
                    //{
                    //    base.ShowCompletelyBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    //    this.txtSERIAL_NO.Text = string.Empty;
                    //    this.txtSERIAL_NO.Focus();

                    //    //this.txtDO_NO.ReadOnly = false;
                    //    //this.txtDO_NO.Text = string.Empty;
                    //    //this.txtDO_NO.Focus();
                    //}
                    //else
                    //{
                    //    this.txtSERIAL_NO.Text = string.Empty;
                    //    this.txtSERIAL_NO.Focus();
                    //}

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
           // this.lblSERIAL_NO.Text = string.Empty;
            this.lblPRODUCT_NO.Text = string.Empty;
            this.lblPRODUCT_NAME.Text = string.Empty;
            this.lblMTL_TYPE.Text = string.Empty;
            this.lblJOB_NO.Text = string.Empty;
            this.lblSHIFT.Text = string.Empty;
            this.lblQty.Text = string.Empty;
            this.lblUNIT_ID.Text = string.Empty;
            this.lblMC_NO.Text = string.Empty;
          //  this.lblTotalPCS.Text = "0 / 0";
            this.lblNo_Box_Scaned.Text = "0 / 0";
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

        private void txtSERIAL_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = sender as TextBox;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtSERIAL_NO_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
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
                        if (string.IsNullOrEmpty(this.txtNoOfBox.Text))
                        {
                            this.txtNoOfBox.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetUpdateProductCardDetail(this.txtSERIAL_NO.Text, editor.Text);
                        break;
                    case Keys.Escape:
                        //rollback
                        this.ClearDataOnScreen();

                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txtNoOfBox.Text = string.Empty;

                        this.txtNoOfBox.Focus();
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

        private void frmMTSTIn_Load(object sender, EventArgs e)
        {
            this.txtNoOfBox.Focus();
        }

        private void txtNoOfBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNoOfBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.ClearDataOnScreen();

                        this._Box_Scann = 0;
                        //this._Box_Total = Convert.ToInt32(editor.Text, NumberFormatInfo.InvariantInfo);
                        this._Box_Total = int.Parse(editor.Text, NumberStyles.AllowThousands);

                        this.lblNo_Box_Scaned.Text = string.Format("{0}/{1}", this._Box_Scann, this._Box_Total);

                        this.txtSERIAL_NO.Focus();
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

        //private void txtDO_NO_GotFocus(object sender, EventArgs e)
        //{
        //    this.ActiveInputBox = sender as TextBox;
        //    BarReader.Instance.BarReader.ScannerEnable = true;
        //}

        //private void txtDO_NO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    TextBox editor = sender as TextBox;
        //    try
        //    {
        //        switch (e.KeyCode)
        //        {
        //            case Keys.Enter:
        //                if (string.IsNullOrEmpty(editor.Text)) return;

        //                this.GetDeliveryOrderInfo(editor.Text);
        //                break;
        //            case Keys.Escape:
        //                //rollback
        //                this.btn01Cancel.Focus();
        //                this.btn01Cancel_Click(this.btn01Cancel, new EventArgs());
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void txtDO_NO_LostFocus(object sender, EventArgs e)
        //{
        //    this.ActiveInputBox = null;
        //    BarReader.Instance.BarReader.ScannerEnable = false;
        //}
    }
}