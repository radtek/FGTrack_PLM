using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    public class TransferOrderInfo
    {
        public TransferOrderInfo()
        {
        }

        #region Variable Member

        private string _TO_NO;

        #endregion

        #region Property Member

        public string TO_NO
        {
            get
            {
                return _TO_NO;
            }
            set
            {
                if (_TO_NO == value)
                    return;
                _TO_NO = value;
            }
        }

        #endregion
    }
}
