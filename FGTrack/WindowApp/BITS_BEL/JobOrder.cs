using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class JobOrder
    {
        public JobOrder()
        {
        }

        #region "Private Member"

        private string _JOB_NO;
        private DateTime _JOB_DATE;
        private string _JOB_TYPE;
        private string _PROD_TYPE;
        private string _REF_NO;
        private string _RETURN_TYPE;
        private DateTime _PRODUCTION_DATE;
        private string _MC_NO;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PARTY_ID;
        private string _PARTY_NAME;
        private DateTime? _MP_START_DATE;
        private DateTime? _MP_STOP_DATE;
        private string _LOT_NO;
        private int _PLAN_QTY;
        private string _UNIT_ID;
        private int _PLAN_DAY;
        private string _REMARK;
        private bool _REC_STAT;
        private int _NG_QTY;

        #endregion

        #region "Property Member"

        public string JOB_NO
        {
            get
            {
                return _JOB_NO;
            }
            set
            {
                if (_JOB_NO == value)
                    return;
                _JOB_NO = value;
            }
        }
        public DateTime JOB_DATE
        {
            get
            {
                return _JOB_DATE;
            }
            set
            {
                if (_JOB_DATE == value)
                    return;
                _JOB_DATE = value;
            }
        }
        public string JOB_TYPE
        {
            get
            {
                return _JOB_TYPE;
            }
            set
            {
                if (_JOB_TYPE == value)
                    return;
                _JOB_TYPE = value;
            }
        }
        public string PROD_TYPE
        {
            get
            {
                return _PROD_TYPE;
            }
            set
            {
                if (_PROD_TYPE == value)
                    return;
                _PROD_TYPE = value;
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
        public string RETURN_TYPE
        {
            get
            {
                return _RETURN_TYPE;
            }
            set
            {
                if (_RETURN_TYPE == value)
                    return;
                _RETURN_TYPE = value;
            }
        }
        public DateTime PRODUCTION_DATE
        {
            get
            {
                return _PRODUCTION_DATE;
            }
            set
            {
                if (_PRODUCTION_DATE == value)
                    return;
                _PRODUCTION_DATE = value;
            }
        }
        public string MC_NO
        {
            get
            {
                return _MC_NO;
            }
            set
            {
                if (_MC_NO == value)
                    return;
                _MC_NO = value;
            }
        }
        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
            }
        }

        public string PRODUCT_NO
        {
            get
            {
                return _PRODUCT_NO;
            }
            set
            {
                if (_PRODUCT_NO == value)
                    return;
                _PRODUCT_NO = value;
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
        public DateTime? MP_START_DATE
        {
            get
            {
                return _MP_START_DATE;
            }
            set
            {
                if (_MP_START_DATE == value)
                    return;
                _MP_START_DATE = value;
            }
        }
        public DateTime? MP_STOP_DATE
        {
            get
            {
                return _MP_STOP_DATE;
            }
            set
            {
                if (_MP_STOP_DATE == value)
                    return;
                _MP_STOP_DATE = value;
            }
        }
        public string LOT_NO
        {
            get
            {
                return _LOT_NO;
            }
            set
            {
                if (_LOT_NO == value)
                    return;
                _LOT_NO = value;
            }
        }
        public int PLAN_QTY
        {
            get
            {
                return _PLAN_QTY;
            }
            set
            {
                if (_PLAN_QTY == value)
                    return;
                _PLAN_QTY = value;
            }
        }
        public string UNIT_ID
        {
            get
            {
                return _UNIT_ID;
            }
            set
            {
                if (_UNIT_ID == value)
                    return;
                _UNIT_ID = value;
            }
        }
        public int PLAN_DAY
        {
            get
            {
                return _PLAN_DAY;
            }
            set
            {
                if (_PLAN_DAY == value)
                    return;
                _PLAN_DAY = value;
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
        public int NG_QTY
        {
            get
            {
                return _NG_QTY;
            }
            set
            {
                if (_NG_QTY == value)
                    return;
                _NG_QTY = value;
            }
        }
        #endregion
    }
}
