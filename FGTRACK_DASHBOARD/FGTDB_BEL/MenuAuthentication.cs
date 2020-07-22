using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class MenuAuthentication
    {
        private int _PROG_TYPE_ID;
        private string _PROG_TYPE_NAME;
        private string _PG_ICON;
        private string _PROG_TYPE_RESOURCE;
        private int _PROG_ID;
        private string _PROG_NAME;
        private string _ICON;
        private string _PROG_KEY;
        private string _PROG_RESOURCE;

        public MenuAuthentication()
        {
        }

        public int PROG_TYPE_ID
        {
            get
            {
                return this._PROG_TYPE_ID;
            }

            set
            {
                if (this._PROG_TYPE_ID == value)
                    return;
                this._PROG_TYPE_ID = value;
            }
        }

        public string PROG_TYPE_NAME
        {
            get
            {
                return this._PROG_TYPE_NAME;
            }

            set
            {
                if (this._PROG_TYPE_NAME == value)
                    return;
                this._PROG_TYPE_NAME = value;
            }
        }

        public string PG_ICON
        {
            get
            {
                return this._PG_ICON;
            }

            set
            {
                if (this._PG_ICON == value)
                    return;
                this._PG_ICON = value;
            }
        }

        public string PROG_TYPE_RESOURCE
        {
            get
            {
                return this._PROG_TYPE_RESOURCE;
            }
            set
            {
                if (this._PROG_TYPE_RESOURCE == value)
                    return;
                this._PROG_TYPE_RESOURCE = value;
                
            }
        }

        public int PROG_ID
        {
            get
            {
                return this._PROG_ID;
            }

            set
            {
                if (this._PROG_ID == value)
                    return;
                this._PROG_ID = value;
            }
        }

        public string PROG_NAME
        {
            get
            {
                return this._PROG_NAME;
            }

            set
            {
                if (this._PROG_NAME == value)
                    return;
                this._PROG_NAME = value;
            }
        }

        public string ICON
        {
            get
            {
                return this._ICON;
            }

            set
            {
                if (this._ICON == value)
                    return;
                this._ICON = value;
            }
        }

        public string PROG_KEY
        {
            get
            {
                return this._PROG_KEY;
            }

            set
            {
                if (this._PROG_KEY == value)
                    return;
                this._PROG_KEY = value;
            }
        }

        public string PROG_RESOURCE
        {
            get
            {
                return this._PROG_RESOURCE;
            }

            set
            {
                if (this._PROG_RESOURCE == value)
                    return;
                this._PROG_RESOURCE = value;
            }
        }
    }
}
