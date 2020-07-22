using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class ULPlanDetail
    {
        #region "Private Member"

        private int _PLAN_DTL_ID;
        private string _PLAN_NO;
        private string _MC_SIZE_TON;
        private string _PDTL_BLOCK;
        private string _MC_NO;
        private string _PDTL_SEQUENCE;
        private string _PRODUCT_NO;
        private string _PARTNAME;
        private string _MAT_TYPE;
        private string _INSERT_1;
        private string _INSERT_2;
        private string _INSERT_3;
        private int _CAV_ACT;
        private int _CAV_FULL;
        private DateTime? _MP_START;
        private DateTime? _MP_FINISH;
        private decimal _PLAN_MP_DAY;
        private decimal _PRO_SHOT_WEIGHT;
        private decimal _CYCLE_TIME;
        private int _QTY_DAY;
        private decimal _TOTAL_MAT_USE_KG;
        private decimal _TPCT_LOSS;
        private int _QTY_PLAN;
        private decimal _PLAN_MAT_AVG_DAY_KG;
        private decimal _MAT_DRY;
        private int _TARGET_DAY;
        private string _PDTL_REMARK;
        private string _PARTY_ID;
        private string _PROD_LOT;
        private string _N_USER_ID;
        private DateTime? _N_USER_DATE;
        private string _U_USER_ID;
        private DateTime? _U_USER_DATE;
        private bool _REC_STAT;
        private string _PROD_SEQ_NO;
        
        private bool _CHANGE_MOLD;
        private bool _CONTINUE_ORDER;
        private bool _REVISED_PLAN;

        private string _PLAN_STAT;
        private string _JOB_NO;

        private int _FLAG;
        private string _MC_NAME;
        #endregion

        #region "Property Member"


        public int PLAN_DTL_ID
        {
            get
            {
                return _PLAN_DTL_ID;
            }
            set
            {
                if (_PLAN_DTL_ID == value)
                    return;
                _PLAN_DTL_ID = value;
            }
        }
        public string PLAN_NO
        {
            get
            {
                return _PLAN_NO;
            }
            set
            {
                if (_PLAN_NO == value)
                    return;
                _PLAN_NO = value;
            }
        }
        public string MC_SIZE_TON
        {
            get
            {
                return _MC_SIZE_TON;
            }
            set
            {
                if (_MC_SIZE_TON == value)
                    return;
                _MC_SIZE_TON = value;
            }
        }
        public string PDTL_BLOCK
        {
            get
            {
                return _PDTL_BLOCK;
            }
            set
            {
                if (_PDTL_BLOCK == value)
                    return;
                _PDTL_BLOCK = value;
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
        public string PDTL_SEQUENCE
        {
            get
            {
                return _PDTL_SEQUENCE;
            }
            set
            {
                if (_PDTL_SEQUENCE == value)
                    return;
                _PDTL_SEQUENCE = value;
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
        public string PARTNAME
        {
            get
            {
                return _PARTNAME;
            }
            set
            {
                if (_PARTNAME == value)
                    return;
                _PARTNAME = value;
            }
        }
        public string MAT_TYPE
        {
            get
            {
                return _MAT_TYPE;
            }
            set
            {
                if (_MAT_TYPE == value)
                    return;
                _MAT_TYPE = value;
            }
        }
        public string INSERT_1
        {
            get
            {
                return _INSERT_1;
            }
            set
            {
                if (_INSERT_1 == value)
                    return;
                _INSERT_1 = value;
            }
        }
        public string INSERT_2
        {
            get
            {
                return _INSERT_2;
            }
            set
            {
                if (_INSERT_2 == value)
                    return;
                _INSERT_2 = value;
            }
        }
        public string INSERT_3
        {
            get
            {
                return _INSERT_3;
            }
            set
            {
                if (_INSERT_3 == value)
                    return;
                _INSERT_3 = value;
            }
        }
        public int CAV_ACT
        {
            get
            {
                return _CAV_ACT;
            }
            set
            {
                if (_CAV_ACT == value)
                    return;
                _CAV_ACT = value;
            }
        }
        public int CAV_FULL
        {
            get
            {
                return _CAV_FULL;
            }
            set
            {
                if (_CAV_FULL == value)
                    return;
                _CAV_FULL = value;
            }
        }
        public DateTime? MP_START
        {
            get
            {
                return _MP_START;
            }
            set
            {
                if (_MP_START == value)
                    return;
                _MP_START = value;
            }
        }
        public DateTime? MP_FINISH
        {
            get
            {
                return _MP_FINISH;
            }
            set
            {
                if (_MP_FINISH == value)
                    return;
                _MP_FINISH = value;
            }
        }
        public decimal PLAN_MP_DAY
        {
            get
            {
                return _PLAN_MP_DAY;
            }
            set
            {
                if (_PLAN_MP_DAY == value)
                    return;
                _PLAN_MP_DAY = value;
            }
        }
        public decimal PRO_SHOT_WEIGHT
        {
            get
            {
                return _PRO_SHOT_WEIGHT;
            }
            set
            {
                if (_PRO_SHOT_WEIGHT == value)
                    return;
                _PRO_SHOT_WEIGHT = value;
            }
        }
        public decimal CYCLE_TIME
        {
            get
            {
                return _CYCLE_TIME;
            }
            set
            {
                if (_CYCLE_TIME == value)
                    return;
                _CYCLE_TIME = value;
            }
        }
        public int QTY_DAY
        {
            get
            {
                return _QTY_DAY;
            }
            set
            {
                if (_QTY_DAY == value)
                    return;
                _QTY_DAY = value;
            }
        }
        public decimal TOTAL_MAT_USE_KG
        {
            get
            {
                return _TOTAL_MAT_USE_KG;
            }
            set
            {
                if (_TOTAL_MAT_USE_KG == value)
                    return;
                _TOTAL_MAT_USE_KG = value;
            }
        }
        public decimal TPCT_LOSS
        {
            get
            {
                return _TPCT_LOSS;
            }
            set
            {
                if (_TPCT_LOSS == value)
                    return;
                _TPCT_LOSS = value;
            }
        }
        public int QTY_PLAN
        {
            get
            {
                return _QTY_PLAN;
            }
            set
            {
                if (_QTY_PLAN == value)
                    return;
                _QTY_PLAN = value;
            }
        }
        public decimal PLAN_MAT_AVG_DAY_KG
        {
            get
            {
                return _PLAN_MAT_AVG_DAY_KG;
            }
            set
            {
                if (_PLAN_MAT_AVG_DAY_KG == value)
                    return;
                _PLAN_MAT_AVG_DAY_KG = value;
            }
        }
        public decimal MAT_DRY
        {
            get
            {
                return _MAT_DRY;
            }
            set
            {
                if (_MAT_DRY == value)
                    return;
                _MAT_DRY = value;
            }
        }
        public int TARGET_DAY
        {
            get
            {
                return _TARGET_DAY;
            }
            set
            {
                if (_TARGET_DAY == value)
                    return;
                _TARGET_DAY = value;
            }
        }
        public string PDTL_REMARK
        {
            get
            {
                return _PDTL_REMARK;
            }
            set
            {
                if (_PDTL_REMARK == value)
                    return;
                _PDTL_REMARK = value;
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
        public string PROD_LOT
        {
            get
            {
                return _PROD_LOT;
            }
            set
            {
                if (_PROD_LOT == value)
                    return;
                _PROD_LOT = value;
            }
        }
        public string N_USER_ID
        {
            get
            {
                return _N_USER_ID;
            }
            set
            {
                if (_N_USER_ID == value)
                    return;
                _N_USER_ID = value;
            }
        }
        public DateTime? N_USER_DATE
        {
            get
            {
                return _N_USER_DATE;
            }
            set
            {
                if (_N_USER_DATE == value)
                    return;
                _N_USER_DATE = value;
            }
        }
        public string U_USER_ID
        {
            get
            {
                return _U_USER_ID;
            }
            set
            {
                if (_U_USER_ID == value)
                    return;
                _U_USER_ID = value;
            }
        }
        public DateTime? U_USER_DATE
        {
            get
            {
                return _U_USER_DATE;
            }
            set
            {
                if (_U_USER_DATE == value)
                    return;
                _U_USER_DATE = value;
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
        public bool CHANGE_MOLD
        {
            get
            {
                return _CHANGE_MOLD;
            }
            set
            {
                if (_CHANGE_MOLD == value)
                    return;
                _CHANGE_MOLD = value;
            }
        }
        public bool CONTINUE_ORDER
        {
            get
            {
                return _CONTINUE_ORDER;
            }
            set
            {
                if (_CONTINUE_ORDER == value)
                    return;
                _CONTINUE_ORDER = value;
            }
        }
        public bool REVISED_PLAN
        {
            get
            {
                return _REVISED_PLAN;
            }
            set
            {
                if (_REVISED_PLAN == value)
                    return;
                _REVISED_PLAN = value;
            }
        }

        public string PLAN_STAT
        {
            get
            {
                return _PLAN_STAT;
            }
            set
            {
                if (_PLAN_STAT == value)
                    return;
                _PLAN_STAT = value;
            }
        }
        public string JOB_NO
        {
            get
            {
                return _JOB_NO;
            }
            set
            {
                if (_JOB_NO == value)
                    return;
                _JOB_NO = value;
            }
        }
        public int FLAG
        {
            get
            {
                return _FLAG;
            }
            set
            {
                if (_FLAG == value)
                    return;
                _FLAG = value;
            }
        }

        public string MC_NAME
        {
            get
            {
                return _MC_NAME;
            }
            set
            {
                if (_MC_NAME == value)
                    return;
                _MC_NAME = value;
            }
        }
        #endregion
    }
}
