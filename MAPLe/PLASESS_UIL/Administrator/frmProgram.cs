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
using HTN.BITS.LIB;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HTN.BITS.UIL.PLASESS.Administrator
{
    public partial class frmProgram : BaseChildFormPopup
    {
        public frmProgram()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProgramList);
        }

        #region "Variable Member"

        private eFormState formState = eFormState.ReadOnly;
        private List<M_Program> lstProgram = null;

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

        private List<M_Program> ProgramList
        {
            get
            {
                return lstProgram;
            }

            set
            {
                this.lstProgram = value;
            }
        }

        #endregion

        #region "Method Member"

        private void ClearDataOnScreen()
        {
            try
            {
                //Program 
                this.txtPROG_ID.EditValue = null;
                this.txtPROG_NAME.EditValue = null;
                this.txtPROG_KEY.EditValue = null;
                this.luePROG_TYPE.EditValue = null;
                this.txtORDER_BY.EditValue = null;
                this.txtICON.EditValue = null;
                this.txtDESCRIPTION.EditValue = null;
                this.icbREC_STAT.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void InitializaLOVData()
        {
            List<ProgramType> lstProgramType;
            using (AdministratorBLL adminBll = new AdministratorBLL())
            {
                lstProgramType = adminBll.GetProgramTypeList(string.Empty);
            }

            this.grvProgramList_rps_PROG_TYPE.DataSource = lstProgramType;
            this.luePROG_TYPE.Properties.DataSource = lstProgramType;
            this.lueProgramType.Properties.DataSource = lstProgramType;
        }

        private void ChangeFormState(eFormState fState)
        {
            try
            {
                switch (fState)
                {
                    case eFormState.Add:



                        //Lock Main menu
                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.xtpProgramList.PageEnabled = false;

                        this.ChangeControlStatus(false);
                        this.dntProgram.TextStringFormat = "      Add Mode      ";
                        this.dntProgram.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;

                        this.btnCancel.Text = "Cancel";

                        break;
                    case eFormState.Edit:

                        ((frmMainMenu)this.ParentForm).LockMenu = true;

                        this.xtpProgramList.PageEnabled = false;


                        this.ChangeControlStatus(false);
                        //set for read only again
                        this.txtPROG_ID.Properties.ReadOnly = true;

                        this.dntProgram.TextStringFormat = "      Edit Mode      ";
                        this.dntProgram.Enabled = false;

                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = true;

                        this.btnCancel.Text = "Cancel";

                        break;
                    case eFormState.ReadOnly:

                        ((frmMainMenu)this.ParentForm).LockMenu = false;

                        this.ChangeControlStatus(true);
                        this.dntProgram.TextStringFormat = " Record {0} of {1} ";
                        this.dntProgram.Enabled = true;

                        this.btnEdit.Enabled = true; ;
                        this.btnSave.Enabled = false;

                        this.btnCancel.Text = "Back";

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
            try
            {
                this.txtPROG_ID.Properties.ReadOnly = status;
                this.txtPROG_NAME.Properties.ReadOnly = status;
                this.txtPROG_KEY.Properties.ReadOnly = status;
                this.luePROG_TYPE.Properties.ReadOnly = status;
                this.txtORDER_BY.Properties.ReadOnly = status;
                this.txtICON.Properties.ReadOnly = status;
                this.txtDESCRIPTION.Properties.ReadOnly = status;
                this.icbREC_STAT.Properties.ReadOnly = status;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetProgramList(string procType, string procID)
        {
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    this.ProgramList = adminBll.GetProgramList(procType, procID);
                }

                if (this.ProgramList != null)
                {
                    this.grdProgramList.DataSource = this.ProgramList;
                    this.dntProgram.DataSource = this.ProgramList;
                }
                else
                {
                    this.grdProgramList.DataSource = null;
                    this.dntProgram.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetProgramByID(string procID)
        {
            M_Program program = null;
            try
            {
                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    program = adminBll.GetProgramByID(procID);
                }

                if (program != null)
                {
                    this.txtPROG_ID.EditValue = program.PROG_ID;
                    this.txtPROG_NAME.EditValue = program.PROG_NAME;
                    this.txtPROG_KEY.EditValue = program.PROG_KEY;
                    this.luePROG_TYPE.EditValue = program.PROG_TYPE;
                    this.txtORDER_BY.EditValue = program.ORDER_BY;
                    this.txtICON.EditValue = program.ICON;
                    this.txtDESCRIPTION.EditValue = program.DESCRIPTION;
                    this.icbREC_STAT.EditValue = program.REC_STAT;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool IsFormValidated()
        {
            //for check supplier code
            if (this.txtPROG_ID.Text.Trim().Length == 0)
            {
                //MessageBox.Show("PROGRAM ID CANNOT NULL", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this, "PROGRAM ID CANNOT NULL", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtPROG_ID.Focus();
                return false;
            }

            //for check invoice empty
            if (this.txtPROG_NAME.Text.Trim().Length == 0)
            {
                //MessageBox.Show("PROGRAM NAME CANNOT NULL.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this, "PROGRAM NAME CANNOT NULL.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtPROG_NAME.Focus();
                return false;
            }

            return true;
        }

        private void InsertProgram()
        {
            string result = string.Empty;
            try
            {
                M_Program program = new M_Program();

                program.PROG_ID = (string)this.txtPROG_ID.EditValue;
                program.PROG_NAME = (string)UiUtility.IsNullValue(this.txtPROG_NAME.EditValue, "");
                program.PROG_KEY = (string)UiUtility.IsNullValue(this.txtPROG_KEY.EditValue, "");
                program.PROG_TYPE = (string)this.luePROG_TYPE.EditValue;
                if (this.txtORDER_BY.EditValue != null)
                {
                    program.ORDER_BY = (int)this.txtORDER_BY.EditValue;
                }
                program.ICON = (string)UiUtility.IsNullValue(this.txtICON.EditValue, "");
                program.DESCRIPTION = (string)UiUtility.IsNullValue(this.txtDESCRIPTION.EditValue, "");
                program.REC_STAT = (bool)this.icbREC_STAT.EditValue;

                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.InsertProgram(program, ((frmMainMenu)this.ParentForm).UserID);
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
                this.lueProgramType.EditValue = this.luePROG_TYPE.EditValue;

                this.GetProgramList(this.lueProgramType.EditValue.ToString(), string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdProgramList.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PROG_ID", this.txtPROG_ID.Text);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntProgram.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }
                }
            }


        }

        private void UpdateProgram()
        {
            string result = string.Empty;

            try
            {

                M_Program program = new M_Program();

                program.PROG_ID = (string)this.txtPROG_ID.EditValue;
                program.PROG_NAME = (string)UiUtility.IsNullValue(this.txtPROG_NAME.EditValue, "");
                program.PROG_KEY = (string)UiUtility.IsNullValue(this.txtPROG_KEY.EditValue, "");
                program.PROG_TYPE = (string)this.luePROG_TYPE.EditValue;
                if (this.txtORDER_BY.EditValue != null)
                {
                    program.ORDER_BY = (int)this.txtORDER_BY.EditValue;
                }

                program.ICON = (string)UiUtility.IsNullValue(this.txtICON.EditValue, "");
                program.DESCRIPTION = (string)UiUtility.IsNullValue(this.txtDESCRIPTION.EditValue, "");
                program.REC_STAT = (bool)this.icbREC_STAT.EditValue;



                using (AdministratorBLL adminBll = new AdministratorBLL())
                {
                    result = adminBll.UpdateProgram(program, ((frmMainMenu)this.ParentForm).UserID);
                }

                if (result.Equals("OK"))
                {
                    GridView view = (GridView)this.grdProgramList.Views[0];
                    view.BeginDataUpdate();

                    view.SetFocusedRowCellValue("PROG_ID", program.PROG_ID);
                    view.SetFocusedRowCellValue("PROG_NAME", program.PROG_NAME);
                    view.SetFocusedRowCellValue("PROG_TYPE", program.PROG_TYPE);
                    view.SetFocusedRowCellValue("ORDER_BY", program.ORDER_BY);
                    view.SetFocusedRowCellValue("REC_STAT", program.REC_STAT);

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
                //Get all Invoice on Invoice List
                this.lueProgramType.EditValue = this.luePROG_TYPE.EditValue;

                this.GetProgramList(this.lueProgramType.EditValue.ToString(), string.Empty);

                if (result.Equals("OK"))
                {
                    GridView viewList = (GridView)this.grdProgramList.Views[0];
                    viewList.ClearSorting();
                    int position = UiUtility.GetRowHandleByColumnValue(viewList, "PROG_ID", this.txtPROG_ID.Text);
                    if (position != 0)
                    {
                        if (position != GridControl.InvalidRowHandle)
                        {
                            this.dntProgram.Position = position;
                        }
                    }
                    else
                    {
                        viewList.FocusedRowHandle = 0;
                    }

                    //Reload Menu
                    ((frmMainMenu)this.ParentForm).ReloadMenu();

                }
            }
        }

        #endregion

        private void frmProgram_Load(object sender, EventArgs e)
        {
            //this.InitializaLOVData();

            ////this.GetProgramList(string.Empty, string.Empty);

            //this.xtpProgramDetail.PageVisible = false;
            //this.xtcProgram.SelectedTabPage = this.xtpProgramList;

            //this.FormState = eFormState.ReadOnly;
        }

        private void frmProgram_LoadCompleted()
        {
            this.InitializaLOVData();

            this.xtpProgramDetail.PageVisible = false;
            this.xtcProgram.SelectedTabPage = this.xtpProgramList;

            this.FormState = eFormState.ReadOnly;
        }

        private void grvProgramList_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string procID = view.GetRowCellValue(info.RowHandle, "PROG_ID").ToString();

                this.btnAddNew.Visible = false;
                this.btnExit.Visible = false;

                //Change tab view.
                this.xtpProgramList.PageEnabled = false;

                this.xtpProgramDetail.PageVisible = true;
                this.xtcProgram.SelectedTabPage = this.xtpProgramDetail;



                //Call record detail.
                this.GetProgramByID(procID);

                this.btnCancel.Text = "Back";
            }
        }

        private void dntProgram_PositionChanged(object sender, EventArgs e)
        {
            GridView view = (GridView)this.grdProgramList.Views[0]; //this.gridView2

            if (this.xtcProgram.SelectedTabPage == this.xtpProgramDetail)
            {
                string procID = view.GetFocusedRowCellValue("PROG_ID").ToString();

                this.GetProgramByID(procID);
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

            this.xtpProgramDetail.PageVisible = true;
            this.xtcProgram.SelectedTabPage = this.xtpProgramDetail;

            this.btnAddNew.Visible = false;
            this.btnExit.Visible = false;

            //check default Program Type
            this.luePROG_TYPE.EditValue = this.lueProgramType.EditValue;

            this.txtPROG_ID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.FormState = eFormState.Edit;
            this.txtPROG_NAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.FormState)
                {
                    case eFormState.Add:
                        this.xtpProgramDetail.PageVisible = false;
                        this.xtpProgramList.PageEnabled = true;
                        this.xtcProgram.SelectedTabPage = this.xtpProgramList;
                        this.btnAddNew.Visible = true;
                        this.btnExit.Visible = true;
                        break;
                    case eFormState.Edit:

                        this.GetProgramByID(this.txtPROG_ID.Text);

                        break;
                    case eFormState.ReadOnly:
                        this.xtpProgramDetail.PageVisible = false;
                        this.xtpProgramList.PageEnabled = true;
                        this.xtcProgram.SelectedTabPage = this.xtpProgramList;
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
                    this.InsertProgram();
                    break;
                case eFormState.Edit:
                    this.UpdateProgram();
                    break;
                case eFormState.ReadOnly:
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueProgramType_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (LookUpEdit)sender;

            if (editor.EditValue != null)
            {
                this.GetProgramList((string)editor.EditValue, string.Empty);
            }
        }

        private void frmProgram_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProgramList.Views[0]);
            this.Controls.Clear();
        }




    }
}