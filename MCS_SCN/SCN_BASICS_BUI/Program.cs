using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.IO;
using HTN.BITS.MCS.SCN.UIL.Components;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using HTN.BITS.MCS.SCN.BLL;
using HTN.BITS.MCS.SCN.BEL;

namespace HTN.BITS.MCS.SCN.UIL
{
    static class Program
    {
        //static SplashInitForm splashForm = null;

        [MTAThread]
        static void Main()
        {

            //StartApplication();
            HTN.BITS.MCS.SCN.LIB.ResourceManager.Instance.CallingAssembly = Assembly.GetExecutingAssembly();
            GlobalVariable.LanguageSelect = MobileConfiguration.Configuration.Settings["DefaultLanguage"].ToString(); //fr-CA is Thai

            //splashForm = new SplashInitForm();
            //splashForm.Show();

            // Application.DoEvents();

            //InitializationApp initializer = new InitializationApp();
            //initializer.Initialize(splashForm);

            try
            {
                //StartApplication();

                bool isCheck = MobileConfiguration.Configuration.Settings["IsCheckVersion"].Equals("True");


                if (isCheck)
                {
                    T_APP_UPDATE objTAU = null;
                    string updateFilename = string.Empty;
                    Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    Version newVersion = curVersion;//NewVersion();
                    String result = String.Empty;

                    using (AdminBLL admBLL = new AdminBLL())
                    {
                        result = admBLL.CheckVersion(curVersion.ToString());
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        newVersion = new Version(result);  //Version.Parse(response.Content.ReadAsAsync<string>().Result);
                        //updateFilename = objTAU.APP_FILENAME;
                    }

                    if (curVersion.ToString() != result.ToString())
                    {
                        CloseInitial();

                        MessageBox.Show("Detected New Version!!", "Please Update");
                        isCheck = CheckUpdate("mcs.zip");
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
                //clsBarcodeReader.Instance.InitialComponent();

                //ServiceProvider.Instance.Connect();
                //fMain = new frmMainMenu();
                //Application.Run(fMain);
                using (frmMainMenu fMain = new frmMainMenu())
                {
                    //GC.ReRegisterForFinalize(fMain);

                    mainTemp = fMain;
                    Application.Run(fMain);

                    //ServiceProvider.Instance.Disconnect();
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
                MessageBox.Show(ex.ToString());
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
                    CloseInitial();

                    MessageBox.Show("The System Can't Check for Update Program", "Warning");
                    newVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error");
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
            request.Proxy = null;

            //UserAuthentication userRequest = new UserAuthentication();
            //request.Credentials = new NetworkCredential(userRequest.USER_NAME, userRequest.User_PASS, userRequest.User_DOMAIN);

            request.Timeout = 4500;

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

        static bool CheckUpdate(string filename)
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
                string appPath = Path.GetDirectoryName(codeBase);
                int index = codeBase.LastIndexOf(@"/") + 1;

                string updateApp = MobileConfiguration.Configuration.Settings["UpdateApp"];

                string fileUpdate = string.Format("{0}\\{1}", appPath, updateApp);
                string startAppName = codeBase.Substring(index, codeBase.Length - index);

                if (File.Exists(fileUpdate))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = fileUpdate;
                    proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", appPath.ToLower(), filename, startAppName);
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

        public static void CloseInitial()
        {
            //if (splashForm != null)
            //{
            //    splashForm.Close();
            //    splashForm.Dispose();
            //    splashForm = null;
            //}
        }

    }

}