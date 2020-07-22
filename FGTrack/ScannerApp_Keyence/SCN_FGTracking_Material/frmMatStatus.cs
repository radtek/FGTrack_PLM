using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using System.Globalization;

using HTN.BITS.FGTRACK.LIB;


using HTN.BITS.FGTRACK.MATERIAL.Components;
using HTN.BITS.FGTRACK.MATERIAL.ServiceRef;
using Bt;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.FGTRACK.MATERIAL
{
    public partial class frmMatStatus : BaseFormFullMode, IDisposable
    {
        public frmMatStatus()
        {
            InitializeComponent();
            base.UpdateResourcesInForm(GlobalVariable.LanguageSelect);

            this.InitialClearDataInform();

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
            this.MsgWin.BarcodeRead -= new Action(MsgWin_BarcodeRead);
         
 
            GC.SuppressFinalize(this);
        }

        #endregion
     
        #region Variable Member
        private MsgWindow MsgWin;

        private string _USER_ID;
        private string _USER_NAME;

        private string _SERIAL_NO;

        private TextBox _ActiveInputBox;



        #endregion

        #region Property Member

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
                //this.lbl01UserName.Text = _USER_ID;
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
                this.lbl01UserName.Text = _USER_NAME;
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

        #endregion

        #region Method Member

     
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

                disp = System.Text.Encoding.GetEncoding(932).GetString(codedataGet, 0, codeLen);


                string resultData = disp;


                switch (this.ActiveInputBox.Name)
                {
                    case "txt01SERIAL_NO":
                        //  this.ActiveInputBox = null;

                        this.txt01SERIAL_NO.Text = resultData;
                        this.SERIAL_NO = resultData;
                        this.MatStatus(resultData);
                        break;

                    case "txt01LOCATION":

                        this.txt01LOCATION.Text = resultData;
                        this.Mat_ChangeLocation(resultData);
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

        private void InitialClearDataInform()
        {
            try
            {
                
                this.txt01SERIAL_NO.Text = "";
                this.SetTextReadOnlyControl(this.txt01SERIAL_NO);
                //this.txt01SERIAL_NO.ReadOnly = true;

                this.ClearScreenDetail();

                this.lbl01CHANGE_LOCATION.Visible = false;
                this.txt01LOCATION.Text = string.Empty;
                this.txt01LOCATION.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetTextFocusControl(TextBox control, bool isReadOnly)
        {
            control.ReadOnly = isReadOnly;
            control.Enabled = !isReadOnly;
            control.Text = string.Empty;
            control.Focus();
        }

        private void SetTextReadOnlyControl(TextBox control)
        {
            control.ReadOnly = true;
            control.Enabled = false;
        }

        private void ClearScreenDetail()
        {
            try
            {
                this.lblSUPPLIER.Text = string.Empty;

                this.lblMTL_CODE.Text = string.Empty;
                this.lblMTL_NAME.Text = string.Empty;
                this.lblMTL_GRADE.Text = string.Empty;
                this.lblMTL_COLOR.Text = string.Empty;

                this.lblQTY.Text = string.Empty;
                this.lblARRIVAL_NO.Text = string.Empty;

                this.lblCARGO_STATUS.Text = string.Empty;
                this.lblLOCATION.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MatStatus(string serialno)
        {
            string resultMsg = string.Empty;
            ServiceRef.MaterialCard mCard = null;

            if (string.IsNullOrEmpty(serialno))
            {
                base.ShowErrorBox(string.Format("{0}", ResourceManager.Instance.GetString("ERR_PLEASE_INPUT_VALUE"))
                            , ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                this.SetTextFocusControl(this.txt01SERIAL_NO, false);

                return;
            }

            try
            {
                base.ShowWaitProcess();
                mCard = ServiceProvider.Instance.Proxy.ScanMat_Status(serialno, out resultMsg);
                base.HideWaitProcess();

                this.ClearScreenDetail();

                if (mCard != null)
                {
                    this.lblSUPPLIER.Text = mCard.PARTY_NAME;
                    this.lblMTL_CODE.Text = mCard.MTL_CODE;
                    this.lblMTL_NAME.Text = mCard.MTL_NAME;
                    this.lblMTL_GRADE.Text = mCard.MTL_GRADE;
                    this.lblMTL_COLOR.Text = mCard.MTL_COLOR;

                    this.lblQTY.Text = string.Format("{0:#,##0.0000} {1}.", mCard.QTY, mCard.UNIT_ID);
                    this.lblARRIVAL_NO.Text = mCard.ARRIVAL_NO;


                    this.lblCARGO_STATUS.Text = ResourceManager.Instance.GetString(mCard.CARGO_STATUS);

                    this.lblLOCATION.Text = mCard.LOCATION_ID;

                    if (resultMsg == "OK")
                    {
                        this.SetTextFocusControl(this.txt01SERIAL_NO, false);
                    }
                    else
                    {
                        base.ShowWarningBox(ResourceManager.Instance.GetString(resultMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                        this.SetTextFocusControl(this.txt01SERIAL_NO, false);
                    }
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(resultMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.SetTextFocusControl(this.txt01SERIAL_NO, false);
                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");

                this.SetTextFocusControl(this.txt01SERIAL_NO, false);
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");

                this.SetTextFocusControl(this.txt01SERIAL_NO, false);
            }
        }

        private void Mat_ChangeLocation(string location)
        {
            string serialno = this.txt01SERIAL_NO.Text;

            string resultMsg = string.Empty;
            ServiceRef.MaterialCard mCard = null;


            if (string.IsNullOrEmpty(serialno))
            {
                base.ShowErrorBox(string.Format("{0}", ResourceManager.Instance.GetString("ERR_PLEASE_INPUT_VALUE"))
                            , ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                this.SetTextFocusControl(this.txt01SERIAL_NO, false);

                return;
            }
            

            if (string.IsNullOrEmpty(location))
            {
                base.ShowErrorBox(string.Format("{0}", ResourceManager.Instance.GetString("ERR_PLEASE_INPUT_VALUE"))
                            , ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                this.SetTextFocusControl(this.txt01LOCATION, false);

                return;
            }

            

            try
            {
                base.ShowWaitProcess();
                mCard = ServiceProvider.Instance.Proxy.ScanMat_ChangeLocation(serialno, location, this.USER_ID, out resultMsg);
                base.HideWaitProcess();

                if (mCard != null)
                {
                    this.lblSUPPLIER.Text = mCard.PARTY_NAME;
                    this.lblMTL_CODE.Text = mCard.MTL_CODE;
                    this.lblMTL_NAME.Text = mCard.MTL_NAME;
                    this.lblMTL_GRADE.Text = mCard.MTL_GRADE;
                    this.lblMTL_COLOR.Text = mCard.MTL_COLOR;

                    this.lblQTY.Text = string.Format("{0:#,##0.0000} {1}.", mCard.QTY, mCard.UNIT_ID);
                    this.lblARRIVAL_NO.Text = mCard.ARRIVAL_NO;


                    this.lblCARGO_STATUS.Text = ResourceManager.Instance.GetString(mCard.CARGO_STATUS);

                    


                    if (resultMsg == "OK")
                    {
                        this.lblLOCATION.Text = mCard.LOCATION_ID;
                        


                        this.lbl01CHANGE_LOCATION.Visible = false;

                        this.txt01LOCATION.Text = string.Empty;
                        this.txt01LOCATION.Visible = false;

                        this.SetTextFocusControl(this.txt01SERIAL_NO, false);
                    }
                    else
                    {
                        base.ShowWarningBox(ResourceManager.Instance.GetString(resultMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                        this.SetTextFocusControl(this.txt01LOCATION, false);
                    }
                }
                else
                {
                    base.ShowWarningBox(ResourceManager.Instance.GetString(resultMsg), ResourceManager.Instance.GetString("TITLE_PLEASE_CHECK"));

                    this.SetTextFocusControl(this.txt01LOCATION, false);

                }
            }
            catch (WebException wex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(wex.Message, "WebException");

                this.SetTextFocusControl(this.txt01LOCATION, false);
            }
            catch (Exception ex)
            {
                base.HideWaitProcess();
                base.ShowErrorBox(ex.Message, "Exception");

                this.SetTextFocusControl(this.txt01LOCATION, false);
            }
        }

        #endregion

        #region Event Of Form

        private void frmMatStatus_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetTextFocusControl(this.txt01SERIAL_NO, false);

                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmMatStatus_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (this.btn01Cancel.Focused)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    break;
                case Keys.F4:

                    //this.btn01ChangeLoc.Focus();
                    //this.btn01ChangeLoc_Click(this.btn01ChangeLoc, new EventArgs());

                    break;
                default:
                    break;
            }

            //if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            //{
            //    // Up
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            //{
            //    // Down
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            //{
            //    // Left
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            //{
            //    // Right
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            //{
            //    // Enter
            //}

        }

        private void frmMatStatus_Closing(object sender, CancelEventArgs e)
        {
           // 
        }

        #endregion Event Of Form

        #region Event Of txt01SERIAL_NO

        private void txt01SERIAL_NO_GotFocus(object sender, EventArgs e)
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

        private void txt01SERIAL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!editor.ReadOnly)
                        {
                            this.MatStatus(editor.Text);
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

        private void txt01SERIAL_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            try
            {
                switch ((int)e.KeyChar)
                {
                    case 13: break;
                    case 27: break;
                    default: e.Handled = true; break;
                }
            }
            catch (Exception ex)
            {
                clsUtility.StartBeep(200, 400, 2);
                MessageBox.Show(ex.ToString());
            }
            */
        }

        private void txt01SERIAL_NO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                return;

            try
            {
                this.txt01SERIAL_NO.Text = this.txt01SERIAL_NO.Text.ToUpper();
                this.txt01SERIAL_NO.SelectionStart = this.txt01SERIAL_NO.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt01SERIAL_NO_LostFocus(object sender, EventArgs e)
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

        #endregion Event Of txt01PalletNo

        #region Event of txt01LOCATION


        private void txt01LOCATION_GotFocus(object sender, EventArgs e)
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

        private void txt01LOCATION_LostFocus(object sender, EventArgs e)
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

        private void txt01LOCATION_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox editor = sender as TextBox;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!editor.ReadOnly)
                        {
                            this.Mat_ChangeLocation(editor.Text);
                        }
                        break;
                    case Keys.F1:

                        this.SetTextReadOnlyControl(editor);

                        this.lbl01CHANGE_LOCATION.Visible = false;
                        this.txt01LOCATION.Visible = false;

                        this.txt01SERIAL_NO.Text = string.Empty;
                        this.SetTextFocusControl(this.txt01SERIAL_NO, false);

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

        private void txt01LOCATION_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            try
            {
                switch ((int)e.KeyChar)
                {
                    case 13: break;
                    case 27: break;
                    default: e.Handled = true; break;
                }
            }
            catch (Exception ex)
            {
                clsUtility.StartBeep(200, 400, 2);
                MessageBox.Show(ex.ToString());
            }
            */
        }

        private void txt01LOCATION_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                return;

            try
            {
                this.txt01LOCATION.Text = this.txt01LOCATION.Text.ToUpper();
                this.txt01LOCATION.SelectionStart = this.txt01LOCATION.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion 


        private void btn01ChangeLoc_Click(object sender, EventArgs e)
        {
            try
            {
                //this.txt01SERIAL_NO.Text = this.SERIAL_NO;
                //this.SetTextReadOnlyControl(this.txt01SERIAL_NO);

                //this.lbl01CHANGE_LOCATION.Visible = true;
                //this.txt01LOCATION.Visible = true;

                //this.SetTextFocusControl(this.txt01LOCATION, false);
            }
            catch (Exception ex)
            {
                base.ShowErrorBox(ex.Message, "Exception");
            }
        }




        

    }
}