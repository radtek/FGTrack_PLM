using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using HTN.BITS.FGTRACK.MTSTTAMPO.Components;
using HTN.BITS.FGTRACK.LIB.Scanner;
using System.Reflection;
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace HTN.BITS.FGTRACK.MTSTTAMPO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //StartApplication();
            try
            {
                bool isCheck = MobileConfiguration.Configuration.Settings["IsCheckVersion"].Equals("True");

                if (isCheck)
                {

                    Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    Version newVersion = NewVersion();

                    if (curVersion.CompareTo(newVersion) < 0)
                    {
                        MessageBox.Show("New version detect!!", "Please Update");
                        isCheck = CheckUpdate();
                        if (isCheck)
                        {
                            Application.Exit();

                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        else
                        {
                            StartApplication();
                        }
                    }
                    else
                    {
                        StartApplication();
                    }
                }
                else
                {
                    StartApplication();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
        }

        static void StartApplication()
        {
            frmMainMenu mainTemp = null;
            try
            {


                HTN.BITS.FGTRACK.LIB.ResourceManager.Instance.CallingAssembly = Assembly.GetExecutingAssembly();
                GlobalVariable.LanguageSelect = "en-US"; //fr-CA is Thai

                //clsBarcodeReader.Instance.InitialComponent();
                //ServiceProvider.Instance.Connect();

                //fMain = new frmMainMenu();
                //Application.Run(fMain);
                using (frmMainMenu fMain = new frmMainMenu())
                {
                    GC.ReRegisterForFinalize(fMain);

                    mainTemp = fMain;
                    Application.Run(fMain);

                    ServiceProvider.Instance.Disconnect();
                    //clsBarcodeReader.Instance.Release();

                    mainTemp = null;
                    GC.SuppressFinalize(fMain);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }


                if (mainTemp != null)
                {
                    FullScreenHandle.StopFullScreen(mainTemp);
                    GC.SuppressFinalize(mainTemp);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                Application.Exit();

            }
            catch (Exception ex)
            {
                if (mainTemp != null)
                {
                    FullScreenHandle.StopFullScreen(mainTemp);
                    GC.SuppressFinalize(mainTemp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Application.Exit();
            }
        }

        static Version NewVersion()
        {
            Version newVersion = null;

            string url = "";
            XmlTextReader reader = null;

            try
            {
                string xmlURL = MobileConfiguration.Configuration.Settings["URLCheckVersion"].ToString();

                if (ExistUrl(xmlURL))
                {
                    reader = new XmlTextReader(xmlURL);

                    reader.MoveToContent();
                    string elementName = "";

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ourfancyapp"))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                elementName = reader.Name;
                            }
                            else
                            {
                                if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                {
                                    switch (elementName)
                                    {
                                        case "scanner_version":
                                            newVersion = new Version(reader.Value);
                                            break;
                                        case "url":
                                            url = reader.Value;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The System Can't Check for Update Program", "Warning");
                    newVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                newVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return newVersion;
        }

        static bool ExistUrl(string url)
        {
            Uri urlCheck = new Uri(url);
            WebRequest request = WebRequest.Create(urlCheck);
            request.Timeout = 15000;

            WebResponse response;
            try
            {
                response = request.GetResponse();
                request.Abort();
                request = null;
                return true;
            }
            catch (Exception)
            {
                return false; //url does not exist
            }
        }

        static bool CheckUpdate()
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
                string appPath = Path.GetDirectoryName(codeBase);
                int index = codeBase.LastIndexOf(@"/") + 1;

                string fileUpdate = string.Format("{0}\\{1}", appPath, "CheckUpdateApp.exe");
                string startAppName = codeBase.Substring(index, codeBase.Length - index);

                if (File.Exists(fileUpdate))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = fileUpdate;
                    proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", appPath, "FGTracking_FGWHS", startAppName);
                    proc.Start();

                    //Application.Exit();
                    return true;
                }
                else
                {
                    MessageBox.Show("File Not Exist");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}