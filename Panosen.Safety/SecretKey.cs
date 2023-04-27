using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panosen.Toolkit;

namespace Panosen.Safety
{
    /// <summary>
    /// 密钥
    /// </summary>
    public partial class SecretKey
    {
        /// <summary>
        /// 长度16, 24, 32
        /// </summary>
        public byte[] AesKey { get; set; }

        /// <summary>
        /// 长度16
        /// </summary>
        public byte[] AesIV { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public partial class SecretKey
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 创建随机密钥
        /// </summary>
        public static SecretKey Create()
        {
            var secretKey = new SecretKey();
            secretKey.AesKey = new byte[32];
            secretKey.AesIV = new byte[16];

            random.NextBytes(secretKey.AesKey);
            random.NextBytes(secretKey.AesIV);

            return secretKey;
        }

        #region 明文导入导出

        /// <summary>
        /// 从明文base64导入密钥
        /// </summary>
        /// <param name="base64">明文base64</param>
        /// <returns></returns>
        public static SecretKey FromBase64(string base64)
        {
            return FromBytes(Convert.FromBase64String(base64));
        }

        private static SecretKey FromBytes(byte[] bytes)
        {
            var secretKey = new SecretKey();
            secretKey.AesKey = new byte[bytes.Length - 16];
            secretKey.AesIV = new byte[16];

            Array.Copy(bytes, 0, secretKey.AesKey, 0, bytes.Length - 16);
            Array.Copy(bytes, bytes.Length - 16, secretKey.AesIV, 0, 16);

            return secretKey;
        }

        /// <summary>
        /// 导出为明文base64
        /// </summary>
        /// <returns></returns>
        public string ToBase64()
        {
            return Convert.ToBase64String(ToBytes());
        }

        private byte[] ToBytes()
        {
            var bytes = new byte[this.AesKey.Length + this.AesIV.Length];
            Array.Copy(this.AesKey, 0, bytes, 0, this.AesKey.Length);
            Array.Copy(this.AesIV, 0, bytes, this.AesKey.Length, this.AesIV.Length);
            return bytes;
        }

        #endregion

        #region 加密导入导出

        /// <summary>
        /// 从密文base64导入密钥
        /// </summary>
        /// <param name="base64">密文base64</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static SecretKey FromBase64(string base64, string password)
        {
            var bytes = Convert.FromBase64String(base64);

            var pbkdf2 = new byte[24];
            Array.Copy(bytes, 0, pbkdf2, 0, pbkdf2.Length);
            if (!Crypto.PBKDF2Verify(password, pbkdf2))
            {
                return null;
            }

            var aesKey = new byte[32];
            Array.Copy(bytes, 0, aesKey, 0, aesKey.Length);

            var ciperBytes = new byte[bytes.Length - aesKey.Length];
            Array.Copy(bytes, aesKey.Length, ciperBytes, 0, ciperBytes.Length);

            var aesIv = Fixed(Encoding.UTF8.GetBytes(password), 16);

            var plainBytes = Crypto.AESDecrypt(ciperBytes, aesKey, aesIv);

            return FromBytes(plainBytes);
        }

        /// <summary>
        /// 导出为密文base64
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string ToBase64(string password)
        {
            var plainBytes = ToBytes();

            var aesKey = PaddingEnd(Crypto.PBKDF2DeriveKey(password), 32);
            var aesIv = Fixed(Encoding.UTF8.GetBytes(password), 16);

            var cipherBytes = Crypto.AESEncrypt(plainBytes, aesKey, aesIv);

            var bytes = new byte[aesKey.Length + cipherBytes.Length];
            Array.Copy(aesKey, 0, bytes, 0, aesKey.Length);
            Array.Copy(cipherBytes, 0, bytes, aesKey.Length, cipherBytes.Length);

            return Convert.ToBase64String(bytes);
        }

        #endregion

        /// <summary>
        /// 将字节数组扩展到指定的长度
        /// </summary>
        private static byte[] PaddingEnd(byte[] bytes, int length)
        {
            byte[] newBytes = new byte[length];

            byte[] paddingBytes = new byte[length - bytes.Length];
            random.NextBytes(paddingBytes);

            Array.Copy(bytes, 0, newBytes, 0, bytes.Length);
            Array.Copy(paddingBytes, 0, newBytes, bytes.Length, paddingBytes.Length);

            return newBytes;
        }

        private static byte[] Fixed(byte[] bytes, int length)
        {
            var newBytes = new byte[length];
            Array.Copy(bytes, 0, newBytes, 0, Math.Min(bytes.Length, length));
            return newBytes;
        }
    }
}
