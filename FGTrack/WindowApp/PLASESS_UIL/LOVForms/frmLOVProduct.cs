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

namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    public partial class frmLOVProduct : BaseDialogForm
    {
        public frmLOVProduct()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProduct);
        }

        #region "Dialog Idle Time"

        ~frmLOVProduct()
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
        private eFormCalling _FormCalling;
        private string _WH_ID;
        private string _PRODUCTION_TYPE;

        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MATERIAL_NAME;

        private string _PARTY_ID;
        private string _PO_NO;

        #endregion

        #region "Property Member"
        public eFormCalling FormCalling
        {
            get
            {
                return _FormCalling;
            }
            set
            {
                if (_FormCalling == value)
                    return;
                _FormCalling = value;
            }
        }

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
        public string PRODUCTION_TYPE
        {
            get
            {
                return _PRODUCTION_TYPE;
            }
            set
            {
                if (_PRODUCTION_TYPE == value)
                    return;
                _PRODUCTION_TYPE = value;
            }
        }

        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
            }
        }
        public string PRODUCT_NO
        {
            get
            {
                return _PRODUCT_NO;
            }
            set
            {
                if (_PRODUCT_NO == value)
                    return;
                _PRODUCT_NO = value;
            }
        }
        public string PRODUCT_NAME
        {
            get
            {
                return _PRODUCT_NAME;
            }
            set
            {
                if (_PRODUCT_NAME == value)
                    return;
                _PRODUCT_NAME = value;
            }
        }
        public string MATERIAL_NAME
        {
            get
            {
                return _MATERIAL_NAME;
            }
            set
            {
                if (_MATERIAL_NAME == value)
                    return;
                _MATERIAL_NAME = value;
            }
        }


        public string PARTY_ID
        {
            get
            {
                return _PARTY_ID;
            }
            set
            {
                if (_PARTY_ID == value)
                    return;
                _PARTY_ID = value;
            }
        }
        public string PO_NO
        {
            get
            {
                return _PO_NO;
            }
            set
            {
                if (_PO_NO == value)
                    return;
                _PO_NO = value;
            }
        }
        #endregion

        #region "Method Member"

        //for Job Order
        public void JobOrder_GetProductList()
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prdBll = new ProductBLL())
                {
                    lstPrd = prdBll.LovProductList(this._PRODUCTION_TYPE, this._PARTY_ID, string.Empty);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                GridView view = (GridView)this.grdProduct.Views[0];
                view.Columns["FREE_STOCK"].Visible = false;
                base.FinishedProcessing();
            }

        }

        private void JobOrder_GetProductList_Search(string search)
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prdBll = new ProductBLL())
                {
                    lstPrd = prdBll.LovProductList(this._PRODUCTION_TYPE, this._PARTY_ID, search);
                }

                this.grdProduct.DataSource = lstPrd;
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

        //for Upload Plan
        public void ULPlan_GetProductList()
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prdBll = new ProductBLL())
                {
                    lstPrd = prdBll.LovProductList(this._PRODUCTION_TYPE, this._PARTY_ID, string.Empty);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                GridView view = (GridView)this.grdProduct.Views[0];
                view.Columns["FREE_STOCK"].Visible = false;
                base.FinishedProcessing();
            }

        }

        private void ULPlan_GetProductList_Search(string search)
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ProductBLL prdBll = new ProductBLL())
                {
                    lstPrd = prdBll.LovProductList(this._PRODUCTION_TYPE, this._PARTY_ID, search);
                }

                this.grdProduct.DataSource = lstPrd;
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

        //for Shipping Order
        public void ShippingOrder_GetProductList()
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstPrd = shipOrdBll.LovProductList(this._WH_ID,this._PARTY_ID, this._PO_NO , string.Empty);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                GridView view = (GridView)this.grdProduct.Views[0];
                view.Columns["FREE_STOCK"].Visible = true;

                base.FinishedProcessing();
            }
        }

        private void ShippingOrder_GetProductList_Search(string search)
        {
            List<Product> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
                {
                    lstPrd = shipOrdBll.LovProductList(this._WH_ID, this._PARTY_ID, this._PO_NO, search);
                }

                this.grdProduct.DataSource = lstPrd;
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

        //for Delivery Order
        public void DeliveryOrder_GetProductList()
        {
            List<Product> lstPrd = null;
            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    lstPrd = delOrdBll.LovProductList(string.Empty, this._PRODUCTION_TYPE);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                GridView view = (GridView)this.grdProduct.Views[0];
                view.Columns["FREE_STOCK"].Visible = false;
            }
        }

        private void DeliveryOrder_GetProductList_Search(string search)
        {
            List<Product> lstPrd = null;
            try
            {
                using (DeliveryOrderBLL delOrdBll = new DeliveryOrderBLL())
                {
                    lstPrd = delOrdBll.LovProductList(search, this._PRODUCTION_TYPE);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //for Transfer Order
        public void TransferOrder_GetProductList()
        {
            List<Product> lstPrd = null;
            try
            {
                using (TransferOrderBLL to_Bll = new TransferOrderBLL())
                {
                    lstPrd = to_Bll.LovProductList(string.Empty,this._PRODUCTION_TYPE, this._PARTY_ID);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {

            }
        }

        private void TransferOrder_GetProductList_Search(string search)
        {
            List<Product> lstPrd = null;
            try
            {
                using (TransferOrderBLL toBll = new TransferOrderBLL())
                {
                    lstPrd = toBll.LovProductList(search, this._PRODUCTION_TYPE, this._PARTY_ID);
                }

                this.grdProduct.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        private void frmLOVProduct_Load(object sender, EventArgs e)
        {
            //this.GetProductList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }


        private void frmLOVProduct_LoadCompleted()
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

        private void grvProduct_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    this.PROD_SEQ_NO = view.GetRowCellValue(info.RowHandle, "PROD_SEQ_NO").ToString();
                    this.PRODUCT_NO = view.GetRowCellValue(info.RowHandle, "PRODUCT_NO").ToString();
                    this.PRODUCT_NAME = view.GetRowCellValue(info.RowHandle, "PRODUCT_NAME").ToString();
                    this.MATERIAL_NAME = view.GetRowCellValue(info.RowHandle, "MATERIAL_NAME").ToString();

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdProduct.Views[0]; //

            if (view.RowCount > 0)
            {
                this.PROD_SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "PROD_SEQ_NO").ToString();
                this.PRODUCT_NO = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NO").ToString();
                this.PRODUCT_NAME = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NAME").ToString();
                this.MATERIAL_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MATERIAL_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(this, "No Record Found Can't Select", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtSearch.Focus();
                this.txtSearch.SelectAll();
            }
        }

        private void grvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                this.PROD_SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "PROD_SEQ_NO").ToString();
                this.PRODUCT_NO = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NO").ToString();
                this.PRODUCT_NAME = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NAME").ToString();
                this.MATERIAL_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MATERIAL_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            switch (this.FormCalling)
            {
                case eFormCalling.fJobOrder:
                    this.JobOrder_GetProductList_Search(textEdit.Text);
                    break;
                case eFormCalling.fShippingOrder:
                    this.ShippingOrder_GetProductList_Search(textEdit.Text);
                    break;
                case eFormCalling.fDeliveryOrder:
                    this.DeliveryOrder_GetProductList_Search(textEdit.Text);
                    break;
                case eFormCalling.fTransferOrder:
                    this.TransferOrder_GetProductList_Search(textEdit.Text);
                    break;
                case eFormCalling.fUploadPlan:
                    this.ULPlan_GetProductList_Search(textEdit.Text);
                    break;
                default:
                    break;
            }
            
        }

        private void frmLOVProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            this.Controls.Clear();
        }

    }
}