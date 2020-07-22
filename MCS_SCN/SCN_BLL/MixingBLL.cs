using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HTN.BITS.MCS.SCN.BLL.Components;
using System.Globalization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using HTN.BITS.MCS.SCN.BEL;
using System.Text.RegularExpressions;

namespace HTN.BITS.MCS.SCN.BLL
{
    public class MixingBLL : IDisposable
    {

        public MixingBLL()
        {
            this.BASE_URI = MobileConfiguration.Configuration.Settings["ServiceURL"].ToString();
            this.TIMEOUT = int.Parse(MobileConfiguration.Configuration.Settings["ServiceTimeOut"].ToString(), NumberStyles.AllowThousands);
            this.USER_AUTH = MobileConfiguration.Configuration.Settings["UserAuthen"].ToString();
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

        ~MixingBLL()
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

        public ResponseResult StartMixing(decimal percen, int noOfBag, string userid)
        {
            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Mixed/StartMixing?percen={1}&noOfBag={2}&userid={3}"
                    , this.BASE_URI
                    , percen
                    , noOfBag
                    , userid
                    );

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                //request.ContentType = "application/json";
                //request.Method = "GET";

                //request.Timeout = this.TIMEOUT;
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
                    res = JsonConvert.DeserializeObject<ResponseResult>(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public ResponseResult ScanMixingLabel(string serialno, string mixedno, string mtlCode, string userid)
        {

            ResponseResult res = new ResponseResult();

            try
            {
                var replacements = new Dictionary<string, string>()
                {
                   {"(plus)","+"},
                   {"(percen)","%"}
                };

                foreach (string key in replacements.Keys)
                {
                    mtlCode = mtlCode.Replace(key, replacements.FirstOrDefault(v => v.Key == key).Value);            
                }


                string uri = string.Format("{0}api/Mixed/ScanMixingLabel?serialno={1}&mixedno={2}&mtlCode={3}&userid={4}"
                    , this.BASE_URI
                    , serialno
                    , mixedno
                    , mtlCode
                    , userid
                    );

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                //request.ContentType = "application/json";
                //request.Method = "GET";

                //request.Timeout = this.TIMEOUT;
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
                    res = JsonConvert.DeserializeObject<ResponseResult>(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public ResponseResult UpdateMixedQty(string serialno, string mixedno, decimal mixQty)
        {

            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Mixed/UpdateMixedQty?serialno={1}&mixedno={2}&mixQty={3}"
                    , this.BASE_URI
                    , serialno
                    , mixedno
                    , mixQty
                    );

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                //request.ContentType = "application/json";
                //request.Method = "GET";

                //request.Timeout = this.TIMEOUT;
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
                    res = JsonConvert.DeserializeObject<ResponseResult>(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion
    }
}