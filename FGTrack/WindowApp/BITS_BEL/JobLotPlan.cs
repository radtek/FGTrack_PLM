using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class JobLotPlan
    {
        public JobLotPlan()
        {
        }

        #region "Variable Member"

        private int _NG_QTY;
        private string _JOB_NO;
        private int _LINE_NO;
        private string _SHIFT_LOT_NO;
        private string _SHIFT_ID;
        private DateTime? _SHIFT_DATE;
        private int _NO_OF_LABEL;
        private int _QTY_PER_LABEL;
        private string _REMARK;
        private bool _REC_STAT;
        private int _FLAG;

        #endregion

        #region "Property Member"

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
        public string SHIFT_LOT_NO
        {
            get
            {
                return _SHIFT_LOT_NO;
            }
            set
            {
                if (_SHIFT_LOT_NO == value)
                    return;
                _SHIFT_LOT_NO = value;
            }
        }
        public string SHIFT_ID
        {
            get
            {
                return _SHIFT_ID;
            }
            set
            {
                if (_SHIFT_ID == value)
                    return;
                _SHIFT_ID = value;
            }
        }
        public DateTime? SHIFT_DATE
        {
            get
            {
                return _SHIFT_DATE;
            }
            set
            {
                if (_SHIFT_DATE == value)
                    return;
                _SHIFT_DATE = value;
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
        public int QTY_PER_LABEL
        {
            get
            {
                return _QTY_PER_LABEL;
            }
            set
            {
                if (_QTY_PER_LABEL == value)
                    return;
                _QTY_PER_LABEL = value;
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
        public int FLAG
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
