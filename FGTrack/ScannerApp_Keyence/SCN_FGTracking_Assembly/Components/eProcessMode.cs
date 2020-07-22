using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.FGTRACK.ASSEMBLY.Components
{
    public enum eProcessMode
    {
        ASSEMBLY,
        INJECTION,
        TRIMMING,
        ELECTRIC,
        AllPROCESS,
        PREQC
    }

    public enum eQCStatus
    {
        OK,
        NG
    }
}
