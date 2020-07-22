using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using HTN.BITS.MCS.SCN.LOCKDOWN.Components;

namespace HTN.BITS.MCS.SCN.LOCKDOWN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            frmNewLockDown tempLock = null;
            try
            {
                HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.CallingAssembly = Assembly.GetExecutingAssembly();
                GlobalVariable.LanguageSelect = MobileConfiguration.Configuration.Settings["DefaultLanguage"].ToString();
                
                //clsBarcodeReader.Instance.InitialComponent();

                //Application.Run(new frmLockDown());
                using (frmNewLockDown fLockDown = new frmNewLockDown())
                {
                    GC.ReRegisterForFinalize(fLockDown);

                    tempLock = fLockDown;
                    Application.Run(fLockDown);

                    //clsBarcodeReader.Instance.Release();

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