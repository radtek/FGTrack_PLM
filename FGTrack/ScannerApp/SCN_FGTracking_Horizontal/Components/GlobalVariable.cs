using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.FGTRACK.HORIZONTAL.Components
{
    public class GlobalVariable
    {
        private static string languageSelect;
        public static string LanguageSelect
        {
            get
            {
                return languageSelect;
            }
            set
            {
                if (languageSelect == value)
                    return;
                languageSelect = value;
            }
        }
    }
}
