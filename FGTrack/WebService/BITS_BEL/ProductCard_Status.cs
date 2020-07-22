using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    public class ProductCard_Status : ProductCard
    {
        public ProductCard_Status()
        { }

        private string _STATUS;
        private DateTime? _PROCESS_DATE;
        private int _REP_QTY;

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
        public DateTime? PROCESS_DATE
        {
            get
            {
                return _PROCESS_DATE;
            }
            set
            {
                if (_PROCESS_DATE == value)
                    return;
                _PROCESS_DATE = value;
            }
        }
        public int REP_QTY
        {
            get
            {
                return _REP_QTY;
            }
            set
            {
                if (_REP_QTY == value)
                    return;
                _REP_QTY = value;
            }
        }
    }

    public class ProductCardStatusFG : ProductCard
    {
        public ProductCardStatusFG()
        {
            
        }

        private string _WH;
        private string _STATUS;
        private string _PROCESS_NO;
        private DateTime? _PROCESS_DATE;
        private string _ORI_LABEL;
        private int _BREAK_QTY;


        public string WH
        {
            get
            {
                return _WH;
            }
            set
            {
                if (_WH == value)
                    return;
                _WH = value;
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
        public string PROCESS_NO
        {
            get
            {
                return _PROCESS_NO;
            }
            set
            {
                if (_PROCESS_NO == value)
                    return;
                _PROCESS_NO = value;
            }
        }
        public DateTime? PROCESS_DATE
        {
            get
            {
                return _PROCESS_DATE;
            }
            set
            {
                if (_PROCESS_DATE == value)
                    return;
                _PROCESS_DATE = value;
            }
        }
        public string ORI_LABEL
        {
            get
            {
                return _ORI_LABEL;
            }
            set
            {
                if (_ORI_LABEL == value)
                    return;
                _ORI_LABEL = value;
            }
        }
        public int BREAK_QTY
        {
            get
            {
                return _BREAK_QTY;
            }
            set
            {
                if (_BREAK_QTY == value)
                    return;
                _BREAK_QTY = value;
            }
        }

    }
}
