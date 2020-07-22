using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class M_ITEM
    {
        #region Variable Member
        private string _CUST_CODE;
        private string _CONS_CODE;
        private string _CUST_CODE_NAME;
        private string _CONS_CODE_NAME;
        private string _ITEM_ID;
        private string _ITEM_CODE;
        private string _ITEM_NAME;
        private string _ITEM_LABEL_UNIT;
        private int _ITEM_QTY_PER_LABEL;
        private string _ITEM_UOM;
        private string _ITEM_MDU;
        private int _ITEM_MDU_QTY;
        private decimal _ITEM_STORAGE_L;
        private decimal _ITEM_STORAGE_W;
        private decimal _ITEM_STORAGE_H;
        private decimal _ITEM_STORAGE_TOTAL;
        private bool _ITEM_STATUS;
        private string _ITEM_CODE_REF; 
        private string _CREATED_BY;
        private DateTime? _CREATED_DATE;
        private string _MODIFIED_BY;
        private DateTime? _MODIFIED_DATE;
        #endregion

        #region Property Member
        public string RESULTMSG { get; set; }
        public string CUST_CODE
        {
            get
            {
                return _CUST_CODE;
            }
            set
            {
                if (_CUST_CODE == value)
                    return;
                _CUST_CODE = value;
            }
        }
        public string CONS_CODE
        {
            get
            {
                return _CONS_CODE;
            }
            set
            {
                if (_CONS_CODE == value)
                    return;
                _CONS_CODE = value;
            }
        }
        public string CUST_CODE_NAME
        {
            get
            {
                return _CUST_CODE_NAME;
            }
            set
            {
                if (_CUST_CODE_NAME == value)
                    return;
                _CUST_CODE_NAME = value;
            }
        }
        public string CONS_CODE_NAME
        {
            get
            {
                return _CONS_CODE_NAME;
            }
            set
            {
                if (_CONS_CODE_NAME == value)
                    return;
                _CONS_CODE_NAME = value;
            }
        }
        public string ITEM_ID
        {
            get
            {
                return _ITEM_ID;
            }
            set
            {
                if (_ITEM_ID == value)
                    return;
                _ITEM_ID = value;
            }
        }
        public string ITEM_CODE
        {
            get
            {
                return _ITEM_CODE;
            }
            set
            {
                if (_ITEM_CODE == value)
                    return;
                _ITEM_CODE = value;
            }
        }
        public string ITEM_NAME
        {
            get
            {
                return _ITEM_NAME;
            }
            set
            {
                if (_ITEM_NAME == value)
                    return;
                _ITEM_NAME = value;
            }
        }
        public string ITEM_LABEL_UNIT
        {
            get
            {
                return _ITEM_LABEL_UNIT;
            }
            set
            {
                if (_ITEM_LABEL_UNIT == value)
                    return;
                _ITEM_LABEL_UNIT = value;
            }
        }
        public int ITEM_QTY_PER_LABEL
        {
            get
            {
                return _ITEM_QTY_PER_LABEL;
            }
            set
            {
                if (_ITEM_QTY_PER_LABEL == value)
                    return;
                _ITEM_QTY_PER_LABEL = value;
            }
        }
        public string ITEM_UOM
        {
            get
            {
                return _ITEM_UOM;
            }
            set
            {
                if (_ITEM_UOM == value)
                    return;
                _ITEM_UOM = value;
            }
        }
        
        public string ITEM_MDU
        {
            get
            {
                return _ITEM_MDU;
            }
            set
            {
                if (_ITEM_MDU == value)
                    return;
                _ITEM_MDU = value;
            }
        }
        public int ITEM_MDU_QTY
        {
            get
            {
                return _ITEM_MDU_QTY;
            }
            set
            {
                if (_ITEM_MDU_QTY == value)
                    return;
                _ITEM_MDU_QTY = value;
            }
        }
        public decimal ITEM_STORAGE_L
        {
            get
            {
                return _ITEM_STORAGE_L;
            }
            set
            {
                if (_ITEM_STORAGE_L == value)
                    return;
                _ITEM_STORAGE_L = value;
            }
        }
        public decimal ITEM_STORAGE_W
        {
            get
            {
                return _ITEM_STORAGE_W;
            }
            set
            {
                if (_ITEM_STORAGE_W == value)
                    return;
                _ITEM_STORAGE_W = value;
            }
        }
        public decimal ITEM_STORAGE_H
        {
            get
            {
                return _ITEM_STORAGE_H;
            }
            set
            {
                if (_ITEM_STORAGE_H == value)
                    return;
                _ITEM_STORAGE_H = value;
            }
        }
        public decimal ITEM_STORAGE_TOTAL
        {
            get
            {
                return _ITEM_STORAGE_TOTAL;
            }
            set
            {
                if (_ITEM_STORAGE_TOTAL == value)
                    return;
                _ITEM_STORAGE_TOTAL = value;
            }
        }
        public bool ITEM_STATUS
        {
            get
            {
                return _ITEM_STATUS;
            }
            set
            {
                if (_ITEM_STATUS == value)
                    return;
                _ITEM_STATUS = value;
            }
        }
        public string ITEM_CODE_REF
        {
            get
            {
                return _ITEM_CODE_REF;
            }
            set
            {
                if (_ITEM_CODE_REF == value)
                    return;
                _ITEM_CODE_REF = value;
            }
        }
        public string CREATED_BY
        {
            get
            {
                return _CREATED_BY;
            }
            set
            {
                if (_CREATED_BY == value)
                    return;
                _CREATED_BY = value;
            }
        }
        public DateTime? CREATED_DATE
        {
            get
            {
                return _CREATED_DATE;
            }
            set
            {
                if (_CREATED_DATE == value)
                    return;
                _CREATED_DATE = value;
            }
        }
        public string MODIFIED_BY
        {
            get
            {
                return _MODIFIED_BY;
            }
            set
            {
                if (_MODIFIED_BY == value)
                    return;
                _MODIFIED_BY = value;
            }
        }
        public DateTime? MODIFIED_DATE
        {
            get
            {
                return _MODIFIED_DATE;
            }
            set
            {
                if (_MODIFIED_DATE == value)
                    return;
                _MODIFIED_DATE = value;
            }
        }
        #endregion
    }
}
