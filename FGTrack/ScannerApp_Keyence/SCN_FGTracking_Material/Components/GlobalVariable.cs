using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HTN.BITS.FGTRACK.MATERIAL.Components
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

        private static int _delay_ms = 150;
        public static int delay_ms
        {
            get
            {
                return _delay_ms;
            }
        }

        private static int _delay_wt = 2000;
        public static int delay_wt
        {
            get
            {
                return _delay_wt;
            }
        }

        public static void Delay(int ms, EventHandler action)
        {
            try
            {
                var tmp = new Timer { Interval = ms };
                tmp.Tick += new EventHandler((o, e) => { tmp.Enabled = false; tmp.Dispose(); tmp = null; });
                tmp.Tick += action;
                tmp.Enabled = true;
                //using (var tmp = new Timer { Interval = ms })
                //{
                //    tmp.Tick += new EventHandler((o, e) => { tmp.Enabled = false; tmp.Dispose(); });
                //    tmp.Tick += action;
                //    tmp.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        [DllImport("itc50.dll", CharSet = CharSet.Unicode)]
        public static extern UInt32 ITCGetSerialNumber(string number, int buffSize);

        public static string GetSerialNumber()
        {
            string sSerial = "            ";
            UInt32 uRes = ITCGetSerialNumber(sSerial, sSerial.Length);
            if (uRes == 0)
            {
                if (sSerial.Length < 12)
                {
                    do
                    {
                        sSerial.Insert(0, "0");
                    } while (sSerial.Length < 12);
                }
                return sSerial;
            }
            else
                return "Error: " + uRes.ToString("X08");
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

        public static Bitmap GetBitmapFromByteArray(byte[] BMPArray)
        {
            // First, create a memory a stream where the
            // passed byte array will be stored
            MemoryStream BitmapMS = new MemoryStream();

            // Write the byte array to the memory stream
            BitmapMS.Write(BMPArray, 0, BMPArray.Length);

            // Create a new bitmap object from the memory stream
            Bitmap TheBitmap = new Bitmap(BitmapMS);

            // Finally, return the bitmap
            return TheBitmap;
        }
    }
}
