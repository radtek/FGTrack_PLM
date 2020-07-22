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

namespace HTN.BITS.MCS.SCN.BLL
{
    public class ReplenishBLL : IDisposable
    {

        public ReplenishBLL()
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

        ~ReplenishBLL()
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

        public ResponseResult CheckJobOrder(string jobNo)
        {
            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Replenish/CheckJobOrder?jobno={1}"
                    , this.BASE_URI
                    , jobNo
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

        public ResponseResult CheckMachine(string machineNO)
        {
            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Replenish/CheckMachine?mcno={1}"
                    , this.BASE_URI
                    , machineNO
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
        
        public ResponseResult StartReplenish(string jobno, string mcno, int noOfBag, string userid)
        {
            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Replenish/StartReplenish?jobno={1}&mcno={2}&noOfBag={3}&userid={4}"
                    , this.BASE_URI
                    , jobno
                    , mcno
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

        public ResponseResult ScanRepLabel(string serialno, string repno, string jobno, string mcno, string userid)
        {

            ResponseResult res = new ResponseResult();

            try
            {
                string uri = string.Format("{0}api/Replenish/ScanRepLabel?serialno={1}&repno={2}&jobno={3}&mcno={4}&userid={5}"
                    , this.BASE_URI
                    , serialno
                    , repno
                    , jobno
                    , mcno
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
        #endregion
    }
}