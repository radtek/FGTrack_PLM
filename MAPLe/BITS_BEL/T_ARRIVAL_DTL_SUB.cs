using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class T_ARRIVAL_DTL_SUB
    {
        public T_ARRIVAL_DTL_SUB()
        {
        }

        #region Variable Member

          private string _ARRIVAL_NO;
          private string _MTL_SEQ_NO;
          private int _LINE_NO;
          private int _LINE_NO_SUB;
          private decimal _STD_QTY;
          private decimal _QTY;
          private string _UNIT;
          private string _U_USER_ID;
          private bool _REC_STAT;
          private decimal _TOTAL_QTY;
          private decimal _DOC_PKG_QTY;

        #endregion

        #region Property Member

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
          public int LINE_NO_SUB
          {
              get
              {
                  return _LINE_NO_SUB;
              }
              set
              {
                  if (_LINE_NO_SUB == value)
                      return;
                  _LINE_NO_SUB = value;
              }
          }
          public decimal STD_QTY
          {
              get
              {
                  return _STD_QTY;
              }
              set
              {
                  if (_STD_QTY == value)
                      return;
                  _STD_QTY = value;
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

          public decimal TOTAL_QTY
          {
              get
              {
                  return _TOTAL_QTY;
              }
              set
              {
                  if (_TOTAL_QTY == value)
                      return;
                  _TOTAL_QTY = value;
              }
          }

          public decimal DOC_PKG_QTY
          {
              get
              {
                  return _DOC_PKG_QTY;
              }
              set
              {
                  if (_DOC_PKG_QTY == value)
                      return;
                  _DOC_PKG_QTY = value;
              }
          }

        #endregion
    }
}
