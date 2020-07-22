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

namespace HTN.BITS.UIL.PLASESS.Query
{
    public partial class frmDocListInfo : BaseDialogForm
    {
        public frmDocListInfo()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdDocument);
        }

        #region "Dialog Idle Time"

        ~frmDocListInfo()
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

        #region "Private Member"

        #endregion

        #region "Property Member"

        #endregion

        #region "Method Member"

        

        //for Purchase Order from SAGE50
        public void Doc_GetPurchaseOrderList()
        {
            DataTable dt;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    dt = queryBll.Doc_PurchaseOrder_List();
                }

                this.grdDocument.DataSource = dt;
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

        //for Sales Order from SAGE50
        public void Doc_GetSalesOrderList()
        {
            DataTable dt;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QueryBLL queryBll = new QueryBLL())
                {
                    dt = queryBll.Doc_SalesOrder_List();
                }

                this.grdDocument.DataSource = dt;
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

        private void frmDocListInfo_Load(object sender, EventArgs e)
        {
            //this.GetProductList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }


        private void frmDocListInfo_LoadCompleted()
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

        private void frmDocListInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            this.Controls.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}