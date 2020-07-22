using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.IO;
using HTN.BITS.FGTRACK.LIB.Scanner;

namespace HTN.BITS.SCN.LOCKDOWN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            frmLockDown tempLock = null;
            try
            {
                

                HTN.BITS.FGTRACK.LIB.ResourceManager.Instance.CallingAssembly = Assembly.GetExecutingAssembly();

                clsBarcodeReader.Instance.InitialComponent();

                //Application.Run(new frmLockDown());
                using (frmLockDown fLockDown = new frmLockDown())
                {
                    GC.ReRegisterForFinalize(fLockDown);

                    tempLock = fLockDown;

                    Application.Run(fLockDown);

                    clsBarcodeReader.Instance.Release();

                    tempLock = null;
                    GC.SuppressFinalize(fLockDown);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                if (tempLock != null)
                {
                    FullScreenHandle.StopFullScreen(tempLock);
                    GC.SuppressFinalize(tempLock);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                if (tempLock != null)
                {
                    FullScreenHandle.StopFullScreen(tempLock);
                    GC.SuppressFinalize(tempLock);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Application.Exit();
            }
        }
    }
}