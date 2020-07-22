using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    [Serializable]
    public class ProductCard
    {
        public ProductCard()
        {
        }

        #region "Private Member"
        
        private string _SERIAL_NO;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MTL_TYPE;
        private string _JOB_NO;
        private string _JOB_LOT;
        private string _SHIFT;
        private int _LINE_NO;
        private string _MC_NO;
        private string _ORI_LABEL;
        private int _QTY;
        private int _BOX_QTY;
        private int _BOX_SCANNED;
        private int _ASG_NG;
        private int _NG_QTY;
        private string _UNIT_ID;

        #endregion

        #region "Property Member"

        public string ORI_LABEL
        {
            get
            {
                return _ORI_LABEL;
            }
            set
            {
                if (_ORI_LABEL == value)
                    return;
                _ORI_LABEL = value;
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
        public string JOB_LOT
        {
            get
            {
                return _JOB_LOT;
            }
            set
            {
                if (_JOB_LOT == value)
                    return;
                _JOB_LOT = value;
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
        public int BOX_SCANNED
        {
            get
            {
                return _BOX_SCANNED;
            }
            set
            {
                if (_BOX_SCANNED == value)
                    return;
                _BOX_SCANNED = value;
            }
        }
        public int ASG_NG
        {
            get
            {
                return _ASG_NG;
            }
            set
            {
                if (_ASG_NG == value)
                    return;
                _ASG_NG = value;
            }
        }
        public int NG_QTY
        {
            get
            {
                return _NG_QTY;
            }
            set
            {
                if (_NG_QTY == value)
                    return;
                _NG_QTY = value;
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
        #endregion
    
    }
}
