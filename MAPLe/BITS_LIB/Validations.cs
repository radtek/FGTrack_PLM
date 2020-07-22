using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Globalization;

namespace HTN.BITS.LIB
{
    public class Validations
    {
        public static void Validate_EmptyStringRule(BaseEdit control, ref DXErrorProvider ctlErrorProvider)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
            {
                ctlErrorProvider.SetError(control, "This field can't be empty", ErrorType.Critical);
            }
            else
            {
                ctlErrorProvider.SetError(control, "");
            }
        }

        public static void Validate_EmptyOrNotEqual(BaseEdit control,string compareValue, ref DXErrorProvider ctlErrorProvider)
        {
            if (control.Text == null || control.Text.Trim().Length == 0 || !control.Text.Equals(compareValue))
            {
                ctlErrorProvider.SetError(control, "This field is Invalid", ErrorType.Critical);
            }
            else
            {
                ctlErrorProvider.SetError(control, "");
            }
        }

        public static void Validate_EmptyOrNotMorethan(BaseEdit control, int compareValue, ref DXErrorProvider ctlErrorProvider)
        {
            if (control.Text == null || control.Text.Trim().Length == 0 || Convert.ToInt32(control.EditValue, NumberFormatInfo.InvariantInfo) <= compareValue)
            {
                ctlErrorProvider.SetError(control, "This field is Invalid", ErrorType.Critical);
            }
            else
            {
                ctlErrorProvider.SetError(control, "");
            }
        }

        public static bool Validate_ValueOverAndSTDMaching(BaseEdit control, int compareValue, int stdValue, ref DXErrorProvider ctlErrorProvider)
        {
            
            if (control.Text == null || control.Text.Trim().Length == 0)
            {
                ctlErrorProvider.SetError(control, "This field is Invalid", ErrorType.Critical);
                return false;
            }
            else
            {
                int controlValue = Convert.ToInt32(control.EditValue);
                if (controlValue <= 0)
                {
                    ctlErrorProvider.SetError(control, "Qty can't be zero", ErrorType.Critical);
                    return false;
                }

                if (controlValue >= compareValue)
                {
                    ctlErrorProvider.SetError(control, "Qty must be less than Label Qty", ErrorType.Critical);
                    return false;
                }
                //Modified by pravin on 12-10-2010
                //if ((controlValue % stdValue) != 0)
                //{
                //    ctlErrorProvider.SetError(control, "Qty must be multiple of " + stdValue.ToString(), ErrorType.Critical);
                //    return false;
                //}


                //no error
                ctlErrorProvider.SetError(control, "");
                return true;

                
            }
        }

        public static void Validate_DateNotEqual(BaseEdit control, DateTime controlDate, DateTime compareDate, ref DXErrorProvider ctlErrorProvider)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
            {
                ctlErrorProvider.SetError(control, "This field is Invalid", ErrorType.Critical);
            }
            else
            {
                if (DateTime.Compare(controlDate.Date, compareDate.Date) != 0)
                {
                    ctlErrorProvider.SetError(control, "This field is Invalid", ErrorType.Critical);
                }
                else
                {
                    ctlErrorProvider.SetError(control, "");
                }
            }
        }
    }
}
