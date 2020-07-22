using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class IF_ControlOrder
    {
        public IF_ControlOrder()
        { }

        #region Variable Member

        private string _SEQ;
        private DateTime _SEQ_DATE;
        private string _FILE_ID;
        private int _LINE_NO;
        private string _TEXT;

        #endregion

        #region Property Member

        public string SEQ
        {
            get
            {
                return _SEQ;
            }
            set
            {
                if (_SEQ == value)
                    return;
                _SEQ = value;
            }
        }
        public DateTime SEQ_DATE
        {
            get
            {
                return _SEQ_DATE;
            }
            set
            {
                if (_SEQ_DATE == value)
                    return;
                _SEQ_DATE = value;
            }
        }
        public string FILE_ID
        {
            get
            {
                return _FILE_ID;
            }
            set
            {
                if (_FILE_ID == value)
                    return;
                _FILE_ID = value;
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
        public string TEXT
        {
            get
            {
                return _TEXT;
            }
            set
            {
                if (_TEXT == value)
                    return;
                _TEXT = value;
            }
        }

        #endregion

    }
}
