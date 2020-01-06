using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Core.AES
{
    public static class AesSecurity
    {

        #region AES 256位加密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plaintextData">明文的数据</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AesEncrypt(string plaintextData, string key, string iv = "0102030405060708", CipherMode model = CipherMode.CBC)
        {
            try
            {
                if (string.IsNullOrEmpty(plaintextData)) return null;
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(plaintextData);
                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    IV = Encoding.UTF8.GetBytes(iv),
                    Mode = model,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rm.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AesDecrypt(string ciphertext, string key, string iv = "0102030405060708", CipherMode model = CipherMode.CBC)
        {
            try
            {
                if (string.IsNullOrEmpty(ciphertext)) return null;
                Byte[] toEncryptArray = Convert.FromBase64String(ciphertext);
                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    IV = Encoding.UTF8.GetBytes(iv),
                    Mode = model,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}
