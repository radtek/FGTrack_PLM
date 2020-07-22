using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class RoleProgram
    {
        public RoleProgram()
        {
        }

        #region "Variable Member"

        private string _ROLE_ID;
        private string _PROG_ID;
        private string _REP_ID;
        private string _PROG_NAME;
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
        public string PROG_ID
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
        public string REP_ID
        {
            get
            {
                return _REP_ID;
            }
            set
            {
                if (_REP_ID == value)
                    return;
                _REP_ID = value;
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
