using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public class Encryption
    {
        public static string Encrypt(String s, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged rijn = new RijndaelManaged();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = rijn.CreateEncryptor(key, IV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(s);
                        }
                    }
                }

                result = System.Convert.ToBase64String(msEncrypt.ToArray());
            }

            rijn.Clear();

            return result;
        }

        public static string Decrypt(String s, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged rijn = new RijndaelManaged();
            using (MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(s)))
            {
                using (ICryptoTransform decryptor = rijn.CreateDecryptor(key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            result = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            rijn.Clear();
            return result;
        }

    }
}
