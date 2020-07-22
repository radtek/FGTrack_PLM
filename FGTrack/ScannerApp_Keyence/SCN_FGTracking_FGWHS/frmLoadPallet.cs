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
using System.Net;
using Bt;
using HTN.BITS.FGTRACK.LIB.Scanner;

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
                this.MsgWin = new MsgWindow();
                this.MsgWin.BarcodeRead += new Action(MsgWin_BarcodeRead);

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
        private MsgWindow MsgWin;
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
                  //  disp = "btScanGetStringSize error ret[" + codeLen + "]";
                  //  MessageBox.Show("Please try agian.", "Error");
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

                switch (this.ActiveInputBox.Name)
                {
                    case "txtLOADING_NO":
                        this.ClearDataOnScreen();
                        //do some method
                        this.txtLOADING_NO.Text = disp;
                        this.GetLoadingDetail(disp);
                        break;
                    case "txtPALLET_NO":
                        this.txtPALLET_NO.Text = disp;
                        this.GetUpdatePallet(this.txtLOADING_NO.Text, disp);
                        break;
                    default:
                        break;
                }

                ret = Bt.ScanLib.Control.btScanEnable();
                if (ret != LibDef.BT_OK)
                {
                    MessageBox.Show("btScanEnable error ret[" + ret + "]", "Error");
                }
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.ToString());
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
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void txtLOADING_NO_GotFocus(object sender, EventArgs e)
        {
            
            Int32 ret = 0;

            try
            {
                this.ActiveInputBox = (TextBox)sender;
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

        private void txtLOADING_NO_LostFocus(object sender, EventArgs e)
        {
            
            Int32 ret = 0;

            try
            {
                this.ActiveInputBox = null;
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
                    case Keys.F1:
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

            Int32 ret = 0;

            try
            {
                this.ActiveInputBox = (TextBox)sender;
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

        private void txtPALLET_NO_LostFocus(object sender, EventArgs e)
        {
            Int32 ret = 0;

            try
            {
                this.ActiveInputBox = null;
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
                    case Keys.F1:
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