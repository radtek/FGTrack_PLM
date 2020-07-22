using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HTN.BITS.MCS.SCN.APPUPDATE.Components;
using System.Net;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace HTN.BITS.MCS.SCN.APPUPDATE
{
    public partial class frmUpdate : Form, IDisposable
    {
        static Bitmap backgroundBmp = null;
        System.ComponentModel.ComponentResourceManager cusResult;

        public frmUpdate()
        {
            InitializeComponent();
            cusResult = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdate));
            FullScreenHandle.StartFullScreen(this);
            
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.Clear(Color.White);

        //    e.Graphics.DrawString("UPDATING.......", new Font("Tahoma", 13, FontStyle.Bold), new SolidBrush(Color.Black), 20, 60);
        //    e.Graphics.DrawString("PLEASE WAIT.....", new Font("Tahoma", 13, FontStyle.Bold), new SolidBrush(Color.Black), 60, 180);
        //    e.Graphics.DrawString("@2012 Hi-Tech Nittsu. All rights reserved", new Font("Tahoma", 7, FontStyle.Bold), new SolidBrush(Color.Gray), 22, 280);
        //}

        #region Variable Member

        private string updatefilename;
        

        #endregion

        #region Property Member

        public string UpdateFileName
        {
            set
            {
                this.updatefilename = value;
            }
        }

        #endregion

        #region Method Member

        private void StartInitialUpdate()
        {

            this.tmrInitial.Enabled = false;

            string _BASE_URI = MobileConfiguration.Configuration.Settings["ServiceURL"].ToString();
            int _TIMEOUT = int.Parse(MobileConfiguration.Configuration.Settings["ServiceTimeOut"].ToString(), NumberStyles.AllowThousands);
            string _USER_AUTH = MobileConfiguration.Configuration.Settings["UserAuthen"].ToString();



            string appPath = string.Format("{0}\\", Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase));

            string zipFileName = string.Format("{0}{1}", appPath, this.updatefilename);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string uri = string.Format("{0}api/AutoUpdate/GetLatestUpdated?filename={1}"
               , _BASE_URI
               , this.updatefilename
               );

                this.DownloadFileBinary(uri, _USER_AUTH, _TIMEOUT, zipFileName);

                this.ExtractFile(appPath, zipFileName);

                this.DeleteZipFile(zipFileName);
                //MessageBox.Show("Extract File Finish");

                Cursor.Current = Cursors.Default;

                this.Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void DownloadFileBinary(string uri, string userAuth, int timeout, string localFile)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);//ServicesUtil.ApiRequest(uri, "GET", this.USER_AUTH, this.TIMEOUT);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userAuth)));
                request.Method = "GET";
                request.Timeout = timeout;



                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Retrieve response stream
                Stream respStream = response.GetResponseStream();
                // Create local file
                FileStream wrtr = new FileStream(localFile, FileMode.Create);

                // Allocate byte buffer to hold stream contents
                byte[] inData = new byte[4096];

                // loop through response stream reading each data block
                //  and writing to the local file
                int bytesRead = respStream.Read(inData, 0, inData.Length);
                while (bytesRead > 0)
                {
                    wrtr.Write(inData, 0, bytesRead);
                    bytesRead = respStream.Read(inData, 0, inData.Length);
                }

                respStream.Close();
                wrtr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //private void DownloadFileBinary(string localFile, string downloadUrl)
        //{
        //    try
        //    {
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(downloadUrl);
        //        req.Proxy = null;
        //        req.Method = "GET";

        //        UserAuthentication userRequest = new UserAuthentication();
        //        req.Credentials = new NetworkCredential(userRequest.USER_NAME, userRequest.User_PASS, userRequest.User_DOMAIN);

        //        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

        //        // Retrieve response stream
        //        Stream respStream = resp.GetResponseStream();
        //        // Create local file
        //        FileStream wrtr = new FileStream(localFile, FileMode.Create);

        //        // Allocate byte buffer to hold stream contents
        //        byte[] inData = new byte[4096];

        //        // loop through response stream reading each data block
        //        //  and writing to the local file
        //        int bytesRead = respStream.Read(inData, 0, inData.Length);
        //        while (bytesRead > 0)
        //        {
        //            wrtr.Write(inData, 0, bytesRead);
        //            bytesRead = respStream.Read(inData, 0, inData.Length);
        //        }

        //        respStream.Close();
        //        wrtr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void ExtractFile(string appPath, string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    using (var zip1 = Ionic.Zip.ZipFile.Read(filename))
                    {
                        foreach (var entry in zip1)
                        {
                            entry.Extract(appPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Zip file not Exists!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception");
            }
        }

        private void DeleteZipFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    var fZip = new FileInfo(filename);
                    fZip.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            FullScreenHandle.StopFullScreen(this);
            GC.SuppressFinalize(this);
        }

        #endregion
        
        private void frmUpdate_Load(object sender, EventArgs e)
        {
            this.tmrInitial.Enabled = true;

            //this.pbBG.Image = HTN.BITS.BPRO.SCN.UPDATE.Properties.Resources.updateBG;
        }

        private void tmrInitial_Tick(object sender, EventArgs e)
        {
            this.StartInitialUpdate();
        }
    }
}