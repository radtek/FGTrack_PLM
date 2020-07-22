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
using HTN.BITS.LIB;
using DevExpress.XtraGrid;
using HTN.BITS.BEL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.Administrator
{
    public partial class frmProgramType : BaseChildFormPopup
    {
        public frmProgramType()
        {
            InitializeComponent();
            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProgramType);
        }

        #region "Variable Member"

        private DataTable dtbProgramType = null;

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
                GridView view = (GridView)this.grdProgramType.Views[0];

                switch (fState)
                {
                    case eFormState.Add:
                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        //set readonly status for grdInvoiceDetail

                        view.OptionsBehavior.Editable = true;
                        view.Columns[0].OptionsColumn.ReadOnly = false;
                        view.Columns[0].OptionsColumn.AllowEdit = true;
                        view.Columns[0].OptionsColumn.AllowFocus = true;

                        this.dntProgramType.TextStringFormat = "      Add Mode      ";
                        this.dntProgramType.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        UiUtility.SetGridEditOnly(view, true, 0);

                        view.Columns[1].OptionsColumn.AllowEdit = true;
                        view.Columns[1].OptionsColumn.AllowFocus = true;

                        this.dntProgramType.TextStringFormat = "      Edit Mode      ";
                        this.dntProgramType.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.btnCancel.Enabled = true;

                        UiUtility.SetGridFocused(view, DrawFocusRectStyle.CellFocus, true);

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        //set readonly status for grdInvoiceDetail;
                        UiUtility.SetGridReadOnly(view, true);

                        this.dntProgramType.TextStringFormat = " Record {0} of {1} ";
                        this.dntProgramType.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;
                        this.btnCancel.Enabled = false;

                        view.OptionsBehavior.Editable = false;
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

        private void GetProgramTypeList()
        {
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                this.dtbProgramType = adminBll.GetProgramType(string.Empty);
            }

            if (this.dtbProgramType != null)
            {
                this.grdProgramType.DataSource = this.dtbProgramType;
                this.dntProgramType.DataSource = this.dtbProgramType;
            }
        }

        private void GetEditProgramType(string procTypeID)
        {
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                this.dtbProgramType = adminBll.GetProgramType(procTypeID);
            }

            if (this.dtbProgramType != null)
            {
                this.grdProgramType.DataSource = this.dtbProgramType;
                this.dntProgramType.DataSource = this.dtbProgramType;
            }
        }

        private void InsertProgramType()
        {
            string result = string.Empty;
            ProgramType programType = null;
            try
            {
                DataRow rowProgramType = this.dtbProgramType.Rows[0];
                if (rowProgramType != null)
                {
                    programType = new ProgramType();

                    programType.PROG_TYPE = rowProgramType["PROG_TYPE"].ToString().ToUpper();
                    programType.PROG_TYPE_NAME = rowProgramType["PROG_TYPE_NAME"].ToString();
                    programType.REC_STAT = (bool)rowProgramType["REC_STAT"];
                    programType.ORDER_BY = Convert.ToInt32(rowProgramType["ORDER_BY"].ToString());
                    programType.ICON = rowProgramType["ICON"].ToString();
                }

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.InsertProgramType(programType, ((frmMainMenu)this.ParentForm).UserID);
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
                NotifierResult.Show(ex.Message, "Error", 100, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.FormState = eFormState.ReadOnly;
                //Get all Invoice on Invoice List
                this.GetProgramTypeList();

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdProgramType.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PROG_TYPE", programType.PROG_TYPE);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntProgramType.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }
            }
        }

        private void UpdateProgramType()
        {
            ProgramType programType = null;
            string result = string.Empty;

            try
            {
                DataRow rowProgramType = this.dtbProgramType.Rows[0];
                if (rowProgramType != null)
                {
                    programType = new ProgramType();

                    programType.PROG_TYPE = rowProgramType["PROG_TYPE"].ToString().ToUpper();
                    programType.PROG_TYPE_NAME = rowProgramType["PROG_TYPE_NAME"].ToString();
                    programType.REC_STAT = (bool)rowProgramType["REC_STAT"];

                    if (!rowProgramType["ORDER_BY"].GetType().Equals(typeof(System.DBNull)))
                    {
                        programType.ORDER_BY = Convert.ToInt32(rowProgramType["ORDER_BY"].ToString());
                    }
                    programType.ICON = rowProgramType["ICON"].ToString();
                }

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UpdateProgramType(programType, ((frmMainMenu)this.ParentForm).UserID);
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
                NotifierResult.Show(ex.Message, "Error", 50, 1000, 0, NotifyType.Warning);
            }
            finally
            {
                this.GetProgramTypeList();

                this.FormState = eFormState.ReadOnly;

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdProgramType.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PROG_TYPE", programType.PROG_TYPE);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntProgramType.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                }

                this.btnAddNew.Visible = true;
                this.btnExit.Visible = true;
            }
        }

        #endregion

        private void frmProgramType_Load(object sender, EventArgs e)
        {
            //this.GetProgramTypeList();

            //this.positionIndex = this.dntProgramType.Position;

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmProgramType_LoadCompleted()
        {
            this.GetProgramTypeList();

            this.positionIndex = this.dntProgramType.Position;

            this.FormState = eFormState.ReadOnly;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.positionIndex = this.dntProgramType.Position;

            this.FormState = eFormState.Add;

            GridView view = (GridView)this.grdProgramType.Views[0];

            //Machine 
            this.dtbProgramType.Rows.Clear();
            this.dtbProgramType.AcceptChanges();

            this.grdProgramType.DataSource = this.dtbProgramType;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            view.AddNewRow();
            view.FocusedRowHandle = GridControl.NewItemRowHandle;

            view.FocusedColumn = view.Columns[0];
            view.ShowEditor();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.positionIndex = this.dntProgramType.Position;

            this.FormState = eFormState.Edit;

            GridView view = (GridView)this.grdProgramType.Views[0];
            string procType = view.GetRowCellValue(view.FocusedRowHandle, "PROG_TYPE").ToString();

            this.GetEditProgramType(procType);

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            this.grdProgramType.Focus();
            view.FocusedColumn = view.Columns[1];
            view.ShowEditor();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (!this.IsFormValidated()) return;
            switch (this.FormState)
            {
                case eFormState.Add:
                    this.InsertProgramType();
                    break;
                case eFormState.Edit:
                    this.UpdateProgramType();
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
            try
            {
                GridView view = (GridView)this.grdProgramType.Views[0];
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
                this.GetProgramTypeList();

                this.FormState = eFormState.ReadOnly;

                this.dntProgramType.Position = this.positionIndex;

                this.btnAddNew.Visible = true;
                this.btnExit.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProgramType_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProgramType.Views[0]);
            this.Controls.Clear();
        }
    }
}