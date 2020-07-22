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
using System.Diagnostics;

namespace HTN.BITS.UIL.PLASESS.LOVForms
{
    public partial class frmLOVParty : BaseDialogForm
    {
        public frmLOVParty()
        {
            InitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdParty);
        }

        #region "Dialog Idle Time"

        ~frmLOVParty()
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

        private string _PARTY_TYPE;
        private string _PARTY_ID;
        private string _PARTY_NAME;

        #endregion

        #region "Property Member"
        public string PARTY_TYPE
        {
            get
            {
                return _PARTY_TYPE;
            }
            set
            {
                if (_PARTY_TYPE == value)
                    return;
                _PARTY_TYPE = value;
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
        public string PARTY_NAME
        {
            get
            {
                return _PARTY_NAME;
            }
            set
            {
                if (_PARTY_NAME == value)
                    return;
                _PARTY_NAME = value;
            }
        }

        #endregion


        #region "Method Member"

        public void GetPartyList()
        {
            List<Party> lstParty = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (PartyBLL partyBll = new PartyBLL())
                {
                    lstParty = partyBll.LovPratyList(this._PARTY_TYPE, string.Empty);
                }

                this.grdParty.DataSource = lstParty;
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

        public void GetPartyListSearchValue(string search)
        {
            List<Party> lstParty = null;
            try
            {
                using (PartyBLL partyBll = new PartyBLL())
                {
                    lstParty = partyBll.LovPratyList(this._PARTY_TYPE, search);
                }

                this.grdParty.DataSource = lstParty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        #endregion

        private void frmLOVParty_Load(object sender, EventArgs e)
        {
            //this.GetPartyList();
            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmLOVParty_LoadCompleted()
        {
            this.GetPartyList();

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

        private void grvParty_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);

                GridHitInfo info = view.CalcHitInfo(pt);
                if (info.InRow || info.InRowCell)
                {
                    this.PARTY_ID = view.GetRowCellValue(info.RowHandle, "PARTY_ID").ToString();
                    this.PARTY_NAME = view.GetRowCellValue(info.RowHandle, "PARTY_NAME").ToString();

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
            GridView view = (GridView)this.grdParty.Views[0]; //

            if (view.RowCount > 0)
            {
                this.PARTY_ID = view.GetRowCellValue(view.FocusedRowHandle, "PARTY_ID").ToString();
                this.PARTY_NAME = view.GetRowCellValue(view.FocusedRowHandle, "PARTY_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(this, "No Record Found Can't Select", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.txtSearch.Focus();
                this.txtSearch.SelectAll();
            }
        }

        private void grvParty_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.KeyCode == Keys.Enter)
            {
                this.PARTY_ID = view.GetRowCellValue(view.FocusedRowHandle, "PARTY_ID").ToString();
                this.PARTY_NAME = view.GetRowCellValue(view.FocusedRowHandle, "PARTY_NAME").ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            this.GetPartyListSearchValue(textEdit.Text);
        }

        private void frmLOVParty_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdParty.Views[0]);
            this.Controls.Clear();
        }



    }
}