using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class Party
    {
        public Party()
        {
        }

        #region "Private Member"

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _PARTY_TYPE;
        private string _ADD1;
        private string _ADD2;
        private string _ADD3;
        private string _ADD4;
        private string _TEL;
        private string _FAX;
        private string _EMAIL;
        private string _PIC;
        private string _REMARK;
        private bool _REC_STAT;

        #endregion

        #region "Property Member"

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
        public string PARTY_TYPE
        {
            get
            {
                return _PARTY_TYPE;
            }
            set
            {
                if (_PARTY_TYPE == value)
                    return;
                _PARTY_TYPE = value;
            }
        }
        public string ADD1
        {
            get
            {
                return _ADD1;
            }
            set
            {
                if (_ADD1 == value)
                    return;
                _ADD1 = value;
            }
        }
        public string ADD2
        {
            get
            {
                return _ADD2;
            }
            set
            {
                if (_ADD2 == value)
                    return;
                _ADD2 = value;
            }
        }
        public string ADD3
        {
            get
            {
                return _ADD3;
            }
            set
            {
                if (_ADD3 == value)
                    return;
                _ADD3 = value;
            }
        }
        public string ADD4
        {
            get
            {
                return _ADD4;
            }
            set
            {
                if (_ADD4 == value)
                    return;
                _ADD4 = value;
            }
        }
        public string TEL
        {
            get
            {
                return _TEL;
            }
            set
            {
                if (_TEL == value)
                    return;
                _TEL = value;
            }
        }
        public string FAX
        {
            get
            {
                return _FAX;
            }
            set
            {
                if (_FAX == value)
                    return;
                _FAX = value;
            }
        }
        public string EMAIL
        {
            get
            {
                return _EMAIL;
            }
            set
            {
                if (_EMAIL == value)
                    return;
                _EMAIL = value;
            }
        }
        public string PIC
        {
            get
            {
                return _PIC;
            }
            set
            {
                if (_PIC == value)
                    return;
                _PIC = value;
            }
        }
        public string REMARK
        {
            get
            {
                return _REMARK;
            }
            set
            {
                if (_REMARK == value)
                    return;
                _REMARK = value;
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
        #endregion

    }
}
