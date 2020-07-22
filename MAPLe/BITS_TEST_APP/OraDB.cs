using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTN.BITS.DAL;

namespace BITS_TEST_APP
{
    public sealed class OraDB : IDisposable
    {
        #region IDisposable Members

        void IDisposable.Dispose() { Release(); }

        #endregion

        public void Release()
        {
            IsReleased = true;;
            OraDB.instance = null;
        }

        [ThreadStatic]
        static OraDB instance = null;
        public bool IsReleased { get; private set; }

        private OraDB() 
        {
            IsReleased = false;
        }

        static OraDB()
        {
            instance = new OraDB();
        }

        public static OraDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OraDB();
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
                return this.da;
            }

            set
            {
            	this.da = value;
            }
        }

        public void Init()
        {
            this.da = new OracleDB();
        }

        public void Connect()
        {
            string conStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(COMMUNITY=tcp.world)(PROTOCOL=TCP)(Host=NHTN0003)(Port=1521)))(CONNECT_DATA=(SID=orcl10g)));Persist Security Info=false;
            User ID=DBATIMES2;Password=TIMES2;Connection Lifetime=10;Connection Timeout=60;Enlist=true;Min Pool Size=1;Max Pool Size=10;pooling=true;validate connection=true;Incr Pool Size=10;Decr Pool Size=10;";

            //this.da.ConnectionString = conStr;
            //this.da.Connect(false);

            // OR

            this.da.Connect(conStr, true);

            // Note : The ‘reconnect’ parameter indicates whether any existing connection to be dropped and new connection to be created. 
            // When you specify false and there already an open connection available, then an exception will be generated.
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
