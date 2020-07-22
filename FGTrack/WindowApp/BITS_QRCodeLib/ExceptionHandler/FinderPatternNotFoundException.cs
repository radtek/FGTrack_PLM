using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.QRCodeLib.ExceptionHandler
{
    [Serializable]
    public class FinderPatternNotFoundException : System.Exception
    {
        internal String message = null;
        public override String Message
        {
            get
            {
                return message;
            }

        }
        public FinderPatternNotFoundException(String message)
        {
            this.message = message;
        }
    }
}
