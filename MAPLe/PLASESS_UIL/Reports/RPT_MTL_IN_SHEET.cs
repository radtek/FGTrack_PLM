using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HTN.BITS.UIL.PLASESS.Reports
{
    public partial class RPT_MTL_IN_SHEET : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_MTL_IN_SHEET()
        {
            InitializeComponent();
        }

        private void RPT_MTL_IN_SHEET_FillEmptySpace(object sender, BandEventArgs e)
        {
            //XRTable tb = this.xrtMTL_DTL;
            //tb.LocationF = new PointF(-1, -1);
            //int i = e.Band.Height;
            int i = 0;
            while (i < e.Band.Height)
            {
            

            XRTableCell tc1 = new XRTableCell();
            if((i + 25) >= e.Band.Height)
                tc1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            else
                tc1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left)));

            tc1.StylePriority.UseBorders = false;
            tc1.Weight = 0.094755489393550912;

            XRTableCell tc2 = new XRTableCell();
            tc2.BackColor = System.Drawing.Color.Transparent;
            tc2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc2.StylePriority.UseBackColor = false;
            tc2.StylePriority.UseBorders = false;
            tc2.StylePriority.UseFont = false;
            tc2.StylePriority.UsePadding = false;
            tc2.StylePriority.UseTextAlignment = false;
            tc2.Weight = 1.0369777605800556;

            XRTableCell tc3 = new XRTableCell();
            tc3.BackColor = System.Drawing.Color.Transparent;
            tc3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc3.StylePriority.UseBackColor = false;
            tc3.StylePriority.UseBorders = false;
            tc3.StylePriority.UseFont = false;
            tc3.StylePriority.UsePadding = false;
            tc3.StylePriority.UseTextAlignment = false;
            tc3.Weight = 0.50965250965250963;

            XRTableCell tc4 = new XRTableCell();
            tc4.BackColor = System.Drawing.Color.Transparent;
            tc4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc4.StylePriority.UseBackColor = false;
            tc4.StylePriority.UseBorders = false;
            tc4.StylePriority.UseFont = false;
            tc4.StylePriority.UsePadding = false;
            tc4.StylePriority.UseTextAlignment = false;
            tc4.Weight = 0.38223938223938225;

            XRTableCell tc5 = new XRTableCell();
            tc5.BackColor = System.Drawing.Color.Transparent;
            tc5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc5.StylePriority.UseBackColor = false;
            tc5.StylePriority.UseBorders = false;
            tc5.StylePriority.UseFont = false;
            tc5.StylePriority.UseTextAlignment = false;
            tc5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            tc5.Weight = 0.41698841698841693;

            XRTableCell tc6 = new XRTableCell();
            tc6.BackColor = System.Drawing.Color.Transparent;
            tc6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc6.StylePriority.UseBackColor = false;
            tc6.StylePriority.UseBorders = false;
            tc6.StylePriority.UseFont = false;
            tc6.StylePriority.UseTextAlignment = false;
            tc6.Weight = 0.17666449233832049;

            XRTableCell tc7 = new XRTableCell();
            tc7.BackColor = System.Drawing.Color.Transparent;
            tc7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tc7.StylePriority.UseBackColor = false;
            tc7.StylePriority.UseBorders = false;
            tc7.StylePriority.UseFont = false;
            tc7.StylePriority.UseTextAlignment = false;
            tc7.Weight = 0.38272194880776422;


            XRTableRow tr = new XRTableRow();
            tr.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            tc1,
            tc2,
            tc3,
            tc4,
            tc5,
            tc6,
            tc7});
            tr.Weight = 1;

            // = new XRTable();

            
                XRTable tb = new XRTable();

                tb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tb.LocationFloat = new DevExpress.Utils.PointFloat(12F, i);
                tb.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {tr});
                tb.SizeF = new System.Drawing.SizeF(777F, 25);
                tb.StylePriority.UseFont = false;

                e.Band.Controls.Add(tb);

                i += 25;
            }



            //this.xrLabel23.LocationF = new PointF(this.DetailReport.LocationFloat.X, this.DetailReport.LocationFloat.Y);

            //XRLabel vf = new XRLabel();
            //vf.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //vf.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //vf.LocationFloat = new DevExpress.Utils.PointFloat(0F, -50F);
            //vf.Angle = 90F;

            //vf.SizeF = new System.Drawing.SizeF(24.54F, 200F);
            //vf.StylePriority.UseFont = false;
            //vf.StylePriority.UseTextAlignment = false;
            //vf.Text = "VERIFICAT";
            //vf.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            //e.Band.Controls.Add(vf);
        }
    }
}
