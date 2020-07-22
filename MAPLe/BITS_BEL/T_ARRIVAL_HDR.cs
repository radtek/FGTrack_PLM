using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class T_ARRIVAL_HDR
    {

        public T_ARRIVAL_HDR()
        {
        }

        #region Variable Member

        private string _ARRIVAL_NO;
        private DateTime? _ARRIVAL_DATE;
        private string _WH_ID;
        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _REF_NO;
        private DateTime? _REF_DATE;
        private string _REMARK;
        private bool _REC_STAT;
        private string _ARR_TYPE;
        private string _USER_ID;
        private string _STATUS;
        private string _PO_NO;

        private string _POST_REF;

        #endregion

        #region Property Member

        public string ARRIVAL_NO
        {
            get
            {
                return _ARRIVAL_NO;
            }
            set
            {
                if (_ARRIVAL_NO == value)
                    return;
                _ARRIVAL_NO = value;
            }
        }
        public DateTime? ARRIVAL_DATE
        {
            get
            {
                return _ARRIVAL_DATE;
            }
            set
            {
                if (_ARRIVAL_DATE == value)
                    return;
                _ARRIVAL_DATE = value;
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
        public string REF_NO
        {
            get
            {
                return _REF_NO;
            }
            set
            {
                if (_REF_NO == value)
                    return;
                _REF_NO = value;
            }
        }
        public DateTime? REF_DATE
        {
            get
            {
                return _REF_DATE;
            }
            set
            {
                if (_REF_DATE == value)
                    return;
                _REF_DATE = value;
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
        public string ARR_TYPE
        {
            get
            {
                return _ARR_TYPE;
            }
            set
            {
                if (_ARR_TYPE == value)
                    return;
                _ARR_TYPE = value;
            }
        }

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

        public string PO_NO
        {
            get
            {
                return _PO_NO;
            }
            set
            {
                if (_PO_NO == value)
                    return;
                _PO_NO = value;
            }
        }

        public string POST_REF
        {
            get
            {
                return _POST_REF;
            }
            set
            {
                if (_POST_REF == value)
                    return;
                _POST_REF = value;
            }
        }

        #endregion


    }
}
