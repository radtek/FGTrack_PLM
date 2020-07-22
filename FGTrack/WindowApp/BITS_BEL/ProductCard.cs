using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class ProductCard
    {
        public ProductCard()
        {
        }

        #region "Private Member"

        
        private Bitmap _PROD_IMAGE;
        private string _SERIAL_NO;
        private string _JOB_NO;
        private int _LINE_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MTL_TYPE;
        private string _PARTY_NAME;
        private string _MC_NO;
        private string _SHIFT;
        private int _QTY;
        private string _UNIT_ID;
        private string _NO_OF_LABEL;
        private string _LABEL_STATUS;
        private int _NO_OF_PRINT;

        #endregion

        #region "Property Member"

        
        public Bitmap PROD_IMAGE
        {
            get
            {
                return _PROD_IMAGE;
            }
            set
            {
                if (_PROD_IMAGE == value)
                    return;
                _PROD_IMAGE = value;
            }
        }
        public string SERIAL_NO
        {
            get
            {
                return _SERIAL_NO;
            }
            set
            {
                if (_SERIAL_NO == value)
                    return;
                _SERIAL_NO = value;
            }
        }
        public string JOB_NO
        {
            get
            {
                return _JOB_NO;
            }
            set
            {
                if (_JOB_NO == value)
                    return;
                _JOB_NO = value;
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
        public string MTL_TYPE
        {
            get
            {
                return _MTL_TYPE;
            }
            set
            {
                if (_MTL_TYPE == value)
                    return;
                _MTL_TYPE = value;
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
        public string SHIFT
        {
            get
            {
                return _SHIFT;
            }
            set
            {
                if (_SHIFT == value)
                    return;
                _SHIFT = value;
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
        public string NO_OF_LABEL
        {
            get
            {
                return _NO_OF_LABEL;
            }
            set
            {
                if (_NO_OF_LABEL == value)
                    return;
                _NO_OF_LABEL = value;
            }
        }
        public string LABEL_STATUS
        {
            get
            {
                return _LABEL_STATUS;
            }
            set
            {
                if (_LABEL_STATUS == value)
                    return;
                _LABEL_STATUS = value;
            }
        }
        public int NO_OF_PRINT
        {
            get
            {
                return _NO_OF_PRINT;
            }
            set
            {
                if (_NO_OF_PRINT == value)
                    return;
                _NO_OF_PRINT = value;
            }
        }

        #endregion
    }
}
