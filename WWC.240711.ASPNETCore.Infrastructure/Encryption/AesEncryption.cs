using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Infrastructure.Encryption
{
    public class AesEncryption
    {
        private readonly string _key;

        public AesEncryption(string key)
        {
            // 将密钥转换为字节数组，并确保长度为 16、24 或 32 字节
            _key = key.Length > 32 ? key.Substring(0, 32) : key.PadRight(32, ' ');
        }

        /// <summary>
        /// 加密 byte[] 类型
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.GenerateIV(); // 生成随机 IV

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // 先写入 IV
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 解密 byte[] 类型
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] cipherText)
        {
            // 提取 IV
            var iv = new byte[16]; // AES IV 长度为 16 字节
            Array.Copy(cipherText, 0, iv, 0, iv.Length);
            var cipher = new byte[cipherText.Length - iv.Length];
            Array.Copy(cipherText, iv.Length, cipher, 0, cipher.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        cs.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 加密文本类型
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.GenerateIV(); // 生成随机 IV

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // 先写入 IV
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密文本类型
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            // 提取 IV
            var iv = new byte[16]; // AES IV 长度为 16 字节
            Array.Copy(fullCipher, 0, iv, 0, iv.Length);
            var cipher = new byte[fullCipher.Length - iv.Length];
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

    }
}
