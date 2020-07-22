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
    public partial class frmAdvLoadingOrder : BaseDialogForm, IDisposable
    {
        public frmAdvLoadingOrder()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);
        }

        ~frmAdvLoadingOrder()
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

        private string _WH_ID;
        private string _LOADING_NO;
        private DateTime? _FROM_DATE;
        private DateTime? _TO_DATE;

        #endregion

        #region Property Member

        public string WH_ID
        {
            get
            {
                return _WH_ID;
            }
            set
            {
                if (_WH_ID == value)
                    return;
                _WH_ID = value;
            }
        }
        public string LOADING_NO
        {
            get
            {
                return _LOADING_NO;
            }
            set
            {
                if (_LOADING_NO == value)
                    return;
                _LOADING_NO = value;
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
                using (LoadingOrderBLL loadingOrdBll = new LoadingOrderBLL())
                {
                    List<Warehouse> lstWH = loadingOrdBll.GetWarehouse();
                    if (lstWH != null)
                    {
                        this.lueWarehouse.Properties.DataSource = lstWH;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        #endregion

        private void frmAdvLoadingOrder_Load(object sender, EventArgs e)
        {
            this.InitializaLOVData();
            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.lueWarehouse.EditValue != null)
            {
                this.WH_ID = this.lueWarehouse.EditValue.ToString();
            }

            this.LOADING_NO = this.txtLOADING_NO.Text;

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