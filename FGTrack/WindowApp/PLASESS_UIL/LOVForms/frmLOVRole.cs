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
    public partial class frmLOVRole : BaseDialogForm
    {
        public frmLOVRole()
        {
            InitializeComponent();

            check = new GridCheckMarksSelection(this.grvRole);

            this.resultList = new List<Role>();
            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdRole);
        }

        ~frmLOVRole()
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

        private List<Role> lstRole;
        private GridCheckMarksSelection check;

        #endregion

        #region "Property Member"

        private List<Role> resultList;

        public List<Role> ResultList
        {
            get
            {
                return resultList;
            }
            set
            {
                resultList = value;
            }
        }

        #endregion "Property Member"

        #region "Method Member"

        private void GetRoleList()
        {
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    this.lstRole = adminBll.GetRoleList();
                }

                if (this.lstRole != null)
                {
                    this.grdRole.DataSource = this.lstRole;
                }
                else
                {
                    this.grdRole.DataSource = null;
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

        private void frmLOVRole_Load(object sender, EventArgs e)
        {
            //this.GetRoleList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmLOVRole_LoadCompleted()
        {
            this.GetRoleList();
            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void grvRole_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {

                Role roleSelect = new Role();

                roleSelect.ROLE_ID = view.GetRowCellValue(info.RowHandle, "ROLE_ID").ToString();
                roleSelect.ROLE_NAME = view.GetRowCellValue(info.RowHandle, "ROLE_NAME").ToString();


                this.resultList.Add(roleSelect);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void grvRole_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                Role roleSelect = new Role();

                roleSelect.ROLE_ID = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_ID").ToString();
                roleSelect.ROLE_NAME = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_NAME").ToString();


                this.resultList.Add(roleSelect);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.check.SelectedCount > 0)
            {
                for (int i = 0; i < check.SelectedCount; i++)
                {
                    this.resultList.Add((Role)check.GetSelectedRow(i));
                }

                this.DialogResult = DialogResult.OK;

            }
            else
            {
                //MessageBox.Show("PLEASE SELECT LABEL TO PRINT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this, "PLEASE SELECT ROLE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmLOVRole_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.grdRole);
            this.Controls.Clear();
        }


    }
}