﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace HTN.BITS.FGTRACK.MTSTTAMPO.Components
{
    public sealed class ServiceProvider : IDisposable
    {
        static int serviceTimeout = 3000;
        static string serviceURL = string.Empty;
        static ServiceProvider instance = null;
        static readonly object locker = new object();

        ServiceRef.Service_MtstTampo webService = null;

        ServiceProvider()
        {
            serviceTimeout = int.Parse(MobileConfiguration.Configuration.Settings["ServiceTimeOut"].ToString(), NumberStyles.AllowThousands);
            serviceURL = string.Format("{0}?Number={1}", MobileConfiguration.Configuration.Settings["ServiceURL"].ToString(), GetInfiniteRandomNumbers());
            //serviceURL = MobileConfiguration.Configuration.Settings["ServiceURL"].ToString();
            webService = new ServiceRef.Service_MtstTampo();
            if (!string.IsNullOrEmpty(serviceURL))
            {
                webService.Url = serviceURL;
                webService.Timeout = serviceTimeout;
                //webService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }
        }

        public static ServiceProvider Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new ServiceProvider();
                    }

                    return instance;
                }
            }
        }

        public ServiceRef.Service_MtstTampo Proxy
        {
            get
            {
                return webService;
            }
        }

        private int GetInfiniteRandomNumbers()
        {
            var rand = new Random();
            return rand.Next(9999);
        }

        public void Connect()
        {
            webService.HelloWorld();
        }

        public void Disconnect()
        {
            Release();
        }

        private void Release()
        {
            if (webService != null)
            {
                webService.Dispose();
                webService = null;
            }

            GC.SuppressFinalize(this);
        }

        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion
    }
}
