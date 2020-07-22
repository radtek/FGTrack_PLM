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
using HTN.BITS.FGTRACK.FGWHS.Components;
using System.Net;

namespace HTN.BITS.FGTRACK.FGWHS
{
    public partial class frmLoadPallet : BaseFormFullMode, IDisposable
    {
        public frmLoadPallet()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txtLOADING_NO.Text = string.Empty;
                this._Total_Box = 0;
                this._Total_Qty = 0;

                this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);

                this.txtPALLET_NO.Text = string.Empty;

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

        private int _Total_Box;
        private int _Total_Qty;

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
                    case "txtLOADING_NO":
                        this.ClearDataOnScreen();
                        //do some method
                        this.txtLOADING_NO.Text = resultData;
                        this.GetLoadingDetail(resultData);
                        break;
                    case "txtPALLET_NO":
                        this.txtPALLET_NO.Text = resultData;
                        this.GetUpdatePallet(this.txtLOADING_NO.Text, resultData);
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

        private void GetLoadingDetail(string loadingNo)
        {
            string outMsg = string.Empty;
            ServiceRef.LoadQty loadInfo = null;
            try
            {
                base.ShowWaitProcess();
                loadInfo = ServiceProvider.Instance.Proxy.GetLoadInfo(loadingNo, this.USER_ID, out outMsg);
                base.HideWaitProcess();

                if (loadInfo != null)
                {
                    this._Total_Box = loadInfo.LOADED_BOX;
                    this._Total_Qty = loadInfo.LOADED_QTY;

                    this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                    this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);

                    this.txtPALLET_NO.Text = string.Empty;
                    this.txtPALLET_NO.Focus();

                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), 
                        ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtLOADING_NO.Text = string.Empty;
                    this.txtLOADING_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txtLOADING_NO.Text = string.Empty;
                this.txtLOADING_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtLOADING_NO.Text = string.Empty;
                this.txtLOADING_NO.Focus();
            }
        }

        private void GetUpdatePallet(string loadingNo, string palletno)
        {
            string outMsg = string.Empty;
            ServiceRef.Pallet palletInfo = null;
            try
            {
                base.ShowWaitProcess();

                palletInfo = ServiceProvider.Instance.Proxy.GetUpdatePalletLoading(loadingNo, palletno, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (palletInfo != null)
                {
                    this.lblPALLET_NO.Text = palletInfo.PALLET_NO;
                    this.lblSO_NO.Text = palletInfo.SO_NO;

                    this.lblPARTY_NAME.Text = palletInfo.PARTY_NAME;

                    this.lblETA.Text = palletInfo.ETA;

                    this.txtPALLET_NO.Text = string.Empty;
                    this.txtPALLET_NO.Focus();

                    this.lblTotalBox.Text = string.Format("{0:#,##0} Box.", palletInfo.PALLET_BOX);
                    this.lblTotalPCS.Text = string.Format("{0:#,##0} Pcs.", palletInfo.PALLET_PCS);

                    this._Total_Box += palletInfo.PALLET_BOX;
                    this._Total_Qty += palletInfo.PALLET_PCS;

                    this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                    this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtPALLET_NO.Text = string.Empty;
                    this.txtPALLET_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                this.ClearDataOnScreen();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txtPALLET_NO.Text = string.Empty;
                this.txtPALLET_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                this.ClearDataOnScreen();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtPALLET_NO.Text = string.Empty;
                this.txtPALLET_NO.Focus();
            }
        }

        private void UpdateLoadingSeal(string loadingNo)
        {
            string resultMsg = string.Empty;
            try
            {
                this.ClearDataOnScreen();

                base.ShowWaitProcess();
                resultMsg = ServiceProvider.Instance.Proxy.UpdateLoadingSeal(loadingNo, this.USER_ID);
                base.HideWaitProcess();

                if (resultMsg.Equals("OK"))
                {
                    this._Total_Box = 0;
                    this._Total_Qty = 0;

                    this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                    this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);

                    this.txtPALLET_NO.Text = string.Empty;

                    base.StartPlayTon_Complete();

                    this.txtLOADING_NO.Text = string.Empty;
                    this.txtLOADING_NO.Focus();
                }
                else
                {
                    base.ShowErrorBox(resultMsg,
                                ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtPALLET_NO.Text = string.Empty;
                    this.txtPALLET_NO.Focus();
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");
                this.txtPALLET_NO.Text = string.Empty;
                this.txtPALLET_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtPALLET_NO.Text = string.Empty;
                this.txtPALLET_NO.Focus();
            }
        }

        private void ClearDataOnScreen()
        {
            this.lblPALLET_NO.Text = string.Empty;
            this.lblSO_NO.Text = string.Empty;
            this.lblPARTY_NAME.Text = string.Empty;
            this.lblETA.Text = string.Empty;

            this.lblTotalPCS.Text = string.Empty;
            this.lblTotalBox.Text = string.Empty;
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

        private void txtLOADING_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = (TextBox)sender;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtLOADING_NO_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void txtLOADING_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetLoadingDetail(editor.Text);

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

        private void txtPALLET_NO_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = (TextBox)sender;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtPALLET_NO_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void txtPALLET_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(this.txtLOADING_NO.Text))
                        {
                            this.txtLOADING_NO.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetUpdatePallet(this.txtLOADING_NO.Text, editor.Text);

                        //if (editor.Text.ToUpper().Equals("F"))
                        //{
                        //    DialogResult result = base.ShowQuestionBox("Are you sure ?", "Please Confirm");
                        //    if (result == DialogResult.Yes)
                        //    {
                        //        this.UpdateLoadingSeal(this.txtLOADING_NO.Text);
                        //    }
                        //}
                        //else
                        //{
                            
                        //}
                        break;
                    case Keys.F:
                        DialogResult result = base.ShowQuestionBox("Are you sure ?", "Please Confirm");
                        if (result == DialogResult.Yes)
                        {
                            this.UpdateLoadingSeal(this.txtLOADING_NO.Text);
                        }
                        break;
                    case Keys.Escape:
                        //rollback
                        this.ClearDataOnScreen();

                        this._Total_Box = 0;
                        this._Total_Qty = 0;

                        this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                        this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);

                        this.txtLOADING_NO.Text = string.Empty;
                        this.txtLOADING_NO.Focus();
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

        

        private void frmLoadPallet_Load(object sender, EventArgs e)
        {
            this.txtLOADING_NO.Focus();
        }
    }
}