using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class SCN_RELOCATION_RESULT_DTL
    {
        public string LABEL_ID { get; set; }
        public string CUST_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public int LABEL_QTY { get; set; }
        public int TOTAL_QTY { get; set; }
        public string OLD_LOCATION { get; set; }
        public string NEW_LOCATION { get; set; }
        public string RESULTMSG { get; set; }
    }
}
