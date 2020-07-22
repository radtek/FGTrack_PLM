using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class MaterialSP
    {
        public MaterialSP()
        {
            //constructor
        }

        #region "Private Member"

        private string _SP_GROUP;
        private string _CUST_PROD_NO;
        private string _MTL_CODE;
        private string _N_USER;
        private DateTime _N_DATE;
        private string _U_USER;
        private DateTime? _U_DATE;

        #endregion

        #region "Property Member"

        public string SP_GROUP
        {
            get
            {
                return _SP_GROUP;
            }
            set
            {
                if (_SP_GROUP == value)
                    return;
                _SP_GROUP = value;
            }
        }

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

        public string U_USER
        {
            get
            {
                return _U_USER;
            }
            set
            {
                if (_U_USER == value)
                    return;
                _U_USER = value;
            }
        }

        public DateTime? U_DATE
        {
            get
            {
                return _U_DATE;
            }
            set
            {
                if (_U_DATE == value)
                    return;
                _U_DATE = value;
            }
        }
        
        #endregion
    }
}
