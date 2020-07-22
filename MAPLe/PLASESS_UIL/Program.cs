using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HTN.BITS.UIL.PLASESS;
using System.Diagnostics;
using HTN.BITS.UIL.PLASESS.Component;
using System.Configuration;
using System.IO;
using HTN.BITS.BLL.PLASESS;
using DevExpress.XtraEditors;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PLASESS_UIL
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
                XtraMessageBox.Show("FG-TRACKING Opened", "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DevExpress.UserSkins.OfficeSkins.Register();
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                //DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);


                //check folder permission
                bool isP = IsPermissionFolder();
                if (!isP)
                    AllowFolderPermission();


                string appSkin = UiUtility.ApplicationStyle;

                if (!string.IsNullOrEmpty(appSkin))
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(appSkin);
                else
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();

                

                Cursor.Current = Cursors.WaitCursor;

                //Process objProcess = Process.GetCurrentProcess();
                //string currAppName = objProcess.ProcessName;

                //if (Process.GetProcessesByName(currAppName).Length > 1)
                //{
                //    Cursor.Current = Cursors.Default;
                    
                //    Application.ExitThread();
                //    Application.Exit();
                //}
                //else
                //{
                if (UiUtility.IsAutoCheckVersion)
                {
                    bool isClearGrid = false;
                    Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    Version newVersion = UiUtility.CheckVersion(false, out isClearGrid);//NewVersion();

                    if (curVersion.CompareTo(newVersion) != 0)
                    {
                        Cursor.Current = Cursors.Default;

                        XtraMessageBox.Show("THE SYSTEM DETECTED NEW VERSION!!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string fileUpdate = string.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["AppUpdate"]);
                        if (File.Exists(fileUpdate))
                        {
                            //Check Clear Grid
                            if (isClearGrid) ClearGridHistory();

                            using (Process proc = new Process())
                            {
                                proc.StartInfo.FileName = fileUpdate;
                                proc.StartInfo.Arguments = string.Format("\"{0}\"", Application.StartupPath);
                                proc.Start();
                            }

                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            Application.Exit();
                        }
                        else
                        {
                            StartApplication();
                        }

                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        StartApplication();
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    StartApplication();
                }

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
            frmMainMenu mainTemp = null;
            string userID = string.Empty;
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

                DialogResult result = DialogResult.None;
                using (frmLogin login = new frmLogin())
                {
                    result = login.ShowDialog();
                    userID = login.UserID;
                }

                if (result == DialogResult.Yes)
                {
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime = true;
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
                    //go to next step
                    using (frmMainMenu mainMenu = new frmMainMenu())
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

                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.IsRuntime = false;
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();

                    Application.Exit();
                }
                else
                {
                    //no running
                    GlobalDB.Instance.Disconenct();

                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForPendingFinalizers();

                    Application.Exit();
                }
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

        static void ClearGridHistory()
        {
            string stateLayoutPath = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.StateConfigPath);

            if (Directory.Exists(stateLayoutPath))
            {
                UiUtility.ClearFolder(stateLayoutPath);
            }
        }

        static bool IsPermissionFolder()
        {
            bool isPermiss = false;

            try
            {
                string path = Application.StartupPath;
                string NtAccountName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                DirectoryInfo di = new DirectoryInfo(path);
                DirectorySecurity acl = di.GetAccessControl(AccessControlSections.Access);
                AuthorizationRuleCollection rules = acl.GetAccessRules(true, true, typeof(NTAccount));

                //Go through the rules returned from the DirectorySecurity
                foreach (AuthorizationRule rule in rules)
                {
                    //If we find one that matches the identity we are looking for
                    if (rule.IdentityReference.Value.Equals(NtAccountName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //Cast to a FileSystemAccessRule to check for access rights
                        if ((((FileSystemAccessRule)rule).FileSystemRights & FileSystemRights.WriteData) > 0)
                        {
                            isPermiss = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return isPermiss;
        }

        static void AllowFolderPermission()
        {
            try
            {
                string path = Application.StartupPath;
                string NtAccountName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                //IdentityReference everybodyIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                FileSystemAccessRule roleFull = new FileSystemAccessRule(NtAccountName,
                                                                        FileSystemRights.FullControl |
                                                                        FileSystemRights.Modify |
                                                                        FileSystemRights.ReadAndExecute |
                                                                        FileSystemRights.ListDirectory |
                                                                        FileSystemRights.Read |
                                                                        FileSystemRights.Write,
                                                                        InheritanceFlags.ContainerInherit |
                                                                        InheritanceFlags.ObjectInherit,
                                                                        PropagationFlags.None,
                                                                        AccessControlType.Allow);


                //DirectorySecurity dirSecurity = new DirectorySecurity(path, AccessControlSections.Group);
                DirectorySecurity dirSecurity = Directory.GetAccessControl(path);

                dirSecurity.AddAccessRule(roleFull);
                Directory.SetAccessControl(path, dirSecurity);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
