using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class T_DOC_ENTRY
    {
        #region Variable Member

        private string _DOC_ID;
        private int _REC_QTY;
        private string _UNIT;
        private int _TOTAL_QTY;
        private string _RESULTMSG;

        #endregion

        #region Property Member

        public string ARRIVAL_NO
        {
            get
            {
                return _DOC_ID;
            }
            set
            {
                if (_DOC_ID == value)
                    return;
                _DOC_ID = value;
            }
        }
        public int RECEIVED_QTY
        {
            get
            {
                return _REC_QTY;
            }
            set
            {
                if (_REC_QTY == value)
                    return;
                _REC_QTY = value;
            }
        }
        public string UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                if (_UNIT == value)
                    return;
                _UNIT = value;
            }
        }
        public string RESULTMSG
        {
            get
            {
                return _RESULTMSG;
            }
            set
            {
                if (_RESULTMSG == value)
                    return;
                _RESULTMSG = value;
            }
        }
        public int TOTAL_QTY
        {
            get
            {
                return _TOTAL_QTY;
            }
            set
            {
                if (_TOTAL_QTY == value)
                    return;
                _TOTAL_QTY = value;
            }
        }

        #endregion
    }
}
