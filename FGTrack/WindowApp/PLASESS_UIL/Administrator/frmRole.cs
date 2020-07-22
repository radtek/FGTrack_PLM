using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using DevExpress.XtraGrid.Views.Grid;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS.Administrator
{
    public partial class frmRole : BaseChildFormPopup
    {
        public frmRole()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdRole);
            base.LoadGridLayout(this.grdRoleProgram);
        }

        #region "Variable Member"

        private DataTable dtbRole = null;
        private DataTable dtbRoleProg = null;

        private eFormState formState = eFormState.ReadOnly;
        private int positionIndex = -1;

        #endregion "Variable Member"

        #region "Property Member"

        public eFormState FormState
        {
            get
            {
                return this.formState;
            }

            set
            {
                this.formState = value;
                this.ChangeFormState(value);
            }
        }

        #endregion "Property Member"

        #region "Method Member"

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = (GridView)this.grdRole.Views[0];
                GridView view2 = (GridView)this.grdRoleProgram.Views[0];

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        //set readonly status for grdInvoiceDetail

                        view.OptionsBehavior.Editable = true;

                        view.Columns[0].OptionsColumn.AllowEdit = true;
                        view.Columns[0].OptionsColumn.AllowFocus = true;

                        this.dntRole.TextStringFormat = "      Add Mode      ";
                        this.dntRole.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        view2.OptionsBehavior.Editable = true;


                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        UiUtility.SetGridEditOnly(view, true, 0);

                        view.Columns[1].OptionsColumn.AllowEdit = true;
                        view.Columns[1].OptionsColumn.AllowFocus = true;

                        this.dntRole.TextStringFormat = "      Edit Mode      ";
                        this.dntRole.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);
                        view2.OptionsBehavior.Editable = true;

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        //set readonly status for grdInvoiceDetail;
                        UiUtility.SetGridReadOnly(view, true);

                        this.dntRole.TextStringFormat = " Record {0} of {1} ";
                        this.dntRole.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = false;

                        view.OptionsBehavior.Editable = false;
                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);
                        view2.OptionsBehavior.Editable = false;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetRoleList()
        {
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    this.dtbRole = adminBll.GetRoles(string.Empty);
                }

                if (this.dtbRole != null)
                {
                    this.grdRole.DataSource = this.dtbRole;
                    this.dntRole.DataSource = this.dtbRole;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetEditRole(string roleID)
        {
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    this.dtbRole = adminBll.GetRoles(roleID);
                }

                if (this.dtbRole != null)
                {
                    this.grdRole.DataSource = this.dtbRole;
                    this.dntRole.DataSource = this.dtbRole;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetRoleProgram(string roleID)
        {
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    this.dtbRoleProg = adminBll.GetRoleProgramByRole(roleID);
                }

                if (this.dtbRoleProg != null)
                {
                    this.grdRoleProgram.DataSource = this.dtbRoleProg;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void InsertRole()
        {
            string result = string.Empty;
            Role role = null;
            try
            {
                DataRow rowRole = this.dtbRole.Rows[0];
                if (rowRole != null)
                {
                    role = new Role();

                    role.ROLE_ID = rowRole["ROLE_ID"].ToString().ToUpper();
                    role.ROLE_NAME = rowRole["ROLE_NAME"].ToString();
                    role.REC_STAT = (bool)rowRole["REC_STAT"];
                }

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.InsertRole(role, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.GetRoleList();

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdRole.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "ROLE_ID", role.ROLE_ID);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntRole.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }
        }

        private void UpdateRole()
        {
            List<RoleProgram> lstRowProgUpd = null;
            Role role = null;
            string result = string.Empty;
            string userid = ((frmMainMenu)this.ParentForm).UserID;

            try
            {
                DataRow rowRole = this.dtbRole.Rows[0];
                if (rowRole != null)
                {
                    role = new Role();

                    role.ROLE_ID = rowRole["ROLE_ID"].ToString().ToUpper();
                    role.ROLE_NAME = rowRole["ROLE_NAME"].ToString();
                    role.REC_STAT = (bool)rowRole["REC_STAT"];
                }

                if (this.dtbRoleProg.Rows.Count != 0)
                {
                    lstRowProgUpd = new List<RoleProgram>();
                    int flag = 0;
                    RoleProgram rowProgUpd;
                    foreach (DataRow rowProg in this.dtbRoleProg.Rows)
                    {
                        flag = Convert.ToInt32(rowProg["FLAG"], NumberFormatInfo.CurrentInfo);
                        if (flag == 2)
                        {
                            rowProgUpd = new RoleProgram();
                            rowProgUpd.ROLE_ID = rowProg["ROLE_ID"].ToString();
                            rowProgUpd.PROG_ID = rowProg["PROG_ID"].ToString();
                            rowProgUpd.REC_STAT = (bool)rowProg["REC_STAT"];
                            lstRowProgUpd.Add(rowProgUpd);
                        }
                    }
                }

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UpdateRole(role, userid);
                    if(lstRowProgUpd != null)
                    {
                        adminBll.UpdateRoleProgram(lstRowProgUpd, userid);
                    }
                }

                if (result.Equals("OK"))
                {
                    NotifierResult.Show("Update Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.GetRoleList();

                this.FormState = eFormState.ReadOnly;

                this.dntRole.Position = this.positionIndex;

                this.btnAddNew.Visible = true;
                this.btnExit.Visible = true;

                //Reload Menu
                if (lstRowProgUpd != null)
                {
                    ((frmMainMenu)this.ParentForm).ReloadMenu();
                }
            }
        }

        #endregion

        private void frmRole_Load(object sender, EventArgs e)
        {
            //this.GetRoleList();
            //this.positionIndex = this.dntRole.Position;

            //GridView view = (GridView)this.grdRole.Views[0]; //this.gridView2
            //if (view.RowCount != 0)
            //{
            //    string roleID = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_ID").ToString();
            //    if (!string.IsNullOrEmpty(roleID))
            //    {
            //        this.GetRoleProgram(roleID);
            //    }
            //}



            //this.FormState = eFormState.ReadOnly;
        }

        private void frmRole_LoadCompleted()
        {
            this.GetRoleList();
            this.positionIndex = this.dntRole.Position;

            GridView view = (GridView)this.grdRole.Views[0]; //this.gridView2
            if (view.RowCount != 0)
            {
                string roleID = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_ID").ToString();
                if (!string.IsNullOrEmpty(roleID))
                {
                    this.GetRoleProgram(roleID);
                }
            }

            this.FormState = eFormState.ReadOnly;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.positionIndex = this.dntRole.Position;
            this.FormState = eFormState.Add;

            GridView view = (GridView)this.grdRole.Views[0];

            //Role 
            this.dtbRole.Rows.Clear();
            this.dtbRole.AcceptChanges();
            this.grdRole.DataSource = this.dtbRole;

            this.dtbRoleProg.Rows.Clear();
            this.dtbRoleProg.AcceptChanges();
            this.grdRoleProgram.DataSource = this.dtbRoleProg;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            view.AddNewRow();
            view.FocusedRowHandle = GridControl.NewItemRowHandle;

            view.FocusedColumn = view.Columns[0];
            view.ShowEditor();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.positionIndex = this.dntRole.Position;

            this.FormState = eFormState.Edit;

            GridView view = (GridView)this.grdRole.Views[0];
            string roleID = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_ID").ToString();

            this.GetEditRole(roleID);

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.grdRole.Focus();
            view.FocusedColumn = view.Columns[1];
            view.ShowEditor();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (!this.IsFormValidated()) return;
            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertRole();
                    break;
                case eFormState.Edit:
                    this.UpdateRole();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }

            this.FormState = eFormState.ReadOnly;

            this.btnAddNew.Visible = true;
            this.btnExit.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdRole.Views[0];

            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:
                        view.CancelUpdateCurrentRow();
                        break;
                    case eFormState.Edit:
                        view.CancelUpdateCurrentRow();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                this.GetRoleList();
                if (view.RowCount != 0)
                {
                    string roleID = view.GetRowCellValue(view.FocusedRowHandle, "ROLE_ID").ToString();
                    if (!string.IsNullOrEmpty(roleID))
                    {
                        this.GetRoleProgram(roleID);
                    }
                }

                this.FormState = eFormState.ReadOnly;

                this.dntRole.Position = this.positionIndex;

                this.btnAddNew.Visible = true;
                this.btnExit.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dntRole_PositionChanged(object sender, EventArgs e)
        {
            if (this.FormState != eFormState.ReadOnly) return;
            try
            {
                GridView view = (GridView)this.grdRole.Views[0]; //this.gridView2
                string roleID = view.GetFocusedRowCellValue("ROLE_ID").ToString();
                if (!string.IsNullOrEmpty(roleID))
                {
                    this.GetRoleProgram(roleID);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvRoleProgram_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                view.SetFocusedRowCellValue("FLAG", 2); //UPDATE
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvRole_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = (GridView)sender;

            view.SetFocusedRowCellValue("REC_STAT", true);
        }

        private void frmRole_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.grdRole);
            //base.SaveGridLayout(this.grdRoleProgram);

            this.Controls.Clear();
        }


    }
}