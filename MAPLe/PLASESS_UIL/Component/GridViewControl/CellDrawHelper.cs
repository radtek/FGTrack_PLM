using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.BandedGrid.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;

namespace HTN.BITS.UIL.PLASESS.Component.GridViewControl
{
    public class CellDrawHelper
    {
        public static void DrawRowBorder(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = Brushes.Black;

            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, 2));
        }

        public static void DoDefaultDrawCell(BandedGridView view, RowCellCustomDrawEventArgs e)
        {
            PropertyInfo pi;
            GridControl grid;
            BandedGridViewInfo info;
            GridCellInfo cell;
            GridEditorContainerHelper helper;
            BandedGridViewDrawArgs args;

            info = view.GetViewInfo() as BandedGridViewInfo;
            cell = e.Cell as GridCellInfo;
            grid = view.GridControl;

            //This line Returns Null with the overrridden Grid. Otherwise it works fine with the normal gridcontrol and gridView.
            pi = grid.GetType().GetProperty("EditorHelper", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            helper = pi.GetValue(grid, null) as GridEditorContainerHelper;
            args = new BandedGridViewDrawArgs(e.Cache, info, e.Bounds);
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            helper.DrawCellEdit(args, cell.Editor, cell.ViewInfo, e.Appearance, cell.CellValueRect.Location);
        }
    }
}
