using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Globalization;

namespace HTN.BITS.UIL.PLASESS.Component
{
    public class UiValidations
    {
        public static void Validate_Detach(BaseEdit control, ref DXErrorProvider ctlErrorProvider, string msg, ErrorType type)
        {
            ctlErrorProvider.SetError(control, msg, type);
        }

        public static void Validate_Clear(BaseEdit control, ref DXErrorProvider ctlErrorProvider)
        {
            ctlErrorProvider.SetError(control, "");
        }

        public static bool Validate_Empty(BaseEdit control, ref DXErrorProvider ctlErrorProvider, string msg, ErrorType type)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
            {
                ctlErrorProvider.SetError(control, msg, ErrorType.Critical);
                return false;
            }
            else
            {
                ctlErrorProvider.SetError(control, "");
                return true;
            }
        }

        public static bool Validate_EmptyOrNotMorethan(BaseEdit control, int compareValue, ref DXErrorProvider ctlErrorProvider, string msg, ErrorType type)
        {
            if (control.Text == null || control.Text.Trim().Length == 0 || Convert.ToInt32(control.EditValue, NumberFormatInfo.InvariantInfo) <= compareValue)
            {
                ctlErrorProvider.SetError(control, msg, ErrorType.Critical);
                return false;
            }
            else
            {
                ctlErrorProvider.SetError(control, "");
                return true;
            }
        }
    }
}
