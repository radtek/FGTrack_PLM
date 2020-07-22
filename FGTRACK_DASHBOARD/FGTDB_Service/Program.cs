using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration.Install;
using System.Threading;
using System.Security.Permissions;
  

namespace FGTDB_Service
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
                        return Assembly.LoadFrom(ServicesUtil.GetOracleDataAccessPath());
                    else
                        return null;
                };
            }
            catch (Exception ex)
            {
            }
        }

        //#region Auto Create Service


        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        //static void Main()
        //{
        //    bool _IsInstalled = false;
        //    bool serviceStarting = false;
        //    string SERVICE_NAME = "FGTDB_Service";

        //    ServiceController[] services = ServiceController.GetServices();

        //    foreach (ServiceController service in services)
        //    {
        //        if (service.ServiceName.Equals(SERVICE_NAME))
        //        {
        //            _IsInstalled = true;
        //            if (service.Status == ServiceControllerStatus.StartPending)
        //            {
        //                // If the status is StartPending then the service was started via the SCM             
        //                serviceStarting = true;
        //            }
        //            break;
        //        }
        //    }

        //    if (!serviceStarting)
        //    {
        //        if (_IsInstalled == true)
        //        {
        //            // Thanks to PIEBALDconsult's Concern V2.0
        //            DialogResult dr = new DialogResult();
        //            dr = MessageBox.Show("Do you REALLY like to uninstall the " + SERVICE_NAME + "?", "Danger", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //            if (dr == DialogResult.Yes)
        //            {
        //                SelfInstaller.UninstallMe();
        //                MessageBox.Show("Successfully uninstalled the " + SERVICE_NAME, "Status",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        else
        //        {
        //            DialogResult dr = new DialogResult();
        //            dr = MessageBox.Show("Do you REALLY like to install the " + SERVICE_NAME + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //            if (dr == DialogResult.Yes)
        //            {
        //                SelfInstaller.InstallMe();
        //                MessageBox.Show("Successfully installed the " + SERVICE_NAME, "Status",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Register Unhandled Exception Handler
        //        AppDomain.CurrentDomain.UnhandledException +=
        //            new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
        //        //SelfInstaller.InstallMe();
        //        // Started from the SCM
        //        System.ServiceProcess.ServiceBase[] servicestorun;
        //        servicestorun = new System.ServiceProcess.ServiceBase[] { new FGTDB_Service() };
        //        ServiceBase.Run(servicestorun);
        //    }
        //}

        //static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        //{

        //    MessageBox.Show("Error : " + args.ExceptionObject.ToString(), "Status",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        //public static class SelfInstaller
        //{
        //    private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
        //    public static bool InstallMe()
        //    {
        //        try
        //        {
        //            ManagedInstallerClass.InstallHelper(
        //                new string[] { _exePath });
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return true;
        //    }

        //    public static bool UninstallMe()
        //    {
        //        try
        //        {
        //            ManagedInstallerClass.InstallHelper(
        //                new string[] { "/u", _exePath });
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //}


        //#endregion

        #region Test Debug

         //<summary>
         //The main entry point for the application.
         //</summary>
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++)
            {
                // start with debug mode
                if (string.Compare(args[i], "-debug", true) == 0)
                {
                    FGTDB_Service svc = new FGTDB_Service();
                    svc.DebugStart();
                    Thread.Sleep(Timeout.Infinite);
                    return;
                }
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new FGTDB_Service() 
            };
            ServiceBase.Run(ServicesToRun);
        }

        #endregion

    }
}
