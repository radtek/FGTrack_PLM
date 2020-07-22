using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class PartyDeliTime : IEquatable<PartyDeliTime>
    {
        public PartyDeliTime()
        {
        }

        #region Variable Member

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _WH_ID;
        private string _ETD_TIME;


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

        #endregion

        #region Method Member

        public bool Equals(PartyDeliTime other)
        {
            if ((this.PARTY_ID == other.PARTY_ID) && (this.WH_ID == other.WH_ID) && (this.ETD_TIME == other.ETD_TIME))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion



    }
}
