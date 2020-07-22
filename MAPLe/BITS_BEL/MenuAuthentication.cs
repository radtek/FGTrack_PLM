using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.BEL.PLASESS
{
    public class MenuAuthentication
    {
        private string _programType;
        private string _programTypeName;
        private string _programTypeImage;
        private string _programid;
        private string _programKey;
        private string _programname;
        private string _iconImage;

        public MenuAuthentication()
        {
        }

        public string ProgramType
        {
            get
            {
                return this._programType;
            }

            set
            {
                this._programType = value;
            }
        }

        public string ProgramTypeName
        {
            get
            {
                return this._programTypeName;
            }

            set
            {
                this._programTypeName = value;
            }
        }

        public string ProgramTypeImage
        {
            get
            {
                return this._programTypeImage;
            }

            set
            {
                this._programTypeImage = value;
            }
        }

        public string ProgramID
        {
            get
            {
                return this._programid;
            }

            set
            {
                this._programid = value;
            }
        }

        public string ProgramKey
        {
            get
            {
                return this._programKey;
            }

            set
            {
                this._programKey = value;
            }
        }

        public string ProgramName
        {
            get
            {
                return this._programname;
            }

            set
            {
                this._programname = value;
            }
        }

        public string IconImage
        {
            get
            {
                return this._iconImage;
            }

            set
            {
                this._iconImage = value;
            }
        }



    }
}
