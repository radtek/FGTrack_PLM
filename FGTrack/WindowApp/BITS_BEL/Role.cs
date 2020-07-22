using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class Role
    {
        public Role()
        {
        }

        #region "Variable Member"

        private string _ROLE_ID;
        private string _ROLE_NAME;
        private bool _REC_STAT;
        private int _FLAG;

        #endregion

        #region "Property Member"

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
        public string ROLE_NAME
        {
            get
            {
                return _ROLE_NAME;
            }
            set
            {
                if (_ROLE_NAME == value)
                    return;
                _ROLE_NAME = value;
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
