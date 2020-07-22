using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class StockByCustomer
    {
        public StockByCustomer()
        { }

        #region Variable Member

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _WH_ID;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _PRODUCT_TYPE_ID;
        private string _PRODUCT_TYPE_NAME;
        private int _BOX_QTY;
        private int _QTY;
        private int _NO_OF_BOX;
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
        public string PRODUCT_TYPE_ID
        {
            get
            {
                return _PRODUCT_TYPE_ID;
            }
            set
            {
                if (_PRODUCT_TYPE_ID == value)
                    return;
                _PRODUCT_TYPE_ID = value;
            }
        }
        public string PRODUCT_TYPE_NAME
        {
            get
            {
                return _PRODUCT_TYPE_NAME;
            }
            set
            {
                if (_PRODUCT_TYPE_NAME == value)
                    return;
                _PRODUCT_TYPE_NAME = value;
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
