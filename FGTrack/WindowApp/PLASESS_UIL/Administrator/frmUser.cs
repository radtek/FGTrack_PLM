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
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HTN.BITS.UIL.PLASESS.LOVForms;

namespace HTN.BITS.UIL.PLASESS.Administrator
{
    public partial class frmUser : BaseChildFormPopup
    {
        public frmUser()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdUserList);
            base.LoadGridLayout(this.grdRole);
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private List<User> lstUser = null;
        private DataTable dtbUserRole = null;
        private List<Role> delUserRole;

        private bool isChangePass = false;

        #endregion

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

        private List<User> UserList
        {
            get
            {
                return lstUser;
            }

            set
            {
                this.lstUser = value;
            }
        }

        #endregion

        #region "Method Member"

        private void ClearDataOnScreen()
        {
            try
            {
                this.isChangePass = false;

                this.txtUSER_ID.EditValue = null;
                this.txtUSER_NAME.EditValue = null;
                this.txtWARE_ID.EditValue = null;
                this.txtLOGIN.EditValue = null;
                this.txtPWD.EditValue = null;
                this.txtEMPLOYEE_ID.EditValue = null;
                this.lueDefaultRole.EditValue = null;

                this.txtREMARK.EditValue = null;
                this.txtEMAIL.EditValue = null;
                this.txtCOMP_ID.EditValue = null;
                this.icbREC_STAT.SelectedIndex = 0;

                this.GetUserRoleList(string.Empty);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void InitializaLOVData()
        {
            List<Role> lstRole;
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                lstRole = adminBll.GetRoleList();
            }

            //for storage type
            this.grvUserList_rps_ROLE_ID.DataSource = lstRole;
            this.lueDefaultRole.Properties.DataSource = lstRole;
        }

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                GridView view = (GridView)this.grdRole.Views[0];

                switch (fState)
                {
                    case eFormState.Add:

                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.xtpUserList.PageEnabled = false;

                        this.ChangeControlStatus(false);

                        UiUtility.SetGridEditOnly(view, true, 0);
                        view.Columns[2].OptionsColumn.AllowEdit = true;

                        this.dntUser.TextStringFormat = "      Add Mode      ";
                        this.dntUser.Enabled = false;
                        this.btnAssignUserRole.Enabled = true;
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;

                        this.btnCancel.Text = "Cancel";

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.xtpUserList.PageEnabled = false;


                        this.ChangeControlStatus(false);
                        //set for read only again
                        this.txtUSER_ID.Properties.ReadOnly = true;

                        UiUtility.SetGridEditOnly(view, true, 0);
                        view.Columns[2].OptionsColumn.AllowEdit = true;

                        this.dntUser.TextStringFormat = "      Edit Mode      ";
                        this.dntUser.Enabled = false;
                        this.btnAssignUserRole.Enabled = true;
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;

                        this.btnCancel.Text = "Cancel";

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlStatus(true);

                        view.Columns[2].OptionsColumn.AllowEdit = false;

                        UiUtility.SetGridReadOnly(view, true);

                        this.dntUser.TextStringFormat = " Record {0} of {1} ";
                        this.dntUser.Enabled = true;
                        this.btnChangePassword.Enabled = false;
                        this.btnAssignUserRole.Enabled = false;
                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;

                        this.btnCancel.Text = "Back";

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);

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

        private void ChangeControlStatus(bool status)
        {
            this.txtUSER_ID.Properties.ReadOnly = status;
            this.txtUSER_NAME.Properties.ReadOnly = status;
            this.txtWARE_ID.Properties.ReadOnly = status;
            this.txtLOGIN.Properties.ReadOnly = status;
            this.txtPWD.Properties.ReadOnly = status;
            this.txtEMPLOYEE_ID.Properties.ReadOnly = status;
            this.lueDefaultRole.Properties.ReadOnly = status;

            this.txtREMARK.Properties.ReadOnly = status;
            this.txtEMAIL.Properties.ReadOnly = status;
            this.txtCOMP_ID.Properties.ReadOnly = status;
            this.icbREC_STAT.Properties.ReadOnly = status;
        }

        private void GetUserList()
        {
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                this.UserList = adminBll.GetUserList(string.Empty);
            }

            if (this.UserList != null)
            {
                this.grdUserList.DataSource = this.UserList;
                this.dntUser.DataSource = this.UserList;
            }
        }

