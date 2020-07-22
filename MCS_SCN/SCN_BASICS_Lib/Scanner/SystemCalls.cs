using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace HTN.BITS.MCS.SCN.LIB.Scanner
{
    public class SystemCalls
    {
        //See more at http://msdn2.microsoft.com/en-us/library/ms927178.aspx
        public const byte VK_NONAME = 0xFC;  // Do nothing
        public const byte VK_ESC = 0x1B;  // Smartphone back-button
        public const byte VK_F4 = 0x73;  // Home Screen
        public const byte VK_APP6 = 0xC6;  // Lock the keys on Smartphone
        public const byte VK_F22 = 0x85;  // Lock the keys on PocketPC (VK_KEYLOCK)
        public const byte VK_F16 = 0x7F;  // Toggle Speakerphone
        public const byte VK_OFF = 0xDF;  // Power button

        /// <summary>
        /// Puts `key` into to global keyboard buffer
        /// </summary>
        /// <param name="key"></param>
        public static void SendKey(byte key)
        {
            const int KEYEVENTF_KEYUP = 0x02;
            const int KEYEVENTF_KEYDOWN = 0x00;
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }

        [DllImport("coredll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    }

}
