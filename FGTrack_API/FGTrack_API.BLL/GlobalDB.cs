using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HTN.BITS.FGTRACK.DAL;

namespace HTN.BITS.FGTRACK.BLL
{
    public class GlobalDB : IDisposable
    {
        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion

        public void Release()
        {
            IsReleased = true;
        }

        private static readonly Lazy<GlobalDB> lazy = new Lazy<GlobalDB>(() => new GlobalDB());

        public static GlobalDB Instance
        {
            get { return lazy.Value; }
        }

        public bool IsReleased { get; private set; }

        private GlobalDB()
        {
            IsReleased = false;
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

        private string _ConnectionKey = String.Empty;
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
                this.da.CheckConnectDB("Select 1 from dual");

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

            string conStr = ConfigurationManager.ConnectionStrings["FGTrack_API.ConnectionString"].ConnectionString;

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

        public Exception LastException
        {
            get { return this.da.LastException; }
        }

    }
}
