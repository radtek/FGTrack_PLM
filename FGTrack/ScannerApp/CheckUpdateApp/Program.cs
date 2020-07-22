using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CheckUpdateApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            frmUpdateProgram mainTemp = null;

            try
            {
                using (frmUpdateProgram fUpdate = new frmUpdateProgram())
                {
                    GC.ReRegisterForFinalize(fUpdate);

                    if (args.Length > 0)
                    {
                        fUpdate.OutputPath = args[0];
                        fUpdate.FolderName = args[1];
                        fUpdate.AppName = args[2];
                    }

                    Application.Run(fUpdate);

                    GC.SuppressFinalize(fUpdate);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                if (mainTemp != null)
                {
                    GC.SuppressFinalize(mainTemp);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            
        }
    }
}