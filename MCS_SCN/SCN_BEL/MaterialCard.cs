using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MSC.SCN.BEL
{
    public class MaterialCard
    {
        public string SERIAL_NO { get; set; }
        public string PARTY_ID { get; set; }
        public string PARTY_NAME { get; set; }
        public string MTL_CODE { get; set; }
        public string MTL_NAME { get; set; }
        public string MTL_GRADE { get; set; }
        public string MTL_COLOR { get; set; }
        public decimal QTY { get; set; }
        public string UNIT_ID { get; set; }
        public string ARRIVAL_NO { get; set; }
        public string CARGO_STATUS { get; set; }
    }
}
