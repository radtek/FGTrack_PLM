using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class DeliveryOrderDtl
    {
        public DeliveryOrderDtl()
        {
        }

        #region Variable Member

        private string _DO_NO;
        private int _LINE_NO;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _QTY;
        private string _UNIT_ID;
        private int _QTY_PER_BOX;
        private int _NO_OF_BOX;
        private int _DELIVERY_QTY;
        private int _DELIVERY_BOX;
        private string _STATUS;
        private string _REMARK;
        private int _FLAG;

        #endregion

        #region Property Member

        public string DO_NO
        {
            get
            {
                return _DO_NO;
            }
            set
            {
                if (_DO_NO == value)
                    return;
                _DO_NO = value;
            }
        }
        public int LINE_NO
        {
            get
            {
                return _LINE_NO;
            }
            set
            {
                if (_LINE_NO == value)
                    return;
                _LINE_NO = value;
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
        public int DELIVERY_QTY
        {
            get
            {
                return _DELIVERY_QTY;
            }
            set
            {
                if (_DELIVERY_QTY == value)
                    return;
                _DELIVERY_QTY = value;
            }
        }
        public int DELIVERY_BOX
        {
            get
            {
                return _DELIVERY_BOX;
            }
            set
            {
                if (_DELIVERY_BOX == value)
                    return;
                _DELIVERY_BOX = value;
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
        public int FLAG
        {
            get
            {
                return _FLAG;
            }
            set
            {
                if (_FLAG == value)
                    return;
                _FLAG = value;
            }
        }

        #endregion
    }
}
