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
    public partial class frmRePacking : BaseFormFullMode, IDisposable
    {
        public frmRePacking()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txt01PICKING_NO.Text = string.Empty;
                this._Qty_Scann = 0;

                this.lbl00SO_NO.Text = string.Empty;
                this.txtPALLET_NO.Text = string.Empty;
                this.lblOrderCount.Text = string.Empty;
                this.lbl00ScanedBox.Text = string.Empty;
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

        private ServiceRef.Pallet[] lstPallet = null;
        private int recordIndex_c = -1;

        private int _Qty_Scann;
        private string _Pallet_Status;

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

                        this.GetPalletList(resultData);
                        break;
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetUpdateProductCardPallet(this.txtPALLET_NO.Text, resultData);
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

        private void GetPalletList(string pickingNo)
        {
            string outMsg = string.Empty;
 
            try
            {
                base.ShowWaitProcess();
                this.lstPallet = ServiceProvider.Instance.Proxy.GetPalletList(pickingNo, out outMsg);
                base.HideWaitProcess();

                if (this.lstPallet != null)
                {
                    //show so#
                    this.lbl00SO_NO.Text = this.lstPallet[0].SO_NO;

                    this.recordIndex_c = -1;

                    this.lblOrderCount.Text = string.Format("{0}/{1}", this.recordIndex_c + 1, this.lstPallet.Length);

                    this.txtPALLET_NO.Text = "(Please Select)";
                    this.txtPALLET_NO.Focus();
                    return;
                    
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(outMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.lblOrderCount.Text = string.Empty;
                    this.txtPALLET_NO.Text = "(NO PALLET)";

                    this.recordIndex_c = -1;

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

        private void GetPalletDetail(string palletno)
        {
            ServiceRef.Pallet pallet;
            try
            {
                base.ShowWaitProcess();
                pallet = ServiceProvider.Instance.Proxy.GetPalletInfo(palletno);
                base.HideWaitProcess();

                if (pallet != null)
                {
                    this._Qty_Scann = pallet.PALLET_BOX;
                    this._Pallet_Status = pallet.PALLET_STATUS;

                    this.lbl00ScanedBox.Text = string.Format("Scaned Box : {0:#,##0}", this._Qty_Scann);

                    if (this._Pallet_Status.Equals("F") || this._Pallet_Status.Equals("P")) //if already finished
                    {
                        base.ShowWarningBox(ResourceManager.Instance.GetString("ERR_PROCESS_FINISH"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        
                        this.txtPALLET_NO.Focus();
                    }
                    else
                    {
                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txtSERIAL_NO.Focus();
                    }
                }
                else
                {
                    this.txtPALLET_NO.Focus();
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");

                this.txtPALLET_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");

                this.txtPALLET_NO.Focus();
            }
        }

        private void GetUpdateProductCardPallet(string palletno, string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetUpdatePCPallet(palletno, serialNo, this.USER_ID, out outMsg);

                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblSERIAL_NO.Text = serialNo;
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblQty.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;

                    

                    this._Qty_Scann += 1;
                    this.lbl00ScanedBox.Text = string.Format("Scaned Box : {0:#,##0}", this._Qty_Scann);

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

        private void UpdatePalletFinished(string palletno)
        {
            string resultMsg = string.Empty;
            
            try
            {
                base.ShowWaitProcess();

                resultMsg = ServiceProvider.Instance.Proxy.UpdatePalletFinish(palletno, this.USER_ID);

                base.HideWaitProcess();

                if(resultMsg.ToUpper().Equals("OK"))
                {
                    this.ClearDataOnScreen();

                    this.recordIndex_c = -1;

                    this.lblOrderCount.Text = string.Format("{0}/{1}", this.recordIndex_c + 1, this.lstPallet.Length);

                    this.txtSERIAL_NO.Text = string.Empty;

                    base.StartPlayTon_Complete();

                    this.txtPALLET_NO.Text = "(Please Select)";
                    this.txtPALLET_NO.Focus();

                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(resultMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

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

        private void txt01PICKING_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetPalletList(editor.Text);
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
                    case Keys.F:
                        
                        if (string.IsNullOrEmpty(this.txtPALLET_NO.Text)) return;

                        if (this._Pallet_Status.Equals("F"))
                        {
                            base.ShowWarningBox(ResourceManager.Instance.GetString("ERR_PROCESS_FINISH"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                            this.ClearDataOnScreen();

                            this.recordIndex_c = -1;
                            editor.Text = string.Empty;
                            this.lblOrderCount.Text = string.Format("{0}/{1}", this.recordIndex_c + 1, this.lstPallet.Length);
                            this.txtSERIAL_NO.Text = string.Empty;
                            this.txtPALLET_NO.Text = "(Please Select)";
                            this.txtPALLET_NO.Focus();

                            return;
                        }

                        DialogResult result = base.ShowQuestionBox("Are you sure ?", "Please Confirm");
                        if (result == DialogResult.Yes)
                        {
                            this.txtSERIAL_NO.Text = string.Empty;
                            this.UpdatePalletFinished(this.txtPALLET_NO.Text);
                        }
                        else
                        {
                            this.txtSERIAL_NO.Text = string.Empty;
                            this.txtSERIAL_NO.Focus();
                        }
                        
                        break;
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(this.txt01PICKING_NO.Text))
                        {
                            this.txt01PICKING_NO.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetUpdateProductCardPallet(this.txtPALLET_NO.Text, editor.Text);

                        break;
                    case Keys.Escape:
                        //rollback
                        this._Qty_Scann = 0;
                        this.ClearDataOnScreen();

                        this.txtSERIAL_NO.Text = string.Empty;
                        this.lbl00ScanedBox.Text = string.Empty;
                        this.txtPALLET_NO.Focus();
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

        private void frmRePacking_Load(object sender, EventArgs e)
        {
            this.txt01PICKING_NO.Focus();
        }


        private void txtPALLET_NO_GotFocus(object sender, EventArgs e)
        {
            TextBox editor = sender as TextBox;
            try
            {
                //editor.ReadOnly = false;
                this.tempTextBox = editor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                editor.Focus();
            }
        }

        private void txtPALLET_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Down: //Key Down
                        if (this.lstPallet != null)
                        {
                            this.recordIndex_c++;

                            if (this.recordIndex_c > (this.lstPallet.Length - 1))
                                this.recordIndex_c = 0;


                            editor.Text = string.Empty;

                            editor.Text = this.lstPallet[this.recordIndex_c].PALLET_NO;
                            editor.Focus();
                            editor.SelectionStart = editor.Text.Length - 1;

                            this.lblOrderCount.Text = string.Format("{0}/{1}", this.recordIndex_c + 1, this.lstPallet.Length);

                            e.Handled = false;
                            return;
                        }
                        break;
                    case Keys.Up: //Key Up
                        if (this.lstPallet != null)
                        {
                            if (this.recordIndex_c < 0)
                                this.recordIndex_c = this.lstPallet.Length - 1;
                            else
                            {
                                this.recordIndex_c--;

                                if (this.recordIndex_c < 0)
                                    this.recordIndex_c = this.lstPallet.Length - 1;

                            }

                            editor.Text = string.Empty;

                            editor.Text = this.lstPallet[this.recordIndex_c].PALLET_NO;
                            editor.Focus();
                            editor.SelectionStart = editor.Text.Length;

                            this.lblOrderCount.Text = string.Format("{0}/{1}", this.recordIndex_c + 1, this.lstPallet.Length);
                           
                            e.Handled = false;

                            return;
                        }
                        break;
                    case Keys.Enter: //Enter
                        if (string.IsNullOrEmpty(editor.Text))
                        {
                            base.ShowWarningBox(ResourceManager.Instance.GetString("ERR_WRONG_LABEL"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                            editor.Focus();
                            return;
                        }

                        this.GetPalletDetail(editor.Text);

                        break;
                    case Keys.Escape: //Escape

                        editor.Text = string.Empty;
                        this._Qty_Scann = 0;
                        this._Pallet_Status = string.Empty;

                        this.lblOrderCount.Text = string.Empty;
                        this.lbl00SO_NO.Text = string.Empty;

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

        private void btn01FinishPallet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPALLET_NO.Text))
            {
                this.txtPALLET_NO.Focus();
                return;
            }

            this.UpdatePalletFinished(this.txtPALLET_NO.Text);
        }

    }
}