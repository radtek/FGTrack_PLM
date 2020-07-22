using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.MCS.SCN.LIB;
using HTN.BITS.MCS.SCN.UIL.Components;
using Intermec.DataCollection;

using BarReader = HTN.BITS.MCS.SCN.LIB.Scanner.clsBarcodeReader;
using System.Net;
using HTN.BITS.MCS.SCN.BLL;
using HTN.BITS.MCS.SCN.BEL;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using Bt;
using System.Text.RegularExpressions;
using HTN.BITS.MSC.SCN.BEL;
using Newtonsoft.Json;
using System.Globalization;

namespace HTN.BITS.MCS.SCN.UIL
{
    public partial class frmReplenish : BaseFormFullMode, IDisposable
    {
        public frmReplenish()
        {
            InitializeComponent();
            this.InitialClearDataInform();

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

            this.SetActiveColor("txtJobNo");
            this.SetTextFocusControl(this.txtJobNo, true);
            FullScreenHandle.StartFullScreen(this);


        }


        void IDisposable.Dispose()
        {
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);

            GC.SuppressFinalize(this);
        }

        private MsgWindow MsgWin;
        private TextBox _ActiveInputBox;

        private string _USER_ID;
        private string _USER_NAME;
        private string _MATERIAL_CODE;
        private int _SCAN_COUNTER;
        public int _TRES_VALUE;
        public string _SERIAL_NO;
        public string _PRODUCT_NO;
        public string _REP_NO;

        public string REP_NO
        {
            get
            {
                return _REP_NO;
            }
            set
            {
                if (_REP_NO == value)
                    return;
                _REP_NO = value;
            }
        }


