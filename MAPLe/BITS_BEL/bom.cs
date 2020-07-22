using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class BOM
    {
        public BOM()
        {
            //constructor
        }

        #region "Private Member"

        private string _CUST_PROD_NO;
        private string _MTL_CODE;
        private string _MTL_TYPE;
        private Decimal _BOM_QTY;
        private string _N_USER;
        private DateTime _N_DATE;

        #endregion

        #region "Property Member"

        public string CUST_PROD_NO
        {
            get
            {
                return _CUST_PROD_NO;
            }
            set
            {
                if (_CUST_PROD_NO == value)
                    return;
                _CUST_PROD_NO = value;
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

        public Decimal BOM_QTY
        {
            get
            {
                return _BOM_QTY;
            }
            set
            {
                if (_BOM_QTY == value)
                    return;
                _BOM_QTY = value;
            }
        }

        public string N_USER
        {
            get
            {
                return _N_USER;
            }
            set
            {
                if (_N_USER == value)
                    return;
                _N_USER = value;
            }
        }

        public DateTime N_DATE
        {
            get
            {
                return _N_DATE;
            }
            set
            {
                if (_N_DATE == value)
                    return;
                _N_DATE = value;
            }
        }
        #endregion
    }
}
