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
    public partial class frmMixing : BaseFormFullMode, IDisposable
    {
        public frmMixing()
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
                    case "txtSerialNo":
                        //this.ActiveInputBox = null;
                        this.txtSerialNo.Text = disp;
                        this.ScanMixingLabel(disp);
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
                case "txtNoOfLabel":
                    this.txtNoOfLabel.Enabled = true;
                    this.txtNoOfLabel.ReadOnly = false;
                    this.txtNoOfLabel.BackColor = Color.White;
                    pnFocusNoOfLabel.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtSerialNo.Enabled = false;
                    this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusSerialNo.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case "txtSerialNo":
                    this.txtSerialNo.Enabled = true;
                    this.txtSerialNo.BackColor = Color.White;
                    pnFocusSerialNo.BackColor = Color.FromArgb(145, 209, 0);

                    this.txtNoOfLabel.Enabled = false;
                    this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);
                    pnFocusNoOfLabel.BackColor = Color.FromArgb(255, 255, 255);
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

                this.txtNoOfLabel.Text = String.Empty;
                this.txtNoOfLabel.ReadOnly = false;
                this.txtNoOfLabel.Enabled = true;
                this.txtNoOfLabel.BackColor = Color.White;


                this.txtSerialNo.Text = String.Empty;
                this.txtSerialNo.ReadOnly = true;
                this.txtSerialNo.Enabled = false;
                this.txtSerialNo.BackColor = Color.FromArgb(0xC8, 0xE8, 0xC8);

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
                this.txtNoOfLabel.Text = String.Empty;
                this.lblTRES.Text = String.Empty;
                this.lblMixingNo.Text = String.Empty;
                this.lblLabelScan.Text = String.Empty;
                this.lblMtlCode.Text = String.Empty;
                this.lblMtlName.Text = String.Empty;
                this.lblGrade.Text = String.Empty;
                this.lblColor.Text = String.Empty;
                this.lblQty.Text = String.Empty;
                this.lblTotalQty.Text = "0   /   0";

                this._MATERIAL_CODE = String.Empty;
                this._SCAN_COUNTER = 0;
                this._TRES_VALUE = 0;
                this.SERIAL_NO = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void StartMixing(decimal tresVal)
        {
            try
            {
                this.lblTRES.Text = tresVal.ToString("F2");
                ResponseResult res = new ResponseResult();
                using (MixingBLL mixingBll = new MixingBLL())
                {
                    res = mixingBll.StartMixing(tresVal, Int32.Parse(txtNoOfLabel.Text), this.USER_ID);
                }


                if (res.Status)
                {
                    if (res.Message == "OK")
                    {
                        this.lblTotalQty.Text = String.Format("{0}   /   {1}", this.SCAN_COUNTER, this.txtNoOfLabel.Text);
                        this.lblMixingNo.Text = res.Data.ToString();
                        this.SetActiveColor("txtSerialNo");
                        this.SetTextFocusControl(this.txtSerialNo, false);
                    }
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");
                this.SetActiveColor("txtNoOfLabel");
                this.SetTextFocusControl(this.txtNoOfLabel, false);
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");
                this.SetActiveColor("txtNoOfLabel");
                this.SetTextFocusControl(this.txtNoOfLabel, false);
            }
        }

        public void ScanMixingLabel(string serialNo)
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (MixingBLL mixingBll = new MixingBLL())
                {
                    res = mixingBll.ScanMixingLabel(serialNo, lblMixingNo.Text, this.MATERIAL_CODE, this.USER_ID);
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

                            if (matInfo.MTL_GRADE.Contains("M/B"))
                            {
                                using (frmInputMB frmMb = new frmInputMB(this))
                                {
                                    frmMb.ShowDialog();
                                }
                            }

                            if (this.SCAN_COUNTER == Convert.ToInt32(this.txtNoOfLabel.Text, NumberFormatInfo.CurrentInfo))
                            {
                                base.ShowCompletelyBox(string.Format("'{0}' {1}", this.lblMixingNo.Text, ResourceManager.Instance.GetString("MIXING_COMPLETE")), ResourceManager.Instance.GetString("TITLE_INFORMATION"));

                                this.txtSerialNo.Text = String.Empty;
                                this.lblTRES.Text = String.Empty;
                                this.lblMixingNo.Text = string.Empty;
                                this.txtNoOfLabel.Text = String.Empty;
                                this.SetActiveColor("txtNoOfLabel");
                            }
                        }
                        else
                        {
                            base.ShowWarningBox(string.Format("'{0}' {1}", serialNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                            this.SetTextFocusControl(this.txtSerialNo, true);
                        }
                    }
                    else
                    {
                        base.ShowWarningBox(string.Format("'{0}' {1}", serialNo, ResourceManager.Instance.GetString(res.Message)), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
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


        public void UpdateMixedQty(decimal mixQty)
        {
            try
            {
                ResponseResult res = new ResponseResult();
                using (MixingBLL mixingBll = new MixingBLL())
                {
                    string test = this.txtSerialNo.Text;
                    res = mixingBll.UpdateMixedQty(this.SERIAL_NO, lblMixingNo.Text,mixQty);
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

        //Event Of Form==========================================================================================================================
        private void frmMixing_Load(object sender, EventArgs e)
        {
            this.lblUserName.Text = "USER: " + _USER_NAME.ToString().ToUpper();

            try
            {
                this.Clear_Detail_Field();
                //this.SetActiveColor("txt01ReLocationNo");
                this.SetTextFocusControl(this.txtNoOfLabel, false);
                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmMixing_Closing(object sender, CancelEventArgs e)
        {
            FullScreenHandle.StopFullScreen(this);
        }

        private void frmMixing_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Escape: //Logoff

            //        if (this.ActiveInputBox.Name == "txt01ReLocationNo")
            //        {
            //            Clear_Detail_Field();
            //            DialogResult = DialogResult.Cancel;
            //        }
            //        else if (this.ActiveInputBox.Name == "txt01LocationNo")
            //        {
            //            Clear_Detail_Field();
            //            this.SetActiveColor("txt01LocationNo");
            //            this.SetTextFocusControl(this.txt01LocationNo, false);
                        
            //        }
            //        else if (this.ActiveInputBox.Name == "txt01LabelNo")
            //        {
            //            Clear_Detail_Field();
            //            this.SetActiveColor("txt01LabelNo");
            //            this.SetTextFocusControl(this.txt01LabelNo, false);
                 
            //        }

            //        break;
            //    default:
            //        break;
            //}


        }




        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        //Event Of txtNoOfLabel==========================================================================================================================
        private void txtNoOfLabel_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Int32 ret = 0;
                this.SetActiveColor("txtNoOfLabel");
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

        private void txtNoOfLabel_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        try
                        {
                            if (String.IsNullOrEmpty(editor.Text))
                            {
                                base.ShowWarningBox(ResourceManager.Instance.GetString("MSG_EMPTY_NO_OF_LABEL"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                            }
                            else
                            {
                                this._SCAN_COUNTER = 0; //reset
                                this._MATERIAL_CODE = String.Empty;
                                this.lblTRES.Text = String.Empty;
                                this.lblMixingNo.Text = String.Empty;
                                this.txtSerialNo.Text = String.Empty;
                                this.lblLabelScan.Text = String.Empty;
                                this.lblMtlCode.Text = String.Empty;
                                this.lblMtlName.Text = String.Empty;
                                this.lblGrade.Text = String.Empty;
                                this.lblColor.Text = String.Empty;
                                this.lblQty.Text = String.Empty;
                                this.lblTotalQty.Text = "0   /   0";


                                int noOFLabel = 0;
                                noOFLabel = Int32.Parse(editor.Text);

                                using (frmInputTRES tres = new frmInputTRES(this))
                                {
                                    tres.ShowDialog();
                                }


                                this.SetTextFocusControl(this.txtSerialNo, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            base.ShowWarningBox(string.Format("{0} {1}", "", "No of label must be numeric"), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));
                        }
                        break;

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

        private void txtNoOfLabel_KeyUp(object sender, KeyEventArgs e)
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

        private void txtNoOfLabel_LostFocus(object sender, EventArgs e)
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

        //Event Of txtMixingNo==========================================================================================================================
        private void txtMixingNo_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtNoOfLabel.Text))
                {
                    this.SetActiveColor("txt01LabelNo");
                    this.ActiveInputBox = sender as TextBox;
                    //BarReader.Instance.BarReader.ScannerEnable = true;
                    int ret = 0;
                    ret = Bt.ScanLib.Control.btScanEnable();
                    if (ret != LibDef.BT_OK)
                    {
                        MessageBox.Show("btScanEnable error ret[" + ret + "]", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtMixingNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
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

        private void txtMixing_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox editor = sender as TextBox;

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        Clear_Detail_Field();
                        this.SetActiveColor("txtNoOfLabel");
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

        private void txtMixingNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                return;

            try
            {
                //this.txt01ScanContainer.Text = this.txt01ScanContainer.Text.ToUpper();
                //this.txt01ScanContainer.SelectionStart = this.txt01ScanContainer.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}