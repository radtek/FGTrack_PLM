using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class TransferOrderHdr
    {
        public TransferOrderHdr()
        {
        }

        #region Variable Member

        
        private string _TO_NO;
        private DateTime _TO_DATE;
        private string _REF_NO;
        private string _PROD_TYPE;
        private string _TO_DEST;
        private DateTime _DELIVERY_DATE;
        private string _REMARK;
        
        private string _ISSUED_BY;
        private DateTime _ISSUED_DATE;
        private string _UPDATE_BY;
        private DateTime _UPDATE_DATE;
        private bool _REC_STAT;
        private string _POST_REF;

     

        #endregion

        #region Property Member

       
        public string TO_NO
        {
            get
            {
                return _TO_NO;
            }
            set
            {
                if (_TO_NO == value)
                    return;
                _TO_NO = value;
            }
        }
        public DateTime TO_DATE
        {
            get
            {
                return _TO_DATE;
            }
            set
            {
                if (_TO_DATE == value)
                    return;
                _TO_DATE = value;
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
        public string TO_DEST
        {
            get
            {
                return _TO_DEST;
            }
            set
            {
                if (_TO_DEST == value)
                    return;
                _TO_DEST = value;
            }
        }
        public DateTime DELIVERY_DATE
        {
            get
            {
                return _DELIVERY_DATE;
            }
            set
            {
                if (_DELIVERY_DATE == value)
                    return;
                _DELIVERY_DATE = value;
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
        
        public string ISSUED_BY
        {
            get
            {
                return _ISSUED_BY;
            }
            set
            {
                if (_ISSUED_BY == value)
                    return;
                _ISSUED_BY = value;
            }
        }
        public DateTime ISSUED_DATE
        {
            get
            {
                return _ISSUED_DATE;
            }
            set
            {
                if (_ISSUED_DATE == value)
                    return;
                _ISSUED_DATE = value;
            }
        }
        public string UPDATE_BY
        {
            get
            {
                return _UPDATE_BY;
            }
            set
            {
                if (_UPDATE_BY == value)
                    return;
                _UPDATE_BY = value;
            }
        }
        public DateTime UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE;
            }
            set
            {
                if (_UPDATE_DATE == value)
                    return;
                _UPDATE_DATE = value;
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
