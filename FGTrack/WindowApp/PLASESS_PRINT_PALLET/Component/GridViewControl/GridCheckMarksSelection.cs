using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component.GridViewControl
{
    public class GridCheckMarksSelection
    {
        
        protected GridView _view;
        protected ArrayList selection;
        GridColumn column;
        RepositoryItemCheckEdit edit;
        const int CheckboxIndent = 4;

        //test by jack
        //protected bool isUsedCondition = false;
        protected string fieldName = string.Empty;
        protected string activeFilterString = string.Empty;
        protected string operater = string.Empty;  //=, <>, IN, NOT IN, IS, IS NOT,
        protected bool isEnableSelectAll = true;

        public GridCheckMarksSelection()
        {
            selection = new ArrayList();
        }

        public GridCheckMarksSelection(GridView view)
            : this()
        {
            View = view;
        }

        public GridCheckMarksSelection(GridView view, string fieldName, string operater, string filterString)
            : this()
        {
            View = view;
            this.fieldName = fieldName;
            this.operater = operater;
            this.activeFilterString = filterString;
        }

        public GridCheckMarksSelection(GridView view, bool enableSelectAll)
            : this()
        {
            View = view;
            this.isEnableSelectAll = enableSelectAll;
        }

        public GridView View
        {
            get { return _view; }
            set
            {
                if (_view != value)
                {
                    Detach();
                    Attach(value);
                }
            }
        }

        public RepositoryItemCheckEdit Edit { get { return edit; } }

        public GridColumn CheckMarkColumn { get { return column; } }

        public int SelectedCount { get { return selection.Count; } }
        public object GetSelectedRow(int index)
        {
            return selection[index];
        }
        public int GetSelectedIndex(object row)
        {
            return selection.IndexOf(row);
        }
        public void ClearSelection()
        {
            selection.Clear();
            Invalidate();
        }
        public void SelectAll()
        {
            selection.Clear();
            // fast (won't work if the grid is filtered)
            //if(_view.DataSource is ICollection)
            //	selection.AddRange(((ICollection)_view.DataSource));
            //else
            // slow:
            //if (isUsedCondition)
            //{
            //    //_view.ActiveFilterCriteria = (new OperandProperty("LOADING_NO") == null);
            //    //_view.ActiveFilterCriteria = (new UnaryOperator(UnaryOperatorType.IsNull, "LOADING_NO"));
                  //"[LOADING_NO] IS NULL"
            //}
            if (!string.IsNullOrEmpty(this.activeFilterString))
            {
                string strValue = string.Empty;
                switch (this.operater)
                {
                    case "IN":
                        string[] arrIn = this.activeFilterString.Split(',');
                        strValue = "('" + string.Join("','", arrIn) + "')";
                        break;
                    case "NOT IN":
                        string[] arrNotIn = this.activeFilterString.Split(',');
                        strValue = "('" + string.Join("','", arrNotIn) + "')";
                        break;
                    default:
                        strValue = this.activeFilterString;
                        break;
                }
                _view.ActiveFilterString = string.Format("[{0}] {1} {2}", this.fieldName, this.operater, strValue);
            }

            for (int i = 0; i < _view.DataRowCount; i++)
                selection.Add(_view.GetRow(i));
            Invalidate();

            //_view.ActiveFilterCriteria = null;
            if (!string.IsNullOrEmpty(this.activeFilterString))
                _view.ActiveFilterString = string.Empty;
        }
        //public void SelectAllTest()
        //{
        //    selection.Clear();

        //    _view.ActiveFilterCriteria = //(new OperandProperty("LOADING_NO") == null);
        //    for (int i = 0; i < _view.DataRowCount; i++)
        //        selection.Add(_view.GetRow(i));
        //    Invalidate();
        //    _view.ActiveFilterCriteria = null;


        //}

        public void SelectGroup(int rowHandle, bool select)
        {
            if (IsGroupRowSelected(rowHandle) && select) return;
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                int childRowHandle = _view.GetChildRowHandle(rowHandle, i);
                if (_view.IsGroupRow(childRowHandle))
                    SelectGroup(childRowHandle, select);
                else
                    SelectRow(childRowHandle, select, false);
            }
            Invalidate();
        }
        public void SelectRow(int rowHandle, bool select)
        {
            SelectRow(rowHandle, select, true);
        }
        public void InvertRowSelection(int rowHandle)
        {
            if (View.IsDataRow(rowHandle))
            {
                SelectRow(rowHandle, !IsRowSelected(rowHandle));
            }
            if (View.IsGroupRow(rowHandle))
            {
                SelectGroup(rowHandle, !IsGroupRowSelected(rowHandle));
            }
        }
        public bool IsGroupRowSelected(int rowHandle)
        {
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                int row = _view.GetChildRowHandle(rowHandle, i);
                if (_view.IsGroupRow(row))
                {
                    if (!IsGroupRowSelected(row)) return false;
                }
                else
                    if (!IsRowSelected(row)) return false;
            }
            return true;
        }
        public bool IsRowSelected(int rowHandle)
        {
            if (_view.IsGroupRow(rowHandle))
                return IsGroupRowSelected(rowHandle);

            object row = _view.GetRow(rowHandle);
            return GetSelectedIndex(row) != -1;
        }

        protected virtual void Attach(GridView view)
        {
            if (view == null) return;
            selection.Clear();
            this._view = view;
            view.BeginUpdate();
            try
            {
                edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                //first check visible column
                column = view.Columns["CheckMarkSelection"];
                if (column == null)
                {
                    column = view.Columns.Insert(0);
                }

                column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                column.Visible = true;
                column.VisibleIndex = 0;
                column.Name = view.Name + "_" + "CheckMarkSelection";
                column.FieldName = "CheckMarkSelection";
                column.Caption = "Mark";
                column.OptionsColumn.ShowCaption = false;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.AllowSize = false;
                column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                column.OptionsColumn.FixedWidth = true;
                column.Width = GetCheckBoxWidth();
                column.ColumnEdit = edit;


                view.Click += new EventHandler(View_Click);
                view.CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
                view.CustomDrawGroupRow += new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
                view.CustomUnboundColumnData += new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
                view.KeyDown += new KeyEventHandler(view_KeyDown);
                view.RowStyle += new RowStyleEventHandler(view_RowStyle);
            }
            finally
            {
                view.EndUpdate();
            }
        }
        protected virtual void Detach()
        {
            if (_view == null) return;
            if (column != null)
                column.Dispose();
            if (edit != null)
            {
                _view.GridControl.RepositoryItems.Remove(edit);
                edit.Dispose();
            }

            _view.Click -= new EventHandler(View_Click);
            _view.CustomDrawColumnHeader -= new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
            _view.CustomDrawGroupRow -= new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
            _view.CustomUnboundColumnData -= new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
            _view.KeyDown -= new KeyEventHandler(view_KeyDown);
            _view.RowStyle -= new RowStyleEventHandler(view_RowStyle);

            _view = null;
        }
        protected int GetCheckBoxWidth()
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            int width = 0;
            GraphicsInfo.Default.AddGraphics(null);
            try
            {
                width = info.CalcBestFit(GraphicsInfo.Default.Graphics).Width;
            }
            finally
            {
                GraphicsInfo.Default.ReleaseGraphics();
            }
            return width + CheckboxIndent * 2;
        }

        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo(g);
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }

        void Invalidate()
        {
            _view.BeginUpdate();
            _view.EndUpdate();
        }
        void SelectRow(int rowHandle, bool select, bool invalidate)
        {
            if (IsRowSelected(rowHandle) == select) return;
            object row = _view.GetRow(rowHandle);
            if (select)
                selection.Add(row);
            else
                selection.Remove(row);
            if (invalidate)
            {
                Invalidate();
            }
        }
        void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == CheckMarkColumn)
            {
                if (e.IsGetData)
                    e.Value = IsRowSelected(e.RowHandle);
                else
                    SelectRow(e.RowHandle, (bool)e.Value);
            }
        }
        void view_KeyDown(object sender, KeyEventArgs e)
        {
            if (View.FocusedColumn != column || e.KeyCode != Keys.Space) return;
            if (!string.IsNullOrEmpty(this.activeFilterString))
            {
                switch (this.operater)
                {
                    case "IN":
                        string[] arrIn = this.activeFilterString.Split(',');
                        if (arrIn.Length > 0)
                        {
                            if (arrIn.Contains(_view.GetRowCellValue(View.FocusedRowHandle, this.fieldName).ToString()))
                                InvertRowSelection(View.FocusedRowHandle);
                        }
                        break;
                    case "NOT IN":
                        string[] arrNotIn = this.activeFilterString.Split(',');
                        if (arrNotIn.Length > 0)
                        {
                            if (arrNotIn.Contains(_view.GetRowCellValue(View.FocusedRowHandle, this.fieldName).ToString()))
                                InvertRowSelection(View.FocusedRowHandle);
                        }
                        break;
                    case ">":
                        if (Convert.ToInt32(_view.GetRowCellValue(View.FocusedRowHandle, this.fieldName), NumberFormatInfo.CurrentInfo) > int.Parse(this.activeFilterString))
                            InvertRowSelection(View.FocusedRowHandle);
                        break;
                    default:
                        InvertRowSelection(View.FocusedRowHandle);
                        break;
                }

                //string[] arr = this.activeFilterString.Split(',');
                //if (arr.Length > 0)
                //{
                //    if (arr.Contains(_view.GetRowCellValue(View.FocusedRowHandle, this.fieldName).ToString()))
                //        InvertRowSelection(View.FocusedRowHandle);
                //}

                //if (string.IsNullOrEmpty(_view.GetRowCellDisplayText(View.FocusedRowHandle, this.fieldName)))
                //    InvertRowSelection(View.FocusedRowHandle);
            }
            else
            {
                InvertRowSelection(View.FocusedRowHandle);
            }
        }
        void View_Click(object sender, EventArgs e)
        {
            GridHitInfo info;
            Point pt = _view.GridControl.PointToClient(Control.MousePosition);
            info = _view.CalcHitInfo(pt);
            if (info.Column == column)
            {
                if (info.InColumn && this.isEnableSelectAll)
                {
                    if (!string.IsNullOrEmpty(this.activeFilterString))
                    {
                        string strValue = string.Empty;
                        switch (this.operater)
                        {
                            case "IN":
                                string[] arrIn = this.activeFilterString.Split(',');
                                strValue = "('" + string.Join("','", arrIn) + "')";
                                break;
                            case "NOT IN":
                                string[] arrNotIn = this.activeFilterString.Split(',');
                                strValue = "('" + string.Join("','", arrNotIn) + "')";
                                break;
                            default:
                                strValue = this.activeFilterString;
                                break;
                        }
                        _view.ActiveFilterString = string.Format("[{0}] {1} {2}", this.fieldName, this.operater, strValue);

                        //_view.ActiveFilterCriteria = (new OperandProperty("LOADING_NO") == null);
                        //string[] arr = this.activeFilterString.Split(',');
                        //string builder = "'" + string.Join("','", arr) + "'";


                        //_view.ActiveFilterString = string.Format("[{0}] IN ({1})", this.fieldName, builder);

                        if (SelectedCount == _view.DataRowCount)
                            ClearSelection();
                        else
                            SelectAll();

                        _view.ActiveFilterString = null;
                    }
                    else
                    {
                        if (SelectedCount == _view.DataRowCount)
                            ClearSelection();
                        else
                            SelectAll();
                    }
                }

                if (info.InRowCell)
                {
                    if (!string.IsNullOrEmpty(this.activeFilterString))
                    {
                        switch (this.operater)
                        {
                            case "IN":
                                string[] arrIn = this.activeFilterString.Split(',');
                                if (arrIn.Length > 0)
                                {
                                    if (arrIn.Contains(_view.GetRowCellValue(info.RowHandle, this.fieldName).ToString()))
                                        InvertRowSelection(info.RowHandle);
                                }
                                break;
                            case "NOT IN":
                                string[] arrNotIn = this.activeFilterString.Split(',');
                                if (arrNotIn.Length > 0)
                                {
                                    if (arrNotIn.Contains(_view.GetRowCellValue(info.RowHandle, this.fieldName).ToString()))
                                        InvertRowSelection(info.RowHandle);
                                }
                                break;
                            case ">":
                                if (Convert.ToInt32(_view.GetRowCellValue(info.RowHandle, this.fieldName), NumberFormatInfo.CurrentInfo) > int.Parse(this.activeFilterString))
                                    InvertRowSelection(info.RowHandle);
                                break;
                            default:
                                InvertRowSelection(info.RowHandle);
                                break;
                        }
                        //if(string.IsNullOrEmpty(_view.GetRowCellDisplayText(info.RowHandle, this.fieldName)))
                        //    InvertRowSelection(info.RowHandle);
                    }
                    else
                    {
                        InvertRowSelection(info.RowHandle);
                    }
                }
            }
            if (info.InRow && _view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton)
            {
                InvertRowSelection(info.RowHandle);
            }
        }
        void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == column && this.isEnableSelectAll)
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == _view.DataRowCount);
                e.Handled = true;
            }
        }

        void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            info.GroupText = "         " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            Rectangle r = info.ButtonBounds;
            r.Offset(r.Width + CheckboxIndent * 2 - 1, 0);
            DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
            e.Handled = true;
        }
        void view_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (IsRowSelected(e.RowHandle))
            {
                //e.Appearance.BackColor = SystemColors.Highlight;
                //e.Appearance.ForeColor = SystemColors.HighlightText;
            }
        }
    }
}
