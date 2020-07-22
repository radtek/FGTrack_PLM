﻿using System;
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
    public partial class frmLoading : BaseFormFullMode, IDisposable
    {
        public frmLoading()
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
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetUpdateProductCardDetail(this.txtLOADING_NO.Text, resultData);
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

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();

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

        private void GetUpdateProductCardDetail(string loadingNo, string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();

                prdInfo = ServiceProvider.Instance.Proxy.GetUpdatePCLoading(loadingNo, serialNo, this.USER_ID, out outMsg);

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

                    this._Total_Box = prdInfo.BOX_QTY;
                    this._Total_Qty = prdInfo.BOX_SCANNED;

                    this.lblBoxLoaded.Text = string.Format("{0:#,##0} Box.", this._Total_Box);
                    this.lblQtyLoaded.Text = string.Format("{0:#,##0} Pcs.", this._Total_Qty);
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

                    this.txtSERIAL_NO.Text = string.Empty;

                    base.StartPlayTon_Complete();

                    this.txtLOADING_NO.Text = string.Empty;
                    this.txtLOADING_NO.Focus();
                }
                else
                {
                    base.ShowErrorBox(resultMsg,
                                ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.txtSERIAL_NO.Text = string.Empty;
                    this.txtSERIAL_NO.Focus();
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");
                this.txtSERIAL_NO.Text = string.Empty;
                this.txtSERIAL_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

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

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSERIAL_NO_KeyDown(object sender, KeyEventArgs e)
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

                        if (editor.Text.ToUpper().Equals("F"))
                        {
                            DialogResult result = base.ShowQuestionBox("Are you sure ?", "Please Confirm");
                            if (result == DialogResult.Yes)
                            {
                                this.UpdateLoadingSeal(this.txtLOADING_NO.Text);
                            }
                        }
                        else
                        {
                            this.GetUpdateProductCardDetail(this.txtLOADING_NO.Text, editor.Text);
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

        private void frmLoading_Load(object sender, EventArgs e)
        {
            this.txtLOADING_NO.Focus();
        }
    }
}