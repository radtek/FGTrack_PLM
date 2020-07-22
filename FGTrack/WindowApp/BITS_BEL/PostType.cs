using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class PostType
    {
        public PostType()
        {
        }

        #region "Variable Member"

        private string _SEQ_NO;
        private string _NAME;

        #endregion

        #region "Property Member"

        public string SEQ_NO
        {
            get
            {
                return _SEQ_NO;
            }
            set
            {
                if (_SEQ_NO == value)
                    return;
                _SEQ_NO = value;
            }
        }
        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                if (_NAME == value)
                    return;
                _NAME = value;
            }
        }

        #endregion
    }
}