        public string PRODUCT_NO
        {
            get
            {
                return _PRODUCT_NO;
            }
            set
            {
                if (_PRODUCT_NO == value)
                    return;
                _PRODUCT_NO = value;
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

        public string MATERIAL_CODE
        {
            get
            {
                return _MATERIAL_CODE;
            }
            set
            {
                if (_MATERIAL_CODE == value)
                    return;
                _MATERIAL_CODE = value;
            }
        }

        public string SERIAL_NO
        {
            get
            {
                return _SERIAL_NO;
            }
            set
            {
                if (_SERIAL_NO == value)
                    return;
                _SERIAL_NO = value;
            }
        }

        public int SCAN_COUNTER
        {
            get
            {
                return _SCAN_COUNTER;
            }
            set
            {
                if (_SCAN_COUNTER == value)
                    return;
                _SCAN_COUNTER = value;
            }
        }

        public string USER_NAME
        {
            get
            {
                return _USER_NAME;
            }
            set
            {
                if (_USER_NAME == value)
                    return;
                _USER_NAME = value;
            }
        }

        private TextBox ActiveInputBox
        {
            get
            {
                return _ActiveInputBox;
            }
            set
            {
                _ActiveInputBox = value;
            }
        }

        public string ITEM_ID { get; set; }



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
                    // disp = "btScanGetStringSize error ret[" + codeLen + "]";
                    // MessageBox.Show("Please try agian.", "Error");
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
                    case "txtJobNo":
                        //this.ActiveInputBox = null;
                        this.txtJobNo.Text = disp;
                        this.CheckJobOrder(disp);
                        break;
                    case "txtMCNo":
                        //this.ActiveInputBox = null;
                        this.txtMCNo.Text = disp;
                        this.CheckMachine(disp);
                        break;
                    case "txtSerialNo":
                        //this.ActiveInputBox = null;
                        this.txtSerialNo.Text = disp;
                        this.ScanLabel(disp);
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

        private void SetTextFocusControl(TextBox control, bool isReadOnly)
        {
            control.ReadOnly = isReadOnly;
            control.Text = string.Empty;
            control.Focus();
        }

        private void SetActiveColor(string textactive)
        {
            switch (textactive)
            {
                case "txtJobNo":
                    this.txtJobNo.Enabled = true;
                    this.txtJobNo.ReadOnly = false;
                    this.txtJobNo.BackColor = Color.White;
                    pnFocusJobOrder.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtMCNo.Enabled = false;
                    this.txtMCNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusMCNo.BackColor = Color.Transparent;

                    this.txtNoOfLabel.Enabled = false;
                    this.txtNoOfLabel.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);

                    this.txtSerialNo.Enabled = false;
                    this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    this.lbl01SerialNo.BackColor = Color.White;
                    pnFocusSerialNo.BackColor = Color.Transparent;
                    break;
                case "txtMCNo":
                    this.txtMCNo.Enabled = true;
                    this.txtMCNo.BackColor = Color.White;
                    pnFocusMCNo.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtJobNo.Enabled = false;
                    this.txtJobNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusJobOrder.BackColor = Color.Transparent;

                    this.txtNoOfLabel.Enabled = false;
                    this.txtNoOfLabel.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);

                    this.txtSerialNo.Enabled = false;
                    this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    this.lbl01SerialNo.BackColor = Color.White;
                    pnFocusSerialNo.BackColor = Color.Transparent;
                    break;
                case "txtNoOfLabel":
                    this.txtNoOfLabel.Enabled = true;
                    this.txtNoOfLabel.BackColor = Color.White;
                    pnFocusMCNo.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtJobNo.Enabled = false;
                    this.txtJobNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusJobOrder.BackColor = Color.Transparent;

                    this.txtMCNo.Enabled = false;
                    this.txtMCNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);

                    this.txtSerialNo.Enabled = false;
                    this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    this.lbl01SerialNo.BackColor = Color.White;
                    pnFocusSerialNo.BackColor = Color.Transparent;
                    break;
                case "txtSerialNo":
                    this.txtSerialNo.Enabled = true;
                    this.txtSerialNo.BackColor = Color.Transparent;
                    this.lbl01SerialNo.BackColor = Color.FromArgb(145, 209, 0);
                    pnFocusSerialNo.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtJobNo.Enabled = false;
                    this.txtJobNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusJobOrder.BackColor = Color.Transparent;

                    this.txtMCNo.Enabled = false;
                    this.txtMCNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusMCNo.BackColor = Color.Transparent;

                    this.txtNoOfLabel.Enabled = false;
                    this.txtNoOfLabel.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    break;
                default:
                    break;
            }
        }

        private void InitialClearDataInform()
        {
            try
            {
                //pnlFocus1.BackColor = Color.FromArgb(145, 209, 0);
                //pnlFocus2.BackColor = Color.FromArgb(255, 255, 255);

                this.txtJobNo.Text = String.Empty;
                this.txtJobNo.ReadOnly = true;
                this.txtJobNo.Enabled = true;
                this.txtJobNo.BackColor = Color.White;


                this.txtMCNo.Text = String.Empty;
                this.txtMCNo.ReadOnly = true;
                this.txtMCNo.Enabled = false;
                this.txtMCNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);

                Clear_Detail_Field();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Clear_Detail_Field()
        {
            try
            {
                this.txtJobNo.Text = String.Empty;
                this.txtMCNo.Text = String.Empty;
                this.txtNoOfLabel.Text = String.Empty;
                this.lblProductNo.Text = String.Empty;
                this.txtSerialNo.Text = string.Empty;
                this.lblLabelScan.Text = String.Empty;
                this.lblMtlCode.Text = String.Empty;
                this.lblMtlName.Text = String.Empty;
                this.lblGrade.Text = String.Empty;
                this.lblColor.Text = String.Empty;
                this.lblQty.Text = String.Empty;
                this.lblTotalQty.Text = "0   /   0";
                this.REP_NO = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CheckJobOrder(string jobNo)
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (ReplenishBLL repBll = new ReplenishBLL())
                {
                    res = repBll.CheckJobOrder(jobNo);
                    if (res.Message == "OK")
                    {
                        this.SetActiveColor("txtMCNo");
                        this.lblProductNo.Text = res.Data.ToString();
                        this.SetTextFocusControl(this.txtMCNo, true);
                    }
                    else
                    {
                        base.ShowWarningBox(string.Format("'{0}' {1}", jobNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        this.SetActiveColor("txtJobNo");
                        this.SetTextFocusControl(this.txtJobNo, true);
                    }
                }
                this.txtMCNo.Text = string.Empty;
                this.txtNoOfLabel.Text = string.Empty;
                //this.lblProductNo.Text = string.Empty;
                this.txtSerialNo.Text = String.Empty;
                this.lblLabelScan.Text = String.Empty;
                this.lblMtlCode.Text = String.Empty;
                this.lblMtlName.Text = String.Empty;
                this.lblGrade.Text = String.Empty;
                this.lblColor.Text = String.Empty;
                this.lblQty.Text = String.Empty;
                this._SCAN_COUNTER = 0;
                this.lblTotalQty.Text = "0   /   0";

            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }

        public void CheckMachine(string machineNo)
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (ReplenishBLL repBll = new ReplenishBLL())
                {
                    res = repBll.CheckMachine(machineNo);
                    if (res.Message == "OK")
                    {
                        this.SetActiveColor("txtNoOfLabel");
                        this.SetTextFocusControl(this.txtNoOfLabel, false);
                    }
                    else
                    {
                        base.ShowWarningBox(string.Format("'{0}' {1}", machineNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        this.SetActiveColor("txtMCNo");
                        this.SetTextFocusControl(this.txtMCNo, true);
                    }
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }

        public void StartReplenish()
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (ReplenishBLL repBll = new ReplenishBLL())
                {
                    res = repBll.StartReplenish(this.txtJobNo.Text, this.txtMCNo.Text, Int32.Parse(this.txtNoOfLabel.Text), this.USER_ID);
                }

                if (res.Status)
                {
                    if (res.Message == "OK")
                    {
                        this.lblTotalQty.Text = String.Format("{0}   /   {1}", this.SCAN_COUNTER, this.txtNoOfLabel.Text);
                        this.REP_NO = res.Data.ToString();
                        this.SetActiveColor("txtSerialNo");
                        this.SetTextFocusControl(this.txtSerialNo, true);
                    }
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
                this.SetActiveColor("txtJobNo");
                this.SetTextFocusControl(this.txtJobNo, true);
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
                this.SetActiveColor("txtJobNo");
                this.SetTextFocusControl(this.txtJobNo, true);
            }
        }

        public void ScanLabel(string serialNo)
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (ReplenishBLL repBll = new ReplenishBLL())
                {
                    res = repBll.ScanRepLabel(serialNo, this.REP_NO, txtJobNo.Text, txtMCNo.Text, this.USER_ID);
                }

                if (res.Status)
                {

                    if (res.Data != null)
                    {
                        MaterialCard matInfo = new MaterialCard();
                        matInfo = JsonConvert.DeserializeObject<MaterialCard>(res.Data.ToString());

                        this.lblLabelScan.Text = serialNo;
                        this.lblMtlCode.Text = matInfo.MTL_CODE;
                        this.lblMtlName.Text = matInfo.MTL_NAME;
                        this.lblGrade.Text = matInfo.MTL_GRADE;
                        this.lblColor.Text = matInfo.MTL_COLOR;
                        this.lblQty.Text = matInfo.QTY.ToString() + "  " + matInfo.UNIT_ID;


                        if (res.Message == "OK")
                        {
                            this.SERIAL_NO = serialNo;
                            this.MATERIAL_CODE = matInfo.MTL_CODE;
                            this.SCAN_COUNTER++;
                            this.lblTotalQty.Text = String.Format("{0}   /   {1}", this.SCAN_COUNTER, this.txtNoOfLabel.Text);
                            this.SetActiveColor("txtSerialNo");
                            this.SetTextFocusControl(this.txtSerialNo, true);

                            if (this.SCAN_COUNTER == Convert.ToInt32(this.txtNoOfLabel.Text, NumberFormatInfo.CurrentInfo))
                            {
                                base.ShowCompletelyBox(string.Format("'{0}' {1}", this.lblProductNo.Text, ResourceManager.Instance.GetString("REPLENISH_COMPLETE")), ResourceManager.Instance.GetString("TITLE_INFORMATION"));

                                this.txtJobNo.Text = String.Empty;
                                this.txtMCNo.Text = String.Empty;
                                this.txtNoOfLabel.Text = String.Empty;
                                this.txtSerialNo.Text = string.Empty;
                                this.SetActiveColor("txtJobNo");
                                this.SetTextFocusControl(this.txtJobNo, true);
                            }
                        }
                        else
                        {
                            base.ShowWarningBox(string.Format("'{0}' {1}", serialNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                            this.SetActiveColor("txtSerialNo");
                            this.SetTextFocusControl(this.txtSerialNo, true);
                        }
                    }
                    else
                    {
                        base.ShowWarningBox(string.Format("'{0}' {1}", serialNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        this.SetActiveColor("txtSerialNo");
                        this.SetTextFocusControl(this.txtSerialNo, true);
                    }
                }


            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
                this.SetActiveColor("txtSerialNo");
                this.SetTextFocusControl(this.txtSerialNo, true);
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
                this.SetActiveColor("txtSerialNo");
                this.SetTextFocusControl(this.txtSerialNo, true);
            }
        }


      

        //Event Of Form==========================================================================================================================
        private void frmReplenish_Load(object sender, EventArgs e)
        {
            this.lblUserName.Text = "USER: " + _USER_NAME.ToString().ToUpper();

            try
            {
                this.Clear_Detail_Field();
                //this.SetActiveColor("txt01ReLocationNo");
                this.SetTextFocusControl(this.txtJobNo, true);
                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmReplenish_Closing(object sender, CancelEventArgs e)
        {
            FullScreenHandle.StopFullScreen(this);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        //Event Of txtNoOfLabel==========================================================================================================================
        private void txtJobNo_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Int32 ret = 0;
                this.SetActiveColor("txtJobNo");
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

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        DialogResult = DialogResult.Cancel;
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtJobNo_KeyUp(object sender, KeyEventArgs e)
        {
            //e.Handled = true;
            //if (e.KeyCode == Keys.Enter)
            //    return;

            //try
            //{
            //    this.txtNoOfLabel.Text = this.txtNoOfLabel.Text.ToUpper();
            //    this.txtNoOfLabel.SelectionStart = this.txtNoOfLabel.Text.Length;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void txtJobNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                //this.SetActiveColor("txt01LabelNo");
                this.ActiveInputBox = null;
                //BarReader.Instance.BarReader.ScannerEnable = false;
                Int32 ret = 0;

          

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


        private void txtNoOfLabel_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:

                        if (String.IsNullOrEmpty(editor.Text))
                        {
                            base.ShowWarningBox(string.Format("{0} {1}", "", "Please input no of label"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                            this.SetActiveColor("txtNoOfLabel");
                            this.SetTextFocusControl(this.txtNoOfLabel, false);
                        }
                        else
                        {
                            this.StartReplenish();
                            this.SetActiveColor("txtSerialNo");
                            this.SetTextFocusControl(this.txtSerialNo, true);
                        }
                        break;

                    case Keys.F1:
                        this.SetActiveColor("txtMCNo");
                        this.SetTextFocusControl(this.txtMCNo, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtMCNo_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        this.Clear_Detail_Field();
                        this.SetActiveColor("txtJobNo");
                        this.SetTextFocusControl(this.txtJobNo, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        this.Clear_Detail_Field();
                        this.SetActiveColor("txtJobNo");
                        this.SetTextFocusControl(this.txtJobNo, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtMCNo_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Int32 ret = 0;
                this.SetActiveColor("txtMCNo");
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

        private void txtMCNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                //this.SetActiveColor("txt01LabelNo");
                this.ActiveInputBox = null;
                //BarReader.Instance.BarReader.ScannerEnable = false;
                Int32 ret = 0;



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

        private void txtSerialNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                //this.SetActiveColor("txt01LabelNo");
                this.ActiveInputBox = null;
                //BarReader.Instance.BarReader.ScannerEnable = false;
                Int32 ret = 0;



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

        private void txtSerialNo_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Int32 ret = 0;
                this.SetActiveColor("txtSerialNo");
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
    }
}