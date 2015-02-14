using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YuYingjian.Crypto
{
    public static class AESCrypto
    {
        private static byte[] _rgbKey;
        private static byte[] _rgbIV;

        public static void SetKeyAndIV(string key = "虞颖健是在海宁一中工作", string iv = "虞颖健是高中老师")
        {
            if (_rgbKey == null || _rgbIV == null)
            {
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var ivBytes = Encoding.UTF8.GetBytes(iv);

                if (keyBytes.Length < 32 || ivBytes.Length < 16)
                {
                    throw new Exception("Key与IV的长度不够，必须能够转换为至少8个Byte");
                }

                _rgbKey = new byte[32];
                _rgbIV = new byte[16];

                Array.Copy(keyBytes, 0, _rgbKey, 0, 32);
                Array.Copy(ivBytes, 0, _rgbIV, 0, 16);
            }
        }

        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string ToAES(this string encryptString)
        {
            var ACSP = new AesCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, ACSP.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// AES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string FromAES(this string decryptString)
        {
            var ACSP = new AesCryptoServiceProvider();

            byte[] inputByteArray;

            try
            {
                inputByteArray = Convert.FromBase64String(decryptString);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, ACSP.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
