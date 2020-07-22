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
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.AdvanceSearch
{
    public partial class frmAdvJobOrder : BaseDialogForm, IDisposable
    {
        public frmAdvJobOrder()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);
        }

        ~frmAdvJobOrder()
        {
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

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.DialogIdle.Stop();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Variable Member

        private string _JOB_NO;
        private string _PROD_TYPE;
        private DateTime? _FROM_DATE;
        private DateTime? _TO_DATE;

        #endregion

        #region Property Member

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
        public string PROD_TYPE
        {
            get
            {
                return _PROD_TYPE;
            }
            set
            {
                if (_PROD_TYPE == value)
                    return;
                _PROD_TYPE = value;
            }
        }
        public DateTime? FROM_DATE
        {
            get
            {
                return _FROM_DATE;
            }
            set
            {
                if (_FROM_DATE == value)
                    return;
                _FROM_DATE = value;
            }
        }
        public DateTime? TO_DATE
        {
            get
            {
                return _TO_DATE;
            }
            set
            {
                if (_TO_DATE == value)
                    return;
                _TO_DATE = value;
            }
        }

        #endregion

        #region Method Member

        private void InitializaLOVData()
        {

            try
            {
                List<ProductionType> lstProductionType;
                using (ProductionTypeBLL pdtBll = new ProductionTypeBLL())
                {
                    lstProductionType = pdtBll.GetProductionTypeList();
                }
                this.luePRODUCTION_TYPE.Properties.DataSource = lstProductionType;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }
        #endregion

        private void frmAdvJobOrder_Load(object sender, EventArgs e)
        {
            this.InitializaLOVData();
            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.JOB_NO = this.txtJobOrderNo.Text;
            if (this.dteFromDate.Text != string.Empty)
            {
                this.FROM_DATE = this.dteFromDate.DateTime;
            }

            if (this.dteToDate.Text != string.Empty)
            {
                this.TO_DATE = this.dteToDate.DateTime;
            }

            this.DialogResult = DialogResult.OK;
        }

    }
}