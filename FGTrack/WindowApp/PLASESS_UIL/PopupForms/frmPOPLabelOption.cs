using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraGrid;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPOPLabelOption : BaseDialogForm
    {
        public frmPOPLabelOption()
        {
            InitializeComponent();

            //this.CustomInitializeComponent();

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdLabelOption);

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            this.KeyPreview = true;

        }

        ~frmPOPLabelOption()
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

        private string _ARRIVAL_NO;
        private int _LINE_NO;
        private Material _ITEM;
        private decimal _TOTAL_INNER_QTY;

        private int _No_Of_Label;

        private bool isLabelGenerated;

        private string _USER_ID;

        List<Material> lstItem;

        private DataTable dtbLabelOptionList ;
        #endregion "Variable Member"

        #region "Property Member"

        public string ARRIVAL_NO
        {
            get
            {
                return _ARRIVAL_NO;
            }
            set
            {
                if (_ARRIVAL_NO == value)
                    return;
                _ARRIVAL_NO = value;
            }
        }

        public int LINE_NO
        {
            get
            {
                return _LINE_NO;
            }
            set
            {
                if (_LINE_NO == value)
                    return;
                _LINE_NO = value;
            }
        }

        public Material ITEM
        {
            get
            {
                return _ITEM;
            }
            set
            {
                if (_ITEM == value)
                    return;
                _ITEM = value;
            }
        }

        public decimal TOTAL_INNER_QTY
        {
            get
            {
                return _TOTAL_INNER_QTY;
            }
            set
            {
                if (_TOTAL_INNER_QTY == value)
                    return;
                _TOTAL_INNER_QTY = value;
            }
        }

        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }

        public bool IsLabelGenerated
        {
            get
            {
                return isLabelGenerated;
            }
            set
            {
                if (isLabelGenerated == value)
                    return;
                isLabelGenerated = value;
            }
        }

        public int No_Of_Label
        {
            get
            {
                return _No_Of_Label;
            }
        }

        #endregion "Property Member"

        #region "Method Member"

        private void CustomInitializeComponent()
        {
            this.btnSave.Image = base.Language.GetBitmap("ButtonSave");
            this.btnCancel.Image = base.Language.GetBitmap("ButtonCancel");
        }

        private void InitializaLOVData()
        {
            

            List<Unit> lstUnit;

            using (MaterialBLL mtlBll = new MaterialBLL())
            {
                lstUnit = mtlBll.GetUnitList();
            }

            if (lstUnit != null)
            {
            }
        }

        private void BingingItem()
        {
           if (this.ITEM == null)
                return;

            this.txtITEM_CODE.EditValue = this.ITEM.MTL_CODE;
            this.txtITEM_NAME.EditValue = this.ITEM.MTL_NAME;
            this.txtITEM_PACKAGE_QTY.EditValue = this.ITEM.STD_QTY;
            this.lblITEM_UNIT.Text = this.ITEM.UNIT;
            this.lblITEM_UNIT2.Text = this.ITEM.UNIT;
            //this.lblITEM_PACKAGE.Text = this.ITEM.ITEM_PACKAGE;
            //this.lblITEM_UNIT2.Text = this.ITEM.ITEM_UNIT;
            this.txtTOTAL_OUTER_QTY.EditValue = this.TOTAL_INNER_QTY;

        }


        private void GetLabelOptionList(string arrNo, int lineNo)
        {
            
            
            try
            {
                using (ArrivalBLL arrivalBLL = new ArrivalBLL())
                {
                    List<T_ARRIVAL_DTL_SUB> lstLabel = arrivalBLL.GetLabelList(arrNo,lineNo);

                    //this.grdUNIT.DataSource = lstUnit;
                    if (lstLabel == null)
                        lstLabel = new List<T_ARRIVAL_DTL_SUB>();
                    //DataTable table = ConvertToDataTable(lstUnit);
                    //this.grdUNIT.DataSource = table;
                    this.dtbLabelOptionList = UiUtility.ConvertToDataTable(lstLabel);
                    
                    if (this.dtbLabelOptionList != null)
                    {
                        this.grdLabelOption.DataSource = this.dtbLabelOptionList;
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }            
          
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
                        row.Delete();
                    }
                }
            }
            finally
            {
                view.EndSort();
                this.dtbLabelOptionList.AcceptChanges();

                this.UpdateBalance(view);
            }
        }

        private void InsertLabelOption()
        {
            
            try
            {

            string result = string.Empty;
            GridView view = (GridView)this.grdLabelOption.Views[0];

            var lstLabel = UiUtility.ConvertToList<T_ARRIVAL_DTL_SUB>(this.dtbLabelOptionList);


            using (ArrivalBLL arrBll = new ArrivalBLL())
            {
                result = arrBll.InsertLabel(this.ARRIVAL_NO, this.LINE_NO, lstLabel, this._USER_ID);
            }

            if (result == "OK")
            {
                this.DialogResult = DialogResult.OK;
            }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UpdateBalance(GridView view)
        {
            decimal totalQty = Convert.ToDecimal(this.txtTOTAL_OUTER_QTY.EditValue, NumberFormatInfo.InvariantInfo);
            decimal arrDocQty = Convert.ToDecimal(view.Columns["QTY"].SummaryItem.SummaryValue, NumberFormatInfo.InvariantInfo);
            this._No_Of_Label = Convert.ToInt32(view.Columns["DOC_PKG_QTY"].SummaryItem.SummaryValue, NumberFormatInfo.InvariantInfo);

            this.txtBalance.EditValue = totalQty - arrDocQty;
        }

        public static object IsNullValue(object expression, object resultIsNull)
        {
            try
            {
                if (expression.Equals(System.DBNull.Value))
                    return resultIsNull;
                else
                    return expression;
            }
            catch (System.NullReferenceException e)
            {
                return resultIsNull;
            }
        }


        #endregion

        private void frmPOPLabelOption_Load(object sender, EventArgs e)
        {

            try
            {

                this.InitializaLOVData();

                this.BingingItem();


                this.txtTOTAL_OUTER_QTY.EditValue = this.TOTAL_INNER_QTY;

                this.GetLabelOptionList(this.ARRIVAL_NO, this.LINE_NO);

                this.btnSave.Enabled = !this.IsLabelGenerated;

                //set grid to read only
                GridView view = (GridView)this.grdLabelOption.Views[0];
                UiUtility.SetGridReadOnly(view, this.IsLabelGenerated);


                if (!base.DialogIdle.IsRunning)
                {
                    base.DialogIdle.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void frmPOPLabelOption_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int balance = Convert.ToInt32(this.txtBalance.EditValue, NumberFormatInfo.InvariantInfo);
            if (balance == 0)
            {
                this.InsertLabelOption();
            }
            else
            {
                XtraMessageBox.Show(this, "CAN NOT SAVE DATA!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                this.btnCancel.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rps_txt_DOC_PKG_QTY_Validating(object sender, CancelEventArgs e)
        {
            TextEdit editor = (TextEdit)sender;
            GridView view = (GridView)this.grdLabelOption.Views[0];

            if (editor.Text == string.Empty || Convert.ToDecimal(editor.Text) <= 0)
            {
                e.Cancel = true;
            }
            else
            {
                object docInnerQty = view.GetRowCellValue(view.FocusedRowHandle, "STD_QTY");
                if (IsNullValue(docInnerQty, null) != null)
                {
                    decimal totalQty = Convert.ToDecimal(editor.EditValue, NumberFormatInfo.InvariantInfo) *
                                      Convert.ToDecimal(docInnerQty, NumberFormatInfo.InvariantInfo);
                    view.SetFocusedRowCellValue("QTY", totalQty);

                    view.UpdateTotalSummary();
                    this.UpdateBalance(view);
                }
            }
        }

        private void rps_txt_DOC_PKG_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit editor = (TextEdit)sender;
            if (e.KeyCode == Keys.Enter)
            {
                GridView view = (GridView)this.grdLabelOption.Views[0];
                if (view.FocusedRowHandle != GridControl.NewItemRowHandle)
                {
                    editor.SendKey(new KeyEventArgs(Keys.Tab));
                }
            }
        }

        private void rps_txt_DOC_INNER_QTY_Validating(object sender, CancelEventArgs e)
        {
            TextEdit editor = (TextEdit)sender;
            GridView view = (GridView)this.grdLabelOption.Views[0];

            if (editor.Text == string.Empty || Convert.ToDecimal(editor.Text) <= 0)
            {
                e.Cancel = true;
            }
            else
            {
                object noOfPlt = view.GetRowCellValue(view.FocusedRowHandle, "DOC_PKG_QTY");
                if (IsNullValue(noOfPlt, null) != null)
                {
                    decimal totalQty = Convert.ToDecimal(editor.EditValue, NumberFormatInfo.InvariantInfo) *
                                      Convert.ToDecimal(noOfPlt, NumberFormatInfo.InvariantInfo);

                    view.SetFocusedRowCellValue("QTY", totalQty);

                    view.UpdateTotalSummary();
                    this.UpdateBalance(view);
                }
            }
        }

        private void rps_txt_DOC_INNER_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit editor = (TextEdit)sender;
            if (e.KeyCode == Keys.Enter)
            {
                GridView view = (GridView)this.grdLabelOption.Views[0];
                if (view.FocusedRowHandle != GridControl.NewItemRowHandle)
                {
                    editor.SendKey(new KeyEventArgs(Keys.Tab));
                }
            }
        }

        private void grvLabelOption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (view.FocusedRowHandle == GridControl.NewItemRowHandle) return;

                this.DeleteSelectedRows(view);
            }
        }

        private void grvLabelOption_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.PrevFocusedColumn != null)
            {
                GridView view = (GridView)sender;
                switch (e.PrevFocusedColumn.FieldName)
                {
                    case "STD_QTY":
                        string strValue = view.GetFocusedRowCellDisplayText(e.PrevFocusedColumn);
                        if (string.IsNullOrEmpty(strValue))
                        {
                            view.FocusedColumn = e.PrevFocusedColumn;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void grvLabelOption_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                {
                    view.FocusedColumn = view.Columns[0];
                    view.ShowEditor();
                }

                this.UpdateBalance(view);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLabelOption_FocusedRowChanged", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLabelOption_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                DataRowView rowView = (DataRowView)e.Row;

                if (rowView["STD_QTY"].ToString() == "")
                {
                    e.Valid = false;
                    //view.SetColumnError(view.Columns["QTY_SKU"], "Value can not be null",
                    //    DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["STD_QTY"];
                }

                if (rowView["DOC_PKG_QTY"].ToString() == "")
                {
                    e.Valid = false;
                    //view.SetColumnError(view.Columns["QTY_SKU"], "Value can not be null",
                    //    DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    view.FocusedColumn = view.Columns["DOC_PKG_QTY"];
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLabelOption_ValidateRow", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLabelOption_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;
            this.UpdateBalance(view);
        }

        private void grvLabelOption_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                
                view.SetFocusedRowCellValue("ARRIVAL_NO", this.ARRIVAL_NO);
                view.SetFocusedRowCellValue("MTL_SEQ_NO", this.ITEM.MTL_SEQ_NO);
                view.SetFocusedRowCellValue("LINE_NO_SUB", view.RowCount);
                view.SetFocusedRowCellValue("UNIT", this.lblITEM_UNIT.Text);
                view.SetFocusedRowCellValue("LINE_NO", this.LINE_NO);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error grvLabelOption_InitNewRow", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void grvLabelOption_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}