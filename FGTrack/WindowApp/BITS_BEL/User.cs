using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class User
    {
        public User()
        {
        }

        #region "Variable Member"

        private string _USER_ID;
        private string _USER_NAME;
        private string _WARE_ID;
        private string _LOGIN;
        private string _PWD;
        private string _EMPLOYEE_ID;
        private string _ROLE_ID;
        private string _EMAIL;
        private string _COMP_ID;
        private string _REMARK;
        private bool _REC_STAT;

        #endregion

        #region "Property Member"

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
        public string USER_NAME
        {
            get
            {
                return _USER_NAME;
            }
            set
            {
                if (_USER_NAME == value)
                    return;
                _USER_NAME = value;
            }
        }
        public string WARE_ID
        {
            get
            {
                return _WARE_ID;
            }
            set
            {
                if (_WARE_ID == value)
                    return;
                _WARE_ID = value;
            }
        }
        public string LOGIN
        {
            get
            {
                return _LOGIN;
            }
            set
            {
                if (_LOGIN == value)
                    return;
                _LOGIN = value;
            }
        }
        public string PWD
        {
            get
            {
                return _PWD;
            }
            set
            {
                if (_PWD == value)
                    return;
                _PWD = value;
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
        public string ROLE_ID
        {
            get
            {
                return _ROLE_ID;
            }
            set
            {
                if (_ROLE_ID == value)
                    return;
                _ROLE_ID = value;
            }
        }
        public string EMAIL
        {
            get
            {
                return _EMAIL;
            }
            set
            {
                if (_EMAIL == value)
                    return;
                _EMAIL = value;
            }
        }
        public string COMP_ID
        {
            get
            {
                return _COMP_ID;
            }
            set
            {
                if (_COMP_ID == value)
                    return;
                _COMP_ID = value;
            }
        }
        public string REMARK
        {
            get
            {
                return _REMARK;
            }
            set
            {
                if (_REMARK == value)
                    return;
                _REMARK = value;
            }
        }
        public bool REC_STAT
        {
            get
            {
                return _REC_STAT;
            }
            set
            {
                if (_REC_STAT == value)
                    return;
                _REC_STAT = value;
            }
        }


        #endregion
    }
}
