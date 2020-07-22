using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class TransferOrderDtl
    {
        public TransferOrderDtl()
        {
        }

        #region Variable Member

        private string _TO_NO;
        private string _LINE_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _PROD_SEQ_NO;
        private int _QTY;
        private string _UNIT_ID;
        private int _QTY_BOX;
        private int _NO_OF_BOX;
        private int _QTY_DELIVERY;
        private int _BOX_DELIVERY;
        private int _STATUS;
        private int _FLAG;
        private string _REMARK;
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
        public string LINE_NO
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
        public int QTY_BOX
        {
            get
            {
                return _QTY_BOX;
            }
            set
            {
                if (_QTY_BOX == value)
                    return;
                _QTY_BOX = value;
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
        public int QTY_DELIVERY
        {
            get
            {
                return _QTY_DELIVERY;
            }
            set
            {
                if (_QTY_DELIVERY == value)
                    return;
                _QTY_DELIVERY = value;
            }
        }
        public int BOX_DELIVERY
        {
            get
            {
                return _BOX_DELIVERY;
            }
            set
            {
                if (_BOX_DELIVERY == value)
                    return;
                _BOX_DELIVERY = value;
            }
        }
        public int STATUS
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
