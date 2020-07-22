using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class RollBackDocument
    {
        public RollBackDocument()
        {
        }

        #region Variable Member

        private string _DOC_NO;
        private string _SERIAL_NO;

        #endregion

        #region Property Member

        public string DOC_NO
        {
            get
            {
                return _DOC_NO;
            }
            set
            {
                if (_DOC_NO == value)
                    return;
                _DOC_NO = value;
            }
        }
        public string SERIAL_NO
        {
            get
            {
                return _SERIAL_NO;
            }
            set
            {
                if (_SERIAL_NO == value)
                    return;
                _SERIAL_NO = value;
            }
        }

        #endregion
    }
}
