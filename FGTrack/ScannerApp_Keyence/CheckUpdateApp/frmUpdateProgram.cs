using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace CheckUpdateApp
{
    public partial class frmUpdateProgram : Form, IDisposable
    {
        public frmUpdateProgram()
        {
            InitializeComponent();

            int iLocTop = (158 - (this.Height / 2)) - 20;
            int iLocLeft = 119 - (this.Width / 2);

            this.Location = new Point(iLocLeft, iLocTop);
        }
        //private const string downloadPath = @"\\NHTN0003\Application_Service\TIMeSx2Service\UpdateFile\timesx2scanner";

        private string outputPath;
        public string OutputPath
        {
            set
            {
                this.outputPath = value;
            }
        }

        private string folderName;
        public string FolderName
        {
            get
            {
                return folderName;
            }
            set
            {
                if (folderName == value)
                    return;
                folderName = value;
            }
        }

        private string appName;
        public string AppName
        {
            get
            {
                return appName;
            }
            set
            {
                if (appName == value)
                    return;
                appName = value;
            }
        }


        private long totalFiles = 0;
        private long filesCompleted = 0;

        private void CopyFiles(string SourcePath, string DestPath)
        {
            totalFiles = getFileCount(SourcePath);
            copyFiles(SourcePath, DestPath);

            this.tmrFinishDownload.Enabled = true;
        }

        private long getFileCount(string SourcePath)
        {
            DirectoryInfo di = new DirectoryInfo(SourcePath);
            long count = 0;

            count += di.GetFiles().Length;

            foreach (DirectoryInfo subdir in di.GetDirectories())
                count += getFileCount(subdir.FullName);

            return count;
        }

        private void copyFiles(string SourcePath, string DestPath)
        {
            if (!Directory.Exists(DestPath))
            {
                Directory.CreateDirectory(DestPath);
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo(SourcePath);

                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.CopyTo(DestPath + @"\" + fi.Name, true);
                    filesCompleted++;
                    int percenComplete = Convert.ToInt32(((float)filesCompleted / (float)totalFiles * 100));
                    this.progressBar1.Value = percenComplete;
                    this.lblUpdateStatus.Text = string.Format("Update complete......{0}%", percenComplete);
                    Application.DoEvents();
                }

                foreach (DirectoryInfo subdir in di.GetDirectories())
                {
                    copyFiles(subdir.FullName, DestPath + @"\" + subdir.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tmrStartDownload_Tick(object sender, EventArgs e)
        {
            try
            {
                string downloadPath = string.Format(MobileConfiguration.Configuration.Settings["DownloadPath"].ToString(), this.FolderName);

                this.tmrStartDownload.Enabled = false;
                this.progressBar1.Value = 0;

                this.lblUpdateStatus.Text = "Start download file.......";
                this.CopyFiles(downloadPath, this.outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void frmUpdateProgram_Load(object sender, EventArgs e)
        {
            this.tmrStartDownload.Enabled = true;
        }

        private void tmrFinishDownload_Tick(object sender, EventArgs e)
        {
            this.tmrFinishDownload.Enabled = false;
            this.Close();
        }

        private void frmUpdateProgram_Closed(object sender, EventArgs e)
        {
            string appStartPart = string.Format("{0}", this.AppName); //this.outputPath,
            try
            {
                if (File.Exists(appStartPart))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = appStartPart;
                    proc.StartInfo.Arguments = "Updated";
                    proc.Start();
                }
                else
                {
                    MessageBox.Show("Can't Start Application");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}