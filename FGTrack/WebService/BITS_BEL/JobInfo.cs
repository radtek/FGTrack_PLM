using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    public class JobInfo
    {
        public JobInfo()
        {
        }


        #region "Private Member"
        
        private string _JOB_NO;
        private string _JOB_LOT;
        private string _PARTY_NAME;
        private string _MTL_GRADE;
        private int _ASG_NG;
        private int _NG_QTY;

        #endregion

        #region "Property Member"

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
        #endregion
    }
}
