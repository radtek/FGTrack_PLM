using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using System.IO;

namespace HTN.BITS.UIL.PLASESS.Component
{
    public partial class frmUserInfo : BaseDialogForm
    {
        public frmUserInfo()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
        }

        #region "Dialog Idle Time"

        ~frmUserInfo()
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }
            base.DialogIdle.IdleAsync -= new EventHandler(this.DialogIdle_IdleAsync);
        }

        private void DialogIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.DialogIdle_Idle(sender, e); })
                );
        }

        private void DialogIdle_Idle(object sender, EventArgs e)
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion 

        #region Private Member

        private string _USER_ID;
        private bool isAdminRole;

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
            }
        }
        public bool IsAdminRole
        {
            get
            {
                return isAdminRole;
            }
            set
            {
                if (isAdminRole == value)
                    return;
                isAdminRole = value;
            }
        }

        #endregion

        #region Method Member

        private void GetUserInfo()
        {
            User user = null;
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    user = adminBll.GetUserByUserID(this._USER_ID);
                }

                if (user != null)
                {
                    this.txtUSER_ID.EditValue = user.USER_ID;
                    this.txtUSER_NAME.EditValue = user.USER_NAME;
                    this.txtEMPLOYEE_ID.EditValue = user.EMPLOYEE_ID;
                    this.txtLOGIN.EditValue = user.LOGIN;

                    this.txtPWD.EditValue = null;
                    this.txtConfirmPws.EditValue = null;

                    this.IsAdminRole = user.ROLE_ID.Equals("ADM");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ResetGridLayout()
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);

            if (Directory.Exists(stateLayoutPath))
            {
                this.ClearFolder(stateLayoutPath);

                XtraMessageBox.Show("Reset Layout Completed");
            }
        }

        private void UserChangePassword(string newPassword)
        {
            string result = string.Empty;
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UserChangePassword(newPassword, this._USER_ID);
                }

                if (!result.Equals("OK"))
                {
                    XtraMessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtPWD.Text = string.Empty;
                    this.txtConfirmPws.Text = string.Empty;
                    this.txtPWD.Focus();
                }
                else
                {
                    XtraMessageBox.Show("Change Password Completed");
                    this.txtPWD.Text = string.Empty;
                    this.txtConfirmPws.Text = string.Empty;
                    this.btnExit.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
        #endregion

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            this.GetUserInfo();

            if (this.IsAdminRole)
            {
                this.pnlFixPrintProdCardRpt.Visible = true;
                this.txtFixProdCardRptName.Text = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.FixProductCardReport;
            }
            else
            {
                this.pnlFixPrintProdCardRpt.Visible = false;
            }

            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetGridLayout_Click(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("Do you want to reset Layout?", "Please Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.ResetGridLayout();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (this.txtPWD.Text == "")
            {
                XtraMessageBox.Show("Password Can't be null!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPWD.Focus();
                return;
            }

            if (this.txtConfirmPws.Text == "")
            {
                XtraMessageBox.Show("Confirm Password Can't be null!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtConfirmPws.Focus();
                return;
            }

            if (this.txtPWD.Text != this.txtConfirmPws.Text)
            {
                XtraMessageBox.Show("Confirm Password Not Maching!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtConfirmPws.Text = string.Empty;
                this.txtConfirmPws.Focus();
                return;
            }

            this.UserChangePassword(this.txtPWD.Text);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (this.IsAdminRole)
            {
                try
                {
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.FixProductCardReport = this.txtFixProdCardRptName.Text;
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();

                    XtraMessageBox.Show("Save Fix Product Card Report Completed", "Result");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                   adminBll.TEST_PROC_EXCEPTION("TEST");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }


    }
}