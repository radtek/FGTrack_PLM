using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class Pallet
    {
        public Pallet()
        {
        }

        #region "Private Member"

        
        private string _PALLET_NO;
        private string _PALLET_SEQ;
        private DateTime _ETA;
        private string _PARTY_NAME;

        #endregion

        #region "Property Member"


        public string PALLET_NO
        {
            get
            {
                return _PALLET_NO;
            }
            set
            {
                if (_PALLET_NO == value)
                    return;
                _PALLET_NO = value;
            }
        }

        public string PALLET_SEQ
        {
            get
            {
                return _PALLET_SEQ;
            }
            set
            {
                if (_PALLET_SEQ == value)
                    return;
                _PALLET_SEQ = value;
            }
        }

        public DateTime ETA
        {
            get
            {
                return _ETA;
            }
            set
            {
                if (_ETA == value)
                    return;
                _ETA = value;
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

        #endregion
    }
}
