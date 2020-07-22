using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using Bt;
using Bt.SysLib;

namespace HTN.BITS.SCN.LOCKDOWN.Components
{
    public class GlobalVariable
    {
        private static string languageSelect;
        public static string LanguageSelect
        {
            get
            {
                return languageSelect;
            }
            set
            {
                if (languageSelect == value)
                    return;
                languageSelect = value;
            }
        }

        //public static string GetSerialNumber()
        //{
        //    try
        //    {
        //        string sSerial;
        //        UInt32 idSet = 0;
        //        Int32 ret = 0;
        //        IntPtr pValueGetDef_SerialNo = Marshal.AllocCoTaskMem((int)((LibDef.BT_SYS_SERIALNO_MAXLEN + 1) * sizeof(Char)));
        //        // Serial number
        //        idSet = LibDef.BT_SYS_PRM_SERIALNO;
        //        ret = Terminal.btGetHandyParameter(idSet, pValueGetDef_SerialNo);
        //        if (ret != LibDef.BT_OK)
        //        {
        //            sSerial = "btGetHandyParameter error ret[" + ret + "]";
        //            sSerial = "";
        //        }

        //        sSerial = Marshal.PtrToStringUni(pValueGetDef_SerialNo);
        //        return sSerial;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static String GetIPAddress()
        {
            string sHostIP = string.Empty;

            try
            {

                IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                IPAddress[] addr = ipEntry.AddressList.Where(w => w.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

                if (addr.Length != 0)
                {
                    sHostIP = addr[0].ToString();

                }
                else
                {
                    sHostIP = "Unknow";
                }
            }
            catch (Exception ex)
            {
                sHostIP = "Unknow";
            }

            return sHostIP;
        }
    }
}
