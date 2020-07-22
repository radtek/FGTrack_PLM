using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ProdProcess
    {
        public ProdProcess()
        {
        }

        private string _PROD_SEQ_NO;
        private string _PROCESS_NO;
        private string _PROCESS_NAME;
        private int _STEP_NO;
        private bool _REC_STAT;
        private int _FLAG;

        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
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
        public string PROCESS_NAME
        {
            get
            {
                return _PROCESS_NAME;
            }
            set
            {
                if (_PROCESS_NAME == value)
                    return;
                _PROCESS_NAME = value;
            }
        }
        public int STEP_NO
        {
            get
            {
                return _STEP_NO;
            }
            set
            {
                if (_STEP_NO == value)
                    return;
                _STEP_NO = value;
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
    }
}
