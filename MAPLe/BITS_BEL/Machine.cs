using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class Machine
    {
        public Machine()
        {
        }

        #region "Private Member"
        
        private string _MC_NO;
        private string _MACHINE_NAME;
        private string _MACHINE_TYPE;
        private string _MACHINE_SIZE;
        private string _REMARK;
        private bool _REC_STAT;
        
        #endregion

        #region "Property Member"

        
        public string MC_NO
        {
            get
            {
                return _MC_NO;
            }
            set
            {
                if (_MC_NO == value)
                    return;
                _MC_NO = value;
            }
        }
        public string MACHINE_NAME
        {
            get
            {
                return _MACHINE_NAME;
            }
            set
            {
                if (_MACHINE_NAME == value)
                    return;
                _MACHINE_NAME = value;
            }
        }
        public string MACHINE_TYPE
        {
            get
            {
                return _MACHINE_TYPE;
            }
            set
            {
                if (_MACHINE_TYPE == value)
                    return;
                _MACHINE_TYPE = value;
            }
        }
        public string MACHINE_SIZE
        {
            get
            {
                return _MACHINE_SIZE;
            }
            set
            {
                if (_MACHINE_SIZE == value)
                    return;
                _MACHINE_SIZE = value;
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
