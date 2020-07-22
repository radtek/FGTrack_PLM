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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    public partial class frmLOVMtlType : BaseDialogForm
    {
        public frmLOVMtlType()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMaterial);

        }

        #region "Dialog Idle Time"

        ~frmLOVMtlType()
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

        private string _SEQ_NO;
        private string _NAME;

        #endregion

        #region "Property Member"

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
        public string MTLTYPT_NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                if (_NAME == value)
                    return;
                _NAME = value;
            }
        }

        #endregion

        #region "Method Member"

        public void GetMaterialtypeList()
        {
            List<MaterialType> lstMtlType = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MaterialTypeBLL mtltypeBll = new MaterialTypeBLL())
                {
                    lstMtlType = mtltypeBll.GetMTLTypeList(string.Empty);
                }

                this.grdMaterial.DataSource = lstMtlType;
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

        public void GetMaterialtypeListSearchValue(string search)
        {
            List<MaterialType> lstMtlType = null;
            try
            {
                using (MaterialTypeBLL mtltypeBll = new MaterialTypeBLL())
                {
                    lstMtlType = mtltypeBll.GetMTLTypeList(search);
                }

                this.grdMaterial.DataSource = lstMtlType;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        #endregion

        private void frmLOVMtlType_Load(object sender, EventArgs e)
        {
            //this.GetMaterialtypeList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmLOVMtlType_LoadCompleted()
        {
            this.GetMaterialtypeList();

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

        private void grvMaterial_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    this.SEQ_NO = view.GetRowCellValue(info.RowHandle, "SEQ_NO").ToString();
                    this.MTLTYPT_NAME = view.GetRowCellValue(info.RowHandle, "NAME").ToString();

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
                this.SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "SEQ_NO").ToString();
                this.MTLTYPT_NAME = view.GetRowCellValue(view.FocusedRowHandle, "NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(this, "No Record Found Can't Select", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtSearch.Focus();
                this.txtSearch.SelectAll();
            }
        }

        private void grvMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                this.SEQ_NO = view.GetRowCellValue(view.FocusedRowHandle, "SEQ_NO").ToString();
                this.MTLTYPT_NAME = view.GetRowCellValue(view.FocusedRowHandle, "NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            this.GetMaterialtypeListSearchValue(textEdit.Text);
        }

        private void frmLOVMtlType_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdMaterial.Views[0]);
            this.Controls.Clear();
        }


    }
}