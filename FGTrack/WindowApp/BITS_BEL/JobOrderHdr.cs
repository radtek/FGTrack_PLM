using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class JobOrderHdr
    {
        public JobOrderHdr()
        {

        }

        #region "Variable Member"

       
        private string _JOB_NO;
        private string _PRODUCTION_TYPE;
        private string _REF_NO;
        private DateTime _PLAN_DATE;
        private string _CUSTOMER;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MTL_TYPE;
        private string _MC_NO;
        private DateTime _MP_START_DATE;
        private DateTime _MP_STOP_DATE;
        private int _PLAN_QTY;
        private string _UNIT_ID;
        private DateTime _SHIFT_DATE;
        private string _SHIFT;

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
        public string PRODUCTION_TYPE
        {
            get
            {
                return _PRODUCTION_TYPE;
            }
            set
            {
                if (_PRODUCTION_TYPE == value)
                    return;
                _PRODUCTION_TYPE = value;
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
        public DateTime PLAN_DATE
        {
            get
            {
                return _PLAN_DATE;
            }
            set
            {
                if (_PLAN_DATE == value)
                    return;
                _PLAN_DATE = value;
            }
        }
        public string CUSTOMER
        {
            get
            {
                return _CUSTOMER;
            }
            set
            {
                if (_CUSTOMER == value)
                    return;
                _CUSTOMER = value;
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
        public string PRODUCT_NAME
        {
            get
            {
                return _PRODUCT_NAME;
            }
            set
            {
                if (_PRODUCT_NAME == value)
                    return;
                _PRODUCT_NAME = value;
            }
        }
        public string MTL_TYPE
        {
            get
            {
                return _MTL_TYPE;
            }
            set
            {
                if (_MTL_TYPE == value)
                    return;
                _MTL_TYPE = value;
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
        public DateTime MP_START_DATE
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
        public DateTime MP_STOP_DATE
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
        public DateTime SHIFT_DATE
        {
            get
            {
                return _SHIFT_DATE;
            }
            set
            {
                if (_SHIFT_DATE == value)
                    return;
                _SHIFT_DATE = value;
            }
        }
        public string SHIFT
        {
            get
            {
                return _SHIFT;
            }
            set
            {
                if (_SHIFT == value)
                    return;
                _SHIFT = value;
            }
        }

        #endregion
    }
}
