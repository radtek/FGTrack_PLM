using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;

//Using for Business Logic Layer
using HTN.BITS.LIB;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BLL.PLASESS;

namespace HTN.BITS.UIL.PLASESS
{
    public partial class frmLogin : BaseForm, IDisposable
    {

        private string userid;

        public string UserID
        {
            get
            {
                return this.userid;
            }

            set
            {
            	this.userid = value;
            }
        }

        public frmLogin()
        {
            InitializeComponent();
            GC.ReRegisterForFinalize(this);

            this.CustomInitializeComponent();

            this.LogInIdle.IdleTime = System.TimeSpan.Parse(UiUtility.LogInIdleTime);
            this.LogInIdle.IdleAsync += new EventHandler(this.LogInIdle_IdleAsync);
        }

        ~frmLogin()
        {
            this.LogInIdle.IdleAsync -= new EventHandler(this.LogInIdle_IdleAsync);
        }

        private void LogInIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.LogIn_Idle(sender, e); })
                );
        }

        private void LogIn_Idle(object sender, EventArgs e)
        {
            if (this.LogInIdle.IsRunning)
            {
                this.LogInIdle.Stop();
            }

            this.Controls.Clear();
            this.DialogResult = DialogResult.Cancel;
        }

        private void CustomInitializeComponent()
        {
            this.lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.picLoginHeader.Image = base.Language.GetBitmap("LoginHeader");
            this.picUserAuthentication.Image = base.Language.GetBitmap("UserAuthentication");
            this.btnLogin.Image = base.Language.GetBitmap("ButtonLogin");
        }

        private bool ValidationForms()
        {
            //for user name field
            if (this.txtUserName.Text.Trim().Length == 0)
            {
                this.txtUserName.Focus();
                return false;
            }

            //for password field
            if (this.txtPassword.Text.Trim().Length == 0)
            {
                this.txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidationForms()) return;
            bool isVid = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (AdministratorBLL adminBLL = new AdministratorBLL())
                {
                    isVid = adminBLL.UserAuthentication(this.txtUserName.Text, this.txtPassword.Text);

                    if (isVid)
                    {
                        if (this.chkRememberMe.Checked)
                        {
                            HTN.BITS.UIL.PLASESS.Properties.Settings.Default.RememberUser = this.txtUserName.Text;
                            HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
                        }
                        else
                        {
                            HTN.BITS.UIL.PLASESS.Properties.Settings.Default.RememberUser = string.Empty;
                            HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
                        }

                        if (this.LogInIdle.IsRunning)
                        {
                            this.LogInIdle.Stop();
                        }
                        //is authentication pass
                        this.UserID = adminBLL.UserID;
                        Cursor.Current = Cursors.Default;
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        //is authentication fail
                        //this.DialogResult = DialogResult.No;
                        XtraMessageBox.Show("Invalid User name or Password", "Authentication", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //this.lblTest.Text = base.Language.GetValue("frmLogin.lblTest");
            string username = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.RememberUser;
            if(!string.IsNullOrEmpty(username))
            {
                this.chkRememberMe.Checked = true;
                this.txtUserName.EditValue = username;
                this.ActiveControl = this.txtPassword;
            }

            if (!this.LogInIdle.IsRunning)
            {
                this.LogInIdle.Start();
            }
        }

        private void imageComboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            UiUtility.LanguageUse = ((ImageComboBoxEdit)sender).Value.ToString();
            base.Language.ChangeLanguage(UiUtility.LanguageUse);

            this.frmLogin_Load(this, new EventArgs());
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            Validations.Validate_EmptyStringRule(sender as BaseEdit, ref this.dxpErr);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            Validations.Validate_EmptyStringRule(sender as BaseEdit, ref this.dxpErr);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}