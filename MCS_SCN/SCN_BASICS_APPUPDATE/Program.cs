using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using HTN.BITS.MCS.SCN.APPUPDATE.Components;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace HTN.BITS.MCS.SCN.APPUPDATE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            frmUpdate tempUpdate = null;
            try
            {
                //Application.Run(new frmLockDown());
                using (frmUpdate fUpdate = new frmUpdate())
                {
                    GC.ReRegisterForFinalize(fUpdate);

                    tempUpdate = fUpdate;

                    fUpdate.UpdateFileName = args[1];
                    Application.Run(fUpdate);
                    //------This section will work after program finish update------
                    //test by jack
                    if (File.Exists(args[2]))
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = args[2];
                        proc.StartInfo.Arguments = "Updated";
                        proc.Start();
                    }
                    else
                    {
                        MessageBox.Show("Can't Start Application");
                    }

                    tempUpdate = null;
                    GC.SuppressFinalize(fUpdate);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                if (tempUpdate != null)
                {
                    FullScreenHandle.StopFullScreen(tempUpdate);
                    GC.SuppressFinalize(tempUpdate);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                if (tempUpdate != null)
                {
                    FullScreenHandle.StopFullScreen(tempUpdate);
                    GC.SuppressFinalize(tempUpdate);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Application.Exit();
            }
        }
    }
}