        private void GetUserRoleList(string userid)
        {
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                this.dtbUserRole = adminBll.GetUserRoleByUserID(userid);
            }

            if (this.dtbUserRole != null)
            {
                this.grdRole.DataSource = this.dtbUserRole;
            }
        }

        private void GetUserByUserID(string userid)
        {
            User user = null;
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                user = adminBll.GetUserByUserID(userid);
            }

            if (user != null)
            {
                this.txtUSER_ID.EditValue = user.USER_ID;
                this.txtUSER_NAME.EditValue = user.USER_NAME;
                this.txtWARE_ID.EditValue = user.WARE_ID;
                this.txtLOGIN.EditValue = user.LOGIN;
                this.txtPWD.EditValue = user.PWD;
                this.txtEMPLOYEE_ID.EditValue = user.EMPLOYEE_ID;
                this.lueDefaultRole.EditValue = user.ROLE_ID;

                this.txtREMARK.EditValue = user.REMARK;
                this.txtEMAIL.EditValue = user.EMAIL;
                this.txtCOMP_ID.EditValue = user.COMP_ID;
                this.icbREC_STAT.EditValue = user.REC_STAT;

                this.GetUserRoleList(user.USER_ID);

                GridView view = (GridView)this.grdRole.Views[0];
                UiUtility.RemoveActiveRow(view);
            }
        }

        private bool IsFormValidated()
        {
            //for check supplier code
            if (this.txtUSER_ID.Text.Trim().Length == 0)
            {
                //MessageBox.Show("USER ID CANNOT NULL", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this, "USER ID CANNOT NULL", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtUSER_ID.Focus();
                return false;
            }

            //for check invoice empty
            if (this.txtUSER_NAME.Text.Trim().Length == 0)
            {
                //MessageBox.Show("USER NAME CANNOT NULL.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this, "USER NAME CANNOT NULL.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtUSER_NAME.Focus();
                return false;
            }

            return true;
        }

        private bool IsDuplicated(GridView view, string roleID)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                if (view.GetRowCellDisplayText(i, "ROLE_ID").Equals(roleID))
                {
                    return true;
                }
            }

            return false;
        }

        private void DeleteSelectedRows(GridView view)
        {

            if (view == null || view.SelectedRowsCount == 0) return;

            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                rows[i] = view.GetDataRow(view.GetSelectedRows()[i]);
            }


            view.BeginSort();
            try
            {
                foreach (DataRow row in rows)
                {
                    if (row != null)
                    {
                        string roleID = row["ROLE_ID"].ToString();
                        if (!string.IsNullOrEmpty(roleID))
                        {
                            Role role = new Role();
                            role.ROLE_ID = roleID;
                            role.FLAG = 0; //delete

                            this.DeleteUserRole(role);
                        }

                        row.Delete();
                    }
                }
            }
            finally
            {
                view.EndSort();
                this.dtbUserRole.AcceptChanges();
            }
        }

        public void DeleteUserRole(Role role)
        {
            if (this.delUserRole == null)
            {
                this.delUserRole = new List<Role>();
            }

            this.delUserRole.Add(role);
        }

        private void InsertUser()
        {
            string result = string.Empty;
            try
            {
                User user = new User();

                user.USER_ID = (string)this.txtUSER_ID.EditValue;
                user.USER_NAME = (string)this.txtUSER_NAME.EditValue;
                user.WARE_ID = (string)this.txtWARE_ID.EditValue;
                user.LOGIN = (string)this.txtLOGIN.EditValue;
                user.PWD = (string)this.txtPWD.EditValue;
                user.EMPLOYEE_ID = (string)UiUtility.IsNullValue(this.txtEMPLOYEE_ID.EditValue, "");
                user.ROLE_ID = (string)this.lueDefaultRole.EditValue;

                user.REMARK = (string)UiUtility.IsNullValue(this.txtREMARK.EditValue, "");
                user.EMAIL = (string)UiUtility.IsNullValue(this.txtEMAIL.EditValue, "");
                user.COMP_ID = (string)UiUtility.IsNullValue(this.txtCOMP_ID.EditValue, "");
                user.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.InsertUser(user, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    this.UpdateUserRole();
                    NotifierResult.Show("Insert Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }
            }
            catch (Exception ex)
            {
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.GetUserList();

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdUserList.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "USER_ID", this.txtUSER_ID.Text);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntUser.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }


        }

        private void UpdateUser()
        {
            string result = string.Empty;

            try
            {

                User user = new User();

                user.USER_ID = (string)this.txtUSER_ID.EditValue;
                user.USER_NAME = (string)this.txtUSER_NAME.EditValue;
                user.WARE_ID = (string)this.txtWARE_ID.EditValue;
                user.LOGIN = (string)this.txtLOGIN.EditValue;

                user.PWD = (string)this.txtPWD.EditValue;

                user.EMPLOYEE_ID = (string)UiUtility.IsNullValue(this.txtEMPLOYEE_ID.EditValue, "");
                user.ROLE_ID = (string)this.lueDefaultRole.EditValue;

                user.REMARK = (string)UiUtility.IsNullValue(this.txtREMARK.EditValue, "");
                user.EMAIL = (string)UiUtility.IsNullValue(this.txtEMAIL.EditValue, "");
                user.COMP_ID = (string)UiUtility.IsNullValue(this.txtCOMP_ID.EditValue, "");
                user.REC_STAT = (bool)this.icbREC_STAT.EditValue;


                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UpdateUser(user, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    this.UpdateUserRole();

                    GridView view = (GridView)this.grdUserList.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("USER_ID", user.USER_ID);
                    view.SetFocusedRowCellValue("USER_NAME", user.USER_NAME);
                    view.SetFocusedRowCellValue("WARE_ID", user.WARE_ID);
                    view.SetFocusedRowCellValue("EMPLOYEE_ID", user.EMPLOYEE_ID);
                    view.SetFocusedRowCellValue("ROLE_ID", user.ROLE_ID);
                    view.SetFocusedRowCellValue("EMAIL", user.EMAIL);
                    view.SetFocusedRowCellValue("REC_STAT", user.REC_STAT);

                    view.EndDataUpdate();

                    NotifierResult.Show("Update Complete", "Result", 50, 1000, 50, NotifyType.Safe);
                }
                else
                {
                    NotifierResult.Show(result, "Error", 100, 1000, 0, NotifyType.Warning);
                }


            }
            catch (Exception ex)
            {
                NotifierResult.Show(ex.Message, "Error", 50, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
            }
        }

        private void UpdateUserRole()
        {
            if (this.dtbUserRole.Rows.Count == 0) return;
            string result = string.Empty;
            List<Role> lstRole = null;
            try
            {
                lstRole = new List<Role>(this.dtbUserRole.Rows.Count);

                //Check Delete Recore
                if (this.delUserRole != null)
                {
                    foreach (Role roleDel in this.delUserRole)
                    {
                        lstRole.Add(roleDel);
                    }
                }

                Role role;
                foreach (DataRow dr in this.dtbUserRole.Rows)
                {
                    role = new Role();

                    role.ROLE_ID = dr["ROLE_ID"].ToString();
                    //REC_STAT for Status of Role Program
                    role.REC_STAT = (bool)dr["REC_STAT"];
                    role.FLAG = Convert.ToInt32(dr["FLAG"].ToString());

                    lstRole.Add(role);
                }

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UpdateUserRole(this.txtUSER_ID.Text, lstRole, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (!result.Equals("OK"))
                {
                    XtraMessageBox.Show(this, result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
            }


        }

        #endregion

        private void frmUser_Load(object sender, EventArgs e)
        {
            //this.InitializaLOVData();

            //this.GetUserList();

            //this.xtpUserDetail.PageVisible = false;
            //this.xtcUser.SelectedTabPage = this.xtpUserList;

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmUser_LoadCompleted()
        {
            this.InitializaLOVData();

            this.GetUserList();

            this.xtpUserDetail.PageVisible = false;
            this.xtcUser.SelectedTabPage = this.xtpUserList;

            this.FormState = eFormState.ReadOnly;
        }

        private void grvUserList_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string userid = view.GetRowCellValue(info.RowHandle, "USER_ID").ToString();

                this.btnAddNew.Visible = false;
                this.btnExit.Visible = false;

                //Change tab view.
                this.xtpUserList.PageEnabled = false;

                this.xtpUserDetail.PageVisible = true;
                this.xtcUser.SelectedTabPage = this.xtpUserDetail;



                //Call record detail.
                this.GetUserByUserID(userid);

                this.btnCancel.Text = "Back";
            }
        }

        private void dntUser_PositionChanged(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdUserList.Views[0]; //this.gridView2

            if (this.xtcUser.SelectedTabPage == this.xtpUserDetail)
            {
                string userid = view.GetFocusedRowCellValue("USER_ID").ToString();

                this.GetUserByUserID(userid);
            }
            else
            {
                UiUtility.SetGridFocused(view, DrawFocusRectStyle.RowFocus, true);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Add;
            this.ClearDataOnScreen();

            this.btnChangePassword.Enabled = false;

            this.txtWARE_ID.Text = "FG"; //set default warehouse
            this.xtpUserDetail.PageVisible = true;
            this.xtcUser.SelectedTabPage = this.xtpUserDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.txtUSER_ID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Edit;

            this.btnChangePassword.Enabled = true;

            this.txtPWD.Properties.ReadOnly = true;
            this.isChangePass = false;

            this.txtUSER_NAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:
                        this.xtpUserDetail.PageVisible = false;
                        this.xtpUserList.PageEnabled = true;
                        this.xtcUser.SelectedTabPage = this.xtpUserList;
                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;
                        break;
                    case eFormState.Edit:

                        if (this.delUserRole != null)
                        {
                            this.delUserRole.Clear();
                            this.delUserRole = null;
                        }

                        this.GetUserByUserID(this.txtUSER_ID.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpUserDetail.PageVisible = false;
                        this.xtpUserList.PageEnabled = true;
                        this.xtcUser.SelectedTabPage = this.xtpUserList;
                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;
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
                this.FormState = eFormState.ReadOnly;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsFormValidated()) return;


            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertUser();
                    break;
                case eFormState.Edit:
                    this.UpdateUser();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }

            if (this.delUserRole != null)
            {
                this.delUserRole.Clear();
                this.delUserRole = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAssignUserRole_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdRole.Views[0];
            //Open Popup For Select Program.
            DialogResult dialogResult;
            int resultCount = 0;
            List<Role> lstRole = null;

            using (frmLOVRole fRoleList = new frmLOVRole())
            {
                dialogResult = UiUtility.ShowPopupForm(fRoleList, this, true);
                resultCount = fRoleList.ResultList.Count;
                lstRole = fRoleList.ResultList;
            }

            if (dialogResult == DialogResult.OK)
            {
                if (resultCount > 0)
                {
                    view.CancelUpdateCurrentRow();

                    foreach (Role roleSelect in lstRole)
                    {
                        if (!this.IsDuplicated(view, roleSelect.ROLE_ID))
                        {
                            view.AddNewRow();

                            view.SetFocusedRowCellValue("ROLE_ID", roleSelect.ROLE_ID);
                            view.SetFocusedRowCellValue("ROLE_NAME", roleSelect.ROLE_NAME);
                            view.SetFocusedRowCellValue("REC_STAT", true); //default
                            //for hidden field 2=NEW ROW
                            view.SetFocusedRowCellValue("FLAG", 2);

                            view.UpdateCurrentRow();
                        }
                    }
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            this.txtPWD.Properties.ReadOnly = false;
            this.isChangePass = true;

            this.txtPWD.EditValue = null;
            this.txtPWD.Focus();
        }

        private void grvRole_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.FormState == eFormState.ReadOnly) return;

            GridView view = sender as GridView;

            if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

            if (e.KeyCode == Keys.Delete)
            {
                //DialogResult isDelete = MessageBox.Show("Do you want to delete this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult isDelete = XtraMessageBox.Show(this, "Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (isDelete == DialogResult.Yes)
                {
                    this.DeleteSelectedRows(view);
                }
            }
        }

        private void frmUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdUserList.Views[0]);
            //base.SaveGridLayout(this.Name, this.grdRole.Views[0]);

            this.Controls.Clear();
        }

        private void grvRole_rps_chkREC_STAT_CheckedChanged(object sender, EventArgs e)
        {
            GridView view = this.grdRole.FocusedView as GridView;

            CheckEdit editor = view.ActiveEditor as CheckEdit;

            if ((int)view.GetFocusedRowCellValue("FLAG") != 2)
                view.SetFocusedRowCellValue("FLAG", 3); //UPDATE

            view.SetFocusedValue(editor.Checked);
            view.PostEditor();
        }


    }
}