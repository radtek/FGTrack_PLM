using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.Vertical.Components;
using System.Globalization;
using System.Net;
using Bt;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.Vertical
{
    public partial class frmOKCargo : BaseFormFullMode, IDisposable
    {
        public frmOKCargo()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                this.MsgWin = new MsgWindow();
                this.MsgWin.BarcodeRead += new Action(MsgWin_BarcodeRead);

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

        private MsgWindow MsgWin;
        private string _USER_ID;
        private eQCStatus _QC_STATUS;

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
        public eQCStatus QC_STATUS
        {
            get
            {
                return _QC_STATUS;
            }
            set
            {
                if (_QC_STATUS == value)
                    return;
                _QC_STATUS = value;
            }
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
                 //   MessageBox.Show(disp, "Error");
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

                this.txtSERIAL_NO.Text = disp;
                this.GetUpdateProductCardDetail(disp);

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

        private void GetUpdateProductCardDetail(string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetUpdatePC_QC_Info(serialNo, this.QC_STATUS.ToString(), this.USER_ID, out outMsg);

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

                    this._Box_Scann++;

                    this.lblNo_Box_Scaned.Text = string.Format("{0}/{1}", this._Box_Scann, this._Box_Total);

                    if (this._Box_Scann == this._Box_Total)
                    {
                        base.StartPlayTon_Complete();

                        this._Box_Scann = 0;
                        this._Box_Total = 0;

                        this.txtNoOfBox.Text = string.Empty;
                        this.txtNoOfBox.Focus();
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
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void txtSERIAL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(editor.Text)) return;

                        this.GetUpdateProductCardDetail(editor.Text);
                        break;
                    case Keys.F1:
                        //rollback
                        this._Box_Scann = 0;
                        this._Box_Total = 0;

                        this.lblNo_Box_Scaned.Text = string.Format("{0}/{1}", this._Box_Scann, this._Box_Total);

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
                    case Keys.F1:
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

        private void txtSERIAL_NO_GotFocus(object sender, EventArgs e)
        {
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

        private void txtSERIAL_NO_LostFocus(object sender, EventArgs e)
        {
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

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOKCargo_Load(object sender, EventArgs e)
        {
            this._Box_Scann = 0;
            this.lblStatus.Text = this.QC_STATUS.ToString();

            this.txtNoOfBox.Focus();
        }
    }
}