using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YuYingjian.Crypto
{
    public static class BASE64Crypto
    {
        public static string ToBASE64(this string encryptString)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptString));
            }
            catch
            {
                return encryptString;
            }
        }

        public static string FromBASE64(this string decryptString)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(decryptString));
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
