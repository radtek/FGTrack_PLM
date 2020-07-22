using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN.BITS.FGTRACK.BLL
{
    public class UtilityBLL
    {
        public static string GetReturnRawData(OracleBinary oraValue)
        {
            string resultString = System.Text.Encoding.Default.GetString(oraValue.Value, 0, oraValue.Value.Length);
            return resultString;
        }
    }
}
