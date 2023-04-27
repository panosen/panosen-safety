using Panosen.Toolkit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if DEBUG
using Panosen.Safety;
#else
using System.IO;
#endif

namespace Panosen.Safety
{
    /// <summary>
    /// 加密保存文件
    /// </summary>
    public class SafeFile
    {
        /// <summary>
        /// 读取所有字节
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string path, SecretKey secretKey)
        {
            var cipherBytes = File.ReadAllBytes(path);

            return Crypto.AESDecrypt(cipherBytes, secretKey.AesKey, secretKey.AesIV);
        }

        /// <summary>
        /// 写入所有字节
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllBytes(string path, byte[] bytes, SecretKey secretKey)
        {
            var cipherBytes = Crypto.AESEncrypt(bytes, secretKey.AesKey, secretKey.AesIV);

            File.WriteAllBytes(path, cipherBytes);
        }

        /// <summary>
        /// 异步读取所有字节
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadAllBytesAsync(string path, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var cipherBytes = await File.ReadAllBytesAsync(path, cancellationToken);

            return Crypto.AESDecrypt(cipherBytes, secretKey.AesKey, secretKey.AesIV);
        }

        /// <summary>
        /// 异步写入所有字节
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task WriteAllBytesAsync(string path, byte[] bytes, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var cipherBytes = Crypto.AESEncrypt(bytes, secretKey.AesKey, secretKey.AesIV);

            return File.WriteAllBytesAsync(path, cipherBytes, cancellationToken);
        }

        /// <summary>
        /// 读取所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path, SecretKey secretKey)
        {
            return ReadAllLines(path, Encoding.UTF8, secretKey);
        }

        /// <summary>
        /// 读取所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path, Encoding encoding, SecretKey secretKey)
        {
            var lines = File.ReadAllLines(path);
            return ToPlainLines(lines, encoding, secretKey);
        }

        /// <summary>
        /// 异步读取所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadAllLinesAsync(string path, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var lines = await File.ReadAllLinesAsync(path, cancellationToken);
            return ToPlainLines(lines, Encoding.UTF8, secretKey);
        }

        /// <summary>
        /// 异步读取所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var lines = await File.ReadAllLinesAsync(path, cancellationToken);
            return ToPlainLines(lines, encoding, secretKey);
        }

        /// <summary>
        /// 写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllLines(string path, string[] lines, SecretKey secretKey)
        {
            WriteAllLines(path, lines, Encoding.UTF8, secretKey);
        }

        /// <summary>
        /// 写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllLines(string path, IEnumerable<string> lines, SecretKey secretKey)
        {
            WriteAllLines(path, lines, Encoding.UTF8, secretKey);
        }

        /// <summary>
        /// 写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllLines(string path, string[] lines, Encoding encoding, SecretKey secretKey)
        {
            var cipherLines = ToCipherLines(lines, encoding, secretKey);
            File.WriteAllLines(path, cipherLines);
        }

        /// <summary>
        /// 写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllLines(string path, IEnumerable<string> lines, Encoding encoding, SecretKey secretKey)
        {
            var cipherLines = ToCipherLines(lines, encoding, secretKey);
            File.WriteAllLines(path, cipherLines);
        }

        /// <summary>
        /// 异步写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> lines, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            await WriteAllLinesAsync(path, lines, Encoding.UTF8, secretKey, cancellationToken);
        }

        /// <summary>
        /// 异步写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> lines, Encoding encoding, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var cipherLines = ToCipherLines(lines, encoding, secretKey);
            await File.WriteAllLinesAsync(path, cipherLines, cancellationToken);
        }




        /// <summary>
        /// 读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string ReadAllText(string path, SecretKey secretKey)
        {
            return ReadAllText(path, Encoding.UTF8, secretKey);
        }

        /// <summary>
        /// 读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string ReadAllText(string path, Encoding encoding, SecretKey secretKey)
        {
            var cipherText = File.ReadAllText(path);
            return ToPlainText(cipherText, encoding, secretKey);
        }

        /// <summary>
        /// 异步读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> ReadAllTextAsync(string path, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            return await ReadAllTextAsync(path, Encoding.UTF8, secretKey, cancellationToken);
        }

        /// <summary>
        /// 异步读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> ReadAllTextAsync(string path, Encoding encoding, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var cipherText = await File.ReadAllTextAsync(path, cancellationToken);
            return ToPlainText(cipherText, encoding, secretKey);
        }

        /// <summary>
        /// 写入读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="secretKey"></param>
        public static void WriteAllText(string path, string text, SecretKey secretKey)
        {
            var cipherText = ToCipherText(path, Encoding.UTF8, secretKey);
            File.WriteAllText(path, cipherText);
        }

        /// <summary>
        /// 写入读取所有文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task WriteAllTextAsync(string path, string text, SecretKey secretKey, CancellationToken cancellationToken = default)
        {
            var cipherText = ToCipherText(text, Encoding.UTF8, secretKey);
            await File.WriteAllTextAsync(path, cipherText, cancellationToken);
        }

        private static string[] ToPlainLines(string[] lines, Encoding encoding, SecretKey secretKey)
        {
            if (lines == null)
            {
                return null;
            }
            var newLines = new List<string>();
            foreach (var line in lines)
            {
                var cipherBytes = Convert.FromBase64String(line);
                var plainBytes = Crypto.AESDecrypt(cipherBytes, secretKey.AesKey, secretKey.AesIV);
                var plainText = encoding.GetString(plainBytes);
                newLines.Add(plainText);
            }
            return newLines.ToArray();
        }

        private static string[] ToCipherLines(IEnumerable<string> lines, Encoding encoding, SecretKey secretKey)
        {
            if (lines == null)
            {
                return null;
            }
            var newLines = new List<string>();
            foreach (var line in lines)
            {
                var plainBytes = encoding.GetBytes(line);
                var cipherBytes = Crypto.AESEncrypt(plainBytes, secretKey.AesKey, secretKey.AesIV);
                var cipherText = Convert.ToBase64String(cipherBytes);
                newLines.Add(cipherText);
            }
            return newLines.ToArray();
        }
        private static string ToPlainText(string text, Encoding encoding, SecretKey secretKey)
        {
            var cipherBytes = Convert.FromBase64String(text);
            var plainBytes = Crypto.AESDecrypt(cipherBytes, secretKey.AesKey, secretKey.AesIV);
            var plainText = encoding.GetString(plainBytes);
            return plainText;
        }
        private static string ToCipherText(string text, Encoding encoding, SecretKey secretKey)
        {
            var plainBytes = encoding.GetBytes(text);
            var cipherBytes = Crypto.AESEncrypt(plainBytes, secretKey.AesKey, secretKey.AesIV);
            var cipherText = Convert.ToBase64String(cipherBytes);
            return cipherText;
        }
    }
}