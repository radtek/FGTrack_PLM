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
using DevExpress.Data.Filtering;

namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    public partial class frmLOVMachine : BaseDialogForm
    {
        public frmLOVMachine()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdMachine);
        }

        #region "Dialog Idle Time"

        ~frmLOVMachine()
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

        private string _MACHINE_TYPE;
        private string _MC_NO;
        private string _MACHINE_NAME;

        #endregion

        #region "Property Member"

        public string MACHINE_TYPE
        {
            set
            {
                if (_MACHINE_TYPE == value)
                    return;
                _MACHINE_TYPE = value;
            }
        }
        public string MC_NO
        {
            get
            {
                return _MC_NO;
            }
        }
        public string MACHINE_NAME
        {
            get
            {
                return _MACHINE_NAME;
            }
        }

        #endregion

        #region "Method Member"

        public void GetMachineList()
        {
            List<Machine> lstMc = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (MachineBLL mcBll = new MachineBLL())
                {
                    lstMc = mcBll.GetMachineList(string.Empty);
                }

                this.grdMachine.DataSource = lstMc;

                if (!string.IsNullOrEmpty(this._MACHINE_TYPE))
                {
                    this.grvMachine.ActiveFilterCriteria = (new OperandProperty("MACHINE_TYPE") == new OperandValue(this._MACHINE_TYPE));
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

        public void GetMachineListSearchValue(string search)
        {
            List<Machine> lstMc = null;
            try
            {
                using (MachineBLL mcBll = new MachineBLL())
                {
                    lstMc = mcBll.GetMachineList(search);
                }

                this.grdMachine.DataSource = lstMc;

                if (!string.IsNullOrEmpty(this._MACHINE_TYPE))
                {
                    this.grvMachine.ActiveFilterCriteria = (new OperandProperty("MACHINE_TYPE") == new OperandValue(this._MACHINE_TYPE));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }


        #endregion

        private void frmLOVMachine_Load(object sender, EventArgs e)
        {
            //this.GetMachineList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmLOVMachine_LoadCompleted()
        {
            this.GetMachineList();

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

        private void grvMachine_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    this._MC_NO = view.GetRowCellValue(info.RowHandle, "MC_NO").ToString();
                    this._MACHINE_NAME = view.GetRowCellValue(info.RowHandle, "MACHINE_NAME").ToString();

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
            GridView view = (GridView)this.grdMachine.Views[0]; //

            if (view.RowCount > 0)
            {
                this._MC_NO = view.GetRowCellValue(view.FocusedRowHandle, "MC_NO").ToString();
                this._MACHINE_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MACHINE_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(this, "No Record Found Can't Select", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtSearch.Focus();
                this.txtSearch.SelectAll();
            }
        }

        private void grvMachine_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                this._MC_NO = view.GetRowCellValue(view.FocusedRowHandle, "MC_NO").ToString();
                this._MACHINE_NAME = view.GetRowCellValue(view.FocusedRowHandle, "MACHINE_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            this.GetMachineListSearchValue(textEdit.Text);
        }

        private void frmLOVMachine_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdMachine.Views[0]);
            this.Controls.Clear();
        }


    }
}