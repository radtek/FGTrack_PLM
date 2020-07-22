using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace BITS_APP_UPDATE
{
    public partial class frmUpdate : DevExpress.XtraEditors.XtraForm, IDisposable
    {
        public frmUpdate()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();

            GC.ReRegisterForFinalize(this);

            this.defaultLookAndFeel1.LookAndFeel.SkinName = ConfigurationManager.AppSettings["AppStyle"];
            this.downloadPath = ConfigurationManager.AppSettings["DownloadPath"];
            this.outputFile = ConfigurationManager.AppSettings["OutputFile"];
            this.applicationEXE = ConfigurationManager.AppSettings["AppStart"];
        }

        private string applicationEXE;
        private string downloadPath;
        private string outputFile;
        private string outputPath;
        public string OutputPath
        {
            set
            {
            	 this.outputPath = value;
            }
        }


        public void DownloadFile(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            try
            {
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(finishedDownload);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(updateProgress);

                client.DownloadFileAsync(new Uri(url), string.Format("{0}\\{1}", this.outputPath, this.outputFile));  //Application.StartupPath + "\\temp.flv"

            }
            catch (Exception ex)
            {
                if (client != null)
                {
                    client.Dispose();
                }

                MessageBox.Show(ex.Message);
            }
        }

        private void updateProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            this.lblUpdateStatus.Text = string.Format("Downloading.... {0}%", e.ProgressPercentage);
            this.pgbDownload.Position = e.ProgressPercentage;
        }

        private void finishedDownload(object sender, AsyncCompletedEventArgs e)
        {
            this.lblUpdateStatus.Text = "Download complete";

            this.UnZipFiles(string.Format("{0}\\{1}", this.outputPath, this.outputFile), this.outputPath, "", true);

            this.tmrFinishUpdate.Enabled = true;
        }

        private void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (password != null && password != String.Empty)
            {
                s.Password = password;
            }

            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory 
                if (directoryName != "")
                {
                    Directory.CreateDirectory(directoryName);
                }
                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = directoryName + "\\" + theEntry.Name;

                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath))
                        {
                            Directory.CreateDirectory(fullDirPath);
                        }

                        FileStream streamWriter = File.Create(fullPath);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
            }

            s.Close();
            if (deleteZipFile)
            {
                File.Delete(zipPathAndFile);
            }
        }

        private bool ExistUrl(string url)
        {
            Uri urlCheck = new Uri(url);
            WebRequest request = WebRequest.Create(urlCheck);
            request.Proxy = null;
            request.Timeout = 5000;

            WebResponse response;
            try
            {
                response = request.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false; //url does not exist
            }
        }

        private void tmrStartDownload_Tick(object sender, EventArgs e)
        {
            this.tmrStartDownload.Enabled = false;
            string urlPath = string.Format("{0}/{1}", this.downloadPath, this.outputFile);
            this.pgbDownload.Position = 0;

            if (this.ExistUrl(urlPath))
            {
                this.lblUpdateStatus.Text = "Start download file.......";
                this.DownloadFile(urlPath);
            }
            else
            {
                MessageBox.Show("Can't Update");
                this.Close();
            }
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            this.lblUpdateStatus.Text = "";
            this.tmrStartDownload.Enabled = true;
        }

        private void tmrFinishUpdate_Tick(object sender, EventArgs e)
        {
            tmrFinishUpdate.Enabled = false;
            this.Close();
        }

        private void frmUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            string appStartPart = string.Format("{0}\\{1}", this.outputPath, this.applicationEXE);

            try
            {
                if (File.Exists(appStartPart))
                {
                    this.StartApp();
                }
                else
                {
                    MessageBox.Show("Can't Start Application");

                    this.Controls.Clear();

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void StartApp()
        {
            this.Controls.Clear();

            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Application.Exit();
            Application.DoEvents();

            ThreadStart operation = new ThreadStart(StartApp_Thread);
            Thread thr = new Thread(operation);
            thr.Start();
        }

        private void StartApp_Thread()
        {
            Thread.Sleep(100);

            string appStartPart = string.Format("{0}\\{1}", this.outputPath, this.applicationEXE);

            Process proc = new Process();
            proc.StartInfo.FileName = appStartPart;
            proc.StartInfo.Arguments = "Updated";
            proc.Start();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}