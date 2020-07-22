using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ProgramType
    {
        public ProgramType()
        {
        }

        #region "Variable Member"

        private string _PROG_TYPE;
        private string _PROG_TYPE_NAME;
        private bool _REC_STAT;
        private int _ORDER_BY;
        private string _ICON;

        #endregion

        #region "Property Member"

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
        public int ORDER_BY
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

        #endregion
    }
}
