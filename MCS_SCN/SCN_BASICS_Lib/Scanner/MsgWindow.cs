using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Bt.ScanLib;
using Bt;

namespace HTN.BITS.MCS.SCN.LIB.Scanner
{
    //---------------------------------------------------------------------------------
    // Message window class
    //---------------------------------------------------------------------------------
    public class MsgWindow : Microsoft.WindowsCE.Forms.MessageWindow
    {
        public event Action BarcodeRead;
        //--------------------------------------------------------------
        // DLLImport
        //--------------------------------------------------------------
        [DllImport("coredll.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(IntPtr hObject);

        public MsgWindow()
        {
        }

        protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message msg)
        {
            switch (msg.Msg)
            {
                case (Int32)LibDef.WM_BT_SCAN:
                    // When reading is successful
                    if (msg.WParam.ToInt32() == (Int32)LibDef.BTMSG_WPARAM.WP_SCN_SUCCESS)
                    {
                        if (BarcodeRead != null)
                            BarcodeRead();
                    }

                    break;
            }

            base.WndProc(ref msg);
        }
    }
}
