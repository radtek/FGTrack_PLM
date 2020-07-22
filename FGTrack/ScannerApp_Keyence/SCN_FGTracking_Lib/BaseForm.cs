using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace HTN.BITS.FGTRACK.LIB
{
    public partial class BaseForm : Form
    {
      //  private Intermec.DataCollection.CSymbology symb = null;
      //  private Intermec.DataCollection.BarcodeReader barReader = null;

        public BaseForm()
        {
            InitializeComponent();

            FullScreenHandle.StartFullScreen(this);

            //create a instance of CSymbology object
            //if (this.symb == null)
            //{
            //    this.symb = new CSymbology("AllScanners");
            //    this.symb.EnableAll();

            //    //use symbology exception
            //    this.symb.EnableSymbologyExceptions(true);
            //}

            //if (this.barReader == null)
            //{
            //    this.barReader = new Intermec.DataCollection.BarcodeReader();
            //    this.SuspendLayout();
            //}

        }

        private string strTest;
        public string StrTest
        {
            get
            {
                return strTest;
            }
            set
            {
                strTest = value;
            }
        }

        //public BarcodeReader GetBarReader
        //{
        //    get
        //    {
        //        return this.barReader;
        //    }
        //    set
        //    {
        //        this.barReader = value;
        //    }
        //}

        protected override void OnClosing(CancelEventArgs e)
        {
            

            //if (this.barReader != null)
            //{
            //    this.barReader.Dispose();
            //    this.barReader = null;
            //}
            base.OnClosing(e);
        }
    }
}