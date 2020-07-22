using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HTN.BITS.BEL
{
    //[Serializable]
    public class Pallet
    {
        public Pallet()
        { 
        }

        #region Variable Member

        private string _SO_NO;
        private string _PALLET_NO;
        private int _PALLET_SEQ;
        private int _PALLET_TOTAL;
        private string _PALLET_STATUS;
        private int _PALLET_BOX;
        private int _PALLET_PCS;

        private string _PARTY_NAME;
        private string _ETA;


        #endregion

        #region Property Merber

        public string SO_NO
        {
            get
            {
                return _SO_NO;
            }
            set
            {
                if (_SO_NO == value)
                    return;
                _SO_NO = value;
            }
        }

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

        public int PALLET_SEQ
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

        public int PALLET_TOTAL
        {
            get
            {
                return _PALLET_TOTAL;
            }
            set
            {
                if (_PALLET_TOTAL == value)
                    return;
                _PALLET_TOTAL = value;
            }
        }

        public string PALLET_STATUS
        {
            get
            {
                return _PALLET_STATUS;
            }
            set
            {
                if (_PALLET_STATUS == value)
                    return;
                _PALLET_STATUS = value;
            }
        }

        public int PALLET_BOX
        {
            get
            {
                return _PALLET_BOX;
            }
            set
            {
                if (_PALLET_BOX == value)
                    return;
                _PALLET_BOX = value;
            }
        }

        public int PALLET_PCS
        {
            get
            {
                return _PALLET_PCS;
            }
            set
            {
                if (_PALLET_PCS == value)
                    return;
                _PALLET_PCS = value;
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

        public string ETA
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

        #endregion
    }

    [Serializable]
    public class PalletList : CollectionBase
    {
        public int Add(Pallet value)
        {
            return base.List.Add(value as object);
        }

        public Pallet this[int index]
        {
            get { return (base.List[index] as Pallet); }
        }

        public void Remove(Pallet value)
        {
            base.List.Remove(value as object);
        }
    }
}
