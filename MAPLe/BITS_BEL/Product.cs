using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class Product
    {
        public Product()
        {
            //constructor
        }

        #region "Private Member"

        
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private string _MATERIAL_TYPE;
        private string _MATERIAL_NAME;
        private string _PRODUCTION_TYPE;
        private string _MC_NO;
        private int _BOX_QTY;
        private string _UNIT;
        private string _REMARK;
        private Bitmap _PROD_IMAGE;
        private bool _REC_STAT;
        private int _FREE_STOCK;

        //add new field 07-Jun-2011
        private string _CUST_PROD_NO;
        private decimal _BUYER_PRICE;
        private decimal _SELLING_PRICE;
        private decimal _COST_PRICE;

        #endregion

        #region "Property Member"

        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
            }
        }
        public string PRODUCT_NO
        {
            get
            {
                return _PRODUCT_NO;
            }
            set
            {
                if (_PRODUCT_NO == value)
                    return;
                _PRODUCT_NO = value;
            }
        }
        public string PRODUCT_NAME
        {
            get
            {
                return _PRODUCT_NAME;
            }
            set
            {
                if (_PRODUCT_NAME == value)
                    return;
                _PRODUCT_NAME = value;
            }
        }
        public string MATERIAL_TYPE
        {
            get
            {
                return _MATERIAL_TYPE;
            }
            set
            {
                if (_MATERIAL_TYPE == value)
                    return;
                _MATERIAL_TYPE = value;
            }
        }

        public string MATERIAL_NAME
        {
            get
            {
                return _MATERIAL_NAME;
            }
            set
            {
                if (_MATERIAL_NAME == value)
                    return;
                _MATERIAL_NAME = value;
            }
        }

        public string PRODUCTION_TYPE
        {
            get
            {
                return _PRODUCTION_TYPE;
            }
            set
            {
                if (_PRODUCTION_TYPE == value)
                    return;
                _PRODUCTION_TYPE = value;
            }
        }
        public string MC_NO
        {
            get
            {
                return _MC_NO;
            }
            set
            {
                if (_MC_NO == value)
                    return;
                _MC_NO = value;
            }
        }

        public int BOX_QTY
        {
            get
            {
                return _BOX_QTY;
            }
            set
            {
                if (_BOX_QTY == value)
                    return;
                _BOX_QTY = value;
            }
        }

        public string UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                if (_UNIT == value)
                    return;
                _UNIT = value;
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
        public Bitmap PROD_IMAGE
        {
            get
            {
                return _PROD_IMAGE;
            }
            set
            {
                if (_PROD_IMAGE == value)
                    return;
                _PROD_IMAGE = value;
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
        public int FREE_STOCK
        {
            get
            {
                return _FREE_STOCK;
            }
            set
            {
                if (_FREE_STOCK == value)
                    return;
                _FREE_STOCK = value;
            }
        }

        //add new Property 07-Jun-2011

        public string CUST_PROD_NO
        {
            get
            {
                return _CUST_PROD_NO;
            }
            set
            {
                if (_CUST_PROD_NO == value)
                    return;
                _CUST_PROD_NO = value;
            }
        }
        public decimal BUYER_PRICE
        {
            get
            {
                return _BUYER_PRICE;
            }
            set
            {
                if (_BUYER_PRICE == value)
                    return;
                _BUYER_PRICE = value;
            }
        }
        public decimal SELLING_PRICE
        {
            get
            {
                return _SELLING_PRICE;
            }
            set
            {
                if (_SELLING_PRICE == value)
                    return;
                _SELLING_PRICE = value;
            }
        }
        public decimal COST_PRICE
        {
            get
            {
                return _COST_PRICE;
            }
            set
            {
                if (_COST_PRICE == value)
                    return;
                _COST_PRICE = value;
            }
        }

        #endregion
    }
}
