using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    [Serializable]
    public class JobLot
    {
        public JobLot()
        {
        }

        #region "Private Member"

        private string _JOB_NO;
        private int _LINE_NO;
        private string _JOB_LOT;

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
        #endregion
    
    }
}
