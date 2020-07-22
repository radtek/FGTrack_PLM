using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class DeliveryBoardDetail
    {
        public DeliveryBoardDetail()
        { }

        #region Variable Member

        private string _PARTY_ID;
        private string _WH_ID;
        private DateTime _ETD_DATE;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _QTY;
        private string _UNIT;
        private int _NO_OF_BOX;
        private int _FREE_STOCK;
        private int _ASSIGN_QTY;
        private int _PICKED_QTY;
        private int _LOADED_QTY;
        private string _STATUS;
        private string _REMARK;
        private int _FLAG;
       
        #endregion

        #region Property Member

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
        public DateTime ETD_DATE
        {
            get
            {
                return _ETD_DATE;
            }
            set
            {
                if (_ETD_DATE == value)
                    return;
                _ETD_DATE = value;
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
        public string UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                if (_UNIT == value)
                    return;
                _UNIT = value;
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
