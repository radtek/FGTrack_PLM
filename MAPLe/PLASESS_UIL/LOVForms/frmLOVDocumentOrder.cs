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
    public partial class frmLOVDocumentOrder : BaseDialogForm
    {
        public frmLOVDocumentOrder()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdDocument);
        }

        #region "Dialog Idle Time"

        ~frmLOVDocumentOrder()
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

        private string _DOC_NO;
        private string _PARTY_ID;
        

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

        public string DOC_NO
        {
            get
            {
                return _DOC_NO;
            }
            set
            {
                if (_DOC_NO == value)
                    return;
                _DOC_NO = value;
            }
        }

        #endregion

        #region "Method Member"

        

        //for Purchase Order from SAGE50
        public void Arrival_GetPurchaseOrderList()
        {
            List<Document> lstDocument = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ArrivalBLL arrivalBll = new ArrivalBLL())
                {
                    lstDocument = arrivalBll.LovDocumentPurchaseList(this._PARTY_ID, string.Empty);
                }

                this.grdDocument.DataSource = lstDocument;
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

        private void Arrival_GetPurchaseOrderList_Search(string search)
        {
            List<Document> lstDocument = null;

            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ArrivalBLL arrivalBll = new ArrivalBLL())
                {
                    lstDocument = arrivalBll.LovDocumentPurchaseList(this._PARTY_ID, search);
                }

                this.grdDocument.DataSource = lstDocument;
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
        public void Shipping_GetSalesOrderList()
        {
            List<Document> lstDocument = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shippingBll = new ShippingOrderBLL())
                {
                    lstDocument = shippingBll.LovDocumentSalesList(this._PARTY_ID, string.Empty);
                }

                this.grdDocument.DataSource = lstDocument;
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

        private void Shipping_GetSalesOrderList_Search(string search)
        {
            List<Document> lstDocument = null;

            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ShippingOrderBLL shippingBll = new ShippingOrderBLL())
                {
                    lstDocument = shippingBll.LovDocumentSalesList(this._PARTY_ID, search);
                }

                this.grdDocument.DataSource = lstDocument;
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
        //public void ShippingOrder_GetProductList()
        //{
        //    List<Product> lstPrd = null;
        //    try
        //    {
        //        base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

        //        using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
        //        {
        //            lstPrd = shipOrdBll.LovProductList(this._WH_ID,this._PARTY_ID, this._PO_NO , string.Empty);
        //        }

        //        this.grdDocument.DataSource = lstPrd;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.FinishedProcessing();
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //    finally
        //    {
        //        GridView view = (GridView)this.grdDocument.Views[0];
        //        view.Columns["FREE_STOCK"].Visible = true;

        //        base.FinishedProcessing();
        //    }
        //}

        //private void ShippingOrder_GetProductList_Search(string search)
        //{
        //    List<Product> lstPrd = null;
        //    try
        //    {
        //        base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

        //        using (ShippingOrderBLL shipOrdBll = new ShippingOrderBLL())
        //        {
        //            lstPrd = shipOrdBll.LovProductList(this._WH_ID, this._PARTY_ID, this._PO_NO, search);
        //        }

        //        this.grdDocument.DataSource = lstPrd;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.FinishedProcessing();
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //    finally
        //    {
        //        base.FinishedProcessing();
        //    }
        //}

        #endregion

        private void frmLOVDocumentOrder_Load(object sender, EventArgs e)
        {
            //this.GetProductList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }


        private void frmLOVDocumentOrder_LoadCompleted()
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
                    this.DOC_NO = view.GetRowCellValue(info.RowHandle, "DOC_NO").ToString();

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
            GridView view = (GridView)this.grdDocument.Views[0]; //

            if (view.RowCount > 0)
            {
                this.DOC_NO = view.GetRowCellValue(view.FocusedRowHandle, "DOC_NO").ToString();

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
                this.DOC_NO = view.GetRowCellValue(view.FocusedRowHandle, "DOC_NO").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            switch (this.FormCalling)
            {
                case eFormCalling.fArrivalEntry:
                    this.Arrival_GetPurchaseOrderList_Search(textEdit.Text);
                    break;
                case eFormCalling.fShippingOrder:
                    this.Shipping_GetSalesOrderList_Search(textEdit.Text);
                    break;
                default:
                    break;
            }
            
        }

        private void frmLOVDocumentOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            this.Controls.Clear();
        }

    }
}