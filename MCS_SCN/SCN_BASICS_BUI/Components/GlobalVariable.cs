using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HTN.BITS.MCS.SCN.UIL.Components
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

        private static string languageSCN;
        public static string LanguageSCN
        {
            get
            {
                return languageSCN;
            }
            set
            {
                if (languageSCN == value)
                    return;
                languageSCN = value;
            }
        }

        public static String GetIPAddress()
        {
            string sHostIP = "";
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

            return sHostIP;

        }
    }
}
