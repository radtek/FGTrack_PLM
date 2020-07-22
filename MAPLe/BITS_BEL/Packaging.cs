using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class Packaging
    {
        public Packaging()
        {
        }

        #region "Variable Member"

        private string _PACKAGE_ID;
        private string _PACKAGE_NAME;

        #endregion

        #region "Property Member"

        public string PACKAGE_ID
        {
            get
            {
                return _PACKAGE_ID;
            }
            set
            {
                if (_PACKAGE_ID == value)
                    return;
                _PACKAGE_ID = value;
            }
        }
        public string PACKAGE_NAME
        {
            get
            {
                return _PACKAGE_NAME;
            }
            set
            {
                if (_PACKAGE_NAME == value)
                    return;
                _PACKAGE_NAME = value;
            }
        }

        #endregion
    }
}
