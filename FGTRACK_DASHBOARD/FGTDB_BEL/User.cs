using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class User
    {
        public User()
        {
        }

        #region "Variable Member"

        private int _USER_ID;  
        private string _EMPLOYEE_ID;
        private string _EMPLOYEE_NAME;
        private string _USER_LOGIN;
        private string _USER_PASS;
        private string _USER_REMARK;
        private bool _IS_ONLINE;
        private bool _IS_ACTIVE;

        #endregion

        #region "Property Member"

        public int USER_ID
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

        public string EMPLOYEE_ID
        {
            get
            {
                return _EMPLOYEE_ID;
            }
            set
            {
                if (_EMPLOYEE_ID == value)
                    return;
                _EMPLOYEE_ID = value;
            }
        }

        public string EMPLOYEE_NAME
        {
            get
            {
                return _EMPLOYEE_NAME;
            }
            set
            {
                if (_EMPLOYEE_NAME == value)
                    return;
                _EMPLOYEE_NAME = value;
            }
        }

        public string USER_LOGIN
        {
            get
            {
                return _USER_LOGIN;
            }
            set
            {
                if (_USER_LOGIN == value)
                    return;
                _USER_LOGIN = value;
            }
        }

        public string USER_PASS
        {
            get
            {
                return _USER_PASS;
            }
            set
            {
                if (_USER_PASS == value)
                    return;
                _USER_PASS = value;
            }
        }

        public string USER_REMARK
        {
            get
            {
                return _USER_REMARK;
            }
            set
            {
                if (_USER_REMARK == value)
                    return;
                _USER_REMARK = value;
            }
        }

        public bool IS_ONLINE
        {
            get
            {
                return _IS_ONLINE;
            }
            set
            {
                if (_IS_ONLINE == value)
                    return;
                _IS_ONLINE = value;
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

        #endregion
    }
}
