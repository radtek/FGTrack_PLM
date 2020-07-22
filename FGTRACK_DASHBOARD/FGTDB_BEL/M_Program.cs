using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class M_Program
    {
        public M_Program()
        {
            this._PROG_GROUP = new ProgramGroup();
        }

        #region "Variable Member"

        private int _PROG_ID;
        private string _PROG_NAME;
        private string _PROG_KEY;
        private ProgramGroup _PROG_GROUP;
        private int? _ORDER_BY;
        private string _ICON;
        private bool _IS_ACTIVE;
        private string _DESCRIPTION;
        private string _PROG_RESOURCE;

        private string _PROG_TYPE_ICON;

        #endregion

        #region "Property Member"

        public int PROG_ID
        {
            get
            {
                return _PROG_ID;
            }
            set
            {
                if (_PROG_ID == value)
                    return;
                _PROG_ID = value;
            }
        }
        public string PROG_NAME
        {
            get
            {
                return _PROG_NAME;
            }
            set
            {
                if (_PROG_NAME == value)
                    return;
                _PROG_NAME = value;
            }
        }
        public string PROG_KEY
        {
            get
            {
                return _PROG_KEY;
            }
            set
            {
                if (_PROG_KEY == value)
                    return;
                _PROG_KEY = value;
            }
        }
        public ProgramGroup PROG_GROUP
        {
            get
            {
                return _PROG_GROUP;
            }
            set
            {
                if (_PROG_GROUP == value)
                    return;
                _PROG_GROUP = value;
            }
        }
        public int? ORDER_BY
        {
            get
            {
                return _ORDER_BY;
            }
            set
            {
                if (_ORDER_BY == value)
                    return;
                _ORDER_BY = value;
            }
        }
        public string ICON
        {
            get
            {
                return _ICON;
            }
            set
            {
                if (_ICON == value)
                    return;
                _ICON = value;
            }
        }
        public bool IS_ACTIVE 
        {
            get
            {
                return _IS_ACTIVE;
            }
            set
            {
                if (_IS_ACTIVE == value)
                    return;
                _IS_ACTIVE = value;
            }
        }
        public string DESCRIPTION
        {
            get
            {
                return _DESCRIPTION;
            }
            set
            {
                if (_DESCRIPTION == value)
                    return;
                _DESCRIPTION = value;
            }
        }
        public string PROG_RESOURCE
        {
            get
            {
                return _PROG_RESOURCE;
            }
            set
            {
                if (_PROG_RESOURCE == value)
                    return;
                _PROG_RESOURCE = value;
            }
        }

        public string PROG_TYPE_ICON
        {
            get
            {
                return _PROG_TYPE_ICON;
            }
            set
            {
                if (_PROG_TYPE_ICON == value)
                    return;
                _PROG_TYPE_ICON = value;
            }
        }

        #endregion
    }
}
