using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Safety.MSTest
{
    [TestClass]
    public class SecretKeyTest
    {
        [TestMethod]
        public void Test()
        {
            var secretKey = SecretKey.Create();

            var base64 = secretKey.ToBase64();

            var newSecretKey = SecretKey.FromBase64(base64);
            TestHelper.Same(secretKey.AesKey, newSecretKey.AesKey);
            TestHelper.Same(secretKey.AesIV, newSecretKey.AesIV);

            var newBase64 = newSecretKey.ToBase64();
            Assert.AreEqual(base64, newBase64);
        }

        [TestMethod]
        public void Test2()
        {
            string password = "abc123";

            SecretKey secretKey = SecretKey.Create();

            var cipherBase64 = secretKey.ToBase64(password);

            SecretKey newSecretKey = SecretKey.FromBase64(cipherBase64, password);

            TestHelper.Same(secretKey.AesKey, newSecretKey.AesKey);
            TestHelper.Same(secretKey.AesIV, newSecretKey.AesIV);
        }
    }
}
