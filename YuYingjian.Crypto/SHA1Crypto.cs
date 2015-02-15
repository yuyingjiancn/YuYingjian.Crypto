using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YuYingjian.Crypto
{
    public static class SHA1Crypto
    {
        public static string ToSHA1(this string encryptString)
        {
            try
            {
                using (SHA1 sha1 = new SHA1CryptoServiceProvider())
                {
                    byte[] data = sha1.ComputeHash(Encoding.UTF8.GetBytes(encryptString));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString();
                }
            }
            catch
            {
                return encryptString;
            }
            
        }
    }
}
