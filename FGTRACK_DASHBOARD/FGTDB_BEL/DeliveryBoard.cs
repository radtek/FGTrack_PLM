using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class DeliveryBoard
    {
         public DeliveryBoard()
        { }

        #region Variable Member

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _WH_ID;
        private DateTime _ETD_DATE;
        private string _ETD_TIME;
        private string _STATUS;
        private string _RESPONSIBLE;
        private int _FLAG;
       
        #endregion

        #region Property Member

        public string PARTY_ID
        {
            get
            {
                return _PARTY_ID;
            }
            set
            {
                if (_PARTY_ID == value)
                    return;
                _PARTY_ID = value;
            }
        }
        public string PARTY_NAME
        {
            get
            {
                return _PARTY_NAME;
            }
            set
            {
                if (_PARTY_NAME == value)
                    return;
                _PARTY_NAME = value;
            }
        }
        public string WH_ID
        {
            get
            {
                return _WH_ID;
            }
            set
            {
                if (_WH_ID == value)
                    return;
                _WH_ID = value;
            }
        }
        public DateTime ETD_DATE
        {
            get
            {
                return _ETD_DATE;
            }
            set
            {
                if (_ETD_DATE == value)
                    return;
                _ETD_DATE = value;
            }
        }
        public string ETD_TIME
        {
            get
            {
                return _ETD_TIME;
            }
            set
            {
                if (_ETD_TIME == value)
                    return;
                _ETD_TIME = value;
            }
        }
        public string STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                if (_STATUS == value)
                    return;
                _STATUS = value;
            }
        }
        public string RESPONSIBLE
        {
            get
            {
                return _RESPONSIBLE;
            }
            set
            {
                if (_RESPONSIBLE == value)
                    return;
                _RESPONSIBLE = value;
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
