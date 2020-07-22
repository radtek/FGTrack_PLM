using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.SQLITE.DAL;
using System.Configuration;
using System.Windows.Forms;

namespace HTN.BITS.SQLITE.BLL
{
    public class GlobalSqliteDB : IDisposable
    {
        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion

        public void Release()
        {
            IsReleased = true; ;
            GlobalSqliteDB.instance = null;
        }

        [ThreadStatic]
        static GlobalSqliteDB instance = null;

        public bool IsReleased { get; private set; }


        private GlobalSqliteDB()
        {
            IsReleased = false;
        }

        static GlobalSqliteDB()
        {
            instance = new GlobalSqliteDB();
        }

        public static GlobalSqliteDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalSqliteDB();
                }

                return instance;
            }
        }

        private SQLiteDataAccess da = null;
        public SQLiteDataAccess DataAc
        {
            get
            {
                if (this.da == null)
                {
                    this.da = new SQLiteDB();
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

        private bool IsConnecting()
        {
            if (this.da == null) return false;

            try
            {
                return this.da.IsConnected;
            }
            catch
            {
                return false;
            }
        }

        public void Init()
        {
            this.da = new SQLiteDB();
        }

        public void Connect()
        {
            //string dbPath = string.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["DBPath"]);
            string dbPath = ConfigurationManager.AppSettings["DBPath"];
            string dbname = ConfigurationManager.AppSettings["DBName"];
            string dbversion = ConfigurationManager.AppSettings["DBVersion"];
            bool isLocal = ConfigurationManager.AppSettings["IsLocal"].ToLower().Equals("true");

            string filePath = string.Format("{0}\\{1}", dbPath, dbname);

            if (isLocal)
            {
                dbPath = string.Format("{0}\\{1}", Application.StartupPath, dbPath);
            }



            string conStr = string.Format(ConfigurationManager.ConnectionStrings["FGTDB_SQLITE.ConnectString"].ConnectionString, dbPath, dbname, dbversion);

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
