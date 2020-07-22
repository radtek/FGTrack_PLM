using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    public class DeliveryOrderInfo
    {
        public DeliveryOrderInfo()
        {
        }

        #region Variable Member

        private string _DO_NO;

        #endregion

        #region Property Member

        public string DO_NO
        {
            get
            {
                return _DO_NO;
            }
            set
            {
                if (_DO_NO == value)
                    return;
                _DO_NO = value;
            }
        }

        #endregion
    }
}
