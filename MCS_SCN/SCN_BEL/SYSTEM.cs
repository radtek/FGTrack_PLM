using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.BEL
{

    public class ResponseResult
    {
        public ResponseResult()
        {
            Status = true;
            Message = String.Empty;
            Data = null;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
