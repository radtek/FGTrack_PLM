using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HTN.BITS.BEL.PLASESS
{
    public class MaterialCard
    {
        public MaterialCard()
        {
        }

        #region "Private Member"

        
        private Bitmap _MTL_IMAGE;      
        private string _SERIAL_NO ;
        private int _REC_NO ;
        private string _WH_ID;
        private string _ARRIVAL_NO ;
        private int _LINE_NO ;
        private DateTime? _LOT_DATE ;
        private string _MTL_SEQ_NO;
        private string _MTL_CODE;
        private string _MTL_NAME;
        private string _MTL_GRADE;
        private string _MTL_COLOR;
        private string _LOCATION_NAME;
        private string _UNIT_ID ;
        private decimal _QTY;
        private string _CARGO_STATUS ;
        private string _PIC_STOCK_IN;
        private DateTime? _STOCK_IN_DATE;
        private string _PIC_STOCK_OUT;
        private DateTime? _STOCK_OUT_DATE ;
        private string _ORDER_CARD_NO;
        private decimal _OUT_QTY;
        private int _NO_OF_PRINT;
        private string _PIC_LAST_PRINT;
        private DateTime? _PRINT_LAST_DATE;
        private string _REMARK;
        private string _N_USER_ID;
        private string _U_USER_ID;
        private string _REC_STAT;
        private string _PARTY_ID;
        private string _PARTY_NAME;
        private string _NO_OF_LABEL;

        private decimal _MIN_QTY;
        private decimal _MAX_QTY;

        #endregion

        #region "Property Member"


              //public Bitmap BARCODE
              //{
              //    get
              //    {
              //        return _BARCODE;
              //    }
              //    set
              //    {
              //        if (_BARCODE == value)
              //            return;
              //        _BARCODE = value;
              //    }
              //}
              public Bitmap MTL_IMAGE
              {
                  get
                  {
                      return _MTL_IMAGE;
                  }
                  set
                  {
                      if (_MTL_IMAGE == value)
                          return;
                      _MTL_IMAGE = value;
                  }
              }
              public string SERIAL_NO
              {
                  get
                  {
                      return _SERIAL_NO;
                  }
                  set
                  {
                      if (_SERIAL_NO == value)
                          return;
                      _SERIAL_NO = value;
                  }
              }
              public int REC_NO
              {
                  get
                  {
                      return _REC_NO;
                  }
                  set
                  {
                      if (_REC_NO == value)
                          return;
                      _REC_NO = value;
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
              public string ARRIVAL_NO
              {
                  get
                  {
                      return _ARRIVAL_NO;
                  }
                  set
                  {
                      if (_ARRIVAL_NO == value)
                          return;
                      _ARRIVAL_NO = value;
                  }
              }
              public int LINE_NO
              {
                  get
                  {
                      return _LINE_NO;
                  }
                  set
                  {
                      if (_LINE_NO == value)
                          return;
                      _LINE_NO = value;
                  }
              }
              public DateTime? LOT_DATE
              {
                  get
                  {
                      return _LOT_DATE;
                  }
                  set
                  {
                      if (_LOT_DATE == value)
                          return;
                      _LOT_DATE = value;
                  }
              }
              public string MTL_SEQ_NO
              {
                  get
                  {
                      return _MTL_SEQ_NO;
                  }
                  set
                  {
                      if (_MTL_SEQ_NO == value)
                          return;
                      _MTL_SEQ_NO = value;
                  }
              }

              public string MTL_CODE
              {
                  get
                  {
                      return _MTL_CODE;
                  }
                  set
                  {
                      if (_MTL_CODE == value)
                          return;
                      _MTL_CODE = value;
                  }
              }

              public string MTL_NAME
              {
                  get
                  {
                      return _MTL_NAME;
                  }
                  set
                  {
                      if (_MTL_NAME == value)
                          return;
                      _MTL_NAME = value;
                  }
              }

              public string MTL_GRADE
              {
                  get
                  {
                      return _MTL_GRADE;
                  }
                  set
                  {
                      if (_MTL_GRADE == value)
                          return;
                      _MTL_GRADE = value;
                  }
              }

              public string MTL_COLOR
              {
                  get
                  {
                      return _MTL_COLOR;
                  }
                  set
                  {
                      if (_MTL_COLOR == value)
                          return;
                      _MTL_COLOR = value;
                  }
              }

              public string LOCATION_NAME
              {
                  get
                  {
                      return _LOCATION_NAME;
                  }
                  set
                  {
                      if (_LOCATION_NAME == value)
                          return;
                      _LOCATION_NAME = value;
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
              public decimal QTY
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
              public string CARGO_STATUS
              {
                  get
                  {
                      return _CARGO_STATUS;
                  }
                  set
                  {
                      if (_CARGO_STATUS == value)
                          return;
                      _CARGO_STATUS = value;
                  }
              }
              public string PIC_STOCK_IN
              {
                  get
                  {
                      return _PIC_STOCK_IN;
                  }
                  set
                  {
                      if (_PIC_STOCK_IN == value)
                          return;
                      _PIC_STOCK_IN = value;
                  }
              }
              public DateTime? STOCK_IN_DATE
              {
                  get
                  {
                      return _STOCK_IN_DATE;
                  }
                  set
                  {
                      if (_STOCK_IN_DATE == value)
                          return;
                      _STOCK_IN_DATE = value;
                  }
              }
              public string PIC_STOCK_OUT
              {
                  get
                  {
                      return _PIC_STOCK_OUT;
                  }
                  set
                  {
                      if (_PIC_STOCK_OUT == value)
                          return;
                      _PIC_STOCK_OUT = value;
                  }
              }
              public DateTime? STOCK_OUT_DATE
              {
                  get
                  {
                      return _STOCK_OUT_DATE;
                  }
                  set
                  {
                      if (_STOCK_OUT_DATE == value)
                          return;
                      _STOCK_OUT_DATE = value;
                  }
              }
              public string ORDER_CARD_NO
              {
                  get
                  {
                      return _ORDER_CARD_NO;
                  }
                  set
                  {
                      if (_ORDER_CARD_NO == value)
                          return;
                      _ORDER_CARD_NO = value;
                  }
              }
              public decimal OUT_QTY
              {
                  get
                  {
                      return _OUT_QTY;
                  }
                  set
                  {
                      if (_OUT_QTY == value)
                          return;
                      _OUT_QTY = value;
                  }
              }
              public int NO_OF_PRINT
              {
                  get
                  {
                      return _NO_OF_PRINT;
                  }
                  set
                  {
                      if (_NO_OF_PRINT == value)
                          return;
                      _NO_OF_PRINT = value;
                  }
              }
              public string PIC_LAST_PRINT
              {
                  get
                  {
                      return _PIC_LAST_PRINT;
                  }
                  set
                  {
                      if (_PIC_LAST_PRINT == value)
                          return;
                      _PIC_LAST_PRINT = value;
                  }
              }
              public DateTime? PRINT_LAST_DATE
              {
                  get
                  {
                      return _PRINT_LAST_DATE;
                  }
                  set
                  {
                      if (_PRINT_LAST_DATE == value)
                          return;
                      _PRINT_LAST_DATE = value;
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
              public string REC_STAT
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

              public string NO_OF_LABEL
              {
                  get
                  {
                      return _NO_OF_LABEL;
                  }
                  set
                  {
                      if (_NO_OF_LABEL == value)
                          return;
                      _NO_OF_LABEL = value;
                  }
              }

              public decimal MIN_QTY
              {
                  get
                  {
                      return _MIN_QTY;
                  }
                  set
                  {
                      if (_MIN_QTY == value)
                          return;
                      _MIN_QTY = value;
                  }
              }

              public decimal MAX_QTY
              {
                  get
                  {
                      return _MAX_QTY;
                  }
                  set
                  {
                      if (_MAX_QTY == value)
                          return;
                      _MAX_QTY = value;
                  }
              }

        #endregion

    }
}
