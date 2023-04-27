using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panosen.Safety.MSTest
{
    [TestClass]
    public class SafeFileTest
    {
        [TestMethod]
        public async Task Test()
        {
            var aesKey = new byte[32];
            var aesIV = new byte[16];

            Random random = new Random();
            random.NextBytes(aesKey);
            random.NextBytes(aesIV);

            SecretKey secretKey = new SecretKey();
            secretKey.AesKey = aesKey;
            secretKey.AesIV = aesIV;

            Dictionary<string, byte[]> BytesMap = new Dictionary<string, byte[]>();
            Dictionary<string, string[]> LinesMap = new Dictionary<string, string[]>();
            Dictionary<string, string> TextMap = new Dictionary<string, string>();

            for (int i = 0; i < 3; i++)
            {
                byte[] bytes = TestHelper.GenBytes();
                SafeFile.WriteAllBytes("a" + i, bytes, secretKey);
                BytesMap["a" + i] = bytes;
            }
            for (int i = 0; i < 4; i++)
            {
                string[] lines = TestHelper.GenLines();
                SafeFile.WriteAllLines("b" + i, lines, secretKey);
                LinesMap["b" + i] = lines;
            }
            for (int i = 0; i < 5; i++)
            {
                string text = TestHelper.GenText();
                SafeFile.WriteAllText("c" + i, text, secretKey);
                TextMap["c" + i] = text;
            }

#if NET6_0
            for (int i = 0; i < 6; i++)
            {
                byte[] bytes = TestHelper.GenBytes();
                await SafeFile.WriteAllBytesAsync("d" + i, bytes, secretKey);
                BytesMap["d" + i] = bytes;
            }
            for (int i = 0; i < 7; i++)
            {
                string[] lines = TestHelper.GenLines();
                await SafeFile.WriteAllLinesAsync("e" + i, lines, secretKey);
                LinesMap["e" + i] = lines;
            }
            for (int i = 0; i < 8; i++)
            {
                string text = TestHelper.GenText();
                await SafeFile.WriteAllTextAsync("f" + i, text, secretKey);
                TextMap["f" + i] = text;
            }

#endif

            for (int i = 0; i < 3; i++)
            {
                var bytes = SafeFile.ReadAllBytes("a" + i, secretKey);
                TestHelper.Same(bytes, BytesMap["a" + i]);
            }
            for (int i = 0; i < 4; i++)
            {
                var lines = SafeFile.ReadAllLines("b" + i, secretKey);
                TestHelper.Same(lines, LinesMap["b" + i]);
            }
            for (int i = 0; i < 5; i++)
            {
                var text = SafeFile.ReadAllText("c" + i, secretKey);
                TestHelper.Same(text, TextMap["c" + i]);
            }

#if NET6_0
            for (int i = 0; i < 6; i++)
            {
                var bytes = await SafeFile.ReadAllBytesAsync("d" + i, secretKey);
                TestHelper.Same(bytes, BytesMap["d" + i]);
            }
            for (int i = 0; i < 7; i++)
            {
                var lines = await SafeFile.ReadAllLinesAsync("e" + i, secretKey);
                TestHelper.Same(lines, LinesMap["e" + i]);
            }
            for (int i = 0; i < 8; i++)
            {
                var text = await SafeFile.ReadAllTextAsync("f" + i, secretKey);
                TestHelper.Same(text, TextMap["f" + i]);
            }

#endif

        }
    }
}