using System;
using SoapExtensionLib;
using System.Net;

namespace HTN.BITS.SCN.LOCKDOWN.ServiceRef
{
    public partial class Service_Center : IProxyProgressExtension
    {
        private WebResponse m_response;
        private string m_requestGuid;

        public ProgressDelegate progressDelegate;

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            m_response = base.GetWebResponse(request);
            return m_response;
        }

        #region IProxyProgressExtension Members

        string IProxyProgressExtension.RequestGuid
        {
            get
            {
                return m_requestGuid;
            }
            set
            {
                m_requestGuid = value;
            }
        }

        long IProxyProgressExtension.ResponseContentLength
        {
            get { return m_response.ContentLength; }
        }

        ProgressDelegate IProxyProgressExtension.Callback
        {
            get { return progressDelegate; }
        }

        #endregion
    }
}
