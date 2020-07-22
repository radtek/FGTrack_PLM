using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BITS_APP_UPDATE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            frmUpdate tempUpdate = null;
            try
            {
                DevExpress.UserSkins.OfficeSkins.Register();
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                Application.SetCompatibleTextRenderingDefault(false);

                using (frmUpdate fUpdate = new frmUpdate())
                {
                    if (args.Length > 0)
                    {
                        fUpdate.OutputPath = args[0];
                    }
                    tempUpdate = fUpdate;
                         
                    Application.Run(fUpdate);

                    GC.SuppressFinalize(fUpdate);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (tempUpdate != null)
                {
                    GC.SuppressFinalize(tempUpdate);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();
            }
        }
    }
}
