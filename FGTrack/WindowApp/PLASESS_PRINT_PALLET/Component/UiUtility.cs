using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Configuration;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Globalization;
using System.Xml.Serialization;

namespace HTN.BITS.UIL.PLASESS_PRINT_PALLET.Component
{
    public class UiUtility
    {
        private static string languageUse = "en-GB";//"en-GB"; //"th-TH"

        public static string LanguageUse
        {
            get
            {
                return languageUse;
            }

            set
            {
                languageUse = value;
            }
        }

        public static string ApplicationStyle
        {
            get
            {
                return ConfigurationManager.AppSettings["AppStyle"];
            }
        }

        public static string WH_ID
        {
            get
            {
                return ConfigurationManager.AppSettings["WH_ID"];
            }
        }

        public static string PASSWORD_EXIT
        {
            get
            {
                return ConfigurationManager.AppSettings["PASSWORD"];
            }
        }

        public static bool START_UP
        {
            get
            {
                return (ConfigurationManager.AppSettings["START_UP"].ToLower().Equals("true"));
            }
        }

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

        #region XML Serialization

        public static void SerializeObject<T>(T obj, string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(stream, obj);

                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeserializeObject<T>(string filename)
        {
            T result;
            try
            {
                using (TextReader textReader = new StreamReader(filename))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    result = (T)deserializer.Deserialize(textReader);


                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static void ClearSelection(BaseView baseView)
        {
            switch (baseView.GetType().Name)
            {
                case "BandedGridView":

                    BandedGridView bView = baseView as BandedGridView;
                    bView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                    bView.FocusedRowHandle = GridControl.NewItemRowHandle;
                    bView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    //bView.OptionsSelection.EnableAppearanceFocusedRow = false;
                    bView.ClearSelection();

                    break;
                case "GridView":
                    GridView gView = baseView as GridView;
                    gView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                    gView.FocusedRowHandle = GridControl.NewItemRowHandle;
                    gView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    //gView.OptionsSelection.EnableAppearanceFocusedRow = false;
                    gView.ClearSelection();
                    break;
                default:
                    break;
            }

        }

        public static string RPTViewerIdleTime
        {
            get
            {
                return ConfigurationManager.AppSettings["RPTViewerIdleTime"];
            }
        }

        public static bool IsAppIdleTime
        {
            get
            {
                return (ConfigurationManager.AppSettings["IsAppIdleTime"].ToLower().Equals("true"));
            }
        }

        public static int SizeFormWidth
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["SizeFormWidth"], NumberFormatInfo.CurrentInfo);
            }
        }

        public static int SizeFormHeight
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["SizeFormHeight"], NumberFormatInfo.CurrentInfo);
            }
        }

        public static string StateConfigPath
        {
            get
            {
                return ConfigurationManager.AppSettings["StateConfigPath"];
            }
        }
    }
}
