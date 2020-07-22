using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class T_ARRIVAL_DTL
    {

        public T_ARRIVAL_DTL()
        {
        }

        #region Variable Member

        private string _ARRIVAL_NO;
        private int _LINE_NO;
        private string _MTL_SEQ_NO;
        private string _MTL_CODE;
        private string _UNIT_ID;
        private decimal _QTY;
        private decimal _REC_QTY;
        private string _REMARK;
        private bool _REC_STAT;
        private string _MTL_NAME;
        private int _FLAG;
        private string _STATUS;
        private string _GEN_LABEL_STATUS;
        private int _NO_OF_LABEL;
        private DateTime? _LOT_DATE;

        private string _MTL_GRADE;
        private string _MTL_COLOR;
       

        #endregion

        #region Property Member

        public string ARRIVAL_NO
        {
            get
            {
                return _ARRIVAL_NO;
            }
            set
            {
                if (_ARRIVAL_NO == value)
                    return;
                _ARRIVAL_NO = value;
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
        public string MTL_SEQ_NO
        {
            get
            {
                return _MTL_SEQ_NO;
            }
            set
            {
                if (_MTL_SEQ_NO == value)
                    return;
                _MTL_SEQ_NO = value;
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
        public decimal QTY
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

        public string MTL_NAME
        {
            get
            {
                return _MTL_NAME;
            }
            set
            {
                if (_MTL_NAME == value)
                    return;
                _MTL_NAME = value;
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

        public string STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                if (_STATUS == value)
                    return;
                _STATUS = value;
            }
        }

        public string GEN_LABEL_STATUS
        {
            get
            {
                return _GEN_LABEL_STATUS;
            }
            set
            {
                if (_GEN_LABEL_STATUS == value)
                    return;
                _GEN_LABEL_STATUS = value;
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

        public DateTime? LOT_DATE
        {
            get
            {
                return _LOT_DATE;
            }
            set
            {
                if (_LOT_DATE == value)
                    return;
                _LOT_DATE = value;
            }
        }

        public decimal REC_QTY
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

        public string MTL_COLOR
        {
            get
            {
                return _MTL_COLOR;
            }
            set
            {
                if (_MTL_COLOR == value)
                    return;
                _MTL_COLOR = value;
            }
        }

        #endregion


    }
}
