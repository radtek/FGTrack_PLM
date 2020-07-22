using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class PickingDtl
    {
        public PickingDtl()
        {
        }

        #region "Variable Member"

        private string _SO_NO;
        private string _PICK_NO;
        private int _LINE_NO;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MTL_TYPE;
        private int _INI_QTY;
        private int _QTY;
        private string _UNIT_ID;
        private int _PICKED_QTY;
        private int _LOADED_QTY;
        private string _FLAG;

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
        public string PICK_NO
        {
            get
            {
                return _PICK_NO;
            }
            set
            {
                if (_PICK_NO == value)
                    return;
                _PICK_NO = value;
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
        public int INI_QTY
        {
            get
            {
                return _INI_QTY;
            }
            set
            {
                if (_INI_QTY == value)
                    return;
                _INI_QTY = value;
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
        public string FLAG
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
