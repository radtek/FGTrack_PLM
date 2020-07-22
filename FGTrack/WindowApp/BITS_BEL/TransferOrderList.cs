using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class TransferOrderList
    {
        public TransferOrderList()
        {
        }

        #region Variable Member

        private string _PRODUCT_TYPE;
        private string _TO_NO;
        private DateTime _TO_DATE;
        private string _REF_NO;
        private string _DESTINATION;
        private DateTime _DELIVERY_DATE;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _QTY;
        private int _BOX_QTY;
        private int _DEV_QTY;
        private int _DEV_BOX;
        private string _REMARK;
        private string _POST_REF;

        #endregion

        #region Property Member

        public string PRODUCT_TYPE
        {
            get
            {
                return _PRODUCT_TYPE;
            }
            set
            {
                if (_PRODUCT_TYPE == value)
                    return;
                _PRODUCT_TYPE = value;
            }
        }
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
        public string DESTINATION
        {
            get
            {
                return _DESTINATION;
            }
            set
            {
                if (_DESTINATION == value)
                    return;
                _DESTINATION = value;
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
        public int QTY
        {
            get
            {
                return _QTY;
            }
            set
            {
                if (_QTY == value)
                    return;
                _QTY = value;
            }
        }
        public int BOX_QTY
        {
            get
            {
                return _BOX_QTY;
            }
            set
            {
                if (_BOX_QTY == value)
                    return;
                _BOX_QTY = value;
            }
        }
        public int DEV_QTY
        {
            get
            {
                return _DEV_QTY;
            }
            set
            {
                if (_DEV_QTY == value)
                    return;
                _DEV_QTY = value;
            }
        }
        public int DEV_BOX
        {
            get
            {
                return _DEV_BOX;
            }
            set
            {
                if (_DEV_BOX == value)
                    return;
                _DEV_BOX = value;
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
