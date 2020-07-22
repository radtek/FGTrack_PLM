using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ShippingOrderDtl
    {
        public ShippingOrderDtl()
        {
        }

        #region "Variable Member"

        private string _SO_NO;
        private string _PROD_SEQ_NO;
        private int _LINE_NO;
        private string _UNIT_ID;
        private string _PACKAGING;
        private int _QTY;
        private int _FREE_STOCK;
        private int _ASSIGN_QTY;
        private int _PICKED_QTY;
        private int _LOADED_QTY;
        private bool _REC_STAT;
        private int _FLAG;
        private decimal _UNIT_PRICE;
        private string _REMARK;
        private string _PO_NO;

        #endregion


        #region "Property Member"

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
        public string PACKAGING
        {
            get
            {
                return _PACKAGING;
            }
            set
            {
                if (_PACKAGING == value)
                    return;
                _PACKAGING = value;
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
        public int FREE_STOCK
        {
            get
            {
                return _FREE_STOCK;
            }
            set
            {
                if (_FREE_STOCK == value)
                    return;
                _FREE_STOCK = value;
            }
        }
        public int ASSIGN_QTY
        {
            get
            {
                return _ASSIGN_QTY;
            }
            set
            {
                if (_ASSIGN_QTY == value)
                    return;
                _ASSIGN_QTY = value;
            }
        }
        public int PICKED_QTY
        {
            get
            {
                return _PICKED_QTY;
            }
            set
            {
                if (_PICKED_QTY == value)
                    return;
                _PICKED_QTY = value;
            }
        }
        public int LOADED_QTY
        {
            get
            {
                return _LOADED_QTY;
            }
            set
            {
                if (_LOADED_QTY == value)
                    return;
                _LOADED_QTY = value;
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
        public decimal UNIT_PRICE
        {
            get
            {
                return _UNIT_PRICE;
            }
            set
            {
                if (_UNIT_PRICE == value)
                    return;
                _UNIT_PRICE = value;
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

        #endregion
    }
}
