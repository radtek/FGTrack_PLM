using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection;

using HTN.BITS.FGTRACK.LIB;
using HTN.BITS.FGTRACK.LIB.Scanner;

using BarReader = HTN.BITS.FGTRACK.LIB.Scanner.clsBarcodeReader;
using HTN.BITS.FGTRACK.HORIZONTAL.Components;
using System.Collections;
using System.Net;
using System.Globalization;
using System.Threading;


namespace HTN.BITS.FGTRACK.HORIZONTAL
{
    public partial class frmProductFinish : BaseFormFullMode, IDisposable
    {
        //declare symbology object
        

        public frmProductFinish()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                BarReader.Instance.BarReader.BarcodeRead += new BarcodeReadEventHandler(this.QR_BarcodeRead);
                BarReader.Instance.BarReader.ThreadedRead(true);

                this.txtMC_NO.Text = string.Empty;
                this.txtSERIAL_NO.Text = string.Empty;
                this.txtQTY.Text = string.Empty;
                this.lblNo_Box_Scaned.Text = "0/0";

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

        private eProcessMode _PROCESS_MODE;

        private int _LINE_NO;

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

        public eProcessMode PROCESS_MODE
        {
            get
            {
                return _PROCESS_MODE;
            }
            set
            {
                if (_PROCESS_MODE == value)
                    return;
                _PROCESS_MODE = value;
            }
        }

        public int LINE_NO
        {
            get
            {
                return _LINE_NO;
            }
            set
            {
                if (_LINE_NO == value)
                    return;
                _LINE_NO = value;
            }
        }
        
        private TextBox ActiveInputBox
        {
            get { return tempTextBox; }
            set { tempTextBox = value; }
        }

        private string PROCESS_ID
        {
            get
            {
                switch (this.PROCESS_MODE)
                {
                    case eProcessMode.INJECTION:
                        return "INJ";
                        break;
                    case eProcessMode.TRIMMING:
                        return "TRM";
                        break;
                    case eProcessMode.ELECTRIC:
                        return "ELC";
                        break;
                    case eProcessMode.PREQC:
                        return "PRQ";
                        break;
                    case eProcessMode.AllPROCESS:
                        return "P";
                        break;
                    default:
                        return "";
                        break;
                }
            }
        }

        #endregion

        //~frmProductFinish()
        //{
        //    //remove event handle
        //    BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.TextBox_BarcodeRead);
        //    BarReader.Instance.BarReader.ThreadedRead(false);

        //}

        #region "Method Member"

