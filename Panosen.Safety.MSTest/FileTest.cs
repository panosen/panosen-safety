#if DEBUG

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Safety.MSTest
{
    [TestClass]
    public class FileTest
    {
        [TestMethod]
        public async Task Test()
        {
            Dictionary<string, byte[]> BytesMap = new Dictionary<string, byte[]>();
            Dictionary<string, string[]> LinesMap = new Dictionary<string, string[]>();
            Dictionary<string, string> TextMap = new Dictionary<string, string>();

            for (int i = 0; i < 3; i++)
            {
                byte[] bytes = TestHelper.GenBytes();
                File.WriteAllBytes("a" + i, bytes);
                BytesMap["a" + i] = bytes;
            }
            for (int i = 0; i < 4; i++)
            {
                string[] lines = TestHelper.GenLines();
                File.WriteAllLines("b" + i, lines);
                LinesMap["b" + i] = lines;
            }
            for (int i = 0; i < 5; i++)
            {
                string text = TestHelper.GenText();
                File.WriteAllText("c" + i, text);
                TextMap["c" + i] = text;
            }
            for (int i = 0; i < 6; i++)
            {
                byte[] bytes = TestHelper.GenBytes();
                await File.WriteAllBytesAsync("d" + i, bytes);
                BytesMap["d" + i] = bytes;
            }
            for (int i = 0; i < 7; i++)
            {
                string[] lines = TestHelper.GenLines();
                await File.WriteAllLinesAsync("e" + i, lines);
                LinesMap["e" + i] = lines;
            }
            for (int i = 0; i < 8; i++)
            {
                string text = TestHelper.GenText();
                await File.WriteAllTextAsync("f" + i, text);
                TextMap["f" + i] = text;
            }

            for (int i = 0; i < 3; i++)
            {
                var bytes = File.ReadAllBytes("a" + i);
                TestHelper.Same(bytes, BytesMap["a" + i]);
            }
            for (int i = 0; i < 4; i++)
            {
                var lines = File.ReadAllLines("b" + i);
                TestHelper.Same(lines, LinesMap["b" + i]);
            }
            for (int i = 0; i < 5; i++)
            {
                var text = File.ReadAllText("c" + i);
                TestHelper.Same(text, TextMap["c" + i]);
            }
            for (int i = 0; i < 6; i++)
            {
                var bytes = await File.ReadAllBytesAsync("d" + i);
                TestHelper.Same(bytes, BytesMap["d" + i]);
            }
            for (int i = 0; i < 7; i++)
            {
                var lines = await File.ReadAllLinesAsync("e" + i);
                TestHelper.Same(lines, LinesMap["e" + i]);
            }
            for (int i = 0; i < 8; i++)
            {
                var text = await File.ReadAllTextAsync("f" + i);
                TestHelper.Same(text, TextMap["f" + i]);
            }

        }
    }
}

#endif
