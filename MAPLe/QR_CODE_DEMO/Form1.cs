using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BusinessRefinery.Barcode;

namespace QR_CODE_DEMO
{
    public partial class Form1 : Form
    {
      

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            

            QRCode barcode = new QRCode();
            barcode.DataMode = QRCodeDataMode.Auto;
            barcode.Version = QRCodeVersion.V5;
            barcode.Code = "www.nittsu.co.th";
            //barcode.Code = "<add name=\"FG_TRACKING.ConnectString\" connectionString=\"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(COMMUNITY=tcp.world)(PROTOCOL=TCP)(Host=NHTN0003)(Port=1521)))(CONNECT_DATA=(SID=orcl10g)));Persist Security Info=false;";
            
            //barcode.drawBarcode2Stream("Stream object");

            Bitmap barcodeInBitmap = barcode.drawBarcodeOnBitmap();

            this.pictureBox1.Image = barcodeInBitmap;
            

            //barcode.drawBarcodeOnGraphics("Graphics object");
        }
    }
}
