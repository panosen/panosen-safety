using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Safety.MSTest
{
    public static class TestHelper
    {
        static readonly Random random = new Random();

        public static byte[] GenBytes()
        {
            var bytes = new byte[random.Next(10, 20)];
            random.NextBytes(bytes);
            return bytes;
        }

        public static string[] GenLines()
        {
            int length = random.Next(5, 10);
            var lines = new List<string>();
            for (int i = 0; i < length; i++)
            {
                lines.Add(Guid.NewGuid().ToString("D"));
            }
            return lines.ToArray();
        }
        public static string GenText()
        {
            return Guid.NewGuid().ToString("D");
        }

        public static bool Same(byte[] bytes1, byte[] bytes2)
        {
            var text1 = Convert.ToBase64String(bytes1);
            var text2 = Convert.ToBase64String(bytes2);
            return text1.Equals(text2);
        }

        public static bool Same(string[] lines1, string[] lines2)
        {
            var text1 = string.Join(";", lines1);
            var text2 = string.Join(";", lines2);
            return text1.Equals(text2);
        }

        public static bool Same(string text1, string text2)
        {
            return text1.Equals(text2);
        }
    }
}
