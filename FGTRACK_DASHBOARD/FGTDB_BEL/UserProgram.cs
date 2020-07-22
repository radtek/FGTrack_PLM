using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class UserProgram
    {
        public UserProgram()
        {
        }

        #region "Variable Member"

        private int _USER_PROG_ID;
        private string _PG_ICON;
        private string _PROG_TYPE_NAME;
        private string _ICON;
        private int _PROG_ID;
        private string _PROG_NAME;
        private bool _IS_ACCESS;
        private bool _IS_INSERT;
        private bool _IS_UPDATE;
        private bool _IS_DELETE;
        private bool _IS_PRINT;

        private string _CUSTOM1;
        private bool _IS_CUSTOM1;
        private string _CUSTOM2;
        private bool _IS_CUSTOM2;
        private string _CUSTOM3;
        private bool _IS_CUSTOM3;

        private string _USER_ID;

        private int _FLAG;

        #endregion

        #region "Property Member"

        public int USER_PROG_ID
        {
            get
            {
                return _USER_PROG_ID;
            }
            set
            {
                if (_USER_PROG_ID == value)
                    return;
                _USER_PROG_ID = value;
            }
        }
        public string PG_ICON
        {
            get
            {
                return _PG_ICON;
            }
            set
            {
                if (_PG_ICON == value)
                    return;
                _PG_ICON = value;
            }
        }
        public string PROG_TYPE_NAME
        {
            get
            {
                return _PROG_TYPE_NAME;
            }
            set
            {
                if (_PROG_TYPE_NAME == value)
                    return;
                _PROG_TYPE_NAME = value;
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
        public bool IS_ACCESS
        {
            get
            {
                return _IS_ACCESS;
            }
            set
            {
                if (_IS_ACCESS == value)
                    return;
                _IS_ACCESS = value;
            }
        }
        public bool IS_INSERT
        {
            get
            {
                return _IS_INSERT;
            }
            set
            {
                if (_IS_INSERT == value)
                    return;
                _IS_INSERT = value;
            }
        }
        public bool IS_UPDATE
        {
            get
            {
                return _IS_UPDATE;
            }
            set
            {
                if (_IS_UPDATE == value)
                    return;
                _IS_UPDATE = value;
            }
        }
        public bool IS_DELETE
        {
            get
            {
                return _IS_DELETE;
            }
            set
            {
                if (_IS_DELETE == value)
                    return;
                _IS_DELETE = value;
            }
        }
        public bool IS_PRINT
        {
            get
            {
                return _IS_PRINT;
            }
            set
            {
                if (_IS_PRINT == value)
                    return;
                _IS_PRINT = value;
            }
        }
        public string CUSTOM1
        {
            get
            {
                return _CUSTOM1;
            }
            set
            {
                if (_CUSTOM1 == value)
                    return;
                _CUSTOM1 = value;
            }
        }
        public bool IS_CUSTOM1
        {
            get
            {
                return _IS_CUSTOM1;
            }
            set
            {
                if (_IS_CUSTOM1 == value)
                    return;
                _IS_CUSTOM1 = value;
            }
        }
        public string CUSTOM2
        {
            get
            {
                return _CUSTOM2;
            }
            set
            {
                if (_CUSTOM2 == value)
                    return;
                _CUSTOM2 = value;
            }
        }
        public bool IS_CUSTOM2
        {
            get
            {
                return _IS_CUSTOM2;
            }
            set
            {
                if (_IS_CUSTOM2 == value)
                    return;
                _IS_CUSTOM2 = value;
            }
        }
        public string CUSTOM3
        {
            get
            {
                return _CUSTOM3;
            }
            set
            {
                if (_CUSTOM3 == value)
                    return;
                _CUSTOM3 = value;
            }
        }
        public bool IS_CUSTOM3
        {
            get
            {
                return _IS_CUSTOM3;
            }
            set
            {
                if (_IS_CUSTOM3 == value)
                    return;
                _IS_CUSTOM3 = value;
            }
        }
        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }
        public int FLAG
        {
            get
            {
                return _FLAG;
            }
            set
            {
                if (_FLAG == value)
                    return;
                _FLAG = value;
            }
        }

        #endregion
    }
}
