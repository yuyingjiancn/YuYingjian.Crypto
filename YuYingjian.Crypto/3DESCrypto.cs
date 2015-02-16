using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YuYingjian.Crypto
{
    public static class ThreeDESCrypto
    {
        private static byte[] _rgbKey1 = { 0xE9, 0x87, 0x91, 0xE5, 0xB0, 0x91, 0xE6, 0x96 };
        private static byte[] _rgbIV1 = { 0x90, 0xE5, 0x81, 0xB6, 0xE5, 0x83, 0x8F, 0x21 };
        private static byte[] _rgbKey2 = { 0xE9, 0x87, 0x91, 0xE5, 0xB0, 0x91, 0xE6, 0x96 };
        private static byte[] _rgbIV2 = { 0x90, 0xE5, 0x81, 0xB6, 0xE5, 0x83, 0x8F, 0x21 };
        private static byte[] _rgbKey3 = { 0xE9, 0x87, 0x91, 0xE5, 0xB0, 0x91, 0xE6, 0x96 };
        private static byte[] _rgbIV3 = { 0x90, 0xE5, 0x81, 0xB6, 0xE5, 0x83, 0x8F, 0x21 };

        public static void SetKeyAndIV(string key1 = "金少斐偶像!前8个字节", string iv1 = "金少斐偶像!后8个字节", string key2 = "金少斐偶像!前8个字节", string iv2 = "金少斐偶像!后8个字节", string key3 = "金少斐偶像!前8个字节", string iv3 = "金少斐偶像!后8个字节")
        {
            var keyBytes1 = Encoding.UTF8.GetBytes(key1);
            var ivBytes1 = Encoding.UTF8.GetBytes(iv1);

            if (keyBytes1.Length < 8 || ivBytes1.Length < 8)
            {
                throw new Exception("Key1与IV1的长度不够，必须能够转换为至少8个字节。");
            }

            _rgbKey1 = new byte[8];
            _rgbIV1 = new byte[8];

            Array.Copy(keyBytes1, 0, _rgbKey1, 0, 8);
            Array.Copy(ivBytes1, 0, _rgbIV1, 0, 8);

            var keyBytes2 = Encoding.UTF8.GetBytes(key2);
            var ivBytes2 = Encoding.UTF8.GetBytes(iv2);

            if (keyBytes2.Length < 8 || ivBytes2.Length < 8)
            {
                throw new Exception("Key2与IV2的长度不够，必须能够转换为至少8个字节。");
            }

            _rgbKey2 = new byte[8];
            _rgbIV2 = new byte[8];

            Array.Copy(keyBytes2, 0, _rgbKey2, 0, 8);
            Array.Copy(ivBytes2, 0, _rgbIV2, 0, 8);

            var keyBytes3 = Encoding.UTF8.GetBytes(key3);
            var ivBytes3 = Encoding.UTF8.GetBytes(iv3);

            if (keyBytes3.Length < 8 || ivBytes3.Length < 8)
            {
                throw new Exception("Key3与IV3的长度不够，必须能够转换为至少8个字节。");
            }

            _rgbKey3 = new byte[8];
            _rgbIV3 = new byte[8];

            Array.Copy(keyBytes3, 0, _rgbKey3, 0, 8);
            Array.Copy(ivBytes3, 0, _rgbIV3, 0, 8);
        }

        public static string To3DES(this string encryptString)
        {
            var e1 = toDES(encryptString, _rgbKey1, _rgbIV1);
            var e2 = toDES(e1, _rgbKey2, _rgbIV2);
            var e3 = toDES(e2, _rgbKey3, _rgbIV3);
            return e3;
        }

        public static string From3DES(this string decryptString)
        {
            var d3 = fromDES(decryptString, _rgbKey3, _rgbIV3);
            var d2 = fromDES(d3, _rgbKey2, _rgbIV2);
            var d1 = fromDES(d2, _rgbKey1, _rgbIV1);
            return d1;
        }

        private static string toDES(string encryptString, byte[] rgbKey, byte[] rgbIV)
        {
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        private static string fromDES(string decryptString, byte[] rgbKey, byte[] rgbIV)
        {
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

            byte[] inputByteArray;

            try
            {
                inputByteArray = Convert.FromBase64String(decryptString);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
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
