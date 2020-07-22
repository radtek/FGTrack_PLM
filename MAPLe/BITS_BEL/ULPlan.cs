using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ULPlan
    {
        #region "Private Member"


        private string _PLAN_NO;
        private string _N_USER_ID;
        private DateTime? _N_USER_DATE;
        private string _U_USER_ID;
        private DateTime? _U_USER_DATE;
        private string _PROD_TYPE;
        private bool _REC_STAT;

        private DateTime? _PLAN_DATE;

        private string _PLAN_STAT;

        #endregion

        #region "Property Member"

        public string PLAN_NO
        {
            get
            {
                return _PLAN_NO;
            }
            set
            {
                if (_PLAN_NO == value)
                    return;
                _PLAN_NO = value;
            }
        }
        public string N_USER_ID
        {
            get
            {
                return _N_USER_ID;
            }
            set
            {
                if (_N_USER_ID == value)
                    return;
                _N_USER_ID = value;
            }
        }
        public DateTime? N_USER_DATE
        {
            get
            {
                return _N_USER_DATE;
            }
            set
            {
                if (_N_USER_DATE == value)
                    return;
                _N_USER_DATE = value;
            }
        }
        public DateTime? U_USER_DATE
        {
            get
            {
                return _U_USER_DATE;
            }
            set
            {
                if (_U_USER_DATE == value)
                    return;
                _U_USER_DATE = value;
            }
        }
        public string U_USER_ID
        {
            get
            {
                return _U_USER_ID;
            }
            set
            {
                if (_U_USER_ID == value)
                    return;
                _U_USER_ID = value;
            }
        }
        public string PROD_TYPE
        {
            get
            {
                return _PROD_TYPE;
            }
            set
            {
                if (_PROD_TYPE == value)
                    return;
                _PROD_TYPE = value;
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
       
        public DateTime? PLAN_DATE
        {
            get
            {
                return _PLAN_DATE;
            }
            set
            {
                if (_PLAN_DATE == value)
                    return;
                _PLAN_DATE = value;
            }
        }

        public string PLAN_STAT
        {
            get
            {
                return _PLAN_STAT;
            }
            set
            {
                if (_PLAN_STAT == value)
                    return;
                _PLAN_STAT = value;
            }
        }
    
        #endregion
    }
}
