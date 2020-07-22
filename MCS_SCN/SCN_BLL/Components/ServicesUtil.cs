using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HTN.BITS.MCS.SCN.BLL.Components
{
    public class ServicesUtil
    {
        public static HttpWebRequest ApiRequest(string uri, string method, string userAuth, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(userAuth))
            {
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userAuth)));
            }

            request.Method = method;

            request.Timeout = timeout;

            return request;
        }
    }
}
