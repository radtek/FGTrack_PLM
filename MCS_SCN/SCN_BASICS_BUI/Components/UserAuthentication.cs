using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public class UserAuthentication
    {
        public UserAuthentication()
        {
            this.encryptKey = Encoding.ASCII.GetBytes(MobileConfiguration.Configuration.Settings["EncryptKey"].ToString());
            this.encryptIV = Encoding.ASCII.GetBytes(MobileConfiguration.Configuration.Settings["EncryptIV"].ToString());
        }

        #region Variable Member

        private byte[] encryptKey;
        private byte[] encryptIV;

        #endregion

        #region Property Member

        public string USER_NAME
        {
            get
            {
                return Encryption.Decrypt(MobileConfiguration.Configuration.Settings["UserAuthentication"].ToString(), this.encryptKey, this.encryptIV);
            }
        }
        public string User_PASS
        {
            get
            {
                return Encryption.Decrypt(MobileConfiguration.Configuration.Settings["PassAuthentication"].ToString(), this.encryptKey, this.encryptIV);
            }
        }
        public string User_DOMAIN
        {
            get
            {
                return Encryption.Decrypt(MobileConfiguration.Configuration.Settings["Domain"].ToString(), this.encryptKey, this.encryptIV);
            }
        }

        #endregion
    }
}
