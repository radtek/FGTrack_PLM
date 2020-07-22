using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class USR_LOGIN
    {
        #region Variable Member

        private string _USER_ID;       
        private string _USER_NAME;
        private string _USER_ROLE;

        #endregion

        #region Property Member

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
        public string USER_ROLE
        {
            get
            {
                return _USER_ROLE;
            }
            set
            {
                if (_USER_ROLE == value)
                    return;
                _USER_ROLE = value;
            }
        }
       
        #endregion
    }
}
