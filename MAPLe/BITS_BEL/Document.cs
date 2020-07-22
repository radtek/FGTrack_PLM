using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class Document
    {
        public Document()
        {

        }

        #region "Private Member"

        private string _DOC_NO;
        private string _DOC_DATE;

        #endregion

        #region "Property Member"

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
        public string DOC_DATE
        {
            get
            {
                return _DOC_DATE;
            }
            set
            {
                if (_DOC_DATE == value)
                    return;
                _DOC_DATE = value;
            }
        }

        #endregion
    }
}
