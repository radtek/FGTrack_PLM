using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
//using Intermec.DataCollection;

namespace HTN.BITS.FGTRACK.LIB.Scanner
{
    public sealed class clsBarcodeReader : IDisposable
    {
        static clsBarcodeReader instance = null;
        

       // private CSymbology symb = null;
        private BarcodeReader barReader = null;

        private clsBarcodeReader()
        {
            IsReleased = false;
        }

        public void InitialComponent()
        {
            Int32 ret = 0;
                //this.symb = new CSymbology("AllScanners");

            //use symbology exception
                //this.symb.EnableSymbologyExceptions(true);

            //set enable scan format type

            //set Code39 enable status
                //this.symb.Code39.Enable = true;

            ////set code128 enable status
            //this.symb.Code128.Enable = true;

            //set PDF417 enable status
                //this.symb.Pdf417.Enable = true;

            //set QrCode enable status
                //this.symb.QrCode.Enable = true;

            ////set Code93 enable status
            //this.symb.Code93.Enable = true;

            //this.barReader = new BarcodeReader();
            //this.barReader.ScannerEnable = false;
            ret = Bt.ScanLib.Control.btScanDisable();
        }

        public bool IsReleased { get; private set; }

        static clsBarcodeReader()
        {
            instance = new clsBarcodeReader();
        }

        public static clsBarcodeReader Instance
        {
            get
            {
                if (instance == null) instance = new clsBarcodeReader();
                return instance;
            }
        }

        public BarcodeReader BarReader
        {
            get
            {
                if (this.barReader == null)
                {
                    throw new Exception("Control is null");
                }
                return this.barReader;
            }

            set
            {
                if (this.barReader == value)
                    return;
                this.barReader = value;
            }
        }

        public void Release()
        {
            IsReleased = true;
            //this.barReader.Dispose();
            //this.barReader = null;
            //this.symb = null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Release();
        }

        #endregion
    }
}
