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

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPopResultUpRevision : BaseDialogForm, IDisposable
    {
        public frmPopResultUpRevision()
        {
            InitializeComponent();
            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdRetUpRevision);
        }

        #region "Dialog Idle Time"

        ~frmPopResultUpRevision()
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

        private string _SEQ_NO;
        private string _USER_ID;

        #endregion

        #region Property Member

        public string SEQ_NO
        {
            get
            {
                return _SEQ_NO;
            }
            set
            {
                if (_SEQ_NO == value)
                    return;
                _SEQ_NO = value;
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

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region Method Member

        private void GetResultUploadRevisionb()
        {
            DataTable dtbResult;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (InterfaceDataBLL inDataBll = new InterfaceDataBLL())
                {
                    dtbResult = inDataBll.GetResultUploadRevision(this._SEQ_NO, this._USER_ID);
                }

                this.grdRetUpRevision.DataSource = dtbResult;
                this.dntRetUpRevision.DataSource = dtbResult;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }

        #endregion

        private void frmPopResultUpRevision_Load(object sender, EventArgs e)
        {
            //this.GetResultUploadRevisionb();

            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmPopResultUpRevision_LoadCompleted()
        {
            this.GetResultUploadRevisionb();

            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPopResultUpRevision_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }


    }
}