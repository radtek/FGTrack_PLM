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
using HTN.BITS.FGTRACK.FGWHS.Components;
using Intermec.DataCollection;
using System.Net;

namespace HTN.BITS.FGTRACK.FGWHS
{
    public partial class frmPicking : BaseFormFullMode, IDisposable
    {
        public frmPicking()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txt01PICKING_NO.Text = string.Empty;
                this._Qty_Scann = 0;
                this._Qty_Total = 0;

                this.lblNo_Qty_Scaned.Text = string.Empty;

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

        private int _Qty_Scann;
        private int _Qty_Total;

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
                    case "txt01PICKING_NO":
                        this.ClearDataOnScreen();
                        //do some method
                        this.txt01PICKING_NO.Text = resultData;
                        this.GetPickingDetail(resultData);
                        break;
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetUpdateProductCardDetail(this.txt01PICKING_NO.Text, resultData);
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

        private void GetPickingDetail(string pickingNo)
        {
            string outMsg = string.Empty;
            ServiceRef.PickQty pickInfo = null;
            try
            {
                base.ShowWaitProcess();
                pickInfo = ServiceProvider.Instance.Proxy.GetPickInfo(pickingNo, this.USER_ID, out outMsg);
                base.HideWaitProcess();

                if (pickInfo != null)
                {
                    this._Qty_Scann = pickInfo.PICKED_QTY;
                    this._Qty_Total = pickInfo.QTY;
                    this.lblNo_Qty_Scaned.Text = string.Format("{0:#,##0} / {1:#,##0}", this._Qty_Scann, this._Qty_Total);
                    this.lbl02UNIT_ID.Text = pickInfo.UNIT_ID;

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();
                    
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txt01PICKING_NO.Text = string.Empty;
                    this.txt01PICKING_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txt01PICKING_NO.Text = string.Empty;
                this.txt01PICKING_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txt01PICKING_NO.Text = string.Empty;
                this.txt01PICKING_NO.Focus();
            }
        }

        private void GetUpdateProductCardDetail(string pickingNo, string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetUpdatePCPicking(pickingNo, serialNo, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblSERIAL_NO.Text = serialNo;
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblJOB_NO.Text = prdInfo.JOB_NO;
                    this.lblSHIFT.Text = prdInfo.JOB_LOT;
                    this.lblQty.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();

                    this._Qty_Scann = prdInfo.BOX_QTY;

                    this.lblNo_Qty_Scaned.Text = string.Format("{0:#,##0}/{1:#,##0}", this._Qty_Scann, this._Qty_Total);

                    if (this._Qty_Scann == this._Qty_Total)
                    {
                        base.StartPlayTon_Complete();

                        this._Qty_Scann = 0;
                        this._Qty_Total = 0;

                        this.txt01PICKING_NO.Text = string.Empty;
                        this.txt01PICKING_NO.Focus();
                    }
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
            this.lblMTL_TYPE.Text = string.Empty;
            this.lblJOB_NO.Text = string.Empty;
            this.lblSHIFT.Text = string.Empty;
            this.lblQty.Text = string.Empty;
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

        private void txt01PICKING_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = (TextBox)sender;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txt01PICKING_NO_LostFocus(object sender, EventArgs e)
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

        private void txtSERIAL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(this.txt01PICKING_NO.Text))
                        {
                            this.txt01PICKING_NO.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetUpdateProductCardDetail(this.txt01PICKING_NO.Text, editor.Text);
                        break;
                    case Keys.Escape:
                        //rollback
                        this._Qty_Scann = 0;
                        this._Qty_Total = 0;

                        this.lblNo_Qty_Scaned.Text = string.Format("{0:#,##0}/{1:#,##0}", this._Qty_Scann, this._Qty_Total);

                        this.ClearDataOnScreen();

                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txt01PICKING_NO.Text = string.Empty;

                        this.txt01PICKING_NO.Focus();
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

        private void txt01PICKING_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetPickingDetail(editor.Text);
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

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPicking_Load(object sender, EventArgs e)
        {
            this.txt01PICKING_NO.Focus();
        }

    }
}