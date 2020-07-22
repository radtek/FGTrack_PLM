using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{
    public class T_CARGO
    {
        public string LABEL_ID { get; set; }
        public string PARTY_ID { get; set; }
        public string CUST_NAME { get; set; }
        public DateTime? RECEIVING_DATE { get; set; }
        public string DOC_ID { get; set; }
        public string DOCUMENT_NO { get; set; }
        public int LINE_NO { get; set; }
        public string ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public int RECEIVING_QTY { get; set; }
        public string RECEVING_UNIT { get; set; }
        public string LOCATION_CODE { get; set; }
        public DateTime? LOCATION_DATE { get; set; }
        public string LABEL_STATUS { get; set; }
        public int ASSIGNED_QTY { get; set; }
        public int PICK_QTY { get; set; }
        public int LOAD_QTY { get; set; }
        public int SHORT_QTY { get; set; }
        public int BREAK_QTY { get; set; }
        public int QTY_PER_LABEL { get; set; }
        public int LABEL_QTY { get; set; }
        public int DOCUMENT_QTY { get; set; }
        public int BALANCE_QTY { get; set; }
        public int LOCATION_QTY { get; set; }
        public string UOM { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public bool HOLD_STATUS { get; set; }
        public string REMARK_DOC_DTL { get; set; }

        public string BRANCH_CODE { get; set; }
        public string WH_CODE { get; set; }
        public string LABEL_REF { get; set; }
        public string REMARK_LABEL { get; set; }
        public bool REC_STAT { get; set; }

        public string PARTY_NAME { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE_REF { get; set; }

        public string PICK_ID { get; set; }
        public string LOAD_ID { get; set; }

        public int CANCEL_QTY { get; set; }

        public string RESULTMSG { get; set; }
    }
}
