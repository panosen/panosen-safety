#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Panosen.Safety
{
    public static class File
    {
        private static readonly Dictionary<string, byte[]> BytesMap = new Dictionary<string, byte[]>();
        private static readonly Dictionary<string, string[]> LinesMap = new Dictionary<string, string[]>();
        private static readonly Dictionary<string, string> TextMap = new Dictionary<string, string>();

        public static byte[] ReadAllBytes(string path)
        {
            if (BytesMap.ContainsKey(path))
            {
                return BytesMap[path];
            }
            return null;
        }
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            BytesMap[path] = bytes;
        }
        public static Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
        {
            if (BytesMap.ContainsKey(path))
            {
                return Task.FromResult(BytesMap[path]);
            }
            return Task.FromResult<byte[]>(null);
        }
        public static Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
        {
            BytesMap[path] = bytes;
            return Task.CompletedTask;
        }


        public static string[] ReadAllLines(string path)
        {
            if (LinesMap.ContainsKey(path))
            {
                return LinesMap[path];
            }
            return null;
        }
        public static void WriteAllLines(string path, string[] lines)
        {
            LinesMap[path] = lines;
        }
        public static Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default)
        {
            if (LinesMap.ContainsKey(path))
            {
                return Task.FromResult(LinesMap[path]);
            }
            return null;
        }
        public static Task WriteAllLinesAsync(string path, string[] lines, CancellationToken cancellationToken = default)
        {
            LinesMap[path] = lines;
            return Task.CompletedTask;
        }



        public static string ReadAllText(string path)
        {
            if (TextMap.ContainsKey(path))
            {
                return TextMap[path];
            }
            return null;
        }
        public static void WriteAllText(string path, string text)
        {
            TextMap[path] = text;
        }
        public static Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
        {
            if (TextMap.ContainsKey(path))
            {
                return Task.FromResult(TextMap[path]);
            }
            return null;
        }
        public static Task WriteAllTextAsync(string path, string text, CancellationToken cancellationToken = default)
        {
            TextMap[path] = text;
            return Task.CompletedTask;
        }
    }
}

#endif
