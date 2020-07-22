using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Reports
{
    public partial class RPT_PALLET_DETAIL : DevExpress.XtraReports.UI.XtraReport
    {
        public RPT_PALLET_DETAIL()
        {
            InitializeComponent();
        }

        private void RPT_PALLET_DETAIL_FillEmptySpace(object sender, BandEventArgs e)
        {
            /*
            //PART NO LABEL
            XRLabel esLBL_PART_NO = new XRLabel();
            esLBL_PART_NO.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            esLBL_PART_NO.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            esLBL_PART_NO.SizeF = new System.Drawing.SizeF(102.0833F, 33.41667F);
            esLBL_PART_NO.StylePriority.UseFont = false;
            esLBL_PART_NO.StylePriority.UseTextAlignment = false;
            esLBL_PART_NO.Text = "PART NO.:";
            esLBL_PART_NO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            esLBL_PART_NO.LocationFloat = new DevExpress.Utils.PointFloat(5.749938F, 20.999996F);

            e.Band.Controls.Add(esLBL_PART_NO);

            //PART NO LINE
            XRLine esLINE_PART_NO = new XRLine();
            esLINE_PART_NO.LocationFloat = new DevExpress.Utils.PointFloat(117.8333F, 37.41665F);
            esLINE_PART_NO.SizeF = new System.Drawing.SizeF(280.9791F, 2F);

            e.Band.Controls.Add(esLINE_PART_NO);

            //QTY LABEL
            XRLabel esLBL_QTY = new XRLabel();
            esLBL_QTY.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            esLBL_QTY.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            esLBL_QTY.SizeF = new System.Drawing.SizeF(58.33334F, 33.41667F);
            esLBL_QTY.StylePriority.UseFont = false;
            esLBL_QTY.StylePriority.UseTextAlignment = false;
            esLBL_QTY.Text = "QTY.";
            esLBL_QTY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            esLBL_QTY.LocationFloat = new DevExpress.Utils.PointFloat(419.0207F, 20.999996F);

            e.Band.Controls.Add(esLBL_QTY);

            //QTY BOX LINE
            XRLine esLINE_QTY_BOX = new XRLine();
            esLINE_QTY_BOX.SizeF = new System.Drawing.SizeF(125.0209F, 2F);
            esLINE_QTY_BOX.LocationFloat = new DevExpress.Utils.PointFloat(477.3541F, 37.41665F);

            e.Band.Controls.Add(esLINE_QTY_BOX);

            //BOX LABEL
            XRLabel esLBL_BOX = new XRLabel();
            esLBL_BOX.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            esLBL_BOX.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            esLBL_BOX.SizeF = new System.Drawing.SizeF(58.33334F, 33.41667F);
            esLBL_BOX.StylePriority.UseFont = false;
            esLBL_BOX.StylePriority.UseTextAlignment = false;
            esLBL_BOX.Text = "BOX";
            esLBL_BOX.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            esLBL_BOX.LocationFloat = new DevExpress.Utils.PointFloat(602.375F, 20.999996F);

            e.Band.Controls.Add(esLBL_BOX);

            //EQUAL LABEL
            XRLabel esLBL_EQUAL = new XRLabel();
            esLBL_EQUAL.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            esLBL_EQUAL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            esLBL_EQUAL.SizeF = new System.Drawing.SizeF(58.33334F, 33.41667F);
            esLBL_EQUAL.StylePriority.UseFont = false;
            esLBL_EQUAL.StylePriority.UseTextAlignment = false;
            esLBL_EQUAL.Text = "=";
            esLBL_EQUAL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            esLBL_EQUAL.LocationFloat = new DevExpress.Utils.PointFloat(670.7916F, 20.999996F);

            e.Band.Controls.Add(esLBL_EQUAL);

            //QTY PCS LINE
            XRLine esLINE_QTY_PCS = new XRLine();
            esLINE_QTY_PCS.SizeF = new System.Drawing.SizeF(146.5416F, 2F);
            esLINE_QTY_PCS.LocationFloat = new DevExpress.Utils.PointFloat(729.125F, 37.41665F);

            e.Band.Controls.Add(esLINE_QTY_PCS);

            //PCS LABEL
            XRLabel esLBL_PCS = new XRLabel();
            esLBL_PCS.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            esLBL_PCS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            esLBL_PCS.SizeF = new System.Drawing.SizeF(58.33334F, 33.41667F);
            esLBL_PCS.StylePriority.UseFont = false;
            esLBL_PCS.StylePriority.UseTextAlignment = false;
            esLBL_PCS.Text = "PCS.";
            esLBL_PCS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            esLBL_PCS.LocationFloat = new DevExpress.Utils.PointFloat(875.6666F, 20.999996F);

            e.Band.Controls.Add(esLBL_PCS);
             * */
        }

    }
}
