//using DevExpress.XtraGrid;
//using DevExpress.XtraGrid.Views.BandedGrid;
//using DevExpress.XtraGrid.Views.Base;
//using DevExpress.XtraGrid.Views.Grid;
//using HTN.BITS.MCS.SCN.BEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public class UiUtility
    {

        private static string languageUse = "en";//"en-GB"; //"th-TH"
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

        private static string userrole = "V";
        public static string USERROLE
        {
            get
            {
                return userrole;
            }

            set
            {
                userrole = value;
            }
        }

        private static string branchcode = "BKK";
        public static string BRANCHCODE
        {
            get
            {
                return branchcode;
            }

            set
            {
                branchcode = value;
            }
        }

        private static string userclass = "V";
        public static string USERCLASS
        {
            get
            {
                return userclass;
            }

            set
            {
                userclass = value;
            }
        }

        private static string userid = "user1";
        public static string USERID
        {
            get
            {
                return userid;
            }

            set
            {
                userid = value;
            }
        }

        private static string custcode = "customer1";
        public static string CUSTCODE
        {
            get
            {
                return custcode;
            }

            set
            {
                custcode = value;
            }
        }

        private static string custname = "customer1";
        public static string CUSTNAME
        {
            get
            {
                return custname;
            }

            set
            {
                custname = value;
            }
        }

        private static string name = "Login Name";
        public static string NAME
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private static string username = "User Name";
        public static string USERNAME
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        private static string pass = "Login Password";
        public static string PASSWORD
        {
            get
            {
                return pass;
            }
            set
            {
                pass = value;
            }
        }

      

        


        #region System Configuration

        public static string ApiServerUrl
        {
            get
            {
                return "http://nhtnbkdv1/BPRO_SCN_API/";
                //return "http://localhost:51709/";
               // return Decrypt(ConfigurationManager.AppSettings["ApiServerUrl"]);
            }
        }
        
        public static int MaxOpenForm
        {
            get
            {
               // return Convert.ToInt32(ConfigurationManager.AppSettings["MaxOpenForm"], NumberFormatInfo.CurrentInfo);
                return Convert.ToInt32(10);
            }
        }

        #endregion


     
    }
}
