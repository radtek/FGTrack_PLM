using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BITS_TEST_APP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OraDB.Instance.Init();
            OraDB.Instance.Connect();

            Application.Run(new Form1());

            OraDB.Instance.Disconenct();
            OraDB.Instance.Release();
        }
    }
}
