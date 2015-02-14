using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YuYingjian.Crypto
{
    public static class DESCrypto
    {
        private static byte[] _rgbKey = { 0xE9, 0x87, 0x91, 0xE5, 0xB0, 0x91, 0xE6, 0x96 };
        private static byte[] _rgbIV = { 0x90, 0xE5, 0x81, 0xB6, 0xE5, 0x83, 0x8F, 0x21 };

        public static void SetKeyAndIV(string key = "金少斐偶像!前8个字节", string iv = "金少斐偶像!后8个字节")
        {
            if (_rgbKey == null || _rgbIV == null)
            {
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var ivBytes = Encoding.UTF8.GetBytes(iv);

                if (keyBytes.Length < 8 || ivBytes.Length < 8)
                {
                    throw new Exception("Key与IV的长度不够，必须能够转换为至少8个字节。");
                }

                _rgbKey = new byte[8];
                _rgbIV = new byte[8];

                Array.Copy(keyBytes, 0, _rgbKey, 0, 8);
                Array.Copy(ivBytes, 0, _rgbIV, 0, 8);
            }
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string ToDES(this string encryptString)
        {
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
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
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

            byte[] inputByteArray;

            try
            {
                inputByteArray = Convert.FromBase64String(decryptString);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, DCSP.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write))
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
