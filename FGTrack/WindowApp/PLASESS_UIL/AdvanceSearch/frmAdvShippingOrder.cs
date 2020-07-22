using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;

namespace HTN.BITS.UIL.PLASESS.AdvanceSearch
{
    public partial class frmAdvShippingOrder : BaseDialogForm, IDisposable
    {
        public frmAdvShippingOrder()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);
        }

        ~frmAdvShippingOrder()
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
        private string _SO_NO;
        private DateTime? _FROM_DATE;
        private DateTime? _TO_DATE;
        private string _REF_NO;
        private DateTime? _REF_FROM_DATE;
        private DateTime? _REF_TO_DATE;

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
        public string SO_NO
        {
            get
            {
                return _SO_NO;
            }
            set
            {
                if (_SO_NO == value)
                    return;
                _SO_NO = value;
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
        public string REF_NO
        {
            get
            {
                return _REF_NO;
            }
            set
            {
                if (_REF_NO == value)
                    return;
                _REF_NO = value;
            }
        }
        public DateTime? REF_FROM_DATE
        {
            get
            {
                return _REF_FROM_DATE;
            }
            set
            {
                if (_REF_FROM_DATE == value)
                    return;
                _REF_FROM_DATE = value;
            }
        }
        public DateTime? REF_TO_DATE
        {
            get
            {
                return _REF_TO_DATE;
            }
            set
            {
                if (_REF_TO_DATE == value)
                    return;
                _REF_TO_DATE = value;
            }
        }

        #endregion

        private void frmAdvShippingOrder_Load(object sender, EventArgs e)
        {
            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.SO_NO = this.txtSO_NO.Text;
            this.REF_NO = this.txtREF_NO.Text;

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