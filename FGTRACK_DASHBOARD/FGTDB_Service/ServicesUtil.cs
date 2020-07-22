using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Net;

namespace FGTDB_Service
{
    public static class ServicesUtil
    {
        #region ODP.NET Assembly

        public static string GetOracleDataAccessPath()
        {
            RegistryKey rgkLM = Registry.LocalMachine;
            string rgkPath = string.Format(@"SOFTWARE\ORACLE\ODP.NET\{0}", ConfigurationManager.AppSettings["ODPNetVersion"]);
            RegistryKey odpnet = rgkLM.OpenSubKey(rgkPath);

            string oracleDataAccPath = odpnet.GetValue("DllPath").ToString().Replace("bin", @"odp.net\bin\2.x\Oracle.DataAccess.dll");

            if (File.Exists(oracleDataAccPath))
            {
                return oracleDataAccPath;
            }
            else
            {
                return "Oracle.DataAccess.dll";
            }
        }

        #endregion

        #region Service Provider

        public static string AppService
        {
            get
            {
                return ConfigurationManager.AppSettings["AppService"];
            }
        }

        public static int ServiceTimeOut
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ServiceTimeOut"], NumberFormatInfo.CurrentInfo);
            }
        }

        public static bool IsServiceAvariable(string url, out string resultMessage)
        {
            bool resultService = false;

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 10000;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    resultService = (response.StatusCode == HttpStatusCode.OK);
                    resultMessage = response.StatusDescription;
                }
            }
            catch (WebException wex)
            {
                resultService = false;
                resultMessage = wex.Message;
            }
            catch (Exception ex)
            {
                resultService = false;
                resultMessage = ex.Message;
            }

            return resultService;
        }

        #endregion

        #region System Configuration

        public static string AutoRefreshTime
        {
            get
            {
                return ConfigurationManager.AppSettings["AutoRefreshTime"];
            }
        }

        public static string AppServiceManager
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceManager"];
            }
        }

        #endregion


    }
}
