using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class M_Program
    {
        public M_Program()
        {
        }

        #region "Variable Member"

        private string _PROG_ID;
        private string _PROG_NAME;
        private string _PROG_TYPE;
        private int? _ORDER_BY;
        private string _ICON;
        private bool _REC_STAT;
        private string _DESCRIPTION;
        private string _PROG_KEY;

        #endregion

        #region "Property Member"

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
        public string PROG_TYPE
        {
            get
            {
                return _PROG_TYPE;
            }
            set
            {
                if (_PROG_TYPE == value)
                    return;
                _PROG_TYPE = value;
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

        #endregion
    }
}
