using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class SCN_LOADING_RESULT_DTL
    {
        public string LOAD_NO { get; set; }
        public string SO_NO { get; set; }
        public string PICK_NO { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_REF { get; set; }
        public string CUST_NAME { get; set; }
        public string LABEL_ID { get; set; }
        public int LABEL_QTY { get; set; }
        public string ITEM_PACKAGE { get; set; }
        public int LOADED_QTY { get; set; }
        public int TOTAL_QTY { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public string RESULTMSG { get; set; }
        public string UNIT { get; set; }
    }
}
