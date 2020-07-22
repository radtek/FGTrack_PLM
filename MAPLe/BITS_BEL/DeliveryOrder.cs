using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class DeliveryOrder
    {
        public DeliveryOrder()
        {
            this._DELIVERY_ORD_DTL = new List<DeliveryOrderDtl>();
        }

        #region Variable Member

        private string _PROD_TYPE;
        private string _DO_NO;
        private DateTime _DO_DATE;
        private string _REF_NO;
        private string _TO_DEST;
        private DateTime _DELIVERY_DATE;
        private int _QTY_PCS;
        private int _QTY_BOX;
        private int _DELIVERY_QTY;
        private int _DELIVERY_BOX;
        private string _REMARK;
        private bool _REC_STAT;
        //private Bitmap _BARCODE;

        private List<DeliveryOrderDtl> _DELIVERY_ORD_DTL;

        #endregion

        #region Property Member

        public string PROD_TYPE
        {
            get
            {
                return _PROD_TYPE;
            }
            set
            {
                if (_PROD_TYPE == value)
                    return;
                _PROD_TYPE = value;
            }
        }
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
        public DateTime DO_DATE
        {
            get
            {
                return _DO_DATE;
            }
            set
            {
                if (_DO_DATE == value)
                    return;
                _DO_DATE = value;
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
        public string TO_DEST
        {
            get
            {
                return _TO_DEST;
            }
            set
            {
                if (_TO_DEST == value)
                    return;
                _TO_DEST = value;
            }
        }
        public DateTime DELIVERY_DATE
        {
            get
            {
                return _DELIVERY_DATE;
            }
            set
            {
                if (_DELIVERY_DATE == value)
                    return;
                _DELIVERY_DATE = value;
            }
        }
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
        public int DELIVERY_QTY
        {
            get
            {
                return _DELIVERY_QTY;
            }
            set
            {
                if (_DELIVERY_QTY == value)
                    return;
                _DELIVERY_QTY = value;
            }
        }
        public int DELIVERY_BOX
        {
            get
            {
                return _DELIVERY_BOX;
            }
            set
            {
                if (_DELIVERY_BOX == value)
                    return;
                _DELIVERY_BOX = value;
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
        

        public List<DeliveryOrderDtl> DELIVERY_ORD_DTL
        {
            get
            {
                return this._DELIVERY_ORD_DTL;
            }
        }

        #endregion

        #region Method Member

        public void AddItem(DeliveryOrderDtl detail)
        {
            this._DELIVERY_ORD_DTL.Add(detail);
        }

        #endregion
    }
}
