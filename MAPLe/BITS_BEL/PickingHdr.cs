using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class PickingHdr
    {
        public PickingHdr()
        {
        }

        #region "Variable Member"

        private string _PICK_NO;
        private string _SO_NO;
        private string _CUSTOMER_NAME;
        private string _ETA;
        private string _REF_NO;
        

        #endregion

        #region "Property Member"
        public string PICK_NO
        {
            get
            {
                return _PICK_NO;
            }
            set
            {
                if (_PICK_NO == value)
                    return;
                _PICK_NO = value;
            }
        }
        public string SO_NO
        {
            get
            {
                return _SO_NO;
            }
            set
            {
                if (_SO_NO == value)
                    return;
                _SO_NO = value;
            }
        }
        public string CUSTOMER_NAME
        {
            get
            {
                return _CUSTOMER_NAME;
            }
            set
            {
                if (_CUSTOMER_NAME == value)
                    return;
                _CUSTOMER_NAME = value;
            }
        }
        public string ETA
        {
            get
            {
                return _ETA;
            }
            set
            {
                if (_ETA == value)
                    return;
                _ETA = value;
            }
        }
        public string REF_NO
        {
            get
            {
                return _REF_NO;
            }
            set
            {
                if (_REF_NO == value)
                    return;
                _REF_NO = value;
            }
        }
        
       

        #endregion
    }
}
