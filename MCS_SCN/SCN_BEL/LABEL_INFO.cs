using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class LABEL_INFO
    {
        public string LABEL_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string LOCATION_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string ARRIVAL_NO { get; set; }
        public string SO_NO { get; set; }
        public int LABEL_QTY { get; set; }
        public string LABEL_UNIT { get; set; }
        public string ITEM_UNIT { get; set; }
        public string LABEL_STATUS { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string PICK_NO { get; set; }
        public string LOAD_NO { get; set; }
        public string LOCATION { get; set; }
        public string RESULT_MSG { get; set; }
    }
}
