using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class ProgramGroup
    {
        public ProgramGroup()
        {
        }

        #region "Variable Member"

        private int _PROG_TYPE_ID;
        private string _PROG_TYPE_NAME;
        private int _ORDER_BY;
        private string _ICON;
        private string _DESCRIPTION;
        private string _PROG_TYPE_RESOURCE;
        private bool _IS_ACTIVE;

        #endregion

        #region Property Member

        public int PROG_TYPE_ID
        {
            get
            {
                return _PROG_TYPE_ID;
            }
            set
            {
                if (_PROG_TYPE_ID == value)
                    return;
                _PROG_TYPE_ID = value;
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
        public string PROG_TYPE_RESOURCE
        {
            get
            {
                return _PROG_TYPE_RESOURCE;
            }
            set
            {
                if (_PROG_TYPE_RESOURCE == value)
                    return;
                _PROG_TYPE_RESOURCE = value;
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
