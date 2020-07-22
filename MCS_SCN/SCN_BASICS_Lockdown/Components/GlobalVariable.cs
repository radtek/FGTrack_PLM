using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HTN.BITS.MCS.SCN.LOCKDOWN.Components
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

        public static String GetIPAddress()
        {
            string sHostIP = "";
            IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            if (addr.Length != 0)
            {
                sHostIP = addr[0].ToString();

            }
            else
            {
                sHostIP = "Unknow";
            }

            return sHostIP;
        }
    }
}
