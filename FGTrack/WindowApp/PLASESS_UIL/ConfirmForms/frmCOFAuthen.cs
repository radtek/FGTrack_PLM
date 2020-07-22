using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BLL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.ConfirmForms
{
    public partial class frmCOFAuthen : BaseDialogForm
    {
        public frmCOFAuthen()
        {
            InitializeComponent();

            base.LoadFormLayout();
        }

        #region "Dialog Idle Time"

        ~frmCOFAuthen()
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



        #region "Variable Member"
        #endregion

        #region "Property Member"
        #endregion

        #region "Method Member"

        private bool SucessAuthentication(string uname, string password)
        {
            bool isSucess = false;

            try
            {
                using (AdministratorBLL userBLL = new AdministratorBLL())
                {
                    isSucess = userBLL.ManagerAuthenRollback(uname, password, string.Empty);
                }
            }
            catch
            {
                isSucess = false;
            }

            return isSucess;
        }

        #endregion

        private void frmCOFAuthen_LoadCompleted()
        {

            //for set idle time
            if (UiUtility.IsAppIdleTime)
            {
                base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
                base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

                if (!base.DialogIdle.IsRunning)
                {
                    base.DialogIdle.Start();
                }

            }
            else
            {
                base.DialogIdle.Stop();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "")
            {
                XtraMessageBox.Show(this, "User Can't be null", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtUserName.Focus();
                return;
            }

            if (this.txtPassword.Text == "")
            {
                XtraMessageBox.Show(this, "PASSWORD Can't be null", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtPassword.Focus();
                return;
            }



            bool isPass = this.SucessAuthentication(this.txtUserName.Text, this.txtPassword.Text);

            if (isPass)
            {
                //UiUtility.USER_PRINT_ID = this.txtUserName.Text;
                //  this._USER_PRINT_ID = this.txtUserName.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(this, "Delete Authentication Fail!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}