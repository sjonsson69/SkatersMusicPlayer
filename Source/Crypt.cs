using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Crypt
{
    class crypt
    {
        private const string S = "RGV0IMOkciBicmEgYXR0IGhhIGV0dCBsw6VuZ3QgbMO2c2Vub3JkJiBIZWxzdCBtZWQga29uc3RpZ2Ega29uc3TDpWtuaW5nc3Rlcm1lciBzb20gUXVhZCBvY2ggQ29yZW9Db25maXJtZWQ=";

        public static string kryptera(string s_Data, string s_Password, bool b_Encrypt)
        {
            if (s_Password == String.Empty)
            {
                byte[] data = Convert.FromBase64String(S);
                s_Password = Encoding.UTF8.GetString(data);
                // Dubbelkryptera lösenordet innan vi använder det!
                s_Password = kryptera(s_Password, S, true);
            }

            byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };
            PasswordDeriveBytes i_Pass = new PasswordDeriveBytes(s_Password, u8_Salt);

            Rijndael i_Alg = Rijndael.Create();
            i_Alg.Key = i_Pass.GetBytes(32);
            i_Alg.IV = i_Pass.GetBytes(16);

            ICryptoTransform i_Trans = (b_Encrypt) ? i_Alg.CreateEncryptor() : i_Alg.CreateDecryptor();

            MemoryStream i_Mem = new MemoryStream();
            CryptoStream i_Crypt = new CryptoStream(i_Mem, i_Trans, CryptoStreamMode.Write);


            try
            {
                byte[] u8_Data;
                if (b_Encrypt) u8_Data = Encoding.Unicode.GetBytes(s_Data);
                else u8_Data = Convert.FromBase64String(s_Data);

                i_Crypt.Write(u8_Data, 0, u8_Data.Length);
                i_Crypt.FlushFinalBlock();
                if (b_Encrypt) return Convert.ToBase64String(i_Mem.ToArray());
                else return Encoding.Unicode.GetString(i_Mem.ToArray());
            }
            catch { return null; }
        }
    }
}

