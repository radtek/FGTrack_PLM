using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HTN.BITS.UIL.PLASESS.ConfirmForms
{
    public partial class frmCOFPrintCard : BaseDialogForm
    {
        public frmCOFPrintCard()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdPrintCard);
        }

        #region "Dialog Idle Time"

        ~frmCOFPrintCard()
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

        private DataTable _DT_PRINTED_TIME;

        #endregion

        #region "Property Member"

        public DataTable DT_PRINTED_TIME
        {
            get
            {
                return _DT_PRINTED_TIME;
            }
            set
            {
                if (_DT_PRINTED_TIME == value)
                    return;
                _DT_PRINTED_TIME = value;
            }
        }

        #endregion

        #region "Method Member"

        private void BindingPrintedTimes()
        {
            try
            {
                if (this._DT_PRINTED_TIME != null)
                {
                    this.lblPrintCount.Text = string.Format("{0:#,##0}", this._DT_PRINTED_TIME.Rows.Count);
                    this.grdPrintCard.DataSource = this._DT_PRINTED_TIME;
                    UiUtility.ClearSelection(this.grdPrintCard.MainView);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error BindingRouteActual", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        private void frmCOFPrintCard_Load(object sender, EventArgs e)
        {

        }


        private void frmCOFPrintCard_LoadCompleted()
        {
            this.BindingPrintedTimes();
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
            this.DialogResult = DialogResult.OK;
        }

        private void frmCOFPrintCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            this.Controls.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

    }
}