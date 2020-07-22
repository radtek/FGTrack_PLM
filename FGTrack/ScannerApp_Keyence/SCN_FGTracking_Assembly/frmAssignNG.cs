using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.LIB;

using HTN.BITS.FGTRACK.ASSEMBLY.Components;

using System.Net;
using System.Collections;
using System.Globalization;
using HTN.BITS.FGTRACK.LIB.Scanner;
using Bt;

namespace HTN.BITS.FGTRACK.ASSEMBLY
{
    public partial class frmAssignNG : BaseFormFullMode, IDisposable
    {
        public frmAssignNG()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            try
            {
                this.MsgWin = new MsgWindow();
                this.MsgWin.BarcodeRead += new Action(MsgWin_BarcodeRead);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FullScreenHandle.StartFullScreen(this);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            //if (this.MsgWin != null)
            //    this.MsgWin = null;
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region "Variable Member"

        private MsgWindow MsgWin;

        private string _JOB_NO;
        private int _LINE_NO;
        private string _JOB_LOT;
        private bool isFinishProd = false;


        private string _USER_ID;
        private TextBox tempTextBox;

        #endregion

        #region "Property Member"

        public string JOB_NO
        {
            get
            {
                return _JOB_NO;
            }
            set
            {
                if (_JOB_NO == value)
                    return;
                _JOB_NO = value;
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
        public string JOB_LOT
        {
            get
            {
                return _JOB_LOT;
            }
            set
            {
                if (_JOB_LOT == value)
                    return;
                _JOB_LOT = value;
            }
        }
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
        public bool IsFinishProd
        {
            get
            {
                return isFinishProd;
            }
            set
            {
                if (isFinishProd == value)
                    return;
                isFinishProd = value;
            }
        }
        #endregion

        #region "Method Member"

        //private void QR_BarcodeRead(object sender, BarcodeReadEventArgs barcodeReaderEvent)
        //{
        //    if (barcodeReaderEvent == null) return;
        //    try
        //    {
        //        string resultData = barcodeReaderEvent.strDataBuffer.Trim();


        //        switch (this.ActiveInputBox.Name)
        //        {
        //            case "txt01JOB_NO":
        //                this.JOB_NO = resultData;
        //                this.txt01JOB_NO.Text = resultData;

        //                this.GetJobLotList();
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
                  //  MessageBox.Show(disp, "Error");
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

                if (this.ActiveInputBox == null)
                {
                    MessageBox.Show("Active Control is Null");
                    return;
                }

                switch (this.ActiveInputBox.Name)
                {
                    case "txt01JOB_NO":
                        disp = System.Text.Encoding.GetEncoding(932).GetString(codedataGet, 0, codeLen);

                        this.JOB_NO = disp;
                        this.txt01JOB_NO.Text = disp;

                        this.GetJobLotList();
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

        private void GetJobLotList()
        {
            ServiceRef.JobLot[] lstJobLot = null;
            try
            {
                base.ShowWaitProcess();
                lstJobLot = ServiceProvider.Instance.Proxy.GetJobLotList(this.JOB_NO, this.USER_ID);
                base.HideWaitProcess();

                if (lstJobLot != null)
                {
                    this.lblJOB_LOT.Visible = false;
                    this.cmbJOB_LOT_LIST.Visible = true;

                    this.cmbJOB_LOT_LIST.DataSource = lstJobLot.ToList<ServiceRef.JobLot>();
                    this.cmbJOB_LOT_LIST.DisplayMember = "JOB_LOT";
                    this.cmbJOB_LOT_LIST.ValueMember = "LINE_NO";

                    this.cmbJOB_LOT_LIST.SelectedIndex = 0;
                    this.cmbJOB_LOT_LIST.Focus();
                }
                else
                {
                    base.ShowErrorBox(ResourceManager.Instance.GetString("ERR_NO_DATA_FOUND"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.JOB_NO = string.Empty;
                    this.lblJOB_LOT.Text = string.Empty;
                    this.txt01JOB_NO.Text = string.Empty;

                    this.txt01JOB_NO.Focus();
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");

                this.JOB_NO = string.Empty;
                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");

                this.JOB_NO = string.Empty;
                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
        }

        private void GetProductAssignNG()
        {
            ServiceRef.ProductCard prdInfo = null;
            try
            {
                
                base.ShowWaitProcess();
                prdInfo = ServiceProvider.Instance.Proxy.GetJobLotInfo(this.JOB_NO, this.LINE_NO, this.USER_ID);
                base.HideWaitProcess();

                if (prdInfo != null)
                {
                    this.lblPRODUCT_NO.Text = prdInfo.PRODUCT_NO;
                    this.lblPRODUCT_NAME.Text = prdInfo.PRODUCT_NAME;
                    this.lblMTL_TYPE.Text = prdInfo.MTL_TYPE;
                    this.lblNG_QTY.Text = string.Format("{0:#,##0}", prdInfo.NG_QTY);
                    this.lblUNIT_ID.Text = prdInfo.UNIT_ID;

                    this.txtQTY.Focus();
                    this.txtQTY.SelectAll();
                }
                else
                {
                    base.ShowErrorBox(ResourceManager.Instance.GetString("ERR_NO_DATA_FOUND"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                    this.lblJOB_LOT.Text = string.Empty;
                    this.txt01JOB_NO.Text = string.Empty;

                    this.txt01JOB_NO.Focus();

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");

                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");

                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
            finally
            {
            }
        }

        private void UpdateAssignNG(int qty)
        {
            string resultMsg = string.Empty;
            try
            {
                base.ShowWaitProcess();
                resultMsg = ServiceProvider.Instance.Proxy.UpdateNGQty(this.JOB_NO, this.LINE_NO, qty, this.USER_ID);
                base.HideWaitProcess();
                if (resultMsg.Equals("OK"))
                {
                    base.StartPlayTon_Complete();

                    if (!this.IsFinishProd)
                    {
                        this.ClearDataOnScreen();
                        this.cmbJOB_LOT_LIST.SelectedValue = this.LINE_NO;
                        this.cmbJOB_LOT_LIST.Focus();
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                    
                }
                else
                {
                    base.ShowErrorBox(resultMsg,
                                      ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.lblJOB_LOT.Text = string.Empty;
                    this.txt01JOB_NO.Text = string.Empty;

                    this.txt01JOB_NO.Focus();
                }


            }
            catch (WebException wex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(wex.Message, "WebException");
                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();

                base.ShowErrorBox(ex.Message, "Exception");
                this.lblJOB_LOT.Text = string.Empty;
                this.txt01JOB_NO.Text = string.Empty;

                this.txt01JOB_NO.Focus();
            }
        }

        private void ClearDataOnScreen()
        {
            this.lblPRODUCT_NO.Text = string.Empty;
            this.lblPRODUCT_NAME.Text = string.Empty;
            this.lblMTL_TYPE.Text = string.Empty;
            this.lblNG_QTY.Text = string.Empty;
            this.lblUNIT_ID.Text = string.Empty;
            this.txtQTY.Text = string.Empty;
        }

        #endregion

        private void frmAssignNG_Load(object sender, EventArgs e)
        {
            this.txt01JOB_NO.Text = string.Empty;
            this.ClearDataOnScreen();

            if (!string.IsNullOrEmpty(this.JOB_NO))
            {
                this.cmbJOB_LOT_LIST.Visible = false;
                this.lblJOB_LOT.Visible = true;
                

                this.lblJOB_LOT.Visible = true;
                this.txt01JOB_NO.Text = this.JOB_NO;
                this.lblJOB_LOT.Text = this.JOB_LOT;

                this.GetProductAssignNG();
            }
            else
            {
                this.lblJOB_LOT.Visible = false;
                this.cmbJOB_LOT_LIST.Visible = true;

                this.txt01JOB_NO.Focus();
            }
        }

        private void cmbJOB_LOT_LIST_KeyDown(object sender, KeyEventArgs e)
        {
            //e.Handled = false;   
        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            //int ngQty = 0;
            //switch (e.KeyCode)
            //{
            //    case Keys.Enter:
            //        if (string.IsNullOrEmpty(this.JOB_NO))
            //        {
            //            this.txt01JOB_NO.Focus();
            //            return;
            //        }

            //        ngQty = Convert.ToInt32(this.txtQTY.Text, NumberFormatInfo.InvariantInfo);

            //        this.UpdateAssignNG(ngQty);

            //        break;
            //    case Keys.F1:

            //        this.ClearDataOnScreen();

            //        if (!this.IsFinishProd)
            //        {
            //            this.cmbJOB_LOT_LIST.SelectedValue = this.LINE_NO;

            //            this.cmbJOB_LOT_LIST.Focus();
            //            this.cmbJOB_LOT_LIST.SelectAll();
            //        }
            //        else
            //        {
            //            DialogResult = DialogResult.OK;
            //        }
                    
            //        break;
            //    default:
            //        break;
            //}
        }

        private void txt01JOB_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = (TextBox)sender;
            if (editor.ReadOnly) return;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.JOB_NO = this.txt01JOB_NO.Text;
                    this.GetJobLotList();

                    break;
                case Keys.F1:
                    DialogResult = DialogResult.Cancel;

                    break;
                default:
                    break;
            }

        }

        private void txt01JOB_NO_GotFocus(object sender, EventArgs e)
        {
            Int32 ret = 0;

            try
            {
                this.ActiveInputBox = this.txt01JOB_NO;
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

        private void txt01JOB_NO_LostFocus(object sender, EventArgs e)
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

        private void btn01Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.JOB_NO))
                {
                    this.txt01JOB_NO.Focus();
                    return;
                }

                if (this.LINE_NO <= 0)
                {
                    this.cmbJOB_LOT_LIST.Focus();
                    return;
                }

                //int ngQty = Convert.ToInt32(this.txtQTY.Text, NumberFormatInfo.InvariantInfo);
                int ngQty = int.Parse(this.txtQTY.Text, NumberStyles.AllowThousands);
                if (ngQty <= 0)
                {
                    this.txtQTY.Text = "1";
                    this.txtQTY.Focus();
                    this.txtQTY.SelectAll();

                    return;
                }

                this.UpdateAssignNG(ngQty);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Error");
            }
        }

        private void cmbJOB_LOT_LIST_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox editor = (ComboBox)sender;

            switch (e.KeyCode)
            {
                case Keys.Enter:

                    int lineNo = (int)editor.SelectedValue;
                    this.LINE_NO = lineNo;

                    this.GetProductAssignNG();

                    break;
                case Keys.F1:
                    editor.DataSource = null;
                    this.txt01JOB_NO.Text = string.Empty;
                    this.txt01JOB_NO.Focus();
                    break;
                default:
                    break;
            }
        }

        private void txtQTY_KeyUp(object sender, KeyEventArgs e)
        {
            int ngQty = 0;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (string.IsNullOrEmpty(this.JOB_NO))
                    {
                        this.txt01JOB_NO.Focus();
                        return;
                    }

                    //ngQty = Convert.ToInt32(this.txtQTY.Text, NumberFormatInfo.InvariantInfo);
                    ngQty = int.Parse(this.txtQTY.Text, NumberStyles.AllowThousands);

                    this.UpdateAssignNG(ngQty);

                    break;
                case Keys.F1:

                    this.ClearDataOnScreen();

                    if (!this.IsFinishProd)
                    {
                        this.cmbJOB_LOT_LIST.SelectedValue = this.LINE_NO;

                        this.cmbJOB_LOT_LIST.Focus();
                        this.cmbJOB_LOT_LIST.SelectAll();
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }

                    break;
                default:
                    break;
            }
        }
    }
}