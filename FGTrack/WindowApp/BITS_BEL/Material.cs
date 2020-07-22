using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class Material
    {
        public Material()
        {
            //constructor
        }

        #region "Private Member"

        private string _MTL_SEQ_NO;
        private string _MTL_CODE;
        private string _MTL_NAME;
        private string _MTL_GRADE;
        private string _MTL_COLOR;
        private string _UNIT;
        private decimal _STD_QTY;
        private decimal _MIN_QTY;
        private decimal _MAX_QTY;
        private string _PARTY_ID;
        private string _PARTY_NAME;
        private Bitmap _MTL_IMAGE;
        private string _LOCATION_ID;
        private string _REMARK;
        private bool _REC_STAT;
        private decimal _FREE_STOCK;

        #endregion

        #region "Property Member"

        public string MTL_SEQ_NO
        {
            get
            {
                return _MTL_SEQ_NO;
            }
            set
            {
                if (_MTL_SEQ_NO == value)
                    return;
                _MTL_SEQ_NO = value;
            }
        }
        public string MTL_CODE
        {
            get
            {
                return _MTL_CODE;
            }
            set
            {
                if (_MTL_CODE == value)
                    return;
                _MTL_CODE = value;
            }
        }
        public string MTL_NAME
        {
            get
            {
                return _MTL_NAME;
            }
            set
            {
                if (_MTL_NAME == value)
                    return;
                _MTL_NAME = value;
            }
        }
        public string MTL_GRADE
        {
            get
            {
                return _MTL_GRADE;
            }
            set
            {
                if (_MTL_GRADE == value)
                    return;
                _MTL_GRADE = value;
            }
        }
        public string MTL_COLOR
        {
            get
            {
                return _MTL_COLOR;
            }
            set
            {
                if (_MTL_COLOR == value)
                    return;
                _MTL_COLOR = value;
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
        public decimal STD_QTY
        {
            get
            {
                return _STD_QTY;
            }
            set
            {
                if (_STD_QTY == value)
                    return;
                _STD_QTY = value;
            }
        }
        public decimal MIN_QTY
        {
            get
            {
                return _MIN_QTY;
            }
            set
            {
                if (_MIN_QTY == value)
                    return;
                _MIN_QTY = value;
            }
        }
        public decimal MAX_QTY
        {
            get
            {
                return _MAX_QTY;
            }
            set
            {
                if (_MAX_QTY == value)
                    return;
                _MAX_QTY = value;
            }
        }
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
        public Bitmap MTL_IMAGE
        {
            get
            {
                return _MTL_IMAGE;
            }
            set
            {
                if (_MTL_IMAGE == value)
                    return;
                _MTL_IMAGE = value;
            }
        }
        public string LOCATION_ID
        {
            get
            {
                return _LOCATION_ID;
            }
            set
            {
                if (_LOCATION_ID == value)
                    return;
                _LOCATION_ID = value;
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
        public decimal FREE_STOCK
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

        #endregion
    }
}
