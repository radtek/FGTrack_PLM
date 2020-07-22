using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;
using HTN.BITS.BLL.PLASESS;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    public partial class frmLOVPartyProduct : BaseDialogForm
    {
        public frmLOVPartyProduct()
        {
            InitializeComponent();

            check = new GridCheckMarksSelection(this.grvProductList);

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProductList);
        }

        ~frmLOVPartyProduct()
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

        #region "Variable Member"

        private List<Product> lstProduct;
        private GridCheckMarksSelection check;

        private string _PARTY_ID;
        private List<Product> _PRODUCT_LIST;

        #endregion

        #region "Property Member"

        public string PARTY_ID
        {
            get { return this._PARTY_ID; }
            set
            {
                if (this._PARTY_ID == value)
                    return;
                this._PARTY_ID = value;
            }
        }

        public List<Product> PRODUCT_LIST
        {
            get
            {
                return _PRODUCT_LIST;
            }
            set
            {
                _PRODUCT_LIST = value;
            }
        }

        #endregion "Property Member"

        #region "Method Member"

        private void GetProductList(string findAll)
        {
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (PartyBLL partyBll = new PartyBLL())
                {
                    this.lstProduct = partyBll.LOV_GetProductList(this._PARTY_ID, findAll);
                }

                if (this.lstProduct != null)
                {
                    this.grdProductList.DataSource = this.lstProduct;
                }
                else
                {
                    this.grdProductList.DataSource = null;
                }
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

        private void frmLOVPartyProduct_Load(object sender, EventArgs e)
        {
        }

        private void frmLOVPartyProduct_LoadCompleted()
        {
            this.GetProductList(string.Empty);

            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void grvProductList_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                if (this._PRODUCT_LIST == null)
                    this._PRODUCT_LIST = new List<Product>();


                Product prod = new Product();

                prod.PROD_SEQ_NO = view.GetRowCellValue(info.RowHandle, "PROD_SEQ_NO").ToString();
                prod.PRODUCT_NO = view.GetRowCellValue(info.RowHandle, "PRODUCT_NO").ToString();
                prod.PRODUCT_NAME = view.GetRowCellValue(info.RowHandle, "PRODUCT_NAME").ToString();

                this._PRODUCT_LIST.Add(prod);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void grvProductList_KeyDown(object sender, KeyEventArgs e)
        {
            

            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                if (this._PRODUCT_LIST == null)
                    this._PRODUCT_LIST = new List<Product>();

                Product prod = new Product();

                prod.PROD_SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "PROD_SEQ_NO").ToString();
                prod.PRODUCT_NO = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NO").ToString();
                prod.PRODUCT_NAME = view.GetRowCellValue(view.FocusedRowHandle, "PRODUCT_NAME").ToString();

                this._PRODUCT_LIST.Add(prod);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.check.SelectedCount > 0)
            {
                if (this._PRODUCT_LIST == null)
                    this._PRODUCT_LIST = new List<Product>();

                for (int i = 0; i < check.SelectedCount; i++)
                {
                    this._PRODUCT_LIST.Add((Product)check.GetSelectedRow(i));
                }

                this.DialogResult = DialogResult.OK;

            }
            else
            {
                XtraMessageBox.Show(this, "PLEASE SELECT PRODUCT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmLOVPartyProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.grdRole);
            this.Controls.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;

            this.GetProductList(textEdit.Text);
        }


    }
}