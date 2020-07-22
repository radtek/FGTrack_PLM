using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ReturnType
    {
        public ReturnType()
        {

        }

        #region "Private Member"

        private string _SEQ_NO;
        private string _NAME;
        private string _REMARK;
        private bool _REC_STAT;

        #endregion

        #region "Property Member"

        public string SEQ_NO
        {
            get
            {
                return _SEQ_NO;
            }
            set
            {
                if (_SEQ_NO == value)
                    return;
                _SEQ_NO = value;
            }
        }
        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                if (_NAME == value)
                    return;
                _NAME = value;
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

        #endregion
    }
}
