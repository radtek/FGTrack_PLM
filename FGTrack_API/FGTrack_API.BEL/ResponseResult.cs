using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN.BITS.FGTRACK.BEL
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
