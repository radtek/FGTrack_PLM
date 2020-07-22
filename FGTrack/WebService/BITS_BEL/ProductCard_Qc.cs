using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL
{
    public class ProductCard_Qc : ProductCard
    {
        public ProductCard_Qc()
        { }

        private int _QC_QTY;

        public int QC_QTY
        {
            get
            {
                return _QC_QTY;
            }
            set
            {
                if (_QC_QTY == value)
                    return;
                _QC_QTY = value;
            }
        }
    }
}
