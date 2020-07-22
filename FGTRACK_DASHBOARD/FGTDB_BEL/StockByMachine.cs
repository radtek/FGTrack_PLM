using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTDB.BEL
{
    public class StockByMachine
    {
        public StockByMachine()
        { }

        #region Variable Member

        private string _PARTY_ID;
        private string _PARTY_NAME;
        private DateTime? _START_DATE;
        private DateTime? _END_DATE;
        private string _STATUS;
        private int _PLAN_QTY;
        private string _PRODUCT_TYPE;
        private string _PROD_TYPE_S;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _STOCK_PCS;
        private int _STOCK_BOX;
        private string _MC_NO;
        private string _MACHINE_NAME;
        private int _MIN_BOX;
        private int _MAX_BOX;
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
        public DateTime? START_DATE
        {
            get
            {
                return _START_DATE;
            }
            set
            {
                if (_START_DATE == value)
                    return;
                _START_DATE = value;
            }
        }
        public DateTime? END_DATE
        {
            get
            {
                return _END_DATE;
            }
            set
            {
                if (_END_DATE == value)
                    return;
                _END_DATE = value;
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
        public string PROD_TYPE_S
        {
            get
            {
                return _PROD_TYPE_S;
            }
            set
            {
                if (_PROD_TYPE_S == value)
                    return;
                _PROD_TYPE_S = value;
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
        public string MACHINE_NAME
        {
            get
            {
                return _MACHINE_NAME;
            }
            set
            {
                if (_MACHINE_NAME == value)
                    return;
                _MACHINE_NAME = value;
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
