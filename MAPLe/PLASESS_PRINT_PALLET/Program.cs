using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component;
using DevExpress.XtraEditors;
using System.Diagnostics;
using HTN.BITS.BLL.PLASESS;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET
{
    static class Program
    {
        /// <summary>
        /// Constructor for reload assembly
        /// </summary>
        static Program()
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
                {
                    AssemblyName requestedName = new AssemblyName(e.Name);
                    //request to change the dll assembly
                    if (requestedName.Name == "Oracle.DataAccess")
                        return Assembly.LoadFrom(UiUtility.GetOracleDataAccessPath());
                    else
                        return null;
                };
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ApplicationRunningHelper.AlreadyRunning())
            {
                return;
            }

            SetMinimizeMemory();

            try
            {
                

                //DevExpress.UserSkins.OfficeSkins.Register();
                //DevExpress.UserSkins.BonusSkins.Register();
                //DevExpress.Skins.SkinManager.EnableFormSkins();
                //DevExpress.Skins.SkinManager.EnableFormSkinsIfNotVista();
                //DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string appSkin = UiUtility.ApplicationStyle;

                if (!string.IsNullOrEmpty(appSkin))
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(appSkin);
                else
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();



                StartApplication();

                //Cursor.Current = Cursors.WaitCursor;

                //Process objProcess = Process.GetCurrentProcess();
                //string currAppName = objProcess.ProcessName;

                //if (Process.GetProcessesByName(currAppName).Length > 1)
                //{
                //    Cursor.Current = Cursors.Default;
                //    XtraMessageBox.Show("Print Partial Opened", "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Application.ExitThread();
                //    Application.Exit();
                //}
                //else
                //{
                //    Cursor.Current = Cursors.Default;
                //    StartApplication();
                //}
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }

        static void StartApplication()
        {
            frmPrintPallet mainTemp = null;
            string userID = string.Empty;

            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //if (UiUtility.START_UP)
            //{
            rkApp.SetValue("Details_On_Pallet", Application.ExecutablePath.ToString());
            //}
            //else
            //{
            //    rkApp.DeleteValue("Details_On_Pallet", false);
            //}

            try
            {
                //Start connect to DB 
                GlobalDB.Instance.Init();
                try
                {
                    GlobalDB.Instance.Connect();
                }
                catch (Exception ex)
                {
                    GlobalDB.Instance.Release();
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Application.Exit();
                    return;
                }

                using (frmPrintPallet mainMenu = new frmPrintPallet())
                {
                    GC.ReRegisterForFinalize(mainMenu);

                    mainMenu.UserID = userID;
                    mainTemp = mainMenu;

                    Application.Run(mainMenu);

                    GlobalDB.Instance.Disconenct();
                    GlobalDB.Instance.Release();

                    MemoryCleaner mc = new MemoryCleaner();
                    mc.Start();
                    GC.SuppressFinalize(mainMenu);
                    mc.Stop();

                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                GlobalDB.Instance.Disconenct();
                GlobalDB.Instance.Release();

                if (mainTemp != null)
                {
                    MemoryCleaner mc = new MemoryCleaner();
                    mc.Start();
                    GC.SuppressFinalize(mainTemp);
                    mc.Stop();
                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
        }

        private static void SetMinimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,
                (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
    }
}
