using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class StockByMinMax
    {
        public StockByMinMax()
        { }

        #region Variable Member

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _WH_ID;
        private string _PRODUCT_NO;
        private string _PRODUCT_SEQ_NO;
        private string _PRODUCT_NAME;
        private string _PRODUCT_TYPE_ID;
        private string _PRODUCT_TYPE_NAME;
        private int _BOX_QTY;
        private int _STOCK_PCS;
        private int _STOCK_BOX;
        private string _STATUS_RUNNING_MC;
        private int _PICK_PENDING;
        private int _EXPECTED_DELAY;
        private int _FORECAST;
        private int _MIN_BOX;
        private int _MAX_BOX;
        private string _STATUS;
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
        public string PRODUCT_SEQ_NO
        {
            get
            {
                return _PRODUCT_SEQ_NO;
            }
            set
            {
                if (_PRODUCT_SEQ_NO == value)
                    return;
                _PRODUCT_SEQ_NO = value;
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
        public int STOCK_PCS
        {
            get
            {
                return _STOCK_PCS;
            }
            set
            {
                if (_STOCK_PCS == value)
                    return;
                _STOCK_PCS = value;
            }
        }
        public int STOCK_BOX
        {
            get
            {
                return _STOCK_BOX;
            }
            set
            {
                if (_STOCK_BOX == value)
                    return;
                _STOCK_BOX = value;
            }
        }
        public string STATUS_RUNNING_MC
        {
            get
            {
                return _STATUS_RUNNING_MC;
            }
            set
            {
                if (_STATUS_RUNNING_MC == value)
                    return;
                _STATUS_RUNNING_MC = value;
            }
        }
        public int PICK_PENDING
        {
            get
            {
                return _PICK_PENDING;
            }
            set
            {
                if (_PICK_PENDING == value)
                    return;
                _PICK_PENDING = value;
            }
        }
        public int EXPECTED_DELAY
        {
            get
            {
                return _EXPECTED_DELAY;
            }
            set
            {
                if (_EXPECTED_DELAY == value)
                    return;
                _EXPECTED_DELAY = value;
            }
        }
        public int FORECAST
        {
            get
            {
                return _FORECAST;
            }
            set
            {
                if (_FORECAST == value)
                    return;
                _FORECAST = value;
            }
        }
        public int MIN_BOX
        {
            get
            {
                return _MIN_BOX;
            }
            set
            {
                if (_MIN_BOX == value)
                    return;
                _MIN_BOX = value;
            }
        }
        public int MAX_BOX
        {
            get
            {
                return _MAX_BOX;
            }
            set
            {
                if (_MAX_BOX == value)
                    return;
                _MAX_BOX = value;
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
