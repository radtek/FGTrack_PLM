using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace HTN.BITS.FGTDB.BEL
{
    [Serializable]
    public class FormConfigState
    {
        public FormConfigState()
        {
        }

        private string _FormName;
        private string _GridControl;
        private string _LastDefaultView;
        private string _PivotGridControl;
        private Size _FormSize;
        private Point _FormLocation;

        public string FormName
        {
            get
            {
                return _FormName;
            }
            set
            {
                if (_FormName == value)
                    return;
                _FormName = value;
            }
        }
        public string GridControl
        {
            get
            {
                return _GridControl;
            }
            set
            {
                if (_GridControl == value)
                    return;
                _GridControl = value;
            }
        }
        public string LastDefaultView
        {
            get
            {
                return _LastDefaultView;
            }
            set
            {
                if (_LastDefaultView == value)
                    return;
                _LastDefaultView = value;
            }
        }

        public string PivotGridControl
        {
            get
            {
                return _PivotGridControl;
            }
            set
            {
                if (_PivotGridControl == value)
                    return;
                _PivotGridControl = value;
            }
        }

        public Size FormSize
        {
            get
            {
                return _FormSize;
            }
            set
            {
                if (_FormSize == value)
                    return;
                _FormSize = value;
            }
        }
        public Point FormLocation
        {
            get
            {
                return _FormLocation;
            }
            set
            {
                if (_FormLocation == value)
                    return;
                _FormLocation = value;
            }
        }
    }
}
