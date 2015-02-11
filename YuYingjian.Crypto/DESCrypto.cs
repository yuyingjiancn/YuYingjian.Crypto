using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YuYingjian.Crypto
{
    public static class DESCrypto
    {
        public static string Key = "WaNgJiA!";
        public static string EncryptKey = "^%$&#*@!";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string ToDES(this string encryptString)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey.Substring(0, 8));
            byte[] rgbIV = Encoding.UTF8.GetBytes(Key);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string FromDES(this string decryptString)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey);
            byte[] rgbIV = Encoding.UTF8.GetBytes(Key);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
