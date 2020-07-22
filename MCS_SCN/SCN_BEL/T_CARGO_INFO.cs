using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class T_CARGO_INFO
    {
        public string LABEL_ID { get; set; }
        public string ARRIVAL_NO { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_UNIT { get; set; }
        public string ITEM_PKG_UNIT { get; set; }
        public int LABEL_QTY { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public int LOCATION_QTY { get; set; }
        public int DOCUMENT_QTY { get; set; }

        /// <summary>
        /// Picking object
        /// </summary>
        public string PICK_NO { get; set; }
        public decimal PICK_QTY { get; set; }
        public decimal TOTAL_QTY { get; set; }

        public string REMARK { get; set; }
        public string RESULTMSG { get; set; }
    }
}
