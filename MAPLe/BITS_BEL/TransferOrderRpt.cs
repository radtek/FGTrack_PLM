using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace HTN.BITS.BEL.PLASESS
{
    public class TransferOrderRpt
    {
        public  TransferOrderRpt()
        {
        }
        #region Variable Member
        
        private string _PRODUCT_TYPE;
        private string _TO_NO;
        private DateTime _TO_DATE;
        private string _REF_NO;
        private string _DESTINATION;
        private DateTime _DELIVERY_DATE;
        private string _HDR_REMARK;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _QTY;
        private string _UNIT_ID;
        private int _QTY_PER_BOX;
        private int _NO_OF_BOX;
        private string _DTL_REMARK;

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
        public string HDR_REMARK
        {
            get
            {
                return _HDR_REMARK;
            }
            set
            {
                if (_HDR_REMARK == value)
                    return;
                _HDR_REMARK = value;
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
        public int QTY_PER_BOX
        {
            get
            {
                return _QTY_PER_BOX;
            }
            set
            {
                if (_QTY_PER_BOX == value)
                    return;
                _QTY_PER_BOX = value;
            }
        }
        public int NO_OF_BOX
        {
            get
            {
                return _NO_OF_BOX;
            }
            set
            {
                if (_NO_OF_BOX == value)
                    return;
                _NO_OF_BOX = value;
            }
        }
       
        public string DTL_REMARK
        {
            get
            {
                return _DTL_REMARK;
            }
            set
            {
                if (_DTL_REMARK == value)
                    return;
                _DTL_REMARK = value;
            }
        }




        #endregion
    }
}
