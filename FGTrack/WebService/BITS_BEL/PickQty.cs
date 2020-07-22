using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    [Serializable]
    public class PickQty
    {
        public PickQty()
        {
        }

        #region "Private Member"

        private string _PICK_NO;
        private int _QTY;
        private int _PICKED_QTY;
        private string _UNIT_ID;

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
        public int QTY
        {
            get
            {
                return _QTY;
            }
            set
            {
                if (_QTY == value)
                    return;
                _QTY = value;
            }
        }
        public int PICKED_QTY
        {
            get
            {
                return _PICKED_QTY;
            }
            set
            {
                if (_PICKED_QTY == value)
                    return;
                _PICKED_QTY = value;
            }
        }
        public string UNIT_ID
        {
            get
            {
                return _UNIT_ID;
            }
            set
            {
                if (_UNIT_ID == value)
                    return;
                _UNIT_ID = value;
            }
        }
        #endregion
    
    }
}
