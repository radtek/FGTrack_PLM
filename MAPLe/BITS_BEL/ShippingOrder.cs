using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ShippingOrder
    {
        public ShippingOrder()
        {
            this._SHIPPING_ORD_DTL = new List<ShippingOrderDtl>();
        }

        #region "Variable Member"

        private string _SO_NO;
        private DateTime? _SO_DATE;
        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _REF_NO;
        private DateTime? _REF_DATE;
        private DateTime? _ETA;
        private string _STATUS;
        private string _WH_ID;
        private string _REMARK;
        private bool _REC_STAT;

        //add new variable on 07-Jun-2011
        private int _QTY_PCS;
        private int _QTY_BOX;
        private string _POST_REF;
        private string _SALES_ORDER_NO;

        private List<ShippingOrderDtl> _SHIPPING_ORD_DTL;

        #endregion

        #region "Property Member"

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
        public DateTime? SO_DATE
        {
            get
            {
                return _SO_DATE;
            }
            set
            {
                if (_SO_DATE == value)
                    return;
                _SO_DATE = value;
            }
        }
        public string PARTY_ID
        {
            get
            {
                return _PARTY_ID;
            }
            set
            {
                if (_PARTY_ID == value)
                    return;
                _PARTY_ID = value;
            }
        }
        public string PARTY_NAME
        {
            get
            {
                return _PARTY_NAME;
            }
            set
            {
                if (_PARTY_NAME == value)
                    return;
                _PARTY_NAME = value;
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
        public DateTime? REF_DATE
        {
            get
            {
                return _REF_DATE;
            }
            set
            {
                if (_REF_DATE == value)
                    return;
                _REF_DATE = value;
            }
        }
        public DateTime? ETA
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
        public string STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                if (_STATUS == value)
                    return;
                _STATUS = value;
            }
        }
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
        public string REMARK
        {
            get
            {
                return _REMARK;
            }
            set
            {
                if (_REMARK == value)
                    return;
                _REMARK = value;
            }
        }
        public bool REC_STAT
        {
            get
            {
                return _REC_STAT;
            }
            set
            {
                if (_REC_STAT == value)
                    return;
                _REC_STAT = value;
            }
        }

        public List<ShippingOrderDtl> SHIPPING_ORD_DTL
        {
            get
            {
                return this._SHIPPING_ORD_DTL;
            }
        }

        //add new Property on 07-Jun-2011
        public int QTY_PCS
        {
            get
            {
                return _QTY_PCS;
            }
            set
            {
                if (_QTY_PCS == value)
                    return;
                _QTY_PCS = value;
            }
        }
        public int QTY_BOX
        {
            get
            {
                return _QTY_BOX;
            }
            set
            {
                if (_QTY_BOX == value)
                    return;
                _QTY_BOX = value;
            }
        }
        public string POST_REF
        {
            get
            {
                return _POST_REF;
            }
            set
            {
                if (_POST_REF == value)
                    return;
                _POST_REF = value;
            }
        }
        public string SALES_ORDER_NO
        {
            get
            {
                return _SALES_ORDER_NO;
            }
            set
            {
                if (_SALES_ORDER_NO == value)
                    return;
                _SALES_ORDER_NO = value;
            }
        }
        #endregion

        #region "Method Member"

        public void AddItem(ShippingOrderDtl detail)
        {
            this._SHIPPING_ORD_DTL.Add(detail);
        }

        #endregion
    }
}
