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
    public partial class frmLOVMtl : BaseDialogForm
    {
        public frmLOVMtl()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMaterial);
        }

        #region "Dialog Idle Time"

        ~frmLOVMtl()
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
        //private string _PRODUCTION_TYPE;

        private string _MTL_SEQ_NO;
        private string _MTL_CODE;
        private string _MTL_NAME;
        private string _UNIT;
        private int _STD_QTY;

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
        //public string PRODUCTION_TYPE
        //{
        //    get
        //    {
        //        return _PRODUCTION_TYPE;
        //    }
        //    set
        //    {
        //        if (_PRODUCTION_TYPE == value)
        //            return;
        //        _PRODUCTION_TYPE = value;
        //    }
        //}

        public string MTL_SEQ_NO
        {
            get
            {
                return _MTL_SEQ_NO;
            }
            set
            {
                if (_MTL_SEQ_NO == value)
                    return;
                _MTL_SEQ_NO = value;
            }
        }
        public string MTL_CODE
        {
            get
            {
                return _MTL_CODE;
            }
            set
            {
                if (_MTL_CODE == value)
                    return;
                _MTL_CODE = value;
            }
        }
        public string MTL_NAME
        {
            get
            {
                return _MTL_NAME;
            }
            set
            {
                if (_MTL_NAME == value)
                    return;
                _MTL_NAME = value;
            }
        }
        public string UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                if (_UNIT == value)
                    return;
                _UNIT = value;
            }
        }

        public int STD_QTY
        {
            get
            {
                return _STD_QTY;
            }
            set
            {
                if (_STD_QTY == value)
                    return;
                _STD_QTY = value;
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

        public void GetMaterialList()
        {
            List<Material> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ArrivalBLL prdBll = new ArrivalBLL())
                {
                    lstPrd = prdBll.GetMaterialList(string.Empty, string.Empty, this.PARTY_ID, this.WH_ID);
                }

                this.grdMaterial.DataSource = lstPrd;
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                //GridView view = (GridView)this.grdProduct.Views[0];
                //view.Columns["FREE_STOCK"].Visible = false;
                base.FinishedProcessing();
            }

        }

        private void GetMaterialList_Search(string search)
        {
            List<Material> lstPrd = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ArrivalBLL prdBll = new ArrivalBLL())
                {
                    lstPrd = prdBll.GetMaterialList(this.txtSearch.Text, string.Empty, this.PARTY_ID,this.WH_ID);
                }

                this.grdMaterial.DataSource = lstPrd;
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

        private void frmLOVMtl_Load(object sender, EventArgs e)
        {
            //this.GetProductList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }


        private void frmLOVMtl_LoadCompleted()
        {

            this.GetMaterialList();
            
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
                    this.MTL_SEQ_NO = view.GetRowCellValue(info.RowHandle, "MTL_SEQ_NO").ToString();
                    this.MTL_CODE = view.GetRowCellValue(info.RowHandle, "MTL_CODE").ToString();
                    this.MTL_NAME = view.GetRowCellValue(info.RowHandle, "MTL_NAME").ToString();
                    this.UNIT = view.GetRowCellValue(info.RowHandle, "UNIT").ToString();



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
            GridView view = (GridView)this.grdMaterial.Views[0]; //

            if (view.RowCount > 0)
            {
                this.MTL_SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "MTL_SEQ_NO").ToString();
                this.MTL_CODE = view.GetRowCellValue(view.FocusedRowHandle, "MTL_CODE").ToString();
                this.MTL_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MTL_NAME").ToString();
                this.UNIT = view.GetRowCellValue(view.FocusedRowHandle, "UNIT").ToString();

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
                this.MTL_SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "MTL_SEQ_NO").ToString();
                this.MTL_CODE = view.GetRowCellValue(view.FocusedRowHandle, "MTL_CODE").ToString();
                this.MTL_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MTL_NAME").ToString();
                this.UNIT = view.GetRowCellValue(view.FocusedRowHandle, "UNIT").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            switch (this.FormCalling)
            {
                case eFormCalling.fArrivalEntry:
                    this.GetMaterialList_Search(textEdit.Text);
                    break;
                default:
                    break;
            }
            
        }

        private void frmLOVMtl_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProduct.Views[0]);
            this.Controls.Clear();
        }

    }
}