using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class QCReturnHdr
    {
        public QCReturnHdr()
        {
        }

        #region Variable Member

       
        private string _RT_NO;
        private DateTime _RT_DATE;
        private string _WH_ID;
        private string _RT_TYPE;
        private string _REMARK;
        private int _NO_OF_LABEL;
        private string _ISSUED_BY;
        private DateTime _ISSUED_DATE;
        private bool _REC_STAT;

        //add new variable on 07-Jun-2011
        private string _POST_REF;

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
        public DateTime RT_DATE
        {
            get
            {
                return _RT_DATE;
            }
            set
            {
                if (_RT_DATE == value)
                    return;
                _RT_DATE = value;
            }
        }
        public string WH_ID
        {
            get
            {
                return _WH_ID;
            }
            set
            {
                if (_WH_ID == value)
                    return; 
                _WH_ID = value;
            }
        }
        public string RT_TYPE
        {
            get
            {
                return _RT_TYPE;
            }
            set
            {
                if (_RT_TYPE == value)
                    return;
                _RT_TYPE = value;
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
        public int NO_OF_LABEL
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
        public string ISSUED_BY
        {
            get
            {
                return _ISSUED_BY;
            }
            set
            {
                if (_ISSUED_BY == value)
                    return;
                _ISSUED_BY = value;
            }
        }
        public DateTime ISSUED_DATE
        {
            get
            {
                return _ISSUED_DATE;
            }
            set
            {
                if (_ISSUED_DATE == value)
                    return;
                _ISSUED_DATE = value;
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

        public string POST_REF
        {
            get
            {
                return _POST_REF;
            }
            set
            {
                if (_POST_REF == value)
                    return;
                _POST_REF = value;
            }
        }

        #endregion



    }
}
