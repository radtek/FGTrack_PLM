using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HTN.BITS.FGTRACK.ASSEMBLY.Components
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
