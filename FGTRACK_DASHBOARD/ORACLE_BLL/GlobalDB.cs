using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.ORACLE.DAL;
using System.Configuration;

namespace HTN.BITS.ORACLE.BLL
{
    public class GlobalDB : IDisposable
    {
        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion

        public void Release()
        {
            IsReleased = true; ;
            GlobalDB.instance = null;
        }

        [ThreadStatic]
        static GlobalDB instance = null;
        public bool IsReleased { get; private set; }

        private GlobalDB()
        {
            IsReleased = false;
        }

        static GlobalDB()
        {
            instance = new GlobalDB();
        }

        public static GlobalDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalDB();
                }

                return instance;
            }
        }

        private ODPDataAccess da = null;
        public ODPDataAccess DataAc
        {
            get
            {
                if (this.da == null)
                {
                    this.da = new OracleDB();
                    this.Connect();
                }

                if (!IsConnecting())
                {
                    this.da.Disconnect();
                    this.Release();
                    this.Init();
                    this.Connect();
                }

                return this.da;
            }

            set
            {
                this.da = value;
            }
        }

        private string _ConnectionKey = string.Empty;
        public string SetConnectionKey
        {
            set
            {
                this._ConnectionKey = value;
            }
        }

        private bool IsConnecting()
        {
            if (this.da == null) return false;

            try
            {
                this.da.ExecuteNonQuery("SELECT * FROM DUAL");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Init()
        {
            this.da = new OracleDB();
        }

        public void Connect()
        {
            string conStr = ConfigurationManager.ConnectionStrings["FGTDB_ORACLE.ConnectString"].ConnectionString;

            bool isConnect = this.da.Connect(conStr, true);

            if (!isConnect)
            {
                throw this.da.LastException;
            }
        }

        public void Disconenct()
        {
            if (this.da != null)
            {
                this.da.Disconnect();
            }
        }
    }
}