        private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        {
            if (barcodeReaderEvent == null) return;
            try
            {
                string resultData = barcodeReaderEvent.strDataBuffer.Trim();
                

                switch (this.ActiveInputBox.Name)
                {
                    case "txtMC_NO":
                        //do some method
                        this.txtMC_NO.Text = resultData;
                        bool isValid = this.CheckExistMachine(resultData);
                        if (isValid)
                        {
                            this.txtSERIAL_NO.Focus();
                        }
                        else
                        {
                            base.ShowWarningBox(string.Format("{0}\n\"{1}\"", ResourceManager.Instance.GetString("ERR_NO_DATA_FOUND"), resultData),
                                ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                            this.txtMC_NO.Text = string.Empty;
                            this.txtMC_NO.Focus();
                        }
                        break;
                    case "txtSERIAL_NO":
                        this.txtSERIAL_NO.Text = resultData;
                        this.GetProductCardDetail(resultData);
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

        private bool CheckExistMachine(string mcNo)
        {
            bool isValid = false;
            try
            {

                base.ShowWaitProcess();
                isValid = ServiceProvider.Instance.Proxy.CheckExistMachine(mcNo);
                base.HideWaitProcess();
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
                isValid = false;
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
                isValid = false;
            }

            return isValid;
        }

        private void GetProductCardDetail(string serialNo)
        {
            string outMsg = string.Empty;
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                base.ShowWaitProcess();
                prdInfo = ServiceProvider.Instance.Proxy.GetProductCardInfo(serialNo, this.PROCESS_ID, this.USER_ID, out outMsg);
                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblJOB_NO.Text = prdInfo.JOB_NO;
                    this.lblSHIFT.Text = prdInfo.SHIFT;
                    this._Box_Scann = prdInfo.BOX_SCANNED;
                    this._Box_Total = prdInfo.BOX_QTY;
                    this.lblNo_Box_Scaned.Text = string.Format("{0}/{1}", this._Box_Scann, this._Box_Total);
                    this.txtQTY.Text = string.Format("{0:#,##0}", prdInfo.QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;

                    this.LINE_NO = prdInfo.LINE_NO;

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

        private void UpdateProductCard(string mcNo, string serialNo, int Qty)
        {
            string resultMsg = string.Empty;
            try
            {
                base.ShowWaitProcess();
                resultMsg = ServiceProvider.Instance.Proxy.UpdProductCard(serialNo, this.PROCESS_ID, this.txtMC_NO.Text, Qty, this.USER_ID);
                base.HideWaitProcess();

                if (resultMsg.Equals("OK"))
                {
                    if (this._Box_Scann == this._Box_Total)
                    {
                        base.StartPlayTon_Complete();

                        base.ShowCompletelyBox(string.Format("{0}\n\"{1}\"", ResourceManager.Instance.GetString("INF_FINISH_PROCESS"), this.PROCESS_MODE.ToString()),
                            ResourceManager.Instance.GetString("TITLE_INFO_COMPLETE"));

                        this.ClearDataOnScreen();
                        this.txtSERIAL_NO.Text = string.Empty;

                        if ((this.PROCESS_MODE == eProcessMode.INJECTION) || (this.PROCESS_MODE == eProcessMode.AllPROCESS))
                        {
                            this.txtMC_NO.Text = string.Empty;
                            this.txtMC_NO.Focus();
                        }
                        else
                        {
                            this.txtSERIAL_NO.Focus();
                        }
                        
                    }
                    else
                    {
                        this.ClearDataOnScreen();
                        this.txtSERIAL_NO.Text = string.Empty;
                        this.txtSERIAL_NO.Focus();
                    }
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

        private void AssignNG()
        {
            //if (this.PROCESS_MODE == eProcessMode.PREQC || this.PROCESS_MODE == eProcessMode.ELECTRIC)
            //{
            //    DialogResult diaResult = base.ShowQuestionBox("Assign NG ?", "Please Confirm");
            //    if (diaResult == DialogResult.Yes)
            //    {
            //        using (frmAssignNG fasNG = new frmAssignNG())
            //        {
            //            fasNG.JOB_NO = this.lblJOB_NO.Text;
            //            fasNG.LINE_NO = this.LINE_NO;
            //            fasNG.USER_ID = this.USER_ID;
            //            fasNG.IsFinishProd = true;

            //            fasNG.ShowDialog();
            //        }
            //    }

            //    this.Close();
            //}
            //else
            //{
            //    this.ClearDataOnScreen();

            //    base.ShowCompletelyBox(string.Format("{0}\n\"{1}\"", ResourceManager.Instance.GetString("INF_FINISH_PROCESS"), this.PROCESS_MODE.ToString()),
            //        ResourceManager.Instance.GetString("TITLE_INFO_COMPLETE"));

            //    this.txtSERIAL_NO.Text = string.Empty;

            //    this.txtMC_NO.Text = string.Empty;
            //    this.txtMC_NO.Focus();
            //}
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




        private void frmProductFinish_Load(object sender, EventArgs e)
        {
            this.lblPROCESS_MODE.Text = this.PROCESS_MODE.ToString();
            switch (this.PROCESS_MODE)
            {
                case eProcessMode.INJECTION:
                    this.lblMachine.Visible = true;
                    this.txtMC_NO.Visible = true;

                    this.txtMC_NO.Focus();

                    break;
                case eProcessMode.AllPROCESS:
                    this.lblMachine.Visible = true;
                    this.txtMC_NO.Visible = true;

                    this.txtMC_NO.Focus();
                    break;
                default:
                    this.lblMachine.Visible = false;
                    this.txtMC_NO.Visible = false;

                    this.txtSERIAL_NO.Focus();
                    break;
            }
            
        }

        private void frmProductFinish_Closing(object sender, CancelEventArgs e)
        {
            FullScreenHandle.StopFullScreen(this);
        }

        private void txt01MCOrderNo_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = this.txtMC_NO;
            BarReader.Instance.BarReader.ScannerEnable = true;

        }

        private void txt01MCOrderNo_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;

        }

        private void txtProductCardNo_GotFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = this.txtSERIAL_NO;
            BarReader.Instance.BarReader.ScannerEnable = true;
        }

        private void txtProductCardNo_LostFocus(object sender, EventArgs e)
        {
            this.ActiveInputBox = null;
            BarReader.Instance.BarReader.ScannerEnable = false;
        }

        private void btn01Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            int prdQty = 0;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if ((this.PROCESS_MODE == eProcessMode.INJECTION) || (this.PROCESS_MODE == eProcessMode.AllPROCESS))
                    {
                        if (string.IsNullOrEmpty(this.txtMC_NO.Text))
                        {
                            this.txtMC_NO.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(this.txtSERIAL_NO.Text))
                    {
                        this.txtSERIAL_NO.Focus();
                        return;
                    }

                    //prdQty = Convert.ToInt32(this.txtQTY.Text, NumberFormatInfo.InvariantInfo);
                    prdQty = int.Parse(this.txtQTY.Text, NumberStyles.AllowThousands);
                    this.UpdateProductCard(this.txtMC_NO.Text, this.txtSERIAL_NO.Text, prdQty);
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


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            //remove event handle
            BarReader.Instance.BarReader.BarcodeRead -= new BarcodeReadEventHandler(this.QR_BarcodeRead);
            BarReader.Instance.BarReader.ThreadedRead(false);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void btn01Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMC_NO.Text))
            {
                this.txtMC_NO.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSERIAL_NO.Text))
            {
                this.txtSERIAL_NO.Focus();
                return;
            }

            try
            {
                //int prdQty = Convert.ToInt32(this.txtQTY.Text, NumberFormatInfo.InvariantInfo);
                int prdQty = int.Parse(this.txtQTY.Text, NumberStyles.AllowThousands);
                if (prdQty <= 0)
                {
                    this.txtQTY.Focus();
                    this.txtQTY.SelectAll();

                    return;
                }

                this.UpdateProductCard(this.txtMC_NO.Text, this.txtSERIAL_NO.Text, prdQty);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");
                this.txtQTY.Text = string.Empty;
                this.txtQTY.Focus();
            }
            
        }

        private void txtSERIAL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (string.IsNullOrEmpty(this.txtMC_NO.Text))
                    {
                        this.txtMC_NO.Focus();
                        return;
                    }

                    this.GetProductCardDetail(this.txtSERIAL_NO.Text);
                    break;
                case Keys.Escape:
                    //rollback
                    this.txtMC_NO.Text = string.Empty;
                    this.txtMC_NO.Focus();
                    break;
                default:
                    break;
            }
        }

        private void txtMC_NO_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    string resultData = this.txtMC_NO.Text;
                    if (string.IsNullOrEmpty(resultData))
                    {
                        this.txtMC_NO.Focus();
                        return;
                    }

                    bool isValid = this.CheckExistMachine(resultData);
                    if (isValid)
                    {
                        this.txtSERIAL_NO.Focus();
                    }
                    else
                    {

                        base.ShowWarningBox(string.Format("{0}\n\"{1}\"", ResourceManager.Instance.GetString("ERR_NO_DATA_FOUND"), resultData),
                                ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        this.txtMC_NO.Text = string.Empty;
                        this.txtMC_NO.Focus();
                    }
                    break;
                case Keys.Escape:
                    //rollback
                    this.btn01Cancel_Click(this.btn01Cancel, new EventArgs());
                    break;
                default:
                    break;
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