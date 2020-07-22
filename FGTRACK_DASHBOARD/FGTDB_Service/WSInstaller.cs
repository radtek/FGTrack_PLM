using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Management;


namespace EDC_Service
{
    [RunInstaller(true)]
    public partial class WSInstaller : Installer
    {
        public WSInstaller()
        {
            ServiceProcessInstaller process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            ServiceInstaller serviceAdmin = new ServiceInstaller();
            serviceAdmin.StartType = ServiceStartMode.Automatic;
            serviceAdmin.ServiceName = "FGTDB_Service";
            serviceAdmin.DisplayName = "FGTDB_Service";
            

            Installers.Add(process);
            Installers.Add(serviceAdmin);

            // Here is where we set the bit on the value in the registry.
            // Grab the subkey to our service
            RegistryKey ckey = Registry.LocalMachine.OpenSubKey(
              @"SYSTEM\CurrentControlSet\Services\FGTDB_Service", true);
            // Good to always do error checking!
            if (ckey != null)
            {
                // Ok now lets make sure the "Type" value is there, 
                //and then do our bitwise operation on it.
                if (ckey.GetValue("Type") != null)
                {
                    ckey.SetValue("Type", ((int)ckey.GetValue("Type") | 256));
                }
            }
        }

        private void WSInstaller_Committed(object sender, InstallEventArgs e)
        {
            ConnectionOptions coOptions = new ConnectionOptions();
            coOptions.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope mgmtScope = new ManagementScope(@"root\CIMV2", coOptions);
            mgmtScope.Connect();
            ManagementObject wmiService;
            wmiService = new ManagementObject("Win32_Service.Name='FGTDB_Service'");
            ManagementBaseObject InParam = wmiService.GetMethodParameters("Change");
            InParam["DesktopInteract"] = true;
            ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", InParam, null);
        }
    }
}
