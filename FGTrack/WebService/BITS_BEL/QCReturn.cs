using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    [Serializable]
    public class QCReturn
    {
        public QCReturn()
        {
        }

        #region "Private Member"

        private string _WH_ID;
        private string _RT_NO;
        private int _NO_OF_LABEL;

        #endregion

        #region "Property Member"

        public string WH_ID
        {
            get
            {
                return _WH_ID;
            }
            set
            {
                if (_WH_ID == value)
                    return;
                _WH_ID = value;
            }
        }
        public string RT_NO
        {
            get
            {
                return _RT_NO;
            }
            set
            {
                if (_RT_NO == value)
                    return;
                _RT_NO = value;
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
        #endregion
    
    }
}
