using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class LoadingOrder
    {
        public LoadingOrder()
        {

        }

        #region "Variable Member"

        private string _WH_ID;
        private string _LOADING_NO;
        private DateTime _LOADING_DATE;
        private DateTime _DELIVERY_DATE;
        private string _TRUCK_NO;
        private string _CONTAINER_NO;
        private string _REMARK;
        private bool _REC_STAT;
        

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
        public DateTime LOADING_DATE
        {
            get
            {
                return _LOADING_DATE;
            }
            set
            {
                if (_LOADING_DATE == value)
                    return;
                _LOADING_DATE = value;
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
        public string TRUCK_NO
        {
            get
            {
                return _TRUCK_NO;
            }
            set
            {
                if (_TRUCK_NO == value)
                    return;
                _TRUCK_NO = value;
            }
        }
        public string CONTAINER_NO
        {
            get
            {
                return _CONTAINER_NO;
            }
            set
            {
                if (_CONTAINER_NO == value)
                    return;
                _CONTAINER_NO = value;
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
        
        #endregion










    }
}
