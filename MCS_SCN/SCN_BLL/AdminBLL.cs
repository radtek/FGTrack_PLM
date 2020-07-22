using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HTN.BITS.MCS.SCN.BEL;
using System.Net;
using System.IO;
using HTN.BITS.MCS.SCN.BLL.Components;
using Newtonsoft.Json;
using System.Globalization;

namespace HTN.BITS.MCS.SCN.BLL
{
    public class AdminBLL : IDisposable
    {
        public AdminBLL()
        {
            this.BASE_URI = MobileConfiguration.Configuration.Settings["ServiceURL"].ToString();
            this.TIMEOUT = int.Parse(MobileConfiguration.Configuration.Settings["ServiceTimeOut"].ToString(), NumberStyles.AllowThousands);
            this.USER_AUTH = MobileConfiguration.Configuration.Settings["UserAuthen"].ToString();

            //this.BASE_URI = "http://10.211.101.7:2016/";
            //this.TIMEOUT = 5000;
            //this.USER_AUTH = "JACK:pa$$";
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    //this.CloseConnection();
                }
            }

            IsDisposed = true;
        }

        ~AdminBLL()
        {
            this.Dispose(false);
        }

        #endregion

        #region Private Member

        private bool IsDisposed = false;
        private string _base_uri;
        private int _timeout;
        private string _USER_AUTH;

        #endregion

        #region Property Member

        public string BASE_URI
        {
            get
            {
                return _base_uri;
            }
            set
            {
                if (_base_uri == value)
                    return;
                _base_uri = value;
            }
        }

        public int TIMEOUT
        {
            get
            {
                return _timeout;
            }
            set
            {
                if (_timeout == value)
                    return;
                _timeout = value;
            }
        }

        public string USER_AUTH
        {
            get
            {
                return _USER_AUTH;
            }
            set
            {
                if (_USER_AUTH == value)
                    return;
                _USER_AUTH = value;
            }
        }

        #endregion

        #region Method Member

        public string CheckLogin(string userid, string ipaddress, string logintype)
        {
            string result = String.Empty;

            string uri = string.Format("{0}api/Authentication/CheckValidationUser?userid={1}&ipaddress={2}&serialno={3}&scanversion={4}"
                , this.BASE_URI
                , userid
                , ipaddress
                , "testserial"
                , "1.0.0.1"
                );


            HttpWebRequest request = ServicesUtil.ApiRequest(uri, "GET", this.USER_AUTH, this.TIMEOUT);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            string jsonString = null;

            using (StreamReader reader = new StreamReader(responseStream))
            {
                jsonString = reader.ReadToEnd();
                reader.Close();
            }


            if (!string.IsNullOrEmpty(jsonString))
            {
                result = JsonConvert.DeserializeObject<String>(jsonString);
            }

            return result;
        }


        public string CheckVersion(string curVersion)
        {
            string result = String.Empty;

            try
            {
                string uri = string.Format("{0}api/AutoUpdate/GetLatestVersion?curVersion={1}"
                , this.BASE_URI
                , curVersion
                );


                HttpWebRequest request = ServicesUtil.ApiRequest(uri, "GET", this.USER_AUTH, this.TIMEOUT);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                string jsonString = null;

                using (StreamReader reader = new StreamReader(responseStream))
                {
                    jsonString = reader.ReadToEnd();
                    reader.Close();
                }

                if (!string.IsNullOrEmpty(jsonString))
                {
                    result = JsonConvert.DeserializeObject<String>(jsonString);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

    }
}
