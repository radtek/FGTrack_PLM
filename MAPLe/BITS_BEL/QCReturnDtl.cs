using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class QCReturnDtl
    {
        public QCReturnDtl()
        {
        }

        #region Variable Member

        private string _RT_NO;
        private string _SERIAL_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _QTY;
        private string _UNIT_ID;
        private string _LABEL_STATUS;
        private string _RETURN_BY;
        private DateTime _RETURN_DATE;

        #endregion

        #region Property Member

        public string RT_NO
        {
            get
            {
                return _RT_NO;
            }
            set
            {
                if (_RT_NO == value)
                    return;
                _RT_NO = value;
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
        public string RETURN_BY
        {
            get
            {
                return _RETURN_BY;
            }
            set
            {
                if (_RETURN_BY == value)
                    return;
                _RETURN_BY = value;
            }
        }
        public DateTime RETURN_DATE
        {
            get
            {
                return _RETURN_DATE;
            }
            set
            {
                if (_RETURN_DATE == value)
                    return;
                _RETURN_DATE = value;
            }
        }

        #endregion
    }
}
