using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    [Serializable]
    public class LoadQty
    {
        public LoadQty()
        {
        }

        #region "Private Member"

        private string _LOADING_NO;
        private int _LOADED_BOX;
        private int _LOADED_QTY;

        #endregion

        #region "Property Member"

        public string LOADING_NO
        {
            get
            {
                return _LOADING_NO;
            }
            set
            {
                if (_LOADING_NO == value)
                    return;
                _LOADING_NO = value;
            }
        }
        public int LOADED_BOX
        {
            get
            {
                return _LOADED_BOX;
            }
            set
            {
                if (_LOADED_BOX == value)
                    return;
                _LOADED_BOX = value;
            }
        }
        public int LOADED_QTY
        {
            get
            {
                return _LOADED_QTY;
            }
            set
            {
                if (_LOADED_QTY == value)
                    return;
                _LOADED_QTY = value;
            }
        }
 
        #endregion
    
    }
}